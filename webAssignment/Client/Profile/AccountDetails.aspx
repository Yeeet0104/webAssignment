<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ProfileMaster/ProfileMasterPage.master" AutoEventWireup="true" CodeBehind="AccountDetails.aspx.cs" Inherits="webAssignment.Client.Profile.AccountDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
        function triggerFileUpload() {
            document.getElementById('<%= fileUpload.ClientID %>').click();
            return false; // Prevent postback on LinkButton click
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div>
        <!--Acc Settings-->
        <div class="shadow-lg border border-gray-200 p-4 rounded-lg  bg-white">
            <span class="font-medium pl-2">ACCOUNT SETTINGS</span>
            <div class="border-b border-gray-200 p-1.5 mb-2"></div>
            <div class="flex py-2 ">
                <!--image col-->
                <div class="pl-2 pr-6 w-1/3 pt-2">
                    <div class="flex justify-center items-center">
                        <asp:Image ID="profilePic" ImageUrl="~\Admin\Layout\image\DexProfilePic.jpeg" CssClass="rounded-full w-[230px] drop-shadow-lg" Height="230" runat="server" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();" />
                    </div>

                    <div class="flex justify-center pt-6">
                        <asp:FileUpload ID="fileUpload" runat="server" Style="cursor: pointer; display: none" onchange="previewImage(this);" />
                        <button type="button" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();" class="bg-blue-500 text-white w-full py-1 rounded-lg cursor-pointer hover:bg-blue-600">
                            Choose File
                        </button>
                    </div>
                </div>
                <!--general details-->
                <div class="w-2/3">
                    <div class="grid grid-cols-2 gap-5 mb-4">
                        <!-- first row-->
                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">First Name</span>
                                <asp:TextBox ID="txtFirstName" runat="server" Text="Feyz" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">Last Name</span>
                                <asp:TextBox ID="txtLastName" runat="server" Text="Ibrahim" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                            </div>
                        </div>

                        <!-- second row-->
                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">Username</span>
                                <asp:TextBox ID="txtUsername" runat="server" Text="feyzibrahim" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">Email</span>
                                <asp:TextBox ID="txtEmail" runat="server" Text="feyzibrahim@gmail.com" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                            </div>
                        </div>

                        <!-- third row-->
                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">Phone Number</span>
                                <asp:TextBox ID="txtPhoneNo" Text="0123136742" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                            </div>
                        </div>

                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">Country</span>
                                <asp:DropDownList ID="ddlCountry" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5">
                                    <asp:ListItem>Australia</asp:ListItem>
                                    <asp:ListItem>Canada</asp:ListItem>
                                    <asp:ListItem>China</asp:ListItem>
                                    <asp:ListItem>India</asp:ListItem>
                                    <asp:ListItem>Malaysia</asp:ListItem>
                                    <asp:ListItem>Singapore</asp:ListItem>
                                    <asp:ListItem>Taiwan</asp:ListItem>
                                    <asp:ListItem>United Kingdom</asp:ListItem>
                                    <asp:ListItem>United State of America</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <!--fourth row-->
                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">State</span>
                                <asp:DropDownList ID="ddlState" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5">
                                    <asp:ListItem>Kedah</asp:ListItem>
                                    <asp:ListItem>Wilayah Persekutuan</asp:ListItem>
                                    <asp:ListItem>Terengganu</asp:ListItem>
                                    <asp:ListItem>Penang</asp:ListItem>
                                    <asp:ListItem>Melaka</asp:ListItem>
                                    <asp:ListItem>Johor</asp:ListItem>
                                    <asp:ListItem>Perak</asp:ListItem>
                                    <asp:ListItem>Sabah</asp:ListItem>
                                    <asp:ListItem>Sarawak</asp:ListItem>
                                    <asp:ListItem>Perlis</asp:ListItem>
                                    <asp:ListItem>Pahang</asp:ListItem>
                                    <asp:ListItem>Negeri Sembilan</asp:ListItem>
                                    <asp:ListItem>Selangor</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">City</span>
                                <asp:DropDownList ID="ddlCity" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5">
                                    <asp:ListItem>Kuala Lumpur</asp:ListItem>
                                    <asp:ListItem>Petaling Jaya</asp:ListItem>
                                    <asp:ListItem>Gombak</asp:ListItem>
                                    <asp:ListItem>Kepong</asp:ListItem>
                                    <asp:ListItem>Subang</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <!--fifth row-->
                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">Zip Code</span>
                                <asp:TextBox ID="txtZipCode" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <!--DOB-->
                    <span>Birthday</span>
                    <div class="mt-1.5 grid grid-cols-3 gap-7">

                        <asp:DropDownList ID="ddlDay" runat="server" class="w-full border-2 border-gray-200 font-medium p-3 rounded-sm">
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

                        <asp:DropDownList ID="ddlMonth" runat="server" class="w-full border-2 border-gray-200 font-medium p-3 rounded-sm">
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

                        <asp:DropDownList ID="ddlYear" runat="server" class="w-full border-2 border-gray-200 font-medium p-3 rounded-sm">
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
                    <div class="w-auto pt-7">
                        <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" class="bg-blue-600 text-white rounded-lg px-8 py-3" />
                    </div>
                </div>
            </div>
        </div>

        <!--Change Password-->
        <div class="shadow-lg border border-gray-200 p-4 rounded-lg mt-5  bg-white">
            <span class="font-medium pl-2">CHANGE PASSWORD</span>
            <div class="border-b border-gray-200 p-1.5 mb-3"></div>

            <div class="flex flex-col">
                <div class="w-full py-3">
                    <div class="flex flex-col relative">
                        <span>Current Password</span>
                        <asp:TextBox ID="txtCurrentPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                        <i class="fa-solid fa-eye absolute right-4 top-2/3 transform -translate-y-1/2 text-black"></i>
                    </div>
                </div>


                <div class="flex flex-col">
                    <div class="w-full py-3">
                        <div class="flex flex-col relative">
                            <span>New Password</span>
                            <asp:TextBox ID="txtNewPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            <i class="fa-solid fa-eye absolute right-4 top-2/3 transform -translate-y-1/2 text-black"></i>
                        </div>
                    </div>
                </div>

                <div class="flex flex-col">
                    <div class="w-full py-3">
                        <div class="flex flex-col relative">
                            <span>Confirm Password</span>
                            <asp:TextBox ID="txtConfirmPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            <i class="fa-solid fa-eye absolute right-4 top-2/3 transform -translate-y-1/2 text-black"></i>
                        </div>
                    </div>
                </div>
                <div class="w-auto py-3">
                    <asp:Button ID="btnChangePass" runat="server" Text="Change Password" class="bg-blue-600 text-white rounded-lg px-12 py-3" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
