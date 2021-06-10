using System;
using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Publisher;
using Interesting.Mediator.Services.Messages;
using Interesting.Mediator.Services.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Interesting.Mediator.Handlers
{
    public class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, Result<Customer>>
    {
        private readonly IMediator mediator;
        private readonly IAsyncPublisher asyncPublisher;
        private readonly ILogger<UpdateCustomerRequestHandler> logger;

        public UpdateCustomerRequestHandler(IMediator mediator, IAsyncPublisher asyncPublisher, ILogger<UpdateCustomerRequestHandler> logger)
        {
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
            var getCustomerByEmailRequest = new GetCustomerByEmailRequest
            {
                Email = request.Email
            };

            var getCustomerOperation = await mediator.Send(getCustomerByEmailRequest);
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

            return mediator.Send(updateCustomerCommand);
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
        }

        private async Task<Result> HandleCustomerEmailUpdatesAsync(CustomerEmailUpdatedEvent updatedEvent, Customer customer, CancellationToken cancellationToken)
        {
            try
            {
                await mediator.Publish(updatedEvent, cancellationToken);
                return Result.Success();
            }
            catch (Exception exception)
            {
                return Result.Failure("EMAIL_UPDATE_ERROR", "Error occurred when updating the email");
            }
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