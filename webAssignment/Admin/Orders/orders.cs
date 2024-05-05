using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAssignment.Admin.Orders
{
    public class orders
    {
        public string OrderId { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductName { get; set; }
        public int AdditionalProductsCount { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
    }
    public class orderInfo
    {
        public string order_id { get; set; }
        public DateTime date_ordered { get; set; }
        public string status { get; set; }
    }
    public class ordersDetail
    {
        public string product_name { get; set; }
        public string variant_name { get; set; }
        public decimal variant_price { get; set; }
        public string category_name { get; set; }
        public int quantity { get; set; }
        public decimal totalRowPrice { get; set; }
        public string ProductImageUrl { get; set; }
    }
}