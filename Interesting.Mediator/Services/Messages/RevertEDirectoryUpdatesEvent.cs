using MediatR;

namespace Interesting.Mediator.Services.Messages
{
    public class RevertEDirectoryUpdatesEvent : INotification
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}