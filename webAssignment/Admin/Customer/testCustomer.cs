using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

    public class testCustomer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public DateTime DOB { get; set; }
        public string Status { get; set; }
        public DateTime Added { get; set; }
    

    public testCustomer(string name, string email, string phoneNo, DateTime dob, string status, DateTime added)
    {
        Name = name;
        Email = email;
        PhoneNo = phoneNo;
        DOB = dob;
        Status = status;
        Added = added;
    }
}