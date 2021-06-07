using System;
using System.Threading.Tasks;
using Interesting.Mediator.Services.Requests;

namespace Interesting.Mediator.Services
{
    public class Auth0Service : IAuth0Service
    {
        public async Task UpdateUserEmailAsync(Auth0UserUpdateRequest request)
        {
            // Simulating Auth0 doing stuff.
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
    }
}