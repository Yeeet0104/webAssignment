<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="AdminProfile.aspx.cs" Inherits="webAssignment.Admin.Profile.AdminProfile" %>

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
                <div class="text-sm relative ml-2 bg-blue-500 text-white flex flex-row items-center p-1 py-2.5 px-2 gap-2 rounded-lg">

                    <i class="fa-solid fa-user-pen left-4 top-5 text-white"></i>
                    <asp:HyperLink ID="editProfileLk" runat="server" NavigateUrl="~/Admin/Profile/editProfile.aspx">Edit Profile</asp:HyperLink>
                </div>

            </div>
        </div>
        <!--Acc Settings-->
        <%--        <div>

            <span class="font-medium pl-2">ACCOUNT SETTINGS</span>
        </div>--%>
        <div class="">
            <div class="grid grid-cols-8 gap-5 ">
                <!--image col-->
                <div class="col-span-2">
                    <div class="text-xl py-2">
                        <p>Profile Picture</p>
                    </div>
                    <div class="drop-shadow bg-white p-6  rounded-xl">


                        <div class="flex justify-center items-center">
                            <asp:Image ID="profilePic" class="w-[300px]  rounded-lg" Height="300" runat="server" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();" />
                        </div>

                        <%--                      <div class="flex justify-center pt-6">
                            <asp:FileUpload ID="fileUpload" runat="server" Style="cursor: pointer;" onchange="previewImage(this);" />
                        </div>--%>
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
                                    <asp:TextBox ID="txtFirstName" runat="server" class="bg-gray-100 rounded-xl p-3" ReadOnly="true"></asp:TextBox>
                                </div>

                                <div>
                                    <div class="flex flex-col">
                                        <span class="pb-1.5">Email</span>
                                        <asp:TextBox ID="txtEmail" runat="server" class="bg-gray-100 rounded-xl p-3" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>


                            </div>

                            <!-- right -->
                            <div class="flex flex-col gap-5">
                                <div class="flex flex-col">
                                    <span class="pb-1.5">Last Name</span>
                                    <asp:TextBox ID="txtLastName" runat="server" class="bg-gray-100 rounded-xl p-3" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div>
                                    <div class="flex flex-col">
                                        <span class="pb-1.5">Phone Number</span>
                                        <asp:TextBox ID="txtPhoneNo" runat="server" class="bg-gray-100 rounded-xl p-3" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <!--DOB-->
                        <div>
                            <span>Birthday</span>
                            <div class="mt-1.5 grid grid-cols-3 gap-7">
                                <asp:TextBox ID="txtDay" runat="server" class="bg-gray-100 rounded-xl p-3" ReadOnly="true"></asp:TextBox>

                                <asp:TextBox ID="txtMonth" runat="server" class="bg-gray-100 rounded-xl p-3" ReadOnly="true"></asp:TextBox>

                                <asp:TextBox ID="txtYear" runat="server" class="bg-gray-100 rounded-xl p-3" ReadOnly="true"></asp:TextBox>
                            </div>
                            <%--                            <div class="w-auto pt-7">
        <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" class="bg-blue-600 text-white rounded-lg px-8 py-3" />
    </div>--%>
                        </div>
                    </div>

                </div>
            </div>
        </div>


    </div>
</asp:Content>
