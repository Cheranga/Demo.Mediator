using FluentValidation;
using FluentValidation.Results;

namespace Coles.Customers.Application.Validators
{
    public class ModelValidatorBase<TModel> : AbstractValidator<TModel>
    {
        protected ModelValidatorBase()
        {
            CascadeMode = CascadeMode.Stop;
        }
        
        protected override bool PreValidate(ValidationContext<TModel> context, ValidationResult result)
        {
            var instance = context.InstanceToValidate;

            if (instance != null) return true;

            result.Errors.Add(new ValidationFailure("", "Instance cannot be null"));
            return false;
        }
    }
}