namespace Interesting.Mediator.Publisher
{
    public enum PublishStrategy
    {
        ParallelNoWait,
        ParallelWhenAll,
        SyncContinueOnException
    }
}