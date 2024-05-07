<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="AddAddress.aspx.cs" Inherits="webAssignment.Client.Profile.AddAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="px-48 py-8">
        <div class="rounded-lg flex flex-col gap-3 text-xl text-gray-704 p-4">
            <asp:Label ID="lblAddress" runat="server" class="text-gray-900 font-bold text-3xl" Text="Label"></asp:Label>
            <div class="flex pt-3">
                <span class="w-1/2 font-semibold">Username</span>
            </div>
            <div class="flex w-full gap-14 pb-3">
                <asp:TextBox class="border border-gray-300 w-1/2 px-3 py-4" ID="txtFirstName" runat="server" placeholder="First name"></asp:TextBox>
                <asp:TextBox class="border border-gray-300 w-1/2 px-3 py-4" ID="txtLastName" runat="server" placeholder="Last name"></asp:TextBox>
            </div>
            <span class="font-semibold">Address</span>
            <asp:TextBox class="border border-gray-300 px-3 py-4 mb-3" ID="txtAddressLine1" placeholder="Address Line 1" runat="server"></asp:TextBox>
            <asp:TextBox class="border border-gray-300 px-3 py-4" ID="txtAddressLine2" placeholder="Address Line 2" runat="server"></asp:TextBox>
            <div class="flex gap-6 font-semibold mt-3">
                <span class="w-1/4">Country</span>
                <span class="w-1/4">Region/State</span>
                <span class="w-1/4">City</span>
                <span class="w-1/4">Zip Code</span>
            </div>
            <div class="flex gap-6">
                <asp:DropDownList ID="ddlCountry" runat="server" class="w-1/4 border border-gray-300 px-3 py-4">
                    <asp:ListItem>Malaysia - MAL</asp:ListItem>
                    <asp:ListItem>Singapore - SGP</asp:ListItem>
                </asp:DropDownList>

                <asp:TextBox class="w-1/4 border border-gray-300 px-3 py-4" ID="txtState" runat="server"></asp:TextBox>

                <asp:TextBox class="w-1/4 border border-gray-300 px-3 py-4" ID="txtCity" runat="server"></asp:TextBox>
                <asp:TextBox class="w-1/4 border border-gray-300 px-3 py-4" ID="txtZipCode" runat="server"></asp:TextBox>
            </div>
            <div class="flex gap-3 font-semibold mt-3">
                <span class="w-1/2">Phone Number</span>
            </div>
            <div class="flex row justify-between">
                <asp:TextBox class="w-1/3 border border-gray-300 px-3 py-4" ID="txtPhoneNumber" runat="server"></asp:TextBox>
                <div>
                    <asp:Button ID="cancelBtn" runat="server" Text="Cancel" class="px-10 py-3 mr-2 text-white w-auto bg-red-700 rounded-xl hover:cursor-pointer" OnClick="cancelBtn_Click" />
                    <asp:Button ID="btnAddAddress" runat="server" Text="Add" class="px-12 py-3 ml-2 text-white w-auto bg-blue-700 rounded-xl hover:cursor-pointer" OnClick="btnAddAddress_Click" />
                    <asp:Button ID="editBtn" runat="server" Text="Edit" class="px-12 py-3 ml-2 text-white w-auto bg-blue-700 rounded-xl hover:cursor-pointer" OnClick="btnEditAddress_Click" />
                </div>
            </div>
            <asp:Label ID="lblErrorMsg" CssClass="font-bold text-red-600" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
