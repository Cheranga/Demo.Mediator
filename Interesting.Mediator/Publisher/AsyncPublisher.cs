using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Interesting.Mediator.Publisher
{
    public class AsyncPublisher : IAsyncPublisher
    {
        private readonly ServiceFactory serviceFactory;
        private readonly IDictionary<PublishStrategy, IMediator> publishStrategies = new Dictionary<PublishStrategy, IMediator>();

        public AsyncPublisher(ServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
            publishStrategies[PublishStrategy.ParallelNoWait] = new CustomMediator(this.serviceFactory, ParallelNoWait);
            publishStrategies[PublishStrategy.ParallelWhenAll] = new CustomMediator(this.serviceFactory, ParallelWhenAll);
        }

        public Task Publish<TNotification>(TNotification notification, PublishStrategy strategy, CancellationToken cancellationToken)
        {
            if (!publishStrategies.TryGetValue(strategy, out var mediator))
            {
                throw new ArgumentException($"Unknown strategy: {strategy}");
            }

            return mediator.Publish(notification, cancellationToken);
        }


        private Task ParallelNoWait(IEnumerable<Func<INotification, CancellationToken, Task>> handlers, INotification notification, CancellationToken cancellationToken)
        {
            foreach (var handler in handlers)
            {
                Task.Run(() => handler(notification, cancellationToken));
            }

            return Task.CompletedTask;
        }

        private Task ParallelWhenAll(IEnumerable<Func<INotification, CancellationToken, Task>> handlers, INotification notification, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            foreach (var handler in handlers)
            {
                tasks.Add(Task.Run(() => handler(notification, cancellationToken)));
            }

            return Task.WhenAll(tasks);
        }
    }
}