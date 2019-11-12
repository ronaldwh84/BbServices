using Bb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Data.Repository.Ef
{
    public class ProductRepositoryEf : IProductRepository
    {
        private BbContext _dbContext;
        public ProductRepositoryEf()
        {
            _dbContext = new BbContext();
        }

        public bool BulkCreate(IEnumerable<Product> products)
        {
            try
            {
                _dbContext.Products.AddRange(products);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Product> GetProducts(IEnumerable<long> productIds)
        {
            try
            {
                return _dbContext.Products;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
