using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bb.WebService.Models
{
    public class ProductModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Sale_Amount { get; set; }
    }
}