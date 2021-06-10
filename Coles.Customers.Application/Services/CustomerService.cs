using System.Threading.Tasks;
using Coles.Customers.Application.Requests;
using Coles.Customers.Domain.Core;
using Coles.Customers.Domain.Entities;
using MediatR;

namespace Coles.Customers.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator mediator;

        public CustomerService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result> CreateCustomerAsync(CreateCustomerRequest request)
        {
            var operation = await mediator.Send(request);
            return operation;
        }

        public async Task<Result<Customer>> GetCustomerAsync(GetCustomerByEmailRequest request)
        {
            var operation = await mediator.Send(request);
            return operation;
        }
    }
}