using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment
{
    public partial class adminProducts : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                productListView.DataSource = GetDummyData();

                productListView.DataBind();
            }
        }
        private DataTable GetDummyData( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("OrderId", typeof(int));
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("ProductName", typeof(string));
            dummyData.Columns.Add("AdditionalProductsCount", typeof(int));
            dummyData.Columns.Add("Date", typeof(DateTime));
            dummyData.Columns.Add("CustomerName", typeof(string));
            dummyData.Columns.Add("Total", typeof(decimal));
            dummyData.Columns.Add("PaymentDate", typeof(DateTime));
            dummyData.Columns.Add("Status", typeof(string));

            // Add rows with dummy data
            dummyData.Rows.Add(10234, "~/Admin/Layout/image/DexProfilePic.jpeg", "Product 1", 2, DateTime.Now, "John Doe", 150.00m, DateTime.Now, "Processing");
            dummyData.Rows.Add(10235, "~/Admin/Dashboard/Images/iphone11.jpg", "Product 2", 3, DateTime.Now, "Jane Smith", 200.00m, DateTime.Now, "Shipped");
            // Add more rows as needed for testing

            return dummyData;
        }

        protected void productListView_SelectedIndexChanged( object sender, EventArgs e )
        {

        }
    }
}