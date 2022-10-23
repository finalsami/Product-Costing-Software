<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" enableEventValidation="false" CodeBehind="RMPriceMaster.aspx.cs" Inherits="Production_Costing_Software.RMPriceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>RM Price Master</b>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
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
                                            <asp:DropDownList ID="drprmcategory" onchange="drprmcategorychange();" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drprmcategory" runat="server" InitialValue="0" ControlToValidate="drprmcategory" Display="None" ValidationGroup="g1" ErrorMessage="Select RM Category" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">RM  Name</span>
                                            </div>
                                            <asp:DropDownList ID="drprmname" class="form-control" onchange="drprmnamechange();" runat="server">
                                            </asp:DropDownList>
                                          <%--  <ajaxToolkit:CascadingDropDown ID="drprmname_CascadingDropDown" runat="server" BehaviorID="drprmname_CascadingDropDown" TargetControlID="drprmname" Category="Id" ParentControlID="drprmcategory" LoadingText="Loading.." ServiceMethod="BindRMName" ServicePath="~/WebService.asmx" />--%>
                                            <asp:RequiredFieldValidator ID="rf_drprmname" runat="server" InitialValue="0" ControlToValidate="drprmname" Display="None" ValidationGroup="g1" ErrorMessage="Select RM Name" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Date of Purchase</span>
                                            </div>
                                            <asp:TextBox ID="txtdop" class="form-control" type="date" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtdop" runat="server"  InitialValue="0" ControlToValidate="txtdop" Display="None" ValidationGroup="g1" ErrorMessage="Select Date of Pruchase" />
                                        </div>
                                        <div class="input-group mb-3" id="Divrfp">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Rate Full Purity </span>
                                            </div>
                                            <div class="form-control">
                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox runat="server" Checked="false" onclick="purityclick();" CssClass="form-check-input" ID="chkpurity" />
                                                <label class="form-check-label" for="flexSwitchCheckDefault">Yes/No</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Rate/Kg-Ltr(Rs)</span>
                                            </div>
                                            <asp:TextBox ID="txtratekgltr" CssClass="form-control" TextMode="Number" step="any" onchange="calculaterate();" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtratekgltr" runat="server" ControlToValidate="txtratekgltr" Display="None" ValidationGroup="g1" ErrorMessage="Enter Rate Kg-Ltr" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Quantity</span>
                                            </div>
                                            <asp:TextBox ID="txtquantity" CssClass="form-control" TextMode="Number" step="any" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtquantity" runat="server" ControlToValidate="txtquantity" Display="None" ValidationGroup="g1" ErrorMessage="Enter Quantity" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3" id="Divpurity">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Purity(%)</span>
                                            </div>
                                            <asp:TextBox ID="txtpuritypercent" CssClass="form-control" onchange="calculaterate();" TextMode="Number" step="any" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpuritypercent" runat="server" InitialValue="0" ControlToValidate="txtpuritypercent" Display="None" ValidationGroup="g1" ErrorMessage="Enter Purity (%)" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Actual Price/Kg-Ltr</span>
                                            </div>
                                            <asp:TextBox ID="txtactualprice" CssClass="form-control" Enabled="false" TextMode="Number" step="any" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtactualprice" runat="server" InitialValue="0" ControlToValidate="txtactualprice" Display="None" ValidationGroup="g1" ErrorMessage="Enter Actual Price" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Transport</span>
                                            </div>
                                            <asp:TextBox ID="txttransport" CssClass="form-control" onchange="calculaterate();" TextMode="Number" step="any" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txttransport" runat="server" InitialValue="0" ControlToValidate="txttransport" Display="None" ValidationGroup="g1" ErrorMessage="Enter Transport" />
                                        </div>
                                        <div class="input-group mb-3">
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Total Rate/Ltr</span>
                                            </div>
                                            <asp:TextBox ID="txttotalrateperltr" CssClass="form-control" Enabled="false" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txttotalrateperltr" runat="server" InitialValue="0" ControlToValidate="txttotalrateperltr" Display="None" ValidationGroup="g1" ErrorMessage="Total Rate/ Ltr" />
                                        </div>
                                        <div class="input-group mb-3">
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdrmpmid" runat="server" />
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
                                <asp:GridView ID="gvrmpricemaster" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RMName" HeaderText="RMName" />
                                        <asp:BoundField DataField="PurchaseDate" HeaderText="Purchase Date" />
                                        <asp:BoundField DataField="ActualPrice" HeaderText="ActualPrice" />
                                        <asp:TemplateField HeaderText="Purity">
                                            <ItemTemplate>
                                                <div class="form-check form-switch" style="margin-left: 65px">
                                                    <input type="checkbox" runat="server" disabled class="form-check-input " checked='<%#  Eval("isPurity") %>'>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                        <asp:BoundField DataField="TransporationRate" HeaderText="TransporationRate" />
                                        <asp:BoundField DataField="Total" HeaderText="Total" />
                                        

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("RMPriceId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("RMPriceId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="hdnRMid" runat="server" />
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
                $('#<%= gvrmpricemaster.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvrmpricemaster.ClientID %>').find("tr:first"))).DataTable({
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

        function drprmnamechange() {
            var rmId = document.getElementById('<%=drprmname.ClientID %>').value;
            document.getElementById('<%=hdnRMid.ClientID %>').value = document.getElementById('<%=drprmname.ClientID %>').value;
            $.ajax({
                url: 'WebService.asmx/GetIsFullPurityByRM',
                data: { RMId: rmId },
                method: 'POST',

                success: function (r) {
                    if (r.all[0].textContent == "True") {
                        $('#Divrfp').show();
                        $('#Divpurity').show();
                    }
                    else {
                        $('#Divrfp').hide();
                        $('#Divpurity').hide();
                    }
                }
            });
        }

        function purityclick() {
            calculaterate();
        }             
    

         function calculaterate() {

            var ispurity = $('#<%=chkpurity.ClientID%>')[0];
            var puritypercent = document.getElementById('<%=txtpuritypercent.ClientID %>');
            var ratekgltr = document.getElementById('<%=txtratekgltr.ClientID %>');
            var transport = document.getElementById('<%=txttransport.ClientID %>');
            var totalrate = document.getElementById('<%=txttotalrateperltr.ClientID %>');
            var txtactualprice = document.getElementById('<%=txtactualprice.ClientID %>');

            puritypercent.value = parseFloat(puritypercent.value) || '0.00';
            ratekgltr.value = parseFloat(ratekgltr.value) || '0.00';
            transport.value = parseFloat(transport.value) || '0.00';

            var act = 0;

            act = ispurity.checked ? parseFloat(ratekgltr.value) * parseFloat(puritypercent.value) / 100 : parseFloat(ratekgltr.value);
            txtactualprice.value = Number(act).toFixed(2);
            totalrate.value = Number(act + parseFloat(transport.value)).toFixed(2);


        }

        function drprmcategorychange() {
            var rmId = document.getElementById('<%=drprmcategory.ClientID %>').value;
            $('#<%=drprmname.ClientID %>').empty();
            $.ajax({
                url: 'WebService.asmx/BindRM',
                data: { RMId: rmId },
                dataType: "xml",
                method: 'POST',
                success: function (Result) {
                    $("#<%=drprmname.ClientID %>").empty();
                    for (var i = 0; i < Result.all[0].children.length; i++) {
                        $("#<%=drprmname.ClientID %>").append($("<option></option>").val(Result.all[0].children[i].children[0].innerHTML).html(Result.all[0].children[i].children[1].innerHTML));
                    }
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }

    </script>
</asp:Content>
