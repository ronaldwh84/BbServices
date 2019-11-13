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
using System.Linq;
using System.Threading.Tasks;

namespace Bb.UnitTest.WebService
{
    /// <summary>
    /// Summary description for ProductsControllerUnitTest
    /// </summary>
    [TestClass]
    public class ProductsControllerUnitTest
    {
        IProductRepository _productRepository;
        IList<string> _dataRow;

        public ProductsControllerUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
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
                    Sale_Amount = decimal.Parse(data[3])
                });
            }
            await _productRepository.BulkCreateAsync(products);
        }

        [TestMethod]
        public async Task Get10ProductFromControllerLessThan1Second()
        {
            int count = 10;
            await InitializeDataAsync(count);

            var productsController = new ProductsController(_productRepository);
            var requestModel = new GetProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }
            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = await productsController.GetProducts(requestModel);
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
        public async Task Get10000ProductsFromControllerLessThan2Seconds()
        {
            int count = 10000;
            await InitializeDataAsync(count);

            var productsController = new ProductsController(_productRepository);
            var requestModel = new GetProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }
            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = await productsController.GetProducts(requestModel);
            sw.Stop();
            GetProductsResponseModel responseModel = ((GetProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Products.Count + " == " + requestModel.Products.Count);
            Assert.IsTrue(responseModel.Products.Count == requestModel.Products.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 2);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 2);
        }

        //[TestMethod]
        public async Task Get100000ProductsFromControllerLessThan3Seconds()
        {
            int count = 100000;
            await InitializeDataAsync(count);

            var productsController = new ProductsController(_productRepository);
            var requestModel = new GetProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }
            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = await productsController.GetProducts(requestModel);
            sw.Stop();
            GetProductsResponseModel responseModel = ((GetProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Products.Count + " == " + requestModel.Products.Count);
            Assert.IsTrue(responseModel.Products.Count == requestModel.Products.Count);

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 3);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 3);
        }

        [TestMethod]
        public async Task Put10ProductFromControllerLessThan1Second()
        {
            int count = 10;
            var productsController = new ProductsController(_productRepository);
            var requestModel = new PutProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    Sale_Amount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = await productsController.PutProducts(requestModel);
            sw.Stop();

            PutProductsResponseModel responseModel = ((PutProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Message + " == Success");
            Assert.IsTrue(responseModel.Message == "Success");

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 1);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);

        }

        [TestMethod]
        public async Task Put10000ProductsFromControllerLessThan2Seconds()
        {
            int count = 10000;
            var productsController = new ProductsController(_productRepository);
            var requestModel = new PutProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    Sale_Amount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = await productsController.PutProducts(requestModel);
            sw.Stop();

            var responseModel = ((PutProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Message + " == Success");
            Assert.IsTrue(responseModel.Message == "Success");

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 2);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 2);

        }

        //[TestMethod]
        public async Task Put100000ProductsFromControllerLessThan3Seconds()
        {
            int count = 100000;
            var productsController = new ProductsController(_productRepository);
            var requestModel = new PutProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0]),
                    Name = data[1],
                    Quantity = int.Parse(data[2]),
                    Sale_Amount = decimal.Parse(data[3])
                });
            }

            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = await productsController.PutProducts(requestModel);
            sw.Stop();

            var responseModel = ((PutProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Message + " == Success");
            Assert.IsTrue(responseModel.Message == "Success");

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 3);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 3);

        }

        [TestMethod]
        public async Task Delete10ProductFromControllerLessThan1Second()
        {
            int count = 10;
            await InitializeDataAsync(count);

            var productsController = new ProductsController(_productRepository);
            var requestModel = new DeleteProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }
            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = await productsController.DeleteProducts(requestModel);
            sw.Stop();
            var responseModel = ((DeleteProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Message + " == Success");
            Assert.IsTrue(responseModel.Message == "Success");

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 1);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 1);
        }

        [TestMethod]
        public async Task Delete10000ProductsFromControllerLessThan2Seconds()
        {
            int count = 10000;
            await InitializeDataAsync(count);

            var productsController = new ProductsController(_productRepository);
            var requestModel = new DeleteProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }
            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = await productsController.DeleteProducts(requestModel);
            sw.Stop();
            var responseModel = ((DeleteProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Message + " == Success");
            Assert.IsTrue(responseModel.Message == "Success");

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 2);
            Assert.IsTrue(sw.Elapsed.TotalSeconds < 2);
        }

        //[TestMethod]
        public async Task Delete100000ProductsFromControllerLessThan3Seconds()
        {
            int count = 100000;
            await InitializeDataAsync(count);

            var productsController = new ProductsController(_productRepository);
            var requestModel = new DeleteProductsRequestModel();
            requestModel.Id = "123";
            requestModel.Timestamp = DateTime.UtcNow;
            requestModel.Products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var data = _dataRow[i].Split(',');
                requestModel.Products.Add(new Product
                {
                    Id = long.Parse(data[0])
                });
            }
            Stopwatch sw = Stopwatch.StartNew();
            JsonResult result = await productsController.DeleteProducts(requestModel);
            sw.Stop();
            var responseModel = ((DeleteProductsResponseModel)result.Data);

            Console.WriteLine(responseModel.Id + " == " + requestModel.Id);
            Assert.IsTrue(responseModel.Id == requestModel.Id);

            Console.WriteLine(responseModel.Message + " == Success");
            Assert.IsTrue(responseModel.Message == "Success");

            Console.WriteLine(sw.Elapsed.TotalSeconds + " < " + 3);
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
