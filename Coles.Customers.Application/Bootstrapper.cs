using Coles.Customers.Application.Behaviours;
using Coles.Customers.Application.Queries;
using Coles.Customers.Application.Requests;
using Coles.Customers.Domain.Core;
using Coles.Customers.Domain.Entities;
using Coles.Customers.Infrastructure.DataAccess.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Coles.Customers.Application
{
    public static class Bootstrapper
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            // TODO: Register application services
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LogPerformanceBehaviour<,>));
            services.AddScoped<IPipelineBehavior<GetCustomerByEmailRequest, Result<Customer>>, ValidationBehaviourWithResult<GetCustomerByEmailRequest, Customer>>();
            services.AddScoped<IPipelineBehavior<GetCustomerByEmailQuery, Result<Customer>>, ValidationBehaviourWithResult<GetCustomerByEmailQuery, Customer>>();
            
            services.AddScoped<IPipelineBehavior<CreateCustomerRequest, Result>, ValidationBehaviour<CreateCustomerRequest, Result>>();
            services.AddScoped<IPipelineBehavior<CreateCustomerCommand, Result>, ValidationBehaviour<CreateCustomerCommand, Result>>();
        }
    }
}