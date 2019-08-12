using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.Exceptions
{
    /// Exception type for domain exceptions by lalji 
    /// for Domain exception
    class TransactionDomainException : Exception
    {
        public TransactionDomainException()
        { }
        public TransactionDomainException(string message) : base(message)
        { }

        public TransactionDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }
}
