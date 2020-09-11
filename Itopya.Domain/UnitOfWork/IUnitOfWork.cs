using Itopya.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Itopya.Domain.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<bool> Commit();
        ICategoryRepository Category { get; }
        ICategoryBundleRepository CategoryBundle { get; }
        IProductRepository Product { get; }
    }
}