using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ApiExercise.Tools.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {
        public IList<string> ErrorMessages { get; private set; }

        public DomainException()
        { }

        public DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        
        public DomainException(string errorMessage) : base(errorMessage)
        {
            ErrorMessages = new List<string> { errorMessage };
        }

        public DomainException(IList<string> errorMessages)
            : base(string.Join("\n", errorMessages))
        {
            ErrorMessages = new List<string>(errorMessages);
        }
    }
}
