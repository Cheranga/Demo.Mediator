using System;
using System.Threading.Tasks;
using Interesting.Mediator.Core;
using Microsoft.Extensions.Logging;

namespace Interesting.Mediator.DataAccess
{
    public class EDirectoryRepository : IEDirectoryRepository
    {
        public async Task<Result> UpdateUserAsync(UpdateEDirectoryUserCommand command)
        {
            // Simulate eDirectory stuff here
            await Task.Delay(TimeSpan.FromSeconds(2));
            return Result.Success();
        }
    }
}