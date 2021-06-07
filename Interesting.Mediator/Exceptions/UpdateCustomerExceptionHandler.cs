using System;
using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Services.Requests;
using MediatR.Pipeline;

namespace Interesting.Mediator.Exceptions
{
    public class UpdateCustomerExceptionHandler : AsyncRequestExceptionHandler<UpdateCustomerRequest, Result<Customer>>
    {
        protected override Task Handle(UpdateCustomerRequest request, Exception exception, RequestExceptionHandlerState<Result<Customer>> state, CancellationToken cancellationToken)
        {
            if (exception is Auth0UpdateUserException)
            {
                state.SetHandled(Result<Customer>.Failure("AUTH0_USER_UPDATE_ERROR", "error occurred when updating the user"));
                return Task.CompletedTask;
            }
            if (exception is EDirectoryUserUpdateException)
            {
                state.SetHandled(Result<Customer>.Failure("EDIRECTORY_USER_UPDATE_ERROR", "error occurred when updating the user"));
                return Task.CompletedTask;
            }
                
            state.SetHandled(Result<Customer>.Failure("EVENT_CUSTOMER_UPDATES", "Error occurred when publishing customer updated events"));
            return Task.CompletedTask;
        }
    }
}