using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transaction.Domain.Validation
{
    public class GreaterThanZero : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var x = (decimal)value;
            return x > 0;
        }
    }
}
