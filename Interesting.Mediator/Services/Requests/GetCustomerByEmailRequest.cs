using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using MediatR;

namespace Interesting.Mediator.Services.Requests
{
    public class GetCustomerByEmailRequest : IRequest<Result<Customer>>
    {
        public string Email { get; set; }
    }
}