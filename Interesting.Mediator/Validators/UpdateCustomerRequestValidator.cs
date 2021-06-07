using FluentValidation;
using Interesting.Mediator.Services.Requests;

namespace Interesting.Mediator.Validators
{
    public class UpdateCustomerRequestValidator : ModelValidatorBase<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Address).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        }
    }
}