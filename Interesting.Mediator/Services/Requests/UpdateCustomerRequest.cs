using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using MediatR;

namespace Interesting.Mediator.Services.Requests
{
    public class UpdateCustomerRequest : IRequest<Result<Customer>>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}