using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Services.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Interesting.Mediator.Handlers
{
    public class RevertEDirectoryUpdatesEventHandler : INotificationHandler<RevertEDirectoryUpdatesEvent>
    {
        private readonly IEDirectoryRepository repository;
        private readonly ILogger<RevertEDirectoryUpdatesEventHandler> logger;

        public RevertEDirectoryUpdatesEventHandler(IEDirectoryRepository repository, ILogger<RevertEDirectoryUpdatesEventHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        
        public async Task Handle(RevertEDirectoryUpdatesEvent notification, CancellationToken cancellationToken)
        {
            var command = new UpdateEDirectoryUserCommand
            {
                Uid = notification.Id,
                Email = notification.Email
            };
            
            logger.LogInformation("Reverting EDirectory changes!");
            await repository.UpdateUserAsync(command);
        }
    }
}