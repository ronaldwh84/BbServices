using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bb.Data.Entities;
using Bb.WebService.Models;
using Newtonsoft.Json;

namespace Bb.WebService.Models
{
    public class PutProductsRequestModel : BaseRequestModel
    {
        [JsonProperty("products")]
        public IList<ProductModel> Products { get; set; }
    }
}