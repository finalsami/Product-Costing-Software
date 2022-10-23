<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PMRMCategory.aspx.cs" Inherits="Production_Costing_Software.PMRMCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="contain">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>PM RM Category</b>
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
                                                <span class="input-group-text">PM RM Category</span>
                                            </div>
                                            <asp:TextBox ID="txtpmrmcategory" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpmrmcategory" runat="server" ControlToValidate="txtpmrmcategory" Display="None" ValidationGroup="g1" ErrorMessage="Enter PM RM Category" />
                                        </div>

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Type</span>
                                            </div>
                                            <div class="form-control rbl">
                                               <%-- &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox runat="server" CssClass="form-check-input" ID="chkIsShipper" />
                                                <label class="form-check-label" for="flexSwitchCheckDefault">Yes/No</label>--%>
                                                <asp:RadioButtonList ID="rdotype" RepeatDirection="Horizontal" runat="server">
                                                    <asp:ListItem Text="PM" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Shipper" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Inner" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6"></div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdnpmrmcid" runat="server" />
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
                                <asp:GridView ID="gvpmrmcategory" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PMRMCategoryName" HeaderText="PM RM Category Name" />
                                         <asp:TemplateField HeaderText="PM">
                                            <ItemTemplate>
                                                <div class="form-check form-switch" style="margin-left: 65px">
                                                    <input type="checkbox" id="chkpm" runat="server" disabled class="form-check-input " checked='<%#  Eval("ChkPM").ToString()=="1"?true:false %>'>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Shipper">
                                            <ItemTemplate>
                                                <div class="form-check form-switch" style="margin-left: 65px">
                                                    <input type="checkbox" id="chkshipper" runat="server" disabled class="form-check-input " checked='<%#  Eval("ChkIsShipper") %>'>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Is Inner">
                                            <ItemTemplate>
                                                <div class="form-check form-switch" style="margin-left: 65px">
                                                    <input type="checkbox" id="chkinner" runat="server" disabled class="form-check-input " checked='<%#  Eval("ChkIsInner") %>'>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("PMRMCategoryId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("PMRMCategoryId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
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
                $('#<%= gvpmrmcategory.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvpmrmcategory.ClientID %>').find("tr:first"))).DataTable({
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
