using Coles.Customers.Infrastructure.DataAccess.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coles.Customers.Infrastructure.DataAccess
{
    public static class Bootstrapper
    {
        public static void RegisterDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection(nameof(DataAccessConfig)).Get<DataAccessConfig>();
            services.AddSingleton(config);
        }
    }
}