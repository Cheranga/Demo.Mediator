using Coles.Customers.Domain.Core;
using Coles.Customers.Domain.Entities;
using MediatR;

namespace Coles.Customers.Application.Requests
{
    public class GetCustomerByEmailRequest : IRequest<Result<Customer>>
    {
        public string Email { get; set; }
    }
}