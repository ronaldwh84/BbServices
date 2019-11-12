using Bb.Data.Entities;
using System;
using System.Collections.Generic;
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

        public int BulkCreate(IEnumerable<Product> products)
        {
            _dbContext.Products.AddRange(products);
            return _dbContext.SaveChanges();
        }

        public int BulkDelete(IEnumerable<long> productIds)
        {
            var totalDeletedProductsCount = 0;
            var iteration = 1;
            var batchCount = 2000;
            var proccessedIds = productIds.Take(batchCount);
            do
            {
                totalDeletedProductsCount += _dbContext.Products.Where(p => proccessedIds.Contains(p.Id)).Delete();
                proccessedIds = productIds.Skip(iteration * batchCount).Take(batchCount);
                iteration++;

            } while (proccessedIds.Count() > 0);

            return totalDeletedProductsCount;
        }

        public IEnumerable<Product> GetProducts(IEnumerable<long> productIds)
        {
            return _dbContext.Products.Where(p => productIds.Contains(p.Id));
        }

        public int DeleteAll()
        {
            return _dbContext.Products.Delete();
        }
    }
}
