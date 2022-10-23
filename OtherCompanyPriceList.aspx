<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" AutoEventWireup="true" CodeBehind="OtherCompanyPriceList.aspx.cs" Inherits="Production_Costing_Software.OtherCompanyPriceList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="contain">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Other Company Price List</b>
                </div>
                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>--%>
                <div class="card-body">
                    <div class="container col-12 grds">
                        <asp:GridView ID="gvothercompany" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvothercompany_RowDataBound" OnDataBound="gvothercompany_DataBound" class="table table-bordered table-striped gridview ">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblid">1</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Report">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdntempbkid" runat="server" Value='<%#  Eval("fkbulkproductid") %>' />
                                        <asp:Button ID="btnReport" Text="Actual Report" runat="server" CommandArgument='<%#  Eval("fkbulkproductid") %>' OnClick="btnReport_Click" CssClass="btn btn-sm btn-secondary" />
                                        <%--<asp:Button ID="btnReportEstimate" Style='<%# Eval("type").ToString() == "Estimate" ? "display:block":"display:none" %>' Text="Estimate Report" runat="server" CommandArgument='<%#  Eval("fkbulkproductid") %>' OnClick="btnReportEstimate_Click" CssClass="btn btn-sm btn-secondary" />--%>
                                        <asp:Button ID="btnReportEstimate" Text="Estimate Report" runat="server" CommandArgument='<%#  Eval("fkbulkproductid") %>' OnClick="btnReportEstimate_Click" CssClass="btn btn-sm btn-success" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="productname" HeaderText="BulkProduct" />
                                <asp:BoundField DataField="TradeName" HeaderText="Trade" />
                                <asp:TemplateField HeaderText="Packing">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#  Eval("packingsize") +"-"+ Eval("packing") + (Eval("PackingCategoryName").ToString()!="" ? "<br>("+Eval("PackingCategoryName")+")" : "") %>'></asp:Label><br />
                                        <asp:Label runat="server" Text='<%# "["+ Eval("shippersize")+"]" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbstatus" runat="server" Enabled="false" Text='<%#  Eval("type") %>'></asp:Label>
                                        <asp:Label ID="lbliscalc" runat="server" Style="display: none" Text='<%#  Eval("iscalc") %>'></asp:Label>
                                        <asp:Label ID="lbothercompanyId" runat="server" Style="display: none" Text='<%#  Eval("OtherComapnyPriceListId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BulkCost">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblbulkcost" runat="server" CssClass="form-control" Enabled="false" Text='<%#  Eval("bulkcost") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Interest">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbulkinterest" onchange="calculatePricelist(this.id);" TextMode="Number" step="any" Text='<%#  Eval("interest") %>' Enabled='<%# Eval("type").ToString() == "Estimate" ? false: true %>' runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:TextBox ID="txtinterestamt" runat="server" Enabled="false" CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Int.Amt">
                                            <ItemTemplate>
                                                 
                                            </ItemTemplate>
                                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="TotalBulk / BulkUnit ">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txttotalbulkcost" runat="server" Enabled="false" CssClass="form-control" />
                                        <asp:TextBox ID="txtbulkcostunit" runat="server" Enabled="false" CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--   <asp:TemplateField HeaderText="Bulk Unit">
                                            <ItemTemplate>
                                                
                                            </ItemTemplate>
                                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Total PM [PM + Buffer]" ControlStyle-Width="200px">
                                    <ItemTemplate>

                                        <asp:TextBox ID="txtpm" runat="server" CssClass="form-control" Enabled="false" Text='<%#  Eval("costperunit") %>' />
                                        <asp:TextBox ID="txtaddpm" onchange="calculatePricelist(this.id);" TextMode="Number" step="any" runat="server" CssClass="form-control" Enabled='<%# Eval("type").ToString() == "Estimate" ? false: true %>' Text='<%#  Eval("AdditionalBufferPM") %>'></asp:TextBox>
                                        <asp:TextBox ID="txtpmtotal" runat="server" Enabled="false" CssClass="form-control" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--          <asp:TemplateField HeaderText="Add Buffer">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Total Labour [Labour + Buffer]">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtlabour" runat="server" Enabled="false" Text='<%#  Eval("labourcost") %>' CssClass="form-control" />
                                        <asp:TextBox ID="txtaddlabour" runat="server" onchange="calculatePricelist(this.id);" TextMode="Number" step="any" Enabled='<%# Eval("type").ToString() == "Estimate" ? false: true %>' Text='<%#  Eval("AdditionalBufferLabour") %>' CssClass="form-control"></asp:TextBox>
                                        <asp:TextBox ID="txttotallabour" runat="server" CssClass="form-control" Enabled="false" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--         <asp:TemplateField HeaderText="Add Buffer">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Total-A [TotalBulk + TotalPM + TotalLabour]">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txttotala" runat="server" CssClass="form-control" Enabled="false" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Loss">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtlossper" runat="server" onchange="calculatePricelist(this.id);" TextMode="Number" step="any" Text='<%#  Eval("LossPercentage") %>' Enabled='<%# Eval("type").ToString() == "Estimate" ? false: true %>' CssClass="form-control" />
                                        <asp:TextBox ID="txtlossamt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="Loss Amt">
                                            <ItemTemplate>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Total-B [Total-A + LossAmt]">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txttotalb" runat="server" CssClass="form-control" Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marketed Charge(%)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtmarketper" runat="server" onchange="calculatePricelist(this.id);" TextMode="Number" step="any" Enabled='<%# Eval("type").ToString() == "Estimate" ? false: true %>' Text='<%#  Eval("MarketedChargePercentage") %>' CssClass="form-control" />
                                        <asp:TextBox ID="txtmarketamt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="Marketed Amt">
                                            <ItemTemplate>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Factory Exp(%)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtfactexpper" runat="server" onchange="calculatePricelist(this.id);" TextMode="Number" step="any" Enabled='<%# Eval("type").ToString() == "Estimate" ? false: true %>' Text='<%#  Eval("FactoryExpensePercentage") %>' CssClass="form-control" />
                                        <asp:TextBox ID="txtfactexpamt" runat="server" CssClass="form-control" Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Fact.Exp.Amt">
                                            <ItemTemplate>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Other Charge(%)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtotherper" runat="server" onchange="calculatePricelist(this.id);" TextMode="Number" step="any" Enabled='<%# Eval("type").ToString() == "Estimate" ? false: true %>' Text='<%#  Eval("OtherPercentage") %>' CssClass="form-control" />
                                        <asp:TextBox ID="txtotheramt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="Other Amt">
                                            <ItemTemplate>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Profit (%)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtprofitper" runat="server" onchange="calculatePricelist(this.id);" TextMode="Number" step="any" Enabled='<%# Eval("type").ToString() == "Estimate" ? false: true %>' Text='<%#  Eval("ProfitPercentage") %>' CssClass="form-control" />
                                        <asp:TextBox ID="txtprofitamt" runat="server" CssClass="form-control" Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="Profit Amt">
                                            <ItemTemplate>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>    --%>

                                <asp:TemplateField HeaderText="TotalExpense [FactAmt + MrktAmt + OtherAmt + ProfitAmt]">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txttotalexp" runat="server" CssClass="form-control" Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Final Suggested [Total-B +TotalExpense]">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtfinalsugg" runat="server" CssClass="form-control" Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="FinalPrice/Unit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtfinal" runat="server" onchange="calculatePricelist(this.id);" TextMode="Number" step="any" Text='<%#  Eval("FinalPriceUnit") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FinalProfit [FinalPrice/Unit - Total B + InterestAmt]">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtfinalprofit" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtfinalprofitper" runat="server" CssClass="form-control" Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--  <asp:TemplateField HeaderText="Profit(%)">
                                            <ItemTemplate>
                                             
                                            </ItemTemplate>
                                        </asp:TemplateField>      --%>
                                <asp:TemplateField HeaderText="FinalPrice/Ltr">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtfinalltr" runat="server" CssClass="form-control" Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FinalProfit/Unit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtfinalltrprofit" runat="server" CssClass="form-control" Enabled="false" />
                                        <asp:TextBox ID="txtfinalltrprofitper" runat="server" CssClass="form-control" Enabled="false" />
                                        <asp:Label ID="lblPackSize" runat="server" Style="display: none" Text='<%#  Eval("packingsize") %>'></asp:Label>
                                        <asp:Label ID="lblPackMeasurement" runat="server" Style="display: none" Text='<%#  Eval("packingMeasurementId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Final Profit(%)">
                                            <ItemTemplate>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>     --%>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnupdate" Text="Update" runat="server" CommandArgument='<%#  Eval("OtherComapnyPriceListId") %>' OnClick="btnupdate_Click" OnClientClick="return confirm('Are you sure want to save this data?');" CssClass="btn btn-sm btn-primary" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="hdinner" runat="server" />

                    </div>
                    <div class="modal fade" id="showdetail" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content ">
                                <div class="modal-header">
                                    <h3 class="modal-title text-success" id="exampleModalLabel" runat="server"></h3>
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
                $('#<%= gvothercompany.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvothercompany.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": false,
                    "autoWidth": true,
                    "responsive": true,
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

            //if (type == '1') {
            //    $('#showdetail').modal('show');
            //}
            //else {
            //    $('#showdetail').modal('hide');
            //}

            if (type == '1') {

                $('#showdetail').modal('show');
               // alert(document.getElementById("<%=hdinner.ClientID%>").innerHTML);
                //if (document.getElementById("<%=dvdetailcontent.ClientID%>").innerHTML == '') {
                    //document.getElementById("<%=dvdetailcontent.ClientID%>").innerHTML = document.getElementById("<%=hdinner.ClientID%>").value;
                // }
                //$(".modal-backdrop").not(':first').remove();

            }
            else {
                $('#showdetail').modal('hide');

                //$(".modal-backdrop").remove();
            }

        }



        function calculatePricelist(id) {

            document.getElementById(id).value = Number(document.getElementById(id).value).toFixed(2).toString();

            var grd = '<%= gvothercompany.ClientID %>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var isEstimate = document.getElementById(grd + "_lbliscalc_" + index);


            var BulkCostPerLtr = document.getElementById(grd + "_lblbulkcost_" + index);
            var BulkInterest = document.getElementById(grd + "_txtbulkinterest_" + index);
            var BulkInterestAmt = document.getElementById(grd + "_txtinterestamt_" + index);
            var BulkCostTotal = document.getElementById(grd + "_txttotalbulkcost_" + index);
            var BulkCostPerUnit = document.getElementById(grd + "_txtbulkcostunit_" + index);


            var PM = document.getElementById(grd + "_txtpm_" + index);
            var PMAddBuffer = document.getElementById(grd + "_txtaddpm_" + index);
            var PMTotal = document.getElementById(grd + "_txtpmtotal_" + index);

            var Labour = document.getElementById(grd + "_txtlabour_" + index);
            var LabourAddBuffer = document.getElementById(grd + "_txtaddlabour_" + index);
            var LabourTotal = document.getElementById(grd + "_txttotallabour_" + index);
            var LabourSubTotalA = document.getElementById(grd + "_txttotala_" + index);

            var Loss = document.getElementById(grd + "_txtlossper_" + index);
            var LossAmt = document.getElementById(grd + "_txtlossamt_" + index);
            var LossSubTotalB = document.getElementById(grd + "_txttotalb_" + index);

            var Expense_Market_Per = document.getElementById(grd + "_txtmarketper_" + index);
            var Expense_Market_Amt = document.getElementById(grd + "_txtmarketamt_" + index);
            var Expense_Factory_Per = document.getElementById(grd + "_txtfactexpper_" + index);
            var Expense_Factory_Amt = document.getElementById(grd + "_txtfactexpamt_" + index);
            var Expense_Other_Per = document.getElementById(grd + "_txtotherper_" + index);
            var Expense_Other_Amt = document.getElementById(grd + "_txtotheramt_" + index);
            var Expense_Profit_Per = document.getElementById(grd + "_txtprofitper_" + index);
            var Expense_Profit_Amt = document.getElementById(grd + "_txtprofitamt_" + index);
            var Expense_Total = document.getElementById(grd + "_txttotalexp_" + index);
            var Expense_Suggested = document.getElementById(grd + "_txtfinalsugg_" + index);


            var Final_Price_Unit = document.getElementById(grd + "_txtfinal_" + index);
            var Final_Price_Profit = document.getElementById(grd + "_txtfinalprofit_" + index);
            var Final_Price_Profit_per = document.getElementById(grd + "_txtfinalprofitper_" + index);
            var Final_Price_Ltr = document.getElementById(grd + "_txtfinalltr_" + index);
            var Final_Profit = document.getElementById(grd + "_txtfinalltrprofit_" + index);
            var Final_Profit_Per = document.getElementById(grd + "_txtfinalltrprofitper_" + index);


            var PackSize = document.getElementById(grd + "_lblPackSize_" + index).innerHTML;
            var PackMeasurement = document.getElementById(grd + "_lblPackMeasurement_" + index).innerHTML;




            var ltrtogm;

            //Bulk Calculation

            var blkintamt = parseFloat(BulkCostPerLtr.value) * parseFloat(BulkInterest.value) / 100;
            BulkInterestAmt.value = Number(blkintamt).toFixed(2);
            var blkwithintamt = parseFloat(BulkCostPerLtr.value) + parseFloat(blkintamt);
            BulkCostTotal.value = Number(blkwithintamt).toFixed(2);

            if (parseFloat(PackSize) < 1000 && (PackMeasurement == '7' || PackMeasurement == '6')) {
                ltrtogm = 1000 / parseFloat(PackSize);
                BulkCostPerUnit.value = Number(parseFloat(blkwithintamt) / ltrtogm).toFixed(2);
            }
            if (parseFloat(PackSize) > 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {
                ltrtogm = parseFloat(PackSize);
                BulkCostPerUnit.value = Number(parseFloat(blkwithintamt) * ltrtogm).toFixed(2);
            }
            if (parseFloat(PackSize) == 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {
                ltrtogm = 1000 / parseFloat(PackSize);
                BulkCostPerUnit.value = Number(parseFloat(blkwithintamt)).toFixed(2);
            }

            //PM calculation

            PMTotal.value = Number(parseFloat(PM.value) + parseFloat(PMAddBuffer.value)).toFixed(2);


            //Labour Calculation

            LabourTotal.value = Number(parseFloat(Labour.value) + parseFloat(LabourAddBuffer.value)).toFixed(2);
            LabourSubTotalA.value = Number(parseFloat(BulkCostPerUnit.value) + parseFloat(PMTotal.value) + parseFloat(LabourTotal.value)).toFixed(2);

            //Loss Calculation

            var lossamtval = parseFloat(LabourSubTotalA.value) * parseFloat(Loss.value) / 100;
            LossAmt.value = Number(lossamtval).toFixed(2);
            LossSubTotalB.value = Number(parseFloat(LabourSubTotalA.value) + parseFloat(lossamtval)).toFixed(2);

            //Expense Calculation

            var market = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Market_Per.value) / 100;
            var factory = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Factory_Per.value) / 100;
            var other = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Other_Per.value) / 100;
            var profit = parseFloat(LossSubTotalB.value) * parseFloat(Expense_Profit_Per.value) / 100;

            Expense_Market_Amt.value = Number(market).toFixed(2);
            Expense_Factory_Amt.value = Number(factory).toFixed(2);
            Expense_Other_Amt.value = Number(other).toFixed(2);
            Expense_Profit_Amt.value = Number(profit).toFixed(2);

            Expense_Total.value = Number(parseFloat(market) + parseFloat(factory) + parseFloat(other) + parseFloat(profit)).toFixed(2);
            Expense_Suggested.value = Number(parseFloat(LossSubTotalB.value) + parseFloat(Expense_Total.value)).toFixed(2);

            //Final Calculation

            var ConvertLtrToGmOrMl = 0.00;

            var unipercetage = 0.00;

            if (parseFloat(Final_Price_Unit.value) > 0) {



                if (parseFloat(PackSize) < 1000 && (PackMeasurement == '7' || PackMeasurement == '6')) {


                    ConvertLtrToGmOrMl = 1000 / parseFloat(PackSize);

                    unipercetage = Number(parseFloat(BulkInterestAmt.value) / parseFloat(ConvertLtrToGmOrMl)).toFixed(2);


                    Final_Price_Profit.value = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB.value) + parseFloat(unipercetage)).toFixed(2);
                    Final_Price_Profit_per.value = Number(parseFloat(Final_Price_Profit.value) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);


                    Final_Price_Ltr.value = Number(parseFloat(Final_Price_Unit.value) * ConvertLtrToGmOrMl).toFixed(2);
                    Final_Profit.value = Number(parseFloat(Final_Price_Profit.value) * ConvertLtrToGmOrMl).toFixed(2);



                    Final_Profit_Per.value = Number(parseFloat(Final_Price_Profit_per.value)).toFixed(2);
                }
                if (parseFloat(PackSize) > 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {


                    ConvertLtrToGmOrMl = parseFloat(PackSize);

                    unipercetage = Number(parseFloat(BulkInterestAmt.value) * parseFloat(ConvertLtrToGmOrMl)).toFixed(2);

                    Final_Price_Profit.value = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB.value) + parseFloat(unipercetage)).toFixed(2);
                    Final_Price_Profit_per.value = Number(parseFloat(Final_Price_Profit.value) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);

                    Final_Price_Ltr.value = Number(parseFloat(Final_Price_Unit.value) / ConvertLtrToGmOrMl).toFixed(2);
                    Final_Profit.value = Number(parseFloat(Final_Price_Profit.value) * ConvertLtrToGmOrMl).toFixed(2);

                    Final_Profit_Per.value = Number(parseFloat(Final_Price_Profit_per.value)).toFixed(2);
                }
                if (parseFloat(PackSize) == 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {

                    ConvertLtrToGmOrMl = parseFloat(PackSize);

                    unipercetage = Number(parseFloat(BulkInterestAmt.value)).toFixed(2);

                    Final_Price_Profit.value = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB.value) + parseFloat(unipercetage)).toFixed(2);
                    Final_Price_Profit_per.value = Number(parseFloat(Final_Price_Profit.value) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);

                    Final_Price_Ltr.value = Number(parseFloat(Final_Price_Unit.value) * ConvertLtrToGmOrMl).toFixed(2);
                    Final_Profit.value = Number(parseFloat(Final_Price_Profit.value) * ConvertLtrToGmOrMl).toFixed(2);

                    Final_Profit_Per.value = Number(parseFloat(Final_Price_Profit_per.value)).toFixed(2);
                }

            }


            //Set to Estimate

            if (isEstimate.innerHTML == '1') {

                var newid = parseInt(index) + 1;

                var BulkCostPerLtr_Estimate = document.getElementById(grd + "_lblbulkcost_" + newid);
                document.getElementById(grd + "_txtbulkinterest_" + newid).value = BulkInterest.value;
                document.getElementById(grd + "_txtaddpm_" + newid).value = PMAddBuffer.value;
                document.getElementById(grd + "_txtaddlabour_" + newid).value = LabourAddBuffer.value;

                var BulkCostPerUnit_Estimate = document.getElementById(grd + "_txtbulkcostunit_" + newid);
                var BulkInterestAmt_Estimate = document.getElementById(grd + "_txtinterestamt_" + newid);
                var BulkCostTotal_Estimate = document.getElementById(grd + "_txttotalbulkcost_" + newid);

                var LossSubTotalA_Estimate = document.getElementById(grd + "_txttotala_" + newid);
                var LossSubTotalB_Estimate = document.getElementById(grd + "_txttotalb_" + newid);

                document.getElementById(grd + "_txtlossper_" + newid).value = Loss.value;
                var LossAmt_Estimate = document.getElementById(grd + "_txtlossamt_" + newid);


                document.getElementById(grd + "_txtmarketper_" + newid).value = Expense_Market_Per.value;
                document.getElementById(grd + "_txtfactexpper_" + newid).value = Expense_Factory_Per.value;
                document.getElementById(grd + "_txtotherper_" + newid).value = Expense_Other_Per.value;
                document.getElementById(grd + "_txtprofitper_" + newid).value = Expense_Profit_Per.value;


                var Expense_Total_Estimate = document.getElementById(grd + "_txttotalexp_" + newid);
                var Expense_Suggested_Estimate = document.getElementById(grd + "_txtfinalsugg_" + newid);


                var Final_Price_Profit_Estimate = document.getElementById(grd + "_txtfinalprofit_" + newid);
                var Final_Price_Profit_per_Estimate = document.getElementById(grd + "_txtfinalprofitper_" + newid);
                var Final_Price_Ltr_Estimate = document.getElementById(grd + "_txtfinalltr_" + newid);
                var Final_Profit_Estimate = document.getElementById(grd + "_txtfinalltrprofit_" + newid);
                var Final_Profit_Per_Estimate = document.getElementById(grd + "_txtfinalltrprofitper_" + newid);


                //Bulk Calculation

                var blkintamt = parseFloat(BulkCostPerLtr_Estimate.value) * parseFloat(BulkInterest.value) / 100;
                BulkInterestAmt_Estimate.value = Number(blkintamt).toFixed(2);
                var blkwithintamt = parseFloat(BulkCostPerLtr_Estimate.value) + parseFloat(blkintamt);
                BulkCostTotal_Estimate.value = Number(blkwithintamt).toFixed(2);

                if (parseFloat(PackSize) < 1000 && (PackMeasurement == '7' || PackMeasurement == '6')) {
                    ltrtogm = 1000 / parseFloat(PackSize);
                    BulkCostPerUnit_Estimate.value = Number(parseFloat(blkwithintamt) / ltrtogm).toFixed(2);
                }
                if (parseFloat(PackSize) > 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {
                    ltrtogm = parseFloat(PackSize);
                    BulkCostPerUnit_Estimate.value = Number(parseFloat(blkwithintamt) * ltrtogm).toFixed(2);
                }
                if (parseFloat(PackSize) == 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {
                    ltrtogm = 1000 / parseFloat(PackSize);
                    BulkCostPerUnit_Estimate.value = Number(parseFloat(blkwithintamt)).toFixed(2);
                }




                //PM calculation

                document.getElementById(grd + "_txtpmtotal_" + newid).value = PMTotal.value;


                //Labour Calculation

                document.getElementById(grd + "_txttotallabour_" + newid).value = LabourTotal.value
                LossSubTotalA_Estimate.value = Number(parseFloat(BulkCostPerUnit_Estimate.value) + parseFloat(PMTotal.value) + parseFloat(LabourTotal.value)).toFixed(2);

                //Loss Calculation

                var lossamtval = parseFloat(LossSubTotalA_Estimate.value) * parseFloat(Loss.value) / 100;
                LossAmt_Estimate.value = Number(lossamtval).toFixed(2);
                LossSubTotalB_Estimate.value = Number(parseFloat(LossSubTotalA_Estimate.value) + parseFloat(lossamtval)).toFixed(2);


                //Expense Calculation

                var market = parseFloat(LossSubTotalB_Estimate.value) * parseFloat(Expense_Market_Per.value) / 100;
                var factory = parseFloat(LossSubTotalB_Estimate.value) * parseFloat(Expense_Factory_Per.value) / 100;
                var other = parseFloat(LossSubTotalB_Estimate.value) * parseFloat(Expense_Other_Per.value) / 100;
                var profit = parseFloat(LossSubTotalB_Estimate.value) * parseFloat(Expense_Profit_Per.value) / 100;

                document.getElementById(grd + "_txtmarketamt_" + newid).value = Number(market).toFixed(2);
                document.getElementById(grd + "_txtfactexpamt_" + newid).value = Number(factory).toFixed(2);
                document.getElementById(grd + "_txtotheramt_" + newid).value = Number(other).toFixed(2);
                document.getElementById(grd + "_txtprofitamt_" + newid).value = Number(profit).toFixed(2);


                Expense_Total_Estimate.value = Number(parseFloat(market) + parseFloat(factory) + parseFloat(other) + parseFloat(profit)).toFixed(2);
                Expense_Suggested_Estimate.value = Number(parseFloat(LossSubTotalB_Estimate.value) + parseFloat(Expense_Total_Estimate.value)).toFixed(2);


                //Final Calculation


                if (parseFloat(Final_Price_Unit.value) > 0) {


                    if (parseFloat(PackSize) < 1000 && (PackMeasurement == '7' || PackMeasurement == '6')) {


                        ConvertLtrToGmOrMl = 1000 / parseFloat(PackSize);

                        unipercetage = Number(parseFloat(BulkInterestAmt_Estimate.value) / parseFloat(ConvertLtrToGmOrMl)).toFixed(2);

                        //console.log(Final_Price_Unit.value + '@' + LossSubTotalB_Estimate.value + '@' + BulkInterestAmt_Estimate.value + '@' + ConvertLtrToGmOrMl);

                        Final_Price_Profit_Estimate.value = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB_Estimate.value) + parseFloat(unipercetage)).toFixed(2);
                        Final_Price_Profit_per_Estimate.value = Number(parseFloat(Final_Price_Profit_Estimate.value) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);


                        Final_Price_Ltr_Estimate.value = Number(parseFloat(Final_Price_Unit.value) * ConvertLtrToGmOrMl).toFixed(2);

                        Final_Profit_Estimate.value = Number(parseFloat(Final_Price_Profit_Estimate.value) * ConvertLtrToGmOrMl).toFixed(2);
                        Final_Profit_Per_Estimate.value = Number(parseFloat(Final_Price_Profit_per_Estimate.value)).toFixed(2);

                    }
                    if (parseFloat(PackSize) > 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {


                        ConvertLtrToGmOrMl = parseFloat(PackSize);

                        unipercetage = Number(parseFloat(BulkInterestAmt_Estimate.value) * parseFloat(ConvertLtrToGmOrMl)).toFixed(2);

                        //console.log(Final_Price_Unit.value + '@' + LossSubTotalB_Estimate.value + '@' + BulkInterestAmt_Estimate.value + '@' + ConvertLtrToGmOrMl);

                        Final_Price_Profit_Estimate.value = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB_Estimate.value) + parseFloat(unipercetage)).toFixed(2);
                        Final_Price_Profit_per_Estimate.value = Number(parseFloat(Final_Price_Profit_Estimate.value) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);

                        Final_Price_Ltr_Estimate.value = Number(parseFloat(Final_Price_Unit.value) / ConvertLtrToGmOrMl).toFixed(2);
                        Final_Profit_Estimate.value = Number(parseFloat(Final_Price_Profit_Estimate.value) * ConvertLtrToGmOrMl).toFixed(2);

                        Final_Profit_Per_Estimate.value = Number(parseFloat(Final_Price_Profit_per_Estimate.value)).toFixed(2);
                    }
                    if (parseFloat(PackSize) == 1 && (PackMeasurement == '1' || PackMeasurement == '2')) {

                        ConvertLtrToGmOrMl = parseFloat(PackSize);

                        unipercetage = Number(parseFloat(BulkInterestAmt_Estimate.value)).toFixed(2);

                        //console.log(Final_Price_Unit.value + '@' + LossSubTotalB_Estimate.value + '@' + BulkInterestAmt_Estimate.value + '@' + ConvertLtrToGmOrMl);

                        Final_Price_Profit_Estimate.value = Number(parseFloat(Final_Price_Unit.value) - parseFloat(LossSubTotalB_Estimate.value) + parseFloat(unipercetage)).toFixed(2);
                        Final_Price_Profit_per_Estimate.value = Number(parseFloat(Final_Price_Profit_Estimate.value) * 100 / parseFloat(Final_Price_Unit.value)).toFixed(2);

                        Final_Price_Ltr_Estimate.value = Number(parseFloat(Final_Price_Unit.value) * ConvertLtrToGmOrMl).toFixed(2);
                        Final_Profit_Estimate.value = Number(parseFloat(Final_Price_Profit_Estimate.value) * ConvertLtrToGmOrMl).toFixed(2);

                        Final_Profit_Per_Estimate.value = Number(parseFloat(Final_Price_Profit_per_Estimate.value)).toFixed(2);
                    }

                }


            }

        }

        function scrolltoend() {

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
