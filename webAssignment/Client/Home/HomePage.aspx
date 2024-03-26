<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="webAssignment.Client.Home.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="HomePage.css">
</asp:Content>
<asp:Content ID="HomePageContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-white">

        <!-- Intro Section -->
        <div class="relative">
            <div class="intro">
                <img class="w-full" src="Home Images/intro_bg.png">
                <div class="absolute md:top-[22%] md:right-[14%] top-[16%] right-[4%]">
                    <h2 class="font-bold md:text-4xl sm:text-2xl text-xl py-2">Discover Most<br/>Affordable<br/>Computer Accessories</h2>
                    <span class="md:text-lg sm:text-sm text-xs">Level up with high performance G-Tech's<br/>computer accessories, or even build your<br/>own pc with G-Tech computer parts.</span><br/>      
                    <button class="mt-3 bg-blue-500 rounded-lg w-[60%] sm:p-2 p-1 sm:text-lg text-xs hover:bg-blue-600 hover:cursor-pointer font-bold">Explore</button>
                </div>
            </div>
        </div>

        <!-- Category Section -->
        <div class="section">
            <h2 class="font-bold text-center text-4xl pt-12 pb-8">Product Categories</h2>
            <div class="flex flex-col sm:flex-row justify-center">
                <!-- Category Box 1 -->
                <a class="relative overflow-hidden m-6 sm:w-1/2 md:w-[40vw] rounded-lg shadow-stone-900 shadow-md transition duration-500 ease-in-out transform hover:scale-105" href="#">
                    <div class="bg-cover bg-center h-[42vh] sm:h-[66vh] flex items-end px-6 py-4 bg-zinc-900 relative">
                        <img src="Home Images/logitec.png" alt="Computer Accessories" class="absolute inset-0 w-full h-full object-cover transition duration-500 ease-in-out transform hover:opacity-100">
                        <div class="flex flex-col justify-center items-center text-center px-4 opacity-0 absolute inset-0 bg-black bg-opacity-60 transition duration-500 ease-in-out transform hover:opacity-100">
                            <h2 class="text-2xl font-semibold">Computer Accessories</h2>
                            <p class="text-gray-400 mt-2">Explore a wide range of accessories such as<br/>mouse, keyboard, and headphones etc.</p>
                        </div>
                    </div>
                </a>

                <!-- Category Box 2 -->
                <a class="relative overflow-hidden m-6 sm:w-1/2 md:w-[40vw] rounded-lg shadow-stone-900 shadow-md transition duration-500 ease-in-out transform hover:scale-105" href="#">
                    <div class="bg-cover bg-center h-[42vh] sm:h-[66vh] flex items-end px-6 py-4 bg-zinc-900 relative">
                        <img src="Home Images/Pc.png" alt="Computer Parts" class="absolute inset-0 w-[90%] h-full object-cover transition duration-500 ease-in-out transform hover:opacity-100">
                        <div class="flex flex-col justify-center items-center text-center px-4 opacity-0 absolute inset-0 bg-black bg-opacity-60 transition duration-500 ease-in-out transform hover:opacity-100">
                            <h2 class="text-2xl font-semibold">Computer Parts</h2>
                            <p class="text-gray-400 mt-2">Discover a variety of parts including<br/>CPU, GPU, and Motherboard etc.</p>
                        </div>
                    </div>
                </a>
            </div>
        </div>

        <!-- Featured Products Section -->
        <asp:ListView ID="ListViewProducts" runat="server" ItemPlaceholderID="productContainer">
            <LayoutTemplate>
                <div class="section px-[6%]">
                    <h2 class="font-bold text-center text-4xl p-8">Featured Products</h2>
                    <div class="products-overview relative">
                        <div class="flex justify-end">
                            <a href="../Product/Product.html" class="view-more-btn">
                                <span>View More</span>
                                <i class="fas fa-chevron-circle-right hvr-icon-forward"></i>
                            </a>
                        </div>
                        <div id="productContainer" class="text-black py-4">
                            <asp:PlaceHolder runat="server" ID="productContainer"></asp:PlaceHolder>
                        </div>
                        <div class="product-icon product-left-icon" onclick="prevProduct()">
                            <i class="fa-solid fa-angle-left"></i>
                        </div>
                        <div class="product-icon product-right-icon" onclick="nextProduct()">
                            <i class="fa-solid fa-angle-right"></i>
                        </div>
                    </div>
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <a class="product" href='<%# "../ProductDetailPage/" + Eval("link") %>'>
                    <div id="product" class="hover-content">
                        <div class="product-header">
                            <img src='<%# "Products Images/" + Eval("ProductImageUrl") %>' alt='<%# Eval("ProductName") %>' />
                        </div>
                        <div class="product-footer">
                            <span class="product-name"><%# Eval("ProductName") %></span>
                            <div class="price-and-sold">
                                <span>
                                    <span class="RM">RM </span>
                                    <span class="product-price"><%# Eval("Price") %></span>
                                </span>
                                <span class="sold-amount">NEW</span>
                            </div>
                        </div>
                    </div>
                </a>
            </ItemTemplate>
        </asp:ListView>
        

        <!-- Video Section -->
        <div class="section relative">
            <div class="videoSection">
                <div id="rtxImg">
                    <img src="Home Images/gtx1080ti.jpg">
                    <div class="videoText">
                        <span class="videoHeading">NVDIA RTX 1080 TI</span><br/>
                        <span class="videoDetails">Up to 3X of performance and breakthrough<br/>gaming technologies and VR experiences</span><br/>
                        <button class="mt-2 bg-blue-500 rounded-lg w-[60%] sm:p-2 p-1 sm:text-lg text-xs hover:bg-blue-600 hover:cursor-pointer font-bold">View Details</button>
                    </div>
                    <a id="playRtxVideo" class="play-rtx-video-btn" href="javascript:playVideo()" onmouseover="snackBar()">
                        <i class="fa-solid fa-circle-play"></i>
                    </a>
                </div>
                <div id="rtxVideoWrapper" class="responsiveVideo rtxVideo" >
                    <video id="rtxVideo" muted preload onpause="pauseVideo()">
                        <source src="Home Images/ExperienceRtxVideo.mp4" type="video/mp4">
                    </video>
                </div>
            </div>
            <div id="snackbar">Click the play button to experience NVDIA RTX 1080 TI in a gaming environment.</div>
        </div>

         <!-- Sign Up Small Section -->
        <div class="small-section offer">
            <div class="promo-section">
                <h2>SIGN UP TO GET</h2>
                <div class="promo-text">
                    <h1>10</h1>
                    <div>
                        <h2>% OFF</h2>
                        <span>ON YOUR FIRST PURCHASE</span>
                    </div>
                </div>
            </div>
            <div class="sign-up-details">
                <h3 class="font-bold text-3xl py-4">After signing up, you'll get</h3>
                <ul>
                    <li>Exclusive offers from time to time</li>
                    <li>Promotions</li>
                    <li>G-Tech Lastest News including new arrivals,<br/> upcoming promotions and special events.</li>
                </ul>
            </div>
            <div class="link-btn">
                <a class="sign-up-btn bg-blue-600 rounded-lg hover:bg-blue-700 font-bold" href=#">Sign Up</a>
            </div>
        </div>

        <!-- Build PC Description Section -->
        <div class="section buildOwnPc-bg">
            <div class="build-pc-caption">
                <h1 class="font-bold text-5xl py-4">BUILD YOUR PC,<br/> BUILD YOUR DREAM.</h1>
                <span>With G-Tech products, you can build your own pc easily.</span>
            </div>
            <img class="voucher hvr-buzz-out" src="Home Images/voucher.png" onclick="copyVoucherCode()" alt="voucher">
        </div>

        <!-- Partnership Section -->
        <div class="partnership-bg">
            <div class="partnership-section">
                <h2  class="text-gray-900">OUR PARTNERS</h2>
                <div class="partnership-logos">
                    <img src="Logos/appleLogo.png" alt="appleLogo">
                    <img src="Logos/samsungLogo.png" alt="samsungLogo">
                    <img src="Logos/asusLogo.png" alt="asusLogo">
                    <img src="Logos/huaweiLogo.png" alt="huaweiLogo">
                    <img src="Logos/logitechLogo.png" alt="logitechLogo">
                    <img src="Logos/hpLogo.png" alt="hpLogo">
                    <img src="Logos/corsairLogo.png" alt="corsairLogo">
                    <img src="Logos/msiLogo.png" alt="msiLogo">
                    <img src="Logos/pnyLogo.png" alt="pnyLogo">
                    <img src="Logos/intelLogo.png" alt="intelLogo">
                    <img src="Logos/amdLogo.png" alt="amdLogo">
                    <img src="Logos/nzxtLogo.png" alt="nzxtLogo">
                </div>
            </div>
        </div>

    </div>

    <script>

        // Prev and next product function
        function nextProduct() {
            productContainer.scrollLeft += 400;
        }

        function prevProduct() {
            productContainer.scrollLeft -= 400;
        }

        // Copy Voucher Code
        function copyVoucherCode() {

            var voucherCode = 'GTECHYYDS';

            navigator.clipboard.writeText(voucherCode);
            alert("Congratulations! You have found our hidden voucher! "
                + "The Voucher Code: " + voucherCode + " have been copied to your clipboard. "
                + "Enjoy shopping with G-TECH by using the voucher code!");
        }
         
        // Play Video Function
        var rtxImg = document.getElementById("rtxImg");
        var rtxVideoWrapper = document.getElementById("rtxVideoWrapper");
        var rtxVideo = document.getElementById("rtxVideo");

        function playVideo() {
            rtxVideo.play();
            rtxVideoWrapper.style.display = "block";
            rtxImg.style.display = "none";
        }

        function pauseVideo() {
            rtxVideo.pause();
            if (rtxVideo.paused === true) {
                rtxImg.style.display = "block";
                rtxVideoWrapper.style.display = "none";
            }
        }

        // Snack Bar before playing the video
        function snackBar() {
            var x = document.getElementById("snackbar");
            x.className = "show";
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        }

    </script>

</asp:Content>
