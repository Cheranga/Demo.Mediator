using FluentValidation;
using Interesting.Mediator.Requests;

namespace Interesting.Mediator.Validators
{
    public class CreateCustomerRequestValidator : ModelValidatorBase<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Address).NotNull().NotEmpty();
        }
    }
}