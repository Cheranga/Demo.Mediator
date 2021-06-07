using System;
using System.Threading.Tasks;
using Interesting.Mediator.Core;

namespace Interesting.Mediator.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<Result> CreateCustomerAsync(CreateCustomerCommand command)
        {
            return Task.FromResult(Result.Success());
        }

        public Task<Result<Customer>> GetCustomerByEmailAsync(string email)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = "Cheranga Hatangala",
                Address = "Melbourne",
                Email = "cheranga@gmail.com"
            };
            
            return Task.FromResult(Result<Customer>.Success(customer));
        }
    }
}