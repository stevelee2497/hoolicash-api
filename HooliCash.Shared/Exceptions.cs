using System;

namespace HooliCash.Shared
{
    public class HooliCashException : Exception
    {
        public HooliCashException(string message) : base(message)
        {
        }
    }
}
