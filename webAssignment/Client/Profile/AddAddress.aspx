<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="AddAddress.aspx.cs" Inherits="webAssignment.Client.Profile.AddAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="px-48 py-8">
        <div class="rounded-lg flex flex-col gap-3 text-xl text-gray-704 p-4">
            <asp:Label ID="lblAddress" runat="server" class="text-gray-900 font-bold text-3xl" Text="Label"></asp:Label>
            <div class="flex pt-3">
                <span class="w-1/2 font-semibold">Username</span>
            </div>
            <div class="flex w-full gap-14 pb-3">
                <asp:TextBox class="border border-gray-300 w-1/2 px-3 py-4" ID="txtFirstName" runat="server" placeholder="First name"></asp:TextBox>
                <asp:TextBox class="border border-gray-300 w-1/2 px-3 py-4" ID="txtLastName" runat="server" placeholder="Last name"></asp:TextBox>
            </div>
            <span class="font-semibold">Address</span>
            <asp:TextBox class="border border-gray-300 px-3 py-4 mb-3" ID="txtAddressLine1" placeholder="Address Line 1" runat="server"></asp:TextBox>
            <asp:TextBox class="border border-gray-300 px-3 py-4" ID="txtAddressLine2" placeholder="Address Line 2" runat="server"></asp:TextBox>
            <div class="flex gap-6 font-semibold mt-3">
                <span class="w-1/4">Country</span>
                <span class="w-1/4">Region/State</span>
                <span class="w-1/4">City</span>
                <span class="w-1/4">Zip Code</span>
            </div>
            <div class="flex gap-6">
                <asp:DropDownList ID="ddlCountry" runat="server" class="w-1/4 border border-gray-300 px-3 py-4">
                    <asp:ListItem>Malaysia - MAL</asp:ListItem>
                    <asp:ListItem>Argentina - ARG</asp:ListItem>
                    <asp:ListItem>Australia - AUS</asp:ListItem>
                    <asp:ListItem>Austria - AUT</asp:ListItem>
                    <asp:ListItem>Belgium - BEL</asp:ListItem>
                    <asp:ListItem>Bulgaria - BGR</asp:ListItem>
                    <asp:ListItem>Canada - CAN</asp:ListItem>
                    <asp:ListItem>Chile - CHL</asp:ListItem>
                    <asp:ListItem>China - CHN</asp:ListItem>
                    <asp:ListItem>Croatia - HRV</asp:ListItem>
                    <asp:ListItem>Czech Republic - CZE</asp:ListItem>
                    <asp:ListItem>Denmark - DNK</asp:ListItem>
                    <asp:ListItem>Egypt - EGY</asp:ListItem>
                    <asp:ListItem>Finland - FIN</asp:ListItem>
                    <asp:ListItem>France - FRA</asp:ListItem>
                    <asp:ListItem>Germany - DEU</asp:ListItem>
                    <asp:ListItem>Greece - GRC</asp:ListItem>
                    <asp:ListItem>Hungary - HUN</asp:ListItem>
                    <asp:ListItem>India - IND</asp:ListItem>
                    <asp:ListItem>Indonesia - IDN</asp:ListItem>
                    <asp:ListItem>Ireland - IRL</asp:ListItem>
                    <asp:ListItem>Italy - ITA</asp:ListItem>
                    <asp:ListItem>Japan - JPN</asp:ListItem>
                    <asp:ListItem>Korea Republic - KOR</asp:ListItem>
                    <asp:ListItem>Latin America - LAC</asp:ListItem>
                    <asp:ListItem>Luxembourg - LUX</asp:ListItem>
                    <asp:ListItem>Macau - MAC</asp:ListItem>
                    <asp:ListItem>Mexico - MEX</asp:ListItem>
                    <asp:ListItem>Morocco - MAR</asp:ListItem>
                    <asp:ListItem>Netherlands - NLD</asp:ListItem>
                    <asp:ListItem>New Zealand - NZL</asp:ListItem>
                    <asp:ListItem>Norway - NOR</asp:ListItem>
                    <asp:ListItem>Philippines - PHL</asp:ListItem>
                    <asp:ListItem>Poland - POL</asp:ListItem>
                    <asp:ListItem>Portugal - PRT</asp:ListItem>
                    <asp:ListItem>Puerto Rico - PRI</asp:ListItem>
                    <asp:ListItem>Romania - ROU</asp:ListItem>
                    <asp:ListItem>Russian Federation - RUS</asp:ListItem>
                    <asp:ListItem>Saudi Arabia - SAU</asp:ListItem>
                    <asp:ListItem>Singapore - SGP</asp:ListItem>
                    <asp:ListItem>Slovakia (Slovak Republic) - SVK</asp:ListItem>
                    <asp:ListItem>Slovenia - SVN</asp:ListItem>
                    <asp:ListItem>South Africa - ZAF</asp:ListItem>
                    <asp:ListItem>Spain - ESP</asp:ListItem>
                    <asp:ListItem>Sweden - SWE</asp:ListItem>
                    <asp:ListItem>Switzerland - CHE</asp:ListItem>
                    <asp:ListItem>Thailand - THA</asp:ListItem>
                    <asp:ListItem>Turkey - TUR</asp:ListItem>
                    <asp:ListItem>United Arab Emirates - ARE</asp:ListItem>
                    <asp:ListItem>United Kingdom - GBR</asp:ListItem>
                    <asp:ListItem>United States - USA</asp:ListItem>
                    <asp:ListItem>Venezuela - VEN</asp:ListItem>
                    <asp:ListItem>Vietnam - VNM</asp:ListItem>

                </asp:DropDownList>

                <asp:TextBox class="w-1/4 border border-gray-300 px-3 py-4" ID="txtState" runat="server"></asp:TextBox>

                <asp:TextBox class="w-1/4 border border-gray-300 px-3 py-4" ID="txtCity" runat="server"></asp:TextBox>
                <asp:TextBox class="w-1/4 border border-gray-300 px-3 py-4" ID="txtZipCode" runat="server"></asp:TextBox>
            </div>
            <div class="flex gap-3 font-semibold mt-3">
                <span class="w-1/2">Phone Number</span>
            </div>
            <div class="flex row justify-between">
                <asp:TextBox class="w-1/3 border border-gray-300 px-3 py-4" ID="txtPhoneNumber" runat="server"></asp:TextBox>
                <div>
                    <asp:Button ID="cancelBtn" runat="server" Text="Cancel" class="px-10 py-3 mr-2 text-white w-auto bg-red-700 rounded-xl hover:cursor-pointer" OnClick="cancelBtn_Click" />
                    <asp:Button ID="btnAddAddress" runat="server" Text="Add" class="px-12 py-3 ml-2 text-white w-auto bg-blue-700 rounded-xl hover:cursor-pointer" OnClick="btnAddAddress_Click" />
                    <asp:Button ID="editBtn" runat="server" Text="Edit" class="px-12 py-3 ml-2 text-white w-auto bg-blue-700 rounded-xl hover:cursor-pointer" OnClick="btnEditAddress_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
