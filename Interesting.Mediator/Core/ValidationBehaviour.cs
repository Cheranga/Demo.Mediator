using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Interesting.Mediator.Core
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, Result>
    {
        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> logger;
        private readonly IValidator<TRequest> validator;

        public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger, IValidator<TRequest> validator = null)
        {
            this.logger = logger;
            this.validator = validator;
        }
        
        public async Task<Result> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result> next)
        {
            if (validator == null)
            {
                return await next();
            }

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Invalid request. {@InvalidRequest}", request);
                return Result.Failure("INVALID_REQUEST", validationResult);
            }

            return await next();
        }
    }
}