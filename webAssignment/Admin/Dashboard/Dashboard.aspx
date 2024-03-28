<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="webAssignment.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-base flex flex-col gap-7 mt-4">

        <!-- header -->
        <div class="flex flex-row justify-between">
            <div class="text-2xl font-bold ">
                <p>Dashboard</p>
            </div>
            <asp:LinkButton ID="filterDatePopUp" runat="server" CssClass="link-button">

                <asp:Label ID="startDate" runat="server" Text="28 jan 2023"></asp:Label>
                <span>- </span>
                <asp:Label ID="endDate" runat="server" Text="28 jan 2024"></asp:Label>



                <i class="fa-regular fa-calendar text-center pl-2"></i>
            </asp:LinkButton>
        </div>

        <!-- some stat -->
        <div class="grid grid-cols-4 gap-7 text-lg text-gray-600">
            <div class="flex justify-between col-span-1 bg-white p-7  rounded-lg drop-shadow items-center gap-5">
                <div class="flex flex-col gap-3">

                    <p>Total Sales</p>
                    <div>

                        <asp:Label ID="todaySales" runat="server" Text="RM100000" CssClass="font-bold text-black text-2xl"></asp:Label>
                    </div>
                    <div class="text-sm">
                        <asp:Label ID="itemSolded" runat="server" Text="Item sold on that time frame"></asp:Label>
                    </div>

                </div>
                <div>
                    <div class="flex flex-col justify-center items-end">
                        <div>

                            <asp:Image ID="soldIcon" runat="server" ImageUrl="~/Admin/Dashboard/Images/sold-out.gif" CssClass="w-24 h-24" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="flex justify-between col-span-1 bg-white p-7  rounded-lg drop-shadow items-center gap-5">
                <div class="flex flex-col gap-3">

                    <p>Total Orders</p>
                    <div>

                        <asp:Label ID="Label1" runat="server" Text="600 Orders" CssClass="font-bold text-black text-2xl"></asp:Label>
                    </div>
                    <div class="text-sm">
                        <asp:Label ID="Label2" runat="server" Text="Total orders on that time frame"></asp:Label>
                    </div>

                </div>
                <div>
                    <div class="flex flex-col justify-center items-end">
                        <div>

                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Admin/Dashboard/Images/bag.gif" CssClass="w-24 h-24" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="flex justify-between col-span-1 bg-white p-7  rounded-lg drop-shadow items-center gap-5">
                <div class="flex flex-col gap-3">

                    <p>Total User Visited</p>
                    <div>

                        <asp:Label ID="Label3" runat="server" Text="150 Users" CssClass="font-bold text-black text-2xl"></asp:Label>
                    </div>
                    <div class="text-sm">
                        <asp:Label ID="Label4" runat="server" Text="Total user visited the web on that time frame"></asp:Label>
                    </div>

                </div>
                <div>
                    <div class="flex flex-col justify-center items-end">
                        <div>

                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Admin/Dashboard/Images/customer.gif" CssClass="w-24 h-24" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="flex justify-between col-span-1 bg-white p-7  rounded-lg drop-shadow items-center gap-5">
                <div class="flex flex-col gap-3">

                    <p>Total Whishlist</p>
                    <div>

                        <asp:Label ID="Label5" runat="server" Text="1233 items" CssClass="font-bold text-black text-2xl"></asp:Label>
                    </div>
                    <div class="text-sm">
                        <asp:Label ID="Label6" runat="server" Text="Total of user whishlist item on that time frame"></asp:Label>
                    </div>

                </div>
                <div>
                    <div class="flex flex-col justify-center items-end">
                        <div>

                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Admin/Dashboard/Images/revenue.gif" CssClass="w-24 h-24" />
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <!-- graph and best selling item -->
        <div class="grid grid-cols-4 gap-7">
            <!-- graph  -->
            <div class="col-span-2">
                <div class="p-7 bg-white rounded-lg  drop-shadow">

                    <div class="flex flex-row justify-between items-center">

                        <p class=" text-lg">Sales Details</p>
                        <div>
                            <i class="fa-solid fa-circle text-blue-600"></i>
                            <span>Sales</span>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="chartSales" runat="server" Text="RM 12000" CssClass="font-bold text-2xl"></asp:Label>

                    </div>
                    <div class="w-full flex justify-center pt-3" style="height: 300px">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Admin/Dashboard/Images/dummyChartV2.jpeg" />
                    </div>
                </div>
            </div>

            <!-- best selling item -->
            <div class="col-span-2 p-7 bg-white rounded-lg  drop-shadow">
                <div class="text-xl font-bold flex flex-row gap-2 items-center justify-between mb-5">
                    <p>Best Sellers</p>
                    <div>

                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Admin/Dashboard/Images/champion.gif" CssClass="w-14 h-14" />
                    </div>


                </div>
                <asp:ListView ID="bestSellingItemLv" runat="server">
                    <LayoutTemplate>
                        <table class="w-full text-center py-4">
                            <tr class="w-full grid grid-cols-5 gap-6 py-4 rounded-lg px-3 mb-5 text-white" style="background-color:#6366F1">
                                <td class="col-span-2 text-left">Product</td>
                                <td class="col-span-1">Price</td>
                                <td class="col-span-1">Sold</td>
                                <td class="col-span-1 text-right">Profit</td>
                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="grid grid-cols-5 gap-6 w-full mb-5 px-3 hover:bg-gray-200 rounded">

                            <td class="col-span-2 flex items-center text-left">
                                <asp:Image ID="productImages" runat="server" AlternateText="Product Image" Height="64" Width="64"
                                    ImageUrl='<%# Eval("ProductImageUrl", "{0}") %>' CssClass="rounded border" />

                                <div class="pl-2 flex flex-col">
                                    <span>

                                        <%# Eval("productName") %>
                                    </span>
                                    <span class="text-gray-500">

                                        <%#Eval("productVariant") %>
                                    </span>
                                </div>



                            </td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("price", "{0:C}") %></td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("sold", "{0:C}") %></td>
                            <td class="col-span-1 flex items-center justify-end"><%# Eval("profit", "{0:C}") %></td>

                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <!--Lastest Order Section -->
        <div class="bg-white p-5">


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
    <style>
        .link-button {
            position: relative;
            padding: 12px 16px;
            color: #000;
            background-color: white;
            border-radius: 8px;
            overflow: hidden; /* Ensures the background doesn't spill out of the border-radius */
            transition: color 0.4s ease-in-out;
        }

            .link-button::before {
                content: '';
                position: absolute;
                bottom: -100%; /* Start from the bottom */
                left: 0;
                width: 100%;
                height: 100%;
                background-color: #007bff; /* The color you want on hover */
                transition: bottom 0.4s ease-in-out;
                z-index: 0;
            }

            .link-button:hover::before {
                bottom: 0; /* Slide in on hover */
            }


            .link-button:hover {
                color: white !important;
            }

                /* Remove background-color change for the icon */
                .link-button:hover i {
                    color: white !important;
                }

            /* Fix the z-index for the icon as well */
            .link-button span, .link-button label, .link-button i {
                position: relative;
                z-index: 1;
            }
    </style>
</asp:Content>
