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
        /// <summary>
        /// Insert multiple product records
        /// </summary>
        /// <param name="products">List of product records that we want to insert.</param>
        void BulkCreate(IList<Product> products);

        /// <summary>
        /// Insert multiple product records asynchronously
        /// </summary>
        /// <param name="products">List of product records that we want to insert.</param>
        /// <returns></returns>
        Task BulkCreateAsync(IList<Product> products);

        /// <summary>
        /// Delete multiple product records
        /// </summary>
        /// <param name="products">List of product records that we want to delete. Only the Id is needed for this operation.</param>
        void BulkDelete(IList<Product> products);

        /// <summary>
        /// Delete multiple product records asynchronously
        /// </summary>
        /// <param name="products">List of product records that we want to delete. Only the Id is needed for this operation.</param>
        /// <returns></returns>
        Task BulkDeleteAsync(IList<Product> products);

        /// <summary>
        /// Retrieve multiple product records.
        /// </summary>
        /// <param name="productIds">List of product ids that we want to retrieve</param>
        /// <returns></returns>
        IList<Product> GetProducts(IList<long> productIds);

        /// <summary>
        /// Retrieve multiple product records asynchronously.
        /// </summary>
        /// <param name="productIds">List of product ids that we want to retrieve</param>
        /// <returns></returns>
        Task<IList<Product>> GetProductsAsync(IList<long> productIds);

        /// <summary>
        /// Delete all products in DB.
        /// </summary>
        /// <returns>The number of products deleted in DB</returns>
        int DeleteAll();

        /// <summary>
        /// Delete all product records.
        /// </summary>
        /// <returns>The number of product records deleted</returns>
        Task<int> DeleteAllAsync();

    }
}
