<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="ProductDetailsPage.aspx.cs" Inherits="webAssignment.Client.ProductDetails.ProductDetailsPage" %>

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
                    <asp:Label ID="lblShortProductDesc1" runat="server" CssClass="product-desc" Text="Short Product Description" /></span>
                <div class="product-variations">
                    <asp:HiddenField ID="selectedVariation" runat="server"  />
                    <asp:Button ID="btnVariation1" runat="server" CssClass="variation-btn" Text="Variation 1" OnClick="UpdatePrice" runat="server" />
                    <asp:Button ID="btnVariation2" runat="server" CssClass="variation-btn" Text="Variation 2" OnClick="UpdatePrice" runat="server" />
                    <asp:Button ID="btnVariation3" runat="server" CssClass="variation-btn" Text="Variation 3" OnClick="UpdatePrice" runat="server" />  
                </div>
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
                    <asp:Label ID="lblShortProductDesc2" runat="server" CssClass="box-content" Text="Long Product Description 1" />
                    <br /><br />
                    <asp:Label ID="lblShortProductDesc3" runat="server" CssClass="box-content" Text="Long Product Description 2" />
                    <br />
                </div>
            </div>
            <div id="customerReview" class="tabcontent">
                <div class="overallRating grid grid-cols-6 px-3">
                    <div class="bg-gray-200 col-span-2 p-4 py-5 rounded-lg">
                        <div class="flex flex-col gap-5 justify-center items-center mb-1">
                            <div>
                                <span class="text-[64px] font-semibold">
                                    <asp:Label ID="lblOverallRating" runat="server" Text="0.0"></asp:Label>
                                </span>
                            </div>
                            <div id="divStarRating" class="text-yellow-400 text-lg ml-2 flex gap-2" runat="server"></div>
                            <div>
                                <span>Total Rating (<asp:Label ID="lblTotalRatings" runat="server" Text="[RatingAmount]"></asp:Label>)</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-span-4 p-4 py-5 rounded-lg flex flex-col gap-5">
                        <div class="flex flex-col gap-5 mb-1">
                            <!-- Star 5 -->
                            <div class="flex grid grid-cols-9 items-center">
                                <div class="col-span-2 flex">
                                    <span class="text-yellow-400 text-lg ml-2 flex gap-2">
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                    </span>
                                </div>
                                <div class="col-span-5 bg-gray-300 w-full h-2 mx-2 rounded-lg">
                                    <div class="bg-[#319ba1] h-2 rounded-lg" style="width: 90%;"></div>
                                </div>
                                <div class="col-span-2 flex gap-1 justify-center">
                                    <asp:Label ID="Label5StarCount" runat="server" Text="9.9%"></asp:Label>
                                    <div>(<asp:Label ID="Label5StarPercentage" runat="server" Text="0"></asp:Label>)</div>
                                </div>
                            </div>
                            <!-- Star 4 -->
                            <div class="flex grid grid-cols-9 items-center">
                                <div class="col-span-2 flex">
                                    <span class="text-yellow-400 text-lg ml-2 flex gap-2">
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                    </span>
                                </div>
                                <div class="col-span-5 bg-gray-300 w-full h-2 mx-2 rounded-lg">
                                    <div class="bg-[#319ba1] h-2 rounded-lg" style='<%# "width:" + Eval("fivestarPercent") + ";" %>'></div>
                                </div>
                                <div class="col-span-2 flex gap-1 justify-center">
                                    <asp:Label ID="Label4StarCount" runat="server" Text="9.9%"></asp:Label>
                                    <div>(<asp:Label ID="Label4StarPercentage" runat="server" Text="0"></asp:Label>)</div>
                                </div>
                            </div>   
                            <!-- Star 3 -->
                            <div class="flex grid grid-cols-9 items-center">
                                <div class="col-span-2 flex">
                                    <span class="text-yellow-400 text-lg ml-2 flex gap-2">
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                    </span>
                                </div>
                                <div class="col-span-5 bg-gray-300 w-full h-2 mx-2 rounded-lg">
                                    <div class="bg-[#319ba1] h-2 rounded-lg" style='<%# "width:" + Eval("fivestarPercent") + ";" %>'></div>
                                </div>
                                <div class="col-span-2 flex gap-1 justify-center">
                                    <asp:Label ID="Label3StarCount" runat="server" Text="9.9%"></asp:Label>
                                    <div>(<asp:Label ID="Label3StarPercentage" runat="server" Text="0"></asp:Label>)</div>
                                </div>
                            </div>       
                            <!-- Star 2 -->
                            <div class="flex grid grid-cols-9 items-center">
                                <div class="col-span-2 flex">
                                    <span class="text-yellow-400 text-lg ml-2 flex gap-2">
                                        <i class="fa-solid fa-star"></i>
                                        <i class="fa-solid fa-star"></i>
                                    </span>
                                </div>
                                <div class="col-span-5 bg-gray-300 w-full h-2 mx-2 rounded-lg">
                                    <div class="bg-[#319ba1] h-2 rounded-lg" style='<%# "width:" + Eval("fivestarPercent") + ";" %>'></div>
                                </div>
                                <div class="col-span-2 flex gap-1 justify-center">
                                    <asp:Label ID="Label2StarCount" runat="server" Text="9.9%"></asp:Label>
                                    <div>(<asp:Label ID="Label2StarPercentage" runat="server" Text="0"></asp:Label>)</div>
                                </div>
                            </div> 
                            <!-- Star 1 -->
                            <div class="flex grid grid-cols-9 items-center">
                                <div class="col-span-2 flex">
                                    <span class="text-yellow-400 text-lg ml-2 flex gap-2">
                                        <i class="fa-solid fa-star"></i>
                                    </span>
                                </div>
                                <div class="col-span-5 bg-gray-300 w-full h-2 mx-2 rounded-lg">
                                    <div class="bg-[#319ba1] h-2 rounded-lg" style='<%# "width:" + Eval("fivestarPercent") + ";" %>'></div>
                                </div>
                                <div class="col-span-2 flex gap-1 justify-center">
                                    <asp:Label ID="Label1StarCount" runat="server" Text="9.9%"></asp:Label>
                                    <div>(<asp:Label ID="Label1StarPercentage" runat="server" Text="0"></asp:Label>)</div>
                                </div>
                            </div> 
                        </div>
                      </div>
                    </div>
                <div class="px-3 pt-5 text-lg font-bold">
                    <p>Customer Ratings</p>
                </div>
                <div class="desc-box-content">
                    <asp:Repeater ID="rptReviews" runat="server">
                        <ItemTemplate>
                            <div class="customer-review-box">
                                <div class="review-container">
                                    <div class="profile-width">
                                        <div class="profile-pic">
                                            <img src="user.png" />
                                        </div>
                                    </div>
                                    <div class="rating w-full">
                                        <div class="profile-name w-full flex justify-between">
                                            <p class="review-username"><%# Eval("ReviewerName") %></p>
                                            <p class="review-time"><%# Convert.ToDateTime(Eval("ReviewDate")).ToString("yyyy-MM-dd HH:mm") %></p>
                                        </div>
                                        <!-- Display star ratings dynamically -->
                                        <div class="stars">
                                            <%# GetStarRating(Convert.ToInt32(Eval("Rating"))) %>
                                        </div>
                                        <div class="comments-container my-2">
                                            <p class="comment-text"><%# Eval("Comment") %></p>
                                        </div>
                                        <div class="flex gap-3 items-center">
                                            <div class="cursor-pointer">
                                                <asp:LinkButton ID="btnLike" runat="server" CssClass="like-button" CommandName="Like" CommandArgument='<%# Eval("ReviewId") %>'>
                                                    <%# Eval("Likes") %> <i class="fa-regular fa-thumbs-up"></i>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="cursor-pointer">
                                                <asp:LinkButton ID="btnDislike" runat="server" CssClass="dislike-button" CommandName="Dislike" CommandArgument='<%# Eval("ReviewId") %>'>
                                                    <%# Eval("Dislikes") %> <i class="fa-regular fa-thumbs-down"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>
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
