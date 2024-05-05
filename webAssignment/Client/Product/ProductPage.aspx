<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="ProductPage.aspx.cs" Inherits="webAssignment.Client.Product.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="ProductPage.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content pb-10">
        <div class="all-products">
            <div class="product-top">
                <h2 id="productPageHeader" class="text-2xl">ALL PRODUCTS</h2>
            </div>
            <div class="row">
                <div class="product-search-bar">
                    <asp:TextBox ID="productName" runat="server" CssClass="search-input" placeholder="Search Products By Name..." ></asp:TextBox>                        
                    <asp:Button ID="resetButton" runat="server" CssClass="reset-button" Text="&times;" OnClick="ResetButton_Click" />
                    <asp:LinkButton ID="searchButton" runat="server" CssClass="search-button" OnClick="SearchButton_Click">
                        <i class="fa-solid fa-magnifying-glass"></i>
                    </asp:LinkButton>
                </div>
                <div id="filterBtn" runat="server" class="filter-btn" >
                    <i class="fa-light fa-filter"></i>
                    <span>Filter</span>
                </div>
                <div id="myModal" class="modal" runat="server">
                    <!-- Modal content -->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h3 class="text-2xl m-2">Filter</h3>
                            <span class="closeModal">&times;</span>
                        </div>
                        <div class="modal-body">
                            <h4 class="text-xl m-2">Computer Accessories</h4>
                            <div class="categories">
                                <span><asp:LinkButton runat="server" Text="Mouse" OnClick="FilterByCategory" CommandArgument="mouse" /></span>
                                <span><asp:LinkButton runat="server" Text="Keyboard" OnClick="FilterByCategory" CommandArgument="keyboard" /></span>
                                <span><asp:LinkButton runat="server" Text="Others" OnClick="FilterByCategory" CommandArgument="others" /></span>
                            </div>
                            <h4 class="text-xl m-2">Computer Parts</h4>
                            <div class="categories">
                                <span><asp:LinkButton runat="server" Text="Motherboard" OnClick="FilterByCategory" CommandArgument="motherboard" /></span>
                                <span><asp:LinkButton runat="server" Text="Ram" OnClick="FilterByCategory" CommandArgument="ram" /></span>
                                <span><asp:LinkButton runat="server" Text="Storage" OnClick="FilterByCategory" CommandArgument="storage" /></span>
                                <span><asp:LinkButton runat="server" Text="Gpu" OnClick="FilterByCategory" CommandArgument="gpu" /></span>
                                <span><asp:LinkButton runat="server" Text="Psu" OnClick="FilterByCategory" CommandArgument="psu" /></span>
                                <span><asp:LinkButton runat="server" Text="Cpu" OnClick="FilterByCategory" CommandArgument="cpu" /></span>
                                <span><asp:LinkButton runat="server" Text="Case" OnClick="FilterByCategory" CommandArgument="case" /></span>
                            </div>


                            <h4 class="text-xl m-2">Price Range</h4>
                            <div class="price-range">
                                <asp:TextBox ID="minprice" runat="server" type="number" CssClass="minprice" min="20" max="5000" placeholder="MIN"></asp:TextBox>
                                <asp:TextBox ID="maxprice" runat="server" type="number" CssClass="maxprice" min="20" max="5000" placeholder="MAX"></asp:TextBox>
                                <asp:Button ID="applyFilterButton" runat="server" CssClass="apply-price-filter-btn" Text="Apply" OnClick="ApplyPriceFilterButton_Click" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <span>*Click the apply button to apply price filter</span>
                        </div>
                    </div>
                </div>

            </div>
            <div class="sort">
                <span>Sort By</span>
                <asp:LinkButton runat="server" Text="All Products (A-Z)" OnClick="SortAllProducts_Click" CssClass="hvr-float" />
                <asp:LinkButton runat="server" Text="Newest Arrivals" OnClick="SortByArrivals_Click" CssClass="hvr-float" />
                <asp:LinkButton runat="server" Text="Price: Low to High" OnClick="SortByPriceLowToHigh_Click" CssClass="hvr-float" />
                <asp:LinkButton runat="server" Text="Price: High to Low" OnClick="SortByPriceHighToLow_Click" CssClass="hvr-float" />
            </div>


            <asp:ListView ID="ListViewProducts" runat="server" ItemPlaceholderID="container">
                <LayoutTemplate>
                    <div class="product-center" id="container">
                        <asp:PlaceHolder runat="server" ID="container"></asp:PlaceHolder>
                    </div>
                </LayoutTemplate>
                    <ItemTemplate>
                        <a class="product" href='<%# "/Client/ProductDetails/ProductDetailsPage.aspx?ProductId=" + Eval("product_id") %>'>
                            <div id="product" class="hover-content">
                                <div class="product-header">
                                    <img src='<%# "~/Client/Product/Products Images/" + Eval("product_image") + ".png" %>' alt='<%# Eval("product_name") %>' />
                                </div>
                                <div class="product-footer">
                                    <span class="product-name"><%# Eval("product_name") %></span>
                                    <div class="price-and-sold">
                                        <span>
                                            <span class="RM">RM </span>
                                            <span class="product-price">
                                                <%# Eval("min_price", "{0:N2}") %>
                                                <%# (decimal)Eval("min_price") != (decimal)Eval("max_price") ? " - " + Eval("max_price", "{0:N2}") : "" %>
                                            </span>       
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </ItemTemplate>

            </asp:ListView>


            <div id="noProductsFoundMessage" class="no-products-found" runat="server" style="display:none;">
                No products found.
            </div>

        </div>
    </div>

    <script>
        function toggleResetButton() {
            var resetButton = document.getElementById('<%= resetButton.ClientID %>');
            resetButton.style.opacity = document.getElementById('<%= productName.ClientID %>').value.trim() !== '' ? '1' : '0';
        }

        //Filter pop up modal
        var modal = document.getElementById('<%= myModal.ClientID %>');
        var filterBtn = document.getElementById('<%= filterBtn.ClientID %>');
        var span = document.getElementsByClassName("closeModal")[0];

        filterBtn.onclick = function () {
            modal.style.display = "block";
        }

        span.onclick = function () {
            modal.style.display = "none";
        }


        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }

    </script>
</asp:Content>
