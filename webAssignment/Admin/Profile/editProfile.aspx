<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="editProfile.aspx.cs" Inherits="webAssignment.Admin.Profile.editProfile" %>

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
        function triggerFileUpload() {
            document.getElementById('<%= fileUpload.ClientID %>').click();
            return false; // Prevent postback on LinkButton click
        }

        function togglePasswordVisibility(eyeIconId, passwordFieldId) {
            var passwordField = document.getElementById(passwordFieldId);
            var eyeIcon = document.getElementById(eyeIconId);

            if (passwordField.type === 'password') {
                passwordField.type = 'text';
                eyeIcon.classList.remove('fa-eye');
                eyeIcon.classList.add('fa-eye-slash');
            } else {
                passwordField.type = 'password';
                eyeIcon.classList.remove('fa-eye-slash');
                eyeIcon.classList.add('fa-eye');
            }
        }

        document.addEventListener("DOMContentLoaded", function () {
            document.getElementById("eyeIcon1").addEventListener("click", function () {
                togglePasswordVisibility("eyeIcon1", '<%= txtCurrentPass.ClientID %>');
        });

        document.getElementById("eyeIcon2").addEventListener("click", function () {
            togglePasswordVisibility("eyeIcon2", '<%= txtNewPass.ClientID %>');
        });
        document.getElementById("eyeIcon3").addEventListener("click", function () {
            togglePasswordVisibility("eyeIcon3", '<%= txtConfirmPass.ClientID %>');
        });
    });
    </script>
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
                <div class="relative mr-2 mt-2">
                    <i class="fa-solid fa-xmark absolute text-white text-xl left-5 top-3 pt-1 transform -translate-y-1/2"></i>
                    <asp:HyperLink ID="cancelBtn" runat="server" class="pl-11 pr-5 py-2.5 text-sm bg-red-500 text-white rounded-lg" NavigateUrl="~/Admin/Profile/AdminProfile.aspx">Cancel</asp:HyperLink>
                </div>
                <div class="relative ml-2">
                    <i class="fa-regular fa-floppy-disk absolute text-white text-lg left-5 top-5 transform -translate-y-1/2"></i>
                    <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" class="pl-11 pr-5 py-2.5 text-sm bg-blue-500 text-white rounded-lg cursor-pointer" OnClick="btnSaveChanges_Click" />
                </div>
            </div>



        </div>
        <!--Acc Settings-->
        <%--        <div>

           <span class="font-medium pl-2">ACCOUNT SETTINGS</span>
       </div>--%>
        <div>
            <div class="grid grid-cols-8 gap-5 ">
                <!--image col-->
                <div class="col-span-2">
                    <div class="text-xl py-2">
                        <p>Profile Picture</p>
                    </div>
                    <div class="drop-shadow bg-white pv-6 pt-6 pb-8 rounded-xl">
                        <div class="flex justify-center items-center pt-3 pb-2">
                            <asp:Image ID="profilePic" class="w-[300px] rounded-lg" Height="300" runat="server" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();" />
                        </div>
                        <div class="flex justify-center pt-3">
                            <asp:FileUpload ID="fileUpload" runat="server" Style="cursor: pointer; display: none" onchange="previewImage(this);" />
                            <button type="button" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();" class="bg-blue-500 text-white w-full py-1 rounded-lg cursor-pointer hover:bg-blue-600">
                                Choose File
                            </button>

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
                                    <asp:TextBox ID="txtFirstName" runat="server" class="bg-gray-100 rounded-xl p-3"></asp:TextBox>
                                </div>
                                <div>
                                    <div class="flex flex-col">
                                        <span class="pb-1.5">Email</span>
                                        <asp:TextBox ID="txtEmail" runat="server" class="bg-gray-100 rounded-xl p-3"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <!-- right -->
                            <div class="flex flex-col gap-5">
                                <div class="flex flex-col">
                                    <span class="pb-1.5">Last Name</span>
                                    <asp:TextBox ID="txtLastName" runat="server" class="bg-gray-100 rounded-xl p-3"></asp:TextBox>
                                </div>
                                <div>
                                    <div class="flex flex-col">
                                        <span class="pb-1.5">Phone Number</span>
                                        <asp:TextBox ID="txtPhoneNo" runat="server" class="bg-gray-100 rounded-xl p-3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <!--DOB-->
                        <div>
                            <span>Birthday</span>
                            <div class="mt-1.5 grid grid-cols-3 gap-7">
                                <asp:DropDownList ID="ddlDay" runat="server" class="bg-gray-100 rounded-xl p-3">
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

                                <asp:DropDownList ID="ddlMonth" runat="server" class="bg-gray-100 rounded-xl p-3">
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

                                <asp:DropDownList ID="ddlYear" runat="server" class="bg-gray-100 rounded-xl p-3">
                                    <asp:ListItem>1950</asp:ListItem>
                                    <asp:ListItem>1951</asp:ListItem>
                                    <asp:ListItem>1952</asp:ListItem>
                                    <asp:ListItem>1953</asp:ListItem>
                                    <asp:ListItem>1954</asp:ListItem>
                                    <asp:ListItem>1955</asp:ListItem>
                                    <asp:ListItem>1956</asp:ListItem>
                                    <asp:ListItem>1957</asp:ListItem>
                                    <asp:ListItem>1958</asp:ListItem>
                                    <asp:ListItem>1959</asp:ListItem>
                                    <asp:ListItem>1960</asp:ListItem>
                                    <asp:ListItem>1961</asp:ListItem>
                                    <asp:ListItem>1962</asp:ListItem>
                                    <asp:ListItem>1963</asp:ListItem>
                                    <asp:ListItem>1964</asp:ListItem>
                                    <asp:ListItem>1965</asp:ListItem>
                                    <asp:ListItem>1966</asp:ListItem>
                                    <asp:ListItem>1967</asp:ListItem>
                                    <asp:ListItem>1968</asp:ListItem>
                                    <asp:ListItem>1969</asp:ListItem>
                                    <asp:ListItem>1970</asp:ListItem>
                                    <asp:ListItem>1971</asp:ListItem>
                                    <asp:ListItem>1972</asp:ListItem>
                                    <asp:ListItem>1973</asp:ListItem>
                                    <asp:ListItem>1974</asp:ListItem>
                                    <asp:ListItem>1975</asp:ListItem>
                                    <asp:ListItem>1976</asp:ListItem>
                                    <asp:ListItem>1977</asp:ListItem>
                                    <asp:ListItem>1978</asp:ListItem>
                                    <asp:ListItem>1979</asp:ListItem>
                                    <asp:ListItem>1980</asp:ListItem>
                                    <asp:ListItem>1981</asp:ListItem>
                                    <asp:ListItem>1982</asp:ListItem>
                                    <asp:ListItem>1983</asp:ListItem>
                                    <asp:ListItem>1984</asp:ListItem>
                                    <asp:ListItem>1985</asp:ListItem>
                                    <asp:ListItem>1986</asp:ListItem>
                                    <asp:ListItem>1987</asp:ListItem>
                                    <asp:ListItem>1988</asp:ListItem>
                                    <asp:ListItem>1989</asp:ListItem>
                                    <asp:ListItem>1990</asp:ListItem>
                                    <asp:ListItem>1991</asp:ListItem>
                                    <asp:ListItem>1992</asp:ListItem>
                                    <asp:ListItem>1993</asp:ListItem>
                                    <asp:ListItem>1994</asp:ListItem>
                                    <asp:ListItem>1995</asp:ListItem>
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
                                    <asp:ListItem>2008</asp:ListItem>
                                    <asp:ListItem>2009</asp:ListItem>
                                    <asp:ListItem>2010</asp:ListItem>
                                    <asp:ListItem>2011</asp:ListItem>
                                    <asp:ListItem>2012</asp:ListItem>
                                    <asp:ListItem>2013</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                    <asp:ListItem>2016</asp:ListItem>
                                    <asp:ListItem>2017</asp:ListItem>
                                    <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                    <asp:ListItem>2020</asp:ListItem>
                                    <asp:ListItem>2021</asp:ListItem>
                                    <asp:ListItem>2022</asp:ListItem>
                                    <asp:ListItem>2023</asp:ListItem>
                                    <asp:ListItem>2024</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <%--                            <div class="w-auto pt-7">
                                <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" class="bg-blue-600 text-white rounded-lg px-8 py-3" />
                            </div>--%>
                        </div>
                        <asp:Label ID="lblErrorMsg" CssClass="font-bold text-red-600" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <!--Change Password-->
        <div class="text-xl mb-2 mt-5">
            <p>CHANGE PASSWORD</p>
        </div>
        <div class="drop-shadow bg-white p-4 rounded-xl">

            <div class="flex flex-col">
                <div class="w-full py-3">
                    <div class="flex flex-col relative">
                        <span>Current Password</span>
                        <asp:TextBox ID="txtCurrentPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5" TextMode="Password"></asp:TextBox>
                        <i id="eyeIcon1" class="fa-solid fa-eye absolute right-4 top-2/3 transform -translate-y-1/2 text-black"></i>
                    </div>
                </div>


                <div class="flex flex-col">
                    <div class="w-full py-3">
                        <div class="flex flex-col relative">
                            <span>New Password</span>
                            <asp:TextBox ID="txtNewPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5" TextMode="Password"></asp:TextBox>
                            <i id="eyeIcon2" class="fa-solid fa-eye absolute right-4 top-2/3 transform -translate-y-1/2 text-black"></i>
                        </div>
                    </div>
                </div>

                <div class="flex flex-col">
                    <div class="w-full py-3">
                        <div class="flex flex-col relative">
                            <span>Confirm Password</span>
                            <asp:TextBox ID="txtConfirmPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5" TextMode="Password"></asp:TextBox>
                            <i id="eyeIcon3" class="fa-solid fa-eye absolute right-4 top-2/3 transform -translate-y-1/2 text-black"></i>
                        </div>
                    </div>
                </div>
                <div class="w-auto py-3">
                    <asp:Button ID="btnChangePass" runat="server" Text="Change Password" class="bg-blue-600 text-white rounded-lg px-12 py-3" OnClick="btnChangePass_Click" />
                </div>
                <asp:Label ID="lblChangePass" CssClass="font-bold text-red-600" runat="server" Text=""></asp:Label>
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
