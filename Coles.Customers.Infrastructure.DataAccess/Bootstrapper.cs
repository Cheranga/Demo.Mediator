using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coles.Customers.Infrastructure.DataAccess
{
    public static class Bootstrapper
    {
        public static void RegisterDataAccess(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            // TODO: Register dependencies
        }
    }
}