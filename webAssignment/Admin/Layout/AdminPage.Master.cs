﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            assignActiveClass();





        }

        private void assignActiveClass( )
        {
            string currentPage = Path.GetFileName(Request.Path).ToLower();
            string pageTitleStr = "";
            // Append 'active' class based on current page
            switch ( currentPage )
            {
                case "customermanagement.aspx":
                    customerLk.Attributes["class"] += " activeNavItem";
                    pageTitleStr = "Customers";
                    break;
                case "dashboard.aspx":
                    dashboardLk.Attributes["class"] += " activeNavItem";
                    pageTitleStr = "Dashboard";
                    break;
                case "addnewproduct.aspx":
                case "adminproducts.aspx":
                    productLk.Attributes["class"] += " activeNavItem";
                    if (currentPage == "addnewproduct.aspx" ) {
                        pageTitleStr = "Add Product";
                    }
                    else
                    {
                        pageTitleStr = "Product";
                    }

                    break;
                case "createcategory.aspx":
                case "category.aspx":
                    categoryLk.Attributes["class"] += " activeNavItem";
                    if ( currentPage == "createcategory.aspx" )
                    {
                        pageTitleStr = "Create Category";
                    }
                    else
                    {
                        pageTitleStr = "Category";
                    }
                    break;
                case "order.aspx":
                    orderLk.Attributes["class"] += " activeNavItem";
                    pageTitleStr = "Orders";
                    break;
            }
        }

        protected void SearchButton_Click( object sender, EventArgs e )
        {
            string searchTerm = SearchTextBox.Text;
            var currentContent = this.Page as IFilterable;

            if ( currentContent != null )
            {
                currentContent.FilterListView(searchTerm);
            }
            else
            {
                // Handle the case where the content page does not implement IFilterable
            }
        }

        protected void SearchTextBox_TextChanged( object sender, EventArgs e )
        {
            string searchTerm = SearchTextBox.Text;
            var currentContent = this.Page as IFilterable;

            if ( currentContent != null )
            {
                currentContent.FilterListView(searchTerm);
            }
            else
            {
                // Handle the case where the content page does not implement IFilterable
            }
        }
    }



}