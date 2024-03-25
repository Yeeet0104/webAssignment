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

        protected void btnNewProfileImg_Click(object sender, EventArgs e)
        {
            if (choosePic.HasFile)
            {
                try
                {
                    // Save the uploaded file to a specific folder
                    string filename = Path.GetFileName(choosePic.FileName);
                    choosePic.SaveAs(Server.MapPath("~/Admin/Layout/image/") + filename);

                    // Update the ImageUrl of the Image control with the newly uploaded image
                    profilePic.ImageUrl = "~/Admin/Layout/image/" + filename;
                }
                catch (Exception ex)
                {
                    // Handle exceptions if any
                    // You can display an error message or log the exception
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

        protected void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            // Get the updated customer data
            object updatedRow = GetUpdatedCustomerData();

            // Pass the updated data as query string parameters
            string queryString = "?updatedData=" + HttpUtility.UrlEncode(updatedRow.ToString());
            Response.Redirect("customerManagement.aspx" + queryString);
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
                lblStatus.Text = ddlStatus.SelectedItem.Text.ToString();
        }
    }
}