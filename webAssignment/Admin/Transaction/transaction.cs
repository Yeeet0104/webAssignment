using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAssignment.Admin.Transaction
{
    public class transactionClass
    {
        public string order_id { get; set; }
        public string user_id { get; set; }
        public string username { get; set; }
        public decimal total_price { get; set; }
        public string payment_details { get; set; }
        public DateTime date_paid { get; set; }
        public string product_details { get; set; }
    }
}