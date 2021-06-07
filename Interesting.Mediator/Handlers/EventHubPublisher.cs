using System;
using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.Services.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Interesting.Mediator.Handlers
{
    public class EventHubPublisher : INotificationHandler<CustomerUpdatedEvent>
    {
        private readonly ILogger<EventHubPublisher> logger;

        public EventHubPublisher(ILogger<EventHubPublisher> logger)
        {
            this.logger = logger;
        }
        public async Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            logger.LogInformation("Updated customer data has been published to event hub!");
        }
    }
}