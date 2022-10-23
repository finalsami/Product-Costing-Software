<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ProductwiseLabourCostMaster.aspx.cs" Inherits="Production_Costing_Software.ProductwiseLabourCostMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Product wise Labour Cost</b>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                    <asp:HiddenField runat="server" ID="MeasurementId" />
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Bulk Product</span>
                                            </div>
                                            <asp:DropDownList ID="drpbpm" onchange="drpbpmchange('0');" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpbpm" runat="server" ControlToValidate="drpbpm" Display="None" ValidationGroup="g1" ErrorMessage="Select Bulk Product" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Packing Size</span>
                                            </div>
                                            <asp:DropDownList ID="drppacksize" onchange="drppacksizechange(this);" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drppacksize" runat="server" ControlToValidate="drppacksize" Display="None" ValidationGroup="g1" ErrorMessage="Select Pack Size" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Packing Discription</span>
                                            </div>
                                            <asp:TextBox ID="txtpackdiscription" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Packing Style Category</span>
                                            </div>
                                            <asp:DropDownList ID="drppsc" onchange="drppscchange(this);" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drppsc" runat="server" ControlToValidate="drppsc" Display="None" ValidationGroup="g1" ErrorMessage="Select Packing Style Category" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Packing Style</span>
                                            </div>
                                            <asp:DropDownList ID="drpps" onchange="drppschange(this);" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpps" runat="server" ControlToValidate="drpps" Display="None" ValidationGroup="g1" ErrorMessage="Select Packing Style" />
                                        </div>

                                    </div>


                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Filling Machine Output</b>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Storck / Nosel</span>
                                            </div>
                                            <asp:TextBox ID="txtstocknosel" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtstocknosel" runat="server" ControlToValidate="txtstocknosel" Display="None" ValidationGroup="g1" ErrorMessage="Enter Storck / Nosel" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">No of Nosels/Filling Line</span>
                                            </div>
                                            <asp:TextBox ID="txtnoselfillingline" class="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtnoselfillingline" runat="server" ControlToValidate="txtnoselfillingline" Display="None" ValidationGroup="g1" ErrorMessage="Enter No of Nosels/Filling Line" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3 ">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Output/Minut/Filling Line</span>
                                            </div>
                                            <asp:TextBox ID="txtttloutminutfilling" Enabled="false" onchange="calculaterate();" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Out Put In Liter Or KG / Shift</span>
                                            </div>
                                            <asp:TextBox ID="txtoutputinliterkgshift" Enabled="false" CssClass="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total out put Bottels in Net Shift Hours</span>
                                            </div>
                                            <asp:TextBox ID="txtttlopbottlenetshifthour" Enabled="false" CssClass="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Total Labour Costing</b>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 ">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">No of workers</span>
                                            </div>
                                            <asp:TextBox ID="txtnoofworker" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Supervisor</span>
                                            </div>
                                            <asp:TextBox ID="txtsupervisor" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtsupervisor" runat="server" ControlToValidate="txtsupervisor" Display="None" ValidationGroup="g1" ErrorMessage="Enter Supervisor" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Labour & Supervisior Coasting</span>
                                            </div>
                                            <asp:TextBox ID="txtttllaboursupercost" Enabled="false" InitialValue="0" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Total Power Costing</b>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total packing Style Power Units X Unit Cost</span>
                                            </div>
                                            <asp:TextBox ID="txtpacktsylepowerXUnitcost" Enabled="false" InitialValue="0" CssClass="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Power Cost (*<asp:Label ID="lblNetshifthour" runat="server"></asp:Label>)</span>
                                            </div>
                                            <asp:TextBox ID="txttotalpowercost" Enabled="false" CssClass="form-control" InitialValue="0" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">/Shift</span>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Other Costing</b>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3 ">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Unloading</span>
                                            </div>
                                            <asp:TextBox ID="txtunloading" CssClass="form-control" InitialValue="0" Enabled="false" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Loading</span>
                                            </div>
                                            <asp:TextBox ID="txtloading" CssClass="form-control" InitialValue="0" Enabled="false" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Machinary Maintatnce</span>
                                            </div>
                                            <asp:TextBox ID="txtmachinemaintanance" Enabled="false" InitialValue="0" CssClass="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="input-group mb-3 ">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Admin Expence</span>
                                            </div>
                                            <asp:TextBox ID="txtadminexpence" InitialValue="0" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3 ">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Extra Expence</span>
                                            </div>
                                            <asp:TextBox ID="txtextraexpence" InitialValue="0" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Final Costing</b>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Costing</span>
                                            </div>
                                            <asp:TextBox ID="txttotalcosting" InitialValue="0" Enabled="false" CssClass="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="input-group mb-3 ">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Output in Liter</span>
                                            </div>
                                            <asp:TextBox ID="txttotalopinltr" InitialValue="0" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Final per liter labour costing</span>
                                            </div>
                                            <asp:TextBox ID="txtfinalltrlabourcost" InitialValue="0" Enabled="false" CssClass="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Additional Cost (Buffer)</b>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Additional Cost (Buffer) /Ltr</span>
                                            </div>
                                            <asp:TextBox ID="txtadditionalcost" InitialValue="0" CssClass="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtadditionalcost" runat="server" ControlToValidate="txtadditionalcost" Display="None" ValidationGroup="g1" ErrorMessage="Enter Additional Buffer" />

                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Net Labour Cost/Ltr</span>
                                            </div>
                                            <asp:TextBox ID="txtnetlabourcostltr" InitialValue="0" Enabled="false" CssClass="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Final Per Unit Labour Cost</span>
                                            </div>
                                            <asp:TextBox ID="txtfinalunitlabourcost" InitialValue="0" Enabled="false" CssClass="form-control" onchange="calculaterate();" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1"  runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdnSvcharge" runat="server" />
                                        <asp:HiddenField ID="hdnLcharge" runat="server" />
                                        <asp:HiddenField ID="hdnloadingltr" runat="server" />
                                        <asp:HiddenField ID="hdnunloadingltr" runat="server" />
                                        <asp:HiddenField ID="hdnadminltr" runat="server" />
                                        <asp:HiddenField ID="hdnextraltr" runat="server" />
                                        <asp:HiddenField ID="hdnPMRMCategoryId" runat="server" />
                                        <asp:HiddenField ID="hdnbpmid" runat="server" />
                                        <asp:HiddenField ID="hdnpackingstyleId" runat="server" />
                                        <asp:HiddenField ID="hdnPackingsizeCatId" runat="server" />
                                        <asp:HiddenField ID="hdnpacksize" runat="server" />
                                        <asp:HiddenField ID="hdnPackingStyleCatId" runat="server" />
                                        <asp:HiddenField ID="hdnunitmeasurementName" runat="server" />
                                        <asp:HiddenField ID="hdnNetshiftHour" runat="server" />
                                        <asp:HiddenField ID="hdnmachineltr" runat="server" />

                                        <asp:TextBox ID="txtsize" Style="display: none" runat="server"></asp:TextBox>

                                        <asp:HiddenField ID="hdnpwlcId" runat="server" />
                                    </div>

                                    <div class="container col-12">
                                        <br />
                                        <br />
                                    </div>



                                </div>




                                <div class="container col-12">

                                    <asp:GridView ID="gvpwlc" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                            <asp:BoundField DataField="PackingSize" HeaderText="PackingSize" />
                                            <asp:BoundField DataField="EnumDescription" HeaderText="Measurement" />
                                            <asp:BoundField DataField="FillingMachine" HeaderText="FillingMachine" />
                                            <asp:BoundField DataField="TotalOutLtrKGUnit" HeaderText="Total-Out-Put-In-LiterOrKG/Shift" />
                                            <asp:BoundField DataField="powercosting" HeaderText="Total-Power-Costing" />
                                            <asp:BoundField DataField="LabourSupervisorCost" HeaderText="Labour-Supervisior Cost" />
                                            <asp:BoundField DataField="TotalCosting" HeaderText="TotalCosting" />
                                            <asp:BoundField DataField="TotaloutputinLtr" HeaderText="TotaloutputinLtr" />
                                            <asp:BoundField DataField="LaboutCostLtr" HeaderText="Labour Costing /Ltr" />
                                            <asp:BoundField DataField="AdditionalCostBuffer" HeaderText="Additional Cost Buffer" />
                                            <asp:BoundField DataField="NetLaboutCostLtr" HeaderText="Net-Labour-Cost/Ltr" />
                                            <asp:BoundField DataField="LaboutCostUnit" HeaderText="Labout Cost/Unit" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("ProductwiseLaborCostId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                    <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("ProductwiseLaborCostId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

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
                $('#<%= gvpwlc.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvpwlc.ClientID %>').find("tr:first"))).DataTable({
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
        function drpbpmchange(id) {
            var drpbpmid = document.getElementById('<%=drpbpm.ClientID %>');

            var PMRMCategoryId = document.getElementById('<%=hdnPMRMCategoryId.ClientID %>');
            var bpmid = drpbpmid.value.split('-')[0];
            PMRMCategoryId.value = drpbpmid.value.split('-')[1];
            document.getElementById('<%=hdnPMRMCategoryId.ClientID %>').value = PMRMCategoryId.value;
            var MeasurementId = document.getElementById('<%=MeasurementId.ClientID %>').value;

            document.getElementById('<%=hdnbpmid.ClientID %>').value = bpmid;

            $('#<%=drppacksize.ClientID %>').empty();
            $.ajax({
                url: 'WebService.asmx/BindPackSize',
                data: { BpmId: bpmid },
                dataType: "xml",
                method: 'POST',
                success: function (Result) {
                    $("#<%=drppacksize.ClientID %>").empty();
                    for (var i = 0; i < Result.all[0].children.length; i++) {
                        $("#<%=drppacksize.ClientID %>").append($("<option></option>").val(Result.all[0].children[i].children[0].innerHTML).html(Result.all[0].children[i].children[1].innerHTML));

                        if (id > 0) {

                            if (id == Result.all[0].children[i].children[0].innerHTML) {
                                document.getElementById("<%=txtsize.ClientID%>").value = Result.all[0].children[i].children[1].innerHTML;
                            }

                            $("#<%=drppacksize.ClientID %> option[value=" + id + "]").attr('selected', 'selected');
                            document.getElementById("<%=drppacksize.ClientID %>").value = id;

                        }
                        else {

                        }
                    }
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }

        function setvalues(packsize, packsizecatId, packstyleId) {

            drpbpmchange(packsize);

            drppacksizechange(packsizecatId, packsize);

            drppscchange(packstyleId, packsizecatId);

            drppschange(packsizecatId, packstyleId);
        }

        function drppacksizechange(id, packsize) {
            var packsizeid = packsize > 0 ? packsize : document.getElementById('<%=drppacksize.ClientID %>').value;
            var hdnpacksize = document.getElementById('<%=hdnpacksize.ClientID %>');
            hdnpacksize.value = packsizeid;
            $('#<%=drppsc.ClientID %>').empty();
            $.ajax({
                url: 'WebService.asmx/PackStyleCatByPackSize',
                data: { PackSize: packsizeid },
                dataType: "xml",
                method: 'POST',
                success: function (Result) {
                    $("#<%=drppsc.ClientID %>").empty();
                    for (var i = 0; i < Result.all[0].children.length; i++) {
                        $("#<%=drppsc.ClientID %>").append($("<option></option>").val(Result.all[0].children[i].children[0].innerHTML).html(Result.all[0].children[i].children[1].innerHTML));
                        if (id > 0) {
                            $("#<%=drppsc.ClientID %> option[value=" + id + "]").attr('selected', 'selected');


                        }
                        else {

                            var v = document.getElementById("<%=drppacksize.ClientID %>");
                            document.getElementById("<%=txtsize.ClientID%>").value = v.options[v.selectedIndex].text;
                        }
                    }
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }

        function drppscchange(id, packsizecatId) {
            var PSCId = packsizecatId > 0 ? packsizecatId : document.getElementById('<%=drppsc.ClientID %>').value;
            var hdnPackingsizeCatId = document.getElementById('<%=hdnPackingsizeCatId.ClientID %>');
            hdnPackingsizeCatId.value = PSCId;
            ////$('#<%=drpps.ClientID %>').empty();
            $.ajax({
                url: 'WebService.asmx/PackStyleByPackstyleCatagory',
                data: { PackSizeCatId: PSCId },
                dataType: "xml",
                method: 'POST',
                success: function (Result) {
                    $("#<%=drpps.ClientID %>").empty();
                    for (var i = 0; i < Result.all[0].children.length; i++) {
                        $("#<%=drpps.ClientID %>").append($("<option></option>").val(Result.all[0].children[i].children[0].innerHTML).html(Result.all[0].children[i].children[1].innerHTML));
                        if (id > 0) {
                            $("#<%=drpps.ClientID %> option[value=" + id + "]").attr('selected', 'selected');
                        }
                    }
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }
        function drppschange(packsizecatId, packstyleId) {
            var PSCId = packsizecatId > 0 ? packsizecatId : document.getElementById('<%=drppsc.ClientID %>').value;
            var PSId = packstyleId > 0 ? packstyleId : document.getElementById('<%=drpps.ClientID %>').value;

<%--            var PSCId = document.getElementById('<%=drppsc.ClientID %>').value;
            var PSId = document.getElementById('<%=drpps.ClientID %>').value;--%>

            var hdnpackingstyleId = document.getElementById('<%=hdnpackingstyleId.ClientID %>');
            hdnpackingstyleId.value = PSId;
            var txtnoofworker = document.getElementById('<%=txtnoofworker.ClientID %>').value;
            var txtttllaboursupercost = document.getElementById('<%=txtttllaboursupercost.ClientID %>').value;
            var txtpacktsylepowerXUnitcost = document.getElementById('<%=txtpacktsylepowerXUnitcost.ClientID %>').value;
            var txttotalpowercost = document.getElementById('<%=txttotalpowercost.ClientID %>').value;

            $.ajax({
                url: 'WebService.asmx/GetDataByPackStyleCatPackStyle',
                data: { PackSizeCatId: PSCId, PackStyleId: PSId },
                dataType: "xml",
                method: 'POST',
                success: function (r) {
                    var Unit = r.all[0].textContent;
                    var val1 = Unit.split('~')[0];
                    var val2 = Unit.split('~')[1];
                    var val3 = Unit.split('~')[2];
                    var val4 = Unit.split('~')[3];
                    var val5 = Unit.split('~')[4];
                    var val6 = Unit.split('~')[5];
                    document.getElementById('<%=txtnoofworker.ClientID %>').value = val1;
                    document.getElementById('<%=txtttllaboursupercost.ClientID %>').value = val2;
                    document.getElementById('<%=txtpacktsylepowerXUnitcost.ClientID %>').value = val3;
                    document.getElementById('<%=txttotalpowercost.ClientID %>').value = val4;
                    document.getElementById('<%=hdnLcharge.ClientID %>').value = val5;
                    document.getElementById('<%=hdnSvcharge.ClientID %>').value = val6;

                    calculaterate();

                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }
        function calculaterate() {
            var Minuts = "60";
            var KGLiter = "1";
            var Militer = "1000";
            var ConvrtMilToLtr = "0";


            var txtstocknosel = document.getElementById('<%=txtstocknosel.ClientID %>');
            var txtnoselfillingline = document.getElementById('<%=txtnoselfillingline.ClientID %>');
            var txtttloutminutfilling = document.getElementById('<%=txtttloutminutfilling.ClientID %>').value;
            var packSize = document.getElementById('<%=drppacksize.ClientID %>');
            var PSCId = document.getElementById('<%=drppsc.ClientID %>').value;
            var txtoutputinliterkgshift = document.getElementById('<%=txtoutputinliterkgshift.ClientID %>').value;
            var txtttlopbottlenetshifthour = document.getElementById('<%=txtttlopbottlenetshifthour.ClientID %>').value;
            var txtsupervisor = document.getElementById('<%=txtsupervisor.ClientID %>');
            var txtttllaboursupercost = document.getElementById('<%=txtttllaboursupercost.ClientID %>');
            var txtnoofworker = document.getElementById('<%=txtnoofworker.ClientID %>');

           // alert(txtnoofworker.value);

            var hdnSvcharge = document.getElementById('<%=hdnSvcharge.ClientID %>');
            var hdnLcharge = document.getElementById('<%=hdnLcharge.ClientID %>');

            var hdnunloadingltr = document.getElementById('<%=hdnunloadingltr.ClientID %>');
            var hdnloadingltr = document.getElementById('<%=hdnloadingltr.ClientID %>');
            var hdnmachineltr = document.getElementById('<%=hdnmachineltr.ClientID %>');
            var hdnadminltr = document.getElementById('<%=hdnadminltr.ClientID %>');
            var hdnextraltr = document.getElementById('<%=hdnextraltr.ClientID %>');
            var txttotalcosting = document.getElementById('<%=txttotalcosting.ClientID %>')
            var txtpacktsylepowerXUnitcost = document.getElementById('<%=txtpacktsylepowerXUnitcost.ClientID %>');
            var txttotalopinltr = document.getElementById('<%=txttotalopinltr.ClientID %>');
            var txtfinalltrlabourcost = document.getElementById('<%=txtfinalltrlabourcost.ClientID %>');
            var txtadditionalcost = document.getElementById('<%=txtadditionalcost.ClientID %>');
            var txtnetlabourcostltr = document.getElementById('<%=txtnetlabourcostltr.ClientID %>');
            var txtfinalunitlabourcost = document.getElementById('<%=txtfinalunitlabourcost.ClientID %>');
            var hdnpacksize = document.getElementById('<%=hdnpacksize.ClientID %>');
            var hdnunitmeasurementName = document.getElementById('<%=hdnunitmeasurementName.ClientID %>');
            var hdnNetshiftHour = document.getElementById('<%=hdnNetshiftHour.ClientID %>');


            txtstocknosel.value = parseFloat(txtstocknosel.value) || '0';
            txtnoselfillingline.value = parseFloat(txtnoselfillingline.value) || '0';
            txtttloutminutfilling.value = parseFloat(txtttloutminutfilling.value) || '0';
            txtoutputinliterkgshift.value = parseFloat(txtoutputinliterkgshift.value) || '0';
            txtttlopbottlenetshifthour.value = parseFloat(txtttlopbottlenetshifthour.value) || '0';
            txtsupervisor.value = parseFloat(txtsupervisor.value) || '0';
            txtnoofworker.value = parseFloat(txtnoofworker.value) || '0';
            hdnSvcharge.value = parseFloat(hdnSvcharge.value) || '0';
            hdnLcharge.value = parseFloat(hdnLcharge.value) || '0';
            hdnunloadingltr.value = parseFloat(hdnunloadingltr.value) || '0';
            hdnloadingltr.value = parseFloat(hdnloadingltr.value) || '0';
            hdnmachineltr.value = parseFloat(hdnmachineltr.value) || '0';
            hdnadminltr.value = parseFloat(hdnadminltr.value) || '0';
            hdnextraltr.value = parseFloat(hdnextraltr.value) || '0';
            //txttotalcosting.value = parseFloat(txttotalcosting.value) || '0';
            txttotalopinltr.value = parseFloat(txttotalopinltr.value) || '0';
            txtfinalltrlabourcost.value = parseFloat(txtfinalltrlabourcost.value) || '0';
            txtadditionalcost.value = parseFloat(txtadditionalcost.value) || '0';
            txtnetlabourcostltr.value = parseFloat(txtnetlabourcostltr.value) || '0';
            txtfinalunitlabourcost.value = parseFloat(txtfinalunitlabourcost.value) || '0';
            hdnpacksize.value = parseFloat(hdnpacksize.value) || '0';
            hdnunitmeasurementName.value = parseFloat(hdnunitmeasurementName.value) || '0';
            hdnNetshiftHour.value = parseFloat(hdnNetshiftHour.value) || '0';
            txtpacktsylepowerXUnitcost.value = parseFloat(txtpacktsylepowerXUnitcost.value) || '0'; 


            var packsizeval = document.getElementById("<%=txtsize.ClientID%>").value;


            var Measurement = packsizeval.split('-')[1];
            if (Measurement.indexOf(" ") > 0) {
                Measurement = Measurement.substring(0, Measurement.indexOf(" "));
            }
            var Packsize = packsizeval.split('-')[0];
         

            document.getElementById('<%=txtttloutminutfilling.ClientID %>').value = Number(parseFloat(txtstocknosel.value) * parseFloat(txtnoselfillingline.value)).toFixed(2);
            document.getElementById('<%=txtttlopbottlenetshifthour.ClientID %>').value = Number((document.getElementById('<%=txtttloutminutfilling.ClientID %>').value) * Minuts * hdnNetshiftHour.value).toFixed(2);
            document.getElementById('<%=txtttllaboursupercost.ClientID %>').value = parseFloat(document.getElementById('<%=txtnoofworker.ClientID %>').value) * (parseFloat(hdnLcharge.value)) + (parseFloat(txtsupervisor.value)) * (parseFloat(hdnSvcharge.value));

           

            if (Measurement == "ML" || Measurement == "GM") {

                document.getElementById('<%=txtoutputinliterkgshift.ClientID %>').value = Number((document.getElementById('<%=txtttlopbottlenetshifthour.ClientID %>').value) * (Packsize / Militer)).toFixed(2);

                ConvrtMilToLtr = 1000 / parseFloat(Packsize);
            }
            else if (Measurement == "LTR" || Measurement == "KG") {
                document.getElementById('<%=txtoutputinliterkgshift.ClientID %>').value = Number((document.getElementById('<%=txtttlopbottlenetshifthour.ClientID %>').value) * (Packsize / KGLiter)).toFixed(2);

                ConvrtMilToLtr = parseFloat(Packsize);
            }
          

            document.getElementById('<%=txtunloading.ClientID %>').value = Number((document.getElementById('<%=txtoutputinliterkgshift.ClientID %>').value) * parseFloat(hdnunloadingltr.value)).toFixed(2);
            document.getElementById('<%=txtloading.ClientID %>').value = Number((document.getElementById('<%=txtoutputinliterkgshift.ClientID %>').value) * parseFloat(hdnloadingltr.value)).toFixed(2);
            document.getElementById('<%=txtmachinemaintanance.ClientID %>').value = Number((document.getElementById('<%=txtoutputinliterkgshift.ClientID %>').value) * parseFloat(hdnmachineltr.value)).toFixed(2);
            document.getElementById('<%=txtadminexpence.ClientID %>').value = Number((document.getElementById('<%=txtoutputinliterkgshift.ClientID %>').value) * parseFloat(hdnadminltr.value)).toFixed(2);
            document.getElementById('<%=txtextraexpence.ClientID %>').value = Number((document.getElementById('<%=txtoutputinliterkgshift.ClientID %>').value) * parseFloat(hdnextraltr.value)).toFixed(2);

            document.getElementById('<%=txttotalcosting.ClientID %>').value = Number(parseFloat(txtpacktsylepowerXUnitcost.value) + parseFloat(txtttllaboursupercost.value) + parseFloat(document.getElementById('<%=txtunloading.ClientID %>').value) +
                parseFloat(document.getElementById('<%=txtloading.ClientID %>').value) + parseFloat(document.getElementById('<%=txtmachinemaintanance.ClientID %>').value) + parseFloat(document.getElementById('<%=txtadminexpence.ClientID %>').value) +
                parseFloat(document.getElementById('<%=txtextraexpence.ClientID %>').value)).toFixed(2);


            document.getElementById('<%=txttotalopinltr.ClientID %>').value = document.getElementById('<%=txtoutputinliterkgshift.ClientID %>').value;
            document.getElementById('<%=txtfinalltrlabourcost.ClientID %>').value = Number(parseFloat(document.getElementById('<%=txttotalcosting.ClientID %>').value) / parseFloat(document.getElementById('<%=txttotalopinltr.ClientID %>').value)).toFixed(2);
            document.getElementById('<%=txtnetlabourcostltr.ClientID %>').value = Number(parseFloat(document.getElementById('<%=txtfinalltrlabourcost.ClientID %>').value) + parseFloat(document.getElementById('<%=txtadditionalcost.ClientID %>').value)).toFixed(2);
            document.getElementById('<%=txtfinalunitlabourcost.ClientID %>').value = Number(document.getElementById('<%=txtnetlabourcostltr.ClientID %>').value / parseFloat(ConvrtMilToLtr)).toFixed(2);


        }
    </script>

</asp:Content>
