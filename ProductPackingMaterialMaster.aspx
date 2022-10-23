<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ProductPackingMaterialMaster.aspx.cs" Inherits="Production_Costing_Software.ProductPackingMaterialMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="contain">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Product Packing Material Master</b>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="card-body">
                            <div class="container col-12 ">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="text-danger" runat="server" ValidationGroup="g1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Bulk Product</span>
                                            </div>
                                            <asp:DropDownList ID="drpproduct" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drprmcategory" runat="server" InitialValue="0" ControlToValidate="drpproduct" Display="None" ValidationGroup="g1" ErrorMessage="Select Product" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Packing Name</span>
                                            </div>
                                            <asp:TextBox ID="txtpackingname" CssClass="form-control" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Packing Size</span>
                                            </div>
                                            <asp:TextBox ID="txtpackingsize" onchange="CalculateShipper();" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtpackingsize" Display="None" ValidationGroup="g1" ErrorMessage="Enter Packing Size" />
                                            <asp:DropDownList ID="drppackingmeasurement" onchange="CalculateShipper();" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0" ControlToValidate="drppackingmeasurement" Display="None" ValidationGroup="g1" ErrorMessage="Select Packing Measurement" />
                                            <asp:DropDownList ID="drpackingcategory" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Master Packing &nbsp;&nbsp;&nbsp;<asp:CheckBox runat="server" ID="chkismaster" /></span>
                                                </div>
                                            </div>
                                            <asp:Label ID="lblmasterpackingname" CssClass="form-control" runat="server" Text=""></asp:Label>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <b>Shipper Detail</b>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Shipper Type &nbsp;&nbsp;&nbsp;<asp:CheckBox runat="server" onclick="shippertype();" Checked="false" ID="chkshippertype" /></span>
                                            </div>

                                            <asp:DropDownList ID="drpshippertype" Enabled="false" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="drpshippertype" Display="None" ValidationGroup="g1" ErrorMessage="Select Shipper Type" />

                                        </div>

                                    </div>
                                    <div class="col-md-4">

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Shipper Size</span>
                                            </div>
                                            <asp:TextBox ID="txtshippersize" TextMode="Number" Enabled="false" onchange="CalculateShipper();" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtshippersize" Display="None" ValidationGroup="g1" ErrorMessage="Enter Shipper Size" />
                                            <asp:DropDownList ID="drpunitmeasurement" Enabled="false" onchange="CalculateShipper();" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0" ControlToValidate="drpunitmeasurement" Display="None" ValidationGroup="g1" ErrorMessage="Select Shipper Unit" />


                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Unit/Shipper</span>
                                            </div>

                                            <asp:Label ID="lblunitshipper" CssClass="form-control" runat="server"></asp:Label>

                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <b>Inner Detail</b>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Inner Type &nbsp;&nbsp;&nbsp;<asp:CheckBox runat="server" onclick="innertype();" Checked="false" ID="chkinner" /></span>
                                            </div>

                                            <asp:DropDownList ID="drpinnertype" Enabled="false" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="0" Enabled="false" ControlToValidate="drpinnertype" Display="None" ValidationGroup="g1" ErrorMessage="Select Inner Type" />

                                        </div>

                                    </div>
                                    <div class="col-md-4">

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Inner Size</span>
                                            </div>
                                            <asp:TextBox ID="txtinnersize" TextMode="Number" Enabled="false" onchange="CalculateShipper();" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtinnersize" Enabled="false" Display="None" ValidationGroup="g1" ErrorMessage="Enter Inner Size" />

                                            <asp:DropDownList ID="drpinnerunit" Enabled="false" onchange="CalculateShipper();" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue="0" Enabled="false" ControlToValidate="drpinnerunit" Display="None" ValidationGroup="g1" ErrorMessage="Select Inner Unit" />

                                        </div>
                                    </div>
                                    <div class="col-md-4">

                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Unit/Inner</span>
                                            </div>
                                            <asp:Label ID="txtinnerunitshipper" CssClass="form-control" runat="server"></asp:Label>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <asp:Button ID="btnupdate" class="btn btn-success" OnClientClick="return CheckExist();" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                    <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClientClick="return CheckExist();" ValidationGroup="g1" OnClick="btnadd_Click" CausesValidation="true" runat="server" Text="Add" />
                                    <asp:Button ID="btncancel" CssClass="btn btn-info " OnClick="btncancel_Click" runat="server" Text="Cancel" />
                                    <asp:HiddenField ID="hdnmaterialid" runat="server" />
                                </div>

                            </div>
                            <div class="container col-sm-12">
                                <br />
                                <br />
                            </div>
                            <div class="container col-sm-12">
                                <asp:GridView ID="gvpackingmaster" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview resposive" ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="productName" HeaderText="Product Name" />
                                        <asp:BoundField DataField="packingName" HeaderText="Pack Name" />
                                        <asp:BoundField DataField="packingSize" HeaderText="Pack Size" />
                                        <asp:BoundField DataField="packing" HeaderText="Pack Measurement" />
                                        <asp:BoundField DataField="packingcategoryName" HeaderText="Packing Category" />
                                        <asp:BoundField DataField="ShipperSize" HeaderText="Shipper Size" />
                                        <asp:BoundField DataField="Unit" HeaderText="Unit Measurement" />
                                        <asp:BoundField DataField="UnitShipperDisplay" HeaderText="Unit/Shipper" />
                                        <asp:BoundField DataField="InnerShipperDisplay" HeaderText="Unit/Inner" />

                                        <asp:TemplateField HeaderText="Total Cost/Unit">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("TotalcostUnit") %>'></asp:Label>
                                                <asp:Button ID="btnshow" Text="Edit" runat="server" CommandArgument='<%#  Eval("FkBulkProductId")+"@"+ Eval("PackingMaterialId")%>' Style="float: right" OnClick="btnEdit_Click1" CssClass="btn btn-sm btn-success" />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="CostperLtr" HeaderText="Total Cost/Ltr" />

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEditMain" Text="Edit" runat="server" CommandArgument='<%#  Eval("PackingMaterialId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                                <asp:Button ID="btnDeleteMain" Text="Delete" runat="server" CommandArgument='<%#  Eval("PackingMaterialId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>

                            <div class="modal" id="showCosting" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                <div class="modal-dialog modal-xl">
                                    <div class="modal-content ">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="exampleModalLabel">Bulk Product Name : [<asp:Label ID="lblpname" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>]
                                                                 &nbsp;
                                                           &nbsp;/ Pack Measurement : [<asp:Label ID="lblpack" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>] 
                                                           &nbsp; / Category  : [<asp:Label ID="lblcat" CssClass="font-monospace" runat="server" Text="/" Visible="true"></asp:Label>] 
                                                           

                                            </h5>
                                            <button type="button" data-dismiss="modal" onclick="showhideCostingwithreload('0');" class="btn-close btn-secondary">Close</button>
                                            <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                          <span aria-hidden="true">&times;</span>--%>
                                        </button>
                                        </div>

                                        <div class="modal-body">

                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:ValidationSummary ID="ValidationSummary2" CssClass="text-danger" runat="server" ValidationGroup="g2" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">Type</span>
                                                        </div>
                                                        <div class="form-control rbl">
                                                            <asp:RadioButtonList ID="rdotype" RepeatDirection="Horizontal" runat="server">
                                                                <asp:ListItem Text="PM" Value="0" onclick="TypeSelect('0');" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Shipper" Value="1" onclick="TypeSelect('1');"></asp:ListItem>
                                                                <asp:ListItem Text="Inner" Value="2" onclick="TypeSelect('2');"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">Shipper Type</span>
                                                        </div>
                                                        <asp:DropDownList ID="drpshipper" onchange="bindproduct('0','0');" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue="0" ControlToValidate="drpshipper" Display="None" ValidationGroup="g2" ErrorMessage="Select Shipper Type" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">PM Name</span>
                                                        </div>
                                                        <asp:DropDownList ID="drppmname" onchange="getrate('0');" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" InitialValue="0" ControlToValidate="drppmname" Display="None" ValidationGroup="g2" ErrorMessage="Select PM Name" />

                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">PM Price (Cost/Unit)</span>
                                                        </div>
                                                        <asp:Label ID="lblcostunit" CssClass="form-control" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">Final Packing Cost/Unit</span>
                                                        </div>
                                                        <asp:Label ID="lblfinalcost" CssClass="form-control" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button ID="btncostupdate" class="btn btn-success" Visible="false" ValidationGroup="g2" OnClientClick="Setval();" OnClick="btncostupdate_Click" runat="server" Text="Update" />
                                                    <asp:Button ID="btncostadd" CssClass="btn btn-primary" ValidationGroup="g2" OnClick="btncostadd_Click" OnClientClick="Setval();" CausesValidation="true" runat="server" Text="Add" />
                                                    <asp:Button ID="btncostcancel" CssClass="btn btn-info " OnClick="btncostcancel_Click" runat="server" Text="Cancel" />
                                                    <asp:HiddenField ID="hdncostid" runat="server" />
                                                    <asp:HiddenField ID="hdnmatid" runat="server" />
                                                    <asp:HiddenField ID="hdnprdid" runat="server" />

                                                    <asp:HiddenField ID="hdsp" runat="server" />
                                                    <asp:HiddenField ID="hdrm" runat="server" />

                                                    <asp:HiddenField ID="hdntotalcostunit" runat="server" />
                                                    <asp:HiddenField ID="hdntotalcostltr" runat="server" />
                                                </div>
                                            </div>

                                            <div class="row">

                                                <div class="container col-12">
                                                    <asp:GridView ID="gvcosting" runat="server" AutoGenerateColumns="false" ShowFooter="true" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="No">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="productName" HeaderText="Product Name" />
                                                            <asp:BoundField DataField="PMRMCategoryName" HeaderText="Category" />
                                                            <asp:BoundField DataField="PMRMName" HeaderText="Name" />
                                                            <asp:BoundField DataField="FinalPrice" HeaderText="Cost/Unit" />
                                                            <asp:BoundField DataField="CostPerUnit" HeaderText="Final Pack/Unit" />

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnEdit_ing" Text="Edit" runat="server" CommandArgument='<%#  Eval("PackingMaterialCostingId") %>' OnClick="btnEdit_ing_Click" CssClass="btn btn-sm btn-primary" />
                                                                    <asp:Button ID="btnDelete_ing" Text="Delete" runat="server" CommandArgument='<%#  Eval("PackingMaterialCostingId") %>' OnClick="btnDelete_ing_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>

                                                </div>

                                            </div>

                                        </div>


                                    </div>
                                </div>
                            </div>

                        </div>
                        <asp:HiddenField ID="hdnreload" runat="server" />
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
                        bindCostingDataTable();
                        var nm = sender._postBackSettings.asyncTarget;

                        if (nm.indexOf('btnEditMain') > 0 || nm.indexOf('btnDeleteMain') > 0 || nm.indexOf('btnadd') > 0 || nm.indexOf('btnupdate') > 0 || nm.indexOf('btncancel') > 0) {

                        }
                        else {
                            showhideCosting('1');
                        }
                    }
                });
            };

            bindDataTable();
            function bindDataTable() {
                $('#<%= gvpackingmaster.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvpackingmaster.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": false,
                    "autoWidth": false,
                   /* "responsive": true*/
                    //fixedHeader: {
                    //    footer: false
                    //}
                });

            }

        });

        function CheckExist() {
            var ret = false;
            var prd = document.getElementById("<%=drpproduct.ClientID%>").value;
            var ps = document.getElementById("<%=txtpackingsize.ClientID%>").value;
            var pu = document.getElementById("<%=drppackingmeasurement.ClientID%>").value;
            var pc = document.getElementById("<%=drpackingcategory.ClientID%>").value;
            var pmid = document.getElementById("<%=hdnmaterialid.ClientID%>").value;

            $.ajax({
                url: 'WebService.asmx/PackingExits',
                data: { productid: prd, packsize: ps, packunit: pu, packcategory: pc, packingid: pmid },
                method: 'POST',
                async: false,
                success: function (r) {
                    if (r.all[0].textContent == '0') {
                        alert('Packing already exists');
                        ret = false;
                    }
                    else {
                        ret = true;

                    }

                }
            });

            return ret;
        }


        function Setval() {


            document.getElementById("<%=hdsp.ClientID%>").value = document.getElementById('<%=drpshipper.ClientID %>').value;
            document.getElementById("<%=hdrm.ClientID%>").value = document.getElementById('<%=drppmname.ClientID %>').value;



        }
        function bindCostingDataTable() {
            $('#<%= gvcosting.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvcosting.ClientID %>').find("tr:first"))).DataTable({
                "destroy": true,
                "paging": false,
                "lengthChange": true,
                "searching": true,
                "ordering": false,
                "info": false,
                "autoWidth": false,
                "responsive": true,
                fixedHeader: {
                    footer: false
                }
            });

            var gridv = document.getElementById("<%=gvcosting.ClientID%>");
            if (gridv != null) {
                var rw = gridv.rows.length;
                if (rw >= 3) {
                    gridv.rows[rw - 1].cells[3].innerHTML = "<b>Total</b>"
                    gridv.rows[rw - 1].cells[5].innerHTML = "<b>" + document.getElementById("<%=hdntotalcostunit.ClientID%>").value + "</b>";
                    gridv.rows[rw - 1].cells[4].innerHTML = "<b>" + document.getElementById("<%=hdntotalcostltr.ClientID%>").value + "</b>";
                }
            }

        }

        function TypeSelect(type) {

            $('#<%=drpshipper.ClientID %>').empty();
            $('#<%=drppmname.ClientID %>').empty();


            bindshipper(type, 0);

            document.getElementById("<%=lblcostunit.ClientID%>").innerHTML = "0.00";
            document.getElementById("<%=lblfinalcost.ClientID%>").innerHTML = "0.00";
        }
        function SelectCosting(type, catid, pmrmid) {

            bindshipper(type, catid);


            bindproduct(pmrmid, catid);


            getrate(pmrmid);
        }

        function bindshipper(id, v) {

            document.getElementById("<%=lblcostunit.ClientID%>").innerHTML = "0.00";
            document.getElementById("<%=lblfinalcost.ClientID%>").innerHTML = "0.00";

            var tp = "";
            if (id == 0) {
                tp = 'ShipperOthers';
            }
            else if (id == 1) {
                tp = 'Shipper';
            }
            else if (id == 2) {
                tp = 'Inner';
            }
            else {
                tp = 'ShipperOthers';
            }
            $('#<%=drpshipper.ClientID %>').empty();
            $.ajax({
                url: 'WebService.asmx/Shipper',
                data: { type: tp },
                dataType: "xml",
                method: 'POST',
                success: function (Result) {
                    $("#<%=drpshipper.ClientID %>").empty();
                    for (var i = 0; i < Result.all[0].children.length; i++) {
                        $("#<%=drpshipper.ClientID %>").append($("<option></option>").val(Result.all[0].children[i].children[0].innerHTML).html(Result.all[0].children[i].children[1].innerHTML));
                    }
                    $("#<%=drpshipper.ClientID %> option[value=" + v + "]").attr('selected', 'selected');

                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }

        function bindproduct(v, cat) {

            var tp = document.getElementById('<%=drpshipper.ClientID %>').value > 0 ? document.getElementById('<%=drpshipper.ClientID %>').value : cat;

            $('#<%=drppmname.ClientID %>').empty();
            $.ajax({
                url: 'WebService.asmx/ShippeProduct',
                data: { type: tp },
                dataType: "xml",
                method: 'POST',
                success: function (Result) {

                    $("#<%=drppmname.ClientID %>").empty();
                    for (var i = 0; i < Result.all[0].children.length; i++) {

                        $("#<%=drppmname.ClientID %>").append($("<option></option>").val(Result.all[0].children[i].children[0].innerHTML).html(Result.all[0].children[i].children[1].innerHTML));
                    }
                    $("#<%=drppmname.ClientID %> option[value=" + v + "]").attr('selected', 'selected');
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }

        function getrate(v) {

            var tp = document.getElementById('<%=drppmname.ClientID %>').value > 0 ? document.getElementById('<%=drppmname.ClientID %>').value : v;

            $.ajax({
                url: 'WebService.asmx/PMRMCost',
                data: { pmrmid: tp },
                dataType: "xml",
                method: 'POST',
                success: function (Result) {
                    if (Result.all[0].textContent != "") {
                        document.getElementById("<%=lblcostunit.ClientID%>").innerHTML = Result.all[0].textContent.split('~~')[0];
                        document.getElementById("<%=lblfinalcost.ClientID%>").innerHTML = Result.all[0].textContent.split('~~')[1];
                    }
                    else {
                        document.getElementById("<%=lblcostunit.ClientID%>").innerHTML = "0.00";
                        document.getElementById("<%=lblfinalcost.ClientID%>").innerHTML = "0.00";
                    }
                },
                error: function error(result) {
                    alert(result.status + ' : ' + result.statusText);
                }
            });
        }

        function showhideCostingwithreload(type) {

            if (type == '1') {

                $('#showCosting').modal('show');

                $(".modal-backdrop").not(':first').remove();

            }
            else {
                $('#showCosting').modal('hide');
                $(".modal-backdrop").remove();
                alert(document.getElementById("<%=hdnreload.Value%>").value);
                if (document.getElementById("<%=hdnreload.Value%>").value == "1") {
                    window.location.reload();
                }
            }
        }

        function showhideCosting(type) {



            if (type == '1') {

                $('#showCosting').modal('show');

                $(".modal-backdrop").not(':first').remove();

            }
            else {
                $('#showCosting').modal('hide');

                $(".modal-backdrop").remove();
            }
        }

        function shippertype() {

            if (document.getElementById("<%=chkshippertype.ClientID%>").checked) {

                document.getElementById("<%=drpshippertype.ClientID%>").disabled = false;
                document.getElementById("<%=txtshippersize.ClientID%>").disabled = false;
                document.getElementById("<%=drpunitmeasurement.ClientID%>").disabled = false;

                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator2.ClientID %>'), true);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator5.ClientID %>'), true);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator3.ClientID %>'), true);

            }
            else {

                document.getElementById("<%=drpshippertype.ClientID%>").disabled = true;
                document.getElementById("<%=txtshippersize.ClientID%>").disabled = true;
                document.getElementById("<%=drpunitmeasurement.ClientID%>").disabled = true;

                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator2.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator5.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator3.ClientID %>'), false);

            }

        }
        function innertype() {

            if (document.getElementById("<%=chkinner.ClientID%>").checked) {

                document.getElementById("<%=drpinnertype.ClientID%>").disabled = false;
                document.getElementById("<%=txtinnersize.ClientID%>").disabled = false;
                document.getElementById("<%=drpinnerunit.ClientID%>").disabled = false;

                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator6.ClientID %>'), true);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator7.ClientID %>'), true);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator8.ClientID %>'), true);

            }
            else {

                document.getElementById("<%=drpinnertype.ClientID%>").disabled = true;
                document.getElementById("<%=txtinnersize.ClientID%>").disabled = true;
                document.getElementById("<%=drpinnerunit.ClientID%>").disabled = true;

                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator6.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator7.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator8.ClientID %>'), false);


            }

        }
        function CalculateShipper() {

            var packingsize = document.getElementById("<%=txtpackingsize.ClientID%>");
            var packingUnit = document.getElementById("<%=drppackingmeasurement.ClientID%>");

            var shippersize = document.getElementById("<%=txtshippersize.ClientID%>");
            var shipperunit = document.getElementById("<%=drpunitmeasurement.ClientID%>");

            var innersize = document.getElementById("<%=txtinnersize.ClientID%>");
            var innerunit = document.getElementById("<%=drpinnerunit.ClientID%>");

            var unitshipper = document.getElementById("<%=lblunitshipper.ClientID%>");
            var unintinner = document.getElementById("<%=txtinnerunitshipper.ClientID%>");

            shippertype();
            innertype();

            if (parseFloat(packingsize.value) > 0 && packingUnit.value > 0) {

                if (shippersize.value > 0 && shipperunit.value > 0) {

                    if (packingUnit.value == '1' || packingUnit.value == '2' || packingUnit.value == '3') {

                        /* if (shipperunit.value == '1' || shipperunit.value == '2' || shipperunit.value == '3') {
 
                             var c = Number(parseFloat(shippersize.value) / parseFloat(packingsize.value)).toFixed(2);
                             unitshipper.innerHTML = c.toString();
                         }
                         else if (shipperunit.value == '6' || shipperunit.value == '7') {
 
                             var c = Number((parseFloat(shippersize.value) / parseFloat(packingsize.value))*1000).toFixed(2);
                             unitshipper.innerHTML = c.toString();
 
                         }
                         */

                        var c = Number(parseFloat(shippersize.value) / parseFloat(packingsize.value)).toFixed(2);
                        unitshipper.innerHTML = c.toString();

                    }
                    else if (packingUnit.value == '6' || packingUnit.value == '7') {

                        /* if (shipperunit.value == '1' || shipperunit.value == '2' || shipperunit.value == '3') {
 
                             var c = Number((parseFloat(shippersize.value) / parseFloat(packingsize.value))/1000).toFixed(2);
                             unitshipper.innerHTML = c.toString();
                         }
                         else if (shipperunit.value == '6' || shipperunit.value == '7') {
 
                             var c = Number((parseFloat(shippersize.value) / parseFloat(packingsize.value))).toFixed(2);
                             unitshipper.innerHTML = c.toString();
 
                         }*/

                        var c = Number((parseFloat(shippersize.value) / parseFloat(packingsize.value)) / 1000).toFixed(2);
                        unitshipper.innerHTML = c.toString();

                    }

                }
                else {
                    unitshipper.value = "0";
                    unintinner.value = "0";
                }

                if (parseFloat(innersize.value) > 0 && shipperunit.value > 0) {

                    if (packingUnit.value == '1' || packingUnit.value == '2' || packingUnit.value == '3') {

                        /* if (innerunit.value == '1' || innerunit.value == '2' || innerunit.value == '3') {
 
                             var c = Number(parseFloat(innersize.value) / parseFloat(packingsize.value)).toFixed(2);
                             unintinner.innerHTML = c.toString();
                         }
                         else if (innerunit.value == '6' || innerunit.value == '7') {
 
                             var c = Number((parseFloat(innersize.value) / parseFloat(packingsize.value)) * 1000).toFixed(2);
                             unintinner.innerHTML = c.toString();
 
                         }*/

                        var c = Number(parseFloat(innersize.value) / parseFloat(packingsize.value)).toFixed(2);
                        unintinner.innerHTML = c.toString();

                    }
                    else if (packingUnit.value == '6' || packingUnit.value == '7') {

                        /* if (innerunit.value == '1' || innerunit.value == '2' || innerunit.value == '3') {
 
                             var c = Number((parseFloat(innersize.value) / parseFloat(packingsize.value)) / 1000).toFixed(2);
                             unintinner.innerHTML = c.toString();
                         }
                         else if (innerunit.value == '6' || innerunit.value == '7') {
 
                             var c = Number((parseFloat(innersize.value) / parseFloat(packingsize.value))).toFixed(2);
                             unintinner.innerHTML = c.toString();
 
                         }*/
                        var c = Number((parseFloat(innersize.value) / parseFloat(packingsize.value)) / 1000).toFixed(2);
                        unintinner.innerHTML = c.toString();
                    }
                }
                else {
                    unitshipper.value = "0";
                    unintinner.value = "0";
                }

            }
            else {
                unitshipper.value = "0";
                unintinner.value = "0";
            }
        }
    </script>
    <style>
  

    </style>

</asp:Content>
