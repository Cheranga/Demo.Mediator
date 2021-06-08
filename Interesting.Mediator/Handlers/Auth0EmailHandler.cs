using System;
using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.Exceptions;
using Interesting.Mediator.Services;
using Interesting.Mediator.Services.Messages;
using Interesting.Mediator.Services.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Interesting.Mediator.Handlers
{
    public class Auth0EmailHandler : INotificationHandler<CustomerEmailUpdatedEvent>
    {
        private readonly IAuth0Service auth0Service;
        private readonly ILogger<Auth0EmailHandler> logger;

        public Auth0EmailHandler(IAuth0Service auth0Service, ILogger<Auth0EmailHandler> logger)
        {
            this.auth0Service = auth0Service;
            this.logger = logger;
        }
        
        public async Task Handle(CustomerEmailUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Auth0 email handling started!");
            await Task.Delay(TimeSpan.FromSeconds(2));

            var auth0UserUpdateRequest = new Auth0UserUpdateRequest
            {
                Id = $"auth0|{notification.CustomerId}",
                Email = notification.NewEmail
            };

            logger.LogError("Auth0 email update handling error!");
            throw new Auth0UpdateUserException(auth0UserUpdateRequest);

            await auth0Service.UpdateUserEmailAsync(auth0UserUpdateRequest);
            
            logger.LogInformation("Auth0 updates are done!");
        }
    }
}