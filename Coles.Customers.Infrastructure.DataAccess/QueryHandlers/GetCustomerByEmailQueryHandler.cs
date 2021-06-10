using System.Threading;
using System.Threading.Tasks;
using Coles.Customers.Application.Queries;
using Coles.Customers.Domain.Core;
using Coles.Customers.Domain.Entities;
using Coles.Customers.Infrastructure.DataAccess.Configs;
using MediatR;

namespace Coles.Customers.Infrastructure.DataAccess.Queries
{
    public class GetCustomerByEmailQueryHandler : QueryHandlerBase, IRequestHandler<GetCustomerByEmailQuery, Result<Customer>>
    {
        public GetCustomerByEmailQueryHandler(DataAccessConfig dataAccessConfig) : base(dataAccessConfig)
        {
        }

        public Task<Result<Customer>> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}