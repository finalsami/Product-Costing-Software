<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PackingSizeCategory.aspx.cs" Inherits="Production_Costing_Software.PackingSizeCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Packing Size Category</b>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                                <span class="input-group-text">Packing  Category</span>
                                            </div>
                                            <asp:DropDownList ID="drppackingcategory" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drppackingcategory" runat="server" InitialValue="0" ControlToValidate="drppackingcategory" Display="None" ValidationGroup="g1" ErrorMessage="Select Packing  Category" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Packing Size</span>
                                            </div>
                                            <asp:TextBox ID="txtpackingsize" class="form-control" type="text" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_PackingSizetxt" runat="server" ControlToValidate="txtpackingsize" Display="None" ValidationGroup="g1" ErrorMessage="Enter Packing Size" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
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
                                        <asp:HiddenField ID="hdpscid" runat="server" />
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
                                <asp:GridView ID="gvpackingsizecategory" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview" ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PackingCategoryName" HeaderText="Packing Category Name" />
                                        <asp:BoundField DataField="PackingSize" HeaderText="Packing Size" />
                                        <asp:BoundField DataField="Measurement" HeaderText="Measurement" />
                                   
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("PackingSizeCategoryId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("PackingSizeCategoryId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
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
                $('#<%= gvpackingsizecategory.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvpackingsizecategory.ClientID %>').find("tr:first"))).DataTable({
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
