using FluentValidation;
using Interesting.Mediator.Services.Requests;

namespace Interesting.Mediator.Validators
{
    public class GetCustomerByEmailRequestValidator : ModelValidatorBase<GetCustomerByEmailRequest>
    {
        public GetCustomerByEmailRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}