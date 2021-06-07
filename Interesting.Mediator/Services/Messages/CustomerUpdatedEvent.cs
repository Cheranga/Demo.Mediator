using MediatR;

namespace Interesting.Mediator.Services.Messages
{
    public class CustomerUpdatedEvent : INotification
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}