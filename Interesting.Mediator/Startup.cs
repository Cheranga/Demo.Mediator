using FluentValidation;
using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Services;
using Interesting.Mediator.Services.Requests;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Interesting.Mediator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            RegisterValidators(services);
            RegisterServices(services);
            RegisterMediators(services);
        }

        private void RegisterMediators(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly
            };

            services.AddMediatR(assemblies);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogPerformanceBehaviour<,>));
            services.AddTransient<IPipelineBehavior<GetCustomerByEmailRequest, Result<Customer>>, ValidationBehaviourWithResult<GetCustomerByEmailRequest, Customer>>();
            services.AddTransient<IPipelineBehavior<UpdateCustomerRequest, Result<Customer>>, ValidationBehaviourWithResult<UpdateCustomerRequest, Customer>>();
            
            services.AddTransient<IPipelineBehavior<CreateCustomerRequest, Result>, ValidationBehaviour<CreateCustomerRequest, Result>>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IAuth0Service, Auth0Service>();
            services.AddSingleton<IEDirectoryRepository, EDirectoryRepository>();
        }

        private void RegisterValidators(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly
            };

            services.AddValidatorsFromAssemblies(assemblies);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}