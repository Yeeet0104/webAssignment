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
                        Edit Voucher :[<asp:Label ID="lblvoucherCode" runat="server" Text="[voucherCode]"></asp:Label>]
                    </p>
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
            <div class="flex w-full justify-end mb-3">
                <div class="relative ml-2 cursor-pointer">
                    <i class="fa-solid fa-plus absolute text-2xl left-4 top-5 text-white transform -translate-y-1/2"></i>
                    <asp:Button ID="edtVoucher" runat="server" Text="Edit Voucher" class="pl-11 pr-5 py-2.5 text-sm bg-blue-500 text-white rounded-lg cursor-pointer" OnClick="edtVoucher_Click"   />
                </div>
            </div>
        </div>

        <!-- Second Row -->
        <div class="grid grid-cols-6 gap-4 mt-5 mb-5">
            <div class="col-span-3 text-sm bg-white rounded-xl">
                <!-- General Description -->
                <div class="p-5 flex flex-col ">
                    <span class="mb-7 text-lg">Basic information For Voucher</span>
                    <div class="flex flex-col gap-7">
                        <div class="grid grid-cols-2 items-center">
                            <div class="flex gap-4  items-center">
                                <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                                    <i class="fa-solid fa-ticket text-lg"></i>
                                </div>
                                <span class=" text-base">Voucher Code</span>
                            </div>
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100" ID="editVoucherCode" runat="server" ToolTip="Category Name" placeholder="Type Voucher Code..." ReadOnly="true" ></asp:TextBox>
                        </div>
                        <div class="grid grid-cols-2 items-center">
                            <div class="flex gap-4  items-center">
                                <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                                    <i class="fa-solid fa-percent"></i>
                                </div>
                                <span class="text-base">Discount Rate (0-100%)</span>
                            </div>
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100" ID="discountRateTb" runat="server" ToolTip="Discount Rate" placeholder="Type Discount Rate ..." TextMode="Number" min="0"></asp:TextBox>
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
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="maxTb" runat="server" ToolTip="Category Name" placeholder="Type Max Amount..." TextMode="Number" min="0"></asp:TextBox>
                        </div>
                        <div class="grid grid-cols-2 items-center">
                            <div class="flex gap-4  items-center">
                                <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                                    <i class="fa-regular fa-square-minus text-lg"></i>
                                </div>
                                <span class="mb-2 text-base">Minimum require Amount</span>
                            </div>
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="minTb" runat="server" ToolTip="Category Name" placeholder="Type Min Amount ..." TextMode="Number" min="0"></asp:TextBox>
                        </div>

                    </div>
                </div>
            </div>

        </div>
        <div class="grid grid-cols-6 gap-4">
            <div class="col-span-3 text-sm">
                <!-- Durations -->
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
            <div class="col-span-3 text-sm">
                <!-- Durations -->
                <div class="p-5 flex flex-col bg-white rounded-xl">
                    <span class="mb-7 text-lg">Quantity Available</span>
                    <div class="flex flex-col gap-7">
                        <div class="grid grid-cols-2 items-center">
                            <div class="flex gap-4  items-center">
                                <div class="flex justify-center  items-center rounded-full bg-gray-200 w-10 h-10">
                                    <i class="fa-solid fa-cubes-stacked"></i>
                                </div>

                                <span class="mb-2 text-base">Quantity</span>
                            </div>
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="quantity" runat="server" ToolTip="Quantity" placeholder="Type Quantity..." TextMode="Number" min="0"></asp:TextBox>
                        </div>
 
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
