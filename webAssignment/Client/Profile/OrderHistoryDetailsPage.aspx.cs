using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Profile
{
    public partial class OrderHistoryDetailsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                lvOrderList.DataSource = GetDummyData();
                lvOrderList.DataBind();
            }
        }

        private DataTable GetDummyData()
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("ProductName", typeof(string));
            dummyData.Columns.Add("Price", typeof(decimal));
            dummyData.Columns.Add("Quantity", typeof(int));
            dummyData.Columns.Add("Subtotal", typeof(decimal));

            // Add rows with dummy data
            dummyData.Rows.Add("~/Client/Cart/images/i7.png", "Iphone 11", 1500.00m, 2, 3000.00m);
            dummyData.Rows.Add("~/Admin/Layout/image/DexProfilePic.jpeg", "DTX 4090", 10.00m, 1, 10.00m);
            dummyData.Rows.Add("~/Client/Cart/images/ssd.jpg", "Iphone 11", 1500.00m, 2, 3000.00m);
            dummyData.Rows.Add("~/Client/Cart/images/cryingKermit.png", "Kermit", 10.00m, 1, 10.00m);
            // Add more rows as needed for testing

            return dummyData;
        }
    }
}