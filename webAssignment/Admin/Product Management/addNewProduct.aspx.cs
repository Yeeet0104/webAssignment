using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Product_Management
{
    public partial class addNewProduct : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {

        }

        protected void ddlnewProdStatus_SelectedIndexChanged( object sender, EventArgs e )
        {
            lblnewProdStatus.Text = ddlnewProdStatus.SelectedValue;
        }

        protected void UploadButton_Click( object sender, EventArgs e )
        {

        }
    }
}