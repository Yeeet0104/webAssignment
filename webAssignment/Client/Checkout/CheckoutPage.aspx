<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="CheckoutPage.aspx.cs" Inherits="webAssignment.Client.Checkout.CheckoutPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex max-w-6xl my-16 mx-auto gap-6">
        <div class="border border-gray-300 shadow-xl rounded-lg w-4/5 flex flex-col gap-3 text-base p-4">
            <span class="text-gray-900 font-bold text-xl">Billing Information</span>
            <div class="flex gap-3">
                <span class="w-1/2 font-semibold">Reciever Details</span>
            </div>
            <div class="flex w-full gap-3">
                <asp:TextBox class="border border-gray-300 w-1/2 p-2" ID="txtFirstName" runat="server" placeholder="First name"></asp:TextBox>
                <asp:TextBox class="border border-gray-300 w-1/2 p-2" ID="txtLastName" runat="server" placeholder="Last name"></asp:TextBox>
            </div>

            <div class="flex gap-3">
                <asp:TextBox class="w-full border border-gray-300 p-2" ID="txtPhoneNumber" runat="server" placeholder="Phone Number"></asp:TextBox>
            </div>

            <span class="font-semibold">Address</span>
            <asp:TextBox class="border border-gray-300 p-2" ID="txtAddressLine1" placeholder="Address Line 1" runat="server"></asp:TextBox>
            <asp:TextBox class="border border-gray-300 p-2" ID="txtAddressLine2" placeholder="Address Line 2" runat="server"></asp:TextBox>
            <div class="flex gap-3 font-semibold">
                <span class="w-1/4">Country</span>
                <span class="w-1/4">Region/State</span>
                <span class="w-1/4">City</span>
                <span class="w-1/4">Zip Code</span>
            </div>
            <div class="flex gap-3">
                <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                    <asp:ListItem Value="null">-Default-</asp:ListItem>
                    <asp:ListItem Value="MY">Malaysia</asp:ListItem>
                    <asp:ListItem Value="SG">Singapore</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                    <asp:ListItem Value="null">-Default-</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlCity" runat="server">
                    <asp:ListItem Value="null">-Default-</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox class="w-1/4 border border-gray-300 p-2" ID="txtZipCode" runat="server"></asp:TextBox>
            </div>
            <div class="flex gap-3">
                <asp:CheckBox ID="cbxShip" runat="server" OnCheckedChanged="cbxShip_CheckedChanged" AutoPostBack="true" CssClass="border-radius: 0.25rem;" />
                <span class="font-semibold">Ship into different address</span>
            </div>


            <!-- If shipping address diff -->
            <div class="hidden" id="shippingAddressDetails" runat="server">
                <div class="flex flex-col gap-2">
                    <span class="text-gray-900 font-bold text-xl mt-8 mb-2">Shipping Information</span>
                    <span class="font-semibold">Address</span>
                    <asp:TextBox class="border border-gray-300 p-2" ID="txtShippingAddressLine1" placeholder="Address Line 1" runat="server"></asp:TextBox>
                    <asp:TextBox class="border border-gray-300 p-2" ID="txtShippingAddressLine2" placeholder="Address Line 2" runat="server"></asp:TextBox>
                    <div class="flex gap-3 font-semibold">
                        <span class="w-1/4">Country</span>
                        <span class="w-1/4">Region/State</span>
                        <span class="w-1/4">City</span>
                        <span class="w-1/4">Zip Code</span>
                    </div>
                    <div class="flex gap-3">
                        <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlShippingCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShippingCountry_SelectedIndexChanged">
                    <asp:ListItem Value="null">-Default-</asp:ListItem>
                    <asp:ListItem Value="MY">Malaysia</asp:ListItem>
                    <asp:ListItem Value="SG">Singapore</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlShippingState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShippingState_SelectedIndexChanged">
                    <asp:ListItem Value="null">-Default-</asp:ListItem>
                </asp:DropDownList>
                        <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlShippingCity" runat="server">
                    <asp:ListItem Value="null">-Default-</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox class="w-1/4 border border-gray-300 p-2" ID="txtShippingZipCode" runat="server"></asp:TextBox>
                    </div>

                </div>
            </div>


            <!-- Additional Information -->
            <span class="font-bold mt-3">Additional Information</span>
            <span class="font-semibold">Order Notes<span class="text-gray-400"> (Optional)</span></span>
            <asp:TextBox class="p-2 border border-gray-300 resize-none" TextMode="MultiLine" Rows="4" ID="txtOrderNote" runat="server" placeholder="Notes about your order, e.g. special note for delivery"></asp:TextBox>
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
                        <tr class="grid grid-cols-4 items-center">
                            <td class="col-span-1">
                                <asp:Image ID="imgProduct" runat="server" AlternateText="Image" ImageUrl='<%# Eval("imagePath", "{0}") %>' class="h-14 w-14 " />
                            </td>
                            <td class="col-span-3 flex flex-col">
                                <span class="font-semibold overflow-hidden whitespace-nowrap overflow-ellipsis"><%# Eval("productName") %> <%# Eval("variantName") %></span>
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
                    <asp:Button ID="btnPlaceOrder" runat="server" Text="Proceed To Payment" class="cursor-pointer bg-blue-700 text-white font-semibold text-sm h-10 rounded-lg mt-4" OnClick="validateInput" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function updateStates(selectedCountry) {
            var states = MyPage.Countries[selectedCountry]; // Access states based on country
            ddlCity.Items.clear(); // Clear existing state options

            if (states) {
                for (var i = 0; i < states.length; i++) {
                    var state = states[i];
                    var listItem = new Option(state, state); // Create a new ListItem
                    ddlCity.Items.add(listItem);
                }
            } else {
                // Handle cases where the country doesn't have states
                ddlCity.Items.add(new Option("No states available", ""));
            }
        }

        function ddlCountry_SelectedIndexChanged(sender, e) {
            var selectedCountry = ddlCountry.SelectedValue;
            updateStates(selectedCountry);
        }
    </script>
</asp:Content>
