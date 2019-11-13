using Bb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Bb.Data.Repository.Ef
{
    public class ProductRepositoryEf : IProductRepository
    {
        private BbContext _dbContext;
        public ProductRepositoryEf()
        {
            _dbContext = new BbContext();
        }

        public void BulkCreate(IList<Product> products)
        {
            _dbContext.BulkInsert(products);
        }

        public async Task BulkCreateAsync(IList<Product> products)
        {
            await _dbContext.BulkInsertAsync(products);
        }

        public void BulkDelete(IList<Product> products)
        {
            _dbContext.BulkDelete(products);
        }

        public async Task BulkDeleteAsync(IList<Product> products)
        {
            await _dbContext.BulkDeleteAsync(products);
        }

        public IList<Product> GetProducts(IList<long> productIds)
        {
            return _dbContext.Products.Where(p => productIds.Contains(p.Id)).ToList();
        }

        public async Task<IList<Product>> GetProductsAsync(IList<long> productIds)
        {
            return await _dbContext.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
        }

        public int DeleteAll()
        {
            return _dbContext.Products.Delete();
        }

        public async Task<int> DeleteAllAsync()
        {
            return await _dbContext.Products.DeleteAsync();
        }
    }
}
