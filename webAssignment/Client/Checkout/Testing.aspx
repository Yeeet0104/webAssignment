<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="Testing.aspx.cs" Inherits="webAssignment.Client.Checkout.Testing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex max-w-6xl my-16 mx-auto gap-6">

        <!-- Payment Option -->
        <div class="w-4/6">

            <div class="flex flex-col p-4 gap-6 border border-gray-300 shadow-xl rounded-lg">
                <span class="font-bold text-xl">Payment Option</span>
                <hr />
                <div class="">
                    <%--<div class="flex flex-col w-1/3 items-center gap-2">
                        <i class="fa-solid fa-money-bill-wave"></i>
                        <span class="font-semibold">Cash On Delivery</span>
                        <asp:RadioButton ID="rdbCOD" runat="server" GroupName="PaymentOption" />
                    </div>
                    <div class="flex flex-col w-1/3 items-center gap-2">
                        <i class="fa-solid fa-credit-card"></i>
                        <span class="font-semibold">Card</span>
                        <asp:RadioButton ID="rbdCard" runat="server" GroupName="PaymentOption" />
                    </div>
                    <div class="flex flex-col w-1/3 items-center gap-2">
                        <i class="fa-solid fa-wallet"></i>
                        <span class="font-semibold">E-wallet</span>
                        <asp:RadioButton ID="rbdWallet" runat="server" GroupName="PaymentOption" />
                    </div>--%>

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="w-full ">
                        <ContentTemplate>
                            <div class="flex justify-around w-full mb-4">
                                <asp:Button ID="btnCard" runat="server" Text="Credit Card" OnClick="btnCard_Click" CssClass="bg-blue-500 text-white px-4 py-2 rounded cursor-pointer" />
                                <asp:Button ID="btnBank" runat="server" Text="Bank FPX" OnClick="btnBank_Click" CssClass="bg-blue-500 text-white px-4 py-2 rounded cursor-pointer" />
                                <asp:Button ID="btnCOD" runat="server" Text="Cash On Delivery" OnClick="btnCOD_Click" CssClass="bg-blue-500 text-white px-4 py-2 rounded cursor-pointer" />
                            </div>

                            <asp:Panel ID="Panel1" runat="server" CssClass="border border-gray-300 p-6 rounded flex flex-col gap-8" Visible="false">
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
                                                    <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                                <span class="text-xl p-2 px-4">/ </span>
                                                <asp:DropDownList ID="ddlExpirationYear" runat="server" class="rounded-lg flex-1 p-2 bg-gray-200 focus:shadow focus:shadow-lg focus:bg-white transition-colors duration-200">
                                                    <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                                    <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                                                    <asp:ListItem Text="2026" Value="2026"></asp:ListItem>
                                                    <asp:ListItem Text="2027" Value="2027"></asp:ListItem>
                                                    <asp:ListItem Text="2028" Value="2028"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="flex flex-col  gap-2 w-full">
                                            <asp:Label ID="lblCVV" runat="server" Text="CVV:" CssClass="font-semibold "></asp:Label>
                                            <asp:TextBox ID="txtCVV" runat="server" class="rounded-lg flex-1 p-2 bg-gray-200 focus:shadow focus:shadow-lg focus:bg-white transition-colors duration-200"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <!-- Add more elements as needed -->
                            </asp:Panel>

                            <asp:Panel ID="Panel2" runat="server" CssClass="border border-gray-300 p-8 rounded " Visible="false">
                                <!-- Content for Bank FPX Payment -->
                                <h3>Bank FPX Payment</h3>
                                <!-- Add ASP.NET elements for bank FPX payment here -->
                                <asp:Label ID="lblBankFPXInfo" runat="server" Text="Select Bank:"></asp:Label>
                                <asp:DropDownList ID="ddlBank" runat="server">
                                    <asp:ListItem Text="Public Bank" Value="public-bank"></asp:ListItem>
                                    <asp:ListItem Text="CIMB Bank" Value="cimb-bank"></asp:ListItem>
                                    <asp:ListItem Text="MayBank" Value="may-bank"></asp:ListItem>

                                </asp:DropDownList>
                            </asp:Panel>

                            <asp:Panel ID="Panel3" runat="server" CssClass="border border-gray-300 p-8 rounded" Visible="false">
                                <p>
                                    Thank you for choosing Cash on Delivery as your payment method. Here's what you need to know:
                                </p>
                                <ul class="list-disc pl-5">
                                    <li>Please keep exact change ready for the delivery person.</li>
                                    <li>You will be required to pay the total order amount in cash upon delivery.</li>
                                    <li>Ensure someone is available at the delivery address to receive the order.</li>
                                </ul>
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
                        <tr class="grid grid-cols-3 items-center">
                            <td class="col-span-1">
                                <asp:Image ID="imgProduct" runat="server" AlternateText="Image" ImageUrl='<%# Eval("ProductImageURL", "{0}") %>' class="h-14 w-14 " />
                            </td>
                            <td class="col-span-2 flex flex-col">
                                <span class="font-semibold"><%# Eval("ProductName") %></span>
                                <span class="text-gray-700">
                                    <%# Eval("Quantity") %> x <span class="text-blue-700 font-bold"><%# Eval("Price", "{0:C}") %></span>
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
                    <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" class="cursor-pointer bg-blue-700 text-white font-semibold text-sm h-10 rounded-lg mt-4" PostBackUrl="~/Client/Checkout/Testing.aspx" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
