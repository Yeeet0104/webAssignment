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
            <div class="tab">
                <button class="tablinks" onclick="openTab(event, 'productDescription')">Product Description</button>
                <button class="tablinks active" onclick="openTab(event, 'customerReview')">Customer Review</button>
            </div>
            <div id="productDescription" class="tabcontent" style="display: block;">
                <h2>Product Description</h2>
                <div class="desc-box-content">
                    <span class="box-header">TUNED FOR LOW NOISE OPERATION</span><br />
                    <span class="box-content">A specially set fan curve ensures that, even at full load, fan noise is kept to a minimum.</span><br /><br />
                    <span class="box-header">ZERO RPM FAN MODE</span><br />
                    <span class="box-content">At low and medium loads the cooling fan switches off entirely for near-silent operation.</span><br /><br />
                </div>
            </div>
            <div id="customerReview" class="tabcontent">
                <div class="desc-box-content">
                    <div class="customer-review-box">
                            <div class="review-container">
                                <div class="profile-width">
                                    <div class="profile-pic">
                                        <img src="user.png">
                                    </div>
                                </div>
                                <div class="rating">
                                    <div class="profile-name">
                                        <p class="review-username">Dexter</p>
                                        <p class="review-time">2021-6-21 09:12pm</p>
                                    </div>
                                    <div class="stars">
                                        <i class="fas fa-star"></i>
                                        <i class="fas fa-star"></i>
                                        <i class="fas fa-star"></i>
                                        <i class="fas fa-star"></i>
                                        <i class="fas fa-star"></i>
                                    </div>
                                    <div class="comments-container my-2">
                                        <p class="comment-text">
                                            As someone who loves to build pc, this power supply is the best psu I've ever owned.
                                            It can easily supply power for any powerful pc and component.
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    <div class="customer-review-box">
                        <div class="review-container">
                            <div class="profile-width">
                                <div class="profile-pic">
                                    <img src="user.png">
                                </div>
                            </div>
                            <div class="rating">
                                <div class="profile-name">
                                    <p class="review-username">Deng Wait</p>
                                    <p class="review-time">2021-6-21 09:12pm</p>
                                </div>
                                <div class="stars">
                                    <i class="fas fa-star"></i>
                                    <i class="fas fa-star"></i>
                                    <i class="fas fa-star"></i>
                                    <i class="fas fa-star"></i>
                                    <i class="fas fa-star"></i>
                                </div>
                                <div class="comments-container my-2">
                                    <p class="comment-text">
                                        I love the power supply. It's a perfect fit for my pc theme and  it's a great price too. I really don't have any complaints about it and would recommend it to anyone looking for a new power supply.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function openTab(evt, tabName) {
            evt.preventDefault(); // Prevent default form submission behavior
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(tabName).style.display = "block";
            evt.currentTarget.className += " active";
        }


    </script>


</asp:Content>
