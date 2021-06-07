using System;
using System.Threading.Tasks;

namespace Interesting.Mediator.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<bool> CreateCustomerAsync(CreateCustomerCommand command)
        {
            // TODO: create the customer
            return Task.FromResult(true);
        }

        public Task<Customer> GetCustomerByEmailAsync(string email)
        {
            // TODO: get the customer
            return Task.FromResult(new Customer
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = "Cheranga Hatangala",
                Address = "Melbourne",
                Email = "cheranga@gmail.com"
            });
        }
    }
}