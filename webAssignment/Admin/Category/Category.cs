using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAssignment.Admin.Category
{
    public partial class Category
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryBanner { get; set; }
        public DateTime date_added { get; set; }
        public int NumberOfProd { get; set; }
        public int Sold { get; set; }
        public int Stock { get; set; }
    }
}