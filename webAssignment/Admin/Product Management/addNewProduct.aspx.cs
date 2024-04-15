using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Product_Management
{
    public partial class addNewProduct : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {

            if ( IsPostBack )
            {
                // the whole section here is to ensure that the text box is retain else it will disapear after refresh due to posback
                int Variantcount = ViewState["VariantCount"] != null ? (int)ViewState["VariantCount"] : 1;
                System.Diagnostics.Debug.WriteLine("huh: " + Variantcount);
                for ( int i = 1 ; i < (Variantcount + 1) ; i++ )
                {
                    Literal divStart = new Literal { Text = "<div class='flex gap-4 items-center flex-wrap justify-evenly'>" };
                    Literal divEnd = new Literal { Text = "</div>" };

                    //creating new asp textbox
                    TextBox textBox = new TextBox();
                    textBox.ID = "variant" + ( i + 1 ) + "Tb";
                    textBox.CssClass = "newVariation_input";
                    textBox.Attributes["Placeholder"] = "Variant " + ( i + 1 );

                    //creating new asp textbox
                    TextBox newPrice = new TextBox();
                    newPrice.ID = "priceVar" + ( i + 1 ) + "Tb";
                    newPrice.CssClass = "newVariation_input";
                    newPrice.Attributes["placeholder"] = "Price for Variant " + ( i + 1 );

                    // adding into the panel ( using updatepanel because of avoiding anoying refresh
                    panelVariantTextBoxes.Controls.Add(divStart);
                    panelVariantTextBoxes.Controls.Add(textBox);
                    panelVariantTextBoxes.Controls.Add(newPrice);
                    panelVariantTextBoxes.Controls.Add(divEnd);


                }
            }
        }

        protected void ddlnewProdStatus_SelectedIndexChanged( object sender, EventArgs e )
        {
            lblnewProdStatus.Text = ddlnewProdStatus.SelectedValue;
        }

        protected void createTextRowBtn_Click( object sender, EventArgs e )
        {
            int Variantcount = ViewState["VariantCount"] != null ? (int)ViewState["VariantCount"] : 1;
            System.Diagnostics.Debug.WriteLine("1: " + Variantcount);
            System.Diagnostics.Debug.WriteLine("2: " + ( Variantcount + 1 ));
            // Create the container div for the new row
            Literal divStart = new Literal { Text = "<div class='flex gap-4 items-center flex-wrap justify-evenly>" };
            int newId = Variantcount + 1;
            // Create the Variant TextBox
            TextBox newVariant = new TextBox();
            newVariant.ID = "variant" + newId + "Tb";

            newVariant.Attributes["Placeholder"] = "Variant " + newId;
            newVariant.CssClass = "newVariation_input";

            // Create the Price TextBox
            TextBox newPrice = new TextBox();
            newPrice.ID = "priceVar" + newId + "Tb"; // Notice that we use Variantcount for both to keep them paired
            newPrice.Attributes["placeholder"] = "Price for Variant " + newId;
            newPrice.CssClass = "newVariation_input";

            // Create the container div for the new row
            Literal divEnd = new Literal { Text = "</div>" };

            // Add the opening div tag, the variant TextBox, the price TextBox, and the closing div tag to the panel
            panelVariantTextBoxes.Controls.Add(divStart);
            panelVariantTextBoxes.Controls.Add(newVariant);
            panelVariantTextBoxes.Controls.Add(newPrice);
            panelVariantTextBoxes.Controls.Add(divEnd);

            ViewState["VariantCount"] = (Variantcount + 1);

            // For debugging (visible in output window during debugging)
            System.Diagnostics.Debug.WriteLine("Variant TextBox created with ID: " + newVariant.ID);
            System.Diagnostics.Debug.WriteLine("Price TextBox created with ID: " + newPrice.ID);
        }

        protected void UploadButton_Click( object sender, EventArgs e )
        {

            String prodName = newProductName.Text.ToString();
            String prodDec = newProductDes.Text.ToString();
            String category = ddlCategory.SelectedValue.ToString();
            String status = ddlnewProdStatus.SelectedValue.ToString();
            int initStock = int.Parse(tbQuantity.Text.ToString());

            int variantCount = ViewState["VariantCount"] != null ? (int)ViewState["VariantCount"] : 0;
            for ( int i = 1 ; i <= variantCount ; i++ )
            {
                TextBox textBoxVariant = (TextBox)panelVariantTextBoxes.FindControl("variant" + i + "Tb");
                TextBox textBoxPrice = (TextBox)panelVariantTextBoxes.FindControl("priceVar" + i + "Tb");

                if ( textBoxVariant != null && !string.IsNullOrWhiteSpace(textBoxVariant.Text) &&
                    textBoxPrice != null && !string.IsNullOrWhiteSpace(textBoxPrice.Text) )
                {
                    // Logic to process the values
                    string variant = textBoxVariant.Text;
                    string price = textBoxPrice.Text;

                    // Now you can use variant and price variables as needed
                }
            }

            if ( fileImages.HasFiles )
            {
                foreach ( HttpPostedFile postedFile in fileImages.PostedFiles )
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileSavePath = Server.MapPath("~/ProductImage/") + fileName;
                    try
                    {
                        // Save the file.
                        postedFile.SaveAs(fileSavePath);
                        Debug.WriteLine("Saving file to: " + fileSavePath);
                        // You can add logic here to add the file details to your database if needed.
                    }
                    catch ( Exception ex )
                    {
                        Debug.WriteLine("Error: " + ex.Message);
                        // Handle the error
                    }
                }
                // After handling all files, you can redirect or update the page as needed.
            }
        }

        public List<string> GetImagePaths( )
        {
            // List to hold the image paths
            List<string> imagePaths = new List<string>();

            // The path to the ProductImage directory
            string folderPath = HttpContext.Current.Server.MapPath("~/ProductImage/");

            // Get all files in the directory
            string[] files = Directory.GetFiles(folderPath);

            // Add paths to the list, converting them to a relative path to be used on the client side
            foreach ( string file in files )
            {
                if ( IsImage(file) )
                {
                    string relativePath = "ProductImage/" + Path.GetFileName(file);
                    imagePaths.Add(relativePath);
                }
            }

            return imagePaths;
        }

        // Helper method to check if a file is an image by extension
        private bool IsImage( string file )
        {
            string extension = Path.GetExtension(file).ToLowerInvariant();
            return extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".gif";
        }

        protected void btnExport_Click( object sender, EventArgs e )
        {
            List<String> files = GetImagePaths();

            foreach( String file in files) { 
               Console.WriteLine(file + "\n");
            
            }


        }

        private void initCategory( )
        {


        }
    }
}