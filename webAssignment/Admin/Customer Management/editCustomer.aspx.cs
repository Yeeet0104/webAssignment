using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Customer
{
    public partial class editCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string name = Request.QueryString["name"];
                string email = Request.QueryString["email"];
                string phone = Request.QueryString["phone"];
                string dob = Request.QueryString["dob"];
                string imgUrl = Request.QueryString["imgUrl"];

                if (!string.IsNullOrEmpty(name))
                {
                    txtEditName.Text = HttpUtility.UrlDecode(name);
                }

                if (!string.IsNullOrEmpty(email))
                {
                    txtEditEmail.Text = HttpUtility.UrlDecode(email);
                }

                if (!string.IsNullOrEmpty(phone))
                {
                    txtEditPhoneNo.Text = HttpUtility.UrlDecode(phone);
                }

                if (!string.IsNullOrEmpty(imgUrl))
                {
                    profilePic.ImageUrl = HttpUtility.UrlDecode(imgUrl);
                }
            }
        }


        private DataRow GetUpdatedCustomerData()
        {
            // Retrieve the updated customer data from the form fields
            string name = txtEditName.Text;
            string email = txtEditEmail.Text;
            string phone = txtEditPhoneNo.Text;
            string imageUrl = profilePic.ImageUrl;

            // Create a DataTable with the required columns
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerName", typeof(string));
            dt.Columns.Add("CustomerEmail", typeof(string));
            dt.Columns.Add("PhoneNo", typeof(string));
            dt.Columns.Add("CustomerImageUrl", typeof(string));

            // Create a new DataRow and populate it with the updated data
            DataRow updatedRow = dt.NewRow();
            updatedRow["CustomerName"] = name;
            updatedRow["CustomerEmail"] = email;
            updatedRow["PhoneNo"] = phone;
            updatedRow["CustomerImageUrl"] = imageUrl;

            return updatedRow;
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
                lblStatus.Text = ddlStatus.SelectedItem.Text.ToString();
        }
    }
}