﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="WishlistPage.aspx.cs" Inherits="webAssignment.Client.Wishlist.WishlistPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="max-w-md md:max-w-2xl lg:max-w-5xl xl:max-w-7xl my-5 mx-auto min-h-[80vh] p-5">

        <div class="px-4">
            <!-- Header -->
            <div class="flex justify-between items-center mb-10">
                <span class="text-3xl text-gray-900 font-black font-bold p-4">Wishlist</span>
<%--                <asp:Button ID="btnMoveAllToCart" runat="server" Text="Move All To Cart" class="mt-2 text-white bg-blue-600 hover:bg-blue-800 rounded-lg w-40 h-10 p-2 font-semibold text-sm cursor-pointer text-center transition duration-200 shadow-md" />--%>
            </div>
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8">

                <asp:ListView ID="lvWishlist" runat="server">
                    <LayoutTemplate>
                        <div id="itemPlaceholder" runat="server"></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <a class="product" href='<%# "/Client/ProductDetails/ProductDetailsPage.aspx?ProductId=" + Eval("product_id") %>'>
                            <div class="bg-white rounded-xl border border-gray-200 shadow-md flex flex-col justify-between gap-4 min-h-[370px] p-5 relative hover:shadow-2xl transition duration-300">

                                <!-- Status Banner -->
                                <div class="absolute top-0 left-0 bg-opacity-95 text-white py-2 px-3 font-bold rounded-tl-lg rounded-br-lg text-md <%# Eval("variant_status").ToString() == "In Stock" ? "bg-green-700" : "bg-gray-700" %>">
                                    <%# Eval("variant_status") %>
                                </div>

                                <!-- Delete Button (top-right corner) -->
                                <button class="absolute top-1 right-1 bg-gray-500 text-white rounded-full p-2 shadow hover:bg-red-600 focus:outline-none w-8 h-8 flex items-center justify-center transition duration-200" id='<%# Eval("product_id") %>'>
                                    <i class="fa-solid fa-heart-crack"></i>
                                </button>

                                <div class="h-[70%] flex justify-center items-center">
                                    <asp:Image ID="imgProduct" runat="server" AlternateText="Image" ImageUrl='<%# Eval("path") %>' class="w-auto h-full rounded-lg" />
                                </div>

                                <!-- Product Details -->
                                <div class="h-[25%] flex flex-col gap-2">
                                    <h2 class="text-lg font-bold overflow-hidden whitespace-nowrap overflow-ellipsis"><%# Eval("product_name") %></h2>
                                    <div class="text-sm text-gray-700">Variation: <%# Eval("variant_name") %></div>
                                    <div class="flex justify-between items-center">
                                        <p class="text-gray-700 font-semibold"><%# Eval("variant_price", "{0:c}") %></p>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </ItemTemplate>
                </asp:ListView>


            </div>
        </div>

    </div>

</asp:Content>
