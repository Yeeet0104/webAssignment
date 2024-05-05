<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="CartPage.aspx.cs" Inherits="webAssignment.Client.Cart.CartPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="max-w-7xl my-16 mx-auto">
        <div class="w-full flex gap-6">

            <!-- Products  -->
            <div class="h-full w-[75%] flex flex-col border border-gray-200 shadow-xl rounded-lg">
                <table class="orders-table w-full">
                    <p class="text-xl text-gray-900 font-bold p-4">Shopping Cart</p>
                    <div class="grid grid-cols-5 p-4 bg-gray-200 font-bold text-gray-600 text-sm flex items-center">
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
                    <asp:ListView ID="lvCartProduct" runat="server" OnItemCommand="cartListView_ItemCommand">
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
                            <tr id="itemPlaceholder" runat="server"></tr>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <%--<tr class="grid grid-cols-5 flex items-center p-4 font-semibold">
                                <td class="col-span-2 flex items-center gap-5">
                                    <asp:Image ID="imgProduct" runat="server" AlternateText="Image" ImageUrl='<%# Eval("ProductImageURL", "{0}") %>' class="h-16 w-16 rounded-md" />
                                    <span>
                                        <%# Eval("ProductName") %>
                                    </span>
                                </td>
                                <td class="col-span-1"><%# Eval("Price", "{0:C}") %></td>
                                <td class="col-span-1 flex items-center gap-4">
                                    <asp:LinkButton ID="btn_reduce_qty" runat="server">                    
                             <i class="fa-solid fa-minus cursor-pointer bg-gray-500 hover:bg-blue-500 p-1 rounded-lg text-white transition duration-200"></i>
                                    </asp:LinkButton>
                                    <span>
                                        <%# Eval("Quantity") %>
                                    </span>
                                    <asp:LinkButton ID="btn_increase_qty" runat="server">
                                <i class="fa-solid fa-plus cursor-pointer bg-gray-500 hover:bg-blue-500 p-1 rounded-lg text-white transition duration-200"></i>
                                    </asp:LinkButton>
                                </td>
                                <td class="col-span-1 flex justify-between items-center">
                                    <%# Eval("Subtotal", "{0:C}") %>
                                    <i class="fa-solid fa-trash-can mr-2 cursor-pointer hover:bg-red-500 p-2 rounded-2xl hover:text-white transition duration-200"></i>
                                </td>
                            </tr>

                        </ItemTemplate>--%>
                            <tr class="grid grid-cols-5 flex items-center p-4 font-semibold">
                                <td class="col-span-2 flex items-center gap-5">
                                    <asp:Image ID="imgProduct" runat="server" AlternateText="Image" ImageUrl='<%# Eval("imagePath", "{0}") %>'  class="h-16 w-16 rounded-md" />

                                    <span class="overflow-hidden whitespace-nowrap overflow-ellipsis pr-1">
                                        <%# Eval("productName") %>  <%# Eval("variantName") %>
                                    </span>
                                </td>
                                <td class="col-span-1">RM <%# Eval("price") %></td>
                                <td class="col-span-1 flex items-center gap-4">
                                    <asp:LinkButton ID="btn_reduce_qty" runat="server" CommandArgument='<%# Eval("variantId") %>' CommandName="reduceQty">                    
                             <i class="fa-solid fa-minus cursor-pointer bg-gray-500 hover:bg-blue-500 p-1 rounded-lg text-white transition duration-200"></i>
                                    </asp:LinkButton>
                                    <span>
                                        <%# Eval("quantity") %>                                    

                                    </span>
                                    <asp:LinkButton ID="btn_increase_qty" runat="server" CommandArgument='<%# Eval("variantId") %>' CommandName="increaseQty">
                                <i class="fa-solid fa-plus cursor-pointer bg-gray-500 hover:bg-blue-500 p-1 rounded-lg text-white transition duration-200"></i>
                                    </asp:LinkButton>
                                </td>
                                <td class="col-span-1 flex justify-between items-center">RM <%# Convert.ToDecimal(Eval("price")) * Convert.ToInt32(Eval("quantity")) %>
                                    <asp:LinkButton ID="btn_delete" runat="server" CommandArgument='<%# Eval("variantId") %>' CommandName="deleteItem">
                                    <i class="fa-solid fa-trash-can mr-2 cursor-pointer hover:bg-red-500 p-2 rounded-2xl hover:text-white transition duration-200"></i>
                                    </asp:LinkButton>
                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:ListView>
                    <!--Display when the cart is empty-->
                    <tr class="">
                        <td id="cartEmptyMsg" runat="server" class="hidden">
                            <div class="flex flex-col justify-center items-center p-10 gap-6 text-gray-700 text-xl font-semibold">
                                <asp:Image ID="imgCryingKermit" runat="server" ImageUrl="~/Client/Cart/images/cryingKermit.png" class="max-h-20 w-auto" />
                                <span>Oh noooo, your cart is empty :(</span>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="flex flex-col h-full w-[25%] gap-8">

                <!-- Coupon Section -->
                <div class="border border-gray-200 shadow-md w-full p-4 flex flex-col gap-4 font-semibold text-gray-700 rounded-xl">
                    <span class="text-gray-700 font-bold text-lg">Coupon Code</span>

                    <asp:TextBox ID="txtCoupon" runat="server" class="text-black border border-gray-400 rounded-md py-2 px-4 bg-transparent max-w-sm" placeholder="Coupon Code"></asp:TextBox>

                    <asp:Button ID="btnApplyCoupon" runat="server" Text="APPLY COUPON" class="border border-blue-700 rounded-lg w-36 text-sm text-blue-700 font-bold p-2 cursor-pointer" OnClick="btnApplyCoupon_Click" />
                </div>
                <!--Cart Total-->
                <div class="border border-gray-200 shadow-xl w-full p-4 flex flex-col gap-4 font-semibold text-gray-500 rounded-lg">
                    <span class="text-gray-700 font-bold text-lg">Cart Total</span>
                    <div class="flex justify-between">
                        <span>Sub-total</span>
                        <asp:Label ID="lblCartSubtotal" runat="server" Text="RM 0.00"></asp:Label>
                    </div>
                    <div class="flex justify-between">
                        <span>Shipping</span>
                        <asp:Label ID="lblCartShipping" runat="server" Text="RM 0.00"></asp:Label>
                    </div>
                    <div class="flex justify-between">
                        <span>Discount</span>
                        <asp:Label ID="lblCartDiscount" runat="server" Text="RM 0.00"></asp:Label>
                    </div>
                    <div class="flex justify-between">
                        <span>Tax</span>
                        <asp:Label ID="lblCartTax" runat="server" Text="RM 0.00"></asp:Label>
                    </div>
                    <hr />
                    <div class="text-gray-700 font-bold flex justify-between">
                        <span>Total</span>
                        <asp:Label ID="lblCartTotal" runat="server" Text="RM 0.00"></asp:Label>
                    </div>

                    <asp:Button ID="btnProceedCheckout" runat="server" Text="Proceed To Checkout" CssClass="mt-2 text-white bg-blue-700 rounded-lg w-full p-2 font-semibold text-sm cursor-pointer text-center" OnClick="btnProceedCheckout_Click" />
                </div>

            </div>
        </div>
        <div>
        </div>
    </div>
</asp:Content>
