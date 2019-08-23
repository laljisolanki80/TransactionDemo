using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace Transaction.Domain.SeedWork
{
    public interface IRepository<T> where T :Entity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
