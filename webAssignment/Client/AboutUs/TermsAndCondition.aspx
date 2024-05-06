<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="TermsAndCondition.aspx.cs" Inherits="webAssignment.Client.AboutUs.TermsAndCondition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="relative border border-black h-[675px] w-full ">
        <asp:Image ID="imgBackground" runat="server" CssClass="w-full h-full object-cover object-center" ImageUrl="~/Client/AboutUs/image/tandc.jpg" />
        <div class="w-full h-full right-0 top-0 absolute " style="background: rgba(0, 0, 0, 0.6)"></div>
        <div class="w-full h-full right-0 top-0 absolute z-1 flex justify-center items-center">
            <span class="text-8xl text-white font-bold mb-10">Terms And Conditions</span>
        </div>
    </div>
    <div class="max-w-7xl mx-auto py-12">
        <div class="w-full flex flex-col gap-3 p-4">
            <!-- Company overview -->
            <div class="flex-1 flex flex-col gap-10 px-4 pt-10 pb-4">
                <h2 class="font-bold text-5xl text-left">Introduction</h2>
                <p class="text-xl text-gray-600 text-justify">
                    Welcome to G-tech! These terms and conditions outline the rules and regulations for the use of our website and the purchase of products from our online store. By accessing this website and/or making a purchase, you agree to be bound by these terms and conditions. If you disagree with any part of these terms and conditions, please do not use our website or make a purchase.
                </p>
            </div>


            <div class="flex-1 flex flex-col gap-3 text-xl text-justify text-gray-600  px-4 py-5">
                <h2 class="font-bold text-black text-3xl text-left">1. Website Use</h2>
                <p>
                    The content of this website is for your general information and use only. It is subject to change without notice.
                </p>
                <p>
                    Neither we nor any third parties provide any warranty or guarantee as to the accuracy, timeliness, performance, completeness, or suitability of the information and materials found or offered on this website for any particular purpose. You acknowledge that such information and materials may contain inaccuracies or errors, and we expressly exclude liability for any such inaccuracies or errors to the fullest extent permitted by law.
                </p>
                <p>
                    
Your use of any information or materials on this website is entirely at your own risk, for which we shall not be liable. It shall be your own responsibility to ensure that any products, services, or information available through this website meet your specific requirements.
                </p>
            </div>



            <div class="flex-1 flex flex-col gap-3 text-xl text-justify text-gray-600  px-4 py-5">
                <h2 class="font-bold text-black text-3xl text-left">2. Product Purchases</h2>
                <p>
                    All purchases made through our website are subject to availability. We reserve the right to refuse or cancel any order for any reason at any time.
                </p>
                <p>
                    Prices for our products are subject to change without notice. We shall not be liable to you or any third party for any modification, price change, suspension, or discontinuance of the service.
                </p>
                <p>
                    We reserve the right to refuse service to anyone for any reason at any time.
                </p>
                
                <p>
All purchases made through our website are subject to our Shipping and Returns Policy, which is hereby incorporated by reference.

                </p>
            </div>

            <div class="flex-1 flex flex-col gap-3 text-xl text-justify text-gray-600  px-4 py-5">
                <h2 class="font-bold text-black text-3xl text-left">3. Intellectual Property</h2>
                <p>
                    This website contains material which is owned by or licensed to us. This material includes, but is not limited to, the design, layout, look, appearance, and graphics. Reproduction is prohibited other than in accordance with the copyright notice, which forms part of these terms and conditions.
                </p>
                <p>
                   All trademarks reproduced in this website, which are not the property of, or licensed to the operator, are acknowledged on the website.
                </p>
                <p>
Unauthorized use of this website may give rise to a claim for damages and/or be a criminal offense.                </p>
                
            </div>
            
            <div class="flex-1 flex flex-col gap-3 text-xl text-justify text-gray-600  px-4 py-5">
                <h2 class="font-bold text-black text-3xl text-left">4. Cancelation and Refund Policy</h2>
                <p>
                    Cancelation and refund requests can only be processed if the order status is pending. Once the order status changes to processing or shipped, cancelation and refund requests cannot be accommodated.
                </p>
                <p>
                    Refunds for canceled orders will be issued to the original payment method within 5 business days of approval.
                </p>
                <p>
                    G-tech reserves the right to refuse cancelation and refund requests if the order status does not meet the specified criteria or if there is evidence of abuse or fraudulent activity.
                </p>
                <p>
                   This cancelation and refund policy is subject to change at any time without prior notice. It is the responsibility of users to review this policy periodically for updates.
                </p>

            </div>

            <div class="flex-1 flex flex-col gap-3 text-xl text-justify text-gray-600  px-4 py-5">
                <h2 class="font-bold text-black text-3xl text-left">5. Governing Law</h2>
                <p>
                    These terms and conditions are governed by and construed in accordance with the laws of [Your Jurisdiction] and you irrevocably submit to the exclusive jurisdiction of the courts in that location.
                </p>
            </div>

            <div class="flex-1 flex flex-col gap-3 text-xl text-justify text-gray-600  px-4 py-5">
                <h2 class="font-bold text-black text-3xl text-left">6. Updates to Terms and Conditions</h2>
                <p>
                    We reserve the right to update or modify these terms and conditions at any time without prior notice. Any changes will be effective immediately upon posting on our website. It is your responsibility to review these terms and conditions periodically for updates.
                </p>
            </div>
        </div>

    </div>
</asp:Content>
