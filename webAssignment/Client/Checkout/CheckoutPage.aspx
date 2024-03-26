﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="CheckoutPage.aspx.cs" Inherits="webAssignment.Client.Checkout.CheckoutPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex max-w-6xl my-16 mx-auto gap-6">
        <div class="border border-gray-400 rounded-lg w-4/5 flex flex-col gap-3 text-base text-gray-704 p-4">
            <span class="text-gray-900 font-bold text-xl">Billing Information</span>
            <div class="flex gap-3">
                <span class="w-1/2 font-semibold">Username</span>
                <span class="w-1/2 font-semibold">Company Name<span class="text-gray-400"> (Optional)</span></span>
            </div>
            <div class="flex justify-between gap-3">
                <div class="flex w-1/2 gap-3">
                    <asp:TextBox class="border border-gray-300 w-1/2 p-2" ID="txtFirstName" runat="server" placeholder="First name"></asp:TextBox>
                    <asp:TextBox class="border border-gray-300 w-1/2 p-2" ID="txtLastName" runat="server" placeholder="Last name"></asp:TextBox>
                </div>
                <asp:TextBox class="border border-gray-300 w-1/2 p-2" ID="txtCompanyName" runat="server"></asp:TextBox>
            </div>
            <span class="font-semibold">Address</span>
            <asp:TextBox class="border border-gray-300 p-2" ID="txtAddress" runat="server"></asp:TextBox>
            <div class="flex gap-3 font-semibold">
                <span class="w-1/4">Country</span>
                <span class="w-1/4">Region/State</span>
                <span class="w-1/4">City</span>
                <span class="w-1/4">Zip Code</span>
            </div>
            <div class="flex gap-3">
                <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlCountry" runat="server"></asp:DropDownList>
                <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlState" runat="server"></asp:DropDownList>
                <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlCity" runat="server"></asp:DropDownList>
                <asp:TextBox class="w-1/4 border border-gray-300 p-2" ID="txtZipCode" runat="server"></asp:TextBox>
            </div>
            <div class="flex gap-3 font-semibold">
                <span class="w-1/2">Email</span>
                <span class="w-1/2">Phone Number</span>
            </div>
            <div class="flex gap-3">
                <asp:TextBox class="w-1/2 border border-gray-300 p-2" ID="txtEmail" runat="server"></asp:TextBox>
                <asp:TextBox class="w-1/2 border border-gray-300 p-2" ID="txtPhoneNumber" runat="server"></asp:TextBox>
            </div>
            <div class="flex gap-3">
                <asp:CheckBox ID="cbxShip" runat="server" OnCheckedChanged="cbxShip_CheckedChanged" AutoPostBack="true" />
                <span class="font-semibold">Ship into different address</span>
            </div>


            <!-- If shipping address diff -->
            <div class="hidden" id="shippingAddressDetails" runat="server">
                <div class="flex flex-col gap-2">
                    <span class="text-gray-900 font-bold text-xl mt-8 mb-2">Shipping Information</span>
                    <span class="font-semibold">Address</span>
                    <asp:TextBox class="border border-gray-300 p-2" ID="txtShippingAddress" runat="server"></asp:TextBox>
                    <div class="flex gap-3 font-semibold">
                        <span class="w-1/4">Country</span>
                        <span class="w-1/4">Region/State</span>
                        <span class="w-1/4">City</span>
                        <span class="w-1/4">Zip Code</span>
                    </div>
                    <div class="flex gap-3">
                        <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlShippingCountry" runat="server"></asp:DropDownList>
                        <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlShippingState" runat="server"></asp:DropDownList>
                        <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlShippingCity" runat="server"></asp:DropDownList>
                        <asp:TextBox class="w-1/4 border border-gray-300 p-2" ID="txtShippingZipCode" runat="server"></asp:TextBox>
                    </div>

                </div>
            </div>





            <div class="flex flex-col my-8 p-4 gap-6 border border-gray-300">
                <span class="font-bold">Payment Option</span>
                <hr />
                <div class="flex">
                    <div class="flex flex-col w-1/3 items-center gap-2">
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
                    </div>
                </div>
            </div>
            <span class="font-bold">Additional Information</span>
            <span class="font-semibold">Order Notes<span class="text-gray-400"> (Optional)</span></span>
            <asp:TextBox class="p-2 border border-gray-300 resize-none" TextMode="MultiLine" Rows="4" ID="txtOrderNote" runat="server" placeholder="Notes about your order, e.g. spcial note for delivery"></asp:TextBox>
        </div>
        <div class="w-2/6">
            <div class="border border-gray-400 rounded-lg p-4 flex flex-col gap-4">

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
                    <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" class="bg-blue-700 text-white font-semibold text-sm h-10 rounded-lg mt-4" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>