﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="transaction.aspx.cs" Inherits="webAssignment.Admin.Transaction.transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">
        <div class="flex flex-col">
            <div class="text-2xl font-bold ">
                <p>Transaction</p>
            </div>
            <div class="flex flex-row text-sm py-2">
                <div class="text-blue-600">Dashboard</div>
                <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                <div>Transaction List</div>
            </div>
        </div>

        <div class="flex">
            <div class="relative mr-2">
                <i class="fa-solid fa-download text-blue-500 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg" />
            </div>
            <%--            <div class="text-sm relative ml-2 bg-blue-500 text-white flex flex-row items-center p-1 px-2 gap-2 rounded-lg">
                <i class="text-lg fa-solid fa-plus left-4 top-5 text-white"></i>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/Product Management/addNewProduct.aspx">Add New Product</asp:HyperLink>
            </div>--%>
        </div>
    </div>
    <!--End-->

    <!--Second Row-->
    <div class="flex flex-row justify-between text-sm text-gray-600 font-medium my-4 justify-self-center">
        <div class="grid grid-cols-3 bg-white gap-3 text-center rounded p-2">

            <div class="col-span-1 px-3 py-1 text-blue-600 bg-gray-100 rounded-lg">
                <asp:Button ID="allFilter" runat="server" Text="All" />
            </div>
            <div class="col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg">
                <asp:Button ID="SuccessFilter" runat="server" Text="Success" />
            </div>
            <div class="col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg">
                <asp:Button ID="CancelledFilter" runat="server" Text="Cancelled" />

            </div>
        </div>
        <div class="flex items-center gap-3">
            <div class="">
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

    <!--Product List-->
    <div class="bg-white p-5 text-base rounded-lg drop-shadow-lg">


        <asp:ListView ID="productListView" runat="server" OnSelectedIndexChanged="productListView_SelectedIndexChanged">
            <LayoutTemplate>
                <table class="orders-table w-full ">
                    <!-- Headers here -->
                    <p class="pb-5">
                        Latest Orders
                    </p>
                    <div class="grid grid-cols-8 gap-10 mb-4">
                        <div class="col-span-1">
                            <p>Customer ID</p>
                        </div>
                        <div class="col-span-1">
                            <p>User Name</p>
                        </div>
                        <div class="col-span-2">
                            <p>Description</p>
                        </div>
                        <div class="col-span-1 flex flex-row justify-between">
                            <p>Amount</p>
                            <asp:LinkButton ID="filterPaidAmount" runat="server">

        <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="col-span-1 flex flex-row justify-between">
                            <p>Method </p>
                            <asp:LinkButton ID="filterPaymentMethod" runat="server">

        <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                            </asp:LinkButton>
                        </div>
                        <div class="col-span-1 flex flex-row justify-between">
                            <p>Date </p>
                            <asp:LinkButton ID="filterDatePaid" runat="server">

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
                <tr class="grid grid-cols-8 gap-10 w-full mb-5 py-7" style="color: #8B8E99">
                    <td class="col-span-1 flex flex-row gap-2 items-center"><%# Eval("CustomerID") %></td>
                    <td class="col-span-1 flex items-center text-black"><%# Eval("UserName") %></td>
                    <td class="col-span-2 flex items-center text-black break-words"><%# Eval("Description") %></td>
                    <td class="col-span-1 flex items-center "><%# Eval("Amount") %></td>
                    <td class="col-span-1 flex items-center "><%# Eval("PaymentMethod") %></td>
                    <td class="col-span-1 flex items-center "><%# Eval("Date", "{0:dd MMM yyyy}") %></td>

                    <td class="col-span-1 flex justify-end items-center">
                        <div class="flex flex-row gap-2">
                            <i class="fa-solid fa-trash"></i>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>

        </asp:ListView>
    </div>
</asp:Content>