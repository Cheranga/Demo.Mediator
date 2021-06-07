using System.Threading.Tasks;
using Interesting.Mediator.Core;

namespace Interesting.Mediator.DataAccess
{
    public interface ICustomerRepository
    {
        Task<Result> CreateCustomerAsync(CreateCustomerCommand command);
        Task<Result> UpdateCustomerAsync(UpdateCustomerCommand command);
        Task<Result<Customer>> GetCustomerByEmailAsync(string email);
    }
}