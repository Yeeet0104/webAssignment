<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="AboutUsPage.aspx.cs" Inherits="webAssignment.Client.AboutUs.AboutUsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="relative border border-black h-[675px] w-full ">
        <asp:Image ID="imgBackground" runat="server" CssClass="w-full h-full object-cover object-center" ImageUrl="~/Client/AboutUs/image/about-us.jpg" />
        <div class="w-full h-full right-0 top-0 absolute " style="background: rgba(0, 0, 0, 0.4)"></div>
        <div class="w-full h-full right-0 top-0 absolute z-1 flex justify-center items-center">
            <span class="text-8xl text-white font-bold mb-10">About Us</span>
        </div>
    </div>
    <div class="max-w-7xl mx-auto py-12">
        <div class="w-full flex gap-5 p-4">
            <!-- Image -->
            <div class="flex-1">
                <asp:Image ID="imgCompany" runat="server" ImageUrl="~/Client/AboutUs/image/comp.jpeg" />
            </div>
            <!-- Company overview -->
            <div class="flex-1 flex flex-col gap-10 px-4 py-10">
                <h2 class="font-bold text-5xl">Our Company Overview</h2>
                <p class="text-lg text-gray-600">
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ante arcu, porttitor in elit id, iaculis condimentum ligula. Sed blandit sem nec sem posuere, et dictum ligula commodo. Proin id leo id neque mollis mollis. Sed id tempor lorem, a laoreet massa. Ut a rutrum nisl. Ut nec scelerisque nulla, eget dignissim ipsum. Proin lacus nibh, finibus sed ultricies quis, tempor a velit.
                </p>
                <div class="flex justify-end">
                    <asp:Button ID="btnLearnMore" runat="server" Text="Learn More" class="py-3 px-5 bg-blue-700 text-white font-semibold rounded-lg" />

                </div>
            </div>
        </div>

    </div>
    <div class="relative border border-black h-[450px] w-full text-white">
        <asp:Image ID="Image1" runat="server" CssClass="w-full h-full object-cover object-center" ImageUrl="~/Client/AboutUs/image/prod.jpeg" />
        <div class="w-full h-full right-0 top-0 absolute " style="background: rgba(0, 0, 0, 0.5)"></div>
        <div class="w-[80%] mx-auto h-full left-[10%] top-0 absolute z-1 flex justify-center items-center px-10 py-20">
            <div class="flex-1 flex flex-col justify-between h-full">
                <h2 class="text-7xl font-bold">The Features of Our Product</h2>
                <div class="flex">
                    <asp:LinkButton ID="lnProd" runat="server" class="py-3 px-10 bg-transparent border border-white border-[1px] text-white font-semibold rounded-lg text-md" PostBackUrl="~/Client/Product/ProductPage.aspx">Our Products</asp:LinkButton>
                </div>
            </div>
            <div class="flex-1 flex gap-5 h-full p-1">
                <div class="flex-1 border border-[3px] border-white rounded-lg flex flex-col justify-center items-center h-full gap-10">
                    <i class="fa-solid fa-star text-5xl"></i>
                    <span class="text-xl font-bold">High Quality</span>
                </div>
                <div class="flex-1 border border-[3px] border-white rounded-lg flex flex-col justify-center items-center h-full gap-10">
                    <i class="fa-solid fa-truck text-5xl"></i>
                    <span class="text-xl font-bold">Fast Delivery</span>
                </div>
                <div class="flex-1 border border-[3px] border-white rounded-lg flex flex-col justify-center items-center h-full gap-10">
                    <i class="fa-solid fa-shield text-5xl"></i>
                    <span class="text-xl font-bold">Warranty</span>
                </div>
            </div>
        </div>
    </div>

    <div class="max-w-7xl mx-auto py-12">
        <div class="w-full flex gap-5 p-4">
            
            <!-- Company overview -->
            <div class="flex-1 flex flex-col gap-10 px-4 py-10">
                <h2 class="font-bold text-6xl">Our Team</h2>
                <p class="text-lg text-gray-600">
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ante arcu, porttitor in elit id, iaculis condimentum ligula. Sed blandit sem nec sem posuere, et dictum ligula commodo. Proin id leo id neque mollis mollis. Sed id tempor lorem, a laoreet massa. Ut a rutrum nisl. Ut nec scelerisque nulla, eget dignissim ipsum. Proin lacus nibh, finibus sed ultricies quis, tempor a velit.
                </p>
                <div>
                    <asp:Button ID="Button1" runat="server" Text="Learn More" class="py-3 px-5 bg-blue-700 text-white font-semibold rounded-lg" />

                </div>
            </div>
            <!-- Image -->
            <div class="flex-1">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Client/AboutUs/image/group.jpg" />
            </div>
        </div>

    </div>
</asp:Content>
