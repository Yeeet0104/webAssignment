<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ClientMasterPage/ClientMasterPage.Master" AutoEventWireup="true" CodeBehind="WishlistPage.aspx.cs" Inherits="webAssignment.Client.Wishlist.WishlistPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="max-w-md md:max-w-2xl lg:max-w-5xl xl:max-w-7xl my-5 mx-auto min-h-[80vh] p-5">

        <div class="px-4">
            <!-- Header -->
            <div class="flex justify-between items-center mb-10">
                <span class="text-3xl text-gray-900 font-black p-4">Wishlist</span>
                <asp:Button ID="btnMoveAllToCart" runat="server" Text="Move All To Cart" class="mt-2 text-white bg-blue-600 hover:bg-blue-800 rounded-lg w-40 h-10 p-2 font-semibold text-sm cursor-pointer text-center transition duration-200 shadow-md" />
            </div>
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8">

                <asp:ListView ID="lvWishlist" runat="server">
                    <LayoutTemplate>
                        <div id="itemPlaceholder" runat="server">
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="bg-white rounded-xl border border-gray-200 shadow-md flex flex-col justify-between gap-4 min-h-[370px] p-5 relative hover:shadow-2xl transition duration-300">

                            <!-- Status Banner -->
                            <script>
                                var qty = "<%# Eval("Quantity") %>";
                                if (qty == 0) {
                                    document.write('<div class="absolute top-0 left-0 bg-gray-700 text-white py-2 px-3 font-bold rounded-tl-lg rounded-br-lg text-sm">Out Of Stock</div>');
                                } else {
                                    document.write('<div class="absolute top-0 left-0 bg-green-700 text-white py-2 px-3 font-bold rounded-tl-lg rounded-br-lg text-sm shadow shadow-lg">In Stock</div>');
                                }
                            </script>

                            <!-- Delete Button (top-right corner) -->
                            <button class="absolute top-1 right-1 bg-gray-500 text-white rounded-full p-2 shadow hover:bg-red-600 focus:outline-none w-8 h-8 flex items-center justify-center transition duration-200" id="<%# Eval("ProductID") %>">
                                <i class="fa-solid fa-heart-crack"></i>
                            </button>

                            <div class="h-[70%] flex justify-center items-center">
                                <asp:Image ID="imgProduct" runat="server" AlternateText="Image" ImageUrl='<%# Eval("ProductImageURL", "{0}") %>' class="w-auto h-full rounded-lg " />
                            </div>

                            <!-- Product Details -->
                            <div class="h-[25%] flex flex-col gap-2">
                                <h2 class="text-lg font-bold overflow-hidden whitespace-nowrap overflow-ellipsis"><%# Eval("ProductName") %></h2>
                                <div class="flex justify-between items-center">
                                    <p class="text-gray-700 font-semibold"><%# Eval("Price", "{0:c}") %></p>
                                    <button class="bg-blue-600 text-white rounded-lg py-2 px-3 shadow hover:bg-blue-800 focus:outline-none font-semibold text-sm transition duration-200">
                                        Add To Cart
                                    </button>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>

        </div>

    </div>
    <%--

            <!-- Product Grid -->
            <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-5">
                <div class="bg-white rounded-lg border border-gray-200 shadow-lg flex flex-col gap-2 h-[464px]">
                    <div class="h-[80%] relative">
                        <img src="../Cart/images/rtx4060ti.png" alt="Product Image" class="w-auto h-full mb-4 rounded-lg">

                        <!-- Status Banner -->
                        <div class="absolute top-0 left-0 bg-gray-700 bg-opacity-95 text-white py-2 px-3 font-bold rounded-tl-lg rounded-br-lg text-md">
                            Out of Stock
                        </div>

                        <!-- Delete Button (top-right corner) -->
                        <button class="absolute top-2 right-2 bg-gray-500 text-white rounded-full p-2 shadow hover:bg-red-600 focus:outline-none w-8 h-8 flex items-center justify-center transition duration-200">
                            <i class="fa-solid fa-heart-crack"></i>
                        </button>
                    </div>
                    <div class="h-[20%] m-2">
                        <h2 class="text-xl font-bold mb-2">Intel i7 11<sup>th</sup> GEN</h2>
                        <div class="flex justify-between items-center">
                            <p class="text-gray-700 font-semibold  text-lg">$19.99</p>
                            <button class="bg-blue-600 text-white rounded-lg py-2 px-3 shadow hover:bg-blue-800 focus:outline-none font-semibold text-sm transition duration-200">
                                Add To Cart
                            </button>
                        </div>
                    </div>
                </div>


                <div class="bg-white rounded-lg border border-gray-200 shadow-lg flex flex-col gap-2 h-[464px]">
                    <div class="h-[80%] relative">
                        <img src="../Cart/images/ryzen.jpg" alt="Product Image" class="w-full h-full mb-4 rounded-lg">

                        <!-- Status Banner -->
                        <div class="absolute top-0 left-0 bg-green-700 bg-opacity-95 text-white py-2 px-3 font-bold rounded-tl-lg rounded-br-lg text-md">
                            In Stock
                        </div>

                        <!-- Delete Button (top-right corner) -->
                        <button class="absolute top-2 right-2 bg-gray-500 text-white rounded-full p-2 shadow hover:bg-red-600 focus:outline-none w-8 h-8 flex items-center justify-center transition duration-200">
                            <i class="fa-solid fa-heart-crack"></i>
                        </button>
                    </div>
                    <div class="h-[20%] m-2">
                        <h2 class="text-xl font-bold mb-2">AMD Ryzen 7 5700X3D</h2>
                        <div class="flex justify-between items-center">
                            <p class="text-gray-700 font-semibold  text-lg">$25.00</p>
                            <button class="bg-blue-600 text-white rounded-lg py-2 px-3 shadow hover:bg-blue-800 focus:outline-none font-semibold text-sm transition duration-200">
                                Add To Cart
                            </button>
                        </div>
                    </div>
                </div>
                <div class="bg-white rounded-lg border border-gray-200 shadow-lg flex flex-col gap-2 h-[464px]">
                    <div class="h-[80%] relative">
                        <img src="../Cart/images/rtx5090.jpg" alt="Product Image" class="w-full h-full mb-4 rounded-lg">

                        <!-- Status Banner -->
                        <div class="absolute top-0 left-0 bg-gray-900 bg-opacity-75 text-white py-2 px-3 font-semibold rounded-tl-lg rounded-br-lg text-sm">
                            Out of Stock
                        </div>

                        <!-- Delete Button (top-right corner) -->
                        <button class="absolute top-2 right-2 bg-gray-500 text-white rounded-full p-2 shadow hover:bg-red-600 focus:outline-none w-8 h-8 flex items-center justify-center transition duration-200">
                            <i class="fa-solid fa-heart-crack"></i>
                        </button>
                    </div>
                    <div class="h-[20%] m-2">
                        <h2 class="text-xl font-bold mb-2">RTX 5090</h2>
                        <div class="flex justify-between items-center">
                            <p class="text-gray-700 font-semibold  text-lg">$9.99</p>
                            <button class="bg-blue-600 text-white rounded-lg py-2 px-3 shadow hover:bg-blue-800 focus:outline-none font-semibold text-sm transition duration-200">
                                Add To Cart
                            </button>
                        </div>
                    </div>
                </div>
                <div class="bg-white rounded-lg border border-gray-200 shadow-lg flex flex-col gap-2 h-[464px] ">
                    <div class="h-[80%] relative">
                        <img src="../../Admin/Layout/image/DexProfilePic.jpeg" alt="Product Image" class="w-full h-full mb-4 rounded-lg">

                        <!-- Status Banner -->
                        <div class="absolute top-0 left-0 bg-gray-900 bg-opacity-75 text-white py-2 px-3 font-semibold rounded-tl-lg rounded-br-lg text-sm">
                            Out of Stock
                        </div>

                        <!-- Delete Button (top-right corner) -->
                        <button class="absolute top-2 right-2 bg-gray-500 text-white rounded-full p-2 shadow hover:bg-red-600 focus:outline-none w-8 h-8 flex items-center justify-center transition duration-200">
                            <i class="fa-solid fa-heart-crack"></i>
                        </button>
                    </div>
                    <div class="h-[20%] m-2">
                        <h2 class="text-xl font-bold mb-2">Intel i7 11<sup>th</sup> GEN</h2>
                        <div class="flex justify-between items-center">
                            <p class="text-gray-700 font-semibold  text-lg">$19.99</p>
                            <button class="bg-blue-600 text-white rounded-lg py-2 px-3 shadow hover:bg-blue-800 focus:outline-none font-semibold text-sm transition duration-200">
                                Add To Cart
                            </button>
                        </div>
                    </div>
                </div>

                <!-- Add more product items as needed -->
            </div>--%>
</asp:Content>
