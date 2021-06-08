using MediatR;

namespace Interesting.Mediator.Services.Messages
{
    public class RevertAuth0UserUpdatesEvent : INotification
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}