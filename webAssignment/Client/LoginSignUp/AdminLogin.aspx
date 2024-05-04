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
    <div class="w-full bg-gray-200 justify-center flex">
    <div class="w-1/4 bg-white p-2 drop-shadow-lg rounded-xl my-32">
        <!--Form Container-->
        <div class="h-full px-3 py-4 rounded-xl">
            <div class="flex flex-col">
                <div class="mb-2 text-4xl font-bold text-center text-black">
                    <span>G-Tech</span>
                </div>
                <span class="font-medium text-gray-600 text-center">Admin Login</span>
                <div class="flex flex-col mx-4">
                    <span class="text-xs mt-8">Email</span>
                    <div class="relative">
                        <i class="fa-regular fa-envelope absolute text-2xl pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Example@gmail.com" class="border border-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                    </div>
                </div>

                <div class="flex flex-col mx-4">
                    <span class="text-xs mt-6">Password</span>
                    <div class="relative">
                        <i class="fa-solid fa-lock absolute text-xl pl-1 pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" placeholder="Enter your password" class="border border-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                        <i id="eyeIcon" class="fa-regular fa-eye absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 cursor-pointer"></i>
                    </div>
                </div>

                <asp:LinkButton ID="linkForgotPass" runat="server" class="text-sm ml-5 my-2 underline">Forgot Password?</asp:LinkButton>
                <asp:Label ID="lblLoginMessage" runat="server" class="mx-4 font-bold text-red-600" Text=""></asp:Label>
                <div class="mx-4 mt-5">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" class="w-full px-16 py-2 bg-blue-700 font-bold text-white rounded-md cursor-pointer font-medium hover:bg-blue-400 duration-500 ease-in-out" OnClick="btnLogin_Click" />
                </div>

                <span class="font-medium text-gray-400 text-center pt-8">Terms of use. Privacy policy</span>
            </div>
        </div>
    </div>
</div>
</asp:Content>
