using Coles.Customers.Application;
using Coles.Customers.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Coles.Customers.Api
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

            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            // TODO: Register application services

            RegisterMediators(services);
            RegisterValidators(services);
            RegisterMappers(services);
            Infrastructure.DataAccess.Bootstrapper.RegisterDataAccess(services, Configuration);
            services.RegisterApplicationServices();


            RegisterApiServices(services);

        }

        private void RegisterMappers(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly,
                typeof(Bootstrapper).Assembly
            };

            services.AddAutoMapper(assemblies);
        }

        private void RegisterApiServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
        }

        private void RegisterMediators(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly,
                typeof(Bootstrapper).Assembly,
                typeof(Infrastructure.DataAccess.Bootstrapper).Assembly
            };

            services.AddMediatR(assemblies, configuration =>
            {
                configuration.AsScoped();
            });
        }

        private void RegisterValidators(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly,
                typeof(Bootstrapper).Assembly
            };

            services.AddValidatorsFromAssemblies(assemblies);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}