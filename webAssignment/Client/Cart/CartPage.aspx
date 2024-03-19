<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="CartPage.aspx.cs" Inherits="webAssignment.Client.Cart.CartPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex max-w-7xl my-16 mx-auto gap-6">
        <div class="h-full w-4/5 flex flex-col border border-gray-300">
            <p class="font-bold text-gray-700 p-3">Shopping Cart</p>
            <div class="flex w-full pl-3 h-10 bg-gray-200 items-center font-bold text-gray-500 text-sm">
                <span class="w-3/5">Products</span>
                <span class="w-1/5">Price</span>
                <span class="w-1/5">Quantity</span>
                <span class="w-1/5">Subtotal</span>
            </div>
            <!--Product List-->
            <div class="flex w-full pl-3 py-2 items-center font-semibold">
                <div class="w-3/5 flex items-center gap-4">
                    <i class="fa-regular fa-circle-xmark"></i>
                    <img src="image/iphone_15.jpg" class="w-auto h-20" />
                    <span>IPhone 15</span>
                </div>
                <span class="w-1/5">RM 20.00</span>
                <div class="w-1/5 flex items-center gap-4">
                    <i class="fa-solid fa-minus"></i>
                    <span>01</span>
                    <i class="fa-solid fa-plus"></i>
                </div>
                <div class="w-1/5">RM 20.00</div>
            </div>
        </div>
        <div class="flex flex-col h-full w-2/5">
            <!--Total-->
            <div class="border border-gray-300 w-full p-4 flex flex-col gap-4 font-semibold text-gray-500">
                <span class="text-gray-700 font-bold text-lg">Cart Total</span>
                <div class="flex justify-between">
                    <span>Sub-total</span>
                    <span>RM 20.00</span>
                </div>
                <div class="flex justify-between">
                    <span>Shipping</span>
                    <span>Free</span>
                </div>
                <div class="flex justify-between">
                    <span>Discount</span>
                    <span>RM 0.00</span>
                </div>
                <div class="flex justify-between">
                    <span>Tax</span>
                    <span>RM 1.00</span>
                </div>
                <hr />
                <div class="text-gray-700 font-bold flex justify-between">
                    <span>Total</span>
                    <span>RM 21.00</span>
                </div>
                <asp:Button ID="btn_proceed_checkout" runat="server" Text="PROCEED TO CHECKOUT" class="mt-2 text-white bg-blue-700 rounded-lg w-full p-2 font-semibold text-sm cursor-pointer" />
            </div>
            <div class="border border-gray-300 w-full p-4 flex flex-col gap-4 font-semibold text-gray-500 mt-2">
                <span class="text-gray-700 font-bold text-lg">Coupon Code</span>
                <asp:TextBox ID="txt_coupon" runat="server" class="my-2 text-black border border-gray-400 rounded-md py-2 px-4 bg-transparent max-w-sm" placeholder="Coupon Code"></asp:TextBox>
                <asp:Button ID="btn_apply_coupon" runat="server" Text="APPLY COUPON" class="border border-blue-700 rounded-lg w-36 text-sm text-blue-700 font-bold p-2 cursor-pointer" />
            </div>
        </div>
    </div>
</asp:Content>
