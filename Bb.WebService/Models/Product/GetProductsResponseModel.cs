using Bb.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bb.WebService.Models
{
    public class GetProductsResponseModel : BaseResponseModel
    {
        [JsonProperty("products")]
        public IEnumerable<Product> Products { get; set; }
    }
}