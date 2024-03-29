<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ProfileMaster/ProfileMasterPage.master" AutoEventWireup="true" CodeBehind="AccountDetails.aspx.cs" Inherits="webAssignment.Client.Profile.AccountDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div>
        <!--Acc Settings-->
        <div class="shadow-lg border border-gray-200 p-4 rounded-lg">
            <span class="font-medium pl-2">ACCOUNT SETTING</span>
            <div class="border-b border-gray-200 p-1.5 mb-3"></div>
            <div class="flex flex-row pt-4">
                <!--image col-->
                <div class="pl-3 pr-6 pt-3">
                    <div class="rounded-full overflow-hidden">
                        <asp:Image ID="profilePic" ImageUrl="~\Admin\Layout\image\DexProfilePic.jpeg" runat="server" />
                    </div>
                </div>
                <!--general details-->
                <div class="w-full flex flex-col">

                    <!-- first row-->
                    <div class="flex flex-row w-full">
                        <div class="w-1/2">
                            <div class="flex flex-col pr-6 pb-2">
                                <span class="pb-1.5">First Name</span>
                                <asp:TextBox ID="txtFirstName" runat="server" Text="Feyz" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                        <div class="w-1/2">
                            <div class="flex flex-col pl-6 pb-2">
                                <span class="pb-1.5">Last Name</span>
                                <asp:TextBox ID="txtLastName" runat="server" Text="Ibrahim" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <!-- second row-->
                    <div class="flex flex-row w-full">
                        <div class="w-1/2">
                            <div class="flex flex-col pr-6 pb-2">
                                <span class="pb-1.5">Username</span>
                                <asp:TextBox ID="txtUsername" runat="server" Text="feyzibrahim" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                        <div class="w-1/2">
                            <div class="flex flex-col pl-6 pb-2">
                                <span class="pb-1.5">Email</span>
                                <asp:TextBox ID="txtEmail" runat="server" Text="feyzibrahim@gmail.com" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <!-- third row-->
                    <div class="flex flex-row w-full">
                        <div class="w-1/2">
                            <div class="flex flex-col pr-6 pb-2">
                                <span class="pb-1.5">Secondary Email</span>
                                <asp:TextBox ID="txtSecEmail" Text="-" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                        <div class="w-1/2">
                            <div class="flex flex-col pl-6 pb-2">
                                <span class="pb-1.5">Phone Number</span>
                                <asp:TextBox ID="txtPhoneNo" Text="0123136742" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <!--Country Row-->
                    <div class="flex flex-row w-full pb-4">
                        <div class="w-1/3">
                            <div class="flex flex-col pr-12">
                                <span class="pb-1.5">Country/Region</span>
                                <asp:DropDownList ID="ddlCountry" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="w-1/3">
                            <div class="flex flex-col pr-6">
                                <span class="pb-1.5">States</span>
                                <asp:DropDownList ID="ddlState" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="w-1/3">
                            <div class="flex flex-col pl-6">
                                <span class="pb-1.5">Pind Code</span>
                                <asp:TextBox ID="txtPinCode" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w-auto py-3">
                        <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" class="bg-blue-600 text-white rounded-lg px-8 py-3" />
                    </div>
                </div>
            </div>
        </div>

        <!--Change Password-->
        <div class="shadow-lg border border-gray-200 p-4 rounded-lg mt-5">
            <span class="font-medium pl-2">CHANGE PASSWORD</span>
            <div class="border-b border-gray-200 p-1.5 mb-3"></div>

            <div class="flex flex-col">
                <div class="w-full py-3">
                    <div class="flex flex-col relative">
                        <span>Current Password</span>
                        <asp:TextBox ID="txtCurrentPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                        <i class="fa-solid fa-eye absolute right-4 top-2/3 transform -translate-y-1/2 text-black"></i>
                    </div>
                </div>


                <div class="flex flex-col">
                    <div class="w-full py-3">
                        <div class="flex flex-col relative">
                            <span>New Password</span>
                            <asp:TextBox ID="txtNewPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
                            <i class="fa-solid fa-eye absolute right-4 top-2/3 transform -translate-y-1/2 text-black"></i>
                        </div>
                    </div>
                </div>

                <div class="flex flex-col">
                    <div class="w-full py-3">
                        <div class="flex flex-col relative">
                            <span>Confirm Password</span>
                            <asp:TextBox ID="txtConfirmPass" runat="server" class="border-2 border-gray-200 rounded-sm px-3 py-2.5"></asp:TextBox>
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
</asp:Content>
