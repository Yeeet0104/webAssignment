<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="editCategory.aspx.cs" Inherits="webAssignment.Admin.Category.editCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function previewImages(input) {
            var imageContainer = document.getElementById('<%= tumbnail.ClientID %>');

            if (input.files && input.files[0]) {
                var file = input.files[0]; // Directly use the first file
                var reader = new FileReader();
                reader.onload = function (e) {
                    imageContainer.src = e.target.result;
                }
                reader.readAsDataURL(file);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--First Row-->
    <div class="flex flex-row justify-between font-medium pt-3 items-center mb-3">
        <div class="flex flex-col w-3/5">
            <div class="text-2xl font-bold ">
                <p>
                    Edit Category
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
            <div class="relative">
                <i class="fa-solid fa-download text-blue-500 absolute text-lg left-4 top-5 transform -translate-y-1/2"></i>
                <asp:Button ID="saveChanges" runat="server" Text="Save Changes" class="pl-11 pr-5 py-2.5 text-sm bg-gray-200 text-blue-500 rounded-lg" OnClick="saveChanges_Click" />
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
                <asp:TextBox class="p-3 rounded-xl bg-gray-100 mb-4" ID="editCategoryName" runat="server" ToolTip="Category Name" placeholder="Type Category Name..."></asp:TextBox>
                <span class="text-gray-500">Description</span>
                <asp:TextBox class="p-3 rounded-xl h-60 bg-gray-100" ID="editCategoryDes" runat="server" ToolTip="Category Description" placeholder="Type Category Description here..." TextMode="MultiLine">
                </asp:TextBox>
            </div>
        </div>
        <div class="col-span-2">
            <div class="flex flex-col gap-3 bg-white p-5  rounded-xl">
                <p class="mb-2">Thumbnail</p>
                <span class="text-sm">Photo
                </span>
                <div class="flex flex-col justify-center items-center text-sm gap-2 border-dotted">
                    <div class="py-1">
                        <asp:Image ID="tumbnail" runat="server" Height="216" Width="216" onclick="document.getElementById('<%= fileUploadClientID %>').click();" />
                    </div>
                    <div id="removeImage" class="w-full flex flex-row gap-10 items-center justify-center hidden">

                        <div class="w-full flex flex-col items-center  justify-center">

                            <span class="p-2 w-12 h-12 bg-blue-500 text-white rounded-xl flex justify-center items-center">

                                <i class=" text-2xl fa-regular fa-image"></i>
                            </span>
                            <span class="mt-2 text-base leading-normal text-gray-500 ">Select Image From File</span>
                        </div>
                    </div>
                    <asp:Panel ID="PanelBackground" runat="server" />
                    <asp:FileUpload ID="fileImages" runat="server" Style="cursor: pointer; display: none" onchange="previewImages(this);" accept="image/*" />
                    <button type="button" onclick="document.getElementById('<%= fileImages.ClientID %>').click();" class="text-lg bg-blue-500 text-white py-1 px-3 rounded-lg cursor-pointer hover:bg-blue-600">
                        Choose File
                    </button>

                </div>
            </div>
        </div>



    </div>
</asp:Content>
