<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RMEstimate.aspx.cs" Inherits="Production_Costing_Software.RMEstimate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>RM Estimate</b>
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
                                                <span class="input-group-text">Estimate Name </span>
                                            </div>
                                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_drprmcategory" runat="server" ControlToValidate="txtname" Display="None" ValidationGroup="g1" ErrorMessage="Enter Extimate Name" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Estimate Date</span>
                                            </div>
                                            <asp:TextBox ID="txtestdate" class="form-control" type="date" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtdop" runat="server" InitialValue="0" ControlToValidate="txtestdate" Display="None" ValidationGroup="g1" ErrorMessage="Select Date of Estimate" />
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">RM Category</span>
                                            </div>
                                            <asp:DropDownList ID="drpcategory" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpcategory_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_txtpackingcategory" runat="server" InitialValue="0" ControlToValidate="drpcategory" Display="None" ValidationGroup="g1" ErrorMessage="Select RM Category" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">RM</span>
                                            </div>
                                            <asp:ListBox ID="drprmlist" Rows="7" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdnestimateid" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <br />
                                        <br />
                                    </div>
                                    <div class="container col-12">
                                        <asp:GridView ID="gvdetail" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="RMName" HeaderText="RM" />
                                                <asp:BoundField DataField="ActualPrice" HeaderText="Actual Rate" />
                                                <asp:TemplateField HeaderText="Modified Price">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtprice" runat="server" Text='<%#  Eval("RMNewPrice") %>' CssClass="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="txtpid" runat="server" Style="display: none" Text='<%#  Eval("RMEstimateDetailId") %>'></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>

                                                        <asp:Button ID="btnDeletePrice" Text="Delete" runat="server" CommandArgument='<%#  Eval("RMEstimateDetailId") %>' OnClick="btnDeletePrice_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="container col-12">
                                        <asp:Button ID="btnupdatePrice" Visible="false" CssClass="btn btn-success" OnClick="btnupdatePrice_Click" CausesValidation="true" runat="server" Text="Update" />
                                    </div>
                                </div>
                            </div>
                            <div class="container col-12">
                                <br />
                                <br />
                            </div>
                            <div class="container col-12">
                                <asp:GridView ID="gvRMEstimate" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EstimateDate1" HeaderText="Date" />
                                        <asp:BoundField DataField="EstimateName" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("RMEstimateId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("RMEstimateId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                                <asp:Button ID="btnreport" Text="Report" runat="server" CommandArgument='<%#  Eval("RMEstimateId") %>' OnClick="btnreport_Click" CssClass="btn btn-sm btn-info" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                        <%--labels--%>
                        <asp:Label ID="lblStatus" Visible="false" runat="server" Text=""></asp:Label>


                        <%--RMEstimateGRid Modal--%>

                        <asp:Button ID="btnDynamic" runat="server" OnClick="btnDynamic_Click" Style="display: none;" />


                        <div class="modal fade" id="RMEstimateReport" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel2" aria-hidden="true">
                            <div class="modal-dialog modal-xl">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" data-dismiss="modal" onclick="showhidemodel('0');" class="btn-close btn-secondary">Close</button>

                                        <div style="color: black">

                                            <h5>Estimated Cost Bulk Report:</h5>
                                            <h6>[<asp:Label ID="EstimateHeader" runat="server" CssClass="font-monospace"></asp:Label>]
                                            </h6>

                                            <%--BulkCount For Company--%>
                                            <asp:Label ID="lblCompanyName" runat="server" Visible="false" CssClass="font-monospace"></asp:Label>
                                        </div>

                                        <asp:Panel ID="Panel1" runat="server" ViewStateMode="Enabled" EnableViewState="true">
                                        </asp:Panel>
                            
                                    </div>
                                    <div class="modal-body">
                                        <div class="card-body" style="overflow: scroll">
                                            <asp:GridView ID="gvestimate" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">


                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BulkProductName" HeaderText="Bulk Name" />
                                                    <asp:BoundField DataField="ActualBulkCost" HeaderText="Actual Cost Bulk/Ltr" />
                                                    <asp:BoundField DataField="ModifiedBulkCost" HeaderText="Modified Bulk Cost/Ltr" />

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnRMEstimatReport" Style="border-radius: 5px; box-shadow: 0px 10px 10px -10px rgba(0, 0, 0, 0.4)" CommandArgument='<%#  Eval("FkBulkProductId") %>' OnClick="btnRMEstimatReport_Click" CausesValidation="false" runat="server" class="btn btn-success btn-sm" Text="View Report" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>

                                </div>

                            </div>
                        </div>


                       <%--Reportpopup--%>
                    <div class="modal fade" id="showdetail" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-xl">
                                <div class="modal-content ">
                                    <div class="modal-header">    
                                        <h3 class="modal-title text-success" id="exampleModalLabel" runat="server">
                                           
                                        </h3>
                                      <%--<button type="button" data-dismiss="modal" onclick="showhidemodel('0');" class="btn-close btn-secondary">Close</button>--%>
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

                        <%-------------%>
       
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:HiddenField ID="hdnCompanyId" runat="server" Value="0" />
        <asp:HiddenField ID="hfDynamicCompId" runat="server" Value="0" />
        <asp:Label ID="lblRMEstimateName" runat="server" Text="" Visible="false"></asp:Label>

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
                $('#<%= gvRMEstimate.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvRMEstimate.ClientID %>').find("tr:first"))).DataTable({
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
        function ShowRMEstimateReport() {
            $("#RMEstimateReport").modal("show");

        }


        function HideRMEstimateReport() {
            $("#RMEstimateReport").modal("hide");

        }

        function showhidemodel(type) {
            if (type == '1') {
                $('#showdetail').modal('show');
            }
            else {
                $('#showdetail').modal('hide');
            }
        }
        function DynamicClick(CompanyId, Estimate, Status) {
            document.getElementById('<%=hfDynamicCompId.ClientID %>').value = CompanyId;
            var strURL = "";
            strURL = document.URL.substring(15, document.URL.length - document.URL.indexOf("RMEstimate.aspx"));
            //alert(document.URL.length, 'document lenght')
            //alert(document.URL.indexOf("RMEstimate.aspx"),'URL index')
            //alert(strURL)
            if (CompanyId == "1" || CompanyId == "0" ) {
                strURL += "/PriceListGP.aspx?CmpId=1&EstimateId=" + Estimate;
            }
            else {
                strURL += "/EstimatePriceList.aspx?CmpId=" + CompanyId + "&EstimateId=" + Estimate + "&Status=" + Status;
            }
            window.location.href = strURL;
            return false;
        }
    </script>

</asp:Content>
