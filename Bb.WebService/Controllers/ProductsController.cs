using Bb.Data;
using Bb.Data.Repository;
using Bb.Data.Repository.Ef;
using Bb.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Bb.WebService.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository _productRepository;
        // GET: Products

        public ProductsController()
        {

            _productRepository = new ProductRepositoryEf();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PutProducts(PutProductsRequestModel requestModel)
        {
            PutProductsResponseModel responseModel = new PutProductsResponseModel();

            _productRepository.BulkCreate(requestModel.Products);

            responseModel.Id = requestModel.Id;
            responseModel.Timestamp = DateTime.UtcNow;
            return Json(responseModel);
        }

        [HttpPost]
        public ActionResult GetProducts(GetProductsRequestModel requestModel)
        {
            GetProductsResponseModel responseModel = new GetProductsResponseModel();

            responseModel.Products = _productRepository.GetProducts(requestModel.Products.Select(p => p.Id)).ToList();

            responseModel.Id = requestModel.Id;
            responseModel.Timestamp = DateTime.UtcNow;
            return Json(responseModel);
        }
    }
}