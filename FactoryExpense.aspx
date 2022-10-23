<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" AutoEventWireup="true" CodeBehind="FactoryExpense.aspx.cs" Inherits="Production_Costing_Software.FactoryExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Company Factory Expence</b>
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
                                            <asp:DropDownList ID="drpbpmaster" onchange="drpbpmasterchange(this);" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpbpmaster" runat="server" InitialValue="0" ControlToValidate="drpbpmaster" Display="None" ValidationGroup="g1" ErrorMessage="Select Bulk Product" />
                                        </div>

                                    </div>


                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Bulk Cost/Ltr</span>
                                            </div>
                                            <asp:TextBox ID="txtBulkcost" CssClass="form-control" InitialValue="0" Enabled="false" runat="server"></asp:TextBox>
                                            <span class="input-group-text">MasterPack:
                                                <asp:Label ID="lblmasterpack" class="font-weight-bold" runat="server"></asp:Label></span>
                                            <asp:Label ID="lblFkBulkProductId" class="font-weight-bold" runat="server" Style="display: none"></asp:Label></span>

                                        </div>
                                    </div>


                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Additional Cost on Cost/Liter or KG:</b>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Factory Expence (%)</span>
                                            </div>
                                            <asp:TextBox ID="txtfactexpercent" onchange="calculaterate();" CssClass="form-control" InitialValue="0" runat="server"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rf_txtfactexpercent" runat="server" InitialValue="0" ControlToValidate="txtfactexpercent" Display="None" ValidationGroup="g1" ErrorMessage="Enter Factory Expence (%)" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Factory Expence Amt</span>
                                            </div>
                                            <asp:TextBox ID="txtfactexpAmt" CssClass="form-control" InitialValue="0" Enabled="false" runat="server"></asp:TextBox>

                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Marketted By Charges (%)</span>
                                            </div>
                                            <asp:TextBox ID="txtmrktchrgepercent" onchange="calculaterate();" CssClass="form-control" InitialValue="0" runat="server"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rf_txtmrktchrgepercent" runat="server" InitialValue="0" ControlToValidate="txtmrktchrgepercent" Display="None" ValidationGroup="g1" ErrorMessage="Enter Marketted By Charges (%)" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Marketted By Charges Amt</span>
                                            </div>
                                            <asp:TextBox ID="txtmrktchrgeAmt" CssClass="form-control" InitialValue="0" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Other (%)</span>
                                            </div>
                                            <asp:TextBox ID="txtotherpercent" onchange="calculaterate();" CssClass="form-control" InitialValue="0" runat="server"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rf_txtotherpercent" runat="server" InitialValue="0" ControlToValidate="txtfactexpercent" Display="None" ValidationGroup="g1" ErrorMessage="Enter Factory Expence (%)" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Other Amt</span>
                                            </div>
                                            <asp:TextBox ID="txtotherAmt" CssClass="form-control" InitialValue="0" Enabled="false" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Expence</span>
                                            </div>
                                            <asp:TextBox ID="txttotalexp" CssClass="form-control" Enabled="false" InitialValue="0" runat="server"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Profit (%)</span>
                                            </div>
                                            <asp:TextBox ID="txtprofitpercent" onchange="calculaterate();" CssClass="form-control" InitialValue="0" runat="server"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rf_txtprofitpercent" runat="server"  ControlToValidate="txtprofitpercent" Display="None" ValidationGroup="g1" ErrorMessage="Enter Profit (%)" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Profit Amt</span>
                                            </div>
                                            <asp:TextBox ID="txtprofitAmt" CssClass="form-control" InitialValue="0" Enabled="false" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3" style="color: green; border: double">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Cost/Liter</span>
                                            </div>
                                            <asp:TextBox ID="txttotalcostltr" Enabled="false" CssClass="form-control" InitialValue="0" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:Button ID="btnreport" CssClass="btn btn-secondary float-end" OnClientClick="redirectPage();" runat="server" Text="Report" />

                                        <asp:HiddenField ID="hdnBulkProductId" runat="server" />
                                        <asp:HiddenField ID="hdncwfeid" runat="server" />

                                    </div>

                                </div>
                            </div>
                            <div class="container col-12">
                                <br />
                                <br />
                            </div>
                            <div class="container col-12">
                                <asp:GridView ID="gvcwfe" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BulkProductName" HeaderText="Bulk Product" />
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
                                        <asp:TemplateField HeaderText="Profit Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProfit" runat="server" Text='<%#Eval("ProfitAmt") %>'></asp:Label>
                                                (<%# Eval("ProfitPercentage")%>%)
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TotalCostPerLtr" HeaderText="Final Factory Cost/Ltr" />



                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("FactoryExpenseId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("FactoryExpenseId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btncancel" />
                    </Triggers>
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
                });

            }
        });
        function drpbpmasterchange(obj) {

            var BulkproductId = document.getElementById("<%=drpbpmaster.ClientID%>");
            var txtBulkcost = document.getElementById("<%=txtBulkcost.ClientID%>");
            var lblmasterpack = document.getElementById("<%=lblmasterpack.ClientID%>");
            var lblFkBulkProductId = document.getElementById("<%=lblFkBulkProductId.ClientID%>");
            var hdnBulkProductId = document.getElementById("<%=hdnBulkProductId.ClientID%>");
            

            $.ajax({
                url: 'WebService.asmx/CostPerLtrByPackMaterial',
                data: { BulkproductId: BulkproductId.value },
                method: 'POST',
                success: function (r) {
                    var FinalcostLtr = r.all[0].textContent;

                    txtBulkcost.value = FinalcostLtr;
                }
            });

            $.ajax({
                url: 'WebService.asmx/GetBulkCostPerLtrWithMasterPack',
                data: { BulkproductId: BulkproductId.value },
                method: 'POST',
                success: function (r) {
                    var Unit = r.all[0].textContent;
                    var val1 = Unit.split('~')[0];
                    var val2 = Unit.split('~')[1];
                    document.getElementById("<%=lblmasterpack.ClientID%>").innerHTML = val1;
                    hdnBulkProductId.value = lblFkBulkProductId.innerHTML = val2;

                }
            });
        }


        function calculaterate() {
            var txtBulkcost = document.getElementById("<%=txtBulkcost.ClientID%>");
            var txtfactexpercent = document.getElementById("<%=txtfactexpercent.ClientID%>");
            var txtfactexpAmt = document.getElementById("<%=txtfactexpAmt.ClientID%>");
            var txtmrktchrgepercent = document.getElementById("<%=txtmrktchrgepercent.ClientID%>");
            var txtmrktchrgeAmt = document.getElementById("<%=txtmrktchrgeAmt.ClientID%>");
            var txtotherpercent = document.getElementById("<%=txtotherpercent.ClientID%>");
            var txtotherAmt = document.getElementById("<%=txtotherAmt.ClientID%>");
            var txttotalexp = document.getElementById("<%=txttotalexp.ClientID%>");
            var txtprofitpercent = document.getElementById("<%=txtprofitpercent.ClientID%>");
            var txtprofitAmt = document.getElementById("<%=txtprofitAmt.ClientID%>");
            var txttotalcostltr = document.getElementById("<%=txttotalcostltr.ClientID%>");

            txtBulkcost.value = parseFloat(txtBulkcost.value) || '0.00';
            txtfactexpercent.value = parseFloat(txtfactexpercent.value) || '0.00';
            txtfactexpAmt.value = parseFloat(txtfactexpAmt.value) || '0.00';
            txtmrktchrgepercent.value = parseFloat(txtmrktchrgepercent.value) || '0.00';
            txtmrktchrgeAmt.value = parseFloat(txtmrktchrgeAmt.value) || '0.00';
            txtotherpercent.value = parseFloat(txtotherpercent.value) || '0.00';
            txttotalexp.value = parseFloat(txttotalexp.value) || '0.00';
            txtprofitpercent.value = parseFloat(txtprofitpercent.value) || '0.00';
            txtprofitAmt.value = parseFloat(txtprofitAmt.value) || '0.00';


            txtfactexpAmt.value = Number(parseFloat(txtfactexpercent.value * txtBulkcost.value) / 100).toFixed(2);
            txtmrktchrgeAmt.value = Number(parseFloat(txtmrktchrgepercent.value * txtBulkcost.value) / 100).toFixed(2);
            txtotherAmt.value = Number(parseFloat(txtotherpercent.value * txtBulkcost.value) / 100).toFixed(2);

            txttotalexp.value = Number(parseFloat(txtfactexpAmt.value) + parseFloat(txtmrktchrgeAmt.value) + parseFloat(txtotherAmt.value)).toFixed(2)

            txtprofitAmt.value = Number((parseFloat(txttotalexp.value) + parseFloat(txtBulkcost.value)) * (parseFloat(txtprofitpercent.value) / 100)).toFixed(2);
            txttotalcostltr.value = Number(parseFloat(txtBulkcost.value) + (parseFloat(txtprofitAmt.value) + parseFloat(txttotalexp.value))).toFixed(2);


        }
        function redirectPage() {
            window.location.href = 'ReportFactoryExpence.aspx';
        }
    </script>
</asp:Content>
