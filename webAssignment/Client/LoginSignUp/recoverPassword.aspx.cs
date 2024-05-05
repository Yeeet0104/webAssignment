using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using MimeKit;
using Irony;

namespace webAssignment.Client.LoginSignUp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool CheckEmail(string email)
        {
            string query = "SELECT COUNT(*) FROM [User] WHERE email = @email";
            bool emailValid = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    emailValid = count > 0;

                    if (emailValid)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ValidateForm()
        {
            bool isEmailValid = !string.IsNullOrEmpty(txtEmail.Text);
            txtEmail.Style["border-color"] = isEmailValid ? string.Empty : "#EF4444";

            return isEmailValid;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (CheckEmail(txtEmail.Text))
                {
                    string receiverEmail = txtEmail.Text;
                    string query = "SELECT user_ID FROM [User] WHERE email = @email";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                        {
                            sqlCommand.Parameters.AddWithValue("@email", receiverEmail);
                            conn.Open();
                            string result = sqlCommand.ExecuteScalar() as string;
                            if (result != null)
                            {
                                string custId = result;

                                try
                                {
                                    string senderEmail = "gtechpc24@gmail.com";
                                    MailMessage verificationMail = new MailMessage(senderEmail, receiverEmail);
                                    string resetEmailParam = HttpUtility.UrlEncode(receiverEmail); // Encode email for URL
                                    string resetUrl = $"https://localhost:44356/Client/LoginSignUp/resetPassword.aspx?email={resetEmailParam}";
                                    verificationMail.Subject = "G-Tech Account Password Reset";
                                    verificationMail.Body = "<h3>Click the following link to reset your password:</h3><br><br>" +
                                   $"<a href=\"{resetUrl}\" style=\"color:black;border:1px solid yellow;background-color:yellow;padding:15px 10px;\">Reset Password</a>";
                                    verificationMail.IsBodyHtml = true;

                                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                                    smtpClient.EnableSsl = true;
                                    smtpClient.UseDefaultCredentials = false;
                                    smtpClient.Credentials = new NetworkCredential(senderEmail, "lajd btuc nhuf qryg");

                                    smtpClient.Send(verificationMail);
                                    lblLoginMessage.Text = "Email sent successfully";
                                }
                                catch (Exception ex)
                                {
                                    lblLoginMessage.Text = "Email failed to send: " + ex.Message;
                                }
                            }
                        }

                    }
                }
                else
                {
                    lblLoginMessage.Text = "This is not a valid email";
                }

            }
            else
            {
                lblLoginMessage.Text = "Please enter email!";
            }

        }
    }
}