<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CostVariableMaster.aspx.cs" Inherits="Production_Costing_Software.CostVariableMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Cost Variable Master</b>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="card-body">
                            <div class="container col-12 ">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="text-danger" runat="server" ValidationGroup="g1" />
                                    </div>



                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Add Labour Task</b>
                                    </div>

                                    <%-------------------- Add Labour Task--------------------------%>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Shift Time</span>
                                            </div>
                                            <asp:TextBox ID="txtshittime" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtshittime" runat="server" ControlToValidate="txtshittime" Display="None" ValidationGroup="g1" ErrorMessage="Enter Shift time" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Break And other Timming</span>
                                            </div>
                                            <asp:TextBox ID="txtbreakandothertimming" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtbreakandothertimming" runat="server" ControlToValidate="txtbreakandothertimming" Display="None" ValidationGroup="g1" ErrorMessage="Enter Break and other trimming" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Net Shift Hours</span>
                                            </div>
                                            <asp:TextBox ID="txtnetshifthour" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtnetshifthour" runat="server" ControlToValidate="txtnetshifthour" Display="None" ValidationGroup="g1" ErrorMessage="Enter Net Shift Hour" />
                                        </div>

                                    </div>


                                    <%-------------------- AddPower Unit Price--------------------------%>

                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Power Unit Price</b>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Power Unit Price/Hour</span>
                                            </div>
                                            <asp:TextBox ID="txtpowerunitprice" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpowerunitprice" runat="server" ControlToValidate="txtpowerunitprice" Display="None" ValidationGroup="g1" ErrorMessage="Power Unit Price/Hour" />
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Labour Cost</b>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Labour Charge/Day</span>
                                            </div>
                                            <asp:TextBox ID="txtlchargeperday" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtlchargeperday" runat="server" ControlToValidate="txtlchargeperday" Display="None" ValidationGroup="g1" ErrorMessage="Enter Power Induction" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Supervisor Costing/Day</span>
                                            </div>
                                            <asp:TextBox ID="txtsupervisorcost" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtsupervisorcost" runat="server" ControlToValidate="txtsupervisorcost" Display="None" ValidationGroup="g1" ErrorMessage="Supervisor Costing/Day" />
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Other Costing Variables</b>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="input-group mb-3 ">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Unloading Expence /Ltr</span>
                                            </div>
                                            <asp:TextBox ID="txtunloadingexpenceltr" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtunloadingexpence" runat="server" ControlToValidate="txtunloadingexpenceltr" Display="None" ValidationGroup="g1" ErrorMessage="Enter Unloading Expence /Ltr" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Unloading Expence /Kg</span>
                                            </div>
                                            <asp:TextBox ID="txtunloadingexpencekg" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtunloadingexpencekg" runat="server" ControlToValidate="txtunloadingexpencekg" Display="None" ValidationGroup="g1" ErrorMessage="Unloading Expence /Kg" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Unloading Expence /Unit</span>
                                            </div>
                                            <asp:TextBox ID="txtunloadingexpenceunit" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtunloadingexpenceunit" runat="server" ControlToValidate="txtunloadingexpenceunit" Display="None" ValidationGroup="g1" ErrorMessage="Unloading Expence /Unit" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="input-group mb-3 ">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">loading Expence /Ltr</span>
                                            </div>
                                            <asp:TextBox ID="txtloadingexpenceperltr" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtloadingexpenceperltr" runat="server" ControlToValidate="txtloadingexpenceperltr" Display="None" ValidationGroup="g1" ErrorMessage="Enter loading Expence /Ltr" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">loading Expence /Kg</span>
                                            </div>
                                            <asp:TextBox ID="txtloadingexpenceperkg" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtloadingexpenceperkg" runat="server" ControlToValidate="txtloadingexpenceperkg" Display="None" ValidationGroup="g1" ErrorMessage="loading Expence /Kg" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">loading Expence /Unit</span>
                                            </div>
                                            <asp:TextBox ID="txtloadingexpenceperunit" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_" runat="server" ControlToValidate="txtloadingexpenceperunit" Display="None" ValidationGroup="g1" ErrorMessage="loading Expence /Unit" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="input-group mb-3 ">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Machinary Mait Expence/Ltr</span>
                                            </div>
                                            <asp:TextBox ID="txtmachinmaitexpenceltr" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtmachinmaitexpenceltr" runat="server" ControlToValidate="txtmachinmaitexpenceltr" Display="None" ValidationGroup="g1" ErrorMessage="Machinary Mait Expence/Ltr" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Machinary Mait Expence/Kg</span>
                                            </div>
                                            <asp:TextBox ID="txtmachinmaitexpencekg" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtmachinmaitexpencekg" runat="server" ControlToValidate="txtmachinmaitexpencekg" Display="None" ValidationGroup="g1" ErrorMessage="Machinary Mait Expence/Kg" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Machinary Mait Expence/Unit</span>
                                            </div>
                                            <asp:TextBox ID="txtmachinmaitexpenceunit" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtmachinmaitexpenceunit" runat="server" ControlToValidate="txtmachinmaitexpenceunit" Display="None" ValidationGroup="g1" ErrorMessage="Machinary Mait Expence/Unit" />
                                        </div>
                                    </div>


                                    <div class="col-md-4">
                                        <div class="input-group mb-3 ">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Admin Expence /Ltr</span>
                                            </div>
                                            <asp:TextBox ID="txtadminexpenceltr" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtadminexpenceltr" runat="server" ControlToValidate="txtmachinmaitexpenceltr" Display="None" ValidationGroup="g1" ErrorMessage="Machinary Mait Expence/Ltr" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Admin Expence /Kg</span>
                                            </div>
                                            <asp:TextBox ID="txtadminexpencekg" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtadminexpencekg" runat="server" ControlToValidate="txtadminexpencekg" Display="None" ValidationGroup="g1" ErrorMessage="Admin Expence /Kg" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Machinary Mait Expence/Unit</span>
                                            </div>
                                            <asp:TextBox ID="txtadminexpenceunit" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtadminexpenceunit" runat="server" ControlToValidate="txtadminexpenceunit" Display="None" ValidationGroup="g1" ErrorMessage="Admin Expence/Unit" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="input-group mb-3 ">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Extra Expence/Ltr</span>
                                            </div>
                                            <asp:TextBox ID="txtextraexpenceltr" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtextraexpenceltr" runat="server" ControlToValidate="txtextraexpenceltr" Display="None" ValidationGroup="g1" ErrorMessage="Extra Expence/Ltr" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Extra Expence/Kg</span>
                                            </div>
                                            <asp:TextBox ID="txtextraexpencekg" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtextraexpencekg" runat="server" ControlToValidate="txtextraexpencekg" Display="None" ValidationGroup="g1" ErrorMessage="Extra Expence/Kg" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Extra Expence/Unit</span>
                                            </div>
                                            <asp:TextBox ID="txtextraexpenceunit" class="form-control"  TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtextraexpenceunit" runat="server" ControlToValidate="txtextraexpenceunit" Display="None" ValidationGroup="g1" ErrorMessage="Admin Extra Expence/Unit" />
                                        </div>
                                    </div>
                                    <%--------------------End  Power Details / Hour--------------------------%>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdncvmId" runat="server" />
                                    </div>

                                    <div class="container col-12">
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>


                        </div>




                        <div class="container col-12">

                            <asp:GridView ID="gvcvmaster" runat="server" style="display:none" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                <Columns>
                        
                                </Columns>
                            </asp:GridView>

                        </div>
                        </div>
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
                $('#<%= gvcvmaster.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvcvmaster.ClientID %>').find("tr:first"))).DataTable({
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
        
    </script>

</asp:Content>
