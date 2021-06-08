using System;
using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Services;
using Interesting.Mediator.Services.Messages;
using Interesting.Mediator.Services.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Interesting.Mediator.Handlers
{
    public class CompensatingEmailUpdateHandler : INotificationHandler<CustomerEmailUpdatedEvent>
    {
        private readonly IAuth0Service auth0Service;
        private readonly IEDirectoryRepository eDirectoryRepository;
        private readonly ILogger<CompensatingEmailUpdateHandler> logger;

        public CompensatingEmailUpdateHandler(IAuth0Service auth0Service, IEDirectoryRepository eDirectoryRepository, ILogger<CompensatingEmailUpdateHandler> logger)
        {
            this.auth0Service = auth0Service;
            this.eDirectoryRepository = eDirectoryRepository;
            this.logger = logger;
        }

        public async Task Handle(CustomerEmailUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating user email handler started");

            var auth0UserRequest = new Auth0UserUpdateRequest
            {
                Id = notification.CustomerId,
                Email = notification.NewEmail
            };


            await auth0Service.UpdateUserEmailAsync(auth0UserRequest);

            logger.LogInformation("Auth0 user updated");

            var updateCommand = new UpdateEDirectoryUserCommand
            {
                Uid = notification.CustomerId,
                Email = notification.NewEmail
            };

            var operation = await eDirectoryRepository.UpdateUserAsync(updateCommand);
            if (!operation.Status)
            {
                logger.LogWarning("eDirectory user updating failed, rolling back Auth0 changes");
                await auth0Service.UpdateUserEmailAsync(new Auth0UserUpdateRequest
                {
                    Id = notification.CustomerId,
                    Email = notification.OldEmail
                });

                throw new Exception("User email update error");
            }

            logger.LogInformation("eDirectory user updated. Updating user email is completed");
        }
    }
}