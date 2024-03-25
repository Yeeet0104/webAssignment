<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="addNewProduct.aspx.cs" Inherits="webAssignment.Admin.Product_Management.addNewProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center pb-5">
        <div class="flex flex-col">
            <div class="text-2xl font-bold ">
                <p>Product</p>
            </div>
            <div class="flex flex-row text-sm py-2">
                <div class="text-blue-600">Dashboard</div>
                <i class="fa-solid fa-caret-right px-6 mt-1"></i>
                <div>Products List</div>
            </div>
        </div>

        <div class="flex justify-between">
            <div class="relative mr-2">
                <i class="fa-solid fa-download text-blue-500 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg" />
            </div>
            <div class="relative ml-2">
                <i class="fa-solid fa-plus absolute text-2xl left-4 top-5 text-white transform -translate-y-1/2"></i>
                <asp:Button ID="btnAddNewCust" runat="server" Text="Add Product" class="pl-11 pr-5 py-2.5 text-sm bg-blue-500 text-white rounded-lg" />
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
                    <label class="w-full h-full flex flex-col items-center px-4 py-6 bg-white text-blue rounded-lg shadow-lg tracking-wide uppercase border border-blue cursor-pointer border-dashed border-2
                        justify-center 
                        ">
                        <span class="p-2 w-12 h-12 bg-blue-500 text-white rounded-xl flex justify-center items-center">

                            <i class=" text-2xl fa-regular fa-image"></i>
                        </span>
                        <span class="mt-2 text-base leading-normal text-gray-500 ">Select Image From File</span>
                        <asp:Panel ID="PanelBackground" runat="server" />
                        <asp:FileUpload ClientIDMode="Static" ID="imageInputPd" runat="server" accept="image/*" />

                    </label>

                </div>
            </div>
            <!-- General Description -->
            <div class="p-5 flex flex-col bg-white rounded-xl">
                <span class="mb-5 text-lg">Pricing</span>
                <span class="text-gray-500">Base Price</span>
                <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="newProductBasePriceTb" runat="server" ToolTip="Product Name" placeholder="(RM) Type base price here ...."></asp:TextBox>

                <div class="grid grid-cols-2 gap-4">

                    <div class="flex flex-col gap-2">
                        <span class="text-gray-500">Discount Type</span>
                        <asp:DropDownList CssClass="p-3 bg-gray-100 text-gray-500 rounded-xl" ID="ddlDiscountType" runat="server">
                            <asp:ListItem Value="-">Select a discount type</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="flex flex-col gap-2">
                        <span class="text-gray-500">Discount Percentage(%)</span>
                        <asp:TextBox class="p-3 rounded-xl  bg-gray-100" ID="TextBox1" runat="server" ToolTip="Product Name" placeholder="Type discount percentage here... (1-100)" TextMode="SingleLine">
                        </asp:TextBox>

                    </div>
                </div>
                <div class="grid grid-cols-2 gap-4">

                    <div class="flex flex-col gap-2">
                        <span class="text-gray-500">Tax Class</span>
                        <asp:DropDownList CssClass="p-3 bg-gray-100 text-gray-500 rounded-xl" ID="ddlTaxClass" runat="server">
                            <asp:ListItem Value="-">Select a tax class</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="flex flex-col gap-2">
                        <span class="text-gray-500">VAT Amount(%)</span>
                        <asp:TextBox class="p-3 rounded-xl  bg-gray-100" ID="tbVatAmount" runat="server" ToolTip="Product Name" placeholder="Type VAT Amount here... (1-100)" TextMode="SingleLine">
                        </asp:TextBox>

                    </div>
                </div>
            </div>
            <!-- General Description -->
            <div class="p-5 flex flex-col bg-white rounded-xl">
                <p class="mb-5 text-lg">Inventory</p>
                <div class="grid grid-cols-3 gap-4">
                    <div class="flex flex-col gap-2">
                        <span class="text-gray-500">SKU</span>
                        <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="tbSKU" runat="server" ToolTip="Product Name" placeholder="Type product SKU here..."></asp:TextBox>

                    </div>
                    <div class="flex flex-col gap-2">
                        <span class="text-gray-500">Barcode</span>
                        <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="tbProductBarcode" runat="server" ToolTip="Product Name" placeholder="Product barcode..."></asp:TextBox>

                    </div>
                    <div class="flex flex-col gap-2">
                        <span class="text-gray-500">Quantity</span>
                        <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="tbQuantity" runat="server" ToolTip="Product Name" placeholder="Type product quantity here..."></asp:TextBox>

                    </div>
                </div>
            </div>
        </div>

        <!--Left Col-->
        <div class="col-span-1 flex flex-col gap-6">

            <div class="bg-white flex flex-col p-4 bg-white rounded-xl h-fit">

                <span class="mb-3">Category</span>
                <span class="text-gray-500">Product Name</span>
                <asp:DropDownList CssClass="p-3 bg-gray-100 mb-4 text-gray-500 rounded-xl" ID="DropDownList1" runat="server">
                    <asp:ListItem Value="-"> Select a category</asp:ListItem>
                </asp:DropDownList>
                <span class="text-gray-500">Product Description</span>
                <asp:DropDownList CssClass="p-3 bg-gray-100 text-gray-500 rounded-xl" ID="DropDownList2" runat="server">
                    <asp:ListItem Value="-">Select Tag</asp:ListItem>
                </asp:DropDownList>
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
</asp:Content>
