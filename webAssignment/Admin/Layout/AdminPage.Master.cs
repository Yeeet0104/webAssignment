using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            assignActiveClass();
            loadProfile();
            var visiblePages = new List<string> { "adminProducts.aspx", "Category.aspx", "voucher.aspx" };

            string currentPage = Path.GetFileName(Request.FilePath);
            Debug.WriteLine("currentPagecurrentPage" + currentPage);
            Debug.WriteLine("currentPagecurrentPage" + visiblePages.Contains(currentPage, StringComparer.OrdinalIgnoreCase));
            if ( visiblePages.Contains(currentPage, StringComparer.OrdinalIgnoreCase) )
            {
                filterDatePopUp.Visible = false;
            }
            else
            {
                filterDatePopUp.Visible = true;
            }
            // Set the visibility of the button based on whether the current page is in the list
            if (!IsPostBack)
            {
                if ( Session["StartDate"] != null && Session["EndDate"] != null )
                {
                    
                    lblDate.Text = ((DateTime)Session["StartDate"]).ToString("dd/MMM/yyyy") + " - " + ( (DateTime)Session["EndDate"] ).ToString("dd/MMM/yyyy");
                }
                else
                {
                    lblDate.Text = "Today";
                }
            }
        }

        private void assignActiveClass()
        {
            string currentPage = Path.GetFileName(Request.Path).ToLower();
            // Append 'active' class based on current page
            switch (currentPage)
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

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = SearchTextBox.Text;
            var currentContent = this.Page as IFilterable;

            if (currentContent != null)
            {
                currentContent.FilterListView(searchTerm);
            }
        }

        protected void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = SearchTextBox.Text;
            var currentContent = this.Page as IFilterable;

            if (currentContent != null)
            {
                currentContent.FilterListView(searchTerm);
            }
            else
            {
                ShowNotification("Search Error", "warning");
            }
        }

        protected void filterDatePopUp_Click( object sender, EventArgs e )
        {
            pnlDateFilter.Style.Add("display", "flex");
        }        
        protected void allTimeDate_Click( object sender, EventArgs e )
        {
            lblDate.Text = "All Time";

            DateTime startDate = new DateTime(1800, 1, 1);  
            DateTime endDate = new DateTime(2100, 12, 31);  

            // Store in session
            Session["StartDate"] = startDate;
            Session["EndDate"] = endDate;
            pnlDateFilter.Style.Add("display", "none");
            Response.Redirect(Request.RawUrl);
        }
        protected void cancelDate_click( object sender, EventArgs e )
        {
            pnlDateFilter.Style.Add("display", "none");
        }

        protected void btnApplyDateFilter_Click( object sender, EventArgs e )
        {
            DateTime startDate;
            DateTime endDate;

            if ( !DateTime.TryParse(txtStartDate.Text, out startDate) )
            {
                ShowNotification("Missing Inputs", "warning");
                txtStartDate.CssClass += " border-red-800 border-2";
                return;
            }

            if ( !DateTime.TryParse(txtEndDate.Text, out endDate) )
            {
                ShowNotification("Missing Inputs", "warning");
                txtEndDate.CssClass += " border-red-800 border-2";
                return;
            }

            if ( startDate > endDate )
            {
                ShowNotification("Start Date cannot more than end date", "warning");
                return;
            }
            if ( startDate != null && endDate != null )
            {
                Session["StartDate"] = startDate;
                Session["EndDate"] = endDate;

                lblDate.Text = startDate.ToString("dd/MM/yyyy") + " - " + endDate.ToString("dd/MM/yyyy");
                Response.Redirect(Request.RawUrl);
                pnlDateFilter.Style.Add("display", "none");
            }
            else
            {
                txtEndDate.CssClass += "border-red-800";
            }
        }

        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";

            // Using ScriptManager to register the startup script
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSnackbar", script, true);
        }

        private void loadProfile()
        {
            string userId = Request.Cookies["userInfo"]["userID"];

            string query = "SELECT * FROM [User] WHERE user_id = @UserId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string name = reader["first_name"].ToString() + " " +  reader["last_name"].ToString();
                        userName.Text = name;
                        role.Text = reader["role"].ToString();
                        if (reader["profile_pic_path"] != DBNull.Value)
                        {
                            string profilePicPath = reader["profile_pic_path"].ToString();

                            userProfilePic.ImageUrl = profilePicPath + "?timestamp=" + DateTime.Now.Ticks;
                        }
                    }
                }
            }
        }


    }
}