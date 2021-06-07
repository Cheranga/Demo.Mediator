using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Services.Requests;
using MediatR;

namespace Interesting.Mediator.Handlers
{
    public class GetCustomerByEmailRequestHandler : IRequestHandler<GetCustomerByEmailRequest, Result<Customer>>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IValidator<GetCustomerByEmailRequest> validator;

        public GetCustomerByEmailRequestHandler(IValidator<GetCustomerByEmailRequest> validator, ICustomerRepository customerRepository)
        {
            this.validator = validator;
            this.customerRepository = customerRepository;
        }

        public async Task<Result<Customer>> Handle(GetCustomerByEmailRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<Customer>.Failure("INVALID_REQUEST", validationResult);
            }

            var customer = await customerRepository.GetCustomerByEmailAsync(request.Email);
            return customer;
        }
    }
}