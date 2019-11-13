using Bb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
            try
            {
                _dbContext.BulkInsert(products);
            }
            catch (Exception ex)
            {
                if (ex is SqlException && ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    throw new DuplicatedIdException("Duplicated Id detected");
                }
                throw;
            }
        }

        public async Task BulkCreateAsync(IList<Product> products)
        {
            try
            {
                await _dbContext.BulkInsertAsync(products);
            }
            catch (Exception ex)
            {
                if (ex is SqlException && ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    throw new DuplicatedIdException("Duplicated Id detected");
                }
                throw;
            }
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
