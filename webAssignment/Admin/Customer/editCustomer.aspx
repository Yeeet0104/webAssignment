<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="editCustomer.aspx.cs" Inherits="webAssignment.Admin.Customer.editCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First Row-->
    <div class="flex flex-row justify-between font-medium p-0">
        <div class="flex flex-col">
            <div class="text-black text-lg">Edit Customer</div>
            <div class="flex flex-row text-sm p-1">
                <div class="text-blue-600">Dashboard</div>
                <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                <div class="text-blue-600">Customers List</div>
                <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                <div>Edit Customer</div>
            </div>
        </div>
        <div class="flex">
            <div class="relative mr-2 mt-0.5">
                <i class="fa-solid fa-xmark text-gray-400 absolute text-2xl left-3 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="pl-8 pr-4 py-1 text-lg border-2 text-gray-400 border-gray-400 rounded-lg" />
            </div>
            <div class="relative ml-2">
                <i class="fa-solid fa-floppy-disk absolute text-2xl left-4 top-5 text-white transform -translate-y-1/2"></i>
                <asp:Button ID="btnSaveCustomer" runat="server" Text="Save Customer" class="pl-11 pr-5 py-2.5 text-sm bg-blue-500 text-white rounded-lg" />
            </div>

        </div>
    </div>
    <!--End-->

    <!--Second Row-->
    <div class="flex flex-row pt-5 pb-8">
        <div>
            <!--Profile Pic container -->
            <div class="bg-white rounded-lg" style="width: 264px; height: 380px;">
                <div class="flex flex-col p-6">
                    <span class="pb-2 font-medium">Profile Picture</span>
                    <span class="text-lg font-medium text-gray-400">Photo</span>

                    <!--Pic Box-->
                    <div class="py-1">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Admin/Layout/image/DexProfilePic.jpeg" Height="216" Width="216" />
                    </div>

                    <div class="flex justify-center pt-3">
                        <asp:Button ID="btnNewProfileImg" runat="server" Text="Change Image" class="bg-blue-100 rounded-lg text-center px-5 py-2 text-sm font-medium text-blue-600" />
                    </div>
                </div>
            </div>

            <!--Status Section-->
            <div class="bg-white rounded-lg mt-4 p-6" style="width: 264px; height: 160px;">
                <div class="flex flex-col">
                    <div class="flex flex-row justify-between">
                        <span class="font-medium">Status</span>
                        <span class="rounded-lg font-medium flex items-center px-3 py-1.5 text-sm text-green-700 bg-green-100">Active</span>
                    </div>
                    <span class="font-medium text-lg pt-2 pb-1 text-gray-400">Customer Status</span>
                    <asp:DropDownList ID="ddlStatus" runat="server" class="w-full font-medium bg-gray-100 px-3 py-2 rounded-lg text-lg border-2 border-gray-300">
                        <asp:ListItem>Active</asp:ListItem>
                        <asp:ListItem>Blocked</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <!--Customer Details-->
        <div class="bg-white grow ml-8 rounded-lg p-10">
            <div class="flex flex-col">
                <span class="text-black pb-2">General Information</span>

                <div class="pb-4">
                    <span class="text-sm font-medium text-gray-600">Name</span>
                    <asp:TextBox ID="TextBox1" runat="server" Text="Yap Zi Yan" class="w-full font-medium bg-gray-100 px-3 py-2 rounded-lg text-lg border-2 border-gray-300"></asp:TextBox>
                </div>

                <div class="pb-4">
                    <span class="text-sm font-medium text-gray-600">Email</span>
                    <asp:TextBox ID="TextBox2" runat="server" Text="zyyap69@gmail.com" class="w-full font-medium bg-gray-100 px-3 py-2 rounded-lg text-lg border-2 border-gray-300"></asp:TextBox>
                </div>

                <div class="pb-4">
                    <span class="text-sm font-medium text-gray-600">Phone Number</span>
                    <asp:TextBox ID="TextBox3" runat="server" Text="0123136742" class="w-full font-medium bg-gray-100 px-3 py-2 rounded-lg text-lg border-2 border-gray-300"></asp:TextBox>
                </div>

                <div>
                    <div class="flex flex-col pb-4">
                        <span class="py-3">Date Of Birth</span>
                        <asp:Label ID="lblDob" runat="server" Text="15 October 2003" class="text-lg font-medium bg-gray-100 rounded-lg border-2 border-gray-300 py-2 px-3 w-44 text-center"></asp:Label>
                    </div>

                    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                </div>
            </div>
        </div>
    </div>

    <!--End-->
</asp:Content>
