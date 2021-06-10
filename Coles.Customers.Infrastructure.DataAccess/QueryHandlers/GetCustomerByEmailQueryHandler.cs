using System;
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

        public async Task<Result<Customer>> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString("N"),
                Address = "Melbourne",
                Email = request.Email,
                Name = "Cheranga"
            };

            return Result<Customer>.Success(customer);
        }
    }
}