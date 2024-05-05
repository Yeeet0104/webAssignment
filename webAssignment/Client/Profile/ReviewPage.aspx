<%@ Page Title="" Language="C#" MasterPageFile="~/Client/ProfileMaster/ProfileMasterPage.master" AutoEventWireup="true" CodeBehind="ReviewPage.aspx.cs" Inherits="webAssignment.Client.Profile.ReviewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="min-h-[80vh] p-2 flex flex-col gap-3 w-full">
        <div class=" p-2 flex justify-between items-center">
            <div class="flex gap-4 items-center text-lg text-gray-500 font-bold">
                <span class="">Order History</span>
                <i class="fa-solid fa-caret-right pt-1 pl-1"></i>
                <span class="">Order Details</span>
                <i class="fa-solid fa-caret-right pt-1 pl-1"></i>
                <span class="text-xl text-gray-900 font-bold">Rate</span>
            </div>
        </div>


        <div class="border border-gray-300 bg-white shadow shadow-xl rounded-2xl  p-4 flex gap-2">

            <!--Order Details-->
            <div class="w-full flex flex-col gap-3 p-2">
                <div class="flex gap-2">

                    <div>
                        <asp:Image ID="imgProd" CssClass="rounded-full w-16 h-auto drop-shadow-lg" runat="server" ImageUrl="~/Client/Cart/images/cryingKermit.png" />
                    </div>
                    <div class="flex flex-col gap-2">
                        <asp:Label ID="lbName" runat="server" Text="Crying Kermit" CssClass="font-bold"></asp:Label>
                        <div class="flex gap-3 items-center">
                            <span class="font-semibold">Rating: </span>
                            <asp:DropDownList ID="ddlRate" runat="server" CssClass="border border-gray-300">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                            <span class="text-yellow-400">
                                        <i class=" fa-solid fa-star star-icon cursor-pointer" data-rating="1"></i>
                                    </span>
                        </div>
                    </div>
                </div>
                <asp:TextBox class="p-2 border border-gray-300 resize-none rounded-lg" TextMode="MultiLine" Rows="4" ID="txtComment" runat="server" placeholder="Please write your comment(s) here"></asp:TextBox>
            </div>
        </div>

        <%--<asp:ListView ID="lvProductList" runat="server" OnItemCommand="orderListView_ItemCommand">
            <LayoutTemplate>
                <div id="itemPlaceholder" runat="server"></div>
            </LayoutTemplate>
            <ItemTemplate>
                <div class="border border-gray-300 bg-white shadow shadow-xl rounded-2xl  p-4 flex gap-2">

                    <!--image upload-->
                    <div class="w-[20%] flex justify-center items-center p-2">
                        <div class=" flex flex-col">
                            <div class="flex justify-center items-center">
                                <asp:Image ID="imgReview" runat="server" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();" ImageUrl="~/Client/Checkout/image/img_placeholder.jpg" class="border border-gray-300 rounded-2xl shadow shadow-xl" Style="height: 120px; width: 120px;" />
                            </div>
                            <div class="flex justify-center pt-6">
                                <asp:FileUpload ID="fileUpload" runat="server" Style="display: none; cursor: pointer;" onchange="previewImage(this);" />

                                <button type="button" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();" class="bg-blue-500 text-white w-full py-1 rounded-lg cursor-pointer hover:bg-blue-600">
                                    Choose File
                                </button>
                            </div>
                        </div>
                    </div>
                    <!--Order Details-->
                    <div class="w-[80%] flex flex-col gap-3 p-2">
                        <div class="flex gap-2">

                            <div>
                                <asp:Image ID="imgProd" CssClass="rounded-full w-16 h-auto drop-shadow-lg" runat="server" ImageUrl='<%# Eval("imagePath") %>' />
                            </div>
                            <div class="flex flex-col gap-2">
                                <h2 class="text-lg font-bold"><%# Eval("productName") %> <%# Eval("variantName") %> </h2>
                                <div class="flex gap-3 items-center">
                                    <span class="font-semibold">Rating: </span>
                                    <asp:DropDownList ID="ddlRate" runat="server">
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="text-yellow-400">
                                        <i class=" fa-solid fa-star star-icon cursor-pointer" data-rating="1"></i>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <asp:TextBox class="p-2 border border-gray-300 resize-none rounded-lg" TextMode="MultiLine" Rows="4" ID="txtComment" runat="server" placeholder="Please write your comment(s) here"></asp:TextBox>
                        <div class="flex flex-row justify-end w-full mt-2">

                            <asp:LinkButton ID="btnSubmitReview" runat="server" CommandArgument='<%# Eval("variantID") %>' CommandName="submitClick" CssClass="py-2 px-6 bg-blue-700 text-white font-semibold rounded-lg"><span>Submit</span></asp:LinkButton>

                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>--%>


        <div class="flex flex-row justify-end w-full mt-2">
            <asp:LinkButton ID="btnSubmitReview" CssClass="py-2 px-6 bg-blue-700 text-white font-semibold rounded-lg" runat="server" OnClick="btnSubmitReview_Click">Submit</asp:LinkButton>
        </div>
    </div>

   <%-- <script>
        const starIcons = document.querySelectorAll(".star-icon");
        const emptyStar = document.querySelector(".empty-star");

        starIcons.forEach(starIcon => {
            starIcon.addEventListener("click", function () {
                const rating = this.dataset.rating;  // Get rating value from data-rating
                updateRatingUI(rating);
            });
        });

        function updateRatingUI(selectedRating) {
            starIcons.forEach(starIcon => {
                const starRating = starIcon.dataset.rating;
                if (starRating <= selectedRating) {
                    starIcon.classList.replace("fa-regular", "fa-solid"); // Fill star
                } else {
                    starIcon.classList.replace("fa-solid", "fa-regular"); // Empty star
                }
            });
        }
    </script>--%>
</asp:Content>
