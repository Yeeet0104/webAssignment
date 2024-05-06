using AjaxControlToolkit;
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
using webAssignment.Client.Checkout;

namespace webAssignment.Client.Profile
{
    public partial class ReviewPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string encOrderID = Request.QueryString["OrderID"];
            
            string encVariantID = Request.QueryString["VariantID"];
            string orderID = DecryptString(encOrderID);
            string variantID = DecryptString(encVariantID);

            // to be deleted
            //string orderID = "O1001";
            //string variantID = "PV1001";

            retrieveOrderDetails(orderID, variantID);
        }




        private void retrieveOrderDetails(string orderID, string variantID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query =
                @"SELECT p.product_name AS productName, pv.variant_name AS variantName, (Select TOP 1 path FROM Image_Path imgp WHERE imgp.product_id = p.product_id ORDER BY imgp.image_path_id ASC) AS imagePath
                FROM Order_Details od 
                INNER JOIN dbo.[Order] o ON od.order_id = o.order_id 
                INNER JOIN Product_Variant pv ON od.product_variant_id = pv.product_variant_id 
                INNER JOIN Product p ON pv.product_id = p.product_id 
                WHERE o.order_id = @orderID AND pv.product_variant_id = @pvID";

            String userId = getCurrentUserId();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@orderID", orderID);
                        command.Parameters.AddWithValue("@pvID", variantID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                lbName.Text = Convert.ToString(reader[0]) + " " + Convert.ToString(reader[1]);
                                imgProd.ImageUrl = Convert.ToString(reader[2]);
                            }
                        }

                    }

                }
                catch (SqlException ex)
                {
                    // Handle database errors gracefully (e.g., log the error, display a user-friendly message)
                    LogError(ex.Message);
                    ShowNotification(ex.Message, "warning"); // Redirect to an error page if needed
                }
            }
        }


        protected string DecryptString(string cipherText)
        {
            string EncryptionKey = "ABC123";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        private String getCurrentUserId()
        {
            if (Session["UserId"] != null)
            {
                return Convert.ToString(Session["UserId"]);
            }
            else
            {
                Response.Redirect("~/Client/LoginSignUo/Login.aspx");

            }
            return "";
        }


        private void LogError(string message)
        {
            Console.WriteLine("Error: " + message);
        }

        protected void ShowNotification(string message, string type)
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }

        protected void btnSubmitReview_Click(object sender, EventArgs e)
        {


            string encOrderID = Request.QueryString["OrderID"];
            string encVariantID = Request.QueryString["VariantID"];
            string orderID = DecryptString(encOrderID);
            string variantID = DecryptString(encVariantID);
            string newReview = getNextReviewID();
            //string orderID = "O1001";
            //string variantID = "PV1001";
            string userID = getCurrentUserId();
            string date = DateTime.Now.ToString();
            string comment = txtComment.Text;
            string strRating = rateValue.Text;
            int rating = Convert.ToInt32(strRating);


            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string getLatestNum = "INSERT INTO Review VALUES (@reviewID, @orderID, @userID, @pvID, @date, @comment, @like, @dislike, @rating)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(getLatestNum, connection))
                    {
                        command.Parameters.AddWithValue("@reviewID", newReview);
                        command.Parameters.AddWithValue("@orderID", orderID);
                        command.Parameters.AddWithValue("@userID", userID);
                        command.Parameters.AddWithValue("@pvID", variantID);
                        command.Parameters.AddWithValue("@date", date);
                        command.Parameters.AddWithValue("@comment", comment);
                        command.Parameters.AddWithValue("@like", 0);
                        command.Parameters.AddWithValue("@dislike", 0);
                        command.Parameters.AddWithValue("@rating", rating);

                        command.ExecuteNonQuery();

                        Response.Redirect($"~/Client/Profile/OrderHistoryDetailsPage.aspx?OrderID={encOrderID}");
                    }

                }
                catch (SqlException ex)
                {
                    // Handle database errors gracefully (e.g., log the error, display a user-friendly message)
                    LogError(ex.Message);
                    ShowNotification(ex.Message, "warning"); // Redirect to an error page if needed
                }
            }

        }

        private string getNextReviewID()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string getLatestNum = "SELECT MAX(review_id) FROM dbo.[Review];";
            string newReviewID = "REV1001";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(getLatestNum, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            int currentRevID = Convert.ToInt32(result.ToString().Substring(3));
                            int nextRevID = currentRevID + 1;
                            newReviewID = "REV" + nextRevID;
                        }
                    }

                }
                catch (SqlException ex)
                {
                    // Handle database errors gracefully (e.g., log the error, display a user-friendly message)
                    LogError(ex.Message);
                    ShowNotification(ex.Message, "warning"); // Redirect to an error page if needed
                }
            }
            return newReviewID;
        }

    }
}