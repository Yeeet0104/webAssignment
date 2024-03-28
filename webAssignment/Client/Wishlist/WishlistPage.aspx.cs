using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Wishlist
{
    public partial class WishlistPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lvWishlist.DataSource = GetDummyData();
                lvWishlist.DataBind();
            }
        }

        private DataTable GetDummyData()
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("ProductID", typeof(string));
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("ProductName", typeof(string));
            dummyData.Columns.Add("Price", typeof(decimal));
            dummyData.Columns.Add("Quantity", typeof(int));

            // Add rows with dummy data
            dummyData.Rows.Add("P1001", "~/Client/Cart/images/rtx4060ti.png", "Intel i7 11th Gen", 1500.00m, 2);
            dummyData.Rows.Add("P1002", "~/Client/Cart/images/ryzen.jpg", "AMD Ryzen 7 5700X3D", 10.00m, 0);
            dummyData.Rows.Add("P1003", "~/Client/Cart/images/ssd.jpg", "Micron 3400 SSD", 1.00m, 1);
            dummyData.Rows.Add("P1004", "~/Admin/Layout/image/DexProfilePic.jpeg", "Dexter", 100000.00m, 1);
            // Add more rows as needed for testing

            return dummyData;
        }
    }
}