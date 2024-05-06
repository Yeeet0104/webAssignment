using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Voucher
{
    public partial class editVoucher : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                string encVou = Request.QueryString["voucherCodeID"];
                if ( string.IsNullOrEmpty(encVou) )
                {
                    Response.Redirect("~/Admin/Product Management/adminProducts.aspx");
                    return;
                }
                string voucherId = DecryptString(encVou);
                lblvoucherCode.Text = voucherId;
                if ( !string.IsNullOrEmpty(voucherId) )
                {
                    LoadVoucherDetails(voucherId);
                }
            }
        }
        private void LoadVoucherDetails( string voucherId )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = "SELECT * FROM Voucher WHERE voucher_id = @voucherId";
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@voucherId", voucherId);
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        if ( reader.Read() )
                        {
                            editVoucherCode.Text = reader["voucher_id"].ToString();
                            discountRateTb.Text = ( (double)reader["discount_rate"] * 100 ).ToString();
                            maxTb.Text = reader["cap_at"].ToString();
                            minTb.Text = reader["min_spend"].ToString();
                            startDateTb.Text = Convert.ToDateTime(reader["started_date"]).ToString("yyyy-MM-dd");
                            expireDateTb.Text = Convert.ToDateTime(reader["expiry_date"]).ToString("yyyy-MM-dd");
                            quantity.Text = reader["quantity"].ToString();
                        }
                    }
                }
            }
        }
        protected void DropDownList1_SelectedIndexChanged( object sender, EventArgs e )
        {

        }

        protected void edtVoucher_Click( object sender, EventArgs e )
        {
            try
            {
                using ( SqlConnection conn = new SqlConnection(connectionString) )
                {
                    conn.Open();
                    string sql = "UPDATE Voucher SET quantity = @qty, discount_rate = @DiscountRate, cap_at = @CapAt,min_spend = @MinSpend, started_date = @StartedDate, expiry_date = @ExpiryDate, voucher_status = @status WHERE voucher_id = @VoucherId";

                    using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                    {
                        cmd.Parameters.AddWithValue("@VoucherId", editVoucherCode.Text);
                        cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(quantity.Text));
                        cmd.Parameters.AddWithValue("@DiscountRate", ( Convert.ToDouble(discountRateTb.Text) / 100 ));
                        cmd.Parameters.AddWithValue("@CapAt", Convert.ToDecimal(maxTb.Text));
                        cmd.Parameters.AddWithValue("@MinSpend", Convert.ToDecimal(minTb.Text));
                        cmd.Parameters.AddWithValue("@StartedDate", Convert.ToDateTime(startDateTb.Text));
                        cmd.Parameters.AddWithValue("@ExpiryDate", Convert.ToDateTime(expireDateTb.Text));
                        cmd.Parameters.AddWithValue("@status", DetermineStatus(Convert.ToDateTime(startDateTb.Text)));

                        cmd.ExecuteNonQuery();
                    }
                }

                ShowNotification("Voucher updated successfully!", "success");
            }
            catch ( SqlException ex )
            {
                ShowNotification("Error updating voucher: " + ex.Message, "error");
            }
        }

        private string DetermineStatus( DateTime startDate )
        {
            return DateTime.Now.Date >= startDate.Date ? "OnGoing" : "Pending";
        }

        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }

        protected string DecryptString( string cipherText )
        {
            string EncryptionKey = "ABC123";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using ( Aes encryptor = Aes.Create() )
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using ( MemoryStream ms = new MemoryStream() )
                {
                    using ( CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write) )
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}