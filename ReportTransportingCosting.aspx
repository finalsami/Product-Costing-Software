<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" AutoEventWireup="true" CodeBehind="ReportTransportingCosting.aspx.cs" Inherits="Production_Costing_Software.ReportTransportingCosting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Report Factory Expence</b>
                    <asp:Label ID="lblStateName" runat="server" ></asp:Label>
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
                                                <span class="input-group-text">Bulk Product</span>
                                            </div>
                                            <asp:DropDownList ID="drpismasterpack"  CssClass="form-control" runat="server">
                                                <asp:ListItem Selected="True" Text="Masterpack" Value="1"></asp:ListItem>
                                                <asp:ListItem  Text="All" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <asp:HiddenField ID="hdnIsmasterpack" runat="server" />
                                    <div class="container col-12">
                                        <br />
                                        <br />
                                    </div>
                                    <div class="container col-12">
                                        <asp:GridView ID="gvreport" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BPM_Product_Name" HeaderText="Bulk Product Name" />
                                                <asp:BoundField DataField="PackingUnitSize" HeaderText="Packing Size"  />
                                                <asp:BoundField DataField="ShipperSize" HeaderText="Box Size Liter or KG"  />
                                                <asp:BoundField DataField="Approx1CartageCharge" HeaderText="Factory To Godown Transport Cost" />

                                                <asp:BoundField DataField="LocalAmt" HeaderText="Godown to Transport Cost"  />
                                                <asp:BoundField DataField="UnloadAmt" HeaderText="Unloading Charges"  />
                                                <asp:BoundField DataField="AverageAmt" HeaderText="Local Cartage To Party Cost"  />
                                                <asp:BoundField DataField="Total" HeaderText="Total"  />
                                                <asp:BoundField DataField="CostPerLtrKG" HeaderText="Cost/Liter or KG"  />


                                            </Columns>
                                        </asp:GridView>

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
                $('#<%= gvreport.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvreport.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": false,
                    "autoWidth": false,
                    "responsive": true,
                });

            }
        });
       <%-- function ismasterpack() {
            var drpismasterpack = document.getElementById("<%=drpismasterpack.ClientID%>").value;
            var drpismasterpack = document.getElementById("<%=drpismasterpack.ClientID%>").value;

            drpismasterpack.value = parseFloat(drpismasterpack.value) || '0';

            alert(drpismasterpack);

            if (drpismasterpack == '1') {
                hdnIsmasterpack.Value = 1;
            }
            else {
                hdnIsmasterpack.Value = 0;
            }
        }--%>
    </script>
</asp:Content>
