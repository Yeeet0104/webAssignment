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
        public DateTime date_added { get; set; }
        public string ProductVariantID { get; set; }
        public decimal VariantPrice { get; set; }
        public string variantCount { get; set; }
        public int total_stock { get; set; }
        public string ProductStatus { get; set; }
    }

    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public List<Variant> Variants { get; set; } = new List<Variant>();
    }
    [Serializable]
    public class Variant
    {
        public string product_variant_id { get; set; }
        public string variant_name { get; set; }
        public decimal variant_price { get; set; }
        public int stock { get; set; }

    }
    public partial class Category
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}