<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="editProfile.aspx.cs" Inherits="webAssignment.Admin.Profile.editProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--First Row-->
        <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">
            <div class="flex flex-col">
                <div class="text-2xl font-bold ">
                    <p>Profile Page</p>
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
                <div class="text-lg relative ml-2 bg-blue-500 text-white flex flex-row items-center p-1 py-2.5 px-2 gap-2 rounded-lg">

                    <i class="fa-regular fa-floppy-disk left-4 top-5 text-white mx-2"></i>
                    <asp:LinkButton ID="saveChangesBtn" runat="server" OnClick="btnEdit_Click">
                        Save Changes
                    </asp:LinkButton>
                </div>

            </div>
        </div>
        <!--Acc Settings-->
        <%--        <div>

           <span class="font-medium pl-2">ACCOUNT SETTINGS</span>
       </div>--%>
        <div class="">
            <div class="grid grid-cols-8 gap-5 ">
                <!--image col-->
                <div class="col-span-2">
                    <div class="text-xl py-2">
                        <p>Profile Picture</p>
                    </div>
                    <div class="drop-shadow bg-white p-6  rounded-xl">


                        <div class="flex justify-center items-center">
                            <asp:Image ID="profilePic" ImageUrl="~\Admin\Layout\image\DexProfilePic.jpeg" class="w-[300px]  rounded-lg" Height="300" runat="server" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();" />
                        </div>

                        <div class="flex justify-center pt-6">
                            <asp:FileUpload ID="fileUpload" runat="server" Style="cursor: pointer;" onchange="previewImage(this);" />
                        </div>
                    </div>
                </div>
                <!--general details-->
                <div class="col-span-6 ">
                    <div class="text-xl py-2">
                        <p>General Informations</p>
                    </div>
                    <div class="drop-shadow bg-white p-8 rounded-xl">

                        <div class="grid grid-cols-2 gap-5 mb-4">
                            <!-- first row-->
                            <div class="flex flex-col gap-5">
                                <div class="flex flex-col">
                                    <span class="pb-1.5">First Name</span>
                                    <asp:TextBox ID="txtFirstName" runat="server" Text="Feyz" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                                </div>
                                <div>
                                    <div class="flex flex-col">
                                        <span class="pb-1.5">Username</span>
                                        <asp:TextBox ID="txtUsername" runat="server" Text="feyzibrahim" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                                    </div>
                                </div>
                                <div>
                                    <div class="flex flex-col">
                                        <span class="pb-1.5">Phone Number</span>
                                        <asp:TextBox ID="txtPhoneNo" Text="0123136742" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                                    </div>
                                </div>
                                <div>
                                    <div class="flex flex-col">
                                        <span class="pb-1.5">State</span>
                                        <asp:DropDownList ID="DropDownList1" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5">
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
                                        <span class="pb-1.5">Zip Code</span>
                                        <asp:TextBox ID="txtZipCode" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <!-- right -->
                            <div class="flex flex-col gap-5">
                                <div class="flex flex-col">
                                    <span class="pb-1.5">Last Name</span>
                                    <asp:TextBox ID="txtLastName" runat="server" Text="Ibrahim" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                                </div>
                                <div>
                                    <div class="flex flex-col">
                                        <span class="pb-1.5">Email</span>
                                        <asp:TextBox ID="txtEmail" runat="server" Text="feyzibrahim@gmail.com" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
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
                            </div>

                        </div>
                        <!--DOB-->
                        <div>

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
                            <%--                            <div class="w-auto pt-7">
                                <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" class="bg-blue-600 text-white rounded-lg px-8 py-3" />
                            </div>--%>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <!--Change Password-->
        <div class="text-xl mb-2">
            <p>CHANGE PASSWORD</p>
        </div>
        <div class="drop-shadow bg-white p-4 rounded-xl">

            <div class="flex flex-col">
                <div class="w-full py-3">
                    <div class="flex flex-col relative">
                        <span>Current Password</span>
                        <asp:TextBox ID="txtCurrentPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5" TextMode="Password"></asp:TextBox>
                        <i class="fa-solid fa-eye absolute right-4 top-2/3 transform -translate-y-1/2 text-black"></i>
                    </div>
                </div>


                <div class="flex flex-col">
                    <div class="w-full py-3">
                        <div class="flex flex-col relative">
                            <span>New Password</span>
                            <asp:TextBox ID="txtNewPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5" TextMode="Password"></asp:TextBox>
                            <i class="fa-solid fa-eye absolute right-4 top-2/3 transform -translate-y-1/2 text-black"></i>
                        </div>
                    </div>
                </div>

                <div class="flex flex-col">
                    <div class="w-full py-3">
                        <div class="flex flex-col relative">
                            <span>Confirm Password</span>
                            <asp:TextBox ID="txtConfirmPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5" TextMode="Password"></asp:TextBox>
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
    <asp:Panel ID="popUpConfirmation" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center ">
        <!-- Modal content -->
        <div class="popUp-content w-1/3 h-fit flex flex-col bg-white p-5 rounded-xl flex flex-col gap-3 drop-shadow-lg">

            <div class="grid grid-cols-3 w-full h-fit justify-center flex p-0">
                <div>
                </div>
                <p class="text-2xl text-yellow-700 font-bold text-center">CONFIRMATION</p>
                <span class="w-auto flex items-center justify-end text-3xl rounded-full">


                    <asp:LinkButton ID="closePopUp" runat="server" OnClick="closePopUp_Click">
                <i class=" fa-solid fa-xmark"></i>
                    </asp:LinkButton>

                </span>

            </div>
            <div class="flex flex-col justify-center items-center gap-5">

                <div style="font-size: 64px">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~\Admin\Profile\image\userProfile.gif" AlternateText="userProfile" CssClass="w-28 h-28 " />

                </div>
                <p class="bold text-lg break-normal text-center">Are you sure you want to update your profile?</p>
                <p class="bold text-lg">
                    <asp:Label ID="lblItemInfo" runat="server" Text="[UserName]"></asp:Label>
                </p>
                <asp:TextBox ID="passwordForEdit" runat="server" TextMode="Password" CssClass="p-2 px-4 border rounded-xl" placeholder="Enter password to confirm"></asp:TextBox>
                <div>

                    <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" CssClass="bg-gray-300 p-2 px-4 rounded-lg cursor-pointer" OnClick="btnCancelEdit_Click" />
                    <asp:Button ID="btnConfirmEdit" runat="server" Text="Confirm" CssClass="bg-green-300 p-2 px-4 rounded-lg cursor-pointer" />
                </div>
            </div>
        </div>

    </asp:Panel>
</asp:Content>
