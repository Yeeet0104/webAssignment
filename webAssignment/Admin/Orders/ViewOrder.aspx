<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="ViewOrder.aspx.cs" Inherits="webAssignment.Admin.Orders.EditOrder" %>

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
            <div class="text-md relative ml-2 bg-blue-500 text-white flex flex-row items-center p-3 gap-2 rounded-lg hover:bg-gray-300 hover:text-black cursor-pointer">
                <i class="fa-solid fa-pen-to-square"></i>

                <asp:LinkButton ID="editStatus" runat="server" OnClick="editStatus_Click">Edit Status</asp:LinkButton>
            </div>

        </div>
    </div>
    <div>
        <asp:Label ID="testing" runat="server" Text="" CssClass="text-2xl"></asp:Label>
    </div>
    <div class="flex flex-col gap-8">

        <!-- second row -->
        <div class="grid grid-cols-2 gap-8">
            <!-- first col -->
            <div class="flex flex-col gap-2 rounded-xl drop-shadow-md col-span-1 w-full bg-white rounded-lg p-6">
                <div class="flex flex-row justify-between items-center mb-2">
                    <div class="flex items-center gap-2">
                        <asp:Label ID="lblorderId" runat="server" Text="Order#30211" CssClass="text-lg"></asp:Label>
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
                        <asp:Label ID="lblDateOrded" runat="server" Text="12 Dec 2022"></asp:Label>
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
                        <asp:Label ID="lblpaymentMethod" runat="server" Text="Visa"></asp:Label>
                    </div>
                </div>
                <div class="flex flex-row justify-between items-center text-sm">
                    <div class="flex justify-start items-center gap-2">
                        <div class="flex justify-center items-center rounded-full bg-gray-200 w-10 h-10">
                            <i class="fa-solid fa-truck"></i>
                        </div>
                        <span>Order Status </span>
                    </div>
                    <div>
                        <asp:Label ID="lblorderStatus" runat="server" Text="Processing" CssClass=""></asp:Label>
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
        </div>

        <div class="grid grid-cols-3 gap-8 pb-3">
            <!--Order List-->

            <div class="col-span-2 bg-white rounded-xl drop-shadow-md pt-8 pl-8 pr-8">
                <asp:ListView ID="ordersListView" runat="server" OnDataBound="ordersListView_DataBound">
                    <LayoutTemplate>
                        <table class="orders-table w-full ">
                            <!-- Headers here -->
                            <div class="flex gap-4 items-center pb-5">

                                <p class="bold text-lg">
                                    Order List 
                                </p>
                            </div>
                            <thead class="w-full rounded-lg  items-center bg-gray-100">
                                <tr class="grid grid-cols-8 gap-6 py-4  rounded-lg">
                                    <th class="col-span-4 text-left pl-4">
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
                                <tr class="grid grid-cols-8 gap-6 w-full  border-b-2 py-5">
                                    <!-- Span across all columns for subtotal row -->
                                    <td class="col-span-6"></td>
                                    <td class="col-span-1 text-center  font-bold ">
                                        <p>Subtotal</p>
                                    </td>
                                    <td class="col-span-1 text-center">
                                        <asp:Label ID="lblSubtotal" runat="server" Text="RM 711.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="grid grid-cols-8 gap-6 w-ful border-b-2 py-5">
                                    <!-- Span across all columns for shipping row -->
                                    <td class="col-span-6"></td>
                                    <td class="col-span-1 text-center  font-bold ">
                                        <p>Shipping</p>
                                    </td>
                                    <td class="col-span-1 text-center">
                                        <asp:Label ID="lblShippingRate" runat="server" Text="RM 20.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="grid grid-cols-8 gap-6 w-ful border-b-2 py-5">
                                    <!-- Span across all columns for shipping row -->
                                    <td class="col-span-6"></td>
                                    <td class="col-span-1 text-center  font-bold ">
                                        <p>Discount</p>
                                        (<asp:Label ID="lblDisRate" runat="server" Text="0%"></asp:Label>)
                                    </td>
                                    <td class="col-span-1 text-center">
                                        <asp:Label ID="lblDiscount" runat="server" Text="RM 20.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="grid grid-cols-8 gap-6 w-full   border-b-2 py-5 ">
                                    <!-- Span across all columns for VAT row -->
                                    <td class="col-span-6"></td>
                                    <td class="col-span-1 text-center  font-bold ">
                                        <p>TAX(6%)</p>
                                    </td>
                                    <td class="col-span-1 text-center">
                                        <asp:Label ID="lblTax" runat="server" Text="RM 0.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="grid grid-cols-8 gap-6 w-full py-5 pb-6">
                                    <!-- Span across all columns for total row -->
                                    <td class="col-span-6"></td>
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
                        <tr class="grid grid-cols-8 gap-6 w-full py-2 border-b-2" style="color: #8B8E99">
                            <td class="col-span-4 flex flex-row gap-2 items-center pl-4">
                                <asp:Image ID="productImages" runat="server" AlternateText="Product Image" Height="64" Width="64"
                                    ImageUrl='<%# Eval("ProductImageUrl", "{0}") %>' CssClass="rounded border" />
                                <div class="flex flex-col gap-2">
                                    <span class="text-black font-semibold">
                                        <%# Eval("product_name") %>  
                                    </span>
                                    <span class="">Variant : <%# Eval("variant_name") %> 
                                    </span>
                                </div>

                            </td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("category_name") %></td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("quantity","{0}") %></td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("variant_price", "{0:C}") %></td>
                            <td class="col-span-1 flex items-center justify-center"><%# Eval("totalRowPrice", "{0:C}") %></td>

                        </tr>
                    </ItemTemplate>
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

        <asp:Panel ID="popUpPanel" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center ">
            <!-- Modal content -->
            <div class="popUp-content w-1/3 h-fit flex flex-col bg-white p-5 rounded-xl flex flex-col gap-3 drop-shadow-lg">

                <div class="grid grid-cols-3 w-full h-fit justify-center flex p-0">
                    <div>
                    </div>
                    <p class="text-2xl text-gray-700 font-bold text-center">Change Status</p>
                    <span class="w-auto flex items-center justify-end text-3xl rounded-full"></span>

                </div>
                <div class="flex flex-col justify-center items-center gap-5">


                    <div class="bold w-1/2 text-lg flex flex-col items-center">
                        <div class="flex flex-col gap-4">
                            <div>
                                <span>Current Status:  </span>
                                <asp:Label ID="currStatus" runat="server" Text="[Status]"></asp:Label>
                            </div>
                            <div>

                                <span>New Status</span>

                                <asp:DropDownList ID="statusDDl" runat="server" CssClass="border drop-shadow cursor-pointer bg-gray-100 rounded-lg p-2">
                                    <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
                                    <asp:ListItem Value="Packed" Text="Packed"></asp:ListItem>
                                    <asp:ListItem Value="On The Road" Text="On The Road"></asp:ListItem>
                                    <asp:ListItem Value="Delivered" Text="Delivered"></asp:ListItem>
                                    <asp:ListItem Value="Cancelled" Text="Cancelled"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="flex flex-row items-center gap-5">
                        <asp:Button ID="cancelChange" CssClass="hover:bg-gray-200 drop-shadow cursor-pointer bg-gray-300 p-2 px-4 rounded-lg" runat="server" Text="Cancel" OnClick="cancelChange_Click" />
                        <asp:Button ID="changeStatus" CssClass="hover:bg-blue-200 hover:text-black cursor-pointer drop-shadow bg-blue-400 text-white p-2 px-4 rounded-lg" runat="server" Text="Change" OnClick="changeStatus_Click" />
                    </div>
                    <div>
                    </div>
                </div>
            </div>

        </asp:Panel>
    </div>
    <style>
        .table-border {
            border-color: #F0F1F3;
        }

        .table-bg {
            background-color: #F9F9FC;
        }
    </style>
</asp:Content>

