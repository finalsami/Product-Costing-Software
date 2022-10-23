<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" AutoEventWireup="true" CodeBehind="TrasportationCostMaster.aspx.cs" Inherits="Production_Costing_Software.TrasportationCostMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Trasportation Cost Master </b>
                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                            <asp:RequiredFieldValidator ID="rf_drpstate" runat="server" InitialValue="0" ControlToValidate="drpstate" Display="None" ValidationGroup="g1" ErrorMessage="Select State" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Truck Load Chrage</span>
                                            </div>
                                            <asp:TextBox ID="txttruckload" class="form-control" onchange="calculaterate();" type="text" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txttruckload" runat="server" ControlToValidate="txttruckload" Display="None" ValidationGroup="g1" ErrorMessage="Enter Truck Load" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Approx No of Carton in 1 lot</span>
                                            </div>
                                            <asp:TextBox ID="txtNoCarton" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtNoCarton" runat="server" ControlToValidate="txtNoCarton" Display="None" ValidationGroup="g1" ErrorMessage="Enter NoCarton" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Approx 1 carton Charge</span>
                                            </div>
                                            <asp:TextBox ID="txt1cartoncharge" CssClass="form-control" type="text" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdntcmid" runat="server" />
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
                                <asp:GridView ID="gvtcm" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="StateName" HeaderText="State" />
                                        <asp:BoundField DataField="TruckLoadCharges" HeaderText="Truck Load Charges" />

                                        <asp:BoundField DataField="NoCarton" HeaderText="Approx No of Carton in 1 lot" />
                                        <asp:BoundField DataField="CartonChrage" HeaderText="Approx 1 carton Charge" />

                                        <asp:TemplateField HeaderText="Average Local Transportation">
                                            <ItemTemplate>
                                                <asp:Button ID="btnaveragepopup" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CommandArgument='<%#  Eval("TransportationCostId") %>' OnClick="btnaveragepopup_Click" CausesValidation="false" Text="Add Range" runat="server" class="btn btn-success btn-sm" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unloading Charge">
                                            <ItemTemplate>
                                                <asp:Button ID="btnunloadingpopup" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CommandArgument='<%#  Eval("TransportationCostId") %>' OnClick="btnunloadingpopup_Click" CausesValidation="false" Text="Add Range" runat="server" class="btn btn-success btn-sm" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Local Cartage">
                                            <ItemTemplate>
                                                <asp:Button ID="btnlocalchargeopup" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CommandArgument='<%#  Eval("TransportationCostId") %>' OnClick="btnlocalchargeopup_Click" CausesValidation="false" Text="Add range" runat="server" data-bs-toggle="modal" data-bs-target="#LocalCartageModal" class="btn btn-success btn-sm" />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Report">
                                            <ItemTemplate>
                                                <asp:Button ID="TransportReport" CommandArgument='<%#  Eval("TransportationCostId") %>' OnClick="TransportReport_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4);" Text="Report" runat="server" class="btn btn-warning btn-sm" />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("TransportationCostId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("TransportationCostId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>

                        </div>
                        <%--AverageLocalmodal--%>
                        <div class="modal fade" id="AverageLocalmodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-xl" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">AverageTransport :
                                [<asp:Label ID="lblstatename" runat="server"></asp:Label>]
                                            <asp:HiddenField runat="server" ID="hdnAverageType" />
                                            <asp:HiddenField runat="server" ID="hdnAveragechargeId" />

                                        </h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-12">
                                                <asp:ValidationSummary ID="ValidationSummary2" CssClass="text-danger" runat="server" ValidationGroup="g2" />
                                            </div>
                                            <div class="col-md-12">
                                                <div class="input-group mb-3">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Start</span>
                                                        <asp:TextBox ID="txtaveragestart" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtaveragestart" runat="server" InitialValue="0" ControlToValidate="txtaveragestart" Display="None" ValidationGroup="g2" ErrorMessage="Enter Start" />
                                                    </div>

                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">End</span>
                                                        <asp:TextBox ID="txtaverageend" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtaverageend" runat="server" InitialValue="0" ControlToValidate="txtaverageend" Display="None" ValidationGroup="g2" ErrorMessage="Enter End" />
                                                    </div>

                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Measurement</span>
                                                        <asp:DropDownList ID="drpunit" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rf_drpunit" runat="server" InitialValue="0" ControlToValidate="drpunit" Display="None" ValidationGroup="g2" ErrorMessage="Select Measurement" />
                                                    </div>

                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Amount</span>
                                                        <asp:TextBox ID="txtaverageAmt" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtaverageAmt" runat="server" InitialValue="0" ControlToValidate="txtaverageAmt" Display="None" ValidationGroup="g2" ErrorMessage="Enter Amount" />
                                                    </div>


                                                </div>
                                            </div>
                                        </div>

                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                            <asp:Button ID="btnaddaverage" CssClass="btn btn-primary" OnClick="btnaddaverage_Click" ValidationGroup="g2" CausesValidation="true" runat="server" Text="Add" />
                                        </div>

                                        <div class="container col-12">
                                            <br />
                                            <br />
                                        </div>
                                        <div class="container col-12">
                                            <asp:GridView ID="gvaverage" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="StateName" HeaderText="State" />--%>
                                                    <asp:BoundField DataField="Start" HeaderText="Start" />
                                                    <asp:BoundField DataField="End" HeaderText="End" />
                                                    <asp:BoundField DataField="EnumDescription" HeaderText="Measurement" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <%--                                                            <asp:Button ID="btnEditAverage" Text="Edit" runat="server" CommandArgument='<%#  Eval("TransportationCostFactorId") %>' OnClick="btnEditAverage_Click" CssClass="btn btn-sm btn-primary" />--%>
                                                            <asp:Button ID="btnDeleteAverage" Text="Delete" runat="server" CommandArgument='<%#  Eval("TransportationCostFactorId") %>' OnClick="btnDeleteAverage_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--Unloadingchargemodal--%>
                        <div class="modal fade" id="Unloadingchargemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-xl" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="unloadingmodal">Unloading Charge :
                                [<asp:Label ID="lblstatenameunload" runat="server"></asp:Label>]
                                            <asp:HiddenField runat="server" ID="hdnunloadchargeId" />
                                            <asp:HiddenField runat="server" ID="hdnUnloadType" />

                                        </h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-12">
                                                <asp:ValidationSummary ID="ValidationSummary3" CssClass="text-danger" runat="server" ValidationGroup="g3" />
                                            </div>
                                            <div class="col-md-12">
                                                <div class="input-group mb-3">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Start</span>
                                                        <asp:TextBox ID="txtunloadstart" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtunloadstart" runat="server" InitialValue="0" ControlToValidate="txtunloadstart" Display="None" ValidationGroup="g3" ErrorMessage="Enter Start" />
                                                    </div>

                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">End</span>
                                                        <asp:TextBox ID="txtunloadend" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtunloadend" runat="server" InitialValue="0" ControlToValidate="txtunloadend" Display="None" ValidationGroup="g3" ErrorMessage="Enter End" />
                                                    </div>

                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Measurement</span>
                                                        <asp:DropDownList ID="drpunitunload" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rf_drpunitunload" runat="server" InitialValue="0" ControlToValidate="drpunitunload" Display="None" ValidationGroup="g3" ErrorMessage="Select Measurement" />
                                                    </div>

                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Amount</span>
                                                        <asp:TextBox ID="txtunloadamount" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtunloadamount" runat="server" InitialValue="0" ControlToValidate="txtunloadamount" Display="None" ValidationGroup="g3" ErrorMessage="Enter Amount" />
                                                    </div>


                                                </div>
                                            </div>
                                        </div>

                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                            <asp:Button ID="btnAddunload" CssClass="btn btn-primary" OnClick="btnAddunload_Click" ValidationGroup="g3" CausesValidation="true" runat="server" Text="Add" />
                                        </div>

                                        <div class="container col-12">
                                            <br />
                                            <br />
                                        </div>
                                        <div class="container col-12">
                                            <asp:GridView ID="gvunloading" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="StateName" HeaderText="State" />--%>
                                                    <asp:BoundField DataField="Start" HeaderText="Start" />
                                                    <asp:BoundField DataField="End" HeaderText="End" />
                                                    <asp:BoundField DataField="EnumDescription" HeaderText="Measurement" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <%--<asp:Button ID="btnEditUnload" Text="Edit" runat="server" CommandArgument='<%#  Eval("UnloadingCostFactorId") %>' OnClick="btnEditUnload_Click" CssClass="btn btn-sm btn-primary" />--%>
                                                            <asp:Button ID="btnDeleteUnload" Text="Delete" runat="server" CommandArgument='<%#  Eval("UnloadingCostFactorId") %>' OnClick="btnDeleteUnload_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--Localchargemodal--%>
                        <div class="modal fade" id="Localchargemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-xl" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="localmodal">Local Charge :
                                [<asp:Label ID="lblstatenamecartage" runat="server"></asp:Label>]
                                            <asp:HiddenField runat="server" ID="hdnlocalchargeId" />
                                            <asp:HiddenField runat="server" ID="hdnlocalType" />

                                        </h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-12">
                                                <asp:ValidationSummary ID="ValidationSummary4" CssClass="text-danger" runat="server" ValidationGroup="g4" />
                                            </div>
                                            <div class="col-md-12">
                                                <div class="input-group mb-3">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Start</span>
                                                        <asp:TextBox ID="txtlocalstart" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtlocalstart" runat="server" InitialValue="0" ControlToValidate="txtlocalstart" Display="None" ValidationGroup="g4" ErrorMessage="Enter Start" />
                                                    </div>

                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">End</span>
                                                        <asp:TextBox ID="txtlocalend" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtlocalend" runat="server" InitialValue="0" ControlToValidate="txtlocalend" Display="None" ValidationGroup="g4" ErrorMessage="Enter End" />
                                                    </div>

                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Measurement</span>
                                                        <asp:DropDownList ID="drpunitlocal" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rf_drpunitlocal" runat="server" InitialValue="0" ControlToValidate="drpunitlocal" Display="None" ValidationGroup="g4" ErrorMessage="Select Measurement" />
                                                    </div>

                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Amount</span>
                                                        <asp:TextBox ID="txtlocalamount" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtlocalamount" runat="server" InitialValue="0" ControlToValidate="txtlocalamount" Display="None" ValidationGroup="g4" ErrorMessage="Enter Amount" />
                                                    </div>


                                                </div>
                                            </div>
                                        </div>

                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                            <asp:Button ID="btnAddlocal" CssClass="btn btn-primary" OnClick="btnAddlocal_Click" ValidationGroup="g4" CausesValidation="true" runat="server" Text="Add" />
                                        </div>

                                        <div class="container col-12">
                                            <br />
                                            <br />
                                        </div>
                                        <div class="container col-12">
                                            <asp:GridView ID="gvcartage" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Start" HeaderText="Start" />
                                                    <asp:BoundField DataField="End" HeaderText="End" />
                                                    <asp:BoundField DataField="EnumDescription" HeaderText="Measurement" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <%--<asp:Button ID="btnEditLocal" Text="Edit" runat="server" CommandArgument='<%#  Eval("CartageCostFactorId") %>' OnClick="btnEditLocal_Click" CssClass="btn btn-sm btn-primary" />--%>
                                                            <asp:Button ID="btnDeleteLocal" Text="Delete" runat="server" CommandArgument='<%#  Eval("CartageCostFactorId") %>' OnClick="btnDeleteLocal_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
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
                    $('#<%= gvtcm.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvtcm.ClientID %>').find("tr:first"))).DataTable({
                        "destroy": true,
                        "paging": true,
                        "lengthChange": true,
                        "searching": true,
                        "ordering": true,
                        "info": false,
                        "autoWidth": false,
                        //"responsive": true,
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

                $('#<%= gvaverage.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvaverage.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": false,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": false,
                    "info": false,
                    "autoWidth": false,
                    "responsive": true,
                    fixedHeader: {
                        footer: false
                    }
                });

                $('#<%= gvunloading.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvunloading.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": false,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": false,
                    "info": false,
                    "autoWidth": false,
                    "responsive": true,
                    fixedHeader: {
                        footer: false
                    }
                });

                $('#<%= gvcartage.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvcartage.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": false,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": false,
                    "info": false,
                    "autoWidth": false,
                    "responsive": true,
                    fixedHeader: {
                        footer: false
                    }
                });
            });

            function popup(type) {


                if (type == '1') {
                    $('#AverageLocalmodal').modal('show');

                }
                else if (type == '2') {
                    $('#Unloadingchargemodal').modal('show');
                }
                else if (type == '3') {
                    $('#Localchargemodal').modal('show');
                }
            }

            function Averagelocalclose() {
                alert('test')
                $('#AverageLocalmodal').modal('hide');
            }
            

            function redirectToReport() {
              
                window.location.href = 'ReportTransportingCosting.aspx';

            }

            function calculaterate() {

                var txttruckload = document.getElementById("<%=txttruckload.ClientID%>");
                    var txtNoCarton = document.getElementById("<%=txtNoCarton.ClientID%>");
                    var txt1cartoncharge = document.getElementById("<%=txt1cartoncharge.ClientID%>");

                txttruckload.value = parseFloat(txttruckload.value) || '0';
                txtNoCarton.value = parseFloat(txtNoCarton.value) || '0';
                txt1cartoncharge.value = parseFloat(txt1cartoncharge.value) || '0';


                txt1cartoncharge.value = Number(parseFloat(txttruckload.value / txtNoCarton.value)).toFixed(2);

            }

        </script>
</asp:Content>
