using Interesting.Mediator.Core;
using MediatR;

namespace Interesting.Mediator.Services.Requests
{
    public class CreateCustomerRequest : IRequest<Result>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}