﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="webAssignment.Client.LoginSignUp.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #googleIcon {
            background: conic-gradient(from 180deg at 50% 50%, green 0deg 70deg, orange 70deg 120deg, red 120deg 220deg, blue 220deg 315deg, green 315deg 360deg);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }
    </style>

   <script>
       // Function to toggle password visibility
       function togglePasswordVisibility(eyeIconId, passwordFieldId) {
           var passwordField = document.getElementById(passwordFieldId);
           var eyeIcon = document.getElementById(eyeIconId);

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

       document.addEventListener("DOMContentLoaded", function () {
           // Add click event listener to the eye icons
           var eyeIcon1 = document.getElementById("eyeIcon");
           var txtPassClientId = '<%= txtPass.ClientID %>';
        eyeIcon1.addEventListener("click", function () {
            togglePasswordVisibility("eyeIcon", txtPassClientId);
        });

        var eyeIcon2 = document.getElementById("eyeIcon2");
        var txtConfirmPassClientId = '<%= txtConfirmPass.ClientID %>';
        eyeIcon2.addEventListener("click", function () {
            togglePasswordVisibility("eyeIcon2", txtConfirmPassClientId);
        });
    });
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
                    <div class="mb-2 pt-4 text-4xl font-bold text-center text-black">
                        <span>G-Tech</span>
                    </div>
                    <span class="font-medium text-xl pl-3">Sign Up</span>

                    <div class="flex flex-col mx-4">
                        <span class="text-xs mt-2">Username</span>
                        <div class="relative">
                            <i class="fa-regular fa-user absolute text-2xl pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                            <asp:TextBox ID="txtUsername" runat="server" placeholder="John Abraham" class="ring-1 ring-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                        </div>
                    </div>

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
                            <i id="eyeIcon" class="fa-regular fa-eye absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 cursor-pointer" onclick="togglePasswordVisibility('eyeIcon', 'txtPass')"></i>
                        </div>
                    </div>

                    <div class="flex flex-col mx-4">
                        <span class="text-xs mt-4">Confirm Password</span>
                        <div class="relative">
                            <i class="fa-solid fa-lock absolute text-xl pl-1 pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                            <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password" placeholder="Enter your password" class="ring-1 ring-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                            <i id="eyeIcon2" class="fa-regular fa-eye absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 cursor-pointer" onclick="togglePasswordVisibility()"></i>
                        </div>
                    </div>
                    <div class="mx-4 mt-6">
                        <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" class="w-full px-16 py-2 bg-blue-700 font-bold text-white rounded-md cursor-pointer font-medium hover:bg-blue-400 duration-500 ease-in-out" />
                    </div>

                    <div class="flex items-center space-x-2 py-5">
                        <div class="flex-grow border-t border-gray-300"></div>
                        <div class="text-sm font-medium text-gray-500">Or Sign Up With</div>
                        <div class="flex-grow border-t border-gray-300"></div>
                    </div>

                    <div class="flex w-full justify-between items-center px-40">
                        <div class="flex flex-col items-center">
                            <div class="min-w-11 min-h-11 rounded-full flex items-center justify-center text-white bg-blue-700 hover:cursor-pointer">
                                <i class="fa-brands fa-facebook-f text-2xl"></i>
                            </div>
                            <span class="">Facebook</span>
                        </div>

                        <div class="flex flex-col">
                            <div class="mx-5 min-w-11 min-h-11 rounded-full bg-black text-white border flex items-center justify-center hover:cursor-pointer">
                                <i class="fa-brands fa-x-twitter"></i>
                            </div>
                            <span class="text-center">X</span>
                        </div>

                        <div class="flex flex-col">
                            <div class="min-w-11 pt-2 text-3xl rounded-full flex items-center justify-center hover:cursor-pointer">
                                <i id="googleIcon" class="fa-brands fa-google"></i>
                            </div>
                            <span>Google</span>
                        </div>
                    </div>

                    <div class="flex flex-row justify-center py-5">
                        <span class="font-medium">Already have an account?</span>
                        <asp:HyperLink ID="linkLogin" runat="server" NavigateUrl="~/Client/LoginSignUp/Login.aspx" class="hover:cursor-pointer pl-2 font-medium text-blue-600">Login Now</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
