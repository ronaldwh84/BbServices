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
        int BulkCreate(IEnumerable<Product> products);

        int BulkDelete(IEnumerable<long> productIds);

        IEnumerable<Product> GetProducts(IEnumerable<long> productIds);

        int DeleteAll();

    }
}
