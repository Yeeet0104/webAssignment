﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="webAssignment.Client.LoginSignUp.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #googleIcon {
            background: conic-gradient(from 180deg at 50% 50%, green 0deg 70deg, orange 70deg 120deg, red 120deg 220deg, blue 220deg 315deg, green 315deg 360deg);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }
    </style>

    <script type="text/javascript">
        function togglePasswordVisibility() {
            var passwordField = document.getElementById(txtPass);
            var eyeIcon = document.getElementById(eyeIcon1);

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="w-full h-full p-8 bg-gray-200 justify-center flex">
        <div class="flex flex-row bg-white p-2 rounded-xl w-3/4">
            <!--Img Container-->
            <div class="w-1/3">
                <asp:Image ID="bgImg" ImageUrl="~/Client/LoginSignUp/bg1.jfif" runat="server" class="w-full h-full object-cover rounded-xl" />
            </div>
            <!--Form Container-->
            <div class="w-2/3 px-7 rounded-xl">
                <div class="flex flex-col">
                    <div class="mb-2 pt-4 text-5xl font-bold text-center text-black">
                        <span>G-Tech</span>
                    </div>
                    <span class="font-medium text-2xl pl-3">Login</span>
                    <div class="flex flex-col mx-4">
                        <span class="text-xs mt-2">Email</span>
                        <div class="relative">
                            <i class="fa-regular fa-envelope absolute text-2xl pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Example@gmail.com" class="ring-1 ring-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                        </div>
                    </div>

                    <div class="flex flex-col mx-4">
                        <span class="text-xs mt-4">Password</span>
                        <div class="relative">
                            <i class="fa-solid fa-lock absolute text-xl pl-1 pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" placeholder="Enter your password" class="ring-1 ring-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                            <i id="eyeIcon1" class="fa-regular fa-eye absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 cursor-pointer" OnClick="togglePasswordVisibility()"></i>
                        </div>
                    </div>

                    <asp:LinkButton ID="linkForgotPass" runat="server" class="text-sm ml-5 mt-2 underline">Forgot Password?</asp:LinkButton>
                    <div class="mx-4 mt-4">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" class="w-full px-16 py-2 bg-blue-700 font-bold text-white rounded-md cursor-pointer font-medium hover:bg-blue-400" />
                    </div>

                    <div class="flex items-center space-x-2 mt-5">
                        <div class="flex-grow border-t border-gray-300"></div>
                        <div class="text-sm font-medium text-gray-500">OR</div>
                        <div class="flex-grow border-t border-gray-300"></div>
                    </div>

                    <div class="flex justify-center">
                        <div class="p-4 text-base flex flex-row gap-3">
                            <div class="mr-5 min-w-11 min-h-11 rounded-full flex items-center justify-center text-white bg-blue-700 hover:cursor-pointer">
                                <i class="fa-brands fa-facebook-f"></i>
                            </div>
                            <div class="mx-5 min-w-11 min-h-11 rounded-full bg-black text-white border flex items-center justify-center hover:cursor-pointer">
                                <i class="fa-brands fa-x-twitter"></i>
                            </div>
                            <div id="googleIcon" class="ml-5 min-w-11 min-h-11 text-2xl border border-white rounded-full flex items-center justify-center hover:cursor-pointer">
                                <i class="fa-brands fa-google"></i>
                            </div>
                        </div>
                    </div>
                    <div class="flex flex-row justify-center py-3">
                        <span class="font-medium">Don't have an account?</span>
                        <asp:HyperLink ID="linkSignUp" runat="server" NavigateUrl="~/Client/LoginSignUp/SignUp.aspx" class="hover:cursor-pointer pl-2 font-medium text-blue-600">Sign Up Now</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
