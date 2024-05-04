<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="voucher.aspx.cs" Inherits="webAssignment.Admin.Voucher.voucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">
        <div class="flex flex-col">
            <div class="text-2xl font-bold ">
                <p>Voucher</p>
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
            <div class="text-sm relative ml-2 bg-blue-500 text-white flex flex-row items-center p-1 px-2 gap-2 rounded-lg">
                <i class="text-lg fa-solid fa-plus left-4 top-5 text-white"></i>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/Voucher/addVoucher.aspx">Add New Voucher</asp:HyperLink>
            </div>

        </div>
    </div>
    <!--End-->

    <!--Second Row-->
    <div class="flex flex-row justify-between text-sm text-gray-600 font-medium my-4 justify-self-center">
        <div class="grid grid-cols-5 bg-white gap-3 text-center rounded p-2">

            <div class="col-span-1 ">
                <asp:Button ID="allVoucherFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 text-blue-600 bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="All" OnClick="allVoucherFilter_Click" />
            </div>
            <div class="col-span-1">
                <asp:Button ID="ogFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="On Going" OnClick="ogFilter_Click" />
            </div>            
            <div class="col-span-1">
                <asp:Button ID="pendingFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 rounded-lg cursor-pointer"  runat="server" Text="Pending" OnClick="pendingFilter_Click" />
            </div>
            <div class="col-span-1">
                <asp:Button ID="expiredFilter" CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 rounded-lg cursor-pointer" runat="server" Text="Expired" OnClick="expiredFilter_Click" />

            </div>
            <div class="col-span-1">
                <asp:Button ID="fullyClaimFilter"  CssClass="w-full px-3 py-2 hover:text-blue-600 hover:bg-gray-100 rounded-lg cursor-pointer"  runat="server" Text="Fully Claimed" OnClick="fullyClaimFilter_Click" />
            </div>
        </div>
        <div class="flex items-center gap-3">
            <div class="">
                <asp:LinkButton ID="filterDateBtn" runat="server" class="p-3 border border-gray-200 rounded-lg bg-white flex gap-3 items-center"> <i class="fa-solid fa-calendar-days"></i><span>Select Date </span></asp:LinkButton>
            </div>
        </div>
    </div>
    <!--End-->

    <div class="bg-white p-5 text-base rounded-lg drop-shadow-lg">

        <%-- voucher list view --%>
        <asp:ListView ID="voucherListView" runat="server" OnItemCommand="voucherListView_ItemCommand" OnSorting="voucherListView_Sorting" OnDataBound="voucherListView_DataBound">
            <EmptyDataTemplate>
                <table class="orders-table w-full ">
                    <tr class="w-full ">
                        <td>
                            <div class="flex flex-col justify-center items-center">
                                <asp:Image ID="sadKermit" runat="server" ImageUrl="~/Admin/Category/sad_kermit.png" AlternateText="Product Image" Height="128" Width="128" />
                                <span>No Voucher Found</span>
                            </div>
                        </td>
                    </tr>

                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <div style="overflow-x: auto">
                    <table class="orders-table w-full " style="overflow-x: auto; min-width: 1450px">
                        <!-- Headers here -->
                        <tbody>
                        <tr class="grid grid-cols-11 gap-6 px-4 py-2 rounded-lg  items-center bg-gray-100 mb-3">
                            <td class="col-span-2 hover:bg-white hover:text-black rounded-lg">
                                <asp:LinkButton ID="filterVoucherCode" runat="server" CommandName="Sort" CommandArgument="voucher_id">
                                  <div class="flex flex-row justify-between items-center p-2">
                                                     <p>Voucher Code </p>
                                        <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                                    
                                 </div>

                                </asp:LinkButton>
                            </td>
                            <td class="col-span-1  text-center">
                                <p>Discount</p>
                            </td>
                            <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                <asp:LinkButton ID="capAtFilter" runat="server" CommandName="Sort" CommandArgument="cap_at">
                                  <div class="flex flex-row justify-between items-center p-2">
                                                     <p>Max </p>
                        <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                                    
                                 </div>

                                </asp:LinkButton>
                            </td>

                            <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                <asp:LinkButton ID="minFilter" runat="server" CommandName="Sort" CommandArgument="min_spend">
                                  <div class="flex flex-row justify-between items-center p-2">
                                                     <p>Min </p>
                        <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>
                                    
                                 </div>

                                </asp:LinkButton>
                            </td>
                            <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                <asp:LinkButton ID="filterStartDate" runat="server" CommandName="Sort" CommandArgument="started_date">
                              <div class="flex flex-row justify-between items-center p-2">
                                    <p>Start Date </p>
                                    <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>

                              </div>
                                </asp:LinkButton>
                            </td>
                            <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                <asp:LinkButton ID="filterExpiredDate" runat="server">
                              <div class="flex flex-row justify-between items-center p-2" CommandName="Sort" CommandArgument="expiry_date">
                                    <p>Expiry </p>
                                    <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>

                              </div>
                                </asp:LinkButton>
                            </td>
                            <td class="col-span-2  text-center">
                                <p>Status</p>
                            </td>
                            <td class="col-span-1 hover:bg-white hover:text-black rounded-lg">
                                <asp:LinkButton ID="filterDateCreated" runat="server" CommandName="Sort" CommandArgument="added_date">
                                <div class="flex flex-row justify-between items-center p-2">
                                    <p>Date Created</p>
                                    <i class="fa-solid fa-sort-down relative" style="top:-3px"></i>

                                </div>

                                </asp:LinkButton>
                            </td>


                            <td class="col-span-1 flex justify-end">
                                <p>Action</p>
                            </td>

                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                        </tbody>
                        <tfoot>
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
                <tr class="grid grid-cols-11 gap-6 w-full mb-5 p-4 border-b-2" style="color: #8B8E99">

                    <td class="col-span-2 flex flex-row gap-2 items-center text-black">
                        <%# Eval("voucher_id") %>
                    </td>
                    <td class="col-span-1 flex items-center px-2 justify-center"><%# Eval("discount_rate") %>%</td>
                    <td class="col-span-1 flex items-center px-2 justify-center"><%# Eval("cap_at") %></td>
                    <td class="col-span-1 flex items-center px-2 justify-center"><%# Eval("min_spend") %></td>
                    <td class="col-span-1 flex items-center px-2 justify-center"><%# Eval("added_date", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-1 flex items-center px-2 justify-center"><%# Eval("expiry_date", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-2">
                        <div class=' flex justify-center items-center text-center'>
                            <span class='<%# Eval("voucher_status").ToString() == "OnGoing" ? "bg-green-200" : Eval("voucher_status").ToString() == "Expired" ? "bg-red-200" : "bg-gray-200" %> w-3/4 rounded-xl p-3 '>
                                <%# Eval("voucher_status") %>
                            </span>
                        </div>
                    </td>
                    <td class="col-span-1 flex items-center px-2 justify-center"><%# Eval("added_Date", "{0:dd MMM yyyy}") %></td>
                    <td class="col-span-1 flex justify-end items-center">
                        <div class="flex flex-row gap-4 items-center">
                            <asp:LinkButton ID="editItem" runat="server" CommandName="editVoucher" CommandArgument='<%# Eval("voucher_id") %>'>                            
                                <i class="fa-solid fa-pen"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="deleteItem" runat="server" CommandName="deleteVoucher" CommandArgument='<%# Eval("voucher_id") %>'>                            
                                <i class="fa-solid fa-trash"></i>
                            </asp:LinkButton>

                        </div>
                    </td>

                </tr>

            </ItemTemplate>

        </asp:ListView>
    </div>
    <%-- pop up delete confirmations --%>
    <asp:Panel ID="popUpDelete" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center ">
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

    <%-- pop up for datepicker --%>


    <!--End-->
</asp:Content>
