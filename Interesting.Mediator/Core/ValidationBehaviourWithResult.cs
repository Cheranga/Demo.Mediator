using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Interesting.Mediator.Core
{
    public class ValidationBehaviourWithResult<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
    {
        private readonly ILogger<ValidationBehaviourWithResult<TRequest, TResponse>> logger;
        private readonly IValidator<TRequest> validator;

        public ValidationBehaviourWithResult(ILogger<ValidationBehaviourWithResult<TRequest, TResponse>> logger, IValidator<TRequest> validator = null)
        {
            this.logger = logger;
            this.validator = validator;
        }
        
        public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResponse>> next)
        {
            if (validator == null)
            {
                return await next();
            }

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Invalid request. {@InvalidRequest}", request);
                return Result<TResponse>.Failure("INVALID_REQUEST", validationResult);
            }

            return await next();
        }
    }
}