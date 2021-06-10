using System;
using System.Threading;
using System.Threading.Tasks;
using Coles.Customers.Domain.Core;
using Coles.Customers.Infrastructure.DataAccess.Configs;
using MediatR;

namespace Coles.Customers.Infrastructure.DataAccess.Commands
{
    public class UpdateCustomerCommandHandler : CommandHandlerBase, IRequestHandler<UpdateCustomerCommand, Result>
    {
        public UpdateCustomerCommandHandler(DataAccessConfig dataAccessConfig) : base(dataAccessConfig)
        {
        }

        public Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}