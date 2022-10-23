<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PMRMMaster.aspx.cs" Inherits="Production_Costing_Software.PMRMMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>PMRM Master</b>
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
                                                <span class="input-group-text">PMRM Catgory</span>
                                            </div>
                                            <asp:DropDownList ID="drppmrmcat"  CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drppmrmcat" runat="server" InitialValue="0" ControlToValidate="drppmrmcat" Display="None" ValidationGroup="g1" ErrorMessage="Select PMRM Category" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">PMRM Name</span>
                                            </div>
                                            <asp:TextBox ID="txtpmrmname" CssClass="form-control" InitialValue="0" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpmrmname" runat="server" ControlToValidate="txtpmrmname" Display="None" ValidationGroup="g1" ErrorMessage="Enter PMRM Name" />

                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Measurement</span>
                                            </div>
                                            <asp:DropDownList ID="drpunit" onchange="drpunitchange(this);" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpunit" runat="server" InitialValue="0" ControlToValidate="drpunit" Display="None" ValidationGroup="g1" ErrorMessage="Select Measurement" />
                                        </div>
                                        <div class="input-group mb-3" id="DivNoofunit" runat="server">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">No of Unit</span>
                                            </div>
                                            <asp:TextBox ID="txtnoofunit" InitialValue="0" onchange="calculaterate();" CssClass="form-control" TextMode="Number" runat="server" placeholder="0.00"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3" id="DivTotalWeight" runat="server">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Weight of Unit</span>
                                            </div>
                                            <asp:TextBox ID="txttotalweight" CssClass="form-control" onchange="calculaterate();" runat="server" placeholder="0.00"></asp:TextBox>
                                            <span class="input-group-text">/KG</span>
                                        </div>
                                        <div class="input-group mb-3" id="DivPerunitkg" runat="server">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Per Unit/Kg</span>
                                            </div>
                                            <asp:TextBox ID="txtPerunitkg" CssClass="form-control" Enabled="false" runat="server" placeholder="0.00"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3" id="DivUnitPerkg" runat="server">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Unit/KG</span>
                                            </div>
                                            <asp:TextBox ID="txtunitkg" CssClass="form-control" Enabled="false" runat="server" placeholder="0.00"></asp:TextBox>

                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdnpmrmid" runat="server" />
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
                                <asp:GridView ID="gvpmrmmaster" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PMRMCategoryName" HeaderText="PMRMCategory" />
                                        <asp:BoundField DataField="PMRMName" HeaderText="PMRMName" />
                                        <asp:BoundField DataField="EnumDescription" HeaderText="Price KG/Unit" />
                                        <asp:BoundField DataField="Unit" HeaderText="No of Unit" />
                                        <asp:BoundField DataField="TotalWeight" HeaderText="Weight of Unit" />
                                        <asp:BoundField DataField="PerUnitWeight" HeaderText="Per Unit Weight" />
                                        <asp:BoundField DataField="UnitKG" HeaderText="Unit/KG" />



                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("PMRMId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("PMRMId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btncancel" />
                    </Triggers>
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
                $('#<%= gvpmrmmaster.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvpmrmmaster.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": false,
                    "autoWidth": false,
                    //"responsive": true,
                });

            }
        });
        function drpunitchange(obj) {
            var drpunit = document.getElementById('<%=drpunit.ClientID %>').value;
<%--            var txtnoofunit = document.getElementById('<%=txtnoofunit.ClientID %>').value;
            var txttotalweight = document.getElementById('<%=txttotalweight.ClientID %>').value;
            var txtPerunitkg = document.getElementById('<%=txtPerunitkg.ClientID %>').value;
            var txtunitkg = document.getElementById('<%=txtunitkg.ClientID %>').value;--%>
            


            drpunit.value = parseFloat(drpunit.value) || '0.00';
            if (drpunit == 3) {

                document.getElementById("<%=DivNoofunit.ClientID%>").style.display = "none";
                document.getElementById("<%=DivTotalWeight.ClientID%>").style.display = "none";
                document.getElementById("<%=DivPerunitkg.ClientID%>").style.display = "none";
                document.getElementById("<%=DivUnitPerkg.ClientID%>").style.display = "none";
               
            }
            else {

                document.getElementById("<%=DivNoofunit.ClientID%>").style.display = "";
                document.getElementById("<%=DivTotalWeight.ClientID%>").style.display = "";
                document.getElementById("<%=DivPerunitkg.ClientID%>").style.display = "";
                document.getElementById("<%=DivUnitPerkg.ClientID%>").style.display = "";

            }
            calculaterate();
        }
        function calculaterate() {
            var drpunit = document.getElementById('<%=drpunit.ClientID %>').value;
            var txtnoofunit = document.getElementById('<%=txtnoofunit.ClientID %>').value;
            var txttotalweight = document.getElementById('<%=txttotalweight.ClientID %>').value;
            var txtPerunitkg = document.getElementById('<%=txtPerunitkg.ClientID %>').value;
            var txtunitkg = document.getElementById('<%=txtunitkg.ClientID %>').value;

            txtPerunitkg.value = parseFloat(txtPerunitkg.value) || '0.00';
            txtunitkg.value = parseFloat(txtunitkg.value) || '0.00';


            if (drpunit == 1 || drpunit == 2) {
                document.getElementById('<%=txtPerunitkg.ClientID %>').value = (txttotalweight / txtnoofunit).toFixed(2);
                document.getElementById('<%=txtunitkg.ClientID %>').value = (txttotalweight / txtnoofunit).toFixed(2);
            }
            else if (drpunit == 6 || drpunit == 7) {
                document.getElementById('<%=txtPerunitkg.ClientID %>').value = (parseFloat(txttotalweight / txtnoofunit) / 1000).toFixed(2);
                document.getElementById('<%=txtunitkg.ClientID %>').value = (parseFloat(txtnoofunit * 1000) / txttotalweight).toFixed(2);
            }
            else {
                document.getElementById('<%=txtPerunitkg.ClientID %>').value = (0).toFixed(2);
                document.getElementById('<%=txtunitkg.ClientID %>').value = (0).toFixed(2);
            }


        }
    </script>
</asp:Content>
