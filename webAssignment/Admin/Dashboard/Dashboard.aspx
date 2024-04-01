<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="webAssignment.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

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
            <div class="flex justify-between col-span-1 bg-white px-6 py-4 rounded-lg drop-shadow items-center gap-5">
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

                            <asp:Image ID="soldIcon" runat="server" ImageUrl="~/Admin/Dashboard/Images/sold-out.gif" CssClass="w-20 h-18" />

                        </div>
                    </div>
                </div>
            </div>
            <div class="flex justify-between col-span-1 bg-white px-6 py-4 rounded-lg drop-shadow items-center gap-5">
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

                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Admin/Dashboard/Images/bag.gif" CssClass="w-20 h-18" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="flex justify-between col-span-1 bg-white px-6 py-4 rounded-lg drop-shadow items-center gap-5">
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

                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Admin/Dashboard/Images/customer.gif" CssClass="w-20 h-18" />

                        </div>
                    </div>
                </div>
            </div>
            <div class="flex justify-between col-span-1 bg-white px-6 py-4 rounded-lg drop-shadow items-center gap-5">
                <div class="flex flex-col gap-3">

                    <p>Total Wishlist</p>
                    <div>

                        <asp:Label ID="Label5" runat="server" Text="1233 items" CssClass="font-bold text-black text-2xl"></asp:Label>
                    </div>
                    <div class="text-sm">
                        <asp:Label ID="Label6" runat="server" Text="Total of user wishlist item on that time frame"></asp:Label>
                    </div>

                </div>
                <div>
                    <div class="flex flex-col justify-center items-end">
                        <div>

                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Admin/Dashboard/Images/revenue.gif" CssClass="w-20 h-18" />
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
                    <div class="w-full flex justify-center pt-10" style="height: 400px; width: 100%">
                        <canvas id="myChart"></canvas>
                        <%--<asp:Image ID="Image4" runat="server" ImageUrl="~/Admin/Dashboard/Images/dummyChartV2.jpeg" />--%>
                    </div>
                </div>
            </div>

            <!-- best selling item -->
            <div class="col-span-2 px-7 py-4 bg-white rounded-lg  drop-shadow">
                <div class="text-xl font-bold flex flex-row gap-2 items-center justify-between mb-2">
                    <p>Best Sellers</p>
                    <div>

                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Admin/Dashboard/Images/champion.gif" CssClass="w-14 h-14" />
                    </div>


                </div>
                <asp:ListView ID="bestSellingItemLv" runat="server">
                    <LayoutTemplate>
                        <table class="w-full text-center py-4">
                            <tr class="w-full grid grid-cols-5 gap-6 py-4 rounded-lg px-3 mb-5 bg-gray-100">
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
        <div class="bg-white p-8 rounded-lg drop-shadow-lg mb-2">
            <p class="pb-5 text-black font-bold">
                Latest Orders
            </p>

            <asp:ListView ID="ordersListView" runat="server" OnSelectedIndexChanged="ordersListView_SelectedIndexChanged" OnItemCommand="OrdersListView_ItemCommand">
                <LayoutTemplate>
                    <div style="overflow-x: auto">
                        <table class="orders-table w-full " style="overflow-x: auto; min-width: 1450px">
                            <!-- Headers here -->
                            <tr class="grid grid-cols-9 gap-6 px-4 py-2 rounded-lg  items-center bg-gray-100 mb-3">
                                <td class="col-span-1">
                                    <p>Order ID</p>
                                </td>
                                <td class="col-span-2">
                                    <p>Product</p>
                                </td>
                                <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                    <asp:LinkButton ID="filterOdDateLv" runat="server">
                            <div class="flex flex-row justify-between items-center p-2">
                                <p>Order Date </p>
                            <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>

                            </div>

                                    </asp:LinkButton>
                                </td>
                                <td class="col-span-1 text-center">
                                    <p>Customer</p>
                                </td>
                                <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                    <asp:LinkButton ID="filterTotalPriceLv" runat="server">
                                  <div class="flex flex-row justify-between items-center p-2">
                                                     <p>Total </p>
                        <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                                    
                                 </div>

                                    </asp:LinkButton>
                                </td>
                                <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">

                                    <asp:LinkButton ID="filterPayDayeLv" runat="server">
                                                                  <div class="flex flex-row justify-between items-center p-2">
                            <p>Payment Date </p>
<i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                                    
                                 </div>

                                    </asp:LinkButton>
                                </td>
                                <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                    <asp:LinkButton ID="filterStatusLtPd" runat="server">
                              <div class="flex flex-row justify-between items-center p-2">
                                    <p>Status </p>
                                    <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>

                              </div>
                                    </asp:LinkButton>
                                </td>

                                <td class="col-span-1 flex justify-end">
                                    <p>Action</p>

                                </td>

                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                        <table>
                            <tfoot>

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
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="grid grid-cols-9 gap-6 w-full mb-5 p-4 border-b-2" style="color: #8B8E99">

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
                        <td class="col-span-1 flex items-center px-2 justify-center"><%# Eval("Order Date", "{0:dd MMM yyyy}") %></td>
                        <td class="col-span-1 flex items-center justify-center"><%# Eval("CustomerName") %></td>
                        <td class="col-span-1 flex items-center justify-center"><%# Eval("Total", "{0:C}") %></td>
                        <td class="col-span-1 flex items-center justify-center"><%# Eval("PaymentDate", "{0:dd MMM yyyy}") %></td>
                        <td class="col-span-1 flex items-center w-full justify-center">

                            <div class="<%# Eval("Status").ToString() == "Shipped" ? "bg-green-200" : "bg-red-200" %> rounded-xl flex w-4/5 p-3 text-center justify-center">

                                <%# Eval("Status") %>
                            </div>


                        </td>
                        <td class="col-span-1 flex justify-end items-center">
                            <div class="flex flex-row gap-4 items-center">
                                <asp:LinkButton ID="editItem" runat="server" CommandName="EditOrder" CommandArgument='<%# Eval("OrderID") %>'>                            
                                <i class="fa-solid fa-pen"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="deleteItem" runat="server" CommandName="DeleteOrder" CommandArgument='<%# Eval("OrderID") %>'>                            
                                <i class="fa-solid fa-trash"></i>
                                </asp:LinkButton>

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
    <script>
        const ctx = document.getElementById('myChart').getContext('2d');
        const myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
                datasets: [{
                    label: 'Monthly Sales',
                    data: [100000, 90000, 80000, 85000, 60000, 50000, 75000, 80000, 85000], // replace these values with your actual data
                    backgroundColor: [
                        '#6366F1',
                    ],
                    borderColor: [
                        '#6366F1',
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Sales Amount (RM)', // Y-axis label
                            font: {
                                size: 14 // Customize the font size as needed

                            }
                        }
                    },
                    x: {
                        title: {
                            display: true,
                            text: 'Month', // X-axis label
                            font: {
                                size: 14 // Customize the font size as needed
                            },
                        }
                    }
                },
                plugins: {
                    legend: {
                        display: false
                    }
                },
            }
        });
    </script>
</asp:Content>
