<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="addNewProduct.aspx.cs" Inherits="webAssignment.Admin.Product_Management.addNewProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function previewImage(input) {
            if (input.files && input.files[0]) {
                var removeOriStatement = document.getElementById('removeImage');
                var reader = new FileReader();
                reader.onload = function (e) {
                    var img = document.getElementById('<%= profilePic.ClientID %>');
                    img.classList.remove("hidden");
                    removeOriStatement.classList.add("hidden");
                    img.src = e.target.result;
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">
        <div class="flex flex-col">
            <div class="text-2xl font-bold ">
                <p>Product</p>
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

        <div class="flex justify-between">
            <div class="relative mr-2">
                <i class="fa-solid fa-download text-blue-500 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg  cursor-pointer" />
            </div>
            <div class="relative ml-2">
                <i class="fa-solid fa-plus absolute text-2xl left-4 top-5 text-white transform -translate-y-1/2"></i>
                <asp:Button ID="btnAddNewCust" runat="server" Text="Add Product" class="pl-11 pr-5 py-2.5 text-sm bg-blue-500 text-white rounded-lg cursor-pointer" />
            </div>

        </div>
    </div>
    <!--First Row-->
    <%--    <div class="flex flex-row justify-between font-medium pt-3 items-center">
        <div class="w-3/5 flex flex-row items-center">
            <asp:TextBox ID="productSearchBox" runat="server" CssClass="
                     w-11/12 px-4 py-2 mr-4 text-gray-700 bg-white border rounded-md focus:border-blue-500 focus:outline-none focus:ring h-fit"
                placeholder="Search...">


            </asp:TextBox>
            <asp:LinkButton ID="searchBtn" runat="server" CssClass="px-1 py-2 text-gray-700 rounded-md">
     <i class="fa-solid fa-magnifying-glass text-xl "></i>
            </asp:LinkButton>
        </div>
    </div>--%>

    <!--End-->

    <!--First Row-->
    <div class="grid grid-cols-5 text-base mt-3 gap-6 h-fit">
        <!--Right Col-->
        <div class="flex flex-col gap-6 col-span-4">
            <!-- General Description -->
            <div class="p-5 flex flex-col bg-white rounded-xl">
                <span class="mb-3 text-lg">General information</span>
                <span class="text-gray-500">Product Name</span>
                <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="newProductName" runat="server" ToolTip="Product Name" placeholder="Type Product Name..."></asp:TextBox>
                <span class="text-gray-500">Description</span>
                <asp:TextBox class="p-3 rounded-xl h-60 bg-gray-100" ID="newProductDes" runat="server" ToolTip="Product Name" placeholder="Type Product Description here..." TextMode="MultiLine">
                </asp:TextBox>
            </div>
            <!-- Media -->
            <div class="flex flex-col bg-white p-5 rounded-xl ">
                <p class="text-lg">Media</p>

                <span class="text-gray-500">Photo</span>
                <div class="flex flex-row justify-center items-center min-h-64">
                    <div id="image-bg-op" class="w-full h-full flex flex-col items-center px-4 py-6 bg-white text-blue rounded-lg shadow-lg tracking-wide uppercase border border-blue cursor-pointer border-dashed border-2
                        justify-center gap-5
                        ">
                        <!--Pic Box-->
                        <div class="py-1">
                            <asp:Image ID="profilePic" CssClass="hidden" runat="server" Height="216" Width="216" onclick="document.getElementById('<%= fileUploadClientID %>').click();" />
                        </div>
                        <div id="removeImage" class="w-full flex flex-col items-center  justify-center">

                            <span class="p-2 w-12 h-12 bg-blue-500 text-white rounded-xl flex justify-center items-center">

                                <i class=" text-2xl fa-regular fa-image"></i>
                            </span>
                            <span class="mt-2 text-base leading-normal text-gray-500 ">Select Image From File</span>
                        </div>
                        <asp:Panel ID="PanelBackground" runat="server" />
                        <asp:FileUpload ID="fileUpload" runat="server" Style="cursor: pointer; display: none" onchange="previewImage(this);" />
                        <button type="button" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();" class="bg-blue-500 text-white w-[20%] py-1 rounded-lg cursor-pointer hover:bg-blue-600">
                            Choose File
                        </button>

                    </div>
                </div>
            </div>
            <!-- Variation Pricing -->
            <div class="grid grid-cols-2 gap-6">
                <div class="p-5 col-span-1  bg-white rounded-xl">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="flex justify-between items-center mb-5">
                                <div class="flex flex-col gap-1">

                                    <span class="text-lg">Variants Pricing</span>
                                    <span class="text-sm text-gray-400">Enter the name and price for each variant.</span>
                                </div>
                                <asp:LinkButton ID="createTextRowBtn" runat="server" OnClick="createTextRowBtn_Click" CssClass="flex items-center gap-4 hover:bg-gray-200 px-2 p-1 rounded-lg drop-shadow-lg border">   
                                    <span>Add Variant</span>
                                    <i class="text-xl fa-regular fa-square-plus "></i>

                                </asp:LinkButton>
                                <%--<asp:Button ID="Button1" runat="server" Text="Create TextBox" OnClick="btnCreateTextBox_Click" />--%>
                            </div>
                            <div>
                                <div class='flex gap-4 items-center flex-wrap justify-evenly mb-4'>
                                    <asp:TextBox ID="variant1Tb" runat="server" CssClass="newVariation_input" Placeholder="Variant 1"></asp:TextBox>
                                    <asp:TextBox ID="priceVar1Tb" runat="server" CssClass="newVariation_input" Placeholder="Price for Variant 1"></asp:TextBox>
                                </div>
                                <asp:Panel ID="panelVariantTextBoxes" runat="server" CssClass="flex flex-col gap-4">
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-span-1">
                    <div class="p-5 flex flex-col bg-white rounded-xl ">

                        <p class="mb-5 text-lg">Stock</p>

                        <div class="flex flex-col gap-2">
                            <span class="text-gray-500">Quantity</span>
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="tbQuantity" runat="server" ToolTip="Product Name" placeholder="Type product quantity here..."></asp:TextBox>

                        </div>
                    </div>

                </div>
            </div>
            <!-- General Description -->

        </div>

        <!--Left Col-->
        <div class="col-span-1 flex flex-col gap-6">

            <div class="bg-white flex flex-col p-4 bg-white rounded-xl h-fit">

                <span class="mb-3">Category</span>
                <span class="text-gray-500">Product Name</span>
                <asp:DropDownList CssClass="p-3 bg-gray-100 mb-4 text-gray-500 rounded-xl" ID="DropDownList1" runat="server">
                    <asp:ListItem Value="-"> Select a category</asp:ListItem>
                </asp:DropDownList>
                <%--                <span class="text-gray-500">Product Description</span>
                <asp:DropDownList CssClass="p-3 bg-gray-100 text-gray-500 rounded-xl" ID="DropDownList2" runat="server">
                    <asp:ListItem Value="-">Select Tag</asp:ListItem>
                </asp:DropDownList>--%>
            </div>
            <div class="bg-white flex flex-col p-4 bg-white rounded-xl h-fit">
                <div class="flex flex-row justify-between mb-5 items-center">

                    <p>Status</p>
                    <asp:Label CssClass="bg-gray-200 rounded-xl p-2 px-4 text-gray-500" ID="lblnewProdStatus" runat="server" Text="Draft"></asp:Label>
                </div>
                <span class="text-gray-500">Product Description</span>
                <asp:DropDownList CssClass="p-3 bg-gray-100 text-gray-500 rounded-xl" ID="ddlnewProdStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlnewProdStatus_SelectedIndexChanged">
                    <asp:ListItem Value="draft">Draft</asp:ListItem>
                    <asp:ListItem Value="publish">Publish</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <!--End-->


    <!--End-->
    <style>
        .newVariation_input {
            padding: 0.75rem;
            background-color: #F3F4F6;
            color: #9CA3AF;
            border-radius: 0.5rem;
        }
    </style>
</asp:Content>
