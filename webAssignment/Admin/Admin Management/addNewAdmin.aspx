﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="addNewAdmin.aspx.cs" Inherits="webAssignment.Admin.Admin_Management.addNewAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    function triggerFileUpload() {
        var fileUpload = document.getElementById('<%= choosePic.ClientID %>');
        if (fileUpload) {
            fileUpload.click();
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First Row-->
    <div class="flex flex-row justify-between font-medium">
        <div class="flex flex-col">
            <div class="text-black text-lg">Add New Admin</div>
            <div class="flex flex-row text-sm p-1">
                <div class="text-blue-600">Dashboard</div>
                <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                <div class="text-blue-600">Admin List</div>
                <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                <div>Add New Admin</div>
            </div>
        </div>
        <div class="flex">
            <div class="relative mr-2 mt-0.5">
                <i class="fa-solid fa-xmark text-gray-400 absolute text-2xl left-3 top-5 transform -translate-y-1/2"></i>
                <asp:HyperLink ID="linkBtnCancel" runat="server" NavigateUrl="~/Admin/Admin Management/adminManagement.aspx" class="pl-8 pr-4 py-1.5 text-lg border-2 text-gray-400 border-gray-400 rounded-lg">Cancel</asp:HyperLink>
            </div>
            <div class="relative ml-2">
                <i class="fa-solid fa-floppy-disk absolute text-2xl left-4 top-5 text-white transform -translate-y-1/2"></i>
                <asp:Button ID="btnAddAdmin" runat="server" Text="Add Admin" class="pl-11 pr-5 py-2.5 text-sm bg-blue-500 text-white rounded-lg" />
            </div>
        </div>
    </div>
    <!--End-->

    <!--Second Row-->
    <div class="flex flex-row pt-3">
        <div>
            <!--Profile Pic container -->
            <div class="bg-white rounded-xl" style="width: 264px; height: 380px;">
                <div class="flex flex-col p-6">
                    <span class="pb-2 font-medium">Profile Picture</span>
                    <span class="text-lg font-medium text-gray-400">Photo</span>

                    <!--Pic Box-->
                    <div class="py-1">
                        <asp:FileUpload ID="choosePic" runat="server" Style="display: none;" />
                        <asp:Image ID="profilePic" runat="server" Height="216" Width="216" OnClick="triggerFileUpload()" Style="cursor: pointer;" />
                    </div>
                    <div class="flex justify-center pt-3">
                        <asp:Button ID="btnUploadImg" runat="server" Text="Upload Image" class="bg-blue-100 rounded-lg text-center px-5 py-2 text-sm font-medium text-blue-600" OnClick="btnNewProfileImg_Click" />
                    </div>
                </div>
            </div>

            <!--Status Section-->
            <div class="bg-white rounded-xl mt-4 p-4" style="width: 264px; height: 150px;">
                <div class="flex flex-col">
                    <div class="flex flex-row justify-between">
                        <span class="font-medium">Status</span>
                        <asp:Label ID="lblStatus" runat="server" Text="Active" class="rounded-xl font-medium flex items-center px-3 py-1.5 text-sm text-green-700 bg-green-100"></asp:Label>
                    </div>
                    <span class="font-medium text-lg pt-2 pb-1 text-gray-400">Customer Status</span>
                    <asp:DropDownList ID="ddlStatus" runat="server" class="w-full font-medium bg-gray-100 px-3 py-2 rounded-xl" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem>Active</asp:ListItem>
                        <asp:ListItem>Blocked</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <!--End-->

        <!--Admin Details-->
        <div class="bg-white h-full w-full ml-8 rounded-xl px-10 py-5">
            <div class="flex flex-col">
                <span class="text-black pb-2 text-3xl font-medium">General Information</span>
                <div class="w-full">
                    <div class="flex flex-row pb-4">
                        <div class="w-1/2 pr-8">
                            <span class="text-sm font-medium text-gray-600">Name</span>
                            <asp:TextBox ID="txtEditName" runat="server" Text="" placeholder="Type Admin's Name..." class="w-full font-medium bg-gray-100 p-3 rounded-xl"></asp:TextBox>
                        </div>
                        <div class="w-1/2 pl-8">
                            <span class="text-sm font-medium text-gray-600">Email</span>
                            <asp:TextBox ID="txtEditEmail" runat="server" Text="" placeholder="Type Admin's Email..." class="w-full font-medium bg-gray-100 p-3 rounded-xl"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="w-full">
                    <div class="pb-6">
                        <div class="w-1/2 justify-start pr-8">
                            <div class="flex flex-col">
                                <span class="text-sm font-medium text-gray-600">Phone Number</span>
                                <asp:TextBox ID="txtEditPhoneNo" runat="server" Text="" placeholder="Type Admin's Number..." class="w-full font-medium bg-gray-100 p-3 rounded-xl"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <!--DOB-->
                <span class="text-lg">Date Of Birth</span>
                <div class="w-full flex flex-row pb-5">
                    <div class="w-1/3 pr-8">
                        <span class="text-sm font-medium text-gray-600">Day</span>
                        <asp:DropDownList ID="ddlDay" runat="server" class="w-full font-medium bg-gray-100 p-3 rounded-xl">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem>23</asp:ListItem>
                            <asp:ListItem>24</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>26</asp:ListItem>
                            <asp:ListItem>27</asp:ListItem>
                            <asp:ListItem>28</asp:ListItem>
                            <asp:ListItem>29</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>31</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="w-1/3 px-8">
                        <span class="text-sm font-medium text-gray-600">Month</span>
                        <asp:DropDownList ID="ddlMonth" runat="server" class="w-full font-medium bg-gray-100 p-3 rounded-xl">
                            <asp:ListItem>January</asp:ListItem>
                            <asp:ListItem>February</asp:ListItem>
                            <asp:ListItem>March</asp:ListItem>
                            <asp:ListItem>April</asp:ListItem>
                            <asp:ListItem>May</asp:ListItem>
                            <asp:ListItem>June</asp:ListItem>
                            <asp:ListItem>July</asp:ListItem>
                            <asp:ListItem>August</asp:ListItem>
                            <asp:ListItem>September</asp:ListItem>
                            <asp:ListItem>October</asp:ListItem>
                            <asp:ListItem>November</asp:ListItem>
                            <asp:ListItem>December</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="w-1/3 pl-8">
                        <span class="text-sm font-medium text-gray-600">Year</span>
                        <asp:DropDownList ID="ddlYear" runat="server" class="w-full font-medium bg-gray-100 p-3 rounded-xl">
                            <asp:ListItem>1996</asp:ListItem>
                            <asp:ListItem>1997</asp:ListItem>
                            <asp:ListItem>1998</asp:ListItem>
                            <asp:ListItem>1999</asp:ListItem>
                            <asp:ListItem>2000</asp:ListItem>
                            <asp:ListItem>2001</asp:ListItem>
                            <asp:ListItem>2002</asp:ListItem>
                            <asp:ListItem>2003</asp:ListItem>
                            <asp:ListItem>2004</asp:ListItem>
                            <asp:ListItem>2005</asp:ListItem>
                            <asp:ListItem>2006</asp:ListItem>
                            <asp:ListItem>2007</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div>
                </div>
            </div>
        </div>
    </div>
    <!--End-->
</asp:Content>
