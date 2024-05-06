using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAssignment.Admin.Admin_Management
{
    public class Admin
    {
        public string user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public DateTime birth_date { get; set; }
        public string profile_pic_path { get; set; }
        public DateTime date_created { get; set; }
        public string status { get; set; }
        public string role { get; set; }
    }
}