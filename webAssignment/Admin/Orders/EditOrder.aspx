<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="EditOrder.aspx.cs" Inherits="webAssignment.Admin.Orders.EditOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">

        <div class="w-3/5 flex flex-row items-center">
            <asp:TextBox ID="productSearchBox" runat="server" CssClass="
                            w-11/12 px-4 py-2 mr-4 text-gray-700 bg-white border rounded-md focus:border-blue-500 focus:outline-none focus:ring h-fit"
                placeholder="Search...">


            </asp:TextBox>
            <asp:LinkButton ID="searchBtn" runat="server" CssClass="px-1 py-2 text-gray-700 rounded-md">
            <i class="fa-solid fa-magnifying-glass text-xl "></i>
            </asp:LinkButton>
        </div>

        <div class="flex">
            <div class="relative mr-2">
                <i class="fa-solid fa-download text-blue-500 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg" />
            </div>
            <div class="text-sm relative ml-2 bg-blue-500 text-white flex flex-row items-center p-1 px-2 gap-2 rounded-lg">
                <i class="text-lg fa-solid fa-plus left-4 top-5 text-white"></i>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/Product Management/addNewProduct.aspx">Add New Product</asp:HyperLink>
            </div>

        </div>
    </div>
    <div>
        <asp:Label ID="testing" runat="server" Text="" CssClass="text-2xl"></asp:Label>
    </div>
</asp:Content>

