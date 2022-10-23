<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="UserMaster.aspx.cs" Inherits="Production_Costing_Software.UserMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>User Master </b>

                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="card-body">
                            <asp:Button ID="btnopenuser" CssClass="btn btn-primary" runat="server" OnClick="btnopenuser_Click" Text="Add" />


                            <div class="container col-12">
                                <br />
                                <br />
                            </div>
                            <div class="container col-12">
                                <asp:GridView ID="gvusermaster" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
                                        <asp:BoundField DataField="LastName" HeaderText="LastName" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" />
                                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                                        <asp:TemplateField HeaderText="Is Active">
                                            <ItemTemplate>
                                                <div class="form-check form-switch" style="margin-left: 65px">
                                                    <input type="checkbox" runat="server" disabled class="form-check-input " checked='<%#  Eval("isActive") %>'>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("UserId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("UserId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>

                            </div>

                        </div>


                        <div class="modal fade" id="modalpopup" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-xl">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">User Form:</h5>
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
                                                            <span class="input-group-text">First Name</span>
                                                        </div>
                                                        <asp:TextBox ID="txtfname" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtfname" runat="server" placeholder="First Name" ControlToValidate="txtfname" Display="None" ValidationGroup="g3" ErrorMessage="Enter First Name" />
                                                    </div>
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">Last Name</span>
                                                        </div>
                                                        <asp:TextBox ID="txtlname" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtlname" runat="server" placeholder="Last Name" ControlToValidate="txtlname" Display="None" ValidationGroup="g3" ErrorMessage="Enter Last Name" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">Email</span>
                                                        </div>
                                                        <asp:TextBox ID="txtemail" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtemail" runat="server" placeholder="Email" ControlToValidate="txtemail" Display="None" ValidationGroup="g3" ErrorMessage="Enter Email" />
                                                    </div>
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">Mobile</span>
                                                        </div>
                                                        <asp:TextBox ID="txtmobile" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtmobile" runat="server" placeholder="Mobile" ControlToValidate="txtmobile" Display="None" ValidationGroup="g3" ErrorMessage="Enter Mobile" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">User Name</span>
                                                        </div>
                                                        <asp:TextBox ID="txtusername" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtusername" runat="server" placeholder="Enter User Name" ControlToValidate="txtusername" Display="None" ValidationGroup="g3" ErrorMessage="Enter User Name" />
                                                    </div>
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">Role</span>
                                                        </div>
                                                        <asp:DropDownList ID="drprole" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rf_drprole" runat="server" placeholder="Mobile" ControlToValidate="drprole" Display="None" ValidationGroup="g3" ErrorMessage="Select Role" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">Password</span>
                                                        </div>
                                                        <asp:TextBox ID="txtpassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtpassword" runat="server" placeholder="Password" ControlToValidate="txtpassword" Display="None" ValidationGroup="g3" ErrorMessage="Enter Password" />
                                                    </div>
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">Confirm Password</span>
                                                        </div>
                                                        <asp:TextBox ID="txtcpassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rf_txtcpassword" runat="server" placeholder="Confirm Password" ControlToValidate="txtcpassword" Display="None" ValidationGroup="g3" ErrorMessage="Enter Confirm Password" />
                                                        <asp:CompareValidator ID="cv_txtcpassword" runat="server" ControlToCompare="txtpassword" Display="None" ControlToValidate="txtcpassword" ValidationGroup="g3" ErrorMessage="Password not Match"></asp:CompareValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="input-group">
                                                        <div class="form-check form-switch">

                                                            <input type="checkbox" id="IsActive" runat="server" class="form-check-input ">

                                                            <label class="form-check-label" for="flexSwitchCheckDefault">Active/DeActive</label>
                                                        </div>
                                                    </div>
                                                    <asp:HiddenField runat="server" ID="hdnuserid" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="modal-footer">
                                            <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>--%>
                                            <asp:Button ID="btnupdate" CssClass="btn btn-secondary" runat="server"  OnClick="btnupdate_Click" Text="Update" />
                                            <asp:Button ID="btnadd" CssClass="btn btn-primary" runat="server" CausesValidation="true" OnClick="btnadd_Click" Text="Add" />
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
                $('#<%= gvusermaster.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvusermaster.ClientID %>').find("tr:first"))).DataTable({
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

        function userpopup(Type) {



            if (Type == "1") {
                $('#modalpopup').modal('show')
<%--                document.getElementById("<%=btnadd.ClientID%>").style.display = "";
                document.getElementById("<%=btnupdate.ClientID%>").style.display = "none";--%>
            }
            else if (Type == "2") {
                var txtfname = document.getElementById("<%=txtfname.ClientID%>").value;
                var txtlname = document.getElementById("<%=txtlname.ClientID%>");
                var txtpassword = document.getElementById("<%=txtpassword.ClientID%>");
                var txtcpassword = document.getElementById("<%=txtcpassword.ClientID%>");
                var txtemail = document.getElementById("<%=txtemail.ClientID%>");
                var txtmobile = document.getElementById("<%=txtmobile.ClientID%>");
                var drprole = document.getElementById("<%=drprole.ClientID%>");
                var hdnuserid = document.getElementById("<%=hdnuserid.ClientID%>");


                txtfname.value = parseFloat(txtfname.value) || '';



                $('#modalpopup').modal('show')
<%--                document.getElementById("<%=btnadd.ClientID%>").style.display = "none";
                document.getElementById("<%=btnupdate.ClientID%>").style.display = "";--%>
            }
            else {
                $('#modalpopup').modal('hide')

            }

        }
    </script>
</asp:Content>
