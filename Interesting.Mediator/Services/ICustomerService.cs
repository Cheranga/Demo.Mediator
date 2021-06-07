using System.Threading.Tasks;
using Interesting.Mediator.Core;
using Interesting.Mediator.DataAccess;
using Interesting.Mediator.Requests;

namespace Interesting.Mediator.Services
{
    public interface ICustomerService
    {
        Task<Result> CreateCustomerAsync(CreateCustomerRequest request);
        Task<Result<Customer>> GetCustomerAsync(GetCustomerByEmailRequest request);
    }
}