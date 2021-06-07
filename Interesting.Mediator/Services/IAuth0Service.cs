using System.Threading.Tasks;
using Interesting.Mediator.Services.Requests;

namespace Interesting.Mediator.Services
{
    public interface IAuth0Service
    {
        Task UpdateUserEmailAsync(Auth0UserUpdateRequest request);
    }
}