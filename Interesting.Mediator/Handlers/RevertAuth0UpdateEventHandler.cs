using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.Services;
using Interesting.Mediator.Services.Messages;
using Interesting.Mediator.Services.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Interesting.Mediator.Handlers
{
    public class RevertAuth0UpdateEventHandler : INotificationHandler<RevertAuth0UserUpdatesEvent>
    {
        private readonly IAuth0Service auth0Service;
        private readonly ILogger<RevertAuth0UpdateEventHandler> logger;

        public RevertAuth0UpdateEventHandler(IAuth0Service auth0Service, ILogger<RevertAuth0UpdateEventHandler> logger)
        {
            this.auth0Service = auth0Service;
            this.logger = logger;
        }
        
        public async Task Handle(RevertAuth0UserUpdatesEvent notification, CancellationToken cancellationToken)
        {
            var request = new Auth0UserUpdateRequest
            {
                Id = notification.Id,
                Email = notification.Email
            };

            logger.LogInformation("Reverting Auth0 changes!");
            await auth0Service.UpdateUserEmailAsync(request);
        }
    }
}