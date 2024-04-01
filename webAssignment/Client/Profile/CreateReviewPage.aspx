<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ProfileMaster/ProfileMasterPage.master" AutoEventWireup="true" CodeBehind="CreateReviewPage.aspx.cs" Inherits="webAssignment.Client.Profile.CreateReviewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="min-h-[80vh] p-2 flex flex-col gap-3">
        <div class=" p-2 flex justify-between items-center">
            <div class="flex gap-4 items-center text-lg text-gray-500 font-bold">
                <span class="">Order History</span>
                <i class="fa-solid fa-caret-right pt-1 pl-1"></i>
                <span class="">Order Details</span>
                <i class="fa-solid fa-caret-right pt-1 pl-1"></i>
                <span class="text-xl text-gray-900 font-bold">Rate</span>
            </div>
        </div>


        <div class="border border-gray-300 bg-white shadow shadow-xl rounded-2xl min-h-[40vh] p-4 flex gap-2">
            <div class="w-[20%]">
                <asp:Image ID="imgProd" runat="server" ImageUrl="~/Client/Cart/images/cryingKermit.png" class="h-28 w-auto" />

            </div>
            <div class="w-[80%]">
                <h2>Kermit</h2>

            </div>

        </div>
        <asp:ListView ID="lvProductList" runat="server">
            <LayoutTemplate>
                <div id="itemPlaceholder" runat="server"></div>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
