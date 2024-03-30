<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="webAssignment.Admin.ErrorPages._404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Noto+Serif+Grantha&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" />
    <title>G-Tech</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="w-screen h-screen">
            <div class="w-full h-full flex-col flex items-center justify-center">

                <p class="text-center text-3xl font-bold"> 404 Page Not Found</p>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Admin/ErrorPages/images/warning.gif" CssClass="w-[350] h-[350] flex items-center justify-center" />


            </div>
        </div>
    </form>
</body>
</html>
