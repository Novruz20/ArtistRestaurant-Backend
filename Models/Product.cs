using System;
using System.Collections.Generic;

namespace Artist_api1.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductPhoto { get; set; }
        public string? ProductMainCategory { get; set; }
        public string? ProductSubCategory { get; set; }
        public int? ProductPrice { get; set; }
        public string? ProductAlcoholCategory { get; set; }
    }
}
