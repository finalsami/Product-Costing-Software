<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" AutoEventWireup="true" CodeBehind="ReportFactoryExpence.aspx.cs" Inherits="Production_Costing_Software.Report_FectoryExpenceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Report Factory Expence</b>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="card-body">
                            <div class="container col-12 ">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="text-danger" runat="server" ValidationGroup="g1" />
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Select</span>
                                            </div>
                                            <asp:DropDownList ID="drpmasterpack" CssClass="form-control" runat="server">
                                                <asp:ListItem Selected="True" Text="Masterpack" Value="1"></asp:ListItem>
                                                <%--<asp:ListItem Text="All" Value="0"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                          <%--          <div class="col-md-6">
                                        <div class="input-group mb-3" style="visibility:hidden">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Bulk Product</span>
                                            </div>
                                            <asp:ListBox ID="lstbulkproduct" runat="server" SelectionMode="Multiple" Style="height: 250px" CssClass="form-control"></asp:ListBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-6"  style="visibility:hidden">
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary float-end" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                    </div>--%>
                                </div>
                            </div>
              <%--              <div class="container col-12">
                                <br />
                                <br />
                            </div>--%>
                            <div class="container col-12">
                                <asp:GridView ID="gvcwfe" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BulkProductName" HeaderText="Bulk Product" />
                                        <asp:BoundField DataField="MasterPack" HeaderText="Pack size" />
                                        <asp:BoundField DataField="BulkcostUnit" HeaderText="BulkCost/Unit" />
                                        <asp:BoundField DataField="TotalPMCostUnit" HeaderText="Total PM Cost/Unit" />
                                        <asp:BoundField DataField="LabourCostPerUnit" HeaderText="LabourCost/Unit" />
                                        <asp:BoundField DataField="PackLossAmt" HeaderText="PackLossAmt" />
                                        <asp:BoundField DataField="TotalCostPerUnit" HeaderText="TotalCost/Unit" />

                                        <asp:BoundField DataField="CostPerLtr" HeaderText="Cost/Ltr" />
                                        <asp:TemplateField HeaderText="Factory Expence Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfactexp" runat="server" Text='<%#Eval("FactoryExpenseAmt") %>'></asp:Label>
                                                (<%# Eval("FactoryExpensePercentage")%>%)
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Marketed Charge Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMarketedCharge" runat="server" Text='<%#Eval("MarketedChargeAmt") %>'></asp:Label>
                                                (<%# Eval("MarketedChargePercentage")%>%)
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Other Expence Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOther" runat="server" Text='<%#Eval("OtherAmt") %>'></asp:Label>
                                                (<%# Eval("OtherPercentage")%>%)
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TotalExpence" HeaderText="TotalExpence" />

                                        <asp:TemplateField HeaderText="Profit Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProfit" runat="server" Text='<%#Eval("ProfitAmt") %>'></asp:Label>
                                                (<%# Eval("ProfitPercentage")%>%)
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TotalCostPerLtr" HeaderText="Final Factory Cost/Ltr" />




                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </ContentTemplate>
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
                        bindDataTable();

                    }
                });
            };
          
            bindDataTable();
            function bindDataTable() {
                $('#<%= gvcwfe.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvcwfe.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": false,
                    "autoWidth": false,
                    "responsive": true,
                    dom: 'Bfrtip',
                    buttons: [
                        'excel', 'print',
                       {
                           extend: 'pdfHtml5',
                           orientation: 'landscape',
                           pageSize: 'LEGAL'
                        }
                    ],
                  
         

                });

            }
        });

    </script>
</asp:Content>
