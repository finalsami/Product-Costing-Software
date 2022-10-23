<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RMMaster.aspx.cs" Inherits="Production_Costing_Software.RMMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>RM Master </b>
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
                                                <span class="input-group-text">RM Category</span>
                                            </div>
                                            <asp:DropDownList ID="drprmcategory" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drprmcategory" runat="server" InitialValue="0" ControlToValidate="drprmcategory" Display="None" ValidationGroup="g1" ErrorMessage="Select RM Category" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">RM Name</span>
                                            </div>
                                            <asp:TextBox ID="txtrmname" class="form-control" type="text" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_RmNametxt" runat="server" ControlToValidate="txtrmname" Display="None" ValidationGroup="g1" ErrorMessage="Enter RM Name" />
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

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">IsPurity Required</span>
                                            </div>
                                            <div class="form-control">
                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox runat="server" CssClass="form-check-input" ID="chkpurity" />
                                                <label class="form-check-label" for="flexSwitchCheckDefault">Yes/No</label>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-6">

                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdnrmid" runat="server" />
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
                                <asp:GridView ID="gvrmmaster" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RMCategoryName" HeaderText="RM Category Name" />
                                        <asp:BoundField DataField="RMName" HeaderText="RM Name" />
                                        <asp:TemplateField HeaderText="Is Purity">
                                            <ItemTemplate>
                                                <div class="form-check form-switch" style="margin-left: 65px">
                                                    <input type="checkbox" runat="server" disabled class="form-check-input " checked='<%#  Eval("isPurity") %>'>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("RmId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("RmId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
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
                $('#<%= gvrmmaster.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvrmmaster.ClientID %>').find("tr:first"))).DataTable({
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

