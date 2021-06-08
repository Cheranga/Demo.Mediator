using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Exceptions;
using Interesting.Mediator.Publisher;
using Interesting.Mediator.Services.Messages;
using Interesting.Mediator.Services.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Interesting.Mediator.Handlers
{
    public class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, Result<Customer>>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMediator mediator;
        private readonly IAsyncPublisher asyncPublisher;
        private readonly ILogger<UpdateCustomerRequestHandler> logger;

        public UpdateCustomerRequestHandler(ICustomerRepository customerRepository, IMediator mediator, IAsyncPublisher asyncPublisher, ILogger<UpdateCustomerRequestHandler> logger)
        {
            this.customerRepository = customerRepository;
            this.mediator = mediator;
            this.asyncPublisher = asyncPublisher;
            this.logger = logger;
        }
        
        public async Task<Result<Customer>> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var getCustomerOperation = await GetCustomerAsync(request);
            if (!getCustomerOperation.Status)
            {
                return Result<Customer>.Failure(getCustomerOperation.ErrorCode, getCustomerOperation.ValidationResult);
            }

            var updateCustomerOperation = await UpdateCustomerAsync(request);
            if (!updateCustomerOperation.Status)
            {
                return Result<Customer>.Failure(updateCustomerOperation.ErrorCode, updateCustomerOperation.ValidationResult);
            }

            var publishEventsOperation = await PublishEventsAsync(request, getCustomerOperation.Data, cancellationToken);
            if (!publishEventsOperation.Status)
            {
                return Result<Customer>.Failure(publishEventsOperation.ErrorCode, publishEventsOperation.ValidationResult);
            }

            // TODO: Discuss 
            // return await GetCustomerAsync(request);
            return Result<Customer>.Success(new Customer
            {
                Id = request.Id,
                Address = request.Address,
                Email = request.Email,
                Name = request.Name
            });
        }

        private async Task<Result<Customer>> GetCustomerAsync(UpdateCustomerRequest request)
        {
            var getCustomerOperation = await customerRepository.GetCustomerByEmailAsync(request.Email);
            if (!getCustomerOperation.Status)
            {
                return Result<Customer>.Failure(getCustomerOperation.ErrorCode, getCustomerOperation.ValidationResult);
            }

            var customer = getCustomerOperation.Data;
            if (customer == null)
            {
                return Result<Customer>.Failure("CUSTOMER_DOES_NOT_EXIST", "customer does not exist");
            }

            return getCustomerOperation;
        }

        private Task<Result> UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            var updateCustomerCommand = new UpdateCustomerCommand
            {
                Id = request.Id,
                Name = request.Name,
                Address = request.Address,
                Email = request.Email
            };

            return customerRepository.UpdateCustomerAsync(updateCustomerCommand);
        }

        private async Task<Result> PublishEventsAsync(UpdateCustomerRequest request, Customer customer, CancellationToken cancellationToken)
        {
            // TODO: In here check if the email has been updated, this will be better rather than the event handlers checking it by themselves.
            var customerEmailUpdatedEvent = new CustomerEmailUpdatedEvent
            {
                CustomerId = request.Id,
                NewEmail = request.Email,
                OldEmail = customer.Email,
                UpdatedDateTimeUtc = DateTime.UtcNow
            };

            var customerUpdatedEvent = new CustomerUpdatedEvent
            {
                Id = request.Id,
                Address = request.Address,
                Email = request.Email,
                Name = request.Name
            };

            var emailUpdatedOperation = await HandleCustomerEmailUpdatesAsync(customerEmailUpdatedEvent, customer, cancellationToken);
            if (!emailUpdatedOperation.Status)
            {
                return emailUpdatedOperation;
            }
            
            await asyncPublisher.Publish(customerUpdatedEvent, PublishStrategy.ParallelNoWait, cancellationToken);
            return Result.Success();

            // await Task.WhenAll(mediator.Publish(customerEmailUpdatedEvent, cancellationToken), mediator.Publish(customerUpdatedEvent, cancellationToken));
            // return Result.Success();

            // try
            // {
            //     await Task.WhenAll(mediator.Publish(customerEmailUpdatedEvent, cancellationToken), mediator.Publish(customerUpdatedEvent, cancellationToken));
            //     return Result.Success();
            // }
            // catch (Exception exception)
            // {
            //     logger.LogError(exception, "Error occurred when publishing customer updated events");
            //
            //     if (exception is Auth0UpdateUserException)
            //     {
            //         return Result.Failure("AUTH0_USER_UPDATE_ERROR", "error occurred when updating the user");
            //     }
            //     if (exception is EDirectoryUserUpdateException)
            //     {
            //         return Result.Failure("EDIRECTORY_USER_UPDATE_ERROR", "error occurred when updating the user");
            //     }
            //     
            //     return Result.Failure("EVENT_CUSTOMER_UPDATES", "Error occurred when publishing customer updated events");
            // }
        }

        private async Task<Result> HandleCustomerEmailUpdatesAsync(CustomerEmailUpdatedEvent updatedEvent, Customer customer, CancellationToken cancellationToken)
        {
            try
            {
                await mediator.Publish(updatedEvent, cancellationToken);
                return Result.Success();
            }
            catch (Auth0UpdateUserException exception)
            {
                await RevertEDirectoryChangesAsync(customer, cancellationToken);
                return Result.Failure("AUTH0_UPDATE_ERROR", "error occurred when updating Auth0");
            }
            catch (EDirectoryUserUpdateException exception)
            {
                await RevertAuth0ChangesAsync(customer, cancellationToken);
                return Result.Failure("EDIRECTORY_UPDATE_ERROR", "error occurred when updating eDirectory");
            }
            catch (Exception exception)
            {
                return Result.Failure("EMAIL_UPDATE_ERROR", "Error occurred when updating the email");
            }
            // catch (AggregateException exception)
            // {
            //     exception.Handle(ex =>
            //     {
            //         if (ex is Auth0UpdateUserException)
            //         {
            //             RevertEDirectoryChangesAsync(customer, cancellationToken).Wait(cancellationToken);
            //         }
            //
            //         if (ex is EDirectoryUserUpdateException)
            //         {
            //             RevertAuth0ChangesAsync(customer, cancellationToken).Wait(cancellationToken);
            //         }
            //
            //         return true;
            //     });
            // }

            return Result.Failure("EMAIL_UPDATE_ERROR", "Error occurred when updating the email");
        }

        private async Task RevertAuth0ChangesAsync(Customer customer, CancellationToken cancellationToken)
        {
            var revertEvent = new RevertAuth0UserUpdatesEvent
            {
                Id = customer.Id,
                Email = customer.Email
            };

            await mediator.Publish(revertEvent, cancellationToken);
        }

        private async Task RevertEDirectoryChangesAsync(Customer customer, CancellationToken cancellationToken)
        {
            var revertEvent = new RevertEDirectoryUpdatesEvent
            {
                Id = customer.Id,
                Email = customer.Email
            };

            await mediator.Publish(revertEvent, cancellationToken);
        }
    }
}