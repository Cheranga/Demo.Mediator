using Coles.Customers.Domain.Core;
using MediatR;

namespace Coles.Customers.Application.Requests
{
    public class CreateCustomerRequest : IRequest<Result>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}