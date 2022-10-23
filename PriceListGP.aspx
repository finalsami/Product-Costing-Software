<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PriceListGP.aspx.cs" Inherits="Production_Costing_Software.PriceListGP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>PriceList GP </b>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="card-body">
                            <div class="container col-12 ">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="text-danger" runat="server" ValidationGroup="g1" />
                                    </div>
                                    <div class="col-md-5">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Select Report</span>
                                            </div>
                                            <asp:DropDownList ID="drpreportcat" class="form-control" runat="server">
                                                <asp:ListItem Value="-1" Selected="True">select</asp:ListItem>
                                                <asp:ListItem Value="0">pdf</asp:ListItem>
                                                <asp:ListItem Value="1">excel</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpreportcat" runat="server" InitialValue="0" ControlToValidate="drpreportcat" Display="None" ValidationGroup="g1" ErrorMessage="Select Report Category" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">PriceList</span>
                                            </div>
                                            <asp:TextBox ID="txtcrtpricelist" onchange="changecreate()" class="form-control" type="text" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtcrtpricelist" runat="server" ControlToValidate="txtcrtpricelist" Display="None" ValidationGroup="g1" ErrorMessage="Enter Price List" />
                                            <asp:Button ID="btnsaveCreate" CssClass="btn btn-info" Enabled="false" Style="margin-left: 5px" OnClick="btnsaveCreate_Click" Text="Create PriceList" runat="server" />

                                        </div>
                                        <asp:Button ID="btnReportAllState" CssClass="btn btn-secondary " OnClick="btnReportAllState_Click" Text="All State Report" runat="server" />
                                        <%--<asp:Button ID="btnEdit" CssClass="btn btn-secondary" Text="Edit" runat="server" OnClick="btnEdit_Click" />--%>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Button ID="btnstatewisereport" OnClick="btnstatewisereport_Click" CssClass="btn btn-success" Text="Statewise Report" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Select State</span>
                                            </div>
                                            <asp:DropDownList ID="drpstate" class="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <asp:Label ID="lblDynamicColumnCount" runat="server" Text="" Visible="false"></asp:Label>
                            <div class="container col-12">
                                <br />
                                <br />
                            </div>
                            <div class="container col-12">
                                <asp:GridView ID="gvpricelistgpactual" runat="server" AutoGenerateColumns="true" EnableViewState="true" OnRowCreated="gvpricelistgpactual_RowCreated" OnRowDataBound="gvpricelistgpactual_RowDataBound" class="table table-bordered table-striped gridview" ClientIDMode="Static">
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                                  <asp:GridView ID="gvpricelistgpactualExcel" runat="server" AutoGenerateColumns="true" EnableViewState="true" style="display:none" OnRowCreated="gvpricelistgpactualExcel_RowCreated" OnRowDataBound="gvpricelistgpactualExcel_RowDataBound" class="table table-bordered table-striped gridview" ClientIDMode="Static">
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </div>
                               
                            <div class="container col-12">
                                <asp:GridView ID="gvstatewisereport" runat="server" AutoGenerateColumns="false" EnableViewState="true" OnRowDataBound="gvstatewisereport_RowDataBound"  class="table table-bordered table-striped gridview" ClientIDMode="Static">
                                    <Columns>
                                        <%--<asp:BoundField DataField="Status" HeaderText="" />--%>
                                        <%--<asp:BoundField DataField="NewStatus" HeaderText="" />--%>
                                        <asp:BoundField DataField="BulkProductName" HeaderText="ProductName" />
                                        <asp:BoundField DataField="TradeName" HeaderText="Technical Name" />
                                        <asp:BoundField DataField="PackMeasure" HeaderText="PackMeasure" />
                                        <asp:BoundField DataField="RPL1Price/L or KG" HeaderText="Price/L or KG (RPL)" />
                                        <asp:BoundField DataField="RPL1PD" HeaderText="PD" />
                                        <asp:BoundField DataField="RPL1QD" HeaderText="QD" />
                                        <asp:BoundField DataField="NCR1Price/L or KG" HeaderText="Price/L or KG (NCR)" />
                                        <asp:BoundField DataField="NCR1PD" HeaderText="PD" />
                                        <asp:BoundField DataField="NCR1QD" HeaderText="QD" />

                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                        <asp:Label ID="lblStateName" runat="server"></asp:Label>
                        <asp:HiddenField runat="server" ID="hfDynamicCompId" />

                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:PostBackTrigger ControlID="btnExcelReport" />--%>
                        <asp:PostBackTrigger ControlID="btnReportAllState" />
                        <asp:PostBackTrigger ControlID="btnstatewisereport" />

                    </Triggers>
                </asp:UpdatePanel>
                <asp:Table runat="server" ID="ExcelReport" Style="display: none"></asp:Table>
            </div>
        </div>
    </div>
    <script type="text/javascript">

       

        function DynamicClick(CompanyId, bpmid, PriceListActualEstimate) {
            document.getElementById('<%=hfDynamicCompId.ClientID %>').value = CompanyId;
            var strURL = "";
            strURL = document.URL.substring(0, document.URL.length - document.URL.indexOf("PriceListGP.aspx"));
            if (CompanyId == "1") {
                strURL += "/PriceListGP.aspx?CmpId=1&bpmId=" + bpmid;
            }
            else {
                strURL += "/EstimatePriceList.aspx?CmpId=" + CompanyId + "&EstimateName=" + Estimate + "&Status=" + Status;
            }
            window.location.href = strURL;
            return false;
        }
        function changecreate() {
            var txtCreatePrice = document.getElementById('<%=txtcrtpricelist.ClientID %>');
            var btnsaveCreate = document.getElementById('<%=btnsaveCreate.ClientID %>').value;

            if (txtCreatePrice.value != "") {

                document.getElementById('<%=btnsaveCreate.ClientID %>').disabled = false;
            }
            else {

                document.getElementById('<%=btnsaveCreate.ClientID %>').disabled = true;
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
    </style>
</asp:Content>
