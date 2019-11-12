using Bb.Data;
using Bb.Data.Repository;
using Bb.Data.Repository.Ef;
using Bb.WebService.Models;
using log4net;
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
        private ILog _logger;
        // GET: Products

        public ProductsController(IProductRepository productRepository)
        {

            _productRepository = productRepository;
            _logger = LogManager.GetLogger("bb.webservice");
        }

        [HttpPost]
        public JsonResult PutProducts(PutProductsRequestModel requestModel)
        {
            _logger.Debug("DeleteProducts BEGIN");
            var responseModel = new PutProductsResponseModel();

            try
            {
                var createdProductsCount = _productRepository.BulkCreate(requestModel.Products);
                responseModel.Message = createdProductsCount + " new products created.";
            }
            catch (Exception ex)
            {
                var tick = requestModel.Id + DateTime.UtcNow.Ticks;
                _logger.Error(tick);
                _logger.Error(ex);
                responseModel.Message = "Failed. Reference Id: " + tick;
            }

            responseModel.Id = requestModel.Id;
            responseModel.Timestamp = DateTime.UtcNow;
            _logger.Debug("DeleteProducts DELETE");
            return Json(responseModel);
        }

        [HttpPost]
        public JsonResult DeleteProducts(DeleteProductsRequestModel requestModel)
        {
            _logger.Debug("DeleteProducts BEGIN");
            var responseModel = new DeleteProductsResponseModel();

            try
            {
                var deletedProductsCount = _productRepository.BulkDelete(requestModel.Products.Select(p => p.Id));
                responseModel.Message = deletedProductsCount + " products deleted.";
            }
            catch (Exception ex)
            {
                var tick = requestModel.Id + DateTime.UtcNow.Ticks;
                _logger.Error(tick);
                _logger.Error(ex);
                responseModel.Message = "Failed. Reference Id: " + tick;
            }

            responseModel.Id = requestModel.Id;
            responseModel.Timestamp = DateTime.UtcNow;
            _logger.Debug("DeleteProducts END");
            return Json(responseModel);
        }

        [HttpPost]
        public JsonResult GetProducts(GetProductsRequestModel requestModel)
        {
            _logger.Debug("GetProducts BEGIN");
            var responseModel = new GetProductsResponseModel();

            try
            {
                responseModel.Products = _productRepository.GetProducts(requestModel.Products.Select(p => p.Id)).ToList();
                responseModel.Message = responseModel.Products.Count() + " products retrieved.";
            }
            catch (Exception ex)
            {
                var tick = requestModel.Id + DateTime.UtcNow.Ticks;
                _logger.Error(tick);
                _logger.Error(ex);
                responseModel.Message = "Failed. Reference Id: " + tick;
            }

            responseModel.Id = requestModel.Id;
            responseModel.Timestamp = DateTime.UtcNow;
            _logger.Debug("GetProducts END");
            return Json(responseModel);
        }
    }
}