using System.Threading.Tasks;
using Coles.Customers.Application.Requests;
using Coles.Customers.Domain.Core;
using Coles.Customers.Domain.Entities;

namespace Coles.Customers.Application.Services
{
    public interface ICustomerService
    {
        Task<Result> CreateCustomerAsync(CreateCustomerRequest request);
        Task<Result<Customer>> GetCustomerAsync(GetCustomerByEmailRequest request);
    }
}