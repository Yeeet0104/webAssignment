<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ProfileMaster/ProfileMasterPage.master" AutoEventWireup="true" CodeBehind="Address.aspx.cs" Inherits="webAssignment.Client.Profile.Address" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        #contentDiv, #contentDiv2 {
            max-height: 0;
            overflow: hidden;
            transition: max-height 0.2s ease-out;
        }

        #shippingCollapseIcon, #billingCollapseIcon {
            position: absolute;
            top: calc(50% - 0.5em);
            right: 50px;
            color: white;
            transition: transform 0.2s linear;
        }

        #plusIcon, #plusIcon2 {
            position: absolute;
            bottom: 15px;
            left: 20px;
        }
    </style>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var collapsible = document.getElementById("<%= collapsible.ClientID %>");
            var shippingCollapseIcon = document.getElementById("shippingCollapseIcon");
            var contentDiv = document.getElementById("contentDiv");

            var collapsible2 = document.getElementById("<%= collapsible2.ClientID %>");
            var billingCollapseIcon = document.getElementById("billingCollapseIcon");
            var contentDiv2 = document.getElementById("contentDiv2");

            // Initially hide the contentDiv
            contentDiv.style.maxHeight = "0";
            contentDiv2.style.maxHeight = "0";

            // Add click event listeners for collapsible1 and collapsible2
            shippingCollapse.addEventListener("click", function () {
                toggleContent(collapsible, contentDiv, shippingCollapseIcon);
            });

            billingCollapse.addEventListener("click", function () {
                toggleContent(collapsible2, contentDiv2, billingCollapseIcon);
            });

            function toggleContent(collapsible, contentDiv, collapseIcon) {
                // Toggle the checked state of the checkbox
                collapsible.checked = !collapsible.checked;

                // Update the max-height of contentDiv based on the checked state
                if (collapsible.checked) {
                    contentDiv.style.maxHeight = "1000px";
                    collapseIcon.style.transform = "rotate(180deg)";
                } else {
                    contentDiv.style.maxHeight = "0";
                    collapseIcon.style.transform = "rotate(0deg)";
                }
            }
        });
    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="w-full h-full">
        <!--Shipping Address-->
        <div id="shippingCollapse" class="w-full bg-gray-800 py-4 px-8 relative rounded-lg cursor-pointer">
            <asp:Label ID="lblCheckBox" runat="server" Text="SHIPPING ADDRESS" class="text-xl text-white block font-medium w-full"></asp:Label>
            <i id="shippingCollapseIcon" class="fa-solid fa-chevron-down hover:cursor-pointer text-white"></i>
        </div>

        <asp:CheckBox ID="collapsible" runat="server" class="hidden" />
        <div id="contentDiv" class="w-full">
            <div class="w-full border border-gray-400 rounded-lg flex flex-col p-4 bg-white">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [address_id], [address_line2], [address_line1], [first_name], [last_name], [zip_code], [city], [state], [countryCode], [phone_number] FROM [Address] WHERE [address_type] = 'shipping'"></asp:SqlDataSource>
                <asp:ListView ID="shippingAddressList" runat="server" DataSourceID="SqlDataSource1">
                    <LayoutTemplate>
                        <div runat="server" id="itemPlaceholderContainer" class="grid grid-cols-2 gap-5">
                            <asp:LinkButton runat="server" id="addNewShippingAddress" OnClick="addNewShippingAddress_Click" class="p-4 border border-gray-400 hover:border-black hover:cursor-pointer relative">
                                <span class="text-gray-600">New Address</span>
                                <i id="plusIcon" class="fa-solid fa-plus text-3xl font-normal"></i>
                            </asp:LinkButton>
                            <span runat="server" id="itemPlaceholder" />
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="flex flex-col">
                            <div class="border border-gray-400 p-4">
                                <h4 class="font-bold pb-1 text-lg"><%# Eval("first_name") %> <%# Eval("last_name") %></h4>
                                <p class="text-lg"><%# Eval("address_line1") %></p>
                                <p class="text-lg"><%# Eval("address_line2") %></p>
                                <p class="text-lg"><%# Eval("zip_code") %>, <%# Eval("city") %>, <%# Eval("state") %>, <%# Eval("countryCode") %></p>
                                <p class="text-lg"><%# Eval("phone_number") %></p>

                                <div class="pt-2">
                                    <asp:LinkButton ID="editButton" CommandArgument='<%# Eval("address_id") %>' OnClick="editButton_Click" runat="server" class="pr-2 underline hover:cursor-pointer" Text="Edit" />
                                    <asp:LinkButton ID="removeButton" runat="server" CommandArgument='<%# Eval("address_id") %>' OnClick="removeButton_Click" class="pr-2 underline hover:cursor-pointer" Text="Remove" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>

        <!--Billing Address-->
        <div id="billingCollapse" class="w-full bg-gray-800 py-4 px-8 relative rounded-xl mt-10 cursor-pointer">
            <asp:Label ID="lblCheckBox2" runat="server" Text="BILLING ADDRESS" class="text-xl text-white block font-medium w-full"></asp:Label>
            <i id="billingCollapseIcon" class="fa-solid fa-chevron-down hover:cursor-pointer text-white"></i>
        </div>

        <asp:CheckBox ID="collapsible2" runat="server" class="hidden" />
        <div id="contentDiv2" class="w-full">
            <div class="w-full border border-gray-400 rounded-lg flex flex-col p-4 bg-white">
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [address_id], [address_line2], [address_line1], [first_name], [last_name], [zip_code], [city], [state], [countryCode], [phone_number] FROM [Address] WHERE [address_type] = 'billing'"></asp:SqlDataSource>
            <asp:ListView ID="billingAddressList" runat="server" DataSourceID="SqlDataSource2">
                <LayoutTemplate>
                    <div runat="server" id="itemPlaceholderContainer" class="grid grid-cols-2 gap-5">
                        <asp:LinkButton runat="server" id="addNewBillingAddress" OnClick="addNewBillingAddress_Click" class="p-4 border border-gray-400 hover:border-black hover:cursor-pointer relative">
    <span class="text-gray-600">New Address</span>
    <i id="plusIcon" class="fa-solid fa-plus text-3xl font-normal"></i>
</asp:LinkButton>
                        <span runat="server" id="itemPlaceholder" />
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="flex flex-col">
                        <div class="border border-gray-400 p-4">
                            <h4 class="font-bold pb-1 text-lg"><%# Eval("first_name") %> <%# Eval("last_name") %></h4>
                            <p class="text-lg"><%# Eval("address_line1") %></p>
                            <p class="text-lg"><%# Eval("address_line2") %></p>
                            <p class="text-lg"><%# Eval("zip_code") %>, <%# Eval("city") %>, <%# Eval("state") %>, <%# Eval("countryCode") %></p>
                            <p class="text-lg"><%# Eval("phone_number") %></p>

                            <div class="pt-2">
                                <asp:LinkButton ID="editButton" CommandArgument='<%# Eval("address_id") %>' OnClick="editButton_Click" runat="server" class="pr-2 underline hover:cursor-pointer" Text="Edit" />
                                <asp:LinkButton ID="removeButton" runat="server" CommandArgument='<%# Eval("address_id") %>' OnClick="removeButton_Click" class="pr-2 underline hover:cursor-pointer" Text="Remove" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                
            </asp:ListView>
        </div>
            </div>
    </div>
    <!--End of Shipping Content-->
</asp:Content>
