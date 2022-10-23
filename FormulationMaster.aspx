<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormulationMaster.aspx.cs" Inherits="Production_Costing_Software.FormulationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Formulation Master</b>
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
                                                <span class="input-group-text">Formulation Name</span>
                                            </div>
                                            <asp:TextBox ID="txtfmname" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtfmname" runat="server" InitialValue="0" ControlToValidate="txtfmname" Display="None" ValidationGroup="g1" ErrorMessage="Enter Formulation Name" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Batch Size</span>
                                            </div>
                                            <asp:TextBox ID="txtbatchsize" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtbatchsize" runat="server" InitialValue="0" ControlToValidate="txtbatchsize" Display="None" ValidationGroup="g1" ErrorMessage="Enter Batch Size" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Measurement</span>
                                            </div>
                                            <asp:DropDownList ID="drpunit" runat="server" class="form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpunit" runat="server" InitialValue="0" ControlToValidate="drpunit" Display="None" ValidationGroup="g1" ErrorMessage="Select Measurement Unit" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">No of Labours</span>
                                            </div>
                                            <asp:TextBox ID="txtlabours" onchange="calculaterate();" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtlabours" runat="server" InitialValue="0" ControlToValidate="txtlabours" Display="None" ValidationGroup="g1" ErrorMessage="Enter Enter Labours" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Supervisors</span>
                                            </div>
                                            <asp:TextBox ID="txtsupervisors" onchange="calculaterate();" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtsupervisors" runat="server" InitialValue="0" ControlToValidate="txtsupervisors" Display="None" ValidationGroup="g1" ErrorMessage="Enter Supervisors" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Labour Charge</span>
                                            </div>
                                            <asp:TextBox ID="txtlabourcharge" CssClass="form-control" Enabled="false" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Labour Cost</span>
                                            </div>
                                            <asp:TextBox ID="txtlabourcost" CssClass="form-control" Enabled="false" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Power (Unit)</span>
                                            </div>
                                            <asp:TextBox ID="txtpowerunit" onchange="calculaterate();" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpowerunit" runat="server" InitialValue="0" ControlToValidate="txtpowerunit" Display="None" ValidationGroup="g1" ErrorMessage="Enter Power" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">PowerCharge (Unit)</span>
                                            </div>
                                            <asp:TextBox ID="txtpowercharge" Enabled="false" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Power Cost (Unit)</span>
                                            </div>
                                            <asp:TextBox ID="txttotalpower" Enabled="false" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Maintenance</span>
                                            </div>
                                            <asp:TextBox ID="txtmaintenence" onchange="calculaterate();" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtmaintenence" runat="server" InitialValue="0" ControlToValidate="txtmaintenence" Display="None" ValidationGroup="g1" ErrorMessage="Enter Maintenence" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Other Cost</span>
                                            </div>
                                            <asp:TextBox ID="txtothercost" onchange="calculaterate();" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtothercost" runat="server" InitialValue="0" ControlToValidate="txtothercost" Display="None" ValidationGroup="g1" ErrorMessage="Enter Other Cost" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Cost</span>
                                            </div>
                                            <asp:TextBox ID="txttotalcost" Enabled="false" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">CostPerLiter/BatchSize</span>
                                            </div>
                                            <asp:TextBox ID="txtCostLtrBatchsize" Enabled="false" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Add Buffer</span>
                                            </div>
                                            <asp:TextBox ID="txtbuffer" onchange="calculaterate();" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtbuffer" runat="server" InitialValue="0" ControlToValidate="txtbuffer" Display="None" ValidationGroup="g1" ErrorMessage="Enter Buffer" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Final Cost/Ltr BatchSize</span>
                                            </div>
                                            <asp:TextBox ID="txtfinalcostltrbatchsize" Enabled="false" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdntxtsvc" runat="server" />
                                        <asp:HiddenField ID="hdnfmid" runat="server" />
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
                                <asp:GridView ID="gvformulationmaster" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FormulationName" HeaderText="FormulationName" />
                                        <asp:BoundField DataField="BatchSize" HeaderText="BatchSize" />
                                        <asp:BoundField DataField="Labours" HeaderText="Labours" />
                                        <asp:BoundField DataField="Supervisors" HeaderText="Supervisors" />
                                        <asp:BoundField DataField="labourcharge" HeaderText="LabourCharge" />
                                        <asp:BoundField DataField="totallabourcharge" HeaderText="Total Labour Cost" />
                                        <asp:BoundField DataField="PowerUnits" HeaderText="PowerUnits" />
                                        <asp:BoundField DataField="MaintenanceCost" HeaderText="Maintenance" />
                                        <asp:BoundField DataField="OtherCost" HeaderText="Other Cost" />
                                        <asp:BoundField DataField="totalcost" HeaderText="Total Cost" />
                                        <asp:BoundField DataField="Unit" HeaderText="Measurement" />
                                        <asp:BoundField DataField="costPerLtr" HeaderText="Cost/Ltr" />
                                        <asp:BoundField DataField="AdditionalBuffer" HeaderText="Add Buffer" />
                                        <asp:BoundField DataField="FinalcostLtr" HeaderText="Final Cost /Ltr" />


                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("FormulationId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("FormulationId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
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
                $('#<%= gvformulationmaster.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvformulationmaster.ClientID %>').find("tr:first"))).DataTable({
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


            var txtlabours = document.getElementById('<%=txtlabours.ClientID %>');
            var txtlabourcost = document.getElementById('<%=txtlabourcost.ClientID %>');
            var txtsupervisors = document.getElementById('<%=txtsupervisors.ClientID %>');
            var supervisorcharge = document.getElementById('<%=hdntxtsvc.ClientID %>');
            var txtlabourcharge = document.getElementById('<%=txtlabourcharge.ClientID %>');
            var txtpowerunit = document.getElementById('<%=txtpowerunit.ClientID %>');
            var txttotalpower = document.getElementById('<%=txttotalpower.ClientID %>');
            var txtpowercharge = document.getElementById('<%=txtpowercharge.ClientID %>');
            var txttotalcost = document.getElementById('<%=txttotalcost.ClientID %>');
            var txtmaintenence = document.getElementById('<%=txtmaintenence.ClientID %>');
            var txtothercost = document.getElementById('<%=txtothercost.ClientID %>');
            var txtbuffer = document.getElementById('<%=txtbuffer.ClientID %>');
            var txtCostLtrBatchsize = document.getElementById('<%=txtCostLtrBatchsize.ClientID %>');
            var txtfinalcostltrbatchsize = document.getElementById('<%=txtfinalcostltrbatchsize.ClientID %>');
            var txtbatchsize = document.getElementById('<%=txtbatchsize.ClientID %>');


            txtlabours.value = parseFloat(txtlabours.value) || '0.00';
            txtsupervisors.value = parseFloat(txtsupervisors.value) || '0.00';
            txttotalpower.value = parseFloat(txttotalpower.value) || '0.00';
            txttotalcost.value = parseFloat(txttotalcost.value) || '0.00';
            txtpowerunit.value = parseFloat(txtpowerunit.value) || '0.00';
            txttotalpower.value = parseFloat(txttotalpower.value) || '0.00';
            txtmaintenence.value = parseFloat(txtmaintenence.value) || '0.00';
            txtothercost.value = parseFloat(txtothercost.value) || '0.00';
            txtbuffer.value = parseFloat(txtbuffer.value) || '0.00';
            txtCostLtrBatchsize.value = parseFloat(txtCostLtrBatchsize.value) || '0.00';
            txtfinalcostltrbatchsize.value = parseFloat(txtfinalcostltrbatchsize.value) || '0.00';
            txtbatchsize.value = parseFloat(txtbatchsize.value) || '0.00';


            var act = 0;

            act = parseFloat(txtlabours.value * txtlabourcost.value) + parseFloat(txtsupervisors.value * supervisorcharge.value);
            txtlabourcharge.value = Number(act).toFixed(2);

            txttotalpower.value = Number((parseFloat(txtpowerunit.value) * (parseFloat(txtpowercharge.value)))).toFixed(2);
            txttotalcost.value = Number(act + parseFloat(txttotalpower.value) + parseFloat(txtothercost.value) + parseFloat(txtmaintenence.value)).toFixed(2);
            txtCostLtrBatchsize.value = Number(parseFloat(txttotalcost.value) + parseFloat(txtbuffer.value)) / parseFloat(txtbatchsize.value).toFixed(2);
            txtfinalcostltrbatchsize.value = Number(parseFloat(txtCostLtrBatchsize.value) + parseFloat(txtbuffer.value)).toFixed(2);
        }


    </script>
</asp:Content>
