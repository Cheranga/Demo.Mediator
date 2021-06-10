using System;
using System.Threading;
using System.Threading.Tasks;
using Coles.Customers.Domain.Core;
using Coles.Customers.Infrastructure.DataAccess.Configs;
using MediatR;

namespace Coles.Customers.Infrastructure.DataAccess.Commands
{
    public class CreateCustomerCommandHandler : CommandHandlerBase, IRequestHandler<CreateCustomerCommand, Result>
    {
        public CreateCustomerCommandHandler(DataAccessConfig dataAccessConfig) : base(dataAccessConfig)
        {
        }

        public Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}