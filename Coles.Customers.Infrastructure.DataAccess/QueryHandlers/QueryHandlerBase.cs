using Coles.Customers.Infrastructure.DataAccess.Configs;

namespace Coles.Customers.Infrastructure.DataAccess.Queries
{
    public class QueryHandlerBase
    {
        private readonly DataAccessConfig dataAccessConfig;

        public QueryHandlerBase(DataAccessConfig dataAccessConfig)
        {
            this.dataAccessConfig = dataAccessConfig;
        }
    }
}