using System;

namespace HooliCash.Shared
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }

    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message) : base(message)
        {
        }
    }

    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message) : base(message)
        {
        }
    }

    public class HooliCashException : Exception
    {
        public HooliCashException(string message) : base(message)
        {
        }
    }
}
