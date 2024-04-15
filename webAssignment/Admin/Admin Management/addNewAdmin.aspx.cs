using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Admin_Management
{
    public partial class addNewAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void btnNewProfileImg_Click(object sender, EventArgs e)
        //{
        //    if (choosePic.HasFile)
        //    {
        //        try
        //        {
        //            // Save the uploaded file to a specific folder
        //            string filename = Path.GetFileName(choosePic.FileName);
        //            choosePic.SaveAs(Server.MapPath("~/Admin/Layout/image/") + filename);

        //            // Update the ImageUrl of the Image control with the newly uploaded image
        //            profilePic.ImageUrl = "~/Admin/Layout/image/" + filename;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exceptions if any
        //            // You can display an error message or log the exception
        //        }
        //    }
        //}

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblStatus.Text = ddlStatus.SelectedItem.Text.ToString();
        }

    }
}