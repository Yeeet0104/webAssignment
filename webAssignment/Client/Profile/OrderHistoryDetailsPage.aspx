<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ProfileMaster/ProfileMasterPage.master" AutoEventWireup="true" CodeBehind="OrderHistoryDetailsPage.aspx.cs" Inherits="webAssignment.Client.Profile.OrderHistoryDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="min-h-[80vh] p-2 flex flex-col gap-3">
        <div class="text-xl text-gray-900 font-bold p-2">Order Details</div>
        <div>
            <div class="container mx-auto p-4 bg-white rounded-lg drop-shadow h-[175px]">
                <div>
                    <p class="font-bold px-2">Order Status</p>
                </div>
                <div class="flex flex-col items-center justify-center ">
                    <div class="flex flex items-center text-blue-500 relative">
                        <div class="relative flex flex-col justify-center items-center">

                            <div class="relative rounded-full transition duration-500 ease-in-out h-[36px] w-[36px] py-3 border-2 border-blue-600 bg-blue-100 flex justify-center items-center bg-blue-600">
                                <i class="fa-solid fa-check text-white"></i>
                                <!-- Insert SVG path for the 'Order Placed' icon -->

                            </div>
                            <div class="absolute w-[100px]  flex flex-col justify-center items-center" style="bottom:-75px">

                                <asp:Image ID="orderPlaced" CssClass="w-[36px] h-[36px]" runat="server" ImageUrl="~/Admin/Dashboard/Images/bag.gif" />
                                <div class="text-center text-xs font-medium uppercase text-blue-500">Order Placed</div>
                            </div>
                        </div>

                        <div class="shrink-0 ">
                            <div class="h-1 w-36 bg-blue-600"></div>
                        </div>
                        <div class="relative flex flex-col justify-center items-center">

                            <div class="relative rounded-full transition duration-500 ease-in-out h-[36px] w-[36px] py-3 border-2 border-blue-600 bg-blue-100 flex justify-center items-center bg-blue-600">
                                <%--<i class="fa-solid fa-check text-white"></i>--%>
                                <%--<!-- Insert SVG path for the 'Order Placed' icon -->--%>

                            </div>
                            <div class="absolute flex flex-col justify-center items-center" style="bottom:-75px">

                                <asp:Image ID="Image1" CssClass="w-[36px] h-[36px]" runat="server" ImageUrl="~/Client/Profile/images/package.gif" />
                                <div class="text-center text-xs font-medium uppercase text-blue-500">Packaging</div>
                            </div>
                        </div>
                        <div class="shrink-0 ">
                            <div class="h-1 w-36 bg-blue-200"></div>
                        </div>
                        <div class="relative flex flex-col justify-center items-center">

                            <div class="relative rounded-full transition duration-500 ease-in-out h-[36px] w-[36px] py-3 border-2 border-blue-600 bg-blue-200 flex justify-center items-center">
                                <%--<i class="fa-solid fa-check text-white"></i>--%>
                                <!-- Insert SVG path for the 'Order Placed' icon -->

                            </div>
                            <div class="absolute w-[100px] flex flex-col justify-center items-center" style="bottom:-75px">

                                <asp:Image ID="Image2" CssClass="w-[36px] h-[36px]" runat="server" ImageUrl="~/Client/Profile/images/truck.gif" />
                                <div class="text-center text-xs font-medium uppercase text-blue-500">On The Road</div>
                            </div>
                        </div>
                        <div class="shrink-0 ">
                            <div class="h-1 w-36 bg-blue-200"></div>
                        </div>
                        <div class="relative flex flex-col justify-center items-center">

                            <div class="relative rounded-full transition duration-500 ease-in-out h-[36px] w-[36px] py-3 border-2 border-blue-600 bg-blue-200 flex justify-center items-center">
                                <%--<i class="fa-solid fa-check text-white"></i>--%>
                                <!-- Insert SVG path for the 'Order Placed' icon -->

                            </div>
                            <div class="absolute flex flex-col justify-center items-center" style="bottom:-75px">

                                <asp:Image ID="Image3" CssClass="w-[36px] h-[36px]" runat="server" ImageUrl="~/Client/Profile/images/agreement.gif" />
                                <div class="text-center text-xs font-medium uppercase text-blue-500">Dlivered</div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="flex flex items-center ">
                        <div class="flex flex-col justify-center items-center">

                            <asp:Image ID="orderPlaced" CssClass="w-[36px] h-[36px]" runat="server" ImageUrl="~/Admin/Dashboard/Images/bag.gif" />
                            <div class="text-center text-xs font-medium uppercase text-blue-500">Order Placed</div>
                        </div>
                        <div class="shrink-0 ">
                            <div class="h-1 w-[110px]"></div>
                        </div>
                        <div class="flex flex-col justify-center items-center">

                            <asp:Image ID="Image1" CssClass="w-[36px] h-[36px]" runat="server" ImageUrl="~/Admin/Dashboard/Images/bag.gif" />
                            <div class="text-center text-xs font-medium uppercase text-blue-500">Packaging</div>
                        </div>
                        <div class="shrink-0 ">
                            <div class="h-1 w-24"></div>
                        </div>
                        <div class="flex flex-col justify-center items-center">

                            <asp:Image ID="Image2" CssClass="w-[36px] h-[36px]" runat="server" ImageUrl="~/Admin/Dashboard/Images/bag.gif" />
                            <div class="text-center text-xs font-medium uppercase text-blue-500">Out For Delivery</div>
                        </div>
                        <div class="shrink-0 ">
                            <div class="h-1 w-24"></div>
                        </div>
                        <div class="flex flex-col justify-center items-center">

                            <asp:Image ID="Image3" CssClass="w-[36px] h-[36px]" runat="server" ImageUrl="~/Admin/Dashboard/Images/bag.gif" />
                            <div class="text-center text-xs font-medium uppercase text-blue-500">Delivered</div>
                        </div>

                    </div>--%>
                </div>
            </div>
        </div>
        <div class="flex flex-col md:flex-row gap-5 w-full">
            <!-- Order List -->
            <div class="w-[70%]">

                <div class="border border-gray-300 bg-white shadow shadow-xl rounded-2xl flex-1 flex flex-col font-semibold">
                    <div class="flex flex-row items-center gap-2 px-5 pt-6 pb-2">
                        <div class="text-lg text-gray-900 font-bold">Order List</div>
                    </div>
                    <div class="grid grid-cols-5 p-4 bg-gray-200 font-bold text-gray-600 text-sm flex items-center px-5">
                        <div class="col-span-2">
                            <p>Product</p>
                        </div>
                        <div class="col-span-1">
                            <p>Price</p>
                        </div>
                        <div class="col-span-1">
                            <p>Quantity</p>
                        </div>
                        <div class="col-span-1">
                            <p>Subtotal</p>
                        </div>
                    </div>
                    <asp:ListView ID="lvOrderList" runat="server">
                        <LayoutTemplate>

                            <div id="itemPlaceholder" runat="server"></div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="grid grid-cols-5 flex items-center px-5 py-4 font-semibold">
                                <div class="col-span-2 flex items-center gap-5">
                                    <asp:Image ID="imgProduct" runat="server" AlternateText="Image" ImageUrl='<%# Eval("ProductImageURL", "{0}") %>' class="h-16 w-16 rounded-md" />
                                    <span>
                                        <%# Eval("ProductName") %>
                                    </span>
                                </div>
                                <div class="col-span-1"><%# Eval("Price", "{0:C}") %></div>
                                <div class="col-span-1 flex items-center gap-4">
                                    <span>
                                        <%# Eval("Quantity") %>
                                    </span>
                                </div>
                                <div class="col-span-1">
                                    <%# Eval("Subtotal", "{0:C}") %>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:ListView>
                </div>


            </div>

            <div class="flex flex-col  md:w-[30%] gap-8">
                <!-- order -->
                <div class="border border-gray-300 bg-white shadow shadow-xl rounded-2xl p-6 flex-1 flex flex-col gap-5 font-semibold">
                    <div class="flex flex-row items-center gap-2 ">
                        <div class="text-lg text-gray-900 font-bold">Order#1001</div>
                    </div>

                    <div class="grid grid-cols-6">
                        <div class="col-span-1 flex items-center">
                            <i class="fa-solid fa-hashtag"></i>

                        </div>
                        <div class="col-span-2 font-semibold">
                            <span>ID</span>
                        </div>
                        <div class="col-span-3 text-right">
                            <span>O1001</span>
                        </div>
                    </div>
                    <div class="grid grid-cols-6">
                        <div class="col-span-1 flex items-center">
                            <i class="fa-solid fa-calendar"></i>

                        </div>
                        <div class="col-span-2 font-semibold">
                            <span>Date</span>
                        </div>
                        <div class="col-span-3 text-right">
                            <span>31/3/2024</span>
                        </div>
                    </div>
                    <div class="grid grid-cols-6">
                        <div class="col-span-1 flex items-center">
                            <i class="fa-solid fa-circle-info"></i>
                        </div>
                        <div class="col-span-2 font-semibold">
                            <span>Status</span>
                        </div>
                        <div class="col-span-3 text-right">
                            <span>Pending</span>
                        </div>
                    </div>

                </div>
                <!-- payment -->

                <div class="border border-gray-300 bg-white shadow shadow-xl rounded-2xl p-6 flex-1 flex flex-col gap-5 font-semibold">
                    <div class="flex flex-row items-center gap-2">
                        <div class="text-lg text-gray-900 font-bold">Payment</div>

                    </div>

                    <div class="grid grid-cols-6">
                        <div class="col-span-1 flex items-center">
                            <i class="fa-solid fa-hashtag"></i>

                        </div>
                        <div class="col-span-2  font-semibold">
                            <span>ID</span>
                        </div>
                        <div class="col-span-3 text-right">
                            <span>P2134</span>
                        </div>
                    </div>
                    <div class="grid grid-cols-6">
                        <div class="col-span-1 flex items-center">
                            <i class="fa-solid fa-calendar"></i>

                        </div>
                        <div class="col-span-2 font-semibold">
                            <span>Date</span>
                        </div>
                        <div class="col-span-3 text-right">
                            <span>1/4/2024</span>
                        </div>
                    </div>

                    <div class="grid grid-cols-6">
                        <div class="col-span-1 flex items-center">
                            <i class="fa-solid fa-coins"></i>
                        </div>
                        <div class="col-span-2 font-semibold">
                            <span>Amount</span>
                        </div>
                        <div class="col-span-3 text-right">
                            <span>RM 100</span>
                        </div>
                    </div>
                </div>

                <!-- Address -->
                <div class="border border-gray-300 shadow shadow-xl rounded-2xl p-6 flex-1 flex flex-col gap-5 font-semibold bg-white">
                    <div class="flex flex-row items-center gap-2 ">
                        <div class="text-lg text-gray-900 font-bold">Address</div>
                    </div>

                    <div class="grid grid-cols-6 ">
                        <div class="col-span-1 flex items-center">
                            <i class="fa-solid fa-truck-fast"></i>

                        </div>
                        <div class="col-span-2">
                            <span>Shipping</span>
                        </div>
                        <div class="col-span-3 text-right">
                        </div>
                        <div class="col-span-6 text-gray-700 pt-1">
                            35, Jalan Wangsa Siaga 1 , Kuala lumpur 53300
                        </div>
                    </div>
                    <div class="mt-2 grid grid-cols-6">
                        <div class="col-span-1 flex items-center">
                            <i class="fa-solid fa-badge-dollar"></i>
                        </div>
                        <div class="col-span-2">
                            <span>Billing</span>
                        </div>
                        <div class="col-span-3 text-right">
                        </div>

                        <div class="col-span-6 text-gray-700 pt-1">
                            35, Jalan Wangsa Siaga 1 , Kuala lumpur 53300
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>
</asp:Content>
