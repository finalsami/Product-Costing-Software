<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinishGoodsPricingReport.aspx.cs" Inherits="Production_Costing_Software.FinishGoodsPricingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="contain">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Finish Good Pricing Report </b>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="card-body">                           
                        </div>

                        <div class="container col-12">
                            <asp:GridView ID="gvfinishgood" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview" ClientIDMode="Static">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="productname" HeaderText="Product Name" />
                                    <asp:BoundField DataField="packingsize" HeaderText="Packing Size" />
                                    <asp:BoundField DataField="bulkcost" HeaderText="Bulk Cost(with Interest)" />
                                    <asp:BoundField DataField="pmcost" HeaderText="PM Cost/Unit" />
                                    <asp:BoundField DataField="labourcost" HeaderText="Labour Cost/Unit" />
                                    <asp:BoundField DataField="packingloss" HeaderText="Packing Loss" />
                                    <asp:BoundField DataField="AmountUnit" HeaderText="Total Amount /Unit" />
                                    <asp:BoundField DataField="FinalNAV" HeaderText="Final NAV" />
                                       <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                            <asp:Button ID="btnreport" class="btn btn-success btn-sm" CommandArgument='<%#  Eval("FkBulkProductId") %>' OnClick="btnreport_Click1" runat="server" Text="Report" />
                                           
                                      </ItemTemplate>
                                           </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>

                         <div class="modal fade" id="showdetail" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-xl">
                                <div class="modal-content ">
                                    <div class="modal-header">    
                                        <h3 class="modal-title text-success" id="exampleModalLabel" runat="server">
                                           
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
                $('#<%= gvfinishgood.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvfinishgood.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": false,
                    "autoWidth": true,
                    //"responsive": true,
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

    </script>
    <style>
       
    </style>
</asp:Content>
