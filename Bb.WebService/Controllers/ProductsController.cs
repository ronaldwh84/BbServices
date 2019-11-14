using AutoMapper;
using Bb.Data;
using Bb.Data.Entities;
using Bb.Data.Repository;
using Bb.Data.Repository.Ef;
using Bb.WebService.Filters;
using Bb.WebService.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private IMapper _mapper;
        // GET: Products

        public ProductsController(IProductRepository productRepository, ILog logger, IMapper mapper)
        {

            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [BasicAuthenticationFilter]
        public async Task<JsonResult> PutProducts(PutProductsRequestModel requestModel)
        {
            _logger.Debug("DeleteProducts BEGIN");
            var responseModel = new PutProductsResponseModel();

            try
            {
                await _productRepository.BulkCreateAsync(_mapper.Map<IList<ProductModel>,IList<Product>>(requestModel.Products));
                responseModel.Message = "Success";
                responseModel.ResponseCode = ResponseCode.SUCCESS;
            }
            catch (DuplicatedIdException ex)
            {
                var tick = requestModel.Id + DateTime.UtcNow.Ticks;
                _logger.Error(tick);
                _logger.Error(ex);
                responseModel.Message = "Failed. " + ex.Message + ". Reference Id: " + tick;
                responseModel.ResponseCode = ResponseCode.DUPLICATED_ID;
            }
            catch (Exception ex)
            {
                var tick = requestModel.Id + DateTime.UtcNow.Ticks;
                _logger.Error(tick);
                _logger.Error(ex);
                responseModel.Message = "Failed. Reference Id: " + tick;
                responseModel.ResponseCode = ResponseCode.GENERAL_ERROR;
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
                await _productRepository.BulkDeleteAsync(_mapper.Map<IList<ProductModel>, IList<Product>>(requestModel.Products));
                responseModel.Message = "Success";
                responseModel.ResponseCode = ResponseCode.SUCCESS;
            }
            catch (Exception ex)
            {
                var tick = requestModel.Id + DateTime.UtcNow.Ticks;
                _logger.Error(tick);
                _logger.Error(ex);
                responseModel.Message = "Failed. Reference Id: " + tick;
                responseModel.ResponseCode = ResponseCode.GENERAL_ERROR;
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
                var productsResult = await _productRepository.GetProductsAsync(requestModel.Products.Select(a => a.Id).ToList());
                responseModel.Products = _mapper.Map<IList<Product>, IList<ProductModel>>(productsResult);
                responseModel.Message = responseModel.Products.Count() + " products retrieved.";
                responseModel.ResponseCode = ResponseCode.SUCCESS;
            }
            catch (Exception ex)
            {
                var tick = requestModel.Id + DateTime.UtcNow.Ticks;
                _logger.Error(tick);
                _logger.Error(ex);
                responseModel.Message = "Failed. Reference Id: " + tick;
                responseModel.ResponseCode = ResponseCode.GENERAL_ERROR;
            }

            responseModel.Id = requestModel.Id;
            responseModel.Timestamp = DateTime.UtcNow;
            _logger.Debug("GetProducts END");
            return Json(responseModel);
        }
    }
}