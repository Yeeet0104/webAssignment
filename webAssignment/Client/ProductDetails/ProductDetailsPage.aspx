﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="ProductDetailsPage.aspx.cs" Inherits="webAssignment.Client.ProductDetails.ProductDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="ProductDetailsPage.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content">
        <%--        <div class="header">
            <h2>Product Details</h2>
        </div>--%>
        <div class="product-details ">
            <div class="product-small-image gap-4">
                <img src="/Client/Product/Products Images/Corsair RMx Series RM850x.png" alt="Corsair RMx Series RM850x" />
                <img src="/Client/Product/Products Images/Corsair RMx Series RM850x(2).jpg" alt="Corsair RMx Series RM850x" />
                <img src="/Client/Product/Products Images/Corsair RMx Series RM850x(3).jpg" alt="Corsair RMx Series RM850x" />
            </div>
            <div class="product-image">
                <asp:Image ID="imgProduct" runat="server" CssClass="product-image" />
            </div>
            <div class="product-info">
                <h3>
                    <asp:Label ID="lblProductName" runat="server" CssClass="product-name" Text="Product Name" /></h3>
                <span class="my-2.5">
                    <asp:Label ID="lblProductPrice" runat="server" CssClass="product-price" Text="Product Price" /></span>
                <span class="my-2 w-[88%]">
                    <asp:Label ID="lblShortProductDesc" runat="server" CssClass="product-desc" Text="Short Product Description" /></span>

                <div class="product-quantity">
                    <h5 class="mr-4">Quantity: </h5>
                    <div class="qty-up-down">
                        <asp:Button ID="stepdown" runat="server" CssClass="qty-button" Text="-" OnClick="StepDown_Click" />
                        <asp:TextBox ID="qtyInput" runat="server" CssClass="qty-input" Text="1" ReadOnly="true" />
                        <asp:Button ID="stepup" runat="server" CssClass="qty-button" Text="+" OnClick="StepUp_Click" />
                    </div>
                </div>
                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="add-to-cart-btn bg-blue-700 hover:bg-blue-900" OnClick="btnAddToCart_Click" />
                <asp:Button ID="btnAddToWishlist" runat="server" Text="Add to Wishlist" CssClass="add-to-wishlist-btn" OnClick="btnAddToWishlist_Click" />
            </div>
        </div>
    </div>

    <div class="content drop-shadow-lg">
        <div class="desc-box">
            <div class="tab">
                <button class="tablinks" onclick="openTab(event, 'productDescription')">Product Description</button>
                <button class="tablinks active" onclick="openTab(event, 'customerReview')">Customer Review</button>
            </div>
            <div id="productDescription" class="tabcontent" style="display: block;">
                <h2>Product Description</h2>
                <div class="desc-box-content">
                    <span class="box-header">TUNED FOR LOW NOISE OPERATION</span><br />
                    <span class="box-content">A specially set fan curve ensures that, even at full load, fan noise is kept to a minimum.</span><br />
                    <br />
                    <span class="box-header">ZERO RPM FAN MODE</span><br />
                    <span class="box-content">At low and medium loads the cooling fan switches off entirely for near-silent operation.</span><br />
                    <br />
                </div>
            </div>
            <div id="customerReview" class="tabcontent">
                <div class="overallRating grid grid-cols-6 px-3">
                    <div class="bg-gray-200 col-span-2 p-4 py-5 rounded-lg">
                        <div class="flex flex-col gap-5 justify-center items-center mb-1">
                            <div>
                                <span class="text-[64px] font-semibold">4.0</span>

                            </div>
                            <div>
                                <span class="text-yellow-400 text-lg ml-2 flex gap-2">
                                    <i class="fa-solid fa-star"></i>
                                    <i class="fa-solid fa-star"></i>
                                    <i class="fa-solid fa-star"></i>
                                    <i class="fa-solid fa-star"></i>
                                    <i class="fa-solid fa-star"></i>
                                    <!-- Duplicate the above SVG for as many stars you need -->
                                </span>

                            </div>
                            <div>
                                <span>Customer Rating (
                                    <asp:Label ID="totalCusRating" runat="server" Text="[RatingAmount]"></asp:Label>
                                    )</span>
                            </div>
                        </div>

                    </div>
                    <div class="col-span-4 p-4 py-5 rounded-lg flex flex-col gap-5">
                        <div class="flex flex-col gap-5 mb-1">

                            <div class="flex grid grid-cols-9 items-center">
                                <div class="col-span-2 flex justify-center">

                                    <span class="text-yellow-400 text-lg ml-2 flex gap-2">
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <!-- Duplicate the above SVG for as many stars you need -->
                                    </span>
                                </div>
                                <div class="col-span-5 bg-gray-300 w-full h-2 mx-2 rounded-lg">
                                    <div class="bg-[#319ba1] h-2 rounded-lg" style="width: 63%;"></div>
                                </div>
                                <div  class="col-span-2 flex gap-1 justify-center">
                                    <asp:Label ID="fivestarPercent" runat="server" Text="63%"></asp:Label>
                                    <div>
                                        (<asp:Label ID="fivestarAmount" runat="server" Text="65002"></asp:Label>)
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="flex flex-col gap-5 mb-1">

                            <div class="flex grid grid-cols-9 items-center">
                                <div class="col-span-2 flex justify-center">

                                    <span class="text-yellow-400 text-lg ml-2 flex gap-2">
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-regular fa-star"></i>
                                        <!-- Duplicate the above SVG for as many stars you need -->
                                    </span>
                                </div>
                                <div class="col-span-5 bg-gray-300 w-full h-2 mx-2 rounded-lg">
                                    <div class="bg-[#319ba1] h-2 rounded-lg" style="width: 50%;"></div>
                                </div>
                                <div class="col-span-2 flex gap-1 justify-center">
                                    <asp:Label ID="Label1" runat="server" Text="50%"></asp:Label>
                                    <div>
                                        (<asp:Label ID="Label2" runat="server" Text="50000"></asp:Label>)
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="flex flex-col gap-5 mb-1">

                            <div class="flex grid grid-cols-9 items-center">
                                <div class="col-span-2 flex justify-center">

                                    <span class="text-yellow-400 text-lg ml-2 flex gap-2">
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-regular fa-star"></i>
                                        <i class="fa-regular fa-star"></i>
                                        <!-- Duplicate the above SVG for as many stars you need -->
                                    </span>
                                </div>
                                <div class="col-span-5 bg-gray-300 w-full h-2 mx-2 rounded-lg">
                                    <div class="bg-[#319ba1] h-2 rounded-lg" style="width: 30%;"></div>
                                </div>
                                <div class="col-span-2 flex gap-1 justify-center">
                                    <asp:Label ID="Label3" runat="server" Text="30%"></asp:Label>
                                    <div>
                                        (<asp:Label ID="Label4" runat="server" Text="2300"></asp:Label>)
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="flex flex-col gap-5 mb-1">

                            <div class="flex grid grid-cols-9 items-center">
                                <div class="col-span-2 flex justify-center">

                                    <span class="text-yellow-400 text-lg ml-2 flex gap-2">
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-regular fa-star"></i>
                                        <i class="fa-regular fa-star"></i>
                                        <i class="fa-regular fa-star"></i>
                                        <!-- Duplicate the above SVG for as many stars you need -->
                                    </span>
                                </div>
                                <div class="col-span-5 bg-gray-300 w-full h-2 mx-2 rounded-lg">
                                    <div class="bg-[#319ba1] h-2 rounded-lg" style="width: 25%;"></div>
                                </div>
                                <div class="col-span-2 flex gap-1 justify-center">
                                    <asp:Label ID="Label5" runat="server" Text="25%"></asp:Label>
                                    <div>
                                        (<asp:Label ID="Label6" runat="server" Text="65002"></asp:Label>)
                                    </div>
                                </div>
                            </div>
 
                        </div>
                        <div class="flex flex-col gap-5 mb-1">

                            <div class="flex grid grid-cols-9 items-center">
                                <div class="col-span-2 flex justify-center">

                                    <span class=" text-yellow-400 text-lg ml-2 flex gap-2">
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-regular fa-star"></i>
                                        <i class="fa-regular fa-star"></i>
                                        <i class="fa-regular fa-star"></i>
                                        <i class="fa-regular fa-star"></i>
                                        <!-- Duplicate the above SVG for as many stars you need -->
                                    </span>
                                </div>
                                <div class="col-span-5 bg-gray-300 w-full h-2 mx-2 rounded-lg">
                                    <div class="bg-[#319ba1] h-2 rounded-lg" style="width: 10%;"></div>
                                </div>
                                <div class="col-span-2 flex gap-1 justify-center">
                                    <asp:Label ID="Label7" runat="server" Text="10%"></asp:Label>
                                    <div>
                                        (<asp:Label ID="Label8" runat="server" Text="2"></asp:Label>)
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="px-3 pt-5 text-lg font-bold">
                    <p>Customer Ratings</p>
                </div>
                <div class="desc-box-content">
                    <div class="customer-review-box w-full">
                        <div class="review-container w-full">
                            <div class="profile-width">
                                <div class="profile-pic">
                                    <img src="user.png">
                                </div>
                            </div>
                            <div class="rating w-full">
                                <div class="profile-name w-full flex justify-between">
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
                                <div class="flex gap-3 items-center">
                                    <div class="cursor-pointer">
                                        12 <i class="fa-regular fa-thumbs-up"></i>
                                    </div>
                                    <div class="cursor-pointer">
                                        3 <i class="fa-regular fa-thumbs-down"></i>
                                    </div>

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
                            <div class="rating w-full">
                                <div class="profile-name w-full flex justify-between">
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
                                <div class="flex gap-3 items-center ">
                                    <div class="cursor-pointer">
                                        12 <i class="fa-regular fa-thumbs-up"></i>
                                    </div>
                                    <div class="cursor-pointer">
                                        3 <i class="fa-regular fa-thumbs-down"></i>
                                    </div>

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
