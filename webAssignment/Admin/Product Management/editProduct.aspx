<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="editProduct.aspx.cs" Inherits="webAssignment.Admin.Product_Management.editProduct" %>

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

    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">
        <div class="flex flex-col">
            <div class="text-2xl font-bold ">
                <p>Edit Product</p>
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
    <div class="grid grid-cols-5 text-base mt-3 gap-6 h-fit">
        <!--Right Col-->
        <div class="flex flex-col gap-6 col-span-4">
            <!-- General Description -->
            <div class="p-5 flex flex-col bg-white rounded-xl">
                <span class="mb-3 text-lg">General information</span>
                <span class="text-gray-500">Product Name</span>
                <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="editTbProductName" runat="server" ToolTip="Product Name" placeholder="Type Product Name..."></asp:TextBox>
                <span class="text-gray-500">Description</span>
                <asp:TextBox class="p-3 rounded-xl h-60 bg-gray-100" ID="editTbProductDes" runat="server" ToolTip="Product Name" placeholder="Type Product Description here..." TextMode="MultiLine">
                </asp:TextBox>
            </div>
            <!-- Media -->
            <div class="flex flex-row justify-center items-center min-h-64">
                <div id="image-bg-op" class="w-full h-full flex flex-col items-center px-4 py-6 bg-white text-blue rounded-lg shadow-lg tracking-wide uppercase border border-blue cursor-pointer border-dashed border-2
        justify-center gap-5
        ">
                    <!--Pic Box-->
                    <div class="py-1">
                        <asp:Image CssClass="rounded-lg" ID="profilePic" runat="server" Height="216" Width="216" onclick="document.getElementById('<%= fileUploadClientID %>').click();" />
                    </div>
                    <div id="removeImage" class="hidden w-full flex flex-col items-center  justify-center">

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
                                    <%--                                <asp:TextBox ID="variant1Tb" runat="server" CssClass="editTbVariation_input" Placeholder="Variant 1"></asp:TextBox>
                                    <asp:TextBox ID="priceVar1Tb" runat="server" CssClass="editTbVariation_input" Placeholder="Price for Variant 1"></asp:TextBox>--%>
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
                            <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="editTbQuantity" runat="server" ToolTip="Product Name" placeholder="Type product quantity here..."></asp:TextBox>

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
                <asp:DropDownList CssClass="p-3 bg-gray-100 mb-4 text-gray-500 rounded-xl" ID="editDdlCategory" runat="server">
                    <asp:ListItem Value="-"> Select a category</asp:ListItem>
                    <asp:ListItem Value="Phone">Phone</asp:ListItem>
                    <asp:ListItem Value="Pc">Pc</asp:ListItem>
                    <asp:ListItem Value="Laptop">Laptop</asp:ListItem>
                </asp:DropDownList>
                <%--                <span class="text-gray-500">Product Description</span>
                <asp:DropDownList CssClass="p-3 bg-gray-100 text-gray-500 rounded-xl" ID="DropDownList2" runat="server">
                    <asp:ListItem Value="-">Select Tag</asp:ListItem>
                </asp:DropDownList>--%>
            </div>
            <div class="bg-white flex flex-col p-4 bg-white rounded-xl h-fit">
                <div class="flex flex-row justify-between mb-5 items-center">

                    <p>Status</p>
                    <asp:Label CssClass="bg-gray-200 rounded-xl p-2 px-4 text-gray-500" ID="editLblProdStatus" runat="server" Text="Draft"></asp:Label>
                </div>
                <span class="text-gray-500">Product Description</span>
                <asp:DropDownList CssClass="p-3 bg-gray-100 text-gray-500 rounded-xl" ID="editDdlProdStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlnewProdStatus_SelectedIndexChanged">
                    <asp:ListItem Value="Draft">Draft</asp:ListItem>
                    <asp:ListItem Value="Published">Published</asp:ListItem>
                    <asp:ListItem Value="OutOfStock">Out Of Stock</asp:ListItem>
                    <asp:ListItem Value="lowStock">Low Stock</asp:ListItem>
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
