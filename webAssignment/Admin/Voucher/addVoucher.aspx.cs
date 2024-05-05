using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Voucher
{
    public partial class addVoucher : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load( object sender, EventArgs e )
        {

        }

        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }

        protected void btnAddNewVoucher_Click1( object sender, EventArgs e )
        {
            // Retrieve values from TextBoxes
            string voucherCode = editVoucherCode.Text.Trim();
            string discountRateText = discountRateTb.Text.Trim();
            string maxDiscountText = maxTb.Text.Trim();
            string minSpendText = minTb.Text.Trim();
            string startDate = startDateTb.Text.Trim();
            string expiryDate = expireDateTb.Text.Trim();
            string qty = quantity.Text.Trim();

            // Validation
            if ( string.IsNullOrEmpty(voucherCode) || string.IsNullOrEmpty(discountRateText) || string.IsNullOrEmpty(maxDiscountText) ||
                string.IsNullOrEmpty(minSpendText) || string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(expiryDate) || string.IsNullOrEmpty(qty) )
            {
                ShowNotification("All fields are required.", "warning");
                return;
            }

            if ( !decimal.TryParse(discountRateText, out decimal discountRate) || discountRate < 0 || discountRate > 100 )
            {
                ShowNotification("Invalid discount rate. Enter a value between 0 and 100.", "error");
                return;
            }

            if ( !decimal.TryParse(maxDiscountText, out decimal maxDiscount) || maxDiscount < 0 )
            {
                ShowNotification("Invalid maximum discount amount.", "error");
                return;
            }

            if ( !decimal.TryParse(minSpendText, out decimal minSpend) || minSpend < 0 )
            {
                ShowNotification("Invalid minimum spend amount.", "error");
                return;
            }
            if ( !int.TryParse(qty, out int qtyCheck) || qtyCheck < 0 )
            {
                ShowNotification("Invalid Quantity Inputed.", "error");
                return;
            }

            DateTime validStartDate;
            DateTime validExpiryDate;
            if ( !DateTime.TryParse(startDate, out validStartDate) || !DateTime.TryParse(expiryDate, out validExpiryDate) )
            {
                ShowNotification("Invalid dates. Please enter valid dates.", "error");
                return;
            }

            if ( validStartDate > validExpiryDate )
            {
                ShowNotification("Expiry date must be after the start date.", "error");
                return;
            }

            try
            {
                using ( SqlConnection conn = new SqlConnection(connectionString) )
                {
                    conn.Open();
                    string sql = "INSERT INTO Voucher (voucher_id,quantity, discount_rate, cap_at, min_spend, added_date, started_date, expiry_date ,voucher_status) " +
                                 "VALUES (@VoucherId,@qty, @DiscountRate, @CapAt, @MinSpend, @AddedDate, @StartedDate, @ExpiryDate ,@status)";

                    using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                    {
                        cmd.Parameters.AddWithValue("@VoucherId", voucherCode);
                        cmd.Parameters.AddWithValue("@qty", qty);

                        cmd.Parameters.AddWithValue("@DiscountRate", (discountRate/100));
                        cmd.Parameters.AddWithValue("@CapAt", maxDiscount);
                        cmd.Parameters.AddWithValue("@MinSpend", minSpend);
                        cmd.Parameters.AddWithValue("@AddedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@StartedDate", validStartDate);
                        cmd.Parameters.AddWithValue("@ExpiryDate", validExpiryDate);
                        string status = DateTime.Now.Date == validStartDate.Date ? "OnGoing" : "Pending";
                        cmd.Parameters.AddWithValue("@status", status);
                        Debug.Write("Babak what do you mean" + status);
                        cmd.ExecuteNonQuery();
                    }
                    Debug.Write("Babak" , sql);
                }

                ShowNotification("Voucher added successfully!", "success");
            }
            catch ( SqlException ex )
            {
                // Check if the exception is a duplicate key exception
                if ( ex.Number == 2627 || ex.Number == 2601 ) // 2601 is also a unique constraint (including primary key) violation
                {
                    ShowNotification("ERROR: Duplicate voucher code Please type another one.", "warning");
                }
                else
                {
                    ShowNotification("Error adding voucher: " + ex.Message, "warning");
                }
            }
            catch ( Exception ex )
            {

                ShowNotification("Error adding voucher: " + ex.Message, "warning");
            }
        }
    }
}