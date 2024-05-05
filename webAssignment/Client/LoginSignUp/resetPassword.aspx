<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="resetPassword.aspx.cs" Inherits="webAssignment.Client.LoginSignUp.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
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
            document.getElementById("eyeIcon").addEventListener("click", function () {
                togglePasswordVisibility("eyeIcon", '<%= txtNewPass.ClientID %>');
            });

            document.getElementById("eyeIcon2").addEventListener("click", function () {
                togglePasswordVisibility("eyeIcon2", '<%= txtConfirmPass.ClientID %>');
            });
        });

        function showPopUp() {
            document.getElementById('<%= popUpText.ClientID %>').classList.remove('hidden');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="w-full bg-gray-200 justify-center flex">
        <!--Form Container-->
        <div class="w-2/5 shadow-lg border border-gray-200 p-4 rounded-lg my-12  bg-white">
            <span class="font-medium pl-2">CHANGE PASSWORD</span>
            <div class="border-b border-gray-200 p-1.5 mb-3"></div>
            <div class="flex flex-col">
                <div class="w-full py-3">
                    <div class="flex flex-col relative">
                        <span>New Password</span>
                        <asp:TextBox ID="txtNewPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5" TextMode="Password"></asp:TextBox>
                        <i id="eyeIcon" class="fa-solid fa-eye-slash absolute right-4 top-2/3 transform -translate-y-1/2 text-black cursor-pointer"></i>
                    </div>
                </div>
            </div>
            <div class="flex flex-col">
                <div class="w-full py-3">
                    <div class="flex flex-col relative">
                        <span>Confirm Password</span>
                        <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                        <i id="eyeIcon2" class="fa-solid fa-eye-slash absolute right-4 top-2/3 transform -translate-y-1/2 text-black cursor-pointer"></i>
                    </div>
                </div>
            </div>
            <div class="w-auto py-3">
                <asp:Button ID="btnChangePass" runat="server" Text="Reset Password" OnClick="btnChangePass_Click" class="bg-blue-600 text-white rounded-lg px-12 py-3 cursor-pointer" />
            </div>
            <asp:Label ID="lblChangePass" CssClass="font-bold text-red-600" runat="server" Text=""></asp:Label>
        </div>
    </div>


    <asp:Panel ID="popUpText" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center ">
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
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Client/Profile/images/successCat.gif" AlternateText="welcome" CssClass="w-35 h-40 " />
                </div>
                <p class="font-bold text-2xl break-normal text-center">You have successfully reset your password.</p>
                <div>
                    <asp:Button ID="btnContinue" runat="server" Text="Continue" CssClass="bg-blue-600 text-white font-bold text-xl p-2 px-4 rounded-lg cursor-pointer" OnClick="btnContinue_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
