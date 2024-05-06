<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="PaymentPage.aspx.cs" Inherits="webAssignment.Client.Checkout.PaymentPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex max-w-6xl my-16 mx-auto gap-6">

        <!-- Payment Option -->
        <div class="w-4/5">

            <div class="flex flex-col p-4 gap-6 border border-gray-300 shadow-xl rounded-lg">
                <span class="font-bold text-xl">Payment Option</span>
                <hr />
                <div class="">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="w-full ">
                        <ContentTemplate>
                            <div class="flex justify-around w-full mb-4 gap-8">
                                <asp:Button ID="btnCard" runat="server" Text="Credit Card" OnClick="btnCard_Click" CssClass="bg-blue-700 text-white p-4 rounded-xl cursor-pointer flex-1 font-semibold" />
                                <asp:Button ID="btnBank" runat="server" Text="Bank FPX" OnClick="btnBank_Click" CssClass="bg-blue-700 text-white p-4 rounded-xl cursor-pointer flex-1 font-semibold" />
                                <asp:Button ID="btnCOD" runat="server" Text="Cash On Delivery" OnClick="btnCOD_Click" CssClass="bg-blue-700 text-white p-4 rounded-xl cursor-pointer flex-1 font-semibold" />
                            </div>

                            
                            <asp:Panel ID="pnlCard" runat="server" CssClass="border border-gray-300 p-6 rounded-lg flex flex-col gap-8" Visible="false">
                                <!-- Content for Credit Card Payment -->
                                <h3 class="font-bold text-xl">Credit Card Payment</h3>
                                <!-- Add ASP.NET elements for credit card payment here -->
                                <div class="flex flex-col gap-5 ">

                                    <div class="flex flex-col  gap-2 w-full">
                                        <asp:Label ID="lblCardNumber" runat="server" Text="Card Number:" CssClass="font-semibold "></asp:Label>
                                        <asp:TextBox ID="txtCardNumber" runat="server" class="rounded-lg flex-1 p-2 bg-gray-200 focus:shadow focus:shadow-lg focus:bg-white transition-colors duration-200"></asp:TextBox>
                                    </div>
                                    <div class="flex gap-8">

                                        <div class="flex flex-col  gap-2 w-full">
                                            <asp:Label ID="lblExpirationDate" runat="server" Text="Expiration Date:" CssClass="font-semibold "></asp:Label>
                                            <div class="flex">
                                                <asp:DropDownList ID="ddlExpirationMonth" runat="server" class="rounded-lg flex-1 p-2 bg-gray-200 focus:shadow focus:shadow-lg focus:bg-white transition-colors duration-200">
                                                </asp:DropDownList>
                                                <span class="text-xl p-2 px-4">/ </span>
                                                <asp:DropDownList ID="ddlExpirationYear" runat="server" class="rounded-lg flex-1 p-2 bg-gray-200 focus:shadow focus:shadow-lg focus:bg-white transition-colors duration-200">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="flex flex-col  gap-2 w-full">
                                            <asp:Label ID="lblCVV" runat="server" Text="CVV:" CssClass="font-semibold "></asp:Label>
                                            <asp:TextBox ID="txtCVV" runat="server" class="rounded-lg flex-1 p-2 bg-gray-200 focus:shadow focus:shadow-lg focus:bg-white transition-colors duration-200"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>



                            <asp:Panel ID="pnlBank" runat="server" CssClass="border border-gray-300 p-6 rounded-lg flex flex-col gap-8" Visible="false">
                                <!-- Content for Bank FPX Payment -->
                                <h3 class="font-bold text-xl">Bank FPX Payment</h3>
                                <!-- Add ASP.NET elements for bank FPX payment here -->
                                <div class="flex gap-3 items-center">
                                    <asp:Label ID="lblBankFPXInfo" runat="server" Text="Select Bank :" CssClass="font-semibold w-1/5"></asp:Label>
                                    <asp:DropDownList ID="ddlBank" runat="server" class="rounded-lg flex-1 p-2 bg-gray-200 focus:shadow focus:shadow-lg focus:bg-white transition-colors duration-200">
                                        <asp:ListItem Text="Public Bank" Value="public-bank"></asp:ListItem>
                                        <asp:ListItem Text="CIMB Bank" Value="cimb-bank"></asp:ListItem>
                                        <asp:ListItem Text="MayBank" Value="may-bank"></asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                 <div class="flex gap-3 items-center">
                                    <asp:Label ID="lblBankFPXNumber" runat="server" Text="Bank Number :" CssClass="font-semibold w-1/5"></asp:Label>
                                     <asp:TextBox ID="txtBankNumber" class="rounded-lg flex-1 p-2 bg-gray-200 focus:shadow focus:shadow-lg focus:bg-white transition-colors duration-200" runat="server"></asp:TextBox>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlCOD" runat="server" CssClass="border border-gray-300 p-6 rounded-lg flex flex-col gap-6" Visible="false">
                                <h3 class="font-bold text-xl">Cash On Delivery</h3>
                                <div class="flex flex-col gap-2">
                                    <p>Thank you for choosing Cash on Delivery as your payment method. Here's what you need to know:</p>
                                    <ul class="list-disc pl-5">
                                        <li>Please keep exact change ready for the delivery person.</li>
                                        <li>You will be required to pay the total order amount in cash upon delivery.</li>
                                        <li>Ensure someone is available at the delivery address to receive the order.</li>
                                    </ul>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCard" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnBank" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnCOD" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
        <!-- Right Hand Side Product List -->
        <div class="w-2/6">
            <div class="border border-gray-300 shadow-xl rounded-lg p-4 flex flex-col gap-4">

                <asp:ListView ID="lvCheckoutProduct" runat="server">
                    <LayoutTemplate>
                        <table class="orders-table w-full">
                            <p class="text-gray-900 font-bold text-xl">Order Summary</p>
                            <tr id="itemPlaceHolder" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="mb-2 grid grid-cols-4 items-center ">
                            <td class="col-span-1">
                                <asp:Image ID="imgProduct" runat="server" AlternateText="Image" ImageUrl='<%# Eval("imagePath", "{0}") %>' class="h-14 w-14 " />
                            </td>
                            <td class="col-span-3 flex flex-col">
                                <span class="font-semibold overflow-hidden whitespace-nowrap overflow-ellipsis"><%# Eval("productName") %>
                                </span>
                                <span class="font-medium overflow-hidden whitespace-nowrap overflow-ellipsis"><%# Eval("variantName") %>
                                </span>
                                <span class="text-gray-700">
                                    <%# Eval("quantity") %> x <span class="text-blue-700 font-bold">RM <%# Eval("price") %></span>
                                </span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>

                <div class="flex flex-col gap-2 font-semibold text-gray-500">
                    <div class="flex justify-between">
                        <span>Sub-total</span>
                        <asp:Label ID="lblCartSubtotal" runat="server" Text="RM 0.00"></asp:Label>
                    </div>
                    <div class="flex justify-between">
                        <span>Shipping</span>
                        <asp:Label ID="lblCartShipping" runat="server" Text="Free"></asp:Label>
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
                    
                    <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" class="cursor-pointer bg-blue-700 text-white font-semibold text-sm h-10 rounded-lg mt-4" OnClick="btnPlaceOrder_Click"  />
                </div>
            </div>
        </div>
        <!--Display Panel after place order-->
         <asp:Panel ID="popUpOrderPlaced" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center ">
        <!-- Modal content -->
        <div class="popUp-content w-1/3 h-fit flex flex-col bg-white p-5 rounded-xl flex flex-col gap-3 drop-shadow-lg">

            <div class=" w-full h-fit justify-center flex p-0">
                <p class="text-2xl text-blue-600 font-bold text-center">ORDER PLACED</p>

            </div>
            <div class="flex flex-col justify-center items-center gap-5">

                <div style="font-size: 64px">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Client/Checkout/image/thanks.gif" AlternateText="trashcan" CssClass="w-28 h-28 " />

                </div>

                <p class="bold text-xl break-normal text-center">Thank you for purchasing our product!</p>
                <div class=" ">
                <asp:LinkButton ID="closePopUp" runat="server" OnClick="closePopUp_Click" class="border border-gray-500 px-3 py-2 rounded-lg flex gap-2 items-center">
                     <i class=" fa-solid fa-house"></i><span class="font-semibold">Back to Home</span>
                </asp:LinkButton>
                </div>
            </div>
        </div>

    </asp:Panel>
    </div>
</asp:Content>
