<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectKQList.aspx.cs" Inherits="OAWEB.SelectKQList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录界面|- 51aspx.com</title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Panel ID="Panel1" runat="server" Height="13px" Width="359px" style="font-weight: bold; font-size: small">
            <table style="width: 372px">
                <tr>
                    <td style="width: 81px">
                        <asp:Label ID="Label1" runat="server" Text="用户名"></asp:Label></td>
                    <td style="width: 146px">
                        <asp:TextBox ID="TextBox1" runat="server" Height="18px"></asp:TextBox></td>
                    <td style="width: 116px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                            ErrorMessage="用户名不能为空" ForeColor="DarkGray" Width="126px"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 81px; height: 28px;">
                        <asp:Label ID="Label2" runat="server" Text="密    码"></asp:Label></td>
                    <td style="width: 146px; height: 28px;">
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" Width="149px"></asp:TextBox></td>
                    <td style="width: 116px; height: 28px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                            ErrorMessage="密码不能为空" ForeColor="DarkGray"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 81px">
                    </td>
                    <td style="width: 146px">
                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="False" ForeColor="Black"
                            OnClick="LinkButton1_Click">登 录</asp:LinkButton>
                                     
                        <asp:LinkButton ID="LinkButton2" runat="server" Font-Underline="False" ForeColor="Black"
                            OnClick="LinkButton2_Click">重  置</asp:LinkButton></td>
                    <td style="width: 116px">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
