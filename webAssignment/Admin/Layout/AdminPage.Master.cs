using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace webAssignment
{
    public interface IFilterable
    {
        void FilterListView( string searchTerm );
    }
    public partial class AdminPage : System.Web.UI.MasterPage
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            assignActiveClass();

        }

        private void assignActiveClass( )
        {
            string currentPage = Path.GetFileName(Request.Path).ToLower();
            // Append 'active' class based on current page
            switch ( currentPage )
            {
                case "customermanagement.aspx":
                case "editcustomer.aspx":
                    customerLk.Attributes["class"] += " activeNavItem";

                    break;
                case "adminmanagement.aspx":
                case "addnewadmin.aspx":
                    adminLk.Attributes["class"] += " activeNavItem";

                    break;
                case "dashboard.aspx":
                    dashboardLk.Attributes["class"] += " activeNavItem";

                    break;
                case "addnewproduct.aspx":
                case "adminproducts.aspx":
                case "editproduct.aspx":
                case "productvariant.aspx":
                    productLk.Attributes["class"] += " activeNavItem";


                    break;
                case "createcategory.aspx":
                case "category.aspx":
                case "editcategory.aspx":
                    categoryLk.Attributes["class"] += " activeNavItem";
                    break;
                case "editorder.aspx":
                case "order.aspx":
                    orderLk.Attributes["class"] += " activeNavItem";

                    break;
                case "transaction.aspx":
                    transactionLk.Attributes["class"] += " activeNavItem";

                    break;
                case "voucher.aspx":
                case "editvoucher.aspx":
                case "addVoucher.aspx":
                    voucherLk.Attributes["class"] += " activeNavItem";

                    break;                
                case "adminprofile.aspx":
                case "editprofile.aspx":
                    profileLk.Attributes["class"] += " activeNavItem";

                    break;
            }
        }

        protected void SearchButton_Click( object sender, EventArgs e )
        {
            string searchTerm = SearchTextBox.Text;
            var currentContent = this.Page as IFilterable;

            if ( currentContent != null )
            {
                currentContent.FilterListView(searchTerm);
            }
            else
            {
                // Handle the case where the content page does not implement IFilterable
            }
        }

        protected void SearchTextBox_TextChanged( object sender, EventArgs e )
        {
            string searchTerm = SearchTextBox.Text;
            var currentContent = this.Page as IFilterable;

            if ( currentContent != null )
            {
                currentContent.FilterListView(searchTerm);
            }
            else
            {
                // Handle the case where the content page does not implement IFilterable
            }
        }
    }



}