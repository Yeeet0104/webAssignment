﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="adminManagement.aspx.cs" Inherits="webAssignment.Admin.Admin_Management.adminManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">
        <div class="flex flex-col">
            <div class="text-2xl font-bold">Admin</div>
            <div class="flex flex-row text-sm py-2">
                <div class="text-blue-600">Dashboard</div>
                <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                <div>Admin List</div>
            </div>
        </div>
        <div class="flex">
            <div class="relative mr-2">
                <i class="fa-solid fa-download absolute text-blue-500 text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg" />
            </div>
            <div class="text-sm relative ml-2 bg-blue-500 text-white flex flex-row items-center p-1 px-3 gap-2 rounded-lg ">
                <i class="fa-solid fa-plus text-white text-lg left-4 top-5"></i>
                <asp:HyperLink ID="linkAddNewAdmin" runat="server" NavigateUrl="~/Admin/Admin Management/addNewAdmin.aspx">Add New Admin</asp:HyperLink>
            </div>
        </div>
    </div>
    <!--End-->

    <!--Second Row-->
    <div class="flex flex-row justify-between text-sm text-gray-600 font-medium my-4 justify-self-center">
        <div class="grid grid-cols-3 bg-white gap-3 text-center rounded p-2">

            <div class="col-span-1 px-3 py-1 text-blue-600 bg-gray-100 rounded-lg">
                <asp:Button ID="allAdmins" runat="server" Text="All" />
            </div>
            <div class="col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg">
                <asp:Button ID="active" runat="server" Text="Active" />
            </div>
            <div class="col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg">
                <asp:Button ID="blocked" runat="server" Text="Blocked" />

            </div>
        </div>
        <div class="flex items-center gap-3">
            <div>
                <asp:LinkButton ID="filterDateBtn" runat="server" class="p-3 border border-gray-200 rounded-lg bg-white flex gap-3 items-center">
                 <i class="fa-solid fa-calendar-days"></i>
                <span>
                   Select Date
                </span>
                </asp:LinkButton>
            </div>
            <div class="">
                <asp:LinkButton ID="filterOptionbtn" runat="server" class="p-3 border border-gray-200 rounded-lg bg-white flex gap-3 items-center">
            <i class="fa-solid fa-sliders "></i>
                <span>
                   Filters
                </span>
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <!--End-->

    <!--Name List-->
    <div class="bg-white p-5 text-base rounded-lg drop-shadow-lg">
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
                        <div class="flex flex-row gap-4 items-center">
                            <asp:HyperLink ID="adminEditLink" runat="server" CssClass="fa-solid fa-pen"></asp:HyperLink>
                            <asp:LinkButton ID="deleteAdminLink" runat="server" CommandName="DeleteAdmin" onclick="showPopUp_Click">
                                <i class="fa-solid fa-trash"></i>
                            </asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>

    <asp:Panel ID="popUpDelete" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center ">
    <!-- Modal content -->
    <div class="popUp-content w-1/3 h-fit flex flex-col bg-white p-5 rounded-xl flex flex-col gap-3 drop-shadow-lg">

        <div class="w-full h-fit  flex justify-end p-0">
            <span class=" flex items-center justify-center text-3xl rounded-full">

                <asp:LinkButton ID="closePopUp" runat="server" OnClick="closePopUp_Click">
                    <i class=" fa-solid fa-xmark"></i>
                </asp:LinkButton>
            </span>

        </div>
        <div class="flex flex-col justify-center items-center gap-5">

            <div style="font-size: 64px">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Admin/Orders/Images/trash.gif" AlternateText="trashcan" CssClass="w-28 h-28 " />

            </div>
            <p class="bold text-lg break-normal text-center">Are you sure you want to delete the following item?</p>
            <p class="bold text-lg">
                <asp:Label ID="lblItemInfo" runat="server" Text="[OrderID]"></asp:Label>
            </p>
            <asp:TextBox ID="passwordForDelete" runat="server" TextMode="Password" CssClass="p-2 px-4 border rounded-xl" placeholder="Enter password to confirm"></asp:TextBox>
            <div>

                <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" CssClass="bg-gray-300 p-2 px-4 rounded-lg cursor-pointer" OnClick="btnCancelDelete_Click" />
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Delete" CssClass="bg-red-400 p-2 px-4 rounded-lg cursor-pointer" />
            </div>
        </div>
    </div>
</asp:Panel>

    <style>
    @keyframes rockUpDown {
        0%, 100% {
            transform: translateY(0);
        }

        10% {
            transform: translateY(-10px);
        }

        20%, 40%, 60%, 80% {
            transform: translateY(-10px) rotate(-6deg);
        }

        30%, 50%, 70% {
            transform: translateY(-10px) rotate(6deg);
        }

        90% {
            transform: translateY(-10px);
        }
    }

    .fa-trash-can {
        display: inline-block;
        transition: transform ease-in-out 0.75s;
    }

        .fa-trash-can:hover {
            animation: rockUpDown 1.2s ease-in-out infinite;
        }
</style>
</asp:Content>
