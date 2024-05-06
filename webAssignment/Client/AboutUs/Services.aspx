<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="Services.aspx.cs" Inherits="webAssignment.Client.AboutUs.Services" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="relative border border-black h-[675px] w-full ">
        <asp:Image ID="imgBackground" runat="server" CssClass="w-full h-full object-cover object-center" ImageUrl="~/Client/AboutUs/image/service.jpg" />
        <div class="w-full h-full right-0 top-0 absolute " style="background: rgba(0, 0, 0, 0.5)"></div>
        <div class="w-full h-full right-0 top-0 absolute z-1 flex justify-center items-center">
            <span class="text-8xl text-white font-bold mb-10">Our Services</span>
        </div>
    </div>
    <div class="max-w-7xl mx-auto py-12">
        <div class="w-full flex justify-between items-center gap-5 p-4">

            <!-- Text-->
            <div class="flex-1 flex flex-col gap-10 px-4 py-10">
                <h2 class="font-bold text-4xl text-center">Technical Support and Troubleshooting</h2>
                <p class="text-xl text-gray-600 text-justify">
                    Not sure which computer parts or accessories are right for you? Let our experienced staff guide you.<br /><br />
                    We provide personalized product consultations and recommendations based on your specific requirements, budget, and usage scenarios. Whether you're looking for a new graphics card, a gaming monitor, or a storage solution, we'll help you find the perfect fit for your needs.
                </p>
            </div>
            <!-- Image -->
            <div class="flex-1 flex justify-end">
                <asp:Image ID="Image2" CssClass="rounded-xl shadow-gray-500 shadow-xl max-w-lg" runat="server" ImageUrl="~/Client/AboutUs/image/technical-service.jpg" />
            </div>
        </div>

    </div>
    <div class="max-w-7xl mx-auto py-12">
        <div class="w-full flex justify-between items-center gap-5 p-4">
            
            <!-- Image -->
            <div class="flex-1 flex justify-start">
                <asp:Image ID="Image1" CssClass="rounded-xl shadow-gray-500 shadow-xl max-w-lg" runat="server" ImageUrl="~/Client/AboutUs/image/suggest.jpg" />
            </div>
            <!-- Text-->
            <div class="flex-1 flex flex-col gap-10 px-4 py-10">
                <h2 class="font-bold text-4xl text-center">Product Consultation and Recommendations</h2>
                <p class="text-xl text-gray-600 text-justify">
                    Need assistance with your computer hardware or software? Our knowledgeable support team is here to help.<br /><br />
                    From troubleshooting common issues to providing expert advice on system optimization and upgrades, we offer prompt and reliable technical support to ensure that your computing experience remains smooth and hassle-free.
                </p>
            </div>
        </div>

    </div>
    <div class="max-w-7xl mx-auto py-12">
        <div class="w-full flex justify-between items-center gap-5 p-4">

            <!-- Text-->
            <div class="flex-1 flex flex-col gap-10 px-4 py-10">
                <h2 class="font-bold text-4xl text-center">Repair and Maintenance Services</h2>
                <p class="text-xl text-gray-600 text-justify">
                    Accidents happen, and technology sometimes needs a little TLC. That's where we come in.<br /><br />
                    Our skilled technicians offer repair and maintenance services for a wide range of computer hardware components and peripherals. Whether you need a quick fix for a malfunctioning component or a comprehensive overhaul of your system, you can count on us to get the job done quickly and effectively.
                </p>
            </div>
            <!-- Image -->
            <div class="flex-1 flex justify-end">
                <asp:Image ID="Image3" CssClass="rounded-xl shadow-gray-500 shadow-xl max-w-lg" runat="server" ImageUrl="~/Client/AboutUs/image/repair.jpg" />
            </div>
        </div>

    </div>
</asp:Content>
