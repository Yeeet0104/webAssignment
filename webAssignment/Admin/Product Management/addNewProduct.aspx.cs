using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        protected void UploadButton_Click( object sender, EventArgs e )
        {

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
    }
}