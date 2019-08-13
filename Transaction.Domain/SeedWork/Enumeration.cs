using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transaction.Domain.SeedWork
{
    public abstract class Enumeration:IComparable
    {
        public string Name { get; set; }
        public int Id { get; set; }

        protected Enumeration()
        { }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public override string ToString() => Name;


        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

    }
}
