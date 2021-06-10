using Interesting.Mediator.Core;
using MediatR;

namespace Interesting.Mediator.DataAccess
{
    public class UpdateCustomerCommand : IRequest<Result>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}