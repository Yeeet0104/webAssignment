using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAssignment.Admin.Voucher
{
    public class Voucher
    {
        public string voucher_id { get; set; }
        public int quantity { get; set; }
        public double discount_rate { get; set; }
        public decimal cap_at { get; set; }
        public decimal min_spend { get; set; }
        public DateTime added_date { get; set; }
        public DateTime started_date { get; set; }
        public DateTime expiry_date { get; set; }
        public string voucher_status { get; set; }
    }
}