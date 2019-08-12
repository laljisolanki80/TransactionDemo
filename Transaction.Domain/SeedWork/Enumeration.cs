using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transaction.Domain.SeedWork
{
    public class Enumeration:IComparable
    {
        public string Name { get; private set; }
        //[Key]
        public int Id { get; private set; }

        protected Enumeration()
        { }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

    }
}
