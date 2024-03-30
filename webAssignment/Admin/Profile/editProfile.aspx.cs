using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Profile
{
    public partial class editProfile : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {

        }
        protected void closePopUp_Click( object sender, EventArgs e )
        {
            popUpConfirmation.Style.Add("display", "none");
        }
        protected void btnCancelEdit_Click( object sender, EventArgs e )
        {
            popUpConfirmation.Style.Add("display", "none");
        }

        protected void btnEdit_Click( object sender, EventArgs e )
        {
            popUpConfirmation.Style.Add("display", "flex");
        }
    }
}