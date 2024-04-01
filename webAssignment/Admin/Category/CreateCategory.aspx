<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="CreateCategory.aspx.cs" Inherits="webAssignment.Admin.Category.CreateCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center mb-3">
        <div class="flex flex-col w-3/5">
            <div class="text-2xl font-bold ">
                <p>
                    Create Category
                </p>
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
        <div class="flex w-full justify-end mb-3">
            <div class="relative mr-2">
                <i class="fa-solid fa-download text-blue-500 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="btnExport" runat="server" Text="Export" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg" />
            </div>
            <div class="relative ml-2">
                <i class="fa-solid fa-plus absolute text-2xl left-4 top-5 text-white transform -translate-y-1/2"></i>
                <asp:Button ID="btnAddNewCust" runat="server" Text="Add Category" class="pl-11 pr-5 py-2.5 text-sm bg-blue-500 text-white rounded-lg" />
            </div>

        </div>
    </div>

    <!-- Second Row -->
    <div class="grid grid-cols-10 gap-4">
        <div class="col-span-8 text-sm">
            <!-- General Description -->
            <div class="p-5 flex flex-col bg-white rounded-xl">
                <span class="mb-3 text-lg">General information</span>
                <span class="text-gray-500">Category Name</span>
                <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="newCategoryName" runat="server" ToolTip="Category Name" placeholder="Type Category Name..."></asp:TextBox>
                <span class="text-gray-500">Description</span>
                <asp:TextBox class="p-3 rounded-xl h-60 bg-gray-100" ID="newCategoryDes" runat="server" ToolTip="Category Description" placeholder="Type Category Description here..." TextMode="MultiLine">
                </asp:TextBox>
            </div>
        </div>
        <div class="col-span-2">
            <div class="flex flex-col gap-3 bg-white p-5  rounded-xl">
                <p class="mb-2">Thumbnail</p>
                <span class="text-sm">Photo
                </span>
                <div class="flex flex-row justify-center items-center text-sm">
                    <label class="w-full h-full flex flex-col items-center px-4 py-6 bg-white text-blue rounded-lg shadow-lg tracking-wide uppercase border border-blue cursor-pointer border-dashed border-2
                        justify-center 
                        "  style="min-height:200px">
                        <span class="p-2 w-8 h-8 bg-blue-500 text-white rounded-xl flex justify-center items-center">

                            <i class="fa-regular fa-image"></i>
                        </span>
                        <span class="mt-2 text-sm leading-normal text-gray-500 ">Select Image From File</span>
                        <%--<asp:FileUpload ID="imageInputPd" runat="server" accept="image/*" />--%>

                    </label>

                </div>
            </div>
        </div>



    </div>
</asp:Content>
