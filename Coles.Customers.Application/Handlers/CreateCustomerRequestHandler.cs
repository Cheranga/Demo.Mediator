using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Coles.Customers.Application.Requests;
using Coles.Customers.Domain.Core;
using Coles.Customers.Infrastructure.DataAccess.Commands;
using MediatR;

namespace Interesting.Mediator.Handlers
{
    public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Result>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateCustomerRequestHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        
        public async Task<Result> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var command = mapper.Map<CreateCustomerCommand>(request);
            var operation = await mediator.Send(command, cancellationToken);

            return operation;
        }
    }
}