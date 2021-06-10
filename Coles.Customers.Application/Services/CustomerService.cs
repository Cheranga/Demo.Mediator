
using System.Threading.Tasks;
using AutoMapper;
using Coles.Customers.Application.Queries;
using Coles.Customers.Application.Requests;
using Coles.Customers.Domain.Core;
using Coles.Customers.Domain.Entities;
using Coles.Customers.Infrastructure.DataAccess.Commands;
using MediatR;

namespace Coles.Customers.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CustomerService(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        
        public async Task<Result> CreateCustomerAsync(CreateCustomerRequest request)
        {
            var command = mapper.Map<CreateCustomerCommand>(request);
            var operation = await mediator.Send(command);
            return operation;
        }

        public async Task<Result<Customer>> GetCustomerAsync(GetCustomerByEmailRequest request)
        {
            var query = new GetCustomerByEmailQuery
            {
                Email = request.Email
            };

            var operation = await mediator.Send(query);
            var customer = operation.Data;
            return customer == null ? Result<Customer>.Failure("CUSTOMER_NOT_FOUND", "customer not found") : Result<Customer>.Success(customer);
        }
    }
}