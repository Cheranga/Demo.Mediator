using Coles.Customers.Infrastructure.DataAccess.Configs;

namespace Coles.Customers.Infrastructure.DataAccess.Commands
{
    public class CommandHandlerBase
    {
        private readonly DataAccessConfig dataAccessConfig;

        public CommandHandlerBase(DataAccessConfig dataAccessConfig)
        {
            this.dataAccessConfig = dataAccessConfig;
        }
    }
}