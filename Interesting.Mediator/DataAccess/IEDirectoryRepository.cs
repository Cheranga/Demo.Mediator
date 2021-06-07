using System.Threading.Tasks;
using Interesting.Mediator.Core;

namespace Interesting.Mediator.DataAccess
{
    public interface IEDirectoryRepository
    {
        Task<Result> UpdateUserAsync(UpdateEDirectoryUserCommand command);
    }
}