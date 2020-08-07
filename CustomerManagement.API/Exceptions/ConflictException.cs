using System;

namespace CustomerManagement.API.Exceptions
{
    public class ConflictException:Exception
    {
        public ConflictException(string message)
            : base(message)
        {
        }

        public ConflictException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}