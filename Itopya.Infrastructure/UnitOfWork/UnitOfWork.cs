using Itopya.Domain.Repositories;
using Itopya.Domain.UnitOfWork;
using Itopya.Infrastructure.Context;
using Itopya.Infrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace Itopya.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db ??
                throw new ArgumentNullException("db can't be null");
        }
        private ICategoryRepository _categoryRepository;
        public ICategoryRepository Category { get { return _categoryRepository ??= new CategoryRepository(_db); } }

        private IProductRepository _productRepository;
        public IProductRepository Product { get { return _productRepository ??= new ProductRepository(_db); } }

        private ICategoryBundleRepository _categoryBundleRepository;
        public ICategoryBundleRepository CategoryBundle { get { return _categoryBundleRepository ??= new CategoryBundleRepository(_db); } }

        private bool isDisposed = false;

        public async Task<bool> Commit()
        {
            return await _db.SaveChangesAsync() >= 0;
        }

        public async ValueTask DisposeAsync()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this);
            }
        }

        protected async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await _db.DisposeAsync();
            }
        }
        
    }
}
