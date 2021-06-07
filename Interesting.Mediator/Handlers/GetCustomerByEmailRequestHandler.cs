using System.Threading;
using System.Threading.Tasks;
using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Services.Requests;
using MediatR;

namespace Interesting.Mediator.Handlers
{
    public class GetCustomerByEmailRequestHandler : IRequestHandler<GetCustomerByEmailRequest, Result<Customer>>
    {
        private readonly ICustomerRepository customerRepository;

        public GetCustomerByEmailRequestHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Result<Customer>> Handle(GetCustomerByEmailRequest request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetCustomerByEmailAsync(request.Email);
            return customer;
        }
    }
}