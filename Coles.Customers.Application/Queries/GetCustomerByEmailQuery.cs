using Coles.Customers.Domain.Core;
using Coles.Customers.Domain.Entities;
using MediatR;

namespace Coles.Customers.Application.Queries
{
    public class GetCustomerByEmailQuery : IRequest<Result<Customer>>
    {
        public string Email { get; set; }
    }
}