using Bb.Data;
using Bb.Data.Repository;
using Bb.Data.Repository.Ef;
using Bb.WebService.Filters;
using Bb.WebService.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [BasicAuthenticationFilter]
        public async Task<JsonResult> PutProducts(PutProductsRequestModel requestModel)
        {
            _logger.Debug("DeleteProducts BEGIN");
            var responseModel = new PutProductsResponseModel();

            try
            {
                await _productRepository.BulkCreateAsync(requestModel.Products);
                responseModel.Message = "Success";
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
        [BasicAuthenticationFilter]
        public async Task<JsonResult> DeleteProducts(DeleteProductsRequestModel requestModel)
        {
            _logger.Debug("DeleteProducts BEGIN");
            var responseModel = new DeleteProductsResponseModel();

            try
            {
                await _productRepository.BulkDeleteAsync(requestModel.Products);
                responseModel.Message = "Success";
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
        [BasicAuthenticationFilter]
        public async Task<JsonResult> GetProducts(GetProductsRequestModel requestModel)
        {
            _logger.Debug("GetProducts BEGIN");
            var responseModel = new GetProductsResponseModel();

            try
            {
                responseModel.Products = await _productRepository.GetProductsAsync(requestModel.Products.Select(p => p.Id).ToList());
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