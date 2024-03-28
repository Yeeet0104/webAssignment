using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Profile
{
    public partial class OrderHistoryPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lvOrder.DataSource = GetDummyData();
                lvOrder.DataBind();
            }
        }

        private DataTable GetDummyData()
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("OrderID", typeof(string));
            dummyData.Columns.Add("Status", typeof(string));
            dummyData.Columns.Add("Date", typeof(DateTime));
            dummyData.Columns.Add("Total", typeof(decimal));

            // Add rows with dummy data
            dummyData.Rows.Add("O1001", "Pending", DateTime.Now.AddDays(-3), 50.00m);
            dummyData.Rows.Add("O1002", "Shipped", DateTime.Now.AddDays(-2), 75.50m);
            dummyData.Rows.Add("O1001", "Cancelled", DateTime.Now, 100.00m);
            dummyData.Rows.Add("O1002", "Shipped", DateTime.Now.AddDays(-2), 75.50m);

            // Add more rows as needed for testing

            return dummyData;
        }
    }
}