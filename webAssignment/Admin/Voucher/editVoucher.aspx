<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="editVoucher.aspx.cs" Inherits="webAssignment.Admin.Voucher.editVoucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First Row-->
    <div class="">


        <div class="flex flex-row justify-between font-medium pt-3 items-center mb-3">
            <div class="flex flex-col w-3/5">
                <div class="text-2xl font-bold ">
                    <p>
                        Edit Voucher
                    </p>
                </div>
                <div class="flex flex-row text-sm py-2">
                    <div class="text-blue-600">Dashboard</div>
                    <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                    <div class="text-blue-600">Voucher</div>
                    <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                    <div>Edit Voucher</div>
                </div>
            </div>
            <div class="flex w-full justify-end mb-3">
                <div class="relative">
                    <i class="fa-solid fa-download text-blue-500 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                    <asp:Button ID="saveChanges" runat="server" Text="Save Changes" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg" />
                </div>
            </div>
        </div>

        <!-- Second Row -->
        <div class="grid grid-cols-6 gap-4 mt-5 mb-5">
            <div class="col-span-3 text-sm bg-white rounded-xl">
                <!-- General Description -->
                <div class="p-5 flex flex-col ">
                    <span class="mb-7 text-lg">Basic information For Voucher:<asp:Label ID="lblVoucherCodeID" runat="server" Text="[voucherCodeID]"></asp:Label></span>
                    <div class="flex flex-col gap-7">
                        <div class="grid grid-cols-2 items-center">
                            <div class="flex gap-4  items-center">
                                <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                                    <i class="fa-solid fa-ticket text-lg"></i>
                                </div>
                                <span class=" text-base">Voucher Code</span>
                            </div>
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100" ID="editVoucherCode" runat="server" ToolTip="Category Name" placeholder="Type Voucher Code..."></asp:TextBox>
                        </div>
                        <div class="grid grid-cols-2 items-center">
                            <div class="flex gap-4  items-center">
                                <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                                    <i class="fa-solid fa-percent"></i>
                                </div>
                                <span class="text-base">Discount Rate (0-100%)</span>
                            </div>
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100" ID="TextBox1" runat="server" ToolTip="Category Name" placeholder="Type Discount Rate ..." TextMode="Number" min="0"></asp:TextBox>
                        </div>

                    </div>
                </div>
            </div>

            <div class="col-span-3 text-sm">
                <!-- General Description -->
                <div class="p-5 flex flex-col bg-white rounded-xl">
                    <span class="mb-7 text-lg">Terms and Condition</span>
                    <div class="flex flex-col gap-7">
                        <div class="grid grid-cols-2 items-center">
                            <div class="flex gap-4  items-center">
                                <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                                    <i class="fa-solid fa-plus text-lg"></i>
                                </div>
                                <span class="mb-2 text-base">Maximum Discount Amount</span>
                            </div>
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="maxTb" runat="server" ToolTip="Category Name" placeholder="Type Voucher Code..." TextMode="Number" min="0"></asp:TextBox>
                        </div>
                        <div class="grid grid-cols-2 items-center">
                            <div class="flex gap-4  items-center">
                                <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                                    <i class="fa-regular fa-square-minus text-lg"></i>
                                </div>
                                <span class="mb-2 text-base">Minimum require Amount</span>
                            </div>
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="minTb" runat="server" ToolTip="Category Name" placeholder="Type Discount Rate ..." TextMode="Number" min="0"></asp:TextBox>
                        </div>

                    </div>
                </div>
            </div>

        </div>
        <div class="grid grid-cols-6 gap-4">


            <div class="col-span-3 text-sm">
                <!-- General Description -->
                <div class="p-5 flex flex-col bg-white rounded-xl">
                    <span class="mb-7 text-lg">Durations</span>
                    <div class="flex flex-col gap-7">
                        <div class="grid grid-cols-2 items-center">
                            <div class="flex gap-4  items-center">
                                <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                                    <i class="fa-regular fa-calendar-days text-lg"></i>
                                </div>

                                <span class="mb-2 text-base">Start Date</span>
                            </div>
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="startDateTb" runat="server" ToolTip="Category Name" placeholder="Type Voucher Code..." TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="grid grid-cols-2 items-center">
                            <div class="flex gap-4  items-center">
                                <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                                    <i class="fa-regular fa-calendar-days text-lg"></i>
                                </div>
                                <span class="mb-2 text-base">Expiry Date</span>
                            </div>
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="expireDateTb" runat="server" ToolTip="Category Name" placeholder="Type Discount Rate ..." TextMode="Date">
                            </asp:TextBox>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
