﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ParkMyLots.Data.Exceptions
{
    public class InvalidKeyException : Exception
    {
        public InvalidKeyException()
        {
        }

        public InvalidKeyException(string message) : base(message)
        {
        }

        public InvalidKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
