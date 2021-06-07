using System.Threading;
using System.Threading.Tasks;

namespace Interesting.Mediator.Publisher
{
    public interface IAsyncPublisher
    {
        Task Publish<TNotification>(TNotification notification, PublishStrategy strategy, CancellationToken cancellationToken);
    }
}