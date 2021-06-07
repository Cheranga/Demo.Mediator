using System;
using Interesting.Mediator.Services.Requests;

namespace Interesting.Mediator.Exceptions
{
    public class Auth0UpdateUserException : Exception
    {
        public Auth0UserUpdateRequest Request { get; private set; }
        
        public Auth0UpdateUserException(Auth0UserUpdateRequest request) : base("Error occurred when updating the Auth0 user")
        {
            Request = request;
        }
    }
}