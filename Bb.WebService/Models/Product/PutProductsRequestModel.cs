using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bb.Data.Entities;
using Newtonsoft.Json;

namespace Bb.WebService.Models
{
    public class PutProductsRequestModel : BaseRequestModel
    {
        [JsonProperty("products")]
        public IList<Product> Products { get; set; }
    }
}