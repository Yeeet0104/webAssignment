<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="ProductDetailsPage.aspx.cs" Inherits="webAssignment.Client.ProductDetails.ProductDetailsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="ProductDetailsPage.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content">
<%--        <div class="header">
            <h2>Product Details</h2>
        </div>--%>
        <div class="product-details">
            <div class="product-small-image">
                <img src="/Client/Product/Products Images/Corsair RMx Series RM850x.png" alt="Corsair RMx Series RM850x" />
                <img src="/Client/Product/Products Images/Corsair RMx Series RM850x(2).jpg" alt="Corsair RMx Series RM850x" />
                <img src="/Client/Product/Products Images/Corsair RMx Series RM850x(3).jpg" alt="Corsair RMx Series RM850x" />
            </div>
            <div class="product-image">
                <asp:Image ID="imgProduct" runat="server" CssClass="product-image" />
            </div>
            <div class="product-info">
                <h3><asp:Label ID="lblProductName" runat="server" CssClass="product-name" Text="Product Name" /></h3>
                <span class="my-2.5"><asp:Label ID="lblProductPrice" runat="server" CssClass="product-price" Text="Product Price" /></span>
                <span class="my-2 w-[88%]"><asp:Label ID="lblShortProductDesc" runat="server" CssClass="product-desc" Text="Short Product Description" /></span>
            
                <div class="product-quantity">
                    <h5 class="mr-4">Quantity: </h5>
                    <div class="qty-up-down">
                        <asp:Button ID="stepdown" runat="server" CssClass="qty-button" Text="-" OnClick="StepDown_Click" />
                        <asp:TextBox ID="qtyInput" runat="server" CssClass="qty-input" Text="1" ReadOnly="true" />
                        <asp:Button ID="stepup" runat="server" CssClass="qty-button" Text="+" OnClick="StepUp_Click" />
                    </div>
                </div>
                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="add-to-cart-btn" OnClick="btnAddToCart_Click" />
                <asp:Button ID="btnAddToWishlist" runat="server" Text="Add to Wishlist" CssClass="add-to-wishlist-btn" OnClick="btnAddToWishlist_Click" />
            </div>
        </div>
    </div>
    <div class="content">
        <div class="desc-box">
            <h2>Product Description</h2>
            <%--<span class="my-2.5"><asp:Label ID="lblLongDesc" runat="server" CssClass="long-desc" Text="Long desc" /></span>--%>
            <div class="desc-box-content">
                <span class="box-header">TUNED FOR LOW NOISE OPERATION</span><br />
                <span class="box-content">A specially set fan curve ensures that, even at full load, fan noise is kept to a minimum.</span><br /><br />
                <span class="box-header">ZERO RPM FAN MODE</span><br />
                <span class="box-content">At low and medium loads the cooling fan switches off entirely for near-silent operation.</span><br /><br />
            </div>

        </div>
    </div>

</asp:Content>
