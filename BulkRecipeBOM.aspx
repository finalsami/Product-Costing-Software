<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BulkRecipeBOM.aspx.cs" Inherits="Production_Costing_Software.BulkRecipeBOM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>BulkRecipe (BOM)</b>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="card-body">
                            <div class="container col-12 ">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:ValidationSummary ID="ValidationSummary2" CssClass="text-danger" runat="server" ValidationGroup="g2" />
                                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="text-danger" runat="server" ValidationGroup="g1" />
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Main Category</span>
                                            </div>
                                            <asp:DropDownList ID="drpmaincategory" onchange="drprmcategorychange();" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpmaincategory" runat="server" InitialValue="0" ControlToValidate="drpmaincategory" Display="None" ValidationGroup="g1" ErrorMessage="Select Main Category" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Bulk Product</span>
                                            </div>
                                            <asp:DropDownList ID="drpbpm" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpbpm" runat="server" InitialValue="0" ControlToValidate="drpbpm" Display="None" ValidationGroup="g1" ErrorMessage="Select Bulk product" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Batch Size</span>
                                            </div>
                                            <asp:TextBox ID="txtbatchsize" CssClass="form-control" TextMode="Number" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtbatchsize" runat="server" ControlToValidate="txtbatchsize" Display="None" ValidationGroup="g1" ErrorMessage="Enter BatchSize" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Measurement</span>
                                            </div>
                                            <asp:DropDownList ID="drpunit" runat="server" class="form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpunit" runat="server" InitialValue="0" ControlToValidate="drpunit" Display="None" ValidationGroup="g1" ErrorMessage="Select Measurement Unit" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdnbomid" runat="server" />
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
                                <asp:GridView ID="gvbulkrecipe" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview" ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MainCategoryName" HeaderText="Main Category" />
                                        <asp:BoundField DataField="BulkProductName" HeaderText="Bulk Product" />
                                        <asp:BoundField DataField="BatchSize" HeaderText="BatchSize" />

                                        <asp:BoundField DataField="Unit" HeaderText="Measurement" />
                                        <asp:BoundField DataField="finalcost" HeaderText="FinalCost Bulk" />

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEditMain" Text="Edit" runat="server" CommandArgument='<%#  Eval("BOMId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDeleteMain" Text="Delete" runat="server" CommandArgument='<%#  Eval("BOMId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnInputTechnical" CommandArgument='<%#  Eval("BOMId") %>' Text="Input Technical" runat="server" OnClick="btnInputTechnical_Click" CssClass="btn btn-sm btn-secondary" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                        <div class="modal fade" id="showInputTechnical" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-xl">
                                <div class="modal-content ">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">Main Category : [<asp:Label ID="lblMainCategoryName" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>]
                                                                 &nbsp;
                                                           &nbsp;/ Name : [<asp:Label ID="lblBPM_Name" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>] 
                                                           &nbsp; / Batch Size : [<asp:Label ID="lblBatchSize" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>] 
                                                            &nbsp;/ Measurement : [<asp:Label ID="lblMeasurement" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>] 

                                        </h5>
                                        <button type="button" data-dismiss="modal" onclick="showhideTechnical('0');" class="btn-close btn-secondary">Close</button>

                                    </div>

                                    <div class="modal-body">

                                        <div class="row">
                                            <div class="col-12">
                                                <asp:ValidationSummary ID="ValidationSummary3" CssClass="text-danger" runat="server" ValidationGroup="g3" />
                                            </div>
                                        </div>


                                        <div class="row mb-3">
                                            <div class="col-md-4">
                                                <div class="input-group mb-3 ">
                                                    <div class="input-group-text">
                                                        <span class="input-group-text">Search RM</span>


                                                        <asp:TextBox ID="txtsearchRM" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <asp:HiddenField ID="hdnMainCategoryId" runat="server" />
                                                    <asp:HiddenField ID="hdnsearchRm" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rf_txtsearchRM" runat="server" ControlToValidate="txtsearchRM" Display="None" ValidationGroup="g3" ErrorMessage="Enter RM" />
                                                </div>

                                                <div class="ui-front">
                                                </div>

                                            </div>
                                            <div id="dvformulation" class="col-md-4">
                                                <div class="input-group-text  mb-3">
                                                    <asp:TextBox ID="txtRequiredFormulation" CssClass="form-control" runat="server" placeholder="Required Formulation"></asp:TextBox>
                                                    <asp:TextBox ID="txtInputKG" CssClass="form-control" runat="server" Style="display: none" placeholder="Quantity Ltr/Kg"></asp:TextBox>
                                                    <asp:Label ID="lblInputKG" Visible="false" CssClass="" runat="server" Text="" placeholder=""></asp:Label>
                                                    <asp:Label ID="lnlReqruiredFormulation" class="form-check" runat="server" Text="Formulation?">
                                                    </asp:Label>
                                                    &nbsp;&nbsp; 
                                                    <asp:CheckBox ID="ChkReqruiredFormulation" OnClick="CheckChange();" class="form-check-inline" runat="server" />

                                                    <asp:RequiredFieldValidator ID="rf_quanity" runat="server" ControlToValidate="txtInputKG" Display="None" InitialValue="" ValidationGroup="g3" ErrorMessage="Enter Quantity Ltr/Kg" />
                                                    <asp:RequiredFieldValidator ID="rf_formulation" runat="server" ControlToValidate="txtRequiredFormulation" InitialValue="" Display="None" ValidationGroup="g3" ErrorMessage="Enter Formulation" />
                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group-text  mb-3">
                                                    <asp:TextBox ID="lblsolvant" CssClass="form-control" Enabled="false" runat="server" placeholder="Solvent(QS)"></asp:TextBox>
                                                    &nbsp;&nbsp;<asp:CheckBox ID="chkSolvent" OnClick="chkSolvent();" Checked="false" CssClass="form-check-inline" Style="margin-left: 5px" runat="server" />
                                                    <label for="txtsolvent">Solvent</label>
                                                    <asp:TextBox ID="InputSubTotaltxt" ReadOnly="true" Visible="false" class="form-control" type="text" runat="server"></asp:TextBox>
                                                </div>
                                                <asp:HiddenField ID="hdntotalsum" runat="server" />
                                                <asp:HiddenField ID="hdnIngredientId" runat="server" />
                                                <asp:HiddenField ID="hdnbulkproductId" runat="server" />
                                                <asp:HiddenField ID="hdnsumQuantityLtrKg" runat="server" />
                                            </div>
                                        </div>

                                        <div class="modal-footer">
                                            <asp:Button ID="btnbomdetailadd" CssClass="btn btn-primary" OnClientClick="return CheckQty();" OnClick="btnbomdetailadd_Click" ValidationGroup="g3" CausesValidation="true" runat="server" Text="Add" />
                                            <asp:Button ID="btnbomdetailcancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btnbomdetailcancel_Click" />
                                        </div>

                                        <div class="container col-12">
                                            <br />
                                            <br />
                                        </div>
                                        <div class="container col-12" style="overflow: auto">
                                            <asp:GridView ID="gvbomdetail" runat="server" ShowFooter="True" AutoGenerateColumns="false" class="table table-bordered table-striped gridview" ClientIDMode="Static">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="IngredientName" HeaderText="Ingredient Name" />
                                                    <asp:BoundField DataField="Formulation" HeaderText="Formulation" />
                                                    <asp:BoundField DataField="QuantityLtrKgDisplay" HeaderText="Quantity Ltr or Kg" />
                                                    <asp:BoundField DataField="RmPrice" HeaderText="Rate (Amount) KG" />
                                                     <asp:BoundField DataField="TransporationRate" HeaderText="Transporation" />
                                                    <asp:BoundField DataField="total" HeaderText="Amount" />

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:Button ID="btnIngredientDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("IngredientId") %>' OnClick="btnIngredientDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>

                                        </div>
                                        <div class="container col-12">
                                            <br />
                                            <br />
                                            <div class="row mb-3">

                                                <div class="col-md-4">
                                                    <div class="input-group-text mb-3 ">
                                                        <asp:CheckBox ID="chkformulation" onclick="chkformulationclick();" Checked="false" runat="server" />

                                                        &nbsp;&nbsp; 
                                                    <asp:Label class="input-group" ID="FormulationDrop" runat="server">Formulation</asp:Label>
                                                        <asp:DropDownList ID="drpformulation" onchange="drpformulationchange();" Enabled="false" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-group-text mb-2 ">
                                                        &nbsp;&nbsp; 
                                                    <asp:TextBox ID="txtbatchsizeinput" Enabled="false" CssClass="form-control" runat="server" type="number" placeholder="BatchSizeInput"></asp:TextBox>
                                                        <asp:Label ID="lblBOM_Batchsize" runat="server" Text="" Visible="true"></asp:Label>
                                                        <label for="BatchSizeInputtxt">  BatchSize</label>

                                                    </div>
                                                </div>

                                                <div class="col-md-4 ">
                                                    <div class="input-group-text  mb-3">
                                                        <asp:TextBox ID="txtfamount" Enabled="false" CssClass="form-control" type="text" runat="server"></asp:TextBox>

                                                        &nbsp;&nbsp;  
                                                    <label for="FormulationSelectedtxt">
                                                        Form. Charges  
                                                                        @
                                                                        <asp:Label ID="lblFormulationAddBuffer" runat="server" Visible="true"></asp:Label></label>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row mb-3">
                                                <div class="col-md-4">
                                                </div>
                                                <div class="col-md-4">
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-group-text  mb-3  border border-success border-3 " style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                        <asp:TextBox ID="txttotalamount" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                        &nbsp;&nbsp; 
                                                    <label for="TotalAmounttxt">Total Amount</label>


                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mb-3">

                                                <div class="col-md-4">
                                                    <div class="input-group-text  mb-3">
                                                        <asp:TextBox ID="txtsprg" onchange="BomDetailCalculation();" CssClass="form-control" runat="server" placeholder="SPGR (Spacigravity)"></asp:TextBox>
                                                        &nbsp;&nbsp;  
                                                    <label for="SPGRtxt">SPGR</label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-group-text  mb-3">
                                                        <asp:TextBox ID="txtTotalOutput_LTR" CssClass="form-control" Enabled="false" runat="server" type="number" placeholder="TotalOutput_LTR"></asp:TextBox>
                                                        &nbsp;&nbsp; 
                                                    <label for="TotalOutput_LTR">Output/Ltr</label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-group-text  mb-3 ">
                                                        <asp:TextBox ID="txtCost" AutoPostBack="true" Enabled="false" CssClass="form-control" runat="server" type="number"></asp:TextBox>
                                                        &nbsp;&nbsp; 
                                                    <label for="Costtxt">Cost/Ltr</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mb-3">

                                                <div class="col-md-4">
                                                    <div class="input-group-text  mb-3">
                                                        <asp:TextBox ID="txtformulationLost" onchange="BomDetailCalculation();" CssClass="form-control" runat="server" placeholder="FormulationLost"></asp:TextBox>
                                                        &nbsp;&nbsp;  
                                                    <label for="FormulationLosttxt">Form. Lost (%)</label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-group-text  mb-3">
                                                        <asp:TextBox ID="txtFormulationALostmount" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                        &nbsp;&nbsp; 
                                                    <label for="FormulationLostAmounttxt">Lost Amount</label>

                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-group-text  border border-success border-3" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)">
                                                        <asp:TextBox ID="txtfinalBulkcost" Enabled="false" CssClass="form-control" runat="server" placeholder="FinalCostBulk"></asp:TextBox>
                                                        &nbsp;&nbsp;  
                                                    <label for="FinalCostBulktxt"> Cost Bulk/Ltr</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="btnbomdetailfinalupdate" class="btn btn-success" OnClick="btnbomdetailfinalupdate_Click" runat="server" Text="Update" />

                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                    <%--  <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btncancel" />
                        <asp:AsyncPostBackTrigger ControlID="btnbomdetailadd" />
                        
                    </Triggers>--%>
                </asp:UpdatePanel>
                <!-- Modal -->

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
                        autocomplete();
                        bindgvdetailDataTable();
                        BomDetailCalculation();
                        var nm = sender._postBackSettings.asyncTarget;

                        if (nm.indexOf('btnEditMain') > 0 || nm.indexOf('btnDeleteMain') > 0 || nm.indexOf('btnadd') > 0 || nm.indexOf('btnupdate') > 0 || nm.indexOf('btncancel') > 0) {

                        }
                        else {

                            showhideTechnical('1');
                        }
                    }
                });
            };

            bindDataTable();
            function bindDataTable() {
                $('#<%= gvbulkrecipe.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvbulkrecipe.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": false,
                    "autoWidth": false,
                    //"responsive": true,
                    fixedHeader: {
                        footer: false
                    }
                });


            }


        });
        function drpformulationchange() {


            var drpformulationId = document.getElementById("<%=drpformulation.ClientID%>");
            var txtfamount = document.getElementById("<%=txtfamount.ClientID%>");
            var txtbatchsizeinput = document.getElementById("<%=txtbatchsizeinput.ClientID%>");
            var lblFormulationAddBuffer = document.getElementById("<%=lblFormulationAddBuffer.ClientID%>");

            $.ajax({
                url: 'WebService.asmx/GetFormulationCharges',
                data: { FormaulationId: drpformulationId.value },
                method: 'POST',
                success: function (r) {
                    var FinalcostLtr = r.all[0].textContent;
                    lblFormulationAddBuffer.innerHTML = FinalcostLtr;
                    txtfamount.value = FinalcostLtr * txtbatchsizeinput.value;
                    BomDetailCalculation();
                }
            });


        }
        function BomDetailCalculation() {


            var txtfamount = document.getElementById("<%=txtfamount.ClientID%>");
            var lblFormulationAddBuffer = document.getElementById("<%=lblFormulationAddBuffer.ClientID%>");
            var txtbatchsizeinput = document.getElementById("<%=txtbatchsizeinput.ClientID%>");
            var txttotalamount = document.getElementById("<%=txttotalamount.ClientID%>");
            var txtfamount = document.getElementById("<%=txtfamount.ClientID%>");
            var totalsum = document.getElementById("<%=hdntotalsum.ClientID%>");
            var txtTotalOutput_LTR = document.getElementById("<%=txtTotalOutput_LTR.ClientID%>");
            var txtsprg = document.getElementById("<%=txtsprg.ClientID%>");
            var txtCost = document.getElementById("<%=txtCost.ClientID%>");
            var txtFormulationALostmount = document.getElementById("<%=txtFormulationALostmount.ClientID%>");
            var txtformulationLost = document.getElementById("<%=txtformulationLost.ClientID%>");
            var txtfinalBulkcost = document.getElementById("<%=txtfinalBulkcost.ClientID%>");


            var val_txtsprg = parseFloat(txtsprg.value) || '1';
            var val_txtformulationLost = parseFloat(txtformulationLost.value) || '0.00';
            var val_txttotalamount = parseFloat(txttotalamount.value) || '0.00';
            var val_txtfamount = parseFloat(txtfamount.value) || '0.00';
            var val_totalsum = parseFloat(totalsum.value) || '0.00';

            txtsprg.value = val_txtsprg;
            txtformulationLost.value = val_txtformulationLost;

            txtfamount.value = Number(parseFloat(lblFormulationAddBuffer.innerHTML) * parseFloat(txtbatchsizeinput.value)).toFixed(2);
            txttotalamount.value = Number(parseFloat(val_txtfamount) + parseFloat(val_totalsum)).toFixed(2);
            txtTotalOutput_LTR.value = Number(parseFloat(txtbatchsizeinput.value) / parseFloat(val_txtsprg)).toFixed(2);
            txtCost.value = Number(parseFloat(txttotalamount.value) / parseFloat(txtTotalOutput_LTR.value)).toFixed(2);
            txtFormulationALostmount.value = Number(parseFloat(txtCost.value * parseFloat(val_txtformulationLost) / 100)).toFixed(2);
            txtfinalBulkcost.value = Number(parseFloat(txtCost.value) + parseFloat(txtFormulationALostmount.value)).toFixed(2);
        }

        function chkformulationclick() {

            var chkformulation = document.getElementById("<%=chkformulation.ClientID%>");

            if (chkformulation.checked) {

                document.getElementById("<%=drpformulation.ClientID%>").disabled = false;



            }
            else {

                document.getElementById("<%=drpformulation.ClientID%>").disabled = true;

                document.getElementById("<%=drpformulation.ClientID%>").value = 0;
                document.getElementById("<%=lblFormulationAddBuffer.ClientID%>").innerHTML = "0.00";
                document.getElementById("<%=txtfamount.ClientID%>").value = "0.00";

                BomDetailCalculation();
            }
        }

        function bindgvdetailDataTable() {

            $('#<%= gvbomdetail.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvbomdetail.ClientID %>').find("tr:first"))).DataTable({
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

            var gridv = document.getElementById("<%= gvbomdetail.ClientID %>");
            if (gridv != null) {
                var rw = gridv.rows.length;
                if (rw >= 3) {
                    gridv.rows[rw - 1].cells[2].innerHTML = "<b>Total:</b>";
                    gridv.rows[rw - 1].cells[3].innerHTML = "<b>" + document.getElementById("<%=hdnsumQuantityLtrKg.ClientID%>").value + "</b>";
                    gridv.rows[rw - 1].cells[6].innerHTML = "<b>" +document.getElementById("<%=hdntotalsum.ClientID%>").value+"</b>";
                }
            }


        }
            //chkSolvent(document.getElementById("<%=chkSolvent.ClientID%>"));




        function showhideTechnical(type) {

            if (type == '1') {

                $('#showInputTechnical').modal('show');

                ValidatorEnable(document.getElementById('<%= rf_quanity.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%= rf_formulation.ClientID %>'), true);



            }
            else {
                $('#showInputTechnical').modal('hide');

                $(".modal-backdrop").remove();
            }
        }

        function CheckChange() {

            if (document.getElementById("<%=ChkReqruiredFormulation.ClientID%>").checked) {

                document.getElementById("<%=txtRequiredFormulation.ClientID%>").style.display = "";
                document.getElementById("<%=txtInputKG.ClientID%>").style.display = "none";

                ValidatorEnable(document.getElementById('<%=rf_quanity.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%=rf_formulation.ClientID %>'), true);

                document.getElementById("<%=txtInputKG.ClientID%>").value = 0;

            }
            else {

                document.getElementById("<%=txtRequiredFormulation.ClientID%>").style.display = "none";
                document.getElementById("<%=txtInputKG.ClientID%>").style.display = "";

                ValidatorEnable(document.getElementById('<%=rf_quanity.ClientID %>'), true);
                ValidatorEnable(document.getElementById('<%=rf_formulation.ClientID %>'), false);

                document.getElementById("<%=txtRequiredFormulation.ClientID%>").value = 0;

            }
        }

        function chkSolvent() {



            var solventAmount = document.getElementById("<%=lblsolvant.ClientID%>");
            var hdnsumQuantityLtrKg = document.getElementById("<%=hdnsumQuantityLtrKg.ClientID%>");
            sumQuantityLtrKg = parseFloat(hdnsumQuantityLtrKg.value) || '0.00';

            if (document.getElementById("<%=chkSolvent.ClientID%>").checked) {


                solventAmount.value = Number(1000 - parseFloat(hdnsumQuantityLtrKg.value)).toFixed(2);

                document.getElementById("<%=txtRequiredFormulation.ClientID%>").style.display = "none";
                document.getElementById("<%=txtInputKG.ClientID%>").style.display = "none";

                document.getElementById("<%=txtRequiredFormulation.ClientID%>").value = "0";

                ValidatorEnable(document.getElementById('<%=rf_quanity.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%=rf_formulation.ClientID %>'), false);

                document.getElementById("dvformulation").style.display = "none";

            }

            else {
                document.getElementById("<%=ChkReqruiredFormulation.ClientID%>").checked = true;
                document.getElementById("<%=txtRequiredFormulation.ClientID%>").style.display = "";
                document.getElementById("<%=txtInputKG.ClientID%>").style.display = "none";

                ValidatorEnable(document.getElementById('<%=rf_quanity.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%=rf_formulation.ClientID %>'), true);

                document.getElementById("dvformulation").style.display = "";

                solventAmount.innerHTML = '0.00';
            }
            BomDetailCalculation();
        }

        function CheckQty() {
            var ret = true;

            var qua = document.getElementById("<%=hdnsumQuantityLtrKg.ClientID%>").value;
            var q = parseFloat(qua) || '0.00';
            var added = parseFloat(document.getElementById("<%=txtInputKG.ClientID%>").value) || '0.00';
            var total = parseFloat(q) + parseFloat(added);
            if (total > 1000) {
                alert('Quanity must be a less then or equal to 1000');
                ret = false;
            }
            else {

                ret = true;
            }

            return ret;
        }

        function autocomplete() {

            $('#<%=txtsearchRM.ClientID %>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'WebService.asmx/RMsearch',
                        data: "{ 'searchtxt': '" + request.term + "'}",
                        dataType: "json",
                        appendTo: "#showContainer",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('~~')[1],
                                    val: item.split('~~')[0]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $('#<%=hdnsearchRm.ClientID %>').val(i.item.val);
                },
                minLength: 1
            });
        }

    </script>

    <style>
        #showContainer {
            display: block;
            position: relative
        }
    </style>
</asp:Content>
