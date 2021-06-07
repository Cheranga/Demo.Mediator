using System;
using Interesting.Mediator.DataAccess;

namespace Interesting.Mediator.Exceptions
{
    public class EDirectoryUserUpdateException : Exception
    {
        public UpdateEDirectoryUserCommand Command { get; }

        public EDirectoryUserUpdateException(UpdateEDirectoryUserCommand command) : base("Error occurred when updating the eDirectory user")
        {
            Command = command;
        }
    }
}