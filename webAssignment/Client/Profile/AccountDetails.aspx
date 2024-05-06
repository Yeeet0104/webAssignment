<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ProfileMaster/ProfileMasterPage.master" AutoEventWireup="true" CodeBehind="AccountDetails.aspx.cs" Inherits="webAssignment.Client.Profile.AccountDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        @keyframes rockUpDown {
            0%, 100% {
                transform: translateY(0);
            }

            10% {
                transform: translateY(-10px);
            }

            20%, 40%, 60%, 80% {
                transform: translateY(-10px) rotate(-6deg);
            }

            30%, 50%, 70% {
                transform: translateY(-10px) rotate(6deg);
            }

            90% {
                transform: translateY(-10px);
            }
        }
        .popup-visible {
    display: flex;
}
    </style>

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
                eyeIcon.classList.remove('fa-eye-slash');
                eyeIcon.classList.add('fa-eye');
            } else {
                passwordField.type = 'password';
                eyeIcon.classList.remove('fa-eye');
                eyeIcon.classList.add('fa-eye-slash');
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
        function showPopUp() {
            document.getElementById('<%= popUpText.ClientID %>').classList.remove('hidden');
        }
            function showConfirmPopUp() {
                document.getElementById('<%= deleteAccPopUp.ClientID %>').classList.remove('hidden');
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
                        <asp:Image ID="profilePic" CssClass="rounded-full w-[230px] drop-shadow-lg border border-gray-200" Height="230" runat="server" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();" ImageUrl='<%# Bind("profile_pic_path", "{0}") %>' />
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
                                <asp:TextBox ID="txtFirstName" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">Last Name</span>
                                <asp:TextBox ID="txtLastName" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                            </div>
                        </div>

                        <!-- second row-->
                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">Username</span>
                                <asp:TextBox ID="txtUsername" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">Email</span>
                                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
                            </div>
                        </div>

                        <!-- third row-->
                        <div>
                            <div class="flex flex-col">
                                <span class="pb-1.5">Phone Number</span>
                                <asp:TextBox ID="txtPhoneNo" TextMode="Phone" runat="server" class="border-2 border-gray-200 rounded-sm px-2 py-2.5"></asp:TextBox>
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
                    <div class="w-auto pt-7">
                        <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" class="bg-blue-500 text-white rounded-lg px-8 py-3 mb-4 cursor-pointer hover:bg-blue-600" OnClick="btnSaveChanges_Click" />
                    </div>
                    <asp:Label ID="lblErrorMsg" CssClass="font-bold text-red-600" runat="server" Text=""></asp:Label>
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
                        <i id="eyeIcon1" class="fa-solid fa-eye-slash absolute right-4 top-2/3 transform -translate-y-1/2 text-black cursor-pointer"></i>
                    </div>
                </div>


                <div class="flex flex-col">
                    <div class="w-full py-3">
                        <div class="flex flex-col relative">
                            <span>New Password</span>
                            <asp:TextBox ID="txtNewPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            <i id="eyeIcon2" class="fa-solid fa-eye-slash absolute right-4 top-2/3 transform -translate-y-1/2 text-black cursor-pointer"></i>
                        </div>
                    </div>
                </div>

                <div class="flex flex-col">
                    <div class="w-full py-3">
                        <div class="flex flex-col relative">
                            <span>Confirm Password</span>
                            <asp:TextBox ID="txtConfirmPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            <i id="eyeIcon3" class="fa-solid fa-eye-slash absolute right-4 top-2/3 transform -translate-y-1/2 text-black cursor-pointer"></i>
                        </div>
                    </div>
                </div>
                <div class="w-auto py-3">
                    <asp:Button ID="btnChangePass" runat="server" Text="Change Password" class="bg-blue-600 text-white rounded-lg px-12 py-3 cursor-pointer" OnClick="btnChangePass_Click" />
                </div>
                <asp:Label ID="lblChangePass" CssClass="font-bold text-red-600" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="w-full flex justify-end">
            <asp:Button ID="btnDeleteAcc" runat="server" Text="Delete Account" class="bg-red-600 text-white rounded-lg px-12 py-3 mt-20 cursor-pointer" OnClick="btnDeleteAcc_Click" />
        </div>
        <asp:Label ID="lblMessage" CssClass="font-bold text-red-600" runat="server" Text=""></asp:Label>
    </div>

    <asp:Panel ID="popUpText" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center popup-visible">
        <!-- Modal content -->
        <div class="popUp-content w-1/3 h-fit flex flex-col bg-white p-5 rounded-xl flex flex-col gap-3 drop-shadow-lg">
            <div class="w-full h-fit  flex justify-end p-0">
                <span class=" flex items-center justify-center text-3xl rounded-full">
                    <asp:LinkButton ID="closePopUp" runat="server" OnClick="closePopUp_Click">
                <i class=" fa-solid fa-xmark"></i>
                    </asp:LinkButton>
                </span>

            </div>
            <div class="flex flex-col justify-center items-center gap-5">
                <div>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Client/Profile/images/successCat.gif" AlternateText="success" CssClass="w-35 h-40 " />

                </div>
                <p class="font-bold text-3xl break-normal text-center">Details Updated</p>
                <div>
                    <asp:Button ID="btnDone" runat="server" Text="Done" CssClass="bg-blue-600 text-white font-bold text-xl p-2 px-4 rounded-lg cursor-pointer" OnClick="btnDone_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>


<asp:Panel ID="deleteAccPopUp" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center popup-visible">
    <!-- Modal content -->
    <div class="popUp-content w-1/3 h-fit flex flex-col bg-white p-5 rounded-xl flex flex-col gap-3 drop-shadow-lg">
        <div class="w-full h-fit flex justify-end p-0">
            <span class=" flex items-center justify-center text-3xl rounded-full">
                <asp:LinkButton ID="closePopUp2" runat="server" OnClick="closePopUp_Click">
            <i class=" fa-solid fa-xmark"></i>
                </asp:LinkButton>
            </span>

        </div>
        <div class="flex flex-col justify-center items-center gap-5">
            <div>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Client/Profile/images/confirmDelete.gif" AlternateText="Are you sure" CssClass="w-35 h-40 " />

            </div>
            <p class="font-bold text-xl break-normal text-center">Once account is deleted, you are no longer a member.<br />Are you sure?</p>
            <div>
                <asp:Button ID="cancelBtn" runat="server" Text="Cancel" CssClass="bg-blue-600 mr-6 text-white font-bold text-xl p-2 px-4 rounded-lg cursor-pointer" OnClick="cancelBtn_Click"  />
                <asp:Button ID="yesBtn" runat="server" Text="Confirm" CssClass="bg-red-600 ml-6 text-white font-bold text-xl py-2 px-6 rounded-lg cursor-pointer" OnClick="yesBtn_Click"  />
            </div>
        </div>
    </div>
</asp:Panel>
</asp:Content>
