using System;
using System.Threading.Tasks;
using Interesting.Mediator.Requests;

namespace Interesting.Mediator.Services
{
    public class CustomerService : ICustomerService
    {
        public Task<bool> CreateCustomerAsync(CreateCustomerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}