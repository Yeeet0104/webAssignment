<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ProfileMaster/ProfileMasterPage.master" AutoEventWireup="true" CodeBehind="OrderHistoryDetailsPage.aspx.cs" Inherits="webAssignment.Client.Profile.OrderHistoryDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="min-h-[80vh] p-2 flex flex-col gap-3">
        <div class=" p-2 flex justify-between items-center">
            <div class="flex gap-4 items-center text-lg text-gray-500 font-bold">
                <span class="">Order History</span>
                <i class="fa-solid fa-caret-right pt-1 pl-1"></i>
                <span class="text-xl text-gray-900 font-bold">Order Details</span>

            </div>
            <div id="cancelBox" class="hidden" runat="server">
                <asp:LinkButton ID="lbCancel" runat="server" CssClass="font-semibold text-base bg-red-600 text-white py-1 px-4 rounded-lg flex items-center gap-2" OnClick="lbCancel_Click"><i class="fa-regular fa-xmark text-xs"></i><span>Cancel & Refund</span></asp:LinkButton>
            </div>
            <%--<div id="reviewBox" class="hidden" runat="server">
                <asp:LinkButton ID="lbReview" runat="server" CssClass="font-semibold text-base bg-blue-700 text-white py-1 px-4 rounded-lg flex items-center gap-2 " OnClick="lbReview_Click"><i class="fa-regular fa-pen-to-square text-xs"></i><span>Rate</span></asp:LinkButton>
            </div>--%>
        </div>
        <div class="mb-4 shadow shadow-lg rounded-xl border border-gray-300">
            <div class="container mx-auto p-4 bg-white rounded-xl h-[200px] relative">

                <span id="cancelled" class="hidden font-bold text-lg text-white px-2 py-1 rounded-lg absolute top-4 right-4 bg-red-500" runat="server">Cancelled</span>
                <div class="pb-4">
                    <p class="font-bold text-lg">Order Status</p>
                </div>
                <!--Status Bar-->
                <div class="flex flex-col items-center justify-center">
                    <div class="flex flex items-center text-blue-500 relative">
                        <!--PENDING-->
                        <div class="relative flex flex-col justify-center items-center">

                            <div id="pending" class="relative rounded-full transition duration-500 ease-in-out h-[36px] w-[36px] py-3 flex justify-center items-center bg-blue-200" runat="server">
                                <!-- checked -->
                                <i id="checkPending" class="fa-solid fa-check text-white hidden" runat="server"></i>
                                <i id="crossPending" class="fa-solid fa-x text-white hidden" runat="server"></i>

                            </div>
                            <div class="absolute w-[100px]  flex flex-col justify-center items-center" style="bottom: -75px">
                                <!--SVG-->
                                <asp:Image ID="orderPlaced" CssClass="w-[36px] h-[36px]" runat="server" ImageUrl="~/Admin/Dashboard/Images/bag.gif" />
                                <div class="text-center text-xs font-medium uppercase text-blue-500">Pending</div>
                            </div>
                        </div>

                        <!--Line-->
                        <div class="shrink-0 ">
                            <div id="line1" class="h-1 w-36 bg-blue-200" runat="server"></div>
                        </div>

                        <!--Packed-->
                        <div class="relative flex flex-col justify-center items-center">

                            <div id="packed" class="relative rounded-full transition duration-500 ease-in-out h-[36px] w-[36px] py-3 flex justify-center items-center bg-blue-200" runat="server">
                                <i id="checkPacked" class="fa-solid fa-check text-white hidden" runat="server"></i>
                                <i id="crossPacked" class="fa-solid fa-x text-white hidden" runat="server"></i>
                            </div>
                            <div class="absolute flex flex-col justify-center items-center" style="bottom: -75px">

                                <asp:Image ID="Image1" CssClass="w-[36px] h-[36px]" runat="server" ImageUrl="~/Client/Profile/images/package.gif" />
                                <div class="text-center text-xs font-medium uppercase text-blue-500">Packed</div>
                            </div>
                        </div>

                        <!--Line-->
                        <div class="shrink-0 ">
                            <div id="line2" class="h-1 w-36 bg-blue-200" runat="server"></div>
                        </div>

                        <!--On The Road-->
                        <div class="relative flex flex-col justify-center items-center">

                            <div id="onTheRoad" class="relative rounded-full transition duration-500 ease-in-out h-[36px] w-[36px] py-3 bg-blue-200 flex justify-center items-center" runat="server">
                                <i id="checkOTR" class="fa-solid fa-check text-white hidden" runat="server"></i>

                                <i id="crossOTR" class="fa-solid fa-x text-white hidden" runat="server"></i>
                            </div>
                            <div class="absolute w-[100px] flex flex-col justify-center items-center" style="bottom: -75px">

                                <asp:Image ID="Image2" CssClass="w-[36px] h-[36px]" runat="server" ImageUrl="~/Client/Profile/images/truck.gif" />
                                <div class="text-center text-xs font-medium uppercase text-blue-500">On The Road</div>
                            </div>
                        </div>

                        <!--Line-->
                        <div class="shrink-0 ">
                            <div id="line3" class="h-1 w-36 bg-blue-200" runat="server"></div>
                        </div>


                        <!--Delivered-->
                        <div class="relative flex flex-col justify-center items-center">

                            <div id="delivered" class="relative rounded-full transition duration-500 ease-in-out h-[36px] w-[36px] py-3 bg-blue-200 flex justify-center items-center" runat="server">
                                <i id="checkDelivered" class="fa-solid fa-check text-white hidden" runat="server"></i>

                                <i id="crossDelivered" class="fa-solid fa-x text-white hidden" runat="server"></i>
                            </div>
                            <div class="absolute flex flex-col justify-center items-center" style="bottom: -75px">

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
        <div class="flex flex-col gap-5 w-full">

            <div class="flex flex-row w-full gap-5">
                <!-- order -->
                <div class="flex-1">

                    <div class="border border-gray-300 bg-white shadow shadow-xl rounded-2xl p-6 flex-1 flex flex-col gap-5 font-semibold">
                        <div class="flex flex-row items-center gap-2 ">
                            <div class="text-lg text-gray-900 font-bold">Order</div>
                        </div>
                        <div class="grid grid-cols-6">
                            <div class="col-span-1 flex items-center">
                                <i class="fa-solid fa-hashtag"></i>
                            </div>
                            <div class="col-span-2 font-semibold">
                                <span>ID</span>
                            </div>
                            <div class="col-span-3 text-right">
                                <asp:Label ID="lblOrderID" runat="server" Text="Label"></asp:Label>
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
                                <asp:Label ID="lblOrderDate" runat="server" Text="Label"></asp:Label>
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
                                <asp:Label ID="lblOrderStatus" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- payment -->
                <div class="flex-1">

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
                                <asp:Label ID="lblPaymentID" runat="server" Text="Label"></asp:Label>
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
                                <asp:Label ID="lblPaymentDate" runat="server" Text="Label"></asp:Label>
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
                                <asp:Label ID="lblPaymentAmount" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Address -->
                <div class="border border-gray-300 shadow shadow-xl rounded-2xl p-6 flex-1 flex flex-col gap-5 font-semibold bg-white">
                    <div class="flex flex-row items-center gap-2 ">
                        <div class="text-lg text-gray-900 font-bold">Order Notes</div>
                    </div>

                    <div class="grid grid-cols-6 ">
                        <div class="col-span-6 text-gray-700 flex flex-col gap-1">
                            <asp:Label ID="lblOrderNote" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>

                </div>

            </div>

            <div class="flex flex-row w-full gap-5">
                <!-- Address -->
                <div class="border border-gray-300 shadow shadow-xl rounded-2xl p-6 flex-1 flex flex-col gap-5 font-semibold bg-white">
                    <div class="flex flex-row items-center gap-2 ">
                        <div class="text-lg text-gray-900 font-bold">Shipping Address</div>
                    </div>

                    <div class="grid grid-cols-6 ">
                        <div class="col-span-6 text-gray-700 flex flex-col gap-1">
                            <asp:Label ID="lblShippingAddress1" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="lblShippingAddress2" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="lblShippingAddress3" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="lblShippingAddress4" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>

                </div>
                <!-- Address -->
                <div class="border border-gray-300 shadow shadow-xl rounded-2xl p-6 flex-1 flex flex-col gap-5 font-semibold bg-white">
                    <div class="flex flex-row items-center gap-2 ">
                        <div class="text-lg text-gray-900 font-bold">Billing Address</div>
                    </div>

                    <div class="grid grid-cols-6 ">
                        <div class="col-span-6 text-gray-700 flex flex-col gap-1">
                            <asp:Label ID="lblBillingAddress1" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="lblBillingAddress2" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="lblBillingAddress3" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="lblBillingAddress4" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>

                </div>
            </div>
            <!-- Order List -->
            <div class="w-full">

                <div class="border border-gray-300 bg-white shadow shadow-xl rounded-2xl flex-1 flex flex-col font-semibold">
                    <div class="flex flex-row items-center gap-2 px-5 pt-6 pb-2">
                        <div class="text-lg text-gray-900 font-bold">Order List</div>
                    </div>
                    <div class="grid grid-cols-7 p-4 bg-gray-200 font-bold text-gray-600 text-sm flex items-center px-5">
                        <div class="col-span-3">
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
                        <div class="col-span-1">
                            <p>Action</p>
                        </div>
                    </div>
                    <asp:ListView ID="lvOrderList" runat="server" OnItemCommand="orderListView_ItemCommand">
                        <LayoutTemplate>

                            <div id="itemPlaceholder" runat="server"></div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="grid grid-cols-7 flex items-center px-5 py-4 text-sm">
                                <div class="col-span-3 flex items-center gap-5">
                                    <asp:Image ID="imgProduct" runat="server" AlternateText="Image" ImageUrl='<%# Eval("imagePath") %>' class="h-16 w-16 rounded-md" />
                                    <span>
                                        <%# Eval("productName") %> <%# Eval("variantName") %> 
                                    </span>
                                </div>
                                <div class="col-span-1">RM <%# Eval("price") %></div>
                                <div class="col-span-1 pl-6">
                                    <span class="text-center">
                                        <%# Eval("quantity") %>
                                    </span>
                                </div>
                                <div class="col-span-1">
                                    RM <%# Eval("amount") %>
                                </div>
                                <div>
                                    <asp:LinkButton ID="lbReview" runat="server" CssClass="font-semibold text-base bg-blue-700 text-white py-1 px-4 w-[70%] rounded-lg flex items-center gap-2 " CommandArgument='<%# Eval("variantID") %>' CommandName="reviewClick"><i class="fa-regular fa-pen-to-square text-xs"></i><span>Rate</span></asp:LinkButton>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:ListView>
                </div>


            </div>

        </div>
        <asp:Panel ID="popUpDelete" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center ">
            <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
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
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Admin/Orders/Images/trash.gif" AlternateText="trashcan" CssClass="w-28 h-28 " />

                    </div>
                    <p class="bold text-lg break-normal text-center">Are you sure you want to cancel this following Order?</p>
                    <div class="flex gap-10 text-white font-bold">

                        <asp:Button ID="btnCancelDelete" runat="server" Text="No" CssClass="bg-gray-500 p-2 p px-6 rounded-lg cursor-pointer" OnClick="btnCancelDelete_Click" />
                        <asp:Button ID="btnConfirmDelete" OnClick="btnConfirmDelete_click" runat="server" Text="Yes" CssClass="bg-red-500 p-2 px-6 rounded-lg cursor-pointer" />
                    </div>
                </div>
            </div>

        </asp:Panel>
        <asp:Panel ID="refund" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center ">
            <asp:LinkButton ID="LinkButton2" runat="server">LinkButton</asp:LinkButton>
            <!-- Modal content -->
            <div class="popUp-content w-1/3 h-fit flex flex-col bg-white p-5 rounded-xl flex flex-col gap-3 drop-shadow-lg">

                <div class="w-full h-fit justify-center flex p-0">
                    <p class="text-2xl text-cyan-500 font-bold text-center">ORDER CANCELLED</p>

                </div>
                <div class="flex flex-col justify-center items-center gap-5">

                    <div style="font-size: 64px">
                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Client/Profile/images/save-money.gif" AlternateText="trashcan" CssClass="w-28 h-28 " />

                    </div>
                    <p class="bold text-lg break-normal text-center">Order cancelled successfully.<br></br>Don't worry, the refund is on the way!</p>
                    <div class="flex gap-10 text-white font-bold">

                        <asp:Button ID="btnDoneCancel" OnClick="btnDoneCancel_Click" runat="server" Text="Confirm" CssClass="bg-cyan-500 p-2 px-6 rounded-lg cursor-pointer"  />
                    </div>
                </div>
            </div>

        </asp:Panel>
    </div>

</asp:Content>
