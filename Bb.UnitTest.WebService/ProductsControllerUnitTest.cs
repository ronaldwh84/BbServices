using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bb.Data.Entities;
using System.IO;
using Bb.Data.Repository.Ef;
using Bb.WebService.Controllers;
using Bb.WebService.Models;
using System.Web.Mvc;
using Bb.Data.Repository;
using System.Diagnostics;

namespace Bb.UnitTest.WebService
{
    /// <summary>
    /// Summary description for ProductsControllerUnitTest
    /// </summary>
    [TestClass]
    public class ProductsControllerUnitTest
    {
        IProductRepository _productRepository;
        public ProductsControllerUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
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
        public void Get5ProductsFromControllerLessThan1Second()
        {
            Initialize5Data();

            var productsController = new ProductsController(_productRepository);
            var requestModel = new GetProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            foreach (var line in File.ReadLines("5ProductsData.csv"))
            {
                var data = line.Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }
            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = productsController.GetProducts(requestModel);
            sw.Stop();
            GetProductsResponseModel responseModel = ((GetProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Products.Count + " == " + requestModel.Products.Count);
            Assert.IsTrue(responseModel.Products.Count == requestModel.Products.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 1);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);
        }

        [TestMethod]
        public void Get10000ProductsFromControllerLessThan1Second()
        {
            Initialize10000Data();

            var productsController = new ProductsController(_productRepository);
            var requestModel = new GetProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            foreach (var line in File.ReadLines("10000ProductsData.csv"))
            {
                var data = line.Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }
            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = productsController.GetProducts(requestModel);
            sw.Stop();
            GetProductsResponseModel responseModel = ((GetProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Products.Count + " == " + requestModel.Products.Count);
            Assert.IsTrue(responseModel.Products.Count == requestModel.Products.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 1);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);
        }

        [TestMethod]
        public void Put5ProductsFromControllerLessThan1Second()
        {
            var productsController = new ProductsController(_productRepository);
            var requestModel = new PutProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            foreach (var line in File.ReadLines("5ProductsData.csv"))
            {
                var data = line.Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    Sale_Amount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = productsController.PutProducts(requestModel);
            sw.Stop();

            PutProductsResponseModel responseModel = ((PutProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Message + " == " + requestModel.Products.Count + " new products created.");
            Assert.IsTrue(responseModel.Message == requestModel.Products.Count + " new products created.");

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 1);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

        }

        [TestMethod]
        public void Put10000ProductsFromControllerLessThan1Second()
        {
            var productsController = new ProductsController(_productRepository);
            var requestModel = new PutProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            foreach (var line in File.ReadLines("10000ProductsData.csv"))
            {
                var data = line.Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    Sale_Amount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = productsController.PutProducts(requestModel);
            sw.Stop();

            var responseModel = ((PutProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Message + " == " + requestModel.Products.Count + " new products created.");
            Assert.IsTrue(responseModel.Message == requestModel.Products.Count + " new products created.");

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 1);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

        }

        [TestMethod]
        public void Delete5ProductsFromControllerLessThan1Second()
        {
            Initialize5Data();

            var productsController = new ProductsController(_productRepository);
            var requestModel = new DeleteProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            foreach (var line in File.ReadLines("5ProductsData.csv"))
            {
                var data = line.Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }
            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = productsController.DeleteProducts(requestModel);
            sw.Stop();
            var responseModel = ((DeleteProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Message + " == " + requestModel.Products.Count + " new products created.");
            Assert.IsTrue(responseModel.Message == requestModel.Products.Count + " products deleted.");

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 1);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);
        }

        [TestMethod]
        public void Delete10000ProductsFromControllerLessThan1Second()
        {
            Initialize10000Data();

            var productsController = new ProductsController(_productRepository);
            var requestModel = new DeleteProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            foreach (var line in File.ReadLines("10000ProductsData.csv"))
            {
                var data = line.Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }
            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = productsController.DeleteProducts(requestModel);
            sw.Stop();
            var responseModel = ((DeleteProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Message + " == " + requestModel.Products.Count + " new products created.");
            Assert.IsTrue(responseModel.Message == requestModel.Products.Count + " products deleted.");

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 1);
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
