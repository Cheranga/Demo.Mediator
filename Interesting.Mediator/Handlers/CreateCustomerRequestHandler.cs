using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Services.Requests;
using MediatR;

namespace Interesting.Mediator.Handlers
{
    public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Result>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IValidator<CreateCustomerRequest> validator;

        public CreateCustomerRequestHandler(IValidator<CreateCustomerRequest> validator, ICustomerRepository customerRepository)
        {
            this.validator = validator;
            this.customerRepository = customerRepository;
        }

        public async Task<Result> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Failure("INVALID_REQUEST", validationResult);
            }

            var command = new CreateCustomerCommand
            {
                Name = request.Name,
                Address = request.Address,
                Email = request.Email
            };

            var operation = await customerRepository.CreateCustomerAsync(command);
            return operation;
        }
    }
}