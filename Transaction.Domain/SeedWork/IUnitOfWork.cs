using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transaction.Domain.SeedWork
{
    public interface IUnitOfWork: IDisposable // Idisposable is relsasing unmanaged resources.
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));

    }
}
