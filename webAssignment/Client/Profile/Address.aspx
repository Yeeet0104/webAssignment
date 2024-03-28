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
        <div id="shippingCollapse" class="w-full bg-black py-4 px-8 relative rounded-xl cursor-pointer">
            <asp:Label ID="lblCheckBox" runat="server" Text="SHIPPING ADDRESS" class="text-xl text-white block font-medium w-full"></asp:Label>
            <i id="shippingCollapseIcon" class="fa-solid fa-chevron-down hover:cursor-pointer text-white"></i>
        </div>

        <asp:CheckBox ID="collapsible" runat="server" class="hidden" />
        <div id="contentDiv" class="w-full">
            <div class="w-full border border-gray-400 rounded-lg flex flex-col gap-3 tex gap-6 text-gray-704 p-4">
                <div class="grid grid-cols-2 gap-6">
                    <!-- Add New Address-->
                    <div class="p-4 border border-gray-400 hover:border-black relative">
                        <span class="text-gray-600">New Address</span>
                        <i id="plusIcon" class="fa-solid fa-plus text-3xl font-normal"></i>
                    </div>

                    <!-- Default Address-->
                    <div class="p-4 border border-black">
                        <div class="text-lg">
                            <span class="font-bold pb-4">Dexter Goh</span><br />
                            <span class="">K16 Jalan Ketapang<br />
                                Taman Setapak<br />
                                53000, Kuala Lumpur, Malaysia<br />
                                0123136742</span>
                        </div>
                        <div class="flex justify-between pt-2">
                            <div>
                                <span class="pr-2 underline hover:cursor-pointer">Edit</span>
                                <span class="pl-2 underline hover:cursor-pointer">Remove</span>
                            </div>
                            <span class="font-bold">Default</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="billingCollapse" class="w-full bg-black py-4 px-8 relative rounded-xl mt-10 cursor-pointer">
            <asp:Label ID="lblCheckBox2" runat="server" Text="BILLING ADDRESS" class="text-xl text-white block font-medium w-full"></asp:Label>
            <i id="billingCollapseIcon" class="fa-solid fa-chevron-down hover:cursor-pointer text-white"></i>
        </div>
        <!--Billing Address-->
        <asp:CheckBox ID="collapsible2" runat="server" class="hidden" />
        <div id="contentDiv2" class="w-full">
            <div class="w-full border border-gray-400 rounded-lg flex flex-col gap-3 tex gap-6 text-gray-704 p-4">
                <div class="grid grid-cols-2 gap-6">
                    <!-- Add New Address-->
                    <div class="p-4 border border-gray-400 hover:border-black relative">
                        <span class="text-gray-600">New Address</span>
                        <i id="plusIcon2" class="fa-solid fa-plus text-3xl font-normal"></i>
                    </div>

                    <!-- Default Address-->
                    <div class="p-4 border border-black">
                        <div class="text-lg">
                            <span class="font-bold pb-4">Dexter Goh</span><br />
                            <span class="">K16 Jalan Ketapang<br />
                                Taman Setapak<br />
                                53000, Kuala Lumpur, Malaysia<br />
                                0123136742</span>
                        </div>
                        <div class="flex justify-between pt-2">
                            <div>
                                <span class="pr-2 underline hover:cursor-pointer">Edit</span>
                                <span class="pl-2 underline hover:cursor-pointer">Remove</span>
                            </div>
                            <span class="font-bold">Default</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <!--End of Shipping Content-->
</asp:Content>
