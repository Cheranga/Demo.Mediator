using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Coles.Customers.Application.Queries;
using Coles.Customers.Application.Requests;
using Coles.Customers.Domain.Core;
using Coles.Customers.Domain.Entities;
using MediatR;

namespace Interesting.Mediator.Handlers
{
    public class GetCustomerByEmailRequestHandler : IRequestHandler<GetCustomerByEmailRequest, Result<Customer>>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public GetCustomerByEmailRequestHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Result<Customer>> Handle(GetCustomerByEmailRequest request, CancellationToken cancellationToken)
        {
            var query = mapper.Map<GetCustomerByEmailQuery>(request);
            var getCustomerOperation = await mediator.Send(query, cancellationToken);
            if (!getCustomerOperation.Status)
            {
                return Result<Customer>.Failure(getCustomerOperation.ErrorCode, getCustomerOperation.ValidationResult);
            }

            var customer = getCustomerOperation.Data;
            return customer == null ? Result<Customer>.Failure("CUSTOMER_NOT_FOUND", "customer not found") : Result<Customer>.Success(customer);
        }
    }
}