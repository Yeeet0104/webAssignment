<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="customerManagement.aspx.cs" Inherits="webAssignment.Admin.Customer.customerManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
    function showPopup() {
        document.getElementById('<%= pnlDateFilter.ClientID %>').style.display = 'block';
    }

    function hidePopup() {
        document.getElementById('<%= pnlDateFilter.ClientID %>').style.display = 'none';
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">
        <div class="flex flex-col">
            <div class="text-2xl font-bold">Customers</div>
            <div class="flex flex-row text-sm py-2">
                <asp:SiteMapPath
                    ID="SiteMapPath1"
                    runat="server"
                    RenderCurrentNodeAsLink="false"
                    PathSeparator=">"
                    CssClass="siteMap font-bold flex gap-2 text-sm pt-2">
                </asp:SiteMapPath>
            </div>
        </div>
        <div class="flex">
            <div class="relative mr-2">
                <i class="fa-solid fa-download text-blue-500 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg hover:cursor-pointer" OnClick="btnExport_Click" />
            </div>
        </div>
    </div>
    <!--End-->

    <!--Second Row-->
    <div class="flex flex-row justify-between text-sm text-gray-600 font-medium my-4 justify-self-center">
        <div class="grid grid-cols-3 bg-white gap-3 text-center rounded p-2">
            <div class="col-span-1">
                <asp:Button ID="allFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 text-blue-600 bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="All Customers" OnClick="allFilter_click" />
            </div>
            <div class="col-span-1">
                <asp:Button ID="activeFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="Active" OnClick="activeFilter_click" />
            </div>
            <div class="col-span-1">
                <asp:Button ID="blockedFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="Blocked" OnClick="blockedFilter_click" />
            </div>
        </div>
        <div class="flex items-center gap-3">
            <div class="flex flex-row gap-2">
                <asp:LinkButton ID="clearDateFilter" runat="server" class="p-3 border border-gray-200 rounded-lg bg-white flex gap-3 items-center" OnClick="clearDateFilter_Click">
                    <i class="fa-solid fa-trash"></i>
                    <asp:Label ID="Label1" runat="server" Text="Clear Date"></asp:Label>
                </asp:LinkButton>
                <asp:LinkButton ID="filterDateBtn" runat="server" class="p-3 border border-gray-200 rounded-lg bg-white flex gap-3 items-center" OnClick="filterDateBtn_click">
                    <i class="fa-solid fa-calendar-days"></i>
                    <asp:Label ID="lblDate" runat="server" Text="Select Date"></asp:Label>
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <!--End-->

    <!--Name List-->
    <div class="bg-white p-5 text-base rounded-lg drop-shadow-lg">
        <asp:ListView ID="customerListView" runat="server" OnItemDataBound="customerListView_DataBound" OnDataBound="customerListView_DataBound" OnSorting="customerListView_Sorting">
            <EmptyDataTemplate>
                <table class="orders-table w-full ">
                    <tr class="w-full ">
                        <td>
                            <div class="flex flex-col justify-center items-center">
                                <asp:Image ID="sadKermit" runat="server" ImageUrl="~/Admin/Category/sad_kermit.png" AlternateText="Product Image" Height="128" Width="128" />
                                <span>No User Found</span>
                            </div>
                        </td>
                    </tr>

                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <div style="overflow-x: auto">
                    <table class="orders-table w-full " style="overflow-x: auto; min-width: 1450px">
                        <!-- Headers here -->
                        <tr class="grid grid-cols-11 gap-6 px-4 py-2 rounded-lg  items-center bg-gray-100 mb-3">
                            <td class="col-span-2 hover:bg-white hover:text-black rounded-lg">
                                <asp:LinkButton ID="filterName" runat="server" CommandName="Sort" CommandArgument="first_name">
                      <div class="flex flex-row justify-between items-center p-2">
                                         <p>Name </p>
            <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>                    
                     </div>
                                </asp:LinkButton>
                            </td>
                            <td class="col-span-1 flex flex-row justify-center items-center">
                                <p>Username</p>
                            </td>
                            <td class="col-span-2 flex flex-row justify-center items-center">
                                <p>Email</p>
                            </td>
                            <td class="col-span-1 text-center">
                                <p>Phone No</p>
                            </td>
                            <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                <asp:LinkButton ID="filterDOB" runat="server" CommandName="Sort" CommandArgument="birth_date">
                   <div class="flex flex-row justify-between items-center p-2">
                                      <p>DOB </p>
         <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                                   </div>
                                </asp:LinkButton>
                            </td>
                            <td class="col-span-1 flex justify-row pl-5">
                                <p>Status</p>
                            </td>
                            <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                <asp:LinkButton ID="filterJoined" runat="server" CommandName="Sort" CommandArgument="date_created">
                   <div class="flex flex-row justify-between items-center p-2">
                                      <p>Joined</p>
         <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                 
                  </div>
                                </asp:LinkButton>
                            </td>
                            <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                <asp:LinkButton ID="filterLastLogin" runat="server" CommandName="Sort" CommandArgument="last_login">
          <div class="flex flex-row justify-between items-center p-2">
                             <p>Last Login</p>
<i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
        
         </div>
                                </asp:LinkButton>
                            </td>
                            <td class="col-span-1 flex justify-end">
                                <p>Action</p>
                            </td>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                    <tfoot>
                        <tr class="">
                            <td class="flex flex-row text-gray-400 justify-between rounded-b-lg bg-white items-center">

                                <div class="text-base flex justify-between flex-row gap-3">
                                    <div class="flex items-center">

                                        <asp:Label ID="pageNumFoot" runat="server" Text="Showing 1-10 from 100" class="text-normal text-base p-5"></asp:Label>
                                    </div>
                                    <div class="p-4 text-base flex justify-between flex-row gap-3">

                                        <asp:LinkButton ID="prevPage" runat="server" OnClick="prevPage_Click" CssClass="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center text-blue-500">
            <i class="fa-solid fa-arrow-left-long"></i>
                                        </asp:LinkButton>
                                        <div class="min-w-11 min-h-11 rounded-full bg-blue-500 text-white border-blue-500 border flex items-center justify-center">
                                            <asp:Label ID="lblCurrPagination" runat="server" Text="1"></asp:Label>
                                        </div>
                                        <asp:LinkButton ID="nextPage" runat="server" OnClick="nextPage_Click" CssClass="min-w-11 min-h-11 rounded-full border-blue-500 border flex items-center justify-center text-blue-500">
            <i class="fa-solid fa-arrow-right-long"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tfoot>
                </div>
            </LayoutTemplate>

            <ItemTemplate>
                <!-- Customer Details -->
                <tr class="grid grid-cols-11 gap-6 w-full mb-5 p-4 border-b-2" style="color: #8B8E99">
                    <td class="col-span-2 flex flex-row gap-2 items-center">
                        <asp:Image ID="Image1" runat="server" AlternateText="Customer Image" Height="64" Width="64"
                            ImageUrl='<%# Eval("profile_pic_path", "{0}") %>' CssClass="rounded border" />
                        <span class="text-black">
                            <%# Eval("first_name") %>  <%# Eval("last_name") %>
                        </span>
                    </td>
                    <td class="col-span-1 flex items-center justify-center"><%# Eval("username") %></td>
                    <td class="col-span-2 flex items-center justify-center"><%# Eval("email") %></td>
                    <td class="col-span-1 flex items-center justify-center"><%# Eval("phone_number") %></td>
                    <td class="col-span-1 flex items-center justify-center"><%# Eval("birth_date", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-1 flex items-center w-full justify-center">
                        <div class="text-center p-1 rounded-lg w-4/5 <%# Eval("status") != null && Eval("status").ToString().ToLower() == "active" ? "text-green-600 bg-green-200" : "text-red-600 bg-red-200" %>">
                            <%# Eval("status") %>
                        </div>
                    </td>

                    <td class="col-span-1 flex items-center justify-center"><%# Eval("date_created", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-1 flex items-center justify-center"><%# Eval("last_login") %></td>
                    <td class="col-span-1 flex justify-end items-center">
                        <div class="flex flex-row gap-4 items-center">
                            <asp:LinkButton ID="customerEditBtn" runat="server" CommandArgument='<%# Eval("user_id") %>' OnClick="customerEditBtn_Click" CssClass="fa-solid fa-pen"></asp:LinkButton>
                            <asp:LinkButton ID="deleteCustomerLink" runat="server" CommandArgument='<%# Eval("user_id") %>' CommandName="DeleteCustomer" OnClick="deleteCustomerLink_Click">
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
                    <asp:Button ID="btnConfirmDelete" runat="server" Text="Delete" CssClass="bg-red-400 p-2 px-4 rounded-lg cursor-pointer" OnClick="btnConfirmDelete_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- date pop up panel -->
<asp:Panel ID="pnlDateFilter" runat="server" CssClass="modal fixed w-full h-full top-0 left-0 flex items-center justify-center" Style="display: none; background-color: rgba(0, 0, 0, 0.5);">
    <div class="modal-content w-1/4 bg-white p-6 rounded-lg shadow-lg text-center flex flex-col gap-10">
        <h2 class="text-xl  font-bold">Select Date Range</h2>
        <div class="flex flex-col gap-2 ">
            <div class="grid grid-cols-3 ">
                <p class="flex justify-center items-center">Start Date :</p>
                <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" CssClass="date-picker form-input block px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm cursor-pointer col-span-2" />
            </div>
            <div class="grid grid-cols-3">
                <p class="flex justify-center items-center">End Date :</p>
                <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" CssClass="date-picker form-input block px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm cursor-pointer col-span-2" />

            </div>

        </div>
        <div class="flex justify-center gap-10">
            <asp:Button ID="cancelDate" runat="server" Text="Cancel" OnClick="cancelDate_click" CssClass="bg-gray-200 hover:bg-blue-700 hover:text-white text-black font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" />
            <asp:Button ID="btnApplyDateFilter" runat="server" Text="Apply Filter" OnClick="btnApplyDateFilter_Click" CssClass="apply-button bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" />
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
