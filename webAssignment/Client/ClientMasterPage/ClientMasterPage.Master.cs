using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.ClientMasterPage
{
    public partial class ClientMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendRequest_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (IsValidEmail(txtFeedbackEmail.Text))
                {
                    string receiverEmail = txtFeedbackEmail.Text;
                    string username = txtFeedbackUsername.Text;

                    try
                    {
                        string senderEmail = "gtechpc24@gmail.com";
                        MailMessage verificationMail = new MailMessage(senderEmail, receiverEmail);
                        verificationMail.Subject = "Request a call from our team";

                        verificationMail.Body = $"<h3>Hi {username},</h3>" +
                                                "<p>We have received your request for a call with our team to seek personalised support.</p>" +
                                                "<p>Please wait until we get back to you within 3 business days.</p>" +
                                                "<p>Thank you for contacting us, G-Tech Team.</p>";
                        verificationMail.IsBodyHtml = true;
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(senderEmail, "lajd btuc nhuf qryg");

                        smtpClient.Send(verificationMail);
                        lblMsg.Text = "Request Sent!";
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = "Email failed to send: " + ex.Message;
                    }
                }
                else
                {
                    lblMsg.Text = "Invalid email format!";
                }
            }
            else
            {
                lblMsg.Text = "Input fields cannot be empty!";
            }
        }
        public static bool IsValidEmail(string email)
        {
            // Define a regular expression pattern for validating email addresses
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Use the IsMatch method to see if the email matches the pattern
            return regex.IsMatch(email);
        }
        private bool ValidateForm()
        {
            bool isEmailValid = !string.IsNullOrEmpty(txtFeedbackEmail.Text);
            txtFeedbackEmail.Style["border-color"] = isEmailValid ? string.Empty : "#EF4444";
            bool isUsernameValid = !string.IsNullOrEmpty(txtFeedbackUsername.Text);
            txtFeedbackUsername.Style["border-color"] = isUsernameValid ? string.Empty : "#EF4444";
            return isEmailValid && isUsernameValid;
        }
    }
}