using System;
using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Exceptions;
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
        private readonly ILogger<UpdateCustomerRequestHandler> logger;

        public UpdateCustomerRequestHandler(ICustomerRepository customerRepository, IMediator mediator, ILogger<UpdateCustomerRequestHandler> logger)
        {
            this.customerRepository = customerRepository;
            this.mediator = mediator;
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
            return await GetCustomerAsync(request);
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
            
            try
            {
                await Task.WhenAll(mediator.Publish(customerEmailUpdatedEvent, cancellationToken), mediator.Publish(customerUpdatedEvent, cancellationToken));
                return Result.Success();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error occurred when publishing customer updated events");
            
                if (exception is Auth0UpdateUserException)
                {
                    return Result.Failure("AUTH0_USER_UPDATE_ERROR", "error occurred when updating the user");
                }
                if (exception is EDirectoryUserUpdateException)
                {
                    return Result.Failure("EDIRECTORY_USER_UPDATE_ERROR", "error occurred when updating the user");
                }
                
                return Result.Failure("EVENT_CUSTOMER_UPDATES", "Error occurred when publishing customer updated events");
            }
        }
    }
}