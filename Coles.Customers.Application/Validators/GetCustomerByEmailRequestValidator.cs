using Coles.Customers.Application.Requests;
using FluentValidation;

namespace Coles.Customers.Application.Validators
{
    public class GetCustomerByEmailRequestValidator : ModelValidatorBase<GetCustomerByEmailRequest>
    {
        public GetCustomerByEmailRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
        }
    }
}