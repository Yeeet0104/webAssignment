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
                            <div class="rating text-yellow-400">
                                <i class="star-icon fa fa-star fa-solid" id="star1" runat="server" data-rating="1" ></i>
                                <i class="star-icon fa fa-star fa-regular" id="star2" runat="server" data-rating="2" ></i>
                                <i class="star-icon fa fa-star fa-regular" id="star3" runat="server" data-rating="3" ></i>
                                <i class="star-icon fa fa-star fa-regular" id="star4" runat="server" data-rating="4" ></i>
                                <i class="star-icon fa fa-star fa-regular" id="star5" runat="server" data-rating="5" ></i>
                            </div>
                            <asp:TextBox ID="rateValue" CssClass="hidden" runat="server">1</asp:TextBox>
                        </div>
                    </div>
                </div>
                <asp:TextBox class="p-2 border border-gray-300 resize-none rounded-lg" TextMode="MultiLine" Rows="4" ID="txtComment" runat="server" placeholder="Please write your comment(s) here"></asp:TextBox>
            </div>
        </div>



        <div class="flex flex-row justify-end w-full mt-2">
            <asp:LinkButton ID="btnSubmitReview" CssClass="py-2 px-6 bg-blue-700 text-white font-semibold rounded-lg" runat="server" OnClick="btnSubmitReview_Click">Submit</asp:LinkButton>
        </div>
    </div>

    <script>
        const starIcons = document.querySelectorAll(".star-icon");
        const emptyStar = document.querySelector(".empty-star");

        starIcons.forEach(starIcon => {
            starIcon.addEventListener("click", function () {
                const rating = this.dataset.rating;  // Get rating value from data-rating
                updateRatingUI(rating);

            });
        });

        function updateRatingUI(selectedRating) {
            document.getElementById("<%= rateValue.ClientID %>").value = selectedRating;

            starIcons.forEach(starIcon => {
                const starRating = starIcon.dataset.rating;
                if (starRating <= selectedRating) {
                    starIcon.classList.replace("fa-regular", "fa-solid"); // Fill star
                } else {
                    starIcon.classList.replace("fa-solid", "fa-regular"); // Empty star
                }
            });
        }


    </script>
</asp:Content>
