using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
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

        public ProductRepositoryUnitTest()
        {
            _productRepository = new ProductRepositoryEf();
        }

        public void Initialize5Data()
        {
            List<Product> products = new List<Product>();
            foreach (var line in File.ReadLines("5ProductsData.csv"))
            {
                var data = line.Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    Sale_Amount = decimal.Parse(data[3])
                });
            }
            _productRepository.BulkCreate(products);
        }

        public void Initialize10000Data()
        {
            List<Product> products = new List<Product>();
            foreach (var line in File.ReadLines("10000ProductsData.csv"))
            {
                var data = line.Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    Sale_Amount = decimal.Parse(data[3])
                });
            }
            _productRepository.BulkCreate(products);
        }

        [TestMethod]
        public void Put5ProductsLessThen1Second()
        {
            List<Product> products = new List<Product>();
            foreach (var line in File.ReadLines("5ProductsData.csv"))
            {
                var data = line.Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    Sale_Amount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            var createdProductsCount = _productRepository.BulkCreate(products);
            sw.Stop();

            Console.WriteLine(createdProductsCount + " == " + products.Count);
            Assert.IsTrue(createdProductsCount == products.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

        }

        [TestMethod]
        public void Put10000ProductsLessThen1Second()
        {
            List<Product> products = new List<Product>();
            foreach (var line in File.ReadLines("10000ProductsData.csv"))
            {
                var data = line.Split(',');
                products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    Sale_Amount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            var createdProductsCount = _productRepository.BulkCreate(products);
            sw.Stop();

            Console.WriteLine(createdProductsCount + " == " + products.Count);
            Assert.IsTrue(createdProductsCount == products.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

        }

        [TestMethod]
        public void Get5ProductsLessThen1Seconds()
        {
            Initialize5Data();

            List<long> productIds = new List<long>();
            foreach (var line in File.ReadLines("5ProductsData.csv"))
            {
                var data = line.Split(',');
                productIds.Add(long.Parse(data[0]));
            }

            Stopwatch sw = Stopwatch.StartNew();
            var productsResult = _productRepository.GetProducts(productIds);
            sw.Stop();

            Console.WriteLine(productsResult.Count() + " == " + productIds.Count);
            Assert.IsTrue(productsResult.Count() == productIds.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

        }

        [TestMethod]
        public void Get10000ProductsLessThen1Seconds()
        {
            Initialize10000Data();

            List<long> productIds = new List<long>();
            foreach (var line in File.ReadLines("10000ProductsData.csv"))
            {
                var data = line.Split(',');
                productIds.Add(long.Parse(data[0]));
            }

            Stopwatch sw = Stopwatch.StartNew();
            var productsResult = _productRepository.GetProducts(productIds);
            sw.Stop();

            Console.WriteLine(productsResult.Count() + " == " + productIds.Count);
            Assert.IsTrue(productsResult.Count() == productIds.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

        }

        [TestMethod]
        public void Delete5ProductsLessThen1Seconds()
        {
            Initialize5Data();

            List<long> productIds = new List<long>();
            foreach (var line in File.ReadLines("5ProductsData.csv"))
            {
                var data = line.Split(',');
                productIds.Add(long.Parse(data[0]));
            }

            Stopwatch sw = Stopwatch.StartNew();
            var deletedProductsCount = _productRepository.BulkDelete(productIds);
            sw.Stop();

            Console.WriteLine(deletedProductsCount + " == " + productIds.Count);
            Assert.IsTrue(deletedProductsCount == productIds.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);
        }

        [TestMethod]
        public void Delete10000ProductsLessThen1Seconds()
        {
            Initialize10000Data();

            List<long> productIds = new List<long>();
            foreach (var line in File.ReadLines("10000ProductsData.csv"))
            {
                var data = line.Split(',');
                productIds.Add(long.Parse(data[0]));
            }

            Stopwatch sw = Stopwatch.StartNew();
            var deletedProductsCount = _productRepository.BulkDelete(productIds);
            sw.Stop();

            Console.WriteLine(deletedProductsCount + " == " + productIds.Count);
            Assert.IsTrue(deletedProductsCount == productIds.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < 1");
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);
        }

        [TestCleanup]
        public void DataCleanUp()
        {
            var deletedDataCount = _productRepository.DeleteAll();
            Console.WriteLine("Deleted Data Count: " + deletedDataCount);
        }
    }
}
