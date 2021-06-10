using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Services.Requests;
using MediatR;

namespace Interesting.Mediator.Handlers
{
    public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Result>
    {
        private readonly ICustomerRepository customerRepository;

        public CreateCustomerRequestHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Result> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
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