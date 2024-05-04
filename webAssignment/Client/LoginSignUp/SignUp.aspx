<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="webAssignment.Client.LoginSignUp.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #googleIcon {
            background: conic-gradient(from 180deg at 50% 50%, green 0deg 70deg, orange 70deg 120deg, red 120deg 220deg, blue 220deg 315deg, green 315deg 360deg);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }
        
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
    </style>

   <script>
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
           document.getElementById("eyeIcon").addEventListener("click", function () {
               togglePasswordVisibility("eyeIcon", '<%= txtPass.ClientID %>');
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
    <div class="w-full h-full p-8 bg-gray-200 justify-center flex">
        <div class="flex flex-row bg-white p-2 rounded-xl w-3/5">
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
                            <asp:TextBox ID="txtUsername" runat="server" placeholder="Jamie125" CssClass="border border-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                        </div>
                    </div>

                    <div class="flex flex-col mx-4">
                        <span class="text-xs mt-2">Email</span>
                        <div class="relative">
                            <i class="fa-regular fa-envelope absolute text-2xl pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Example@gmail.com" class="border border-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                        </div>
                    </div>

                    <div class="flex flex-col mx-4">
                        <span class="text-xs mt-4">Password</span>
                        <div class="relative">
                            <i class="fa-solid fa-lock absolute text-xl pl-1 pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" placeholder="Enter your password" class="border border-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                            <i id="eyeIcon" class="fa-regular fa-eye absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 cursor-pointer"></i>
                        </div>
                    </div>

                    <div class="flex flex-col mx-4">
                        <span class="text-xs mt-4">Confirm Password</span>
                        <div class="relative">
                            <i class="fa-solid fa-lock absolute text-xl pl-1 pt-1 left-3 top-6 transform -translate-y-1/2 text-gray-400"></i>
                            <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password" placeholder="Enter your password" class="border border-gray-300 rounded-md px-12 py-2 text-lg mt-1 w-full"></asp:TextBox>
                            <i id="eyeIcon2" class="fa-regular fa-eye absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 cursor-pointer"></i>
                        </div>
                    </div>

                    <asp:Label ID="passwordLabel" runat="server" class="mx-4 font-bold text-red-600"  Text=""></asp:Label>

                    <div class="mx-4 mt-6">
                        <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" class="w-full px-16 py-2 bg-blue-700 font-bold text-white rounded-md cursor-pointer font-medium hover:bg-blue-400 duration-200 ease-in-out" OnClick="btnSignUp_Click" />
                    </div>

                    <div class="flex items-center space-x-2 py-5">
                        <div class="flex-grow border-t border-gray-300"></div>
                        <div class="text-sm font-medium text-gray-500">Or Sign Up With</div>
                        <div class="flex-grow border-t border-gray-300"></div>
                    </div>

                    <div class="flex w-full justify-center items-center gap-12">
                        <div class="flex flex-col items-center">
                            <div class="min-w-11 min-h-11 rounded-full flex items-center justify-center text-white bg-blue-700 hover:cursor-pointer">
                                <i class="fa-brands fa-facebook-f text-2xl"></i>
                            </div>
                            <span class="">Facebook</span>
                        </div>

                        <div class="flex flex-col">
                            <div class="min-w-11 min-h-11 rounded-full bg-black text-white border flex items-center justify-center hover:cursor-pointer">
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
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Client/LoginSignUp/welcomeDog.gif" AlternateText="welcome" CssClass="w-35 h-40 " />

            </div>
            <p class="font-bold text-2xl break-normal text-center">Thank You For Joing And Welcome </br> To G-Tech</p>
            <div>
                <asp:Button ID="btnContinue" runat="server" Text="Continue" CssClass="bg-blue-600 text-white font-bold text-xl p-2 px-4 rounded-lg cursor-pointer" OnClick="btnContinue_Click" />
            </div>
        </div>
    </div>
</asp:Panel>

</asp:Content>
