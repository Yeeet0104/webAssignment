using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAssignment.Admin.Product_Management
{
    public class productsList
    {
        public string CategoryName { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public string ProductVariantID { get; set; }
        public decimal VariantPrice { get; set; }
        public string variantCount { get; set; }
        public int Stock { get; set; }
        public string ProductStatus { get; set; }
    }

    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public List<Variant> Variants { get; set; } = new List<Variant>();
    }

    public class Variant
    {
        public string VariantId { get; set; }
        public string VariantName { get; set; }
        // Add other properties as needed, such as price, stock, etc.
    }

}