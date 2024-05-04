using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Home
{
    public partial class HomePage : System.Web.UI.Page
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductData();
            }
        }

        private DataTable GetProductData()
        {
            DataTable productData = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = @"SELECT TOP 6 p.product_id, p.product_name, p.product_description,
                              MIN(pv.variant_price) AS min_price,
                              MAX(pv.variant_price) AS max_price,
                              (
                                  SELECT TOP 1 path
                                  FROM Image_Path ip
                                  WHERE ip.product_id = p.product_id
                                  ORDER BY image_path_id
                              ) AS product_image
                       FROM Product p
                       LEFT JOIN Product_Variant pv ON p.product_id = pv.product_id
                       WHERE p.product_status = 'Publish'
                       GROUP BY p.product_id, p.product_name, p.product_description";


            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            productData.Load(reader);
            reader.Close();
            conn.Close();
            return productData;
        }


        private void BindProductData()
        {
            DataTable productData = GetProductData();
            HomeListViewProducts.DataSource = productData;
            HomeListViewProducts.DataBind();
        }

        protected void btnExplore_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Client/Product/ProductPage.aspx");
        }

        protected void btnViewDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Client/ProductDetails/ProductDetailsPage.aspx?ProductId=P1007");
        }


    }
}