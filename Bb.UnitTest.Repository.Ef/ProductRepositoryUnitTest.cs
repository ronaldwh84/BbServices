using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bb.Data;
using Bb.Data.Entities;
using Bb.Data.Repository;
using Bb.Data.Repository.Ef;
using Bb.WebService.Controllers;
using Bb.WebService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bb.UnitTest.Repository.Ef
{
    [TestClass]
    public class ProductRepositoryUnitTest
    {
        IProductRepository _productRepository;
        IList<string> _dataRow;

        public ProductRepositoryUnitTest()
        {
            _productRepository = new ProductRepositoryEf();
            _dataRow = File.ReadLines("ProductsData.csv").ToList();
        }

        public async Task InitializeDataAsync(int count)
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    SaleAmount = decimal.Parse(data[3])
                });
            }
            await _productRepository.BulkCreateAsync(products);
        }

        //[TestMethod]
        public async Task Put1ProductLessThen1Second()
        {
            int count = 1;
            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    SaleAmount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            await _productRepository.BulkCreateAsync(products);
            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

            var checkProducts = await _productRepository.GetProductsAsync(products.Select(p => p.Id).ToList());
            Console.WriteLine(checkProducts.Count + " == " + products.Count);
            Assert.IsTrue(checkProducts.Count == products.Count);

        }

        [TestMethod]
        public async Task Put1ProductWithExistingId()
        {
            int count = 1;
            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    SaleAmount = decimal.Parse(data[3])
                });
            }

            await _productRepository.BulkCreateAsync(products);

            try
            {
                await _productRepository.BulkCreateAsync(products);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DuplicatedIdException);
            }
        }

        [TestMethod]
        public async Task Put10ProductsLessThen1Second()
        {
            int count = 10;
            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    SaleAmount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            await _productRepository.BulkCreateAsync(products);
            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

            var checkProducts = await _productRepository.GetProductsAsync(products.Select(p => p.Id).ToList());
            Console.WriteLine(checkProducts.Count + " == " + products.Count);
            Assert.IsTrue(checkProducts.Count == products.Count);

        }

        //[TestMethod]
        public async Task Put100ProductsLessThen1Second()
        {
            int count = 100;
            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    SaleAmount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            await _productRepository.BulkCreateAsync(products);
            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

            var checkProducts = await _productRepository.GetProductsAsync(products.Select(p => p.Id).ToList());
            Console.WriteLine(checkProducts.Count + " == " + products.Count);
            Assert.IsTrue(checkProducts.Count == products.Count);

        }

        //[TestMethod]
        public async Task Put1000ProductsLessThen1Second()
        {
            int count = 1000;
            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    SaleAmount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            await _productRepository.BulkCreateAsync(products);
            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

            var checkProducts = await _productRepository.GetProductsAsync(products.Select(p => p.Id).ToList());
            Console.WriteLine(checkProducts.Count + " == " + products.Count);
            Assert.IsTrue(checkProducts.Count == products.Count);

        }

        [TestMethod]
        public async Task Put10000ProductsLessThen2Seconds()
        {
            int count = 10000;
            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    SaleAmount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            await _productRepository.BulkCreateAsync(products);
            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 2");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 2);

            var checkProducts = await _productRepository.GetProductsAsync(products.Select(p => p.Id).ToList());
            Console.WriteLine(checkProducts.Count + " == " + products.Count);
            Assert.IsTrue(checkProducts.Count == products.Count);

        }

        //[TestMethod]
        public async Task Put100000ProductsLessThen3Seconds()
        {
            int count = 100000;
            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    SaleAmount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            await _productRepository.BulkCreateAsync(products);
            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 3");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 3);

            var checkProducts = await _productRepository.GetProductsAsync(products.Select(p => p.Id).ToList());
            Console.WriteLine(checkProducts.Count + " == " + products.Count);
            Assert.IsTrue(checkProducts.Count == products.Count);

        }

        [TestMethod]
        public async Task Get10ProductLessThen1Second()
        {
            int count = 10;
            await InitializeDataAsync(count);

            List<long> productIds = new List<long>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                productIds.Add(long.Parse(data[0]));
            }

            Stopwatch sw = Stopwatch.StartNew();
            var productsResult = await _productRepository.GetProductsAsync(productIds);
            sw.Stop();

            Console.WriteLine(productsResult.Count() + " == " + productIds.Count);
            Assert.IsTrue(productsResult.Count() == productIds.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

        }

        [TestMethod]
        public async Task Get10000ProductsLessThen2Seconds()
        {
            int count = 10000;
            await InitializeDataAsync(count);

            List<long> productIds = new List<long>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                productIds.Add(long.Parse(data[0]));
            }

            Stopwatch sw = Stopwatch.StartNew();
            var productsResult = await _productRepository.GetProductsAsync(productIds);
            sw.Stop();

            Console.WriteLine(productsResult.Count() + " == " + productIds.Count);
            Assert.IsTrue(productsResult.Count() == productIds.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 2");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 2);

        }

        //[TestMethod]
        public async Task Get100000ProductsLessThen3Seconds()
        {
            int count = 100000;
            await InitializeDataAsync(count);

            List<long> productIds = new List<long>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                productIds.Add(long.Parse(data[0]));
            }

            Stopwatch sw = Stopwatch.StartNew();
            var productsResult = await _productRepository.GetProductsAsync(productIds);
            sw.Stop();

            Console.WriteLine(productsResult.Count() + " == " + productIds.Count);
            Assert.IsTrue(productsResult.Count() == productIds.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 3");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 3);

        }

        [TestMethod]
        public async Task Delete10ProductsLessThen1Second()
        {
            int count = 10;
            await InitializeDataAsync(count);

            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            await _productRepository.BulkDeleteAsync(products);
            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);
        }

        [TestMethod]
        public async Task Delete10000ProductsLessThen2Seconds()
        {
            int count = 10000;
            await InitializeDataAsync(count);

            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            await _productRepository.BulkDeleteAsync(products);
            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 2");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 2);
        }

        //[TestMethod]
        public async Task Delete100000ProductsLessThen3Seconds()
        {
            int count = 100000;
            await InitializeDataAsync(count);

            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            await _productRepository.BulkDeleteAsync(products);
            sw.Stop();

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 3");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 3);
        }

        [TestCleanup]
        public async Task DataCleanUp()
        {
            var deletedDataCount = await _productRepository.DeleteAllAsync();
            Console.WriteLine("Deleted Data Count: " + deletedDataCount);
        }
    }
}
