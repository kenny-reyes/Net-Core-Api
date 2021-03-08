using System;

namespace NetCoreApiScaffolding.Tools.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}