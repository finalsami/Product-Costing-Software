<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PriceListGPActual.aspx.cs" Inherits="Production_Costing_Software.PriceListGPActual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        var strURL = "";
        strURL = document.URL.substring(22, document.URL.length - document.URL.indexOf("PriceListGPActual.aspx"));
        //alert(document.URL.length, 'document lenght')
        //alert(document.URL.indexOf("PriceListGPActual.aspx"),'URL index')
        //alert(strURL)

        strURL += "/PriceListGP.aspx?CmpId=1&EstimateId=" + 0;

        window.location.href = strURL;
    </script>
</asp:Content>
