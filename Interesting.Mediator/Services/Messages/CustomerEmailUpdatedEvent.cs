using System;
using MediatR;

namespace Interesting.Mediator.Services.Messages
{
    public class CustomerEmailUpdatedEvent : INotification
    {
        public string CustomerId { get; set; }
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }
        public DateTime UpdatedDateTimeUtc { get; set; }
    }
}