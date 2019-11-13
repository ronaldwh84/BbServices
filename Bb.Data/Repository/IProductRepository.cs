using Bb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Data.Repository
{
    public interface IProductRepository
    {
        void BulkCreate(IList<Product> products);

        Task BulkCreateAsync(IList<Product> products);

        void BulkDelete(IList<Product> products);

        Task BulkDeleteAsync(IList<Product> products);

        IList<Product> GetProducts(IList<long> productIds);

        Task<IList<Product>> GetProductsAsync(IList<long> productIds);

        int DeleteAll();

        Task<int> DeleteAllAsync();

    }
}
