using System.Threading.Tasks;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Requests;

namespace Interesting.Mediator.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<bool> CreateCustomerAsync(CreateCustomerRequest request)
        {
            var command = new CreateCustomerCommand
            {
                Name = request.Name,
                Address = request.Address
            };

            var result = await customerRepository.CreateCustomerAsync(command);
            return result;
        }

        public Task<Customer> GetCustomerAsync(GetCustomerByEmailRequest request)
        {
            return customerRepository.GetCustomerByEmailAsync(request.Email);
        }
    }
}