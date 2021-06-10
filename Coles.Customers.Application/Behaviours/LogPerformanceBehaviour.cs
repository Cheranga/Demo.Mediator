using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coles.Customers.Application.Behaviours
{
    public class LogPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LogPerformanceBehaviour<TRequest, TResponse>> logger;

        public LogPerformanceBehaviour(ILogger<LogPerformanceBehaviour<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            
            logger.LogInformation("{Request} started", typeof(TRequest).Name);
            var response = await next();
            stopWatch.Stop();
            logger.LogInformation("{Request} finished. Time taken {TimeElapsed}ms", typeof(TRequest).Name, stopWatch.ElapsedMilliseconds);

            return response;
        }
    }
}