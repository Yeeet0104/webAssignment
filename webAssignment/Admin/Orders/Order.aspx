<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="webAssignment.Admin.Orders.Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">

        <div class="flex flex-col">
            <div class="text-2xl font-bold ">
                <p>Orders</p>
            </div>
            <div class="flex flex-row text-sm py-2">
                <asp:SiteMapPath
                    ID="SiteMapPath1"
                    runat="server"
                    RenderCurrentNodeAsLink="false"
                    PathSeparator=">"
                    CssClass="siteMap font-bold flex gap-2 text-sm pt-2">
                </asp:SiteMapPath>
            </div>
        </div>

        <div class="flex">
            <div class="relative">
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
        <div class="grid grid-cols-6 bg-white gap-3 text-center  p-2 rounded-md drop-shadow-md">

            <div class="col-span-1">
                <asp:Button ID="allStatusFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 text-blue-600 bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="All Product" OnClick="allStatusFilter_click" style="height: 26px" />
            </div>
            <div class="col-span-1">
                <asp:Button ID="pendingFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="Pending" OnClick="pendingFilter_click" />
            </div>
            <div class="col-span-1">
                <asp:Button ID="packedFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="Packed" OnClick="packedFilter_click" />
            </div>
            <div class="col-span-1">
                <asp:Button ID="otr" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="On The Road" OnClick="OnTheRoadFilter_click" />
            </div>
            <div class="col-span-1">
                <asp:Button ID="deliveredFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="Delivered" OnClick="deliveredFilter_click" />
            </div>
            <div class="col-span-1">
                <asp:Button ID="cancelFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="Cancel" OnClick="cancelFilter_click" />
            </div>
        </div>
        <div class="flex items-center gap-3">
            <div class="">
                <asp:LinkButton ID="filterDateBtn" runat="server" class="p-3 border border-gray-200 rounded-md drop-shadow-md bg-white flex gap-3 items-center">
                     <i class="fa-solid fa-calendar-days"></i>
                    <span>
                       Select Date
                    </span>
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <!--End-->

    <!--Product List-->
    <div class="bg-white p-5 text-base rounded-lg drop-shadow-lg">
        <p class="pb-5">
            Latest Orders
        </p>

        <asp:ListView ID="ordersListView" runat="server" OnItemCommand="OrdersListView_ItemCommand" OnSorting="ordersListView_Sorting" OnDataBound="ordersListView_DataBound">
            <LayoutTemplate>
                <div style="overflow-x: auto">
                    <table class="orders-table w-full " style="overflow-x: auto; min-width: 1450px">
                        <!-- Headers here -->
                        <tbody>
                            <tr class="grid grid-cols-9 gap-6 px-4 py-2 rounded-lg  items-center bg-gray-100 mb-3">
                                <td class="col-span-1">
                                    <p>Order ID</p>
                                </td>
                                <td class="col-span-2">
                                    <p>Product</p>
                                </td>
                                <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                    <asp:LinkButton ID="filterOdDateLv" runat="server" CommandName="Sort" CommandArgument="date_ordered">
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
                                    <asp:LinkButton ID="filterTotalPriceLv" runat="server" CommandName="Sort" CommandArgument="total_price">
                                  <div class="flex flex-row justify-between items-center p-2">
                                                     <p>Total </p>
                        <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                                    
                                 </div>

                                    </asp:LinkButton>
                                </td>
                                <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">

                                    <asp:LinkButton ID="filterPayDayeLv" runat="server" CommandName="Sort" CommandArgument="date_paid">
                                                                  <div class="flex flex-row justify-between items-center p-2">
                            <p>Payment Date </p>
<i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                                    
                                 </div>

                                    </asp:LinkButton>
                                </td>
                                <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                    <asp:LinkButton ID="filterStatusLtPd" runat="server" CommandName="Sort" CommandArgument="o.status">
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

                        </tbody>
                        <tfoot>
                            <tr class="">
                                <td class="flex flex-row text-gray-400 justify-between rounded-b-lg bg-white items-center">

                                    <asp:Label ID="pageNumFoot" runat="server" Text="Showing 1-10 from 100" class="text-normal text-base p-5"></asp:Label>
                                    <div class="p-4 text-base flex flex-row gap-3">
                                        <asp:LinkButton ID="prevPage" runat="server" OnClick="prevPage_Click" CssClass="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center text-blue-500">
                                    <i class="fa-solid fa-arrow-left-long"></i>
                                        </asp:LinkButton>
                                        <div class="min-w-11 min-h-11 rounded-full bg-blue-500 text-white border-blue-500 border flex items-center justify-center">
                                            <asp:Label ID="lblCurrPagination" runat="server" Text="1"></asp:Label>
                                        </div>

                                        <asp:LinkButton ID="nextPage" runat="server" OnClick="nextPage_Click" CssClass="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center text-blue-500">
<i class="fa-solid fa-arrow-right-long"></i>
                                        </asp:LinkButton>

                                    </div>

                                </td>
                            </tr>
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
                    <td class="col-span-1 flex items-center px-2 justify-center"><%# Eval("OrderDate", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-1 flex items-center justify-center"><%# Eval("CustomerName") %></td>
                    <td class="col-span-1 flex items-center justify-center"><%# Eval("Total", "{0:C}") %></td>
                    <td class="col-span-1 flex items-center justify-center"><%# Eval("PaymentDate", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-1 flex items-center w-full text-gray-600 justify-center">

                        <div class="<%# 
                            Eval("Status").ToString() == "Pending" ? "bg-gray-200" 
                                :  Eval("Status").ToString() == "On The Road" ? "bg-blue-200" 
                                :   Eval("Status").ToString() == "Packed" ? "bg-yellow-200"
                                :   Eval("Status").ToString() == "Delivered" ? "bg-green-200"
                                :   Eval("Status").ToString() == "Cancelled" ? "bg-red-200" : "bg-white-200"
    
                            
                            %> rounded-xl flex w-4/5 p-3 text-center justify-center">

                            <%# Eval("Status") %>
                        </div>


                    </td>
                    <td class="col-span-1 flex justify-end items-center">
                        <div class="flex flex-row gap-4 items-center">
                            <asp:LinkButton ID="editItem" runat="server" CommandName="EditOrder" CommandArgument='<%# Eval("OrderID") %>' ToolTip="View Order">                            
                                <i class="fa-solid fa-eye"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="cancelOrder" runat="server" CommandName="DeleteOrder" CommandArgument='<%# Eval("OrderID") %>' ToolTip="Cancel Order">                            
                                <i class="fa-solid fa-ban"></i>
                            </asp:LinkButton>

                        </div>
                    </td>

                </tr>

            </ItemTemplate>

        </asp:ListView>
    </div>

    <asp:Panel ID="popUpDelete" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center ">
        <!-- Modal content -->
        <div class="popUp-content w-1/3 h-fit flex flex-col bg-white p-5 rounded-xl flex flex-col gap-3 drop-shadow-lg">

            <div class="grid grid-cols-3 w-full h-fit justify-center flex p-0">
                <div>
                </div>
                <p class="text-2xl text-red-600 font-bold text-center">WARNING</p>
                <span class="w-auto flex items-center justify-end text-3xl rounded-full">


                    <asp:LinkButton ID="closePopUp" runat="server" OnClick="closePopUp_Click">
                     <i class=" fa-solid fa-xmark"></i>
                    </asp:LinkButton>

                </span>

            </div>
            <div class="flex flex-col justify-center items-center gap-5">

                <div style="font-size: 64px">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Admin/Orders/Images/trash.gif" AlternateText="trashcan" CssClass="w-28 h-28 " />

                </div>
                <p class="bold text-lg break-normal text-center">Are you sure you want to delete the following item?</p>
                <p class="bold text-lg">
                    <asp:Label ID="lblItemInfo" runat="server" Text="[OrderID]"></asp:Label>
                </p>
                <asp:TextBox ID="passwordForDelete" runat="server" TextMode="Password" CssClass="p-2 px-4 border rounded-xl" placeholder="Enter password to confirm"></asp:TextBox>
                <div>

                    <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" CssClass="bg-gray-300 p-2 px-4 rounded-lg cursor-pointer" OnClick="btnCancelDelete_Click" />
                    <asp:Button ID="btnConfirmDelete" runat="server" Text="Delete" CssClass="bg-red-400 p-2 px-4 rounded-lg cursor-pointer" />
                </div>
            </div>
        </div>

    </asp:Panel>
    <style>
        @keyframes rockUpDown {
            0%, 100% {
                transform: translateY(0);
            }

            10% {
                transform: translateY(-10px);
            }

            20%, 40%, 60%, 80% {
                transform: translateY(-10px) rotate(-6deg);
            }

            30%, 50%, 70% {
                transform: translateY(-10px) rotate(6deg);
            }

            90% {
                transform: translateY(-10px);
            }
        }

        .fa-trash-can {
            display: inline-block;
            transition: transform ease-in-out 0.75s;
        }

            .fa-trash-can:hover {
                animation: rockUpDown 1.2s ease-in-out infinite;
            }
    </style>
</asp:Content>
