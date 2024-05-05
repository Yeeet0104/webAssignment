<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="recoverPassword.aspx.cs" Inherits="webAssignment.Client.LoginSignUp.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="w-full bg-gray-200 justify-center flex">
    <div class="w-1/4 bg-white p-2 drop-shadow-lg rounded-xl my-32">
        <!--Form Container-->
        <div class="h-full px-3 py-4 rounded-xl">
            <div class="flex flex-col">
                <div class="mb-12 mt-3 text-3xl font-bold text-center text-black">
                    <span>Password Recovery</span>
                </div>
                <span class="font-medium text-gray-600 text-sm mx-4">Enter the email address associated with your account and we'll send you a link to reset your password.</span>
                <div class="flex flex-col mx-4">
                    <span class="text-xs mt-3">Email</span>
                    <div class="relative">
                        <i class="fa-regular fa-envelope absolute text-2xl pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Example@gmail.com" class="border border-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                    </div>
                </div>
                <asp:Label ID="lblLoginMessage" runat="server" class="mx-4 font-bold text-red-600" Text=""></asp:Label>
                <div class="mx-4 mt-5">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="w-full px-16 py-2 bg-blue-700 font-bold text-white rounded-md cursor-pointer font-medium hover:bg-blue-400 duration-500 ease-in-out" OnClick="btnSubmit_Click" />
                </div>

                <span class="font-medium text-gray-400 text-center pt-8">Don't have an account?<asp:HyperLink ID="linkSignUp" runat="server" NavigateUrl="~/Client/LoginSignUp/SignUp.aspx" class="hover:cursor-pointer pl-2 font-medium text-blue-600">Sign Up</asp:HyperLink></span>
            </div>
        </div>
    </div>
</div>
</asp:Content>