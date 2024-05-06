<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="webAssignment.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/lib/highcharts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-base flex flex-col gap-7 mt-4">

        <!-- header -->
        <div class="flex flex-row justify-between">
            <div class="text-2xl font-bold ">
                <p>Dashboard</p>
            </div>
        </div>

        <!-- some stat -->
        <div class="grid grid-cols-4 gap-7 text-lg text-gray-600">
            <div class="flex justify-between col-span-1 bg-white px-6 py-4 rounded-lg drop-shadow items-center gap-5">
                <div class="flex flex-col gap-3">

                    <p>Total Sales</p>
                    <div>

                        <asp:Label ID="todaySales" runat="server" Text="RM 0" CssClass="font-bold text-black text-2xl"></asp:Label>
                    </div>
                    <div class="text-sm">
                        <asp:Label ID="itemSolded" runat="server" Text="Item sold "></asp:Label>
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

                        <asp:Label ID="lblOrders" runat="server" Text="600 Orders" CssClass="font-bold text-black text-2xl"></asp:Label>
                    </div>
                    <div class="text-sm">
                        <asp:Label ID="subLblOrders" runat="server" Text="Total orders on that time frame"></asp:Label>
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

                        <asp:Label ID="lblVisitCount" runat="server" Text="150 Users" CssClass="font-bold text-black text-2xl"></asp:Label>
                    </div>
                    <div class="text-sm">
                        <asp:Label ID="sublblVisitCount" runat="server" Text="Total user visited the web on that time frame"></asp:Label>
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

                        <asp:Label ID="lblWishlist" runat="server" Text="0 items" CssClass="font-bold text-black text-2xl"></asp:Label>
                    </div>
                    <div class="text-sm">
                        <asp:Label ID="sublblWishlist" runat="server" Text="Total of user wishlist item on that time frame"></asp:Label>
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
        <div class="grid grid-cols-4 gap-7 ">
            <!-- graph  -->
            <div class="col-span-2 h-[550px] bg-white rounded-lg  drop-shadow">
                <div class="p-7">

                    <div class="flex flex-row justify-between items-center">

                        <p class=" text-lg">
                            Sales Details
                            (<asp:Label ID="lblDateRange" runat="server" Text=""></asp:Label>)
                        </p>
                        <div>
                            <i class="fa-solid fa-circle text-blue-600"></i>
                            <span>Sales Details </span>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="chartSales" runat="server" Text="RM 0" CssClass="font-bold text-2xl"></asp:Label>

                    </div>
                    <div class="w-full flex justify-center pt-10" style="height: 400px; width: 100%">
                        <div id="salesChartContainer" style="width: 100%; height: 400px;"></div>
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
                    <EmptyDataTemplate>
                        <table class="orders-table w-full ">
                            <tr class="w-full ">
                                <td>
                                    <div class="flex flex-col justify-center items-center">
                                        <asp:Image ID="sadKermit" runat="server" ImageUrl="~/Admin/Category/sad_kermit.png" AlternateText="Product Image" Height="128" Width="128" />
                                        <span>No Product Found</span>
                                    </div>
                                </td>
                            </tr>

                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table class="w-full text-center py-4">
                            <tr class="w-full grid grid-cols-5 gap-6 py-4 rounded-lg px-3 mb-5 bg-gray-100">
                                <td class="col-span-2 text-left">Product</td>
                                <td class="col-span-1">Price</td>
                                <td class="col-span-1">Unit Sold</td>
                                <td class="col-span-1 text-right">Total Revenue</td>
                            </tr>
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="grid grid-cols-5 gap-6 w-full mb-5 px-3 hover:bg-gray-200 rounded">

                            <td class="col-span-2 flex items-center text-left">
        <%--                        <asp:Image ID="productImages" runat="server" AlternateText="Product Image" Height="64" Width="64"
                                    ImageUrl='<%# Eval("ProductImagePath", "{0}") %>' CssClass="rounded border" />--%>

                                <div class="pl-2 flex flex-col">
                                    <span>

                                        <%# Eval("product_name") %>
                                    </span>
                                    <span class="text-gray-500">

                                        <%#Eval("variant_name") %>
                                    </span>
                                </div>



                            </td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("variant_price", "{0:C}") %></td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("TotalUnitsSold", "{0}") %></td>
                            <td class="col-span-1 flex items-center justify-end"><%# Eval("TotalRevenue", "{0:C}") %></td>

                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <!--Lastest Order Section -->
       <%-- <div class="bg-white p-8 rounded-lg drop-shadow-lg mb-2">
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


        </div>--%>
    </div>
    <style>
    </style>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            updateChart();

            document.getElementById('btnApplyDateFilter').addEventListener('click', function () {
                var startDate = document.getElementById('txtStartDate').value;
                var endDate = document.getElementById('txtEndDate').value;
                updateChart(startDate, endDate);
            });
        });

        function updateChart(startDate, endDate) {
            // Assuming GetSalesJsonData is correctly getting and parsing the updated SQL data
            var salesData = JSON.parse('<%= GetSalesJsonData() %>');
            console.log(salesData);

            Highcharts.chart('salesChartContainer', {
                chart: {
                    type: 'line'
                },
                title: {
                    text: 'Sales Data by Date'
                },
                xAxis: {
                    categories: salesData.map(data => data.Date),
                    title: {
                        text: 'Date'
                    }
                },
                yAxis: {
                    title: {
                        text: 'Total Sales'
                    }
                },
                series: [{
                    name: 'Sales',
                    data: salesData.map(data => data.TotalSales)
                }],
                plotOptions: {
                    line: {
                        dataLabels: {
                            enabled: true
                        },
                        enableMouseTracking: false
                    }
                }
            });
        }
    </script>
</asp:Content>
