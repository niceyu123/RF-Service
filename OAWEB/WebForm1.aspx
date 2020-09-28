<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="OAWEB.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <script src="./js/jquery-1.10.1.min.js" type="text/javascript"></script>

</head>
<body>
        <iframe id="ifrm" src="" width="0" height="0"></iframe>
    <script type="text/javascript">
        var strPrintData = 'aHR0cDovL2xvY2FsaG9zdDozMTE4L1ByaW50VGVtcC9QcmludFRlbXAyMDIwMDkxMDExMTEzMjQ0NjY4Ni50eHQ=';
            var aa = strPrintData;
            $("#ifrm")[0].src = "ChuLinPrint:aHR0cDovL2xvY2FsaG9zdDozMTE4L1ByaW50VGVtcC9QcmludFRlbXAyMDIwMDkxMDExMTEzMjQ0NjY4Ni50eHQ=";
       //}      // if (strPrintData != '') { 
 
<%--        var strPrintData = '<%=strPrintData%>';
        if (strPrintData != '') {
            var aa = strPrintData;
            $("#ifrm")[0].src = "ChuLinPrint:<%=strPrintData%>";
        }--%>
    </script>
    <form id="form1" runat="server">
        <div>
            <table style="width: 71%; height: 200px;">
            <tr>
                <td class="style2">
     
        <asp:Button ID="BtnDepositPreview" runat="server" 
            onclick="BtnDepositPreview_Click" Text="票据预览" Width="87px" />
                </td>
                <td class="style1">
     
                    &nbsp;</td>
                <td class="style1">
     
                    &nbsp;</td>
            </tr>
                </table> 
        </div>
    </form>
</body>
</html>
