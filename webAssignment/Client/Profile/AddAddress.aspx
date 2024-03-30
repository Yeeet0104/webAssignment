<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="AddAddress.aspx.cs" Inherits="webAssignment.Client.Profile.AddAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="px-44 py-8">
        <div class="rounded-lg flex flex-col gap-3 text-xl text-gray-704 p-4">
            <span class="text-gray-900 font-bold text-3xl">Add New Address</span>
            <div class="flex gap-3">
                <span class="w-1/2 font-semibold">Username</span>
            </div>
            <div class="flex w-full gap-10">
                <asp:TextBox class="border border-gray-300 w-1/2 p-2" ID="txtFirstName" runat="server" placeholder="First name"></asp:TextBox>
                <asp:TextBox class="border border-gray-300 w-1/2 p-2" ID="txtLastName" runat="server" placeholder="Last name"></asp:TextBox>
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
                <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlCountry" runat="server">
                    <asp:ListItem>Australia</asp:ListItem>
                    <asp:ListItem>Canada</asp:ListItem>
                    <asp:ListItem>China</asp:ListItem>
                    <asp:ListItem>India</asp:ListItem>
                    <asp:ListItem>Malaysia</asp:ListItem>
                    <asp:ListItem>Singapore</asp:ListItem>
                    <asp:ListItem>Taiwan</asp:ListItem>
                    <asp:ListItem>United Kingdom</asp:ListItem>
                    <asp:ListItem>United State of America</asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlState" runat="server">
                    <asp:ListItem>Kedah</asp:ListItem>
                    <asp:ListItem>Wilayah Persekutuan</asp:ListItem>
                    <asp:ListItem>Terengganu</asp:ListItem>
                    <asp:ListItem>Penang</asp:ListItem>
                    <asp:ListItem>Melaka</asp:ListItem>
                    <asp:ListItem>Johor</asp:ListItem>
                    <asp:ListItem>Perak</asp:ListItem>
                    <asp:ListItem>Sabah</asp:ListItem>
                    <asp:ListItem>Sarawak</asp:ListItem>
                    <asp:ListItem>Perlis</asp:ListItem>
                    <asp:ListItem>Pahang</asp:ListItem>
                    <asp:ListItem>Negeri Sembilan</asp:ListItem>
                    <asp:ListItem>Selangor</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList class="w-1/4 border border-gray-300 p-2" ID="ddlCity" runat="server">
                    <asp:ListItem>Kuala Lumpur</asp:ListItem>
                    <asp:ListItem>Petaling Jaya</asp:ListItem>
                    <asp:ListItem>Gombak</asp:ListItem>
                    <asp:ListItem>Kepong</asp:ListItem>
                    <asp:ListItem>Subang</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox class="w-1/4 border border-gray-300 p-2" ID="txtZipCode" runat="server"></asp:TextBox>
            </div>
            <div class="flex gap-3 font-semibold">
                <span class="w-1/2">Phone Number</span>
            </div>
            <div class="flex row justify-between">
                <asp:TextBox class="w-1/2 border border-gray-300 p-2" ID="txtPhoneNumber" runat="server"></asp:TextBox>
                <asp:HyperLink ID="btnAddAddress" runat="server" NavigateUrl="~/Client/Profile/Address.aspx" class="px-10 py-2 text-white w-auto bg-blue-700 rounded-3xl hover:cursor-pointer">Add</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
