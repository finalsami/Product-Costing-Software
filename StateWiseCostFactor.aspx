<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" AutoEventWireup="true" CodeBehind="StateWiseCostFactor.aspx.cs" Inherits="Production_Costing_Software.StateWiseCostFactor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>State Wise Cost Factor</b>
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
                                                <span class="input-group-text">State</span>
                                            </div>
                                            <asp:DropDownList ID="drpstate" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpstate2" runat="server" InitialValue="0" ControlToValidate="drpstate" Display="None" ValidationGroup="g2" ErrorMessage="Select State" />
                                            <asp:RequiredFieldValidator ID="rf_drpstate" runat="server" InitialValue="0" ControlToValidate="drpstate" Display="None" ValidationGroup="g1" ErrorMessage="Select State" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Product Category</span>
                                            </div>
                                            <asp:DropDownList ID="drpproductcat" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpproductcat2" runat="server" InitialValue="0" ControlToValidate="drpproductcat" Display="None" ValidationGroup="g2" ErrorMessage="Select Product Category" />
                                            <asp:RequiredFieldValidator ID="rf_drpproductcat" runat="server" InitialValue="0" ControlToValidate="drpproductcat" Display="None" ValidationGroup="g1" ErrorMessage="Select Product Category" />
                                        </div>
                                    </div>

                                    <div class="card-header col-md-6">
                                        <i class="fas fa-table me-1"></i>
                                        <b>RPL Expence</b>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="card-header col-md-6">
                                            <i class="fas fa-table me-1"></i>
                                            <b>NCR Expence</b>
                                        </div>
                                        <div class="col-12">
                                            <asp:ValidationSummary ID="ValidationSummary2" CssClass="text-danger" runat="server" ValidationGroup="g2" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Staff Expence</span>
                                            </div>
                                            <asp:TextBox ID="txtrplstaff" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtrplstaff" runat="server" ControlToValidate="txtrplstaff" Display="None" ValidationGroup="g1" ErrorMessage="Enter RPL Staff" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Depo Expence</span>
                                            </div>
                                            <asp:TextBox ID="txtrpldepo" CssClass="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtrpldepo" runat="server" ControlToValidate="txtrpldepo" Display="None" ValidationGroup="g1" ErrorMessage="Enter RPL Depo" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Incentive</span>
                                            </div>
                                            <asp:TextBox ID="txtrplIncentive" CssClass="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtrplIncentive" runat="server" ControlToValidate="txtrplIncentive" Display="None" ValidationGroup="g1" ErrorMessage="Enter  RPL Incentive" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Marketing</span>
                                            </div>
                                            <asp:TextBox ID="txtrplmarket" CssClass="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtrplmarket" runat="server" ControlToValidate="txtrplmarket" Display="None" ValidationGroup="g1" ErrorMessage="Enter RPL  Marketing" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Interest</span>
                                            </div>
                                            <asp:TextBox ID="txtrplinterest" CssClass="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtrplinterest" runat="server" ControlToValidate="txtrplinterest" Display="None" ValidationGroup="g1" ErrorMessage="Enter RPL Interest" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Other</span>
                                            </div>
                                            <asp:TextBox ID="txtrplother" CssClass="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtrplother" runat="server" ControlToValidate="txtrplother" Display="None" ValidationGroup="g1" ErrorMessage="Enter RPL Other" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">RPL Total</span>
                                            </div>
                                            <asp:TextBox ID="txtrpltotal" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                        </div>
                                    </div>


                                    <%--NCR--%>


                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Staff Expence</span>
                                            </div>
                                            <asp:TextBox ID="txtncrstaff" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtncrstafff" runat="server" ControlToValidate="txtncrstaff" Display="None" ValidationGroup="g2" ErrorMessage="Enter NCR Staff" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Depo Expence</span>
                                            </div>
                                            <asp:TextBox ID="txtncrdepo" CssClass="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtncrdepo" runat="server" ControlToValidate="txtncrdepo" Display="None" ValidationGroup="g2" ErrorMessage="Enter NCR Depo" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Incentive</span>
                                            </div>
                                            <asp:TextBox ID="txtncrincentive" CssClass="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtncrincentive" runat="server" ControlToValidate="txtncrincentive" Display="None" ValidationGroup="g2" ErrorMessage="Enter NCR Incentive" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Marketing</span>
                                            </div>
                                            <asp:TextBox ID="txtncrmarket" CssClass="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtncrmarket" runat="server" ControlToValidate="txtncrmarket" Display="None" ValidationGroup="g2" ErrorMessage="Enter NCR Market" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Interest</span>
                                            </div>
                                            <asp:TextBox ID="txtncrinterest" CssClass="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtncrinterest" runat="server" ControlToValidate="txtncrinterest" Display="None" ValidationGroup="g2" ErrorMessage="Enter NCR Interest" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Other</span>
                                            </div>
                                            <asp:TextBox ID="txtncrother" CssClass="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                            <asp:RequiredFieldValidator ID="rf_txtncrother" runat="server" ControlToValidate="txtncrother" Display="None" ValidationGroup="g2" ErrorMessage="Enter NCR Other" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">NCR Total</span>
                                            </div>
                                            <asp:TextBox ID="txtncrtotal" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            <span class="input-group-text">%</span>
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnncrupdate" class="btn btn-success" Visible="false" OnClick="btnncrupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnncradd" CssClass="btn btn-primary" OnClick="btnncradd_Click" ValidationGroup="g2" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btnncrcancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btnncrcancel_Click" />
                                    </div>
                                </div>

                                <%--hidden data--%>
                                <asp:HiddenField ID="hdnswcf" runat="server" />
                                <%-------%>
                            </div>
                        </div>
                        <div class="container col-12">
                            <br />
                            <br />
                        </div>
                        <div class="container col-12">
                            <asp:GridView ID="gvswcf" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StateName" HeaderText="StateName" />
                                    <asp:BoundField DataField="ProductCategoryName" HeaderText="ProductCategoryName" />
                                    <asp:BoundField DataField="PriceType" HeaderText="PriceType " />
                                    <asp:BoundField DataField="StaffExpense" HeaderText="StaffExpense " />
                                    <asp:BoundField DataField="DepoExpence" HeaderText="DepoExpence " />
                                    <asp:BoundField DataField="Incentive" HeaderText="Incentive " />
                                    <asp:BoundField DataField="Marketing" HeaderText="Marketing " />
                                    <asp:BoundField DataField="Interest" HeaderText="Interest " />
                                    <asp:BoundField DataField="Other" HeaderText="Other " />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("StateWiseCostFactorId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                            <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("StateWiseCostFactorId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

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
                $('#<%= gvswcf.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvswcf.ClientID %>').find("tr:first"))).DataTable({
                        "destroy": true,
                        "paging": true,
                        "lengthChange": true,
                        "searching": true,
                        "ordering": true,
                        "info": false,
                        "autoWidth": false,
                        //"responsive": true,
                    });

                }
            });

        function calculaterate() {

            //RPL-----------
            var txtrplstaff = document.getElementById("<%=txtrplstaff.ClientID%>");
                var txtrpldepo = document.getElementById("<%=txtrpldepo.ClientID%>");
                var txtrplIncentive = document.getElementById("<%=txtrplIncentive.ClientID%>");
                var txtrplinterest = document.getElementById("<%=txtrplinterest.ClientID%>");
                var txtrplmarket = document.getElementById("<%=txtrplmarket.ClientID%>");
                var txtrplother = document.getElementById("<%=txtrplother.ClientID%>");
                var txtrpltotal = document.getElementById("<%=txtrpltotal.ClientID%>");


                txtrplstaff.value = parseFloat(txtrplstaff.value) || '0';
                txtrpldepo.value = parseFloat(txtrpldepo.value) || '0';
                txtrplIncentive.value = parseFloat(txtrplIncentive.value) || '0';
                txtrplinterest.value = parseFloat(txtrplinterest.value) || '0';
                txtrplmarket.value = parseFloat(txtrplmarket.value) || '0';
                txtrplother.value = parseFloat(txtrplother.value) || '0';
                txtrpltotal.value = parseFloat(txtrpltotal.value) || '0';


                txtrpltotal.value = Number(parseFloat(txtrplstaff.value) + parseFloat(txtrpldepo.value) + parseFloat(txtrplIncentive.value) + parseFloat(txtrplinterest.value) + parseFloat(txtrplmarket.value) + parseFloat(txtrplother.value)).toFixed(2);

                //----NCR-------

                var txtncrstaff = document.getElementById("<%=txtncrstaff.ClientID%>");
                var txtncrdepo = document.getElementById("<%=txtncrdepo.ClientID%>");
                var txtncrincentive = document.getElementById("<%=txtncrincentive.ClientID%>");
                var txtncrinterest = document.getElementById("<%=txtncrinterest.ClientID%>");
                var txtncrmarket = document.getElementById("<%=txtncrmarket.ClientID%>");
                var txtncrother = document.getElementById("<%=txtncrother.ClientID%>");
                var txtncrtotal = document.getElementById("<%=txtncrtotal.ClientID%>");


            txtncrstaff.value = parseFloat(txtncrstaff.value) || '0';
            txtncrdepo.value = parseFloat(txtncrdepo.value) || '0';
            txtncrincentive.value = parseFloat(txtncrincentive.value) || '0';
            txtncrinterest.value = parseFloat(txtncrinterest.value) || '0';
            txtncrmarket.value = parseFloat(txtncrmarket.value) || '0';
            txtncrother.value = parseFloat(txtncrother.value) || '0';
            txtncrtotal.value = parseFloat(txtncrtotal.value) || '0';

            txtncrtotal.value = Number(parseFloat(txtncrstaff.value) + parseFloat(txtncrdepo.value) + parseFloat(txtncrincentive.value) + parseFloat(txtncrinterest.value) + parseFloat(txtncrmarket.value) + parseFloat(txtncrother.value)).toFixed(2);


        }
    </script>
</asp:Content>
