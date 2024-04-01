<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="EditOrder.aspx.cs" Inherits="webAssignment.Admin.Orders.EditOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">


        <div class="flex flex-col">
            <div class="text-2xl font-bold ">
                <p>Orders Details</p>
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
            <div class="relative mr-2">
                <i class="fa-solid fa-download text-blue-500 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg" />
            </div>
            <div class="text-sm relative ml-2 bg-blue-500 text-white flex flex-row items-center p-1 px-2 gap-2 rounded-lg">
                <i class="text-lg fa-solid fa-receipt left-4 top-5 text-white"></i>
                <asp:HyperLink ID="generateInvoice" runat="server" NavigateUrl="#">Invoice</asp:HyperLink>
            </div>

        </div>
    </div>
    <div>
        <asp:Label ID="testing" runat="server" Text="" CssClass="text-2xl"></asp:Label>
    </div>
    <div class="flex flex-col gap-8">

        <!-- second row -->
        <div class="grid grid-cols-3 gap-8">
            <!-- first col -->
            <div class="flex flex-col gap-2 rounded-xl drop-shadow-md col-span-1 w-full bg-white rounded-lg p-6">
                <div class="flex flex-row justify-between items-center mb-2">
                    <div class="flex items-center gap-2">
                        <asp:Label ID="orderId" runat="server" Text="Order#30211" CssClass="text-lg"></asp:Label>
                        <asp:Label ID="orderStatus" runat="server" Text="Processing" CssClass="bg-red-100 text-red-500 text-sm p-1 rounded-lg"></asp:Label>
                    </div>
                    <div>
                        <asp:LinkButton ID="EditOrderDetailsBtn" runat="server" CssClass="">
                        <i class="fa-solid fa-pen text-gray-400 text-sm"></i>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="flex flex-row justify-between items-center text-sm">
                    <div class="flex justify-start items-center gap-2">
                        <div class="flex justify-center  items-center rounded-full bg-gray-200 p-3 w-10 h-10">
                            <i class="fa-regular fa-calendar "></i>
                        </div>
                        <span>Added </span>
                    </div>
                    <div>
                        <asp:Label ID="addedDate" runat="server" Text="12 Dec 2022"></asp:Label>
                    </div>
                </div>
                <div class="flex flex-row justify-between items-center text-sm">
                    <div class="flex justify-start items-center gap-2">
                        <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                            <i class="fa-solid fa-credit-card"></i>
                        </div>
                        <span>Payment Method </span>
                    </div>
                    <div>
                        <asp:Label ID="paymentMethod" runat="server" Text="Visa"></asp:Label>
                    </div>
                </div>
                <div class="flex flex-row justify-between items-center text-sm">
                    <div class="flex justify-start items-center gap-2">
                        <div class="flex justify-center items-center rounded-full bg-gray-200 w-10 h-10">
                            <i class="fa-solid fa-truck"></i>
                        </div>
                        <span>Shipping Method </span>
                    </div>
                    <div>
                        <asp:Label ID="shippingMethod" runat="server" Text="Flat Shipping"></asp:Label>
                    </div>
                </div>
            </div>
            <!-- second col -->
            <div class="flex flex-col gap-2 rounded-xl drop-shadow-md col-span-1 w-full bg-white rounded-lg p-6">
                <div class="text-lg mb-2">
                    <p>Customer</p>

                </div>
                <div class="flex flex-row justify-between items-center text-sm">
                    <div class="flex justify-start items-center gap-2">
                        <div class="flex justify-center  items-center rounded-full bg-gray-200 p-3 w-10 h-10">
                            <i class="fa-solid fa-user"></i>
                        </div>
                        <span>Customer </span>
                    </div>
                    <div>
                        <asp:Label ID="customerName" runat="server" Text="John Doe"></asp:Label>
                    </div>
                </div>
                <div class="flex flex-row justify-between items-center text-sm">
                    <div class="flex justify-start items-center gap-2">
                        <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                            <i class="fa-solid fa-envelope"></i>
                        </div>
                        <span>Email </span>
                    </div>
                    <div>
                        <asp:Label ID="cusEmail" runat="server" Text="ok@gmail.com"></asp:Label>
                    </div>
                </div>
                <div class="flex flex-row justify-between items-center text-sm">
                    <div class="flex justify-start items-center gap-2">
                        <div class="flex justify-center items-center rounded-full bg-gray-200 w-10 h-10">
                            <i class="fa-solid fa-phone"></i>
                        </div>
                        <span>Phone </span>
                    </div>
                    <div>
                        <asp:Label ID="cusPhoneNum" runat="server" Text="+60173443893"></asp:Label>
                    </div>
                </div>
            </div>
            <!-- third col -->
            <div class="flex flex-col gap-2 rounded-xl drop-shadow-md col-span-1 w-full bg-white rounded-lg p-6">
                <div class="text-lg mb-2">
                    <p>Document</p>
                </div>
                <div class="flex flex-row justify-between items-center text-sm">
                    <div class="flex justify-start items-center gap-2">
                        <div class="flex justify-center  items-center rounded-full bg-gray-200 p-3 w-10 h-10">
                            <i class="fa-solid fa-receipt"></i>
                        </div>
                        <span>Invoice </span>
                    </div>
                    <div>
                        <asp:Label ID="invoiceID" runat="server" Text="INV-32011"></asp:Label>
                    </div>
                </div>
                <div class="flex flex-row justify-between items-center text-sm">
                    <div class="flex justify-start items-center gap-2">
                        <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                            <i class="fa-solid fa-box"></i>
                        </div>
                        <span>Shipping </span>
                    </div>
                    <div>
                        <asp:Label ID="shippingInfo" runat="server" Text="SHP-2011REG"></asp:Label>
                    </div>
                </div>
                <div class="flex flex-row justify-between items-center text-sm">
                    <div class="flex justify-start items-center gap-2">
                        <div class="flex justify-center items-center rounded-full bg-gray-200 w-10 h-10">
                            <i class="fa-solid fa-trophy"></i>
                        </div>
                        <span>Rewards </span>
                    </div>
                    <div>
                        <asp:Label ID="rewards" runat="server" Text="480 points"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <div class="grid grid-cols-3 gap-8 pb-3">
            <!--Order List-->

            <div class="col-span-2 bg-white rounded-xl drop-shadow-md pt-8 pl-8 pr-8">
                <asp:ListView ID="ordersListView" runat="server">
                    <LayoutTemplate>
                        <table class="orders-table w-full ">
                            <!-- Headers here -->
                            <div class="flex gap-4 items-center pb-5">

                                <p class="bold text-lg">
                                    Order List 
                                </p>
                                <asp:Label ID="orderAmount" runat="server" Text="+2 Orders" CssClass="p-1 px-2 bg-green-100 rounded-lg"></asp:Label>
                            </div>
                            <thead class="w-full rounded-lg  items-center bg-gray-100">
                                <tr class="grid grid-cols-6 gap-6 py-4  rounded-lg">
                                    <th class="col-span-2 text-left pl-4">
                                        <p>Product</p>
                                    </th>
                                    <th class="col-span-1 text-center">
                                        <p>Category</p>
                                    </th>
                                    <th class="col-span-1 text-center">
                                        <p>Quantity</p>
                                    </th>
                                    <th class="col-span-1 text-center">
                                        <p>Price</p>
                                    </th>
                                    <th class="col-span-1 text-center">
                                        <p>Total</p>
                                    </th>
                                </tr>
                            </thead>

                            <tr id="itemPlaceholder" runat="server"></tr>
 
                            <tfoot>
                                <tr class="grid grid-cols-6 gap-6 w-full  border-b-2 py-5">
                                    <!-- Span across all columns for subtotal row -->
                                    <td class="col-span-4"></td>
                                    <td class="col-span-1 text-center  font-bold ">
                                        <p>Subtotal</p>
                                    </td>
                                    <td class="col-span-1 text-center">
                                        <asp:Label ID="lblSubtotal" runat="server" Text="$711.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="grid grid-cols-6 gap-6 w-full   border-b-2 py-5 ">
                                    <!-- Span across all columns for VAT row -->
                                    <td class="col-span-4"></td>
                                    <td class="col-span-1 text-center  font-bold ">
                                        <p>SST(0%)</p>
                                    </td>
                                    <td class="col-span-1 text-center">
                                        <asp:Label ID="lblVAT" runat="server" Text="$0.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="grid grid-cols-6 gap-6 w-ful border-b-2 py-5">
                                    <!-- Span across all columns for shipping row -->
                                    <td class="col-span-4"></td>
                                    <td class="col-span-1 text-center  font-bold ">
                                        <p>Shipping Rate</p>
                                    </td>
                                    <td class="col-span-1 text-center">
                                        <asp:Label ID="lblShippingRate" runat="server" Text="$20.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="grid grid-cols-6 gap-6 w-full py-5 pb-6">
                                    <!-- Span across all columns for total row -->
                                    <td class="col-span-4"></td>
                                    <td class="col-span-1 text-center  font-bold ">
                                        <p>Total</p>
                                    </td>
                                    <td class="col-span-1 text-center">
                                        <asp:Label ID="lblTotal" runat="server" Text="$731.00"></asp:Label>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="grid grid-cols-6 gap-6 w-full py-2 border-b-2" style="color: #8B8E99">
                            <td class="col-span-2 flex flex-row gap-2 items-center pl-4">
                                <asp:Image ID="productImages" runat="server" AlternateText="Product Image" Height="64" Width="64"
                                    ImageUrl='<%# Eval("ProductImageUrl", "{0}") %>' CssClass="rounded border" />
                                <div class="flex flex-col gap-2">
                                    <span class="text-black font-semibold">
                                        <%# Eval("ProductName") %>  
                                    </span>
                                    <span class="text-xs">
                                        <%# Eval("variant") %> Products
                                    </span>
                                </div>

                            </td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("Category") %></td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("Quantity","{0}") %></td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("Price", "{0:C}") %></td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("Total", "{0:C}") %></td>

                        </tr>
                    </ItemTemplate>
                    <ItemSeparatorTemplate>
                    </ItemSeparatorTemplate>
                </asp:ListView>
            </div>


            <!-- address -->
            <div class="  drop-shadow-md col-span-1 w-full">
                <div class="bg-white rounded-xl p-6 flex flex-col gap-2">
                    <div class="text-lg mb-2">
                        <p>Address</p>
                    </div>
                    <div class="flex flex-row justify-between items-center text-sm">
                        <div class="flex justify-start items-center gap-2">
                            <div class="flex justify-center  items-center rounded-full bg-gray-200 p-3 w-10 h-10">
                                <i class="fa-solid fa-location-dot"></i>
                            </div>
                            <div class="flex flex-col">

                                <span>Billing Address </span>
                                <asp:Label ID="billingAddlbl" runat="server" Text="35, Jalan Wangsa Siaga 1 , Kuala lumpur 53300"></asp:Label>
                            </div>
                        </div>
                        <div>
                        </div>
                    </div>
                    <div class="flex flex-row justify-between items-center text-sm">
                        <div class="flex justify-start items-center gap-2">
                            <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                                <i class="fa-solid fa-location-dot"></i>
                            </div>

                            <div class="flex flex-col">

                                <span>Shipping Address </span>
                                <asp:Label ID="shippingAddresslbl" runat="server" Text="35, Jalan Wangsa Siaga 1 , Kuala lumpur 53300"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <style>
        .table-border {
              border-color: #F0F1F3;
        }
        .table-bg{
            background-color : #F9F9FC;
        }
    </style>
</asp:Content>

