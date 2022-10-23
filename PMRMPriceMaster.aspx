<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PMRMPriceMaster.aspx.cs" Inherits="Production_Costing_Software.PMRMPriceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>PMRM Price Masrer</b>
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
                                            <asp:DropDownList ID="drppmrmcat" onchange="drppmrmcatchange(this);" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drppmrmcat" runat="server" InitialValue="0" ControlToValidate="drppmrmcat" Display="None" ValidationGroup="g1" ErrorMessage="Select PMRM Category" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Price</span>
                                            </div>
                                            <asp:TextBox ID="txtprice" CssClass="form-control" onchange="calculaterate();" InitialValue="0" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtprice" runat="server" ControlToValidate="txtprice" Display="None" ValidationGroup="g1" ErrorMessage="Enter Price" />
                                            <span class="input-group-text">
                                                <asp:Label ID="lblpmrmcatunit" runat="server"></asp:Label></span>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">PMRM Name</span>
                                            </div>
                                            <asp:DropDownList ID="drppmrmname" onchange="drppmrmnamechange(this);" Enabled="false" InitialValue="0" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drppmrmname" runat="server" ControlToValidate="drppmrmname" Display="None" ValidationGroup="g1" ErrorMessage="Select PMRM Name" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Transportation</span>
                                            </div>
                                            <asp:TextBox ID="txttransport" CssClass="form-control" InitialValue="0" onchange="calculaterate();" runat="server" placeholder="0.00"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txttransport" runat="server" ControlToValidate="txttransport" Display="None" ValidationGroup="g1" ErrorMessage="Enter Transport" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">No. of Units/KG</span>
                                            </div>
                                            <asp:TextBox ID="txtnoofunit" InitialValue="0" Enabled="false" CssClass="form-control" TextMode="Number" runat="server" placeholder="0.00"></asp:TextBox>
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Loss (%)</span>
                                            </div>
                                            <asp:TextBox ID="txtloss" CssClass="form-control" onchange="calculaterate();" runat="server" placeholder="0.00"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtloss" runat="server" ControlToValidate="txtloss" Display="None" ValidationGroup="g1" ErrorMessage="Enter Loss %" />
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Price /Unit</span>
                                            </div>
                                            <asp:TextBox ID="txtPerunit" CssClass="form-control" Enabled="false" runat="server" placeholder="0.00"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdnUnitMeasurementId" runat="server" />
                                        <asp:HiddenField ID="hdnpmrmpriceid" runat="server" />
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
                                <asp:GridView ID="gvpmrmpricemaster" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PMRMCategoryName" HeaderText="PMRMCategory" />
                                        <asp:BoundField DataField="PMRMName" HeaderText="PMRMName" />
                                        <asp:BoundField DataField="Price" HeaderText="Price" />
                                        <asp:BoundField DataField="TrasportationCost" HeaderText="TrasportationCost" />

                                        <asp:BoundField DataField="FinalPrice" HeaderText="FinalPrice" />



                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("PMRMPriceId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("PMRMPriceId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
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
                $('#<%= gvpmrmpricemaster.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvpmrmpricemaster.ClientID %>').find("tr:first"))).DataTable({
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



        function calculaterate() {
            var txtprice = document.getElementById('<%=txtprice.ClientID %>').value;
            var txttransport = document.getElementById('<%=txttransport.ClientID %>').value;
            var txtPerunit = document.getElementById('<%=txtPerunit.ClientID %>').value;
            var txtloss = document.getElementById('<%=txtloss.ClientID %>').value;

            txtprice.value = parseFloat(txtprice.value) || '0';
            txttransport.value = parseFloat(txttransport.value) || '0';
            txtloss.value = parseFloat(txtloss.value) || '0';

            act = 0;
            act = ((parseFloat(txtloss) * (parseFloat(txtprice)) / 100) + parseFloat(txtprice));
  

             txtprice = Number(act + parseFloat(txttransport)).toFixed(2);
            document.getElementById('<%=txtPerunit.ClientID %>').value = parseFloat(txtprice);    }

        function drppmrmcatchange() {
            var pmrmcatId = document.getElementById('<%=drppmrmcat.ClientID %>').value;
            $('#<%=drppmrmname.ClientID %>').empty();
            $.ajax({
                url: 'WebService.asmx/BindPMRMName',
                data: { PMRMCatId: pmrmcatId },
                dataType: "xml",
                method: 'POST',
                success: function (Result) {
                    document.getElementById("<%=drppmrmname.ClientID%>").disabled = false;
                    $("#<%=drppmrmname.ClientID %>").empty();
                    for (var i = 0; i < Result.all[0].children.length; i++) {
                        $("#<%=drppmrmname.ClientID %>").append($("<option></option>").val(Result.all[0].children[i].children[0].innerHTML).html(Result.all[0].children[i].children[1].innerHTML));
                    }
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }
        function drppmrmnamechange(obj) {
            var drppmrmname = document.getElementById('<%=drppmrmname.ClientID %>').value;
            var lblpmrmcatunit = document.getElementById('<%=lblpmrmcatunit.ClientID %>').value;
            var txtnoofunit = document.getElementById('<%=txtnoofunit.ClientID %>').value;
            var txtPerunit = document.getElementById('<%=txtPerunit.ClientID %>').value;

            txtnoofunit = parseFloat(txtnoofunit.value) || '0.00';

            $.ajax({
                url: 'WebService.asmx/BindUnitByPMRMName',
                data: { PMRMCatId: drppmrmname },
                dataType: "xml",
                method: 'POST',
                success: function (r) {

                    var Unit = r.all[0].textContent;
                    var val1 = Unit.split('~')[0];
                    var val2 = Unit.split('~')[1];
                    var val3 = Unit.split('~')[2];
                    document.getElementById('<%=txtnoofunit.ClientID %>').value = val1;
                    document.getElementById('<%=lblpmrmcatunit.ClientID %>').innerHTML = val2;
                    document.getElementById('<%=hdnUnitMeasurementId.ClientID %>').value = val3;
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }
    </script>
</asp:Content>
