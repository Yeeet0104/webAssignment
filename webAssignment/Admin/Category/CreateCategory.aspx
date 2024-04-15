<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Layout/AdminPage.Master" AutoEventWireup="true" CodeBehind="CreateCategory.aspx.cs" Inherits="webAssignment.Admin.Category.CreateCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function previewImages(input) {
            var imageContainer = document.getElementById('removeImage');
            imageContainer.innerHTML = ''; // Clear the container

            if (input.files && input.files[0]) {
                var file = input.files[0]; // Directly use the first file
                var reader = new FileReader();
                reader.onload = function (e) {
                    var img = document.createElement('img');
                    img.classList.add('preview-image');
                    img.src = e.target.result;
                    imageContainer.appendChild(img);
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
                <asp:Button ID="btnAddNewCust" runat="server" Text="Add Category" class="pl-11 pr-5 py-2.5 text-sm bg-blue-500 text-white rounded-lg" OnClick="btnAddNewCust_Click" />
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
                <div class="flex flex-col justify-center items-center text-sm gap-2 border-dotted">
                    <div class="py-1">
                        <asp:Image ID="profilePic" CssClass="hidden" runat="server" Height="216" Width="216" onclick="document.getElementById('<%= fileUploadClientID %>').click();" />
                    </div>
                    <div id="removeImage" class="w-full flex flex-row gap-10 items-center  justify-center">

                        <div class="w-full flex flex-col items-center  justify-center">

                            <span class="p-2 w-12 h-12 bg-blue-500 text-white rounded-xl flex justify-center items-center">

                                <i class=" text-2xl fa-regular fa-image"></i>
                            </span>
                            <span class="mt-2 text-base leading-normal text-gray-500 ">Select Image From File</span>
                        </div>
                    </div>
                    <asp:Panel ID="PanelBackground" runat="server" />
                    <asp:FileUpload ID="fileImages" runat="server" Style="cursor: pointer; display: none" onchange="previewImages(this);" />
                    <button type="button" onclick="document.getElementById('<%= fileImages.ClientID %>').click();" class="text-lg bg-blue-500 text-white py-1 px-3 rounded-lg cursor-pointer hover:bg-blue-600">
                        Choose File
                    </button>

                </div>
            </div>
        </div>



        <asp:Panel ID="popUpPanel" runat="server" CssClass="hidden popUp fixed z-1 w-full h-full top-0 left-0 bg-gray-200 bg-opacity-50 flex justify-center items-center ">
            <!-- Modal content -->
            <div class="popUp-content w-1/3 h-fit flex flex-col bg-white p-5 rounded-xl flex flex-col gap-3 drop-shadow-lg">

                <div class="grid grid-cols-3 w-full h-fit justify-center flex p-0">
                    <div>
                    </div>
                    <p class="text-2xl text-red-600 font-bold text-center">Are You Sure?</p>
                    <span class="w-auto flex items-center justify-end text-3xl rounded-full">


                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="closePopUp_Click">
            <i class=" fa-solid fa-xmark"></i>
                        </asp:LinkButton>

                    </span>

                </div>
                <div class="flex flex-col justify-center items-center gap-5">

                    <div style="font-size: 64px">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Admin/PopUpImages/add.gif" AlternateText="trashcan" CssClass="w-28 h-28 " />

                    </div>
                    <p class="bold text-lg break-normal text-center">Are you sure you want to add the following item?</p>
                    <div class="bold text-lg flex flex-col items-center">
                        <asp:Image ID="bannerImage" runat="server" Height="200px" Width="200px" />
                        <div>
                            <span>CategoryID:</span><asp:Label ID="categoryID" runat="server" Text="[CategoryID]"></asp:Label>
                        </div>
                        <div>
                            <span>CategoryName</span><asp:Label ID="categoryName" runat="server" Text="[categoryName]"></asp:Label>
                        </div>
                    </div>
                    <asp:TextBox ID="TextBox1" runat="server" TextMode="Password" CssClass="p-2 px-4 border rounded-xl" placeholder="Enter password to confirm"></asp:TextBox>
                    <div>

                        <asp:Button ID="cancelBtn" runat="server" Text="Cancel" CssClass="bg-gray-300 p-2 px-4 rounded-lg cursor-pointer" OnClick="btnCancelDelete_Click" />
                        <asp:Button ID="ConfirmAdd" runat="server" Text="Confirm" CssClass="bg-red-400 p-2 px-4 rounded-lg cursor-pointer" OnClick="btnConfirmAdd_Click" />
                    </div>
                </div>
            </div>

        </asp:Panel>
    </div>
    <!--End-->
</asp:Content>
