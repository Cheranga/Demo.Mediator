using FluentValidation;
using Interesting.Mediator.DataAccess;

namespace Interesting.Mediator.Validators
{
    public class UpdateCustomerCommandHandler : ModelValidatorBase<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandHandler()
        {
            RuleFor(x => x.Address).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}