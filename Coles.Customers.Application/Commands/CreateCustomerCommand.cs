using Coles.Customers.Domain.Core;
using MediatR;

namespace Coles.Customers.Infrastructure.DataAccess.Commands
{
    public class CreateCustomerCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}