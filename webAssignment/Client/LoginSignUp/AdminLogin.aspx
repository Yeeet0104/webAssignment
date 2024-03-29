<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="webAssignment.Client.LoginSignUp.AdminLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
    #googleIcon {
        background: conic-gradient(from 180deg at 50% 50%, green 0deg 70deg, orange 70deg 120deg, red 120deg 220deg, blue 220deg 315deg, green 315deg 360deg);
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
    }
</style>

<script>
    document.addEventListener("DOMContentLoaded", function () {
        // Function to toggle password visibility
        function togglePasswordVisibility() {
            var passwordField = document.getElementById("<%= txtPass.ClientID %>");
            var eyeIcon = document.getElementById("eyeIcon");

            if (passwordField.type === 'password') {
                passwordField.type = 'text';
                eyeIcon.classList.remove('fa-eye');
                eyeIcon.classList.add("fa-eye-slash");
            } else {
                passwordField.type = 'password';
                eyeIcon.classList.remove('fa-eye-slash');
                eyeIcon.classList.add("fa-eye");
            }
        }

        // Add click event listener to the eye icon
        var eyeIcon = document.getElementById("eyeIcon");
        eyeIcon.addEventListener("click", togglePasswordVisibility);
    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="w-full h-screen bg-gray-200 justify-center items-center flex">
    <div class="flex bg-white p-2 rounded-xl w-1/2 h-3/4">
        <!--Img Container-->
        <div class="w-1/3">
            <asp:Image ID="bgAdminImg" ImageUrl="~/Client/LoginSignUp/bgAdmin.jfif" runat="server" class="w-full h-full object-cover rounded-xl" />
        </div>
        <!--Form Container-->
        <div class="w-2/3 h-full px-3 rounded-xl pt-12">
            <div class="flex flex-col">
                <div class="mb-2 text-4xl font-bold text-center text-black">
                    <span>G-Tech</span>
                </div>
                <span class="font-medium text-gray-600 text-center">Admin Login</span>
                <div class="flex flex-col mx-4">
                    <span class="text-xs mt-8">Email</span>
                    <div class="relative">
                        <i class="fa-regular fa-envelope absolute text-2xl pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Example@gmail.com" class="ring-1 ring-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                    </div>
                </div>

                <div class="flex flex-col mx-4">
                    <span class="text-xs mt-6">Password</span>
                    <div class="relative">
                        <i class="fa-solid fa-lock absolute text-xl pl-1 pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" placeholder="Enter your password" class="ring-1 ring-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                        <i id="eyeIcon" class="fa-regular fa-eye absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 cursor-pointer"></i>
                    </div>
                </div>

                <asp:LinkButton ID="linkForgotPass" runat="server" class="text-sm ml-5 mt-2 underline">Forgot Password?</asp:LinkButton>
                <div class="mx-4 mt-10">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" class="w-full px-16 py-2 bg-blue-700 font-bold text-white rounded-md cursor-pointer font-medium hover:bg-blue-400 duration-500 ease-in-out" />
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
