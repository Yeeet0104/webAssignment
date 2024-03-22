﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="webAssignment.Admin.Orders.Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">

        <div class="w-3/5 flex flex-row items-center">
            <asp:TextBox ID="productSearchBox" runat="server" CssClass="
                                w-11/12 px-4 py-2 mr-4 text-gray-700 bg-white border rounded-md focus:border-blue-500 focus:outline-none focus:ring h-fit"
                placeholder="Search...">


            </asp:TextBox>
            <asp:LinkButton ID="searchBtn" runat="server" CssClass="px-1 py-2 text-gray-700 rounded-md">
                <i class="fa-solid fa-magnifying-glass text-xl "></i>
            </asp:LinkButton>
        </div>

        <div class="flex">
            <div class="relative mr-2">
                <i class="fa-solid fa-download text-blue-500 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg" />
            </div>
            <%--            <div class="text-sm relative ml-2 bg-blue-500 text-white flex flex-row items-center p-1 px-2 gap-2 rounded-lg">
                <i class="text-lg fa-solid fa-plus left-4 top-5 text-white"></i>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/Orders/EditOrders.aspx">Add New Product</asp:HyperLink>
            </div>--%>
        </div>
    </div>
    <!--End-->

    <!--Second Row-->
    <div class="flex flex-row justify-between text-sm text-gray-600 font-medium my-4 justify-self-center">
        <div class="grid grid-cols-5 bg-white gap-3 text-center rounded p-2">
            <div class="col-span-1 px-3 py-1 text-blue-600 bg-gray-100 rounded-lg">
                All Status
            </div>
            <div class="col-span-1 px-3 py-1">
                Processing
            </div>
            <div class="col-span-1 px-3 py-1 ">
                Shipped
            </div>
            <div class="col-span-1 px-3 py-1 ">
                Delivered
            </div>
            <div class="col-span-1 px-3 py-1 ">
                Cancelled
            </div>
        </div>
        <div class="flex items-center gap-3">
            <div class="">
                <asp:LinkButton ID="filterDateBtn" runat="server" class="p-3 border border-gray-200 rounded-lg bg-white flex gap-3 items-center">
                     <i class="fa-solid fa-calendar-days"></i>
                    <span>
                       Select Date
                    </span>
                </asp:LinkButton>
            </div>
            <div class="">
                <asp:LinkButton ID="filterOptionbtn" runat="server" class="p-3 border border-gray-200 rounded-lg bg-white flex gap-3 items-center">
                <i class="fa-solid fa-sliders "></i>
                    <span>
                       Filters
                    </span>
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <!--End-->

    <!--Product List-->
    <div class="bg-white p-5 text-base rounded-lg">


        <asp:ListView ID="ordersListView" runat="server" OnSelectedIndexChanged="ordersListView_SelectedIndexChanged" OnItemCommand="OrdersListView_ItemCommand">
            <LayoutTemplate>
                <table class="orders-table w-full">
                    <!-- Headers here -->
                    <p class="pb-5">
                        Latest Orders
                    </p>
                    <div class="grid grid-cols-9 gap-6 mb-4">
                        <div class="col-span-1">
                            <p>Order ID</p>
                        </div>
                        <div class="col-span-2">
                            <p>Product</p>
                        </div>
                        <div class="col-span-1 flex flex-row justify-between">
                            <p>Order Date </p>
                            <asp:LinkButton ID="filterOdDateLv" runat="server">

        <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="col-span-1">
                            <p>Customer</p>
                        </div>
                        <div class="col-span-1 flex flex-row justify-between">
                            <p>Total </p>
                            <asp:LinkButton ID="filterTotalPriceLv" runat="server">

        <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="col-span-1 flex flex-row justify-between">
                            <p>Payment Date </p>
                            <asp:LinkButton ID="filterPayDayeLv" runat="server">

        <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="col-span-1 flex flex-row justify-between">
                            <p>Status </p>
                            <asp:LinkButton ID="filterStatusLtPd" runat="server">

        <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                            </asp:LinkButton>
                        </div>

                        <div class="col-span-1 flex justify-end">
                            <p>Action</p>
                        </div>

                        <tr id="itemPlaceholder" runat="server"></tr>
                        <hr class="border rounded mb-3" />
                </table>
                <table>
                    <tfoot>
                        <hr class="border rounded mb-2" />

                        <!-- footer for pagination ( WILL CHANGE TO physical button later) -->
                        <div class="flex flex-row text-gray-400 justify-between rounded-b-lg bg-white items-center">
                            <asp:Label ID="pageNumFoot" runat="server" Text="Showing 1-10 from 100" class="text-normal text-base p-5"></asp:Label>
                            <div class="flex">
                                <div class="p-4 text-base flex flex-row gap-3">
                                    <div class="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center text-blue-500">
                                        <i class="fa-solid fa-arrow-left-long"></i>
                                    </div>
                                    <div class="min-w-11 min-h-11 rounded-full bg-blue-500 text-white border-blue-500 border flex items-center justify-center">
                                        <i class="fa-solid fa-1"></i>
                                    </div>
                                    <div class="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center">
                                        <i class="fa-solid fa-2"></i>
                                    </div>
                                    <div class="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center">
                                        <i class="fa-solid fa-3"></i>
                                    </div>
                                    <div class="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center">
                                        <i class="fa-solid fa-4"></i>
                                    </div>
                                    <div class="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center text-blue-500">
                                        <i class="fa-solid fa-arrow-right-long"></i>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </tfoot>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="grid grid-cols-9 gap-6 w-full mb-5" style="color: #8B8E99">
                    <td class="col-span-1 flex items-center text-black"><%# Eval("OrderId") %></td>
                    <td class="col-span-2 flex flex-row gap-2 items-center">
                        <asp:Image ID="productImages" runat="server" AlternateText="Product Image" Height="64" Width="64"
                            ImageUrl='<%# Eval("ProductImageUrl", "{0}") %>' CssClass="rounded border" />
                        <div class="flex flex-col gap-2">
                            <span>
                                <%# Eval("ProductName") %>  
                            </span>
                            <span>+ <%# Eval("AdditionalProductsCount") %> Products
                            </span>
                        </div>

                    </td>
                    <td class="col-span-1 flex items-center"><%# Eval("Order Date", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-1 flex items-center"><%# Eval("CustomerName") %></td>
                    <td class="col-span-1 flex items-center"><%# Eval("Total", "{0:C}") %></td>
                    <td class="col-span-1 flex items-center"><%# Eval("PaymentDate", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-1 flex items-center w-full justify-center">

                        <div class="rounded-xl flex bg-red-200 w-4/5 p-3 text-center justify-center">

                            <%# Eval("Status") %>
                        </div>


                    </td>
                    <td class="col-span-1 flex justify-end items-center">
                        <div class="flex flex-row gap-2">
                            <asp:LinkButton ID="editItem" runat="server" CommandName="EditOrder" CommandArgument='<%# Eval("OrderID") %>'>                            
                                <i class="fa-solid fa-pen"></i>
                            </asp:LinkButton>
                            <i class="fa-solid fa-eye"></i>
                            <i class="fa-solid fa-trash"></i>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>

        </asp:ListView>
    </div>
</asp:Content>
