using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Category
{
    public partial class Category : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {

            if ( !IsPostBack )
            {
                categoryListView.DataSource = GetDummyData();

                categoryListView.DataBind();
            }
        }
        private DataTable GetDummyData( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("ProductName", typeof(string));
            dummyData.Columns.Add("AdditionalProductsCount", typeof(int));
            dummyData.Columns.Add("PaymentDate", typeof(DateTime));
            dummyData.Columns.Add("Sold", typeof(int));
            dummyData.Columns.Add("Stock", typeof(int));

            // Add rows with dummy data
            dummyData.Rows.Add( "~/Admin/Layout/image/DexProfilePic.jpeg", "Product 1", 2, DateTime.Now, 10, 10);
            dummyData.Rows.Add( "~/Admin/Layout/image/DexProfilePic.jpeg", "Product 1", 2, DateTime.Now, 10, 10);
            dummyData.Rows.Add( "~/Admin/Layout/image/DexProfilePic.jpeg", "Product 1", 2, DateTime.Now, 10, 10);
            // Add more rows as needed for testing

            return dummyData;
        }

        protected void categoryListView_SelectedIndexChanged( object sender, EventArgs e )
        {

        }
    }
}