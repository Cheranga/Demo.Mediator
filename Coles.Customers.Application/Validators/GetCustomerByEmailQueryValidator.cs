using Coles.Customers.Application.Queries;
using FluentValidation;

namespace Coles.Customers.Application.Validators
{
    public class GetCustomerByEmailQueryValidator : ModelValidatorBase<GetCustomerByEmailQuery>
    {
        public GetCustomerByEmailQueryValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().MinimumLength(5);
        }
    }
}