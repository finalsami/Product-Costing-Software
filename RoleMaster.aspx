<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoleMaster.aspx.cs" Inherits="Production_Costing_Software.RoleMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Role Master </b>

                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="card-body">
                            <asp:Button ID="btnopenrole" CssClass="btn btn-primary" runat="server" OnClick="btnopenrol_Click" Text="Add" />


                            <div class="container col-12">
                                <br />
                                <br />
                            </div>
                            <div class="container col-12">
                                <asp:GridView ID="gvrolemaster" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="GroupName" HeaderText="Group Name" />

                                        <asp:TemplateField HeaderText="Is Active">
                                            <ItemTemplate>
                                                <div class="form-check form-switch" style="margin-left: 65px">
                                                    <input type="checkbox" runat="server" disabled class="form-check-input " checked='<%#  Eval("isActive") %>'>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                                              
                                                    <asp:Button ID="btnrights" Text="Rights" runat="server" OnClientClick="popupUserrights();" CssClass="btn btn-sm btn-secondary" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("GroupId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("GroupId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>

                            </div>

                        </div>


                        <div class="modal fade" id="modalpopup" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog ">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">Role Information:</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="container col-12 ">
                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:ValidationSummary ID="ValidationSummary3" CssClass="text-danger" runat="server" ValidationGroup="g3" />
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">Role</span>
                                                        </div>
                                                        <asp:TextBox ID="txtrole" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtrole" runat="server" placeholder="Role" ControlToValidate="txtrole" Display="None" ValidationGroup="g3" ErrorMessage="Enter Role" />
                                                    </div>
                                                    <div class="input-group mb-3">
                                                        <div class="form-check form-switch">
                                                            <input type="checkbox" id="IsActive" runat="server" class="form-check-input ">
                                                            <label class="form-check-label" for="flexSwitchCheckDefault">Active/DeActive</label>
                                                        </div>
                                                    </div>
                                                    <asp:HiddenField runat="server" ID="hdnroleid" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="modal-footer">
                                            <asp:Button ID="btnupdate" CssClass="btn btn-secondary" runat="server" OnClick="btnupdate_Click" Text="Update" />
                                            <asp:Button ID="btnadd" CssClass="btn btn-primary" runat="server"  CausesValidation="true" OnClick="btnadd_Click" Text="Add" />
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
                $('#<%= gvrolemaster.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvrolemaster.ClientID %>').find("tr:first"))).DataTable({
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

        function popupUserrights(Type) {

            if (Type == "1") {
                $('#modalpopup').modal('show')
              document.getElementById("<%=btnadd.ClientID%>").style.display = "";
                document.getElementById("<%=btnupdate.ClientID%>").style.display = "none";
            }
            else if (Type == "2") {
                var txtrole = document.getElementById("<%=txtrole.ClientID%>").value;
              
                var IsActive = document.getElementById("<%=IsActive.ClientID%>");
                var hdnroleid = document.getElementById("<%=hdnroleid.ClientID%>");


                txtrole.value = parseFloat(txtrole.value) || '';



                $('#modalpopup').modal('show')
             document.getElementById("<%=btnadd.ClientID%>").style.display = "none";
                document.getElementById("<%=btnupdate.ClientID%>").style.display = "";
            }
            else {
                $('#modalpopup').modal('hide')

            }

        }
    </script>
</asp:Content>
