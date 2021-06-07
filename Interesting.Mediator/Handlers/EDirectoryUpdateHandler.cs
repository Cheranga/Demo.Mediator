using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Services.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Interesting.Mediator.Handlers
{
    public class EDirectoryUpdateHandler : INotificationHandler<CustomerUpdatedEvent>
    {
        private readonly IEDirectoryRepository repository;
        private readonly ILogger<EDirectoryUpdateHandler> logger;

        public EDirectoryUpdateHandler(IEDirectoryRepository repository, ILogger<EDirectoryUpdateHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
        {
            //
            // Simulating some eDirectory related work
            //
            var nameParts = notification.Name?.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameParts?.FirstOrDefault();
            var lastName = nameParts?.Skip(1)?.FirstOrDefault();

            var updateCommand = new UpdateEDirectoryUserCommand
            {
                Uid = notification.Id,
                Email = notification.Email,
                FirstName = firstName,
                LastName = lastName,
                Address = notification.Address
            };

            logger.LogInformation("EDirectory updates are done!");
            await repository.UpdateUserAsync(updateCommand);
        }
    }
}