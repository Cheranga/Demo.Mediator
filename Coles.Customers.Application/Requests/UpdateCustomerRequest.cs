using Coles.Customers.Domain.Core;
using Coles.Customers.Domain.Entities;
using MediatR;

namespace Coles.Customers.Application.Requests
{
    public class UpdateCustomerRequest : IRequest<Result<Customer>>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}