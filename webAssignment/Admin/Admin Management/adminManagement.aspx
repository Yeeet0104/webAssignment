﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="adminManagement.aspx.cs" Inherits="webAssignment.Admin.Admin_Management.adminManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <!--First Row-->
    <div class="flex flex-row justify-between font-medium p-0">
        <div class="flex flex-col">
            <div class="text-black text-lg">Admin</div>
            <div class="flex flex-row text-sm p-1">
                <div class="text-blue-600">Dashboard</div>
                <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                <div>Admin List</div>
            </div>
        </div>
        <div class="flex flex-row">
            <div class="relative mr-2">
                <i class="fa-solid fa-download text-blue-600 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" class="pl-11 pr-5 py-2.5 text-sm bg-white text-blue-600 rounded-lg" />
            </div>
            <div class="relative ml-2">
                <i class="fa-solid fa-plus text-white absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:HyperLink ID="linkAddNewAdmin" runat="server" NavigateUrl="~/Admin/Admin Management/addNewAdmin.aspx" class="pl-11 pr-5 py-2.5 text-sm bg-blue-600 text-white rounded-lg hover:cursor-pointer">Add New Admin</asp:HyperLink>
            </div>
        </div>
    </div>
    <!--End-->

    <!--Second Row-->
    <div class="flex flex-row justify-between text-sm text-gray-600 font-medium my-4">
        <div class="p-1 border border-gray-200 flex flex-row justify-between w-62 h-11 rounded-lg bg-white">
            <div class="px-3 py-2 text-blue-600 bg-gray-100 rounded-lg">
                All
            </div>
            <div class="px-3 py-2">
                Active
            </div>
            <div class="px-3 py-2 ">
                Blocked
            </div>
            <div class="px-3 py-2 ">
                Hidden
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
    <div class="bg-white p-5 text-base rounded-lg">
        <asp:ListView ID="adminListView" runat="server" OnSelectedIndexChanged="adminListView_SelectedIndexChanged">
            <LayoutTemplate>
                <table class="orders-table w-full">
                    <!-- Headers here -->
                    <div class="grid grid-cols-10 gap-6 mb-4">
                        <div class="col-span-3 flex flex-row justify-between">
                            <p>Name </p>
                            <asp:LinkButton ID="filterName" runat="server">

<i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                            </asp:LinkButton>
                        </div>

                        <div class="col-span-2 flex flex-row justify-between">
                            <p>Email </p>
                            <asp:LinkButton ID="filterEmail" runat="server">

<i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="col-span-1">
                            <p>Phone No</p>
                        </div>
                        <div class="col-span-1 flex flex-row justify-between">
                            <p>DOB </p>
                            <asp:LinkButton ID="filterDOB" runat="server">

    <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                            </asp:LinkButton>
                        </div>

                        <div class="col-span-1 flex justify-row pl-5">
                            <p>Status</p>
                        </div>

                        <div class="col-span-1 flex flex-row justify-between">
                            <p>Added </p>
                            <asp:LinkButton ID="filterAdded" runat="server">

    <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="col-span-1 flex justify-end">
                            <p>Action</p>
                        </div>

                    <tr id="itemPlaceholder" runat="server"></tr>
                    <hr class="border rounded mb-3" />
                </table>
                <table>
                    <tfoot>
                        <hr class="border rounded mb-2" />

                        <!-- footer for pagination ( WILL CHANGE TO physical button later) -->
                        <div class="flex flex-row text-gray-400 justify-between rounded-b-lg bg-white items-center">
                            <asp:Label ID="pageNumFoot" runat="server" Text="Showing 1-10 from 100" class="text-normal text-base p-5"></asp:Label>
                            <div class="flex">
                                <div class="p-4 text-base flex flex-row gap-3">
                                    <div class="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center text-blue-500">
                                        <i class="fa-solid fa-arrow-left-long"></i>
                                    </div>
                                    <div class="min-w-11 min-h-11 rounded-full bg-blue-500 text-white border-blue-500 border flex items-center justify-center">
                                        <i class="fa-solid fa-1"></i>
                                    </div>
                                    <div class="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center">
                                        <i class="fa-solid fa-2"></i>
                                    </div>
                                    <div class="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center">
                                        <i class="fa-solid fa-3"></i>
                                    </div>
                                    <div class="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center">
                                        <i class="fa-solid fa-4"></i>
                                    </div>
                                    <div class="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center text-blue-500">
                                        <i class="fa-solid fa-arrow-right-long"></i>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </tfoot>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="grid grid-cols-10 gap-6 w-full mb-5" style="color: #8B8E99">
                    <td class="col-span-3 flex flex-row gap-2 items-center">
                        <asp:Image ID="customerImage" runat="server" AlternateText="Customer Image" Height="64" Width="64"
                            ImageUrl='<%# Eval("AdminImageUrl", "{0}") %>' CssClass="rounded border" />
                        <span class="text-black">
                            <%# Eval("AdminName") %>  
                        </span>
                    </td>
                    <td class="col-span-2 flex items-center"><%# Eval("AdminEmail") %></td>
                    <td class="col-span-1 flex items-center"><%# Eval("PhoneNo") %></td>
                    <td class="col-span-1 flex items-center"><%# Eval("DOB", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-1 flex items-center w-full justify-center">
                        <div class="text-green-600 bg-green-200 text-center p-1 rounded-lg w-4/5">
                            <%# Eval("Status") %>
                        </div>
                    </td>
                    <td class="col-span-1 flex items-center"><%# Eval("Added", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-1 flex justify-end items-center">
                        <div class="flex flex-row gap-2">
                            <asp:HyperLink ID="customerEditLink" runat="server" CssClass="fa-solid fa-pen"></asp:HyperLink>
                            <i class="fa-solid fa-eye"></i>
                            <i class="fa-solid fa-trash"></i>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>

        </asp:ListView>
    </div>
    <!--End-->
</asp:Content>