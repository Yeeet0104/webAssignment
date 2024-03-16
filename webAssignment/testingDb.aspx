<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testingDb.aspx.cs" Inherits="webAssignment.testingDb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [User]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
