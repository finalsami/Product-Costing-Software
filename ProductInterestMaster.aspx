<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductInterestMaster.aspx.cs" Inherits="Production_Costing_Software.ProductInterestMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Product Interest Master</b>
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
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Bulk Cost/Ltr</span>
                                            </div>
                                            <asp:TextBox ID="txtBulkcost" CssClass="form-control" InitialValue="0" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Bulk Interest</span>
                                            </div>
                                            <asp:TextBox ID="txtInterestPercent" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtInterestPercent" runat="server"  ControlToValidate="txtInterestPercent" Display="None" ValidationGroup="g1" ErrorMessage="Enter Interest" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Interest Amount</span>
                                            </div>
                                            <asp:TextBox ID="txtinterestAmt" CssClass="form-control" Enabled="false" TextMode="Number" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Amount</span>
                                            </div>
                                            <asp:TextBox ID="txttotalamt" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="input-group mb-3">
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>


                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdnbpmid" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>
                            </div>
                            <div class="container col-12">
                                <br />
                                <br />
                            </div>
                            <div class="container col-12">
                                <asp:GridView ID="gvbpimaster" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BulkProductName" HeaderText="Bulk Product" />
                                        <asp:BoundField DataField="finalcost" HeaderText="Final Cost/Ltr" />
                                        <asp:BoundField DataField="Interest" HeaderText="Interest (%)" />
                                        <asp:BoundField DataField="InterestAmount" HeaderText="Interest Amount" />
                                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />



                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("BulkProductId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("BulkProductId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
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
                $('#<%= gvbpimaster.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvbpimaster.ClientID %>').find("tr:first"))).DataTable({
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


            $.ajax({
                url: 'WebService.asmx/GetBulkCostPerLtr',
                data: { BulkproductId: BulkproductId.value },
                method: 'POST',
                success: function (r) {
                    var FinalcostLtr = r.all[0].textContent;
                  
                    txtBulkcost.value = FinalcostLtr;
                }
            });

        }
        function calculaterate() {
            var txtBulkcost = document.getElementById("<%=txtBulkcost.ClientID%>");
            var txtInterestPercent = document.getElementById("<%=txtInterestPercent.ClientID%>");
            var txtinterestAmt = document.getElementById("<%=txtinterestAmt.ClientID%>");
            var txttotalamt = document.getElementById("<%=txttotalamt.ClientID%>");


  
            act = 0;
            act = txtinterestAmt.value = (parseFloat(txtBulkcost.value * txtInterestPercent.value) / 100).toFixed(2);
            
            txttotalamt.value = Number(parseFloat(txtBulkcost.value) + parseFloat(act)).toFixed(2)
           
        }
    </script>
</asp:Content>
