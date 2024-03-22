<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="webAssignment.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="grid grid-row-3 gap-4 h-full">

        <!--Daily details  Section -->
        <div class="grid grid-cols-3 gap-4">

            <div class="bg-white rounded shadow-sm text-base p-5 flex flex-row justify-between" style="color: #8B8E99">
                <div>


                    <p>Today Sales</p>
                    <p class="text-bold text-black">RM 100,000</p>
                    <p>We Have Sold 123 Items</p>
                </div>


                <!-- 
                <div>

                    <div class="w-24 h-24 bg-gray-200 rounded-full flex items-center justify-center">

                        <div class=" top-0 left-0 w-24 h-24 rounded-full flex justify-center items-center bg-blue-600" style="clip-path: inset(0 50% 0 0);">

                            <div class=" w-12 h-12 rounded-full bg-gray-200 border-none" style="clip-path: inset(0 50% 0 0);"></div>
                        </div>
  
                    </div>
                </div>
                -->

            </div>
            <div class="bg-white rounded shadow-sm text-base  p-5" style="color: #8B8E99">

                <p>Today Reveneu</p>
                <p class="text-bold text-black">RM 100,000</p>
                <p>Profit made for today so far</p>


            </div>
            <div class="bg-white rounded shadow-sm text-base  p-5" style="color: #8B8E99">

                <p>User Count</p>
                <p class="text-bold text-black">20069</p>
                <p>Total users signed up to G-tech</p>


            </div>
        </div>
        <!--Revenue and Progress Section -->
        <div class="grid grid-cols-3 gap-4">

            <div class="bg-white rounded col-span-2 shadow-sm text-base p-5 flex flex-col justify-between">
                <div class=" w-full mb-3">

                    <div class="flex flex-row justify-between">
                        <p>Total Revenue</p>
                        <div class="flex flex-row justify-between gap-10">

                            <div class="flex flex-row gap-3 items-center">

                                <span class="w-5 h-5 rounded-full bg-blue-600"></span>
                                <p>Profit</p>
                            </div>
                            <div class="flex flex-row gap-3 items-center">

                                <span class="w-5 h-5 rounded-full bg-gray-400"></span>
                                <p>Loss</p>
                            </div>
                        </div>
                    </div>
                    <div class="flex flex-row gap-1 items-center">

                        <p class="text-3xl text-bold text-black">RM 100,000</p>
                        <p class="text-green-500 flex items-center text-center">
                            <i class="fa-solid fa-arrow-up-long"></i>
                            5% Than Last Month

                        </p>
                    </div>

                </div>

                <div>
                    <!-- the chart goes here -->
                    <div class="h-96 bg-gray-400 w-full rounded">
                    </div>
                </div>

            </div>
            <div class="bg-white rounded shadow-sm text-base flex flex-col justify-between  p-5">
                <p>Most Sold item</p>
                <div>
                    <!-- the progress goes here -->
                    <div class="h-96 bg-gray-400 w-full rounded">
                    </div>
                </div>
            </div>

        </div>


        <!--Lastest Order Section -->
        <div class="bg-white p-5 text-base">


            <asp:ListView ID="OrdersListView" runat="server" OnSelectedIndexChanged="OrdersListView_SelectedIndexChanged">
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
                                <p>Date </p>
                                <asp:LinkButton ID="filterDateLtPd" runat="server">

            <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="col-span-1">
                                <p>Customer</p>
                            </div>
                            <div class="col-span-1 flex flex-row justify-between">
                                <p>Total </p>
                                <asp:LinkButton ID="filterTotalLtPd" runat="server">

            <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="col-span-1 flex flex-row justify-between">
                                <p>Payment </p>
                                <asp:LinkButton ID="filterPaymentLtPd" runat="server">

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
                        <td class="col-span-1 flex items-center"><%# Eval("Date", "{0:dd MMM yyyy}") %></td>
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
                                <i class="fa-solid fa-pen"></i>
                                <i class="fa-solid fa-eye"></i>
                                <i class="fa-solid fa-trash"></i>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
