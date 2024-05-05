<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="webAssignment.Admin.Category.Category" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-6">
        <div class="flex flex-col">
            <div class="text-2xl font-bold ">
                <p>Category</p>
            </div>
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
            <div class="relative rounded-lg text-blue-500 cursor-pointer mr-2 cursor-pointer hover:text-gray-200 hover:bg-blue-500 pl-11 pr-5 py-2.5 text-sm bg-gray-200">
                <i class="fa-solid fa-download absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" class="cursor-pointer" />
            </div>
            <div class="text-sm relative ml-2 bg-blue-500 hover:text-blue-500 hover:bg-gray-300 text-white flex flex-row items-center p-1 px-2 gap-2 rounded-lg">
                <i class="text-lg fa-solid fa-plus left-4 top-5"></i>
                <asp:HyperLink ID="createNewCategory" runat="server" NavigateUrl="~/Admin/Category/CreateCategory.aspx">Create Category</asp:HyperLink>
            </div>

        </div>
    </div>
    <!--End-->
    <div class="flex justify-end mb-2">
        <asp:LinkButton ID="clearDateFilter" runat="server" class="mr-2 p-3 border border-gray-200 rounded-lg bg-white flex gap-3 items-center" OnClick="clearDateFilter_Click">
            <i class="fa-solid fa-trash"></i>
            <asp:Label ID="Label1" runat="server" Text="Clear Date"></asp:Label>
        </asp:LinkButton>
        <asp:LinkButton ID="filterDateBtn" runat="server" class="p-3 border border-gray-200 rounded-lg bg-white flex gap-3 items-center" OnClick="filterDateBtn_click">
            <i class="fa-solid fa-calendar-days"></i>
            <asp:Label ID="labelDateRange" runat="server" Text="Select Date "></asp:Label>
        </asp:LinkButton>
    </div>
    <!--category List-->
    <div class="bg-white p-5 text-base rounded-lg">

        <asp:ListView ID="categoryListView" runat="server" OnSelectedIndexChanged="categoryListView_SelectedIndexChanged" OnItemCommand="categoryListView_ItemCommand" OnSorting="ListView1_Sorting" OnDataBound="categoryListView_DataBound">
            <EmptyDataTemplate>
                <table class="orders-table w-full ">
                    <tr class="w-full ">
                        <td>
                            <div class="flex flex-col justify-center items-center">
                                <asp:Image ID="sadKermit" runat="server" ImageUrl="~/Admin/Category/sad_kermit.png" AlternateText="Product Image" Height="128" Width="128" />
                                <span>No Category in the list</span>
                            </div>
                        </td>
                    </tr>

                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <div style="overflow-x: auto">
                    <table class="orders-table w-full " style="overflow-x: auto; min-width: 1450px">
                        <tbody>
                            <!-- Headers here -->
                            <tr class="grid grid-cols-8 gap-6 px-4 py-2 rounded-lg  items-center bg-gray-100 mb-3">

                                <td class="col-span-4">
                                    <p>Category Name</p>
                                </td>
                                <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                    <asp:LinkButton ID="filterSold" runat="server" CommandName="Sort" CommandArgument="Sold">
                                    <div id="babi" class="flex flex-row justify-between items-center p-2">
                                        <p>Total Sold</p>
                                        <i id="iconSold" class="fa-solid fa-sort-down sort-icon relative" style="top:-3px"></i>

                                    </div>

                                    </asp:LinkButton>
                                </td>

                                <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                    <asp:LinkButton ID="filterStock" runat="server" CommandName="Sort" CommandArgument="Stock">
                                    <div class="flex flex-row justify-between items-center p-2">
                                        <p>Stock</p>
                                        <i id="iconStock" class="fa-solid fa-sort-down sort-icon relative " style="top:-3px"></i>

                                    </div>

                                    </asp:LinkButton>
                                </td>

                                <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                    <asp:LinkButton ID="filterDateAdded" runat="server" CommandName="Sort" CommandArgument="date_added">
                                    <div class="flex flex-row justify-between items-center p-2" >
                                        <p>Date Added</p>
                                        <i id="iconDateAdded" class="fa-solid fa-sort-down sort-icon relative " style="top:-3px"></i>

                                    </div>

                                    </asp:LinkButton>
                                </td>

                                <td class="col-span-1 flex justify-end">
                                    <p>Action</p>
                                </td>

                                <tr id="itemPlaceholder" runat="server"></tr>
                        </tbody>
                        <tfoot>
                            <!-- footer for pagination ( WILL CHANGE TO physical button later) -->
                            <tr class="">
                                <td class="flex flex-row text-gray-400 justify-between rounded-b-lg bg-white items-center">

                                    <asp:Label ID="pageNumFoot" runat="server" Text="Showing 1-10 from 100" class="text-normal text-base p-5"></asp:Label>
                                    <div class="p-4 text-base flex flex-row gap-3">
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

                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="grid grid-cols-8 gap-6 w-full mb-5 p-4 border-b-2" style="color: #8B8E99">
                    <td class="col-span-4 flex flex-row gap-2 items-center">
                        <asp:Image ID="productImages" runat="server" AlternateText="Product Image" Height="64" Width="64"
                            ImageUrl='<%# Eval("CategoryBanner", "{0}") %>' CssClass="rounded border" />
                        <div class="flex flex-col gap-2">
                            <span class="text-black font-bold">
                                <%# Eval("CategoryName") %>  
                            </span>
                            <span>Total Products : <%# Eval("numberOfProd") %> 
                            </span>
                        </div>

                    </td>
                    <td class="col-span-1 flex items-center justify-center"><%# Eval("Sold") %></td>
                    <td class="col-span-1 flex items-center justify-center"><%# Eval("Stock") %></td>
                    <td class="col-span-1 flex items-center justify-center"><%# Eval("date_added", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-1 flex justify-end items-center">
                        <div class="flex flex-row gap-4 items-center">


                            <asp:LinkButton ID="editItem" runat="server" CommandName="EditCategory" CommandArgument='<%# Eval("categoryID") %>'>                            
                             <i class="fa-solid fa-pen"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="deleteItem" runat="server" CommandName="DeleteCategory" CommandArgument='<%# Eval("categoryID") %>'>                            
                                <i class="fa-solid fa-trash"></i>
                            </asp:LinkButton>
                        </div>
                    </td>
                </tr>

            </ItemTemplate>

        </asp:ListView>

        <asp:Panel ID="popUpDelete" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center ">
            <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
            <!-- Modal content -->
            <div class="popUp-content w-1/3 h-fit flex flex-col bg-white p-5 rounded-xl flex flex-col gap-3 drop-shadow-lg">

                <div class="grid grid-cols-3 w-full h-fit justify-center flex p-0">
                    <div>
                    </div>
                    <p class="text-2xl text-red-600 font-bold text-center">WARNING</p>
                    <span class="w-auto flex items-center justify-end text-3xl rounded-full">


                        <asp:LinkButton ID="closePopUp" runat="server" OnClick="closePopUp_Click">
                   <i class=" fa-solid fa-xmark"></i>
                        </asp:LinkButton>

                    </span>

                </div>
                <div class="flex flex-col justify-center items-center gap-5">

                    <div style="font-size: 64px">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Admin/Orders/Images/trash.gif" AlternateText="trashcan" CssClass="w-28 h-28 " />

                    </div>
                    <p class="bold text-lg break-normal text-center">Are you sure you want to delete the following Category?</p>
                    <p class="bold text-lg">
                        <asp:Label ID="lblItemInfo" runat="server" Text="[OrderID]"></asp:Label>
                    </p>
                    <asp:TextBox ID="passwordForDelete" runat="server" TextMode="Password" CssClass="p-2 px-4 border rounded-xl" placeholder="Enter password to confirm"></asp:TextBox>
                    <div>

                        <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" CssClass="bg-gray-300 p-2 px-4 rounded-lg cursor-pointer" OnClick="btnCancelDelete_Click" />
                        <asp:Button ID="btnConfirmDelete" OnClick="btnConfirmDelete_click" runat="server" Text="Delete" CssClass="bg-red-400 p-2 px-4 rounded-lg cursor-pointer" />
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
    </div>
</asp:Content>
