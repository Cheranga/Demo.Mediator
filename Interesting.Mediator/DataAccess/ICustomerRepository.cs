using System.Threading.Tasks;

namespace Interesting.Mediator.DataAccess
{
    public interface ICustomerRepository
    {
        Task<bool> CreateCustomerAsync(CreateCustomerCommand command);
        Task<Customer> GetCustomerByEmailAsync(string email);
    }
}