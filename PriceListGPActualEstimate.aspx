<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" AutoEventWireup="true" CodeBehind="PriceListGPActualEstimate.aspx.cs" Inherits="Production_Costing_Software.PriceListGPActualEstimate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="contain">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>PriceList GP Actual</b>

                    <p style="font-family: monospace; font-weight: bolder; color: #28a745">
                        <asp:Label ID="lblBulkProductName" Style="font-size: larger" runat="server" Text="" Enabled="false"></asp:Label>
                    </p>

                    <asp:Button ID="GPActualEstimateFinalBtn" OnClick="GPActualEstimateFinalBtn_Click" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Price List GP ActualEstimate" runat="server" class="btn btn-outline-danger btn-sm" />

                    <button type="button" id="ReportActual" class="btn btn-info  align-content-end" onclick="btnActualReport()" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" data-bs-toggle="modal" data-bs-target="#exampleModal">Actual Report</button>
                    <button type="button" id="ReportEstimate" class="btn btn-secondary  align-content-end" onclick="btnEstimateReport()" style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" data-bs-toggle="modal" data-bs-target="#exampleModal">Estimate Report</button>

                    <%--<asp:Button ID="ReportEstimate" runat="server" OnClientClick="btnEstimateReport()" class="btn btn-info" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" Text="Estimate Report" data-bs-toggle="modal" data-bs-target="#exampleModal" />--%>
                </div>

                <div id="dvLoadingCSS" style="display: none;" class="modalLoading">
                    <div class="centerLoading">
                        <i class="fas fa-3x fa-sync-alt fa-spin"></i>
                    </div>
                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="card-header">
                            <div id="divrdActualEstimate" class="form-check form-check-inline " visible="false" runat="server">
                                <asp:RadioButtonList class="form-check-input " runat="server" name="exampleRadios" AutoPostBack="true" OnSelectedIndexChanged="rdActualEstimate_SelectedIndexChanged1" ID="rdActualEstimate">
                                    <asp:ListItem Value="0" onclick="EnableLoader(true);" Text=" Actual"></asp:ListItem>
                                    <asp:ListItem Selected="True" onclick="EnableLoader(true);" Text=" Estimate" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>


                        </div>
                        <div class="card-body">

                            <div class="p-2 p-2  d-flex justify-content-end" style="">
                                   <asp:Button ID="btnupdateall" Text="Update" runat="server" OnClick="btnupdateall_Click" OnClientClick="return confirm('Are you sure want to save all data?');" CssClass="btn btn-sm btn-primary" />
</div>
                            <div id="dvgd" class="container col-12 grds">
                                   
                                <asp:GridView ID="gvgpactualestimate" runat="server" GridLines="Both" AutoGenerateColumns="false" OnRowDataBound="gvgpactualestimate_RowDataBound1" class="table table-bordered table-striped gridview ">


                                    <Columns>
                                        <asp:TemplateField HeaderText="State" HeaderStyle-CssClass="sticky-col first-col" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStateName" runat="server" Text='<%#Eval("StateName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="sticky-col second-col" ControlStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name" SortExpression="MainCategory_Name" Visible="false" />
                                        <asp:TemplateField HeaderText="NRV" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNRV" runat="server" Text='<%#Eval("NRV") %>'></asp:Label>
                                                <span><%# Eval("NewStatus").ToString() == "RPL" ? datadisplay : datadisplayEstimate %></span>
                                                <span id="lbexnav" runat="server" style="display: none"><%# Eval("NewStatus").ToString() == "RPL" ? datadisplayfinal : datadisplayfinalEstimate %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transportation Cost/Liter or KG" ControlStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransport" runat="server" Text='<%#Eval("Transport") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" NRV with Transport" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFinalNRV" runat="server" Text='<%#Eval("FinalNRV") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="RPL/NCR Price" ControlStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEnumDescription" runat="server" Text='<%#Eval("EnumDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TOD" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TODtxt" onchange="calculaterate('0',this.id);" CssClass="form-control" Text='<%#Eval("TOD") %>' TextMode="Number" step="any" Style="text-align: center" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PD" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PDtxt" onchange="calculaterate('1',this.id);" CssClass="form-control" Text='<%#Eval("PD") %>' TextMode="Number" step="any" Style="text-align: center" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="QD" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="QDtxt" onchange="calculaterate('2',this.id);" CssClass="form-control" Text='<%#Eval("QD") %>' TextMode="Number" step="any" Style="text-align: center" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Profit-Amt(%)" ControlStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:Label ID="ProfitAmttxt_lbl" AutoPostBack="true" Style="text-align: center" runat="server"></asp:Label>

                                                <asp:TextBox ID="ProfitAmttxt" CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                <div class="input-group">
                                                    <asp:TextBox ID="ProfitPertxt" onchange="calculaterate('3',this.id);" CssClass="form-control postext" Text='<%#Eval("ProfitPer") %>' TextMode="Number" step="any" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="basic-addon2">%</span>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Suggested-Price with PD" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblGujaratCurrent" runat="server" Text="Current"></asp:Label>--%>

                                                <asp:Label ID="SuggestedPriceWithPDttxt_lbl" AutoPostBack="true" Text='<%#Eval("SuggestedPricePD") %>' TextMode="Number" Style="text-align: center;" runat="server"></asp:Label>

                                                <asp:TextBox ID="SuggestedPriceWithPDttxt" Text='<%#Eval("SuggestedPricePD") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                <span id="lblpddiff" runat="server" class="ncrlabel"></span>

                                                <span id="lblamtexp" runat="server" style="display: none"></span>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ControlStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNewStatus" runat="server" Text='<%#Eval("NewStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Shared Final Price" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLast_Shared_Final_Price" runat="server" Text='<%#Eval("LastShareFinalPrice") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="  Final Price  " ControlStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="FinalPricettxt"
                                                    onchange="calculaterate('4',this.id);"
                                                    CssClass="form-control" TextMode="Number" step="any" Text='<%#Eval("FinalPrice") %>' Style="text-align: center" runat="server"></asp:TextBox>
                                                <span id="lblpricediff" runat="server" class="ncrlabel" style='<%# Eval("NewStatus").ToString() == "RPL" ? "display:none": "display:block" %>'></span>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Additional PD" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="AdditionalPDtxt" onchange="calculaterate('5',this.id);" CssClass="form-control" TextMode="Number" step="any" Text='<%#Eval("AdditionalPD") %>' Style="text-align: center" runat="server"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Gross Profit Amount (%)" ControlStyle-Width="130px">
                                            <ItemTemplate>

                                                <asp:Label ID="GrossProfitAmounttxt_lbl" CssClass="form-control" Style="text-align: center" runat="server"></asp:Label>

                                                <asp:TextBox ID="GrossProfitAmounttxt" CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                <span id="lblgpamt" runat="server" class="ncrlabel"></span>

                                                <asp:Label ID="lblGrossProfitPer_lbl" CssClass="form-control" Style="text-align: center" runat="server">%</asp:Label>

                                                <asp:TextBox ID="lblGrossProfitPer" CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>
                                                <span id="lblgpamtper" runat="server" class="ncrlabel"></span>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Expence" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:Label ID="TotalExpencetxt_lbl" Style="text-align: center" Text='<%#Eval("TotalExpense") %>' runat="server"></asp:Label>

                                                <asp:TextBox ID="TotalExpencetxt" CssClass="txtlbl" Text='<%#Eval("TotalExpense") %>' Style="text-align: center" runat="server"></asp:TextBox>




                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Net Profit Amount (%)" ControlStyle-Width="130px">
                                            <ItemTemplate>

                                                <asp:Label ID="NetProfitAmounttxt_lbl" AutoPostBack="true" TextMode="Number" Style="text-align: center" runat="server"></asp:Label>
                                                <asp:TextBox ID="NetProfitAmounttxt" CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                <asp:Label ID="lblNetProfitAmtPer_lbl" runat="server" Style="text-align: center"></asp:Label>
                                                <asp:TextBox ID="lblNetProfitAmtPer" CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pack Size" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPacking_Size" runat="server" Text='<%#Eval("PackingSize") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%--Update--%>
                                        <asp:TemplateField HeaderText="Select (Make Action Default)" ControlStyle-Width="90px">
                                            <ItemTemplate>
                                                <asp:Button ID="btnupdate" Text="Update" runat="server" CommandArgument='<%#  Eval("PriceListGPActualEStimateId") %>' OnClick="btnupdate_Click" OnClientClick="return confirm('Are you sure want to save this data?');" CssClass="btn btn-sm btn-primary" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--------------------%>
                                        <asp:TemplateField ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStaffExpense" runat="server" Text='<%#Eval("StaffExpense") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMarketing" runat="server" Text='<%#Eval("Marketing") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIncentive" runat="server" Text='<%#Eval("Incentive") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInterest" runat="server" Text='<%#Eval("Interest") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepoExpence" runat="server" Text='<%#Eval("DepoExpence") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOther" runat="server" Text='<%#Eval("Other") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fk_State_Id Size" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFkStateId" runat="server" Text='<%#Eval("FkStateId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fk_UnitMeasurement_Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFk_UnitMeasurement_Id" runat="server" Text='<%#Eval("PackingMeasurementId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--          <asp:TemplateField HeaderText="Fk_PM_RM_Category_Id" ItemStyle-CssClass="displaynone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFk_PM_RM_Category_Id" runat="server" Text='<%#Eval("Fk_PM_RM_Category_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="FkBulkProductId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFkBulkProductId" runat="server" Text='<%#Eval("FkBulkProductId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FkPriceTypeId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFkPriceTypeId" runat="server" Text='<%#Eval("FkPriceTypeId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--             <asp:TemplateField HeaderText="TradeName_Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTradeName_Id" runat="server" Text='<%#Eval("TradeId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="PackingMaterial" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPackingMaterialId" runat="server" Text='<%#Eval("PackingMaterialId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="InterestAmt" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInterestAmt" runat="server" Text='<%#Eval("InterestAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PriceListGPActualEStimateId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriceListGPActualEstimateId" runat="server" Text='<%#Eval("PriceListGPActualEstimateId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>

                                <asp:GridView ID="gvpricelistactual" class="table table-bordered table-striped gridview "
                                    GridLines="Both" OnRowDataBound="gvpricelistactual_RowDataBound"
                                    runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="State" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStateName" runat="server" Text='<%#Eval("StateName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" ControlStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="MainCategory_Name" />--%>
                                        <asp:BoundField DataField="BPM_Product_Name" HeaderText="BPM Product Name" SortExpression="MainCategory_Name" Visible="false" />
                                        <asp:TemplateField HeaderText="NRV" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNRV" runat="server" Text='<%#Eval("NRV") %>'></asp:Label>
                                                <span><%=datadisplay %></span>
                                                <span id="lbexnav" runat="server" style="display: none"><%=datadisplayfinal %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transportation Cost/Liter or KG" ControlStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransport" runat="server" Text='<%#Eval("Transport") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Final Fectory Cost (NRV) / Ltr with Transportation (Final NRV)" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFinalNRV" runat="server" Text='<%#Eval("FinalNRV") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="RPL/NCR Price" ControlStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEnumDescription" runat="server" Text='<%#Eval("EnumDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TOD" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TODtxt" onchange="calculaterate_actual('0',this.id);" CssClass="form-control" Text='<%#Eval("TOD") %>' TextMode="Number" step="any" Style="text-align: center" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PD" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PDtxt" onchange="calculaterate_actual('1',this.id);" CssClass="form-control" Text='<%#Eval("PD") %>' TextMode="Number" step="any" Style="text-align: center" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="QD" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="QDtxt" onchange="calculaterate_actual('2',this.id);" CssClass="form-control" Text='<%#Eval("QD") %>' TextMode="Number" step="any" Style="text-align: center" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Profit Amt(%)" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblProfitPer" runat="server" Text=""></asp:Label>--%>
                                                <asp:Label ID="ProfitAmttxt_lbl" Style="text-align: center" runat="server"></asp:Label>

                                                <asp:TextBox ID="ProfitAmttxt" CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                <div class="input-group">
                                                    <asp:TextBox ID="ProfitPertxt" onchange="calculaterate_actual('3',this.id);" CssClass="form-control" Text='<%#Eval("ProfitPer") %>' TextMode="Number" step="any" Style="width: 25px" runat="server"></asp:TextBox>
                                                    <span class="input-group-text" id="basic-addon2">%</span>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Suggested-Price with PD" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblGujaratCurrent" runat="server" Text="Current"></asp:Label>--%>
                                                <asp:Label ID="SuggestedPriceWithPDttxt_lbl" Text='<%#Eval("SuggestedPricePD") %>' Style="text-align: center;" runat="server"></asp:Label>

                                                <asp:TextBox ID="SuggestedPriceWithPDttxt" Text='<%#Eval("SuggestedPricePD") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                <span id="lblpricediff_act" runat="server" class="ncrlabel"></span>

                                                <span id="lblamtexp" runat="server" style="display: none"></span>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Last Shared Final Price" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLast_Shared_Final_Price" runat="server" Text='<%#Eval("LastShareFinalPrice") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Final Price" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="FinalPricettxt" onchange="calculaterate_actual('4',this.id);" CssClass="form-control" TextMode="Number" step="any" Text='<%#Eval("FinalPrice") %>' Style="text-align: center" runat="server"></asp:TextBox>
                                                <span id="lblpricediff" runat="server" class="ncrlabel" style='<%# Eval("EnumDescription").ToString() == "RPL" ? "display:none": "display:block" %>'></span>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Additional PD" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="AdditionalPDtxt" onchange="calculaterate_actual('5',this.id);" CssClass="form-control" TextMode="Number" step="any" Text='<%#Eval("AdditionalPD") %>' Style="text-align: center" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Gross Profit Amount (%)" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <%-- <asp:Label ID="GrossProfitAmounttxt" TextMode="Number"  Text='<%#Eval("GrossProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>
                                                        <asp:Label ID="lblGrossProfitPer" runat="server" Text='<%#Eval("GrossProfitPer") %>'></asp:Label>--%>

                                                <asp:Label ID="GrossProfitAmounttxt_lbl" CssClass="form-control" Style="text-align: center" runat="server"></asp:Label>


                                                <asp:TextBox ID="GrossProfitAmounttxt" CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                <span id="lblgpamt" runat="server" class="ncrlabel"></span>


                                                <asp:Label ID="lblGrossProfitPer_lbl" CssClass="form-control" runat="server">%</asp:Label>

                                                <asp:TextBox ID="lblGrossProfitPer" CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                                <span id="lblgpamtper" runat="server" class="ncrlabel"></span>



                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Expence" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <asp:Label ID="TotalExpencetxt_lbl" Text='<%#Eval("TotalExpense") %>' Style="text-align: center" runat="server"></asp:Label>

                                                <asp:TextBox ID="TotalExpencetxt" Text='<%#Eval("TotalExpense") %>' CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Net Profit Amount (%)" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="NetProfitAmounttxt" TextMode="Number" Text='<%#Eval("NetProfitAmt") %>' Style="text-align: center" runat="server"></asp:Label>
                                                        <asp:Label ID="lblNetProfitAmtPer" runat="server" Text='<%#Eval("NetProfitPer") %>'></asp:Label>--%>

                                                <asp:Label ID="NetProfitAmounttxt_lbl" AutoPostBack="true" TextMode="Number" Style="text-align: center" runat="server"></asp:Label>

                                                <asp:TextBox ID="NetProfitAmounttxt" CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>


                                                <asp:Label ID="lblNetProfitAmtPer_lbl" runat="server" Style="text-align: center"></asp:Label>


                                                <asp:TextBox ID="lblNetProfitAmtPer" CssClass="txtlbl" Style="text-align: center" runat="server"></asp:TextBox>




                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pack Size" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPacking_Size" runat="server" Text='<%#Eval("PackingSize") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Select (Make Action Default)" ControlStyle-Width="90px">
                                            <ItemTemplate>
                                                <asp:Button ID="btnupdate" Text="Update" runat="server" CommandArgument='<%#  Eval("PriceListGPActualEstimateId") %>' OnClick="btnupdate_Click" OnClientClick="return confirm('Are you sure want to save this data?');" CssClass="btn btn-sm btn-primary" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStaffExpense" runat="server" Text='<%#Eval("StaffExpense") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMarketing" runat="server" Text='<%#Eval("Marketing") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIncentive" runat="server" Text='<%#Eval("Incentive") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInterest" runat="server" Text='<%#Eval("Interest") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepoExpence" runat="server" Text='<%#Eval("DepoExpence") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pack Size" ItemStyle-CssClass="displaynone" HeaderStyle-CssClass="displaynone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOther" runat="server" Text='<%#Eval("Other") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fk_State_Id Size" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFkStateId" runat="server" Text='<%#Eval("FkStateId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fk_UnitMeasurement_Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFk_UnitMeasurement_Id" runat="server" Text='<%#Eval("PackingMeasurementId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PackingMaterial" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPackingMaterialId" runat="server" Text='<%#Eval("PackingMaterialId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fk_BPM_Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFkBulkProductId" runat="server" Text='<%#Eval("FkBulkProductId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FkPriceTypeId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFkPriceTypeId" runat="server" Text='<%#Eval("FkPriceTypeId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TradeName_Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsTradeName_Id" runat="server" Text='<%#Eval("TradeId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PriceListGPActualEStimateId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriceListGPActualEstimateId" runat="server" Text='<%#Eval("PriceListGPActualEstimateId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>



                            </div>

                            <div class="p-2 p-2  d-flex justify-content-end" style="">
                                   <asp:Button ID="btnupdateall2" Text="Update" runat="server" OnClick="btnupdateall_Click" OnClientClick="return confirm('Are you sure want to save all data?');" CssClass="btn btn-sm btn-primary" />
</div>

                        </div>

                        <asp:Label ID="lblDynamicColumnCount" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblNewStatus" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblFinalNRV" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblactest" runat="server" Style="display: none"></asp:Label>

                        <asp:HiddenField runat="server" ID="HiddenField1" />
                        <asp:HiddenField runat="server" ID="hdnIsEstimate" />
                        <asp:HiddenField runat="server" ID="btnFkBulkproductId" />
                        <asp:HiddenField runat="server" ID="hdnSwitchReadOnlyEdit" />

                        <asp:HiddenField ID="hdngrdcalc" runat="server" />

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal fade" id="showdetail" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <h3 class="modal-title text-success" id="exampleModalLabel" runat="server">
                                <asp:Label ID="lblBulkProductNameReport" runat="server"></asp:Label>
                                <asp:HiddenField runat="server" ID="hdnbulkProductName" />
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
        </div>
    </div>

    <script type="text/javascript">     




        function calculaterate_actual(type, id) {

            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var grd = '<%=gvpricelistactual.ClientID%>';

            var lblrplncr = document.getElementById(grd + "_lblEnumDescription_" + index).innerHTML;


            if (type == '0' || type == '1' || type == '2' || type == '3') {

                if (lblrplncr.trim().toUpperCase() == "RPL") {

                    updatecalc_actual(index, index, grd, 1);

                    var t = parseInt(index) + 1;
                    updatecalc_actual(t, parseInt(t), grd, 0);


                    calculategrossprofit_actual(index, grd, type, 0);


                    var check = document.getElementById(grd + "_CheckBox_Check_" + index);
                    if (check != null) {
                        check.checked = true;
                    }


                }
                else {

                    var lblid = parseInt(index) - 1;

                    updatecalc_actual(index, parseInt(lblid), grd, 0);

                    calculategrossprofit_actual(index, grd, type, 1);


                    var check = document.getElementById(grd + "_CheckBox_Check_" + index);
                    if (check != null) {
                        check.checked = true;
                    }
                }


            }
            else {
                var isrplncr = 0;
                if (lblrplncr.trim().toUpperCase() == "NCR") {
                    isrplncr = 1;
                }

                calculategrossprofit_actual(index, grd, type, isrplncr)

                var check = document.getElementById(grd + "_CheckBox_Check_" + index);
                if (check != null) {
                    check.checked = true;
                }

            }



        }
        function updatecalc_actual(mainid, lblindexid, grd, tp) {


            var pd = document.getElementById(grd + "_PDtxt_" + mainid);
            var qd = document.getElementById(grd + "_QDtxt_" + mainid);
            var tod = document.getElementById(grd + "_TODtxt_" + mainid);


            var txtprofiramt = document.getElementById(grd + "_ProfitPertxt_" + mainid);
            var lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + lblindexid);

            var newid;

            if (tp == '1') {

                document.getElementById(grd + "_PDtxt_" + mainid).value = pd.value;
                document.getElementById(grd + "_QDtxt_" + mainid).value = qd.value;
                document.getElementById(grd + "_TODtxt_" + mainid).value = tod.value;

                document.getElementById(grd + "_ProfitPertxt_" + mainid).value = txtprofiramt.value;

                var final = lblFinalNAV.innerHTML;

                var percentage = final * (parseFloat(txtprofiramt.value) / 100);

                var total = parseFloat(tod.value) + parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(final);


                document.getElementById(grd + "_ProfitAmttxt_lbl_" + mainid).innerHTML = Number(percentage).toFixed(2);

                document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + mainid).value = Number(total + percentage).toFixed(2);
                document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + mainid).innerHTML = Number(total + percentage).toFixed(2);

                document.getElementById(grd + "_lblamtexp_" + mainid).innerHTML = Number(percentage).toFixed(2);

                newid = mainid;


                var lbexnav = parseFloat(document.getElementById(grd + "_lbexnav_" + mainid).innerHTML);
                var lblamtexp = parseFloat(document.getElementById(grd + "_lblamtexp_" + mainid).innerHTML);
                var lblt = Number(lbexnav + lblamtexp).toFixed(2);
                var lbnav = parseFloat(lblFinalNAV.innerHTML);
                document.getElementById(grd + "_lblgpamt_" + mainid).innerHTML = document.getElementById(grd + "_lbexnav_" + mainid).innerHTML + "+" + document.getElementById(grd + "_lblamtexp_" + mainid).innerHTML + "(" + lblt + ")";
                document.getElementById(grd + "_lblgpamtper_" + mainid).innerHTML = Number((lblt * parseFloat(100)) / (lbnav - lbexnav)).toFixed(2) + '%';




            }
            else {




                lblindexid = parseInt(lblindexid) - 1;
                var t = parseInt(mainid) - 1;


                pd = document.getElementById(grd + "_PDtxt_" + mainid);
                qd = document.getElementById(grd + "_QDtxt_" + mainid);
                tod = document.getElementById(grd + "_TODtxt_" + mainid);


                var lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + t);

                var rplestprice = parseFloat(lblFinalNAV.innerHTML) * (parseFloat(txtprofiramt.value) / 100);
                var newsuggprice = parseFloat(lblFinalNAV.innerHTML) + rplestprice + parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(tod.value);

                document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + mainid).value = Number(newsuggprice).toFixed(2);
                document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + mainid).innerHTML = Number(newsuggprice).toFixed(2);


                document.getElementById(grd + "_ProfitAmttxt_" + mainid).value = Number(rplestprice).toFixed(2);
                document.getElementById(grd + "_ProfitAmttxt_lbl_" + mainid).innerHTML = Number(rplestprice).toFixed(2);

                document.getElementById(grd + "_lblamtexp_" + mainid).innerHTML = Number(rplestprice).toFixed(2);


                var lbexnav = parseFloat(document.getElementById(grd + "_lbexnav_" + t).innerHTML);
                var lblamtexp = parseFloat(document.getElementById(grd + "_lblamtexp_" + mainid).innerHTML);
                var lblt = Number(lbexnav + lblamtexp).toFixed(2);
                var lbnav = parseFloat(lblFinalNAV.innerHTML);
                document.getElementById(grd + "_lblgpamt_" + mainid).innerHTML = document.getElementById(grd + "_lbexnav_" + t).innerHTML + "+" + document.getElementById(grd + "_lblamtexp_" + mainid).innerHTML + "(" + lblt + ")";
                document.getElementById(grd + "_lblgpamtper_" + mainid).innerHTML = Number((lblt * parseFloat(100)) / (lbnav - lbexnav)).toFixed(2) + '%';




                newid = t;

            }


            var rpl_act;
            var current_act;

            if (document.getElementById(grd + "_lblEnumDescription_" + mainid).innerHTML == "RPL") {

                rpl_act = mainid;
                current_act = parseInt(mainid) + 1;

            }
            else {

                rpl_act = parseInt(mainid) - 1;
                current_act = parseInt(mainid);

            }

            if (document.getElementById(grd + "_lblEnumDescription_" + mainid).innerHTML == "NCR") {

                var rp = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + rpl_act).innerHTML);
                var nc = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + current_act).innerHTML);
                var percentage = Number(((rp - nc) / rp) * 100).toFixed(2);
                if (parseFloat(rp) == 0 && parseFloat(nc) == 0) {
                    percentage = "0";
                }
                document.getElementById(grd + "_lblpricediff_act_" + current_act).innerHTML = Number(rp - nc).toFixed(2) + '<span class=ncrlabel>(' + percentage + '%)</span>';


            }
            else {
                document.getElementById(grd + "_lblpricediff_act_" + rpl_act).style.display = 'none';

            }


        }
        function calculategrossprofit_actual(mainid, grd, type, isncr) {

            var AdditionalPDtxt = "";
            var GrossProfitAmounttxt = "";
            var GrossProfitAmountper = "";
            var TotalExpencetxt = "";
            var NetProfitAmounttxt = "";
            var NetProfitAmounttxt_per = "";
            var lblFinalNAV = "";
            var pd_lbl;
            var qd_lbl;
            var tod_lbl;

            if (isncr == '1') {

                var tid = parseInt(mainid) - 1;
                lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + tid);
            }
            else {
                lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + mainid);
            }

            FinalPricettxt = document.getElementById(grd + "_FinalPricettxt_" + mainid);




            var f_act;
            var f_est;
            var diff_act;

            if (document.getElementById(grd + "_lblEnumDescription_" + mainid).innerHTML == "RPL") {
                f_act = parseInt(mainid);
                f_est = parseInt(mainid) + 1;
                diff_act = parseInt(mainid) + 1;
            }
            else {
                f_act = parseInt(mainid) - 1;
                f_est = parseInt(mainid);
                diff_act = parseInt(mainid)
            }

            var rp = document.getElementById(grd + "_FinalPricettxt_" + f_act).value;
            var nc = document.getElementById(grd + "_FinalPricettxt_" + f_est).value;

            var percentage = Number(((rp - nc) / rp) * 100).toFixed(2);

            if (parseFloat(rp) == 0 && parseFloat(nc) == 0) {
                percentage = "0";
            }


            document.getElementById(grd + "_lblpricediff_" + diff_act).style.backgroundColor = 'white';
            document.getElementById(grd + "_lblpricediff_" + diff_act).innerHTML = Number(rp - nc).toFixed(2) + '<span class=ncrlabel>(' + percentage + '%)</span>';




            AdditionalPDtxt = document.getElementById(grd + "_AdditionalPDtxt_" + mainid);

            GrossProfitAmounttxt = document.getElementById(grd + "_GrossProfitAmounttxt_" + mainid);

            var GrossProfitAmounttxt_lbl = document.getElementById(grd + "_GrossProfitAmounttxt_lbl_" + mainid);

            GrossProfitAmountper = document.getElementById(grd + "_lblGrossProfitPer_" + mainid);

            var GrossProfitAmountper_lbl = document.getElementById(grd + "_lblGrossProfitPer_lbl_" + mainid);

            TotalExpencetxt = document.getElementById(grd + "_TotalExpencetxt_" + mainid);

            var TotalExpencetxt_lbl = document.getElementById(grd + "_TotalExpencetxt_lbl_" + mainid);



            NetProfitAmounttxt = document.getElementById(grd + "_NetProfitAmounttxt_" + mainid);

            var NetProfitAmounttxt_lbl = document.getElementById(grd + "_NetProfitAmounttxt_lbl_" + mainid);

            NetProfitAmounttxt_per = document.getElementById(grd + "_lblNetProfitAmtPer_" + mainid);

            var NetProfitAmounttxt_per_lbl = document.getElementById(grd + "_lblNetProfitAmtPer_lbl_" + mainid);

            pd_lbl = document.getElementById(grd + "_PDtxt_" + mainid);
            qd_lbl = document.getElementById(grd + "_QDtxt_" + mainid);
            tod_lbl = document.getElementById(grd + "_TODtxt_" + mainid);



            var grossprofit = parseFloat(FinalPricettxt.value) - parseFloat(AdditionalPDtxt.value) - parseFloat(lblFinalNAV.innerHTML) - parseFloat(pd_lbl.value) - parseFloat(qd_lbl.value) - parseFloat(tod_lbl.value);

            GrossProfitAmounttxt.value = Number(grossprofit).toFixed(2);
            GrossProfitAmounttxt_lbl.innerHTML = Number(grossprofit).toFixed(2);

            var grossprofit_per = Number(grossprofit * (100 / parseFloat(lblFinalNAV.innerHTML))).toFixed(2);
            GrossProfitAmountper.value = Number(grossprofit_per).toFixed(2);
            GrossProfitAmountper_lbl.innerHTML = Number(grossprofit_per).toFixed(2);


            var lblStaffExpense = document.getElementById(grd + "_lblStaffExpense_" + mainid);
            var lblMarketing = document.getElementById(grd + "_lblMarketing_" + mainid);
            var lblIncentive = document.getElementById(grd + "_lblIncentive_" + mainid);
            var lblInterest = document.getElementById(grd + "_lblInterest_" + mainid);
            var lblDepoExpence = document.getElementById(grd + "_lblDepoExpence_" + mainid);
            var lblOther = document.getElementById(grd + "_lblOther_" + mainid);


            var TotalExpencetxt = document.getElementById(grd + "_TotalExpencetxt_" + mainid);

            var TotalExpencetxt_lbl = document.getElementById(grd + "_TotalExpencetxt_lbl_" + mainid);

            expense = (parseFloat(FinalPricettxt.value) * parseFloat(lblStaffExpense.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblMarketing.innerHTML) / 100) + + (parseFloat(FinalPricettxt.value) * parseFloat(lblIncentive.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblInterest.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblDepoExpence.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblOther.innerHTML) / 100)
            TotalExpencetxt.value = Number(expense).toFixed(2);
            TotalExpencetxt_lbl.innerHTML = Number(expense).toFixed(2);


            var netprofit = parseFloat(grossprofit) - parseFloat(TotalExpencetxt.value);
            NetProfitAmounttxt.value = Number(netprofit).toFixed(2);
            NetProfitAmounttxt_lbl.innerHTML = Number(netprofit).toFixed(2);

            var NetProfit_per = netprofit * (100 / parseFloat(lblFinalNAV.innerHTML));
            NetProfitAmounttxt_per.value = Number(NetProfit_per).toFixed(2);
            NetProfitAmounttxt_per_lbl.innerHTML = Number(NetProfit_per).toFixed(2) + ' %';


            //var newid = parseInt(mainid) + 1;

            //var lblamtexp_2 = parseFloat(document.getElementById(grd + "_lblamtexp_" + newid).innerHTML);
            //var lblt_2 = Number(lbexnav + lblamtexp_2).toFixed(2);
            //var lbnav_2 = parseFloat(lblFinalNAV.innerHTML);
            //document.getElementById(grd + "_lblgpamt_" + newid).innerHTML = document.getElementById(grd + "_lbexnav_" + mainid).innerHTML + "+" + document.getElementById(grd + "_lblamtexp_" + newid).innerHTML + "(" + lblt_2 + ")";
            //document.getElementById(grd + "_lblgpamtper_" + newid).innerHTML = Number((lblt_2 * parseFloat(100)) / (lbnav_2 - lbexnav)).toFixed(2);


        }

        function calculaterate(type, id, ischk) {

            document.getElementById(id).value = Number(document.getElementById(id).value).toFixed(2).toString();

            //1-pd,2-qd,3-pa,4-fp,5-adpd;

            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);

            var grd = '<%=gvgpactualestimate.ClientID%>';

            var lblrplncr = document.getElementById(grd + "_lblEnumDescription_" + index).innerHTML;

            var lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + index);
            var lblfinalrplncr = document.getElementById(grd + "_lblNewStatus_" + index);

            var txtpd = document.getElementById(grd + "_PDtxt_" + index);
            var txtqd = document.getElementById(grd + "_QDtxt_" + index);
            var txtprofiramt = document.getElementById(grd + "_ProfitPertxt_" + index);
            var txtfinalprice = document.getElementById(grd + "_FinalPricettxt_" + index);
            var txtaddpd = document.getElementById(grd + "_AdditionalPDtxt_" + index);

            var txtpd_ncr = document.getElementById(grd + "_PDtxt_" + index);
            var txtqd_ncr = document.getElementById(grd + "_QDtxt_" + index);
            var txtprofiramt_ncr = document.getElementById(grd + "_ProfitPertxt_" + index);
            var txtfinalprice_ncr = document.getElementById(grd + "_FinalPricettxt_" + index);
            var txtaddpd_ncr = document.getElementById(grd + "_AdditionalPDtxt_" + index);


            var lblProfitAmttxt = document.getElementById(grd + "_ProfitAmttxt_" + index);
            var lblSuggestedPrice = document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + index);
            var lblGrossProfitAmt = document.getElementById(grd + "_GrossProfitAmounttxt_" + index);
            var lblGrossProfitPer = document.getElementById(grd + "_lblGrossProfitPer_" + index);
            var lblNetProfitAmt = document.getElementById(grd + "_NetProfitAmounttxt_" + index);
            var lblNetProfirPer = document.getElementById(grd + "_lblNetProfitAmtPer_" + index);

            var lblNetProfitAmt_lbl = document.getElementById(grd + "_NetProfitAmounttxt_lbl_" + index);



            if (type == '0' || type == '1' || type == '2' || type == '3') {

                if (lblrplncr.trim().toUpperCase() == "RPL") {



                    updatecalc(index, index, grd, 1);

                    var t = parseInt(index) + 1;

                    updatecalc(t, parseInt(t), grd, 0)

                    calculategrossprofit(index, grd, type);




                }
                else {

                    updatecalc(index, parseInt(index), grd, 0)
                    var t = parseInt(index) + 1;

                    calculategrossprofit(t, grd, type);


                }


            }
            else {


                calculategrossprofit(index, grd, type)


            }


        }

        function updatecalc_EstimateToActual(mainid, grd, tp) {

            var nrvindex;
            var suggindex;

            var pd = document.getElementById(grd + "_PDtxt_" + mainid);
            var qd = document.getElementById(grd + "_QDtxt_" + mainid);
            var tod = document.getElementById(grd + "_TODtxt_" + mainid);
            var txtprofiramt = document.getElementById(grd + "_ProfitPertxt_" + mainid);




            if (tp == '1') {

                nrvindex = mainid;
                suggindex = mainid;

            }
            else {

                nrvindex = parseInt(mainid) - 3;
                suggindex = parseInt(mainid) - 1;

            }



            var lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + nrvindex);

            var final = lblFinalNAV.innerHTML;

            var percentage = final * (parseFloat(txtprofiramt.value) / 100);

            var total = parseFloat(tod.value) + parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(final);
            document.getElementById(grd + "_ProfitAmttxt_lbl_" + mainid).innerHTML = Number(percentage).toFixed(2);


            document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + suggindex).value = Number(total + percentage).toFixed(2);
            document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + suggindex).innerHTML = Number(total + percentage).toFixed(2);

            /*
            document.getElementById(grd + "_lblamtexp_" + mainid).innerHTML = Number(percentage).toFixed(2);                      

            var lbexnav = parseFloat(document.getElementById(grd + "_lbexnav_" + mainid).innerHTML);
            var lblamtexp = parseFloat(document.getElementById(grd + "_lblamtexp_" + mainid).innerHTML);
            var lblt = Number(lbexnav + lblamtexp).toFixed(2);
            var lbnav = parseFloat(lblFinalNAV.innerHTML);
            document.getElementById(grd + "_lblgpamt_" + mainid).innerHTML = document.getElementById(grd + "_lbexnav_" + mainid).innerHTML + "+" + document.getElementById(grd + "_lblamtexp_" + mainid).innerHTML + "(" + lblt + ")";
            document.getElementById(grd + "_lblgpamtper_" + mainid).innerHTML = Number((lblt * parseFloat(100)) / (lbnav - lbexnav)).toFixed(2) + '%';
            */




        }

        function calculategrossprofit_EstimateToActual(mainid, grd, type) {

        }


        function updatecalc(mainid, lblindexid, grd, tp) {



            var pd = document.getElementById(grd + "_PDtxt_" + mainid);
            var qd = document.getElementById(grd + "_QDtxt_" + mainid);
            var tod = document.getElementById(grd + "_TODtxt_" + mainid);

            var txtprofiramt = document.getElementById(grd + "_ProfitPertxt_" + mainid);
            var txtfinalprice = document.getElementById(grd + "_FinalPricettxt_" + mainid);
            var txtaddpd = document.getElementById(grd + "_AdditionalPDtxt_" + mainid);

            var newid = parseInt(mainid) + 2;


            if (tp == '1') {

                var lblst = document.getElementById(grd + "_lblStatus_" + mainid);

                if (lblst.innerHTML == "Actual") {

                    document.getElementById(grd + "_PDtxt_" + newid).value = pd.value;
                    document.getElementById(grd + "_QDtxt_" + newid).value = qd.value;
                    document.getElementById(grd + "_TODtxt_" + newid).value = tod.value;

                    document.getElementById(grd + "_ProfitPertxt_" + newid).value = txtprofiramt.value;
                }



                var lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + lblindexid);
                var final = lblFinalNAV.innerHTML;
                var percentage = final * (parseFloat(txtprofiramt.value) / 100);

                var total = parseFloat(tod.value) + parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(final);




                document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + mainid).value = Number(total + percentage).toFixed(2);
                document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + mainid).innerHTML = Number(total + percentage).toFixed(2);

                document.getElementById(grd + "_ProfitAmttxt_" + mainid).value = Number(percentage).toFixed(2);
                document.getElementById(grd + "_ProfitAmttxt_lbl_" + mainid).innerHTML = Number(percentage).toFixed(2);


                document.getElementById(grd + "_lblamtexp_" + mainid).innerHTML = Number(percentage).toFixed(2);



                var lblFinalNAV_sub = document.getElementById(grd + "_lblFinalNRV_" + newid);


                var final_sub = lblFinalNAV_sub.innerHTML;

                var percentage_sub = final_sub * (parseFloat(txtprofiramt.value) / 100);


                var total_sub = parseFloat(tod.value) + parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(final_sub);

                if (lblst.innerHTML == "Actual") {

                    document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + newid).value = Number(total_sub + percentage_sub).toFixed(2);
                    document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + newid).innerHTML = Number(total_sub + percentage_sub).toFixed(2);


                    document.getElementById(grd + "_ProfitAmttxt_" + newid).value = Number(percentage_sub).toFixed(2);
                    document.getElementById(grd + "_ProfitAmttxt_lbl_" + newid).innerHTML = Number(percentage_sub).toFixed(2);


                    document.getElementById(grd + "_lblamtexp_" + newid).innerHTML = Number(percentage_sub).toFixed(2);
                }



            }
            else {

                lblindexid = parseInt(lblindexid) - 1;

                var lblst = document.getElementById(grd + "_lblStatus_" + lblindexid);

                if (lblst.innerHTML == "Actual") {


                    document.getElementById(grd + "_PDtxt_" + newid).value = pd.value;
                    document.getElementById(grd + "_QDtxt_" + newid).value = qd.value;
                    document.getElementById(grd + "_TODtxt_" + newid).value = tod.value;

                    document.getElementById(grd + "_ProfitPertxt_" + newid).value = txtprofiramt.value;
                }

                var t = parseInt(mainid) - 1;

                pd = document.getElementById(grd + "_PDtxt_" + t);
                qd = document.getElementById(grd + "_QDtxt_" + t);
                tod = document.getElementById(grd + "_TODtxt_" + t);


                var n_pd = document.getElementById(grd + "_PDtxt_" + newid).value;
                var n_qd = document.getElementById(grd + "_QDtxt_" + newid).value;
                var n_tod = document.getElementById(grd + "_TODtxt_" + newid).value;

                if (lblst.innerHTML == "Estimate") {

                    n_pd = document.getElementById(grd + "_PDtxt_" + mainid).value;
                    n_qd = document.getElementById(grd + "_QDtxt_" + mainid).value;
                    n_tod = document.getElementById(grd + "_TODtxt_" + mainid).value;
                }



                var lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + lblindexid);
                var final = lblFinalNAV.innerHTML;
                var percentage = final * (parseFloat(txtprofiramt.value) / 100);
                var total = parseFloat(n_tod) + parseFloat(n_pd) + parseFloat(n_qd) + parseFloat(final);



                var rplestprice = parseFloat(final) * (parseFloat(txtprofiramt.value) / 100);



                var newsuggprice = parseFloat(final) + rplestprice + parseFloat(n_pd) + parseFloat(n_qd) + parseFloat(n_tod);



                document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + mainid).value = Number(newsuggprice).toFixed(2);
                document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + mainid).innerHTML = Number(newsuggprice).toFixed(2);


                document.getElementById(grd + "_ProfitAmttxt_" + mainid).value = Number(rplestprice).toFixed(2);
                document.getElementById(grd + "_ProfitAmttxt_lbl_" + mainid).innerHTML = Number(rplestprice).toFixed(2);


                document.getElementById(grd + "_lblamtexp_" + mainid).innerHTML = Number(rplestprice).toFixed(2);




                var nid = parseInt(lblindexid) + 2;
                var lblFinalNAV_sub = document.getElementById(grd + "_lblFinalNRV_" + nid);



                var final_sub = lblFinalNAV_sub.innerHTML;
                var total_sub = parseFloat(n_tod) + parseFloat(n_pd) + parseFloat(n_qd) + parseFloat(final_sub);
                var percentage_sub = 0.0;
                percentage_sub = total_sub * (parseFloat(txtprofiramt.value) / 100);

                var t1 = parseInt(newid) - 1;


                percentage_sub = parseFloat(final_sub) * (parseFloat(txtprofiramt.value) / 100);

                var rplestprice_sub = parseFloat(final_sub) * (parseFloat(txtprofiramt.value) / 100);
                var newsuggprice_sub = parseFloat(final_sub) + rplestprice_sub + parseFloat(n_pd) + parseFloat(n_qd) + parseFloat(n_tod);


                if (lblst.innerHTML == "Actual") {

                    document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + newid).value = Number(newsuggprice_sub).toFixed(2);
                    document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + newid).innerHTML = Number(newsuggprice_sub).toFixed(2);


                    document.getElementById(grd + "_ProfitAmttxt_" + newid).value = Number(rplestprice_sub).toFixed(2);
                    document.getElementById(grd + "_ProfitAmttxt_lbl_" + newid).innerHTML = Number(rplestprice_sub).toFixed(2);


                    document.getElementById(grd + "_lblamtexp_" + newid).innerHTML = Number(rplestprice_sub).toFixed(2);
                }
                else {

                }



            }

            var rpl_act;
            var rpl_est;
            var current_act;
            var current_est;
            if (document.getElementById(grd + "_lblEnumDescription_" + mainid).innerHTML == "RPL") {

                rpl_act = mainid;
                rpl_est = parseInt(rpl_act) + 2;
                current_act = parseInt(mainid) + 1;
                current_est = parseInt(current_act) + 2;
            }
            else {

                rpl_act = parseInt(mainid) - 1;
                rpl_est = parseInt(rpl_act) + 2;
                current_act = parseInt(mainid);
                current_est = parseInt(current_act) + 2;
            }

            if (document.getElementById(grd + "_lblEnumDescription_" + mainid).innerHTML == "NCR") {

                var rp = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + rpl_act).innerHTML);
                var nc = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + current_act).innerHTML);

                var rp_2 = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + rpl_est).innerHTML);
                var nc_2 = parseFloat(document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + current_est).innerHTML);

                var percentage = Number(((rp - nc) / rp) * 100).toFixed(2);
                var percentage_est = Number(((rp_2 - nc_2) / rp_2) * 100).toFixed(2);

                if (parseFloat(rp) == 0 && parseFloat(nc) == 0) {
                    percentage = "0";
                }

                if (parseFloat(rp_2) == 0 && parseFloat(nc_2) == 0) {
                    percentage_est = "0";
                }

                document.getElementById(grd + "_lblpddiff_" + current_act).innerHTML = Number(rp - nc).toFixed(2) + '<span class=ncrlabel>(' + percentage + '%)</span>';
                document.getElementById(grd + "_lblpddiff_" + current_est).innerHTML = Number(rp_2 - nc_2).toFixed(2) + '<span class=ncrlabel>(' + percentage_est + '%)</span>';

            }
            else {
                document.getElementById(grd + "_lblpddiff_" + rpl_act).style.display = 'none';
                document.getElementById(grd + "_lblpddiff_" + rpl_est).style.display = 'none';

            }



            /*
             var rplval_1;
             var rplval_2;
             
 
             if (document.getElementById(grd + "_lblEnumDescription_" + mainid).innerHTML == "RPL") {
 
                 rplval_1 = document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + mainid).innerHTML;
                 var rplindex = parseInt(mainid) + 2;
                 rplval_2 = document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + rplindex).innerHTML;
 
                
 
             }
             else {
                 var rplindex1 = parseInt(mainid) - 1;
                 rplval_1 = document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + mainid).innerHTML;
                 var rplindex = parseInt(rplindex1) + 2;
                 rplval_2 = document.getElementById(grd + "_SuggestedPriceWithPDttxt_lbl_" + rplindex).innerHTML;
 
                
             }
 
             if (document.getElementById(grd + "_lblEnumDescription_" + mainid).innerHTML == "NCR") {
 
                 document.getElementById(grd + "_lblpddiff_" + mainid).innerHTML = rplval_1 + '@' + rplval_2;
                 var rplindex = parseInt(rplindex1) + 2;
                 document.getElementById(grd + "_lblpddiff_" + rplindex).innerHTML = rplval_1 + '@' + rplval_2;
             }
             */
            /*

            lblFinalNAV_sub = document.getElementById(grd + "_lblFinalNRV_" + newid);

            
            var final_sub = lblFinalNAV_sub.innerHTML;
            var total_sub = parseFloat(pd.value) + parseFloat(qd.value) + parseFloat(final_sub);

            var percentage_sub = total_sub * (parseFloat(txtprofiramt.value) / 100);

            document.getElementById(grd + "_SuggestedPriceWithPDttxt_" + newid).innerHTML = Number(total_sub + percentage_sub).toFixed(2);
            document.getElementById(grd + "_ProfitAmttxt_" + newid).innerHTML = Number(percentage_sub).toFixed(2);
            */
            /*
            var tid = parseInt(newid) - 1;

            var lblFinalNAV_sub = document.getElementById(grd + "_lblFinalNRV_" + tid);
            */
        }
        function calculategrossprofit(mainid, grd, type) {

            var status = document.getElementById("<%=lblactest.ClientID%>").innerHTML;
            var status_cal = document.getElementById(grd + "_lblNewStatus_" + mainid);

            var act_est = "";

            act_est = document.getElementById(grd + "_lblStatus_" + mainid).innerHTML.trim().toUpperCase();

            var lblFinalNAV = "";
            var pd_lbl = "";
            var qd_lbl = "";
            var tod_lbl = "";
            var finalprice = "";
            var newid = 0;
            var lblindex = 0;
            var qdpd = 0;
            var AdditionalPDtxt = "";
            var GrossProfitAmounttxt = "";
            var GrossProfitAmountper = "";
            var TotalExpencetxt = "";
            var NetProfitAmounttxt = "";
            var NetProfitAmounttxt_per = "";



            FinalPricettxt = document.getElementById(grd + "_FinalPricettxt_" + mainid);

            AdditionalPDtxt = document.getElementById(grd + "_AdditionalPDtxt_" + mainid);

            GrossProfitAmounttxt = document.getElementById(grd + "_GrossProfitAmounttxt_" + mainid);

            var GrossProfitAmounttxt_lbl = document.getElementById(grd + "_GrossProfitAmounttxt_lbl_" + mainid);

            GrossProfitAmountper = document.getElementById(grd + "_lblGrossProfitPer_" + mainid);

            var GrossProfitAmountper_lbl = document.getElementById(grd + "_lblGrossProfitPer_lbl_" + mainid);

            TotalExpencetxt = document.getElementById(grd + "_TotalExpencetxt_" + mainid);

            var TotalExpencetxt_lbl = document.getElementById(grd + "_TotalExpencetxt_lbl_" + mainid);

            NetProfitAmounttxt = document.getElementById(grd + "_NetProfitAmounttxt_" + mainid);

            var NetProfitAmounttxt_lbl = document.getElementById(grd + "_NetProfitAmounttxt_lbl_" + mainid);

            NetProfitAmounttxt_per = document.getElementById(grd + "_lblNetProfitAmtPer_" + mainid);

            var NetProfitAmounttxt_per_lbl = document.getElementById(grd + "_lblNetProfitAmtPer_lbl_" + mainid);

            var expenseind = 0;

            var f_act;
            var f_est;
            var diff_act;

            var prdiff;

            var newchangeid;
            var newchangeid2;

           // console.log(document.getElementById("<%=hdngrdcalc.ClientID%>").value);
            if (document.getElementById("<%=hdngrdcalc.ClientID%>").value == '1') {

                

                if (document.getElementById(grd + "_lblStatus_" + mainid).innerHTML == "Actual") {

                    lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + mainid);

                    pd_lbl = document.getElementById(grd + "_PDtxt_" + mainid);
                    qd_lbl = document.getElementById(grd + "_QDtxt_" + mainid);
                    tod_lbl = document.getElementById(grd + "_TODtxt_" + mainid);

                    f_act = parseInt(mainid);
                    f_est = parseInt(mainid) + 2;
                    diff_act = parseInt(mainid) + 2;

                    prdiff = mainid;

                }
                else {

                    var tpid = parseInt(mainid) - 2;
                    var pdid = parseInt(mainid) - 1;

                    lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + tpid);

                    pd_lbl = document.getElementById(grd + "_PDtxt_" + pdid);
                    qd_lbl = document.getElementById(grd + "_QDtxt_" + pdid);
                    tod_lbl = document.getElementById(grd + "_TODtxt_" + pdid);


                    f_act = parseInt(mainid) - 2;
                    f_est = parseInt(mainid);
                    diff_act = parseInt(mainid);

                    prdiff = parseInt(mainid) - 1;

                }

                if (document.getElementById(grd + "_lblNewStatus_" + mainid).innerHTML == "RPL") {                   

                    newchangeid = parseInt(mainid);
                }
                else {
                    
                    newchangeid = parseInt(mainid)-2;
                }





                var grossprofit = parseFloat(FinalPricettxt.value) - parseFloat(AdditionalPDtxt.value) - parseFloat(lblFinalNAV.innerHTML) - parseFloat(pd_lbl.value) - parseFloat(qd_lbl.value) - parseFloat(tod_lbl.value);

                GrossProfitAmounttxt.value = Number(grossprofit).toFixed(2);

                GrossProfitAmounttxt_lbl.innerHTML = Number(grossprofit).toFixed(2);


                var grossprofit_per = Number(grossprofit * (100 / parseFloat(lblFinalNAV.innerHTML))).toFixed(2);

                GrossProfitAmountper.value = Number(grossprofit_per).toFixed(2);

                GrossProfitAmountper_lbl.innerHTML = Number(grossprofit_per).toFixed(2);





                var lblStaffExpense = document.getElementById(grd + "_lblStaffExpense_" + mainid);
                var lblMarketing = document.getElementById(grd + "_lblMarketing_" + mainid);
                var lblIncentive = document.getElementById(grd + "_lblIncentive_" + mainid);
                var lblInterest = document.getElementById(grd + "_lblInterest_" + mainid);
                var lblDepoExpence = document.getElementById(grd + "_lblDepoExpence_" + mainid);
                var lblOther = document.getElementById(grd + "_lblOther_" + mainid);


                console.log(FinalPricettxt.value + '@' + lblStaffExpense.innerHTML + '@' + lblDepoExpence.innerHTML);

                var expense = 0.0;

                var TotalExpencetxt = document.getElementById(grd + "_TotalExpencetxt_" + mainid);

                var TotalExpencetxt_lbl = document.getElementById(grd + "_TotalExpencetxt_lbl_" + mainid);

                expense = (parseFloat(FinalPricettxt.value) * parseFloat(lblStaffExpense.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblMarketing.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblIncentive.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblInterest.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblDepoExpence.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblOther.innerHTML) / 100)

                TotalExpencetxt.value = Number(expense).toFixed(2);

                TotalExpencetxt_lbl.innerHTML = Number(expense).toFixed(2);

                var netprofit = parseFloat(grossprofit) - parseFloat(TotalExpencetxt.value);

                NetProfitAmounttxt.value = Number(netprofit).toFixed(2);

                NetProfitAmounttxt_lbl.innerHTML = Number(netprofit).toFixed(2);

                var NetProfit_per = netprofit * (100 / parseFloat(lblFinalNAV.innerHTML));

                NetProfitAmounttxt_per.value = Number(NetProfit_per).toFixed(2);

                NetProfitAmounttxt_per_lbl.innerHTML = Number(NetProfit_per).toFixed(2) + ' % ';


                var lbexnav = parseFloat(document.getElementById(grd + "_lbexnav_" + newchangeid).innerHTML);


                var lblamtexp = parseFloat(document.getElementById(grd + "_lblamtexp_" + prdiff).innerHTML);


                var lblt = Number(lbexnav + lblamtexp).toFixed(2);

                var lbnav = parseFloat(document.getElementById(grd + "_lblFinalNRV_" + mainid).innerHTML);
                document.getElementById(grd + "_lblgpamt_" + mainid).innerHTML = document.getElementById(grd + "_lbexnav_" + newchangeid).innerHTML + "+" + document.getElementById(grd + "_lblamtexp_" + prdiff).innerHTML + "(" + lblt + ")";

                document.getElementById(grd + "_lblgpamtper_" + mainid).innerHTML = Number((lblt * parseFloat(100)) / (lbnav - lbexnav)).toFixed(2) + '%';


                var rp = document.getElementById(grd + "_FinalPricettxt_" + f_act).value;
                var nc = document.getElementById(grd + "_FinalPricettxt_" + f_est).value;

                var percentage = Number(((rp - nc) / rp) * 100).toFixed(2);


                if (parseFloat(rp) == 0 && parseFloat(nc) == 0) {
                    percentage = "0";
                }


                document.getElementById(grd + "_lblpricediff_" + diff_act).style.backgroundColor = 'white';

                document.getElementById(grd + "_lblpricediff_" + diff_act).innerHTML = Number(rp - nc).toFixed(2) + '<span class=ncrlabel>(' + percentage + '%)</span>';



            }
            else {

                if (document.getElementById(grd + "_lblNewStatus_" + mainid).innerHTML == "RPL") {
                    f_act = parseInt(mainid);
                    f_est = parseInt(mainid) + 2;
                    diff_act = parseInt(mainid) + 2;

                    newchangeid = parseInt(mainid) + 2;
                    newchangeid2 =  parseInt(mainid) + 2;
                }
                else {
                    f_act = parseInt(mainid) - 2;
                    f_est = parseInt(mainid);
                    diff_act = parseInt(mainid);
                    newchangeid = parseInt(mainid);
                    newchangeid2  = parseInt(mainid) + 1;
                }

                var rp = document.getElementById(grd + "_FinalPricettxt_" + f_act).value;
                var nc = document.getElementById(grd + "_FinalPricettxt_" + f_est).value;

                var percentage = Number(((rp - nc) / rp) * 100).toFixed(2);


                if (parseFloat(rp) == 0 && parseFloat(nc) == 0) {
                    percentage = "0";
                }



                document.getElementById(grd + "_lblpricediff_" + diff_act).style.backgroundColor = 'white';


                document.getElementById(grd + "_lblpricediff_" + diff_act).innerHTML = Number(rp - nc).toFixed(2) + '<span class=ncrlabel>(' + percentage + '%)</span>';



                if (status.trim().toUpperCase() == "ACTUAL") {


                    if (act_est == "ACTUAL") {

                        lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + mainid);

                        expenseind = parseInt(mainid);

                    }
                    else {

                        var tid = parseInt(mainid) + 2;
                        lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + tid);



                        expenseind = parseInt(mainid) + 1;

                    }

                    if (status_cal.innerHTML.trim().toUpperCase() == "RPL") {

                        lblindex = mainid;
                        qdpd = mainid;
                    }
                    else {
                        lblindex = parseInt(mainid) + 1;
                        qdpd = parseInt(mainid) + 1;
                    }
                }
                else {

                    var id = parseInt(mainid) + 2;

                    if (act_est == "ACTUAL") {

                        var tid = parseInt(mainid) + 2;
                        lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + tid);
                        expenseind = parseInt(mainid);
                    }
                    else {

                        lblFinalNAV = document.getElementById(grd + "_lblFinalNRV_" + mainid);

                        expenseind = parseInt(mainid) + 1;
                    }

                    if (status_cal.innerHTML.trim().toUpperCase() == "RPL") {
                        lblindex = mainid;
                        qdpd = parseInt(id);
                    }
                    else {
                        lblindex = parseInt(mainid) + 1;
                        qdpd = parseInt(mainid) + 1;
                    }

                }


                pd_lbl = document.getElementById(grd + "_PDtxt_" + qdpd);
                qd_lbl = document.getElementById(grd + "_QDtxt_" + qdpd);
                tod_lbl = document.getElementById(grd + "_TODtxt_" + qdpd);


                var grossprofit = parseFloat(FinalPricettxt.value) - parseFloat(AdditionalPDtxt.value) - parseFloat(lblFinalNAV.innerHTML) - parseFloat(pd_lbl.value) - parseFloat(qd_lbl.value) - parseFloat(tod_lbl.value);

                GrossProfitAmounttxt.value = Number(grossprofit).toFixed(2);

                GrossProfitAmounttxt_lbl.innerHTML = Number(grossprofit).toFixed(2);


                var grossprofit_per = Number(grossprofit * (100 / parseFloat(lblFinalNAV.innerHTML))).toFixed(2);

                GrossProfitAmountper.value = Number(grossprofit_per).toFixed(2);

                GrossProfitAmountper_lbl.innerHTML = Number(grossprofit_per).toFixed(2);



                var lblStaffExpense = document.getElementById(grd + "_lblStaffExpense_" + expenseind);
                var lblMarketing = document.getElementById(grd + "_lblMarketing_" + expenseind);
                var lblIncentive = document.getElementById(grd + "_lblIncentive_" + expenseind);
                var lblInterest = document.getElementById(grd + "_lblInterest_" + expenseind);
                var lblDepoExpence = document.getElementById(grd + "_lblDepoExpence_" + expenseind);
                var lblOther = document.getElementById(grd + "_lblOther_" + expenseind);

                console.log(FinalPricettxt.value + '@' + lblStaffExpense.innerHTML + '@' + lblDepoExpence.innerHTML);

                var expense = 0.0;

                var TotalExpencetxt = document.getElementById(grd + "_TotalExpencetxt_" + mainid);

                var TotalExpencetxt_lbl = document.getElementById(grd + "_TotalExpencetxt_lbl_" + mainid);

                expense = (parseFloat(FinalPricettxt.value) * parseFloat(lblStaffExpense.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblMarketing.innerHTML) / 100) + + (parseFloat(FinalPricettxt.value) * parseFloat(lblIncentive.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblInterest.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblDepoExpence.innerHTML) / 100) + (parseFloat(FinalPricettxt.value) * parseFloat(lblOther.innerHTML) / 100)

                TotalExpencetxt.value = Number(expense).toFixed(2);

                TotalExpencetxt_lbl.innerHTML = Number(expense).toFixed(2);

                var netprofit = parseFloat(grossprofit) - parseFloat(TotalExpencetxt.value);

                NetProfitAmounttxt.value = Number(netprofit).toFixed(2);

                NetProfitAmounttxt_lbl.innerHTML = Number(netprofit).toFixed(2);

                var NetProfit_per = netprofit * (100 / parseFloat(lblFinalNAV.innerHTML));

                NetProfitAmounttxt_per.value = Number(NetProfit_per).toFixed(2);

                NetProfitAmounttxt_per_lbl.innerHTML = Number(NetProfit_per).toFixed(2) + ' % ';


                var lbexnav = parseFloat(document.getElementById(grd + "_lbexnav_" + newchangeid).innerHTML);


                var lblamtexp = parseFloat(document.getElementById(grd + "_lblamtexp_" + newchangeid2).innerHTML);
                var lblt = Number(lbexnav + lblamtexp).toFixed(2);
                var lbnav = parseFloat(document.getElementById(grd + "_lblFinalNRV_" + mainid).innerHTML);


               

                document.getElementById(grd + "_lblgpamt_" + mainid).innerHTML = document.getElementById(grd + "_lbexnav_" + newchangeid).innerHTML + "+" + document.getElementById(grd + "_lblamtexp_" + newchangeid2).innerHTML + "(" + lblt + ")";

                document.getElementById(grd + "_lblgpamtper_" + mainid).innerHTML = Number((lblt * parseFloat(100)) / (lbnav - lbexnav)).toFixed(2) + '%';


                var lblamtexp_2 = parseFloat(document.getElementById(grd + "_lblamtexp_" + newchangeid2).innerHTML);
                var lblt_2 = Number(lbexnav + lblamtexp_2).toFixed(2);
                var lbnav_2 = parseFloat(document.getElementById(grd + "_lblFinalNRV_" + id).innerHTML);
                document.getElementById(grd + "_lblgpamt_" + id).innerHTML = document.getElementById(grd + "_lbexnav_" + newchangeid).innerHTML + "+" + document.getElementById(grd + "_lblamtexp_" + newchangeid2).innerHTML + "(" + lblt_2 + ")";


                document.getElementById(grd + "_lblgpamtper_" + id).innerHTML = Number((lblt_2 * 100) / (lbnav_2 - lbexnav)).toFixed(2) + '%';

            }

        }

        function EnableLoader(flag) {
            if (flag) {
                document.getElementById("dvLoadingCSS").style.display = "";
            }
            else {
                document.getElementById("dvLoadingCSS").style.display = "none";
            }
        }

        $(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        EnableLoader(false);

                    }
                });
            }

        });

        $(function () {
            var hdnIsEstimate = document.getElementById('<%=hdnIsEstimate.ClientID %>').value;
            if (hdnIsEstimate != 0) {
                document.getElementById("ReportEstimate").style.visibility = "visible";

            }
            else {

                document.getElementById("ReportEstimate").style.visibility = "hidden";

            }

        });

        function btnActualReport() {

            var FkBulkproductId = document.getElementById('<%=btnFkBulkproductId.ClientID %>').value;
            var hdnIsEstimate = document.getElementById('<%=hdnIsEstimate.ClientID %>').value;
            var dvdetailcontent = document.getElementById('<%=dvdetailcontent.ClientID %>');
            var BulkProductNameReport = document.getElementById('<%=hdnbulkProductName.ClientID %>');

            $.ajax({
                url: 'WebService.asmx/GetReportbyBPMId',
                data: { productid: FkBulkproductId, IsEstimate: 0 },
                dataType: "xml",
                method: 'POST',
                success: function (r) {
                    var Actual = BulkProductNameReport.value + " [Actual]"
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

        function btnEstimateReport() {

            var FkBulkproductId = document.getElementById('<%=btnFkBulkproductId.ClientID %>').value;
            var BulkProductNameReport = document.getElementById('<%=hdnbulkProductName.ClientID %>');
            var hdnIsEstimate = document.getElementById('<%=hdnIsEstimate.ClientID %>').value;
            var dvdetailcontent = document.getElementById('<%=dvdetailcontent.ClientID %>');

            $.ajax({
                url: 'WebService.asmx/GetReportbyBPMId',
                data: { productid: FkBulkproductId, IsEstimate: hdnIsEstimate },
                dataType: "xml",
                method: 'POST',
                success: function (r) {
                    var Estimate = BulkProductNameReport.value + " [Estimate]"
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

        function showhidemodel(type) {

            if (type == '1') {
                $('#exampleModal').modal("show");
            }
            else {
                $('#exampleModal').modal('hide');
            }
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

        .txtlbl {
            border: none;
            display: none;
        }

        .displaynone {
            display: none;
        }

        .sticky-col {
            position: -webkit-sticky;
            position: fixed;
            top: 0;
            background-color: aliceblue;
        }

        .first-col {
            width: 130px;
            min-width: 130px;
            max-width: 130px;
            left: 0px;
            position: sticky;
        }

        .second-col {
            width: 130px;
            min-width: 130px;
            max-width: 130px;
            left: 100px;
            position: sticky;
        }

        #dvgd tr:first-child {
            position: sticky;
            top: 0;
        }

        .input-group {
            position: unset !important;
        }

        .postext {
            position: unset !important;
        }

        .modalLoading {
            position: fixed;
            z-index: 1060;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: grey;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .centerLoading {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }

        /**/
    </style>

</asp:Content>
