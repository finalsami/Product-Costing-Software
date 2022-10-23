<%@ Page Title="" Language="C#" MasterPageFile="~/Company.Master" AutoEventWireup="true" CodeBehind="TradeBulkMapping.aspx.cs" Inherits="Production_Costing_Software.TradeBulkMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="card-body">
        <div class="contain">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Trade Bulk Mapping</b>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="card-body">
                            <div class="container col-12 ">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="text-danger" runat="server" ValidationGroup="g1" />
                                    </div>
                                     <div class="col-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Company Name</span>
                                            </div>
                                            <h3 id="lblcompany" class="form-control" runat="server"></h3>
                                        </div>

                                    </div>
                                     <div class="col-md-6">
                                    </div>
                                     <div class="col-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Bulk Product</span>
                                            </div>
                                            <asp:DropDownList ID="drpproduct" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drptrade_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_txtpackingcategory" runat="server" InitialValue="0" ControlToValidate="drpproduct" Display="None" ValidationGroup="g1" ErrorMessage="Select Bulk" />
                                        </div>

                                    </div>
                                     <div class="col-md-6">
                                    </div>
                                    <div class="col-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Trade</span>
                                            </div>
                                            <asp:ListBox id="drptrade" Rows="15" runat="server" CssClass="form-control" SelectionMode="Single"></asp:ListBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ControlToValidate="drptrade" Display="None" ValidationGroup="g1" ErrorMessage="Select Trade" />
                                        </div>

                                    </div>
                                   <div class="col-md-6">
                                    </div>
                                    <div class="col-md-6">
                                        
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />                                      
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdnmcid" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>


                                <div class="container col-12">
                                    <br />
                                    <br />
                                </div>
                            </div>
                            <div class="container col-12">
                                 <asp:GridView ID="gvbulk" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Company" HeaderText="Company" />
                                        <asp:BoundField DataField="TradeName" HeaderText="Trade" />
                                         <asp:BoundField DataField="Product" HeaderText="Product" />

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                                               
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("TradeBulkMappingId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?. Its also deleted from Price List of this trade');" CssClass="btn btn-sm btn-danger" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
                $('#<%= gvbulk.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvbulk.ClientID %>').find("tr:first"))).DataTable({
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
