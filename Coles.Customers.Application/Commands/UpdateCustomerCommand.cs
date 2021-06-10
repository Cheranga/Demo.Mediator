using Coles.Customers.Domain.Core;
using Coles.Customers.Domain.Entities;
using MediatR;

namespace Coles.Customers.Infrastructure.DataAccess.Commands
{
    public class UpdateCustomerCommand : IRequest<Result>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}