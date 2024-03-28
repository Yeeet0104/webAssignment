using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace webAssignment
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {

                OrdersListView.DataSource = GetDummyData();
                OrdersListView.DataBind();

                bestSellingItemLv.DataSource = GetDummyDataBestSeller();
                bestSellingItemLv.DataBind();
                //initChart();
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

        protected void OrdersListView_SelectedIndexChanged( object sender, EventArgs e )
        {

        }

        private DataTable GetDummyDataBestSeller( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("productName", typeof(string));
            dummyData.Columns.Add("productVariant", typeof(string));
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("sold", typeof(int));
            dummyData.Columns.Add("price", typeof(int));
            dummyData.Columns.Add("profit", typeof(int));

            // Add rows with dummy data
            dummyData.Rows.Add("Iphone 30 ", " pro max gao", "~/Admin/Layout/image/DexProfilePic.jpeg", 350 , 10 , (350*10));
            dummyData.Rows.Add("Iphone 30 ", " max", "~/Admin/Layout/image/DexProfilePic.jpeg", 350 , 10 , (350*10));
            dummyData.Rows.Add("Iphone 30 ", " pro max", "~/Admin/Layout/image/DexProfilePic.jpeg", 350 , 10 , (350*10));


            // Add more rows as needed for testing

            return dummyData;
        }
    }
}