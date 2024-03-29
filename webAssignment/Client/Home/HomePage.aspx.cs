using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Home
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dummyData = GetDummyData();
            HomeListViewProducts.DataSource = dummyData;
            HomeListViewProducts.DataBind();
        }

        private DataTable GetDummyData()
        {
            DataTable dummyData = new DataTable();

            // Add columns to match ListView's ItemTemplate
            dummyData.Columns.Add("ProductName", typeof(string));
            dummyData.Columns.Add("Price", typeof(decimal));
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("Link", typeof(string));

            // Add rows with dummy data
            dummyData.Rows.Add("CORSAIR ONE i160 Compact Gaming PC", 13999.00m, "CORSAIR ONE i160 Compact Gaming PC.png", "Others/PC-Cosair.html");
            dummyData.Rows.Add("Logitech G PRO X Gaming Keyboard", 649.00m, "Logitech G PRO X Gaming Keyboard.png", "Others/keyboard-proX.html");
            dummyData.Rows.Add("G502 Hero High Performance Gaming Mouse", 399.00m, "G502 Hero High Performance Gaming Mouse.png", "Others/Mouse-G502.html");
            dummyData.Rows.Add("G560 Lightsync PC Gaming Speakers", 329.00m, "G560 Lightsync PC Gaming Speakers.png", "Others/Speaker-G560.html");
            dummyData.Rows.Add("NZXT H710i PC case", 789.99m, "NZXT H710i PC case.png", "CASE/Case-NZXT.html");
            dummyData.Rows.Add("Kingston A400 SSD (480GB)", 225.00m, "Kingston A400 SSD(480gb).png", "Storage/SSD-Kingston.html");
            dummyData.Rows.Add("MSI MPG Z590 ATX", 1299.00m, "MSI MPG Z590 ATX.png", "MotherBoard/MTB-msi.html");
            dummyData.Rows.Add("NVIDIA GTX 1080 Ti", 2899.00m, "NVIDIA GTX 1080 Ti.png", "GPU/GPU-gtx1080Ti.html");

            return dummyData;
        }

    }
}