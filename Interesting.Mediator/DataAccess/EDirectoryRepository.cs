using System;
using System.Threading.Tasks;
using Interesting.Mediator.Core;

namespace Interesting.Mediator.DataAccess
{
    public class EDirectoryRepository : IEDirectoryRepository
    {
        public async Task<Result> UpdateUserAsync(UpdateEDirectoryUserCommand command)
        {
            // Simulate eDirectory stuff here
            await Task.Delay(TimeSpan.FromSeconds(2));
            return Result.Failure("EDIRECTORY_UPDATE_ERROR", "error occurred when updating eDirectory");
        }
    }
}