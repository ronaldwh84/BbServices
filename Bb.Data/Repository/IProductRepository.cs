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
        bool BulkCreate(IEnumerable<Product> products);

        IEnumerable<Product> GetProducts(IEnumerable<long> productIds);
    }
}
