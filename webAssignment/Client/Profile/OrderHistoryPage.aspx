<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ProfileMaster/ProfileMasterPage.master" AutoEventWireup="true" CodeBehind="OrderHistoryPage.aspx.cs" Inherits="webAssignment.Client.Profile.OrderHistoryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
     <div class="rounded-lg shadow-xl border border-gray-300  bg-white pb-4">
        <table class="orders-table w-full">
            <p class="text-xl text-gray-900 font-bold p-4">Order History</p>
            <div class="grid grid-cols-5 p-4 bg-gray-200 font-bold text-gray-600 text-sm flex items-center">
                <div class="col-span-1">
                    <p>Order ID</p>
                </div>
                <div class="col-span-1">
                    <p>Status</p>
                </div>
                <div class="col-span-1">
                    <p>Date</p>
                </div>
                <div class="col-span-1">
                    <p>Total</p>
                </div>
                <div class="col-span-1">
                    <p>Action</p>
                </div>
            </div>
            <asp:ListView ID="lvOrder" runat="server" OnItemCommand="orderListView_ItemCommand">
                <LayoutTemplate>
                        <tr id="itemPlaceholder" runat="server"></tr>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="grid grid-cols-5 flex items-center p-4 font-semibold">
                        <td class="col-span-1 flex items-center gap-2">
                            <span>
                                <%# Eval("orderID") %>
                            </span>
                        </td>
                        <td class="col-span-1 font-bold">
                            <script>
                                var status = "<%# Eval("status") %>";
                                status = status.toUpperCase();
                                if (status === "PENDING") {
                                    document.write('<span class="text-amber-500">' + status + '</span>');
                                } else if (status == "CANCELLED") {
                                    document.write('<span class="text-rose-600">' + status + '</span>');
                                } else if (status == "DELIVERED") {
                                    document.write('<span class="text-emerald-500">' + status + '</span>');
                                } else if (status == "PACKED"){
                                    document.write('<span class="text-blue-700">' + status + '</span>');
                                }
                                else if (status == "ON THE ROAD") {
                                    document.write('<span class="text-purple-600">' + status + '</span>');
                                }
                            </script>
                        </td>
                        <td class="col-span-1"><%# ((DateTime)Eval("date")).ToShortDateString() %></td>
                        <td class="col-span-1">RM <%# Eval("total") %></td>
                        <td class="col-span-1">
                            <asp:LinkButton ID="lbtnViewDetails" runat="server" CommandArgument='<%# Eval("orderID") %>' CommandName="orderDetails" class="text-blue-500 hover:text-blue-800 cursor-pointer">
                                <span class="">View Details</span>
                                 <i class="fa-solid fa-circle-chevron-right ml-1"></i>
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </table>
    </div>
</asp:Content>
