<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" AutoEventWireup="true" CodeBehind="SharedBulkCostReport.aspx.cs" Inherits="Production_Costing_Software.SharedBulkCostReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="contain">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Shared Report</b>
                </div>
                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>--%>
                <div class="card-body">
                    <div class="container col-12 grds">
                        <div class="col-sm-12">
                            <div id="dvdetailcontentOnlyTable" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
