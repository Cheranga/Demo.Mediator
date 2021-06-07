using FluentValidation.Results;

namespace Interesting.Mediator.Core
{
    public class Result
    {
        public string ErrorCode { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public bool Status => string.IsNullOrEmpty(ErrorCode);

        public static Result Failure(string errorCode, ValidationResult validationResult)
        {
            return new Result
            {
                ErrorCode = errorCode,
                ValidationResult = validationResult
            };
        }

        public static Result Failure(string errorCode, string errorMessage)
        {
            return Failure(errorCode, new ValidationResult(new[]
            {
                new ValidationFailure("", errorMessage)
                {
                    ErrorCode = errorCode
                }
            }));
        }

        public static Result Success()
        {
            return new Result();
        }
    }

    public class Result<TData>
    {
        public TData Data { get; set; }
        public string ErrorCode { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public bool Status => string.IsNullOrEmpty(ErrorCode);

        public static Result<TData> Failure(string errorCode, ValidationResult validationResult)
        {
            return new Result<TData>
            {
                ErrorCode = errorCode,
                ValidationResult = validationResult
            };
        }

        public static Result<TData> Failure(string errorCode, string errorMessage)
        {
            return Failure(errorCode, new ValidationResult(new[]
            {
                new ValidationFailure("", errorMessage)
                {
                    ErrorCode = errorCode
                }
            }));
        }

        public static Result<TData> Success(TData data)
        {
            return new Result<TData>
            {
                Data = data
            };
        }
    }
}