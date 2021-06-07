using System;
using System.Threading.Tasks;
using Interesting.Mediator.Core;

namespace Interesting.Mediator.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<Result> CreateCustomerAsync(CreateCustomerCommand command)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return Result.Success();
        }

        public async Task<Result<Customer>> GetCustomerByEmailAsync(string email)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            
            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = "Cheranga Hatangala",
                Address = "Melbourne",
                Email = "cheranga@gmail.com"
            };

            return Result<Customer>.Success(customer);
        }
    }
}