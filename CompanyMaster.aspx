<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyMaster.aspx.cs" Inherits="Production_Costing_Software.CompanyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="contain">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Company Name </b>
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
                                                <span class="input-group-text">Company Name</span>
                                            </div>
                                            <asp:TextBox ID="txtcname" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtcname" runat="server" ControlToValidate="txtcname" Display="None" ValidationGroup="g1" ErrorMessage="Enter Comapny Name" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="form-control rbl">
                                                <asp:RadioButtonList ID="rdotype" RepeatDirection="Horizontal" runat="server">
                                                    <asp:ListItem Text="Individual Packing" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Packing Difference from Master" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdnIsPackingMaster" runat="server" />
                                        <asp:HiddenField ID="hdncompid" runat="server" />
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="container col-12">
                            <asp:GridView ID="gvcompany" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview" ClientIDMode="Static">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company" />

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("CompanyId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                            <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("CompanyId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
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
                $('#<%= gvcompany.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvcompany.ClientID %>').find("tr:first"))).DataTable({
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
