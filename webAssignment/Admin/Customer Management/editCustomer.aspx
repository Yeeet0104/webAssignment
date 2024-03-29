<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="editCustomer.aspx.cs" Inherits="webAssignment.Admin.Customer.editCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function previewImage(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    var img = document.getElementById('<%= profilePic.ClientID %>');
                    img.src = e.target.result;
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 pb-5 items-center">
        <div class="flex flex-col">
            <div class="text-2xl font-bold">Customer Details</div>
            <div class="flex flex-row text-sm p-1">
                <div class="text-blue-600">Dashboard</div>
                <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                <div class="text-blue-600">Customers List</div>
                <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                <div>Edit Customer</div>
            </div>
        </div>
        <div class="flex">
            <div class="relative mr-2">
                <i class="fa-solid fa-xmark absolute text-white text-xl left-5 top-5 pt-1 transform -translate-y-1/2"></i>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="pl-11 pr-5 py-2.5 text-sm bg-red-500 text-white rounded-lg" />
            </div>
            <div class="text-sm relative ml-2 bg-blue-500 text-white flex flex-row items-center px-4 gap-2 rounded-lg ">
                <i class="fa-solid fa-plus text-white text-lg left-2 top-5"></i>
                <asp:HyperLink ID="btnSaveCustomer" runat="server" NavigateUrl="~/Admin/Customer Management/customerManagement.aspx">Save Details</asp:HyperLink>
            </div>
        </div>
    </div>
    <!--End-->

    <!--Second Row-->
    <div class="flex flex-row my-4">
        <div>
            <!--Profile Pic container -->
            <div class="bg-white drop-shadow-lg rounded-xl" style="width: 264px; height: 380px;">
                <div class="flex flex-col p-6">
                    <span class="pb-2 font-medium">Profile Picture</span>
                    <span class="text-lg font-medium text-gray-400">Photo</span>

                    <!--Pic Box-->
                    <div class="py-1">
                        <asp:Image ID="profilePic" runat="server" ImageUrl="~/Admin/Layout/image/DexProfilePic.jpeg" Height="216" Width="216" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();"/>
                    </div>
                    <div class="flex justify-center pt-3">
                        <asp:FileUpload ID="fileUpload" runat="server" Style="cursor: pointer;" onchange="previewImage(this);"/>
                    </div>
                </div>
            </div>

            <!--Status Section-->
            <div class="bg-white drop-shadow-lg rounded-xl mt-6 p-4" style="width: 264px; height: 150px;">
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

        <!--Customer Details-->
        <div class="bg-white drop-shadow-lg w-full h-full ml-8 rounded-xl px-10 py-5">
            <div class="flex flex-col">
                <span class="text-black pb-2 text-2xl font-medium">General Information</span>
                <div class="w-full">
                    <div class="flex flex-row pb-4">
                        <div class="w-1/2 pr-8">
                            <span class="text-sm font-medium text-gray-600">Name</span>
                            <asp:TextBox ID="txtEditName" runat="server" Text="Yap Zi Yan" class="w-full font-medium bg-gray-100 p-3 rounded-xl"></asp:TextBox>
                        </div>
                        <div class="w-1/2 pl-8">
                            <span class="text-sm font-medium text-gray-600">Email</span>
                            <asp:TextBox ID="txtEditEmail" runat="server" Text="zyyap69@gmail.com" class="w-full font-medium bg-gray-100 p-3 rounded-xl"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="w-full">
                    <div class="pb-6">
                        <div class="w-1/2 justify-start pr-8">
                            <div class="flex flex-col">
                                <span class="text-sm font-medium text-gray-600">Phone Number</span>
                                <asp:TextBox ID="txtEditPhoneNo" runat="server" Text="0123136742" class="w-full font-medium bg-gray-100 p-3 rounded-xl"></asp:TextBox>
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
                            <asp:ListItem Value="6"></asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem Value="9"></asp:ListItem>
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
