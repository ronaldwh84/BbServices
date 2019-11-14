using Bb.Data.Entities;
using Bb.WebService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bb.WebService.Models
{
    public class PutProductsResponseModel : BaseResponseModel
    {
        [JsonProperty("products")]
        public IList<ProductModel> Products { get; set; }
    }
}