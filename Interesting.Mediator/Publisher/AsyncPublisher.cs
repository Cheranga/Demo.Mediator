using System;
using System.Collections.Generic;
using System.Linq;
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
            publishStrategies[PublishStrategy.SyncContinueOnException] = new CustomMediator(this.serviceFactory, SyncContinueOnException);
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
        
        private async Task SyncContinueOnException(IEnumerable<Func<INotification, CancellationToken, Task>> handlers, INotification notification, CancellationToken cancellationToken)
        {
            var exceptions = new List<Exception>();

            foreach (var handler in handlers)
            {
                try
                {
                    await handler(notification, cancellationToken).ConfigureAwait(false);
                }
                catch (AggregateException ex)
                {
                    exceptions.AddRange(ex.Flatten().InnerExceptions);
                }
                catch (Exception ex) when (!(ex is OutOfMemoryException || ex is StackOverflowException))
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}