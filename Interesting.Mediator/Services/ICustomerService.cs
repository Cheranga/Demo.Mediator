using System.Threading.Tasks;
using Interesting.Mediator.Requests;

namespace Interesting.Mediator.Services
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CreateCustomerRequest request);
    }
}