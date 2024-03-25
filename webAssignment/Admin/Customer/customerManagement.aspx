<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="webAssignment.Admin.Customer.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First Row-->
    <div class="flex flex-row justify-between font-medium p-0">
        <div class="flex flex-col">
            <div class="text-black text-lg">Customers</div>
            <div class="flex flex-row text-sm p-1">
                <div class="text-blue-600">Dashboard</div>
                <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                <div>Customers List</div>
            </div>
        </div>
        <div class="flex">
            <div class="relative mr-2">
                <i class="fa-solid fa-download text-blue-500 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg" />
            </div>
            <div class="relative ml-2">
                <i class="fa-solid fa-plus absolute text-2xl left-4 top-5 text-white transform -translate-y-1/2"></i>
                <asp:Button ID="btnAddNewCust" runat="server" Text="Add New Customer" class="pl-11 pr-5 py-2.5 text-sm bg-blue-500 text-white rounded-lg" />
            </div>

        </div>
    </div>
    <!--End-->

    <!--Second Row-->
    <div class="flex flex-row justify-between text-sm text-gray-600 font-medium my-4">
        <div class="p-1 border border-gray-200 flex flex-row justify-between w-52 h-11 rounded-lg bg-white">
            <div class="px-3 py-2 text-blue-600 bg-gray-100 rounded-lg">
                All
            </div>
            <div class="px-3 py-2">
                Active
            </div>
            <div class="px-3 py-2 ">
                Blocked
            </div>
        </div>
        <div class="flex">
            <div class="relative mr-2">
                <i class="fa-solid fa-calendar-days absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnSelectDate" runat="server" Text="Select Date" class="pl-10 pr-4 py-2 border border-gray-200 rounded-lg bg-white" />
            </div>
            <div class="relative ml-2">
                <i class="fa-solid fa-sliders absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnFilters" runat="server" Text="Filters" class="pl-10 pr-4 py-2 border border-gray-200 rounded-lg bg-white" />
            </div>
        </div>
    </div>
    <!--End-->

    <!--Name List-->

    <asp:Repeater ID="rptCustomer" runat="server">
        <HeaderTemplate>
            <div class="border-b-2 border-gray-300 bg-gray-100 rounded-t-lg text-sm text-black text-normal px-3 py-4 flex justify-between bg-white">
                <div class="w-36">Name</div>
                <div class="w-36">Email</div>
                <div class="w-20">Phone No</div>
                <div class="w-12">DOB</div>
                <div class="w-16">Status</div>
                <div class="w-16">Added</div>
                <div class="w-16">Action</div>
            </div>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="bg-blue-50 border-b-2 bg-gray-100 border-gray-300 text-sm flex justify-between px-3 py-5 bg-white">
                <div class="w-36 text-black"><%# Eval("Name") %></div>
                <div class="w-36"><%# Eval("Email") %></div>
                <div class="w-20"><%# Eval("PhoneNo") %></div>
                <div class="w-12"><%# Eval("DOB", "{0:dd.MM.yyyy}") %></div>
                <div class="w-16 text-green-600 bg-green-200 text-center p-1 rounded-lg"><%# Eval("Status") %></div>
                <div class="w-16"><%# Eval("Added", "{0:dd.MM.yyyy}") %></div>
                <div class="w-16">
                    <div class="flex justify-between">
                        <div>
                            <i class="fa-solid fa-pen"></i>
                        </div>
                        <div>
                            <i class="fa-solid fa-eye"></i>
                        </div>
                        <div>
                            <i class="fa-solid fa-trash-can"></i>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            <div class="flex flex-row justify-between bg-gray-100 rounded-b-lg bg-white">
                <asp:Label ID="pageNumFoot" runat="server" Text="Showing 1-10 from 100" class="text-normal text-sm p-5"></asp:Label>
                <div class="flex">
                    <div class="p-4 text-xl flex flex-row">
                        <div class="p-2">
                            <i class="fa-solid fa-arrow-left-long"></i>
                        </div>
                        <div class="p-2">
                            <i class="fa-solid fa-1"></i>
                        </div>
                        <div class="p-2">
                            <i class="fa-solid fa-2"></i>
                        </div>
                        <div class="p-2">
                            <i class="fa-solid fa-3"></i>
                        </div>
                        <div class="p-2">
                            <i class="fa-solid fa-4"></i>
                        </div>
                        <div class="p-2">
                            <i class="fa-solid fa-arrow-right-long"></i>
                        </div>
                    </div>

                </div>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <!--End-->
</asp:Content>
