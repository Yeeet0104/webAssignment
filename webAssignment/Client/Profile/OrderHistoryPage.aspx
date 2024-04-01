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
            <asp:ListView ID="lvOrder" runat="server">
                <LayoutTemplate>
                        <tr id="itemPlaceholder" runat="server"></tr>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="grid grid-cols-5 flex items-center p-4 font-semibold">
                        <td class="col-span-1 flex items-center gap-2">
                            <span>
                                <%# Eval("OrderID") %>
                            </span>
                        </td>
                        <td class="col-span-1 font-bold">
                            <script>
                                var status = "<%# Eval("Status") %>";
                                if (status === "Pending") {
                                    document.write('<span class="text-orange-500">' + status + '</span>');
                                } else if (status == "Cancelled") {
                                    document.write('<span class="text-red-500">' + status + '</span>');
                                } else if (status == "Shipped") {
                                    document.write('<span class="text-green-500">' + status + '</span>');
                                }
                            </script>
                        </td>
                        <td class="col-span-1"><%# ((DateTime)Eval("Date")).ToShortDateString() %></td>
                        <td class="col-span-1"><%# Eval("Total", "{0:C}") %></td>
                        <td class="col-span-1">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Client/Profile/OrderHistoryDetailsPage.aspx" class="text-blue-500 hover:text-blue-800">
                                <span>View Details</span>
                                 <i class="fa-solid fa-circle-chevron-right ml-1"></i>
                            </asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </table>
    </div>
</asp:Content>
