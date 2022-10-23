<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" Debug="true" ValidateRequest="false" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="GpBulkCost.aspx.cs" Inherits="Production_Costing_Software.GpBulkCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="contain">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Bulk Costing</b>
                            <marquee behavior="scroll" direction="left" style="color:red;font-size:16px">Before Share to Customer make sure You have to Select Terms And Condition....</marquee>

                </div>
                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>--%>
                <div class="card-body">
                    <div class="container col-12 grds">
                        <asp:GridView ID="gvbulkcost" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvbulkcost_RowDataBound" OnDataBound="gvbulkcost_DataBound" class="table table-bordered table-striped gridview ">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblid">1</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="___Report___">
                                    <ItemTemplate>

                                        <asp:Label ID="lblbpmid" Style="display: none" runat="server" Text='<%#  Eval("FkBulkProductId") %>' />
                                        <asp:HiddenField ID="hdntempbkid" runat="server" Value='<%#  Eval("FkBulkProductId") %>' />
                                        <button type="button" id="ReportActual" class="btn btn-success btn-sm align-content-end" onclick="btnActualReport(this)" data-bs-toggle="modal" data-bs-target="#exampleModal" runat="server">Actual Report</button>
                                        <button type="button" id="ReportEstimate" class="btn btn-primary btn-sm  align-content-end" onclick="btnEstimateReport(this)" data-bs-toggle="modal" data-bs-target="#exampleModal" runat="server">Estimate Report</button>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbstatus" runat="server" Enabled="false" Text='<%#  Eval("type") %>'></asp:Label>
                                        <asp:Label ID="lbliscalc" runat="server" Style="display: none" Text='<%#  Eval("iscalc") %>'></asp:Label>
                                        <asp:Label ID="lblBulkCostId" runat="server" Style="display: none" Text='<%#  Eval("FkBulkProductId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:BoundField DataField="productName" HeaderText="Technical Name" />--%>

                                <asp:TemplateField HeaderText="TechnicalName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblproductName" runat="server" Enabled="false" Text='<%#  Eval("productName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BulkCost (w/o Interest)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblbulkcost" runat="server" CssClass="form-control" Enabled="false" Text='<%#  Eval("bulkcost") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Packing Size">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPackingsize" runat="server" onchange="calculatePricelist(id)" CssClass="form-control" Text='<%#  Eval("Packingsize") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="____PackingType____">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drppmrm" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblpackingtype" Text='<%#  Eval("PMRMPriceId") %>' Style="display: none" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PM Cost">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtUnitPerKg" Enabled="false" runat="server" Text='<%#  Eval("UnitPerKg") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PM Cost / KG">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPMCostPerKg" Enabled="false" runat="server" Text='<%#  Eval("PMCostPerKg") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Final BulkCost With PM/Kg Ltr">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFinalBulkWpmKgLtr" runat="server" Enabled="false" Text='<%#  Eval("FinalBulkCostWPMKgLtr") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% Of Profit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtProfitPer" onchange="calculatePricelist(id)" runat="server" Text='<%#  Eval("ProfitPer") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Profit Amt">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtProfitAmt" runat="server" Enabled="false" Text='<%#  Eval("ProfitAmt") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Suggested Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSuggPrice" runat="server" Enabled="false" Text='<%#  Eval("SuggestPrice") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Final Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFinalPrice" onchange="calculatePricelist(id)" runat="server" Text='<%#  Eval("FinalPrice") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Additional Discount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAddDiscount" onchange="calculatePricelist(id)" runat="server" Text='<%#  Eval("AddDiscount") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Net Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNetPrice" runat="server" Enabled="false" Text='<%#  Eval("NetPrice") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Net Profit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNetProfit" runat="server" Enabled="false" Text='<%#  Eval("NetProfit") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Terms">
                                    <ItemTemplate>
                                        <%--<asp:Button ID="btnTerms" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" OnClientClick="GetTerms();" Text="T & C" class="btn btn-secondary btn-sm" />--%>
                                        <button type="button" id="TermId" runat="server" onclick="GetTerms(this);" class="btn btn-secondary btn-sm">
                                            T & C
                                        </button>
                                        <%--<asp:Button ID="btnTerms" runat="server" Text="T & C" OnClientClick="return GetTerms(this)" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <%--<asp:Button ID="btnShare" Text="ShareTo" runat="server" OnClick="btnShare_Click" CssClass="btn btn-sm btn-success" />--%>
                                        <button type="button" id="btnShareId" runat="server" onclick="btnShare(this);" class="btn btn-info btn-sm">
                                            Share
                                        </button>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <button type="button" text="Save" id="btnSaveId" runat="server" onclick="btnSave(this);" onclientclick="return confirm('Are you sure want to save this data?');" class="btn btn-sm btn-primary">Save</button>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <button type="button" text="Save" id="btnHistoryId" runat="server" onclick="btnHistroy(this);" onclientclick="return confirm('Are you sure want to save this data?');" class="btn btn-sm btn-success">History</button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="hdnpmrmid" runat="server" />
                        <asp:HiddenField ID="hdnUnitPerKg" runat="server" />
                        <asp:HiddenField ID="hdnindex" runat="server" />
                        <asp:HiddenField ID="hdnCompanyId" runat="server" />
                        <asp:HiddenField ID="hdnbpmid" runat="server" />
                        <asp:HiddenField ID="hdnpackingtype" runat="server" />
                        <asp:HiddenField ID="hdnpackingsize" runat="server" />
                        <asp:HiddenField ID="hdnfinalprice" runat="server" />
                        <asp:HiddenField ID="hdnprofitper" runat="server" />
                        <asp:HiddenField ID="hdnadditionalDiscount" runat="server" />
                        <asp:HiddenField ID="hdnhtml" runat="server" />
                        <asp:HiddenField ID="hdnBulkproductname" runat="server" />
                        <asp:HiddenField ID="hdnpackingname" runat="server" />
                        <asp:HiddenField ID="hdnDateTime" runat="server" />
                        <asp:HiddenField ID="hdnTermsCondition" runat="server" />
                        <asp:HiddenField ID="hdnTermsCondId" runat="server" />
                        <asp:HiddenField ID="hdinner" runat="server" />

                    </div>
                    <%--Reports--%>
                    <div class="modal fade" id="showdetail" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content ">
                                <div class="modal-header">
                                    <h3 class="modal-title text-success" id="exampleModalLabel" runat="server">
                                        <asp:Label ID="lblBulkProductNameReport" runat="server"></asp:Label>
                                        <%--<asp:HiddenField runat="server" ID="hdnbulkProductName" />--%>
                                    </h3>
                                    <button type="button" data-dismiss="modal" onclick="showhidemodel('0');" class="btn-close btn-secondary">Close</button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="container col-12">
                                            <div id="dvdetailcontent" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="ModalShare" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="ShareModalLabel">Share To :</h5>
                                    <button type="button" data-dismiss="modal" onclick="showhidemodel('0');" class="btn-close btn-secondary"></button>
                                </div>
                                <div class="modal-body">
                                    <div class="col-10">
                                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="text-danger" runat="server" ValidationGroup="g1" />
                                    </div>
                                    <div class="input-group mb-3">
                                        <span class="input-group-text" id="basic-addon1">Name</span>
                                        <asp:TextBox ID="txtShareName" runat="server" CssClass="form-control" aria-label="ShareName" aria-describedby="basic-addon1" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShareName" Display="None" ValidationGroup="g1" ErrorMessage="Enter Name" />

                                    </div>
                                    <div class="input-group mb-3">
                                        <span class="input-group-text" id="basic-addon2">Mobile</span>
                                        <asp:TextBox ID="txtmobile" TextMode="Number" runat="server" CssClass="form-control" aria-label="Mobile" aria-describedby="basic-addon2" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShareName" Display="None" ValidationGroup="g1" ErrorMessage="Enter Name" />

                                    </div>

                                    <div class="container col-12">
                                        <div id="dvdetailcontentreport" runat="server">
                                        </div>
                                    </div>

                                </div>
                                <div class="modal-footer">
                                    <button type="button" onclick="btnShareWhatsapp(this);" id="rf_txtmobile" controltovalidate="txtmobile" display="None" validationgroup="g1" errormessage="Enter Name" class="btn btn-success">Share </button>

                                    <%--<button type="button" onclick="btnAddShare(this);" id="rf_txtmobile2" controltovalidate="txtmobile" display="None" validationgroup="g1" errormessage="Enter Name" class="btn btn-primary">Save </button>--%>

                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    <%--<button type="button" class="btn btn-primary" validationgroup="g1">Save changes</button>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--TermsConditionModal--%>
                    <div class="modal fade" id="TermsConditionModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="TermsConditionModalLabel">Terms & Condition :
                            
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
                                        <div class="col-md-10">
                                            <div class="input-group mb-3">
                                                <div class="col-md-10" style="border: 1px; height: 500px">
                                                    <div class="input-group">
                                                        <asp:ListBox ID="TermsConditionListbox" runat="server" SelectionMode="Multiple" class="form-control"></asp:ListBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-md-1">
                                                <button type="button" id="btnAdd" runat="server" onclick="btnAddTerms(this);" class="btn btn-success">AddTerms</button>
                                            </div>
                                            <%--<asp:Button ID="AddTerms" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="ChkTermsSubmit_Click" />--%>
                                        </div>
                                    </div>

                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <%--OnlyShareTable--%>
                    <div class="modal fade" id="OnlyShareTableModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="OnlyShareTableLabel">Table :
                            
                                    </h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div id="dvdetailcontentOnlyTable" runat="server">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <%--History--%>
                    <div class="modal fade" id="HistoryModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="HistoryModalLabel">History :
                            
                                    </h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div id="dvdetailcontentHistory" runat="server" class="col-sm-12">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--  </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        $(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {

                    if (sender._postBackSettings.panelsToUpdate != null) {
                        var nm = sender._postBackSettings.asyncTarget;
                        //if (nm.indexOf('btnupdate') > 0) {
                        //    alert('Other Company Price list Updated Successfully');
                        //}
                        //else {                            
                        //    showhidemodel('1');
                        //}

                        bindDataTable();
                    }
                });
            };

            bindDataTable();
            function bindDataTable() {
                $('#<%= gvbulkcost.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvbulkcost.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": false,
                    "autoWidth": true,
                    //"responsive": true,
                    fixedHeader: {
                        header: true,
                        footer: false
                    },
                    "scrollX": false,
                    "pageLength": 50
                });

            }
        });
        function showhidemodel(type) {

            if (type == '1') {

                $('#showdetail').modal('show');
            }
            else {
                $('#showdetail').modal('hide');
            }

        }


        function calculatePricelist(id) {

            var grd = '<%= gvbulkcost.ClientID %>';
            document.getElementById(id).value = Number(document.getElementById(id).value).toFixed(2).toString();

            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var isEstimate = document.getElementById(grd + "_lbliscalc_" + index);
            var lblbpmid = document.getElementById(grd + "_lblbpmid_" + index);
            var BulkCostPerLtr = document.getElementById(grd + "_lblbulkcost_" + index);
            var Packingsize = document.getElementById(grd + "_txtPackingsize_" + index);
            var PmCostUnit = document.getElementById(grd + "_txtUnitPerKg_" + index);
            var PMCostPerKg = document.getElementById(grd + "_txtPMCostPerKg_" + index);
            var FinalBulkWpmKgLtr = document.getElementById(grd + "_txtFinalBulkWpmKgLtr_" + index);
            var ProfitPer = document.getElementById(grd + "_txtProfitPer_" + index);
            var ProfitAmt = document.getElementById(grd + "_txtProfitAmt_" + index);
            var SuggPrice = document.getElementById(grd + "_txtSuggPrice_" + index);
            var FinalPrice = document.getElementById(grd + "_txtFinalPrice_" + index);
            var AddDiscount = document.getElementById(grd + "_txtAddDiscount_" + index);
            var NetPrice = document.getElementById(grd + "_txtNetPrice_" + index);
            var NetProfit = document.getElementById(grd + "_txtNetProfit_" + index);
            var Packingtype = document.getElementById(grd + "_lblpackingtype_" + index);

            if (document.getElementById('<%=hdnpmrmid.ClientID %>').value != 0) {
                document.getElementById(grd + "_drppmrm_" + index).value = document.getElementById('<%=hdnpmrmid.ClientID %>').value;
            }
            else {
                document.getElementById(grd + "_drppmrm_" + index).value = Packingtype.innerHTML;

            }

            PMCostPerKg.value = Number(PmCostUnit.value / Packingsize.value).toFixed(2);
            FinalBulkWpmKgLtr.value = Number(parseFloat(BulkCostPerLtr.value) + parseFloat(PMCostPerKg.value)).toFixed(2);
            if (ProfitPer.value != 0) {
                ProfitAmt.value = Number(parseFloat((FinalBulkWpmKgLtr.value * ProfitPer.value) / 100)).toFixed(2);
            }
            SuggPrice.value = Number(parseFloat(FinalBulkWpmKgLtr.value) + (parseFloat(ProfitAmt.value))).toFixed(2);

            NetPrice.value = Number(parseFloat((FinalPrice.value - AddDiscount.value))).toFixed(2);
            NetProfit.value = Number(parseFloat((NetPrice.value - FinalBulkWpmKgLtr.value))).toFixed(2);


            if (isEstimate.innerHTML == '1') {

                var newid = parseInt(index) + 1;

                var BulkCostPerLtr_Estimate = document.getElementById(grd + "_lblbulkcost_" + newid);
                //var Packingsize_Estimate = document.getElementById(grd + "_txtPackingsize_" + newid);
                var PmCostUnit_Estimate = document.getElementById(grd + "_txtUnitPerKg_" + newid);
                var PMCostPerKg_Estimate;
                var FinalBulkWpmKgLtr_Estimate = document.getElementById(grd + "_txtFinalBulkWpmKgLtr_" + newid);
                var ProfitPer_Estimate;
                var ProfitAmt_Estimate = document.getElementById(grd + "_txtProfitAmt_" + newid);


                var SuggPrice_Estimate = document.getElementById(grd + "_txtSuggPrice_" + newid);
                var FinalPrice_Estimate;
                var AddDiscount_Estimate;
                var NetPrice_Estimate;
                var NetProfit_Estimate = document.getElementById(grd + "_txtNetProfit_" + newid);





                PMCostPerKg_Estimate = Number(PmCostUnit.value / Packingsize.value).toFixed(2);

                FinalBulkWpmKgLtr_Estimate.value = Number(parseFloat(BulkCostPerLtr_Estimate.value) + parseFloat(PMCostPerKg.value)).toFixed(2);

                ProfitPer_Estimate = ProfitPer.value;

                ProfitAmt_Estimate.value = Number(parseFloat((FinalBulkWpmKgLtr_Estimate.value * ProfitPer_Estimate) / 100)).toFixed(2);
                SuggPrice_Estimate.value = Number(parseFloat(FinalBulkWpmKgLtr_Estimate.value) + (parseFloat(ProfitAmt_Estimate.value))).toFixed(2);

                FinalPrice_Estimate = FinalPrice.value;
                AddDiscount_Estimate = AddDiscount.value;

                NetPrice_Estimate = Number(parseFloat((FinalPrice_Estimate - AddDiscount_Estimate))).toFixed(2);

                NetProfit_Estimate.value = Number(parseFloat((NetPrice_Estimate - FinalBulkWpmKgLtr_Estimate.value))).toFixed(2);

            }

        }

        function drpchangeval(id) {
            var grd = '<%= gvbulkcost.ClientID %>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var hdnpmrmid = document.getElementById('<%=hdnpmrmid.ClientID %>');
            var UnitPerKg = document.getElementById('<%=hdnUnitPerKg.ClientID %>');
            hdnpmrmid.value = document.getElementById(id).value;
            $.ajax({
                url: 'WebService.asmx/GetPriceByPMRMCatId',
                data: { pmrmid: hdnpmrmid.value },
                dataType: "xml",
                method: 'POST',
                success: function (r) {

                    var Unit = r.all[0].textContent;
                    var val1 = Unit.split('~')[0];

                    document.getElementById(grd + "_txtUnitPerKg_" + index).value = val1;

                    calculatePricelist(id);
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });

        }

        function GetTerms(button) {

            id = button.id;


            var grd = '<%= gvbulkcost.ClientID %>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);
            var BulkId = document.getElementById(grd + "_lblbpmid_" + index);
            var hdnbpmid = document.getElementById("<%=hdnbpmid.ClientID%>");
            hdnbpmid.value = BulkId.innerHTML;
            //var message = button.id;
            //Display the data using JavaScript Alert Message Box.

            $.ajax({
                url: 'WebService.asmx/GetTermsCondition',
                data: { FkBulkProductId: hdnbpmid.value },
                dataType: "xml",
                method: 'POST',
                success: function (Result) {
                    $("#<%=TermsConditionListbox.ClientID %>").empty();
                    for (var i = 0; i < Result.all[0].children.length; i++) {
                        $("#<%=TermsConditionListbox.ClientID %>").append($("<option></option>").val(Result.all[0].children[i].children[0].innerHTML).html(Result.all[0].children[i].children[1].innerHTML));
                        if (id > 0) {
                            $("#<%=TermsConditionListbox.ClientID %> option[value=" + id + "]").attr('selected', 'selected');
                            document.getElementById("<%=TermsConditionListbox.ClientID %>").value = id;
                        }
                    }
                    //alert(id);
                    $('#TermsConditionModal').modal('show');
                },
            });
        }


        function btnShare(button) {
            id = button.id;
            var grd = '<%= gvbulkcost.ClientID %>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var FkBulkProductId = document.getElementById(grd + "_lblbpmid_" + index);
            var PackingType = document.getElementById(grd + "_drppmrm_" + index);

            var Packingsize = document.getElementById(grd + "_txtPackingsize_" + index);

            var ProfitPer = document.getElementById(grd + "_txtProfitPer_" + index);

            var FinalPrice = document.getElementById(grd + "_txtFinalPrice_" + index);

            var AddDiscount = document.getElementById(grd + "_txtAddDiscount_" + index);

            var txtShareName = document.getElementById('<%=txtShareName.ClientID %>');

            var hdnbpmid = document.getElementById('<%=hdnbpmid.ClientID %>');
            var hdnpackingtype = document.getElementById('<%=hdnpackingtype.ClientID %>');
            var hdnprofitper = document.getElementById('<%=hdnprofitper.ClientID %>');
            var hdnpackingsize = document.getElementById('<%=hdnpackingsize.ClientID %>');
            var hdnfinalprice = document.getElementById('<%=hdnfinalprice.ClientID %>');
            var hdnadditionalDiscount = document.getElementById('<%=hdnadditionalDiscount.ClientID %>');
            var hdnbpmid = document.getElementById('<%=hdnbpmid.ClientID %>');
            var htmlTable = document.getElementById('<%=dvdetailcontentreport.ClientID %>');
            var hdnhtml = document.getElementById('<%=hdnhtml.ClientID %>');
            var hdnBulkproductname = document.getElementById('<%=hdnBulkproductname.ClientID %>');
            var hdnDateTime = document.getElementById('<%=hdnDateTime.ClientID %>');
            var hdnpackingname = document.getElementById('<%=hdnpackingname.ClientID %>');
            var hdnTermsCondition = document.getElementById('<%=hdnTermsCondition.ClientID %>');
            var hdnTermsCondId = document.getElementById('<%=hdnTermsCondId.ClientID %>');



            hdnbpmid.value = FkBulkProductId.innerHTML;
            hdnpackingtype.value = PackingType.value;
            hdnprofitper.value = ProfitPer.value;
            hdnpackingsize.value = Packingsize.value;
            hdnfinalprice.value = FinalPrice.value;
            hdnadditionalDiscount.value = AddDiscount.value;

            txtShareName.value = "";

            $.ajax({
                url: 'WebService.asmx/BulkCostShareReport',
                data: {
                    FkBulkProductId: hdnbpmid.value, PackingSize: Packingsize.value, PackingType: PackingType.value, FinalPrice: FinalPrice.value, TermsCondId: hdnTermsCondId.value
                },
                dataType: "xml",
                method: 'POST',
                success: function (r) {
                    var Unit = r.all[0].textContent;
                    var val1 = Unit.split('~')[0];
                    hdnBulkproductname.value = Unit.split('~')[1];
                    hdnpackingname.value = Unit.split('~')[2];
                    hdnpackingsize.value = Unit.split('~')[3];
                    FinalPrice.value = Unit.split('~')[4];
                    hdnDateTime.value = Unit.split('~')[5];
                    hdnTermsCondition.value = Unit.split('~')[6];
                    if (Unit == 0) {
                        alert('Please Enter Name And Mobile !')

                    }
                    else {
                        htmlTable.innerHTML = val1;
                        hdnhtml.value = htmlTable.innerHTML
                        txtShareName.value = "";
                        $('#ModalShare').modal('show');

                    }


                },
                error: function error(result) {
                    //    alert(result.status + ' : ' + result.statusText);
                    alert('Please Enter Data!')
                }
            });


        }
        function btnSave(button) {
            id = button.id;
            var grd = '<%= gvbulkcost.ClientID %>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var FkBulkProductId = document.getElementById(grd + "_lblbpmid_" + index);
            var PackingType = document.getElementById(grd + "_drppmrm_" + index);

            var Packingsize = document.getElementById(grd + "_txtPackingsize_" + index);

            var ProfitPer = document.getElementById(grd + "_txtProfitPer_" + index);

            var FinalPrice = document.getElementById(grd + "_txtFinalPrice_" + index);

            var AddDiscount = document.getElementById(grd + "_txtAddDiscount_" + index);

            var txtShareName = document.getElementById('<%=txtShareName.ClientID %>');

            var hdnbpmid = document.getElementById('<%=hdnbpmid.ClientID %>');
            var hdnpackingtype = document.getElementById('<%=hdnpackingtype.ClientID %>');
            var hdnprofitper = document.getElementById('<%=hdnprofitper.ClientID %>');
            var hdnpackingsize = document.getElementById('<%=hdnpackingsize.ClientID %>');
            var hdnfinalprice = document.getElementById('<%=hdnfinalprice.ClientID %>');
            var hdnadditionalDiscount = document.getElementById('<%=hdnadditionalDiscount.ClientID %>');
            var hdnpackingname = document.getElementById('<%=hdnpackingname.ClientID %>');

            var hdnpmrmid = document.getElementById('<%=hdnpmrmid.ClientID %>');
            var hdnCompanyId = document.getElementById('<%=hdnCompanyId.ClientID %>');


            hdnbpmid.value = FkBulkProductId.innerHTML;


            hdnpackingtype.value = hdnpmrmid.value;
            hdnprofitper.value = ProfitPer.value;
            hdnpackingsize.value = Packingsize.value;
            hdnfinalprice.value = FinalPrice.value;
            hdnadditionalDiscount.value = AddDiscount.value;


            txtShareName.value = "";


            $.ajax({
                url: 'WebService.asmx/SaveBulkCost',
                data: { FkBulkProductId: hdnbpmid.value, FkCompanyId: hdnCompanyId.value, PackingType: hdnpackingtype.value, Packingsize: hdnpackingsize.value, ProfitPer: hdnprofitper.value, FinalPrice: hdnfinalprice.value, AddDiscount: hdnadditionalDiscount.value },
                dataType: "xml",
                method: 'POST',
                success: function (r) {
                    var Unit = r.all[0].textContent;
                    if (Unit == 0) {

                        alert(' please Enter Data !')
                    }
                    else {

                        alert(' Save Successfully !')

                        //txtShareName.value = "";



                    }


                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });


        }

        function btnShareWhatsapp(button) {
            id = button.id;
            var grd = '<%= gvbulkcost.ClientID %>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);
            var txtShareName = document.getElementById('<%=txtShareName.ClientID %>');
            var txtmobile = document.getElementById('<%=txtmobile.ClientID %>');
            var hdnhtml = document.getElementById('<%=hdnhtml.ClientID %>');



            var FkBulkProductId = document.getElementById(grd + "_lblbpmid_" + index);
            var PackingType = document.getElementById(grd + "_drppmrm_" + index);

            var Packingsize = document.getElementById(grd + "_txtPackingsize_" + index);

            var ProfitPer = document.getElementById(grd + "_txtProfitPer_" + index);

            var FinalPrice = document.getElementById(grd + "_txtFinalPrice_" + index);

            var AddDiscount = document.getElementById(grd + "_txtAddDiscount_" + index);



            //Save
      <%--      id = button.id;
            var grd = '<%= gvbulkcost.ClientID %>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);--%>
            var txtShareName = document.getElementById('<%=txtShareName.ClientID %>');
            var txtmobile = document.getElementById('<%=txtmobile.ClientID %>');
            var hdnbpmid = document.getElementById('<%=hdnbpmid.ClientID %>');
            var hdnCompanyId = document.getElementById('<%=hdnCompanyId.ClientID %>');
            var hdnadditionalDiscount = document.getElementById('<%=hdnadditionalDiscount.ClientID %>');
            var hdnfinalprice = document.getElementById('<%=hdnfinalprice.ClientID %>');
            var hdnpackingsize = document.getElementById('<%=hdnpackingsize.ClientID %>');
            var hdnpackingtype = document.getElementById('<%=hdnpackingtype.ClientID %>');
            var hdnprofitper = document.getElementById('<%=hdnprofitper.ClientID %>');
            var hdnfinalprice = document.getElementById('<%=hdnfinalprice.ClientID %>');
            var hdnhtml = document.getElementById('<%=hdnhtml.ClientID %>');
            var hdnBulkproductname = document.getElementById('<%=hdnBulkproductname.ClientID %>');
            var hdnDateTime = document.getElementById('<%=hdnDateTime.ClientID %>');
            var hdnpackingname = document.getElementById('<%=hdnpackingname.ClientID %>');
            var hdnTermsCondition = document.getElementById('<%=hdnTermsCondition.ClientID %>');
            var hdnTermsCondId = document.getElementById('<%=hdnTermsCondId.ClientID %>');


            $.ajax({
                url: 'WebService.asmx/AddShareName',
                data: { FkBulkProductId: hdnbpmid.value, ShareName: txtShareName.value, Mobile: txtmobile.value, FkCompanyId: hdnCompanyId.value, PackingType: hdnpackingtype.value, Packingsize: hdnpackingsize.value, ProfitPer: hdnprofitper.value, FinalPrice: hdnfinalprice.value, AddDiscount: hdnadditionalDiscount.value, TermsCondId: hdnTermsCondId.value },
                dataType: "xml",
                method: 'POST',
                success: function (r) {
                    var Unit = r.all[0].textContent;
                    if (Unit == 0) {
                        alert('Please Enter Name And Mobile !')
                        //$('#ModalShare').modal('show');

                    }
                    else {

                        alert(' Shared Successfully !')

                        txtShareName.value = "";
                        $('#ModalShare').modal('hide');


                    }


                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });

            $('#ModalShare').modal('hide');

            //redirect to WhatsApp
            var url = "https://api.whatsapp.com/send?phone=91" + txtmobile.value + "&app_absent=0&text=" + "Report To " + txtShareName.value + "%0a%0a%0aDate :  " + hdnDateTime.value + "%0a%0aTechnical Name : " + hdnBulkproductname.value + "%0a%0aPacking Type : " + hdnpackingname.value + "  Kg%0a%0aPacking Size : " + hdnpackingsize.value + "%0a%0aPrice / L or Kg : " + hdnfinalprice.value + "%0a%0aTerms and Condition : %0a" + hdnTermsCondition.value
            window.open(url, '_blank').focus();




        }


        function btnHistroy(button) {
            id = button.id;
            var grd = '<%= gvbulkcost.ClientID %>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);
            var FkBulkProductId = document.getElementById(grd + "_lblbpmid_" + index);

            var txtShareName = document.getElementById('<%=txtShareName.ClientID %>');
            var txtmobile = document.getElementById('<%=txtmobile.ClientID %>');
            var hdnbpmid = document.getElementById('<%=hdnbpmid.ClientID %>');
            var hdnCompanyId = document.getElementById('<%=hdnCompanyId.ClientID %>');
            var htmlTable = document.getElementById('<%=dvdetailcontentHistory.ClientID %>');

            var hdnhtml = document.getElementById('<%=hdnhtml.ClientID %>');

            hdnbpmid.value = FkBulkProductId.innerHTML


            $.ajax({
                url: 'WebService.asmx/ShowHistory',
                data: { FkBulkProductId: hdnbpmid.value },
                dataType: "xml",
                method: 'POST',
                success: function (r) {
                    var Unit = r.all[0].textContent;
                    var val1 = Unit.split('~')[0];
                    if (Unit == 0) {
                        alert('No History !')

                    }
                    else {
                        htmlTable.innerHTML = val1;
                        hdnhtml.value = htmlTable.innerHTML
                        txtShareName.value = "";
                        $('#HistoryModal').modal('show');

                    }
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });

        }

        function btnAddTerms(button) {
            id = button.id;
            var grd = '<%= gvbulkcost.ClientID %>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var FkBulkProductId = document.getElementById(grd + "_lblbpmid_" + index);
            var txtShareName = document.getElementById('<%=txtShareName.ClientID %>');
            var hdnCompanyId = document.getElementById('<%=hdnCompanyId.ClientID %>').value;
            var x = document.getElementById('<%=TermsConditionListbox.ClientID %>');
            var hdnbpmid = document.getElementById('<%=hdnbpmid.ClientID %>');
            var hdnTermsCondId = document.getElementById('<%=hdnTermsCondId.ClientID %>');

            var TermId = "";
            for (var i = 0; i < x.options.length; i++) {
                if (x.options[i].selected) {
                    TermId = TermId + x.options[i].value + ","
                }
            }
            var lastIndex = TermId.lastIndexOf(",");
            TermId = TermId.substring(0, lastIndex);
            hdnTermsCondId.value = TermId;
            if (hdnTermsCondId.value!=0) {
                alert('Terms & Condition Selected')

            }
            else {
                alert('Select Terms & Condition !')

            }

        }

        function btnActualReport(button) {

            id = button.id;
            var grd = '<%= gvbulkcost.ClientID %>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var FkBulkProductId = document.getElementById(grd + "_lblbpmid_" + index);
            var lblproductName = document.getElementById(grd + "_lblproductName_" + index);

<%--            var hdnIsEstimate = document.getElementById('<%=hdnIsEstimate.ClientID %>').value;--%>
            var dvdetailcontent = document.getElementById('<%=dvdetailcontent.ClientID %>');
            var BulkProductNameReport = lblproductName.innerHTML;
            $.ajax({
                url: 'WebService.asmx/GetReportbyBPMId',
                data: { productid: FkBulkProductId.innerHTML, IsEstimate: 0 },
                dataType: "xml",
                method: 'POST',
                success: function (r) {
                    var Actual = BulkProductNameReport + " [Actual]"
                    document.getElementById('<%=lblBulkProductNameReport.ClientID %>').innerHTML = Actual;

                    document.getElementById("<%=dvdetailcontent.ClientID%>").innerHTML = r.all[0].textContent;
                    $('#showdetail').modal("show");
                    //dvdetailcontent = r.all[0].textContent;
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }

        function btnEstimateReport(button) {

            id = button.id;
            var grd = '<%= gvbulkcost.ClientID %>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);
            //for Estimate
            var IndexEst = index - 1;
            debugger;
            var FkBulkProductId = document.getElementById(grd + "_lblbpmid_" + IndexEst);
            var lblproductName = document.getElementById(grd + "_lblproductName_" + IndexEst);
            var dvdetailcontent = document.getElementById('<%=dvdetailcontent.ClientID %>');
            var BulkProductNameReport = lblproductName.innerHTML;
            $.ajax({
                url: 'WebService.asmx/GetReportbyBPMId',
                data: { productid: FkBulkProductId.innerHTML, IsEstimate: 1 },
                dataType: "xml",
                method: 'POST',
                success: function (r) {
                    var Estimate = BulkProductNameReport + " [Estimate]"
                    document.getElementById('<%=lblBulkProductNameReport.ClientID %>').innerHTML = Estimate;
                    document.getElementById("<%=dvdetailcontent.ClientID%>").innerHTML = r.all[0].textContent;
                    $('#showdetail').modal("show");
                    //dvdetailcontent = r.all[0].textContent;
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }

        function OnlyShowTable() {

        }
        function viewreport(id, bulk) {


            var strURL = "";
            strURL = document.URL.substring(15, document.URL.length - document.URL.indexOf("GpBulkCost.aspx"));
            //var bpmid = id.split('')[0];
            //var No = id.split('')[1];
           
            strURL += "/SharedBulkCostReport.aspx?SharedId=" + id + "&bpmno=" + bulk +"" ;


            window.open(strURL, '_blank').focus();


        }

    </script>
    <style>
        .grds input[type=text] {
            width: 90px !important;
            margin-top: 5px;
        }

        .grds input[type=number] {
            width: 90px !important;
            margin-top: 5px;
        }

        .textboxdiv span {
            font-size: 11px;
        }

        .table-striped tbody tr:nth-of-type(odd) {
            background-color: aliceblue !important;
        }

        .table-striped tbody tr:nth-of-type(even) {
            background-color: antiquewhite !important;
        }

    </style>

</asp:Content>
