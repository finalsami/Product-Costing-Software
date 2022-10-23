<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" AutoEventWireup="true" CodeBehind="PriceListReportGP.aspx.cs" Inherits="Production_Costing_Software.PriceListReportGP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Price List GP Report</b>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="card-body">

                            <div class="container col-12">
                                <br />
                                <br />
                            </div>
                            <div class="container col-12">
                                <asp:GridView ID="gvpricelistgp" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PriceListName" HeaderText="PriceList" />
                                        <asp:BoundField DataField="AsOnDate" HeaderText="AsOnDate" />
                                        <asp:BoundField DataField="StateName" HeaderText="StateName " />
                                        <asp:BoundField DataField="CompanyName" HeaderText="CompanyName " />
                                        <%--<asp:BoundField DataField="MergeList" HeaderText="MergeList " Visible="false" />--%>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMergeList" runat="server" Text='<%#  Eval("MergeList") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Report">
                                            <ItemTemplate>
                                                <asp:Button ID="ReportId" Text="Report" CommandArgument='<%#  Eval("MergeList") %>' class="btn btn-success btn-sm align-content-end" OnClick="btnReport_Click" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField runat="server" ID="hdnMergeList" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <%--         <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btncancel" />
                    </Triggers>--%>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        $(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {

                    if (sender._postBackSettings.panelsToUpdate != null) {
                        var nm = sender._postBackSettings.asyncTarget;

                        bindDataTable();
                    }
                });
            };

            bindDataTable();
            function bindDataTable() {
                $('#<%= gvpricelistgp.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvpricelistgp.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": false,
                    "autoWidth": true,
                    "responsive": true,
                    fixedHeader: {
                        header: true,
                        footer: false
                    },
                    "scrollX": false,
                    "pageLength": 50
                });

            }
        });

    </script>
</asp:Content>
