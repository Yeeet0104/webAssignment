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
                                <asp:DropDownList ID="ddlDay" runat="server" class="bg-gray-100 rounded-xl p-3">
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

                                <asp:DropDownList ID="ddlMonth" runat="server" class="bg-gray-100 rounded-xl p-3">
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

                                <asp:DropDownList ID="ddlYear" runat="server" class="bg-gray-100 rounded-xl p-3">
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
