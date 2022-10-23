<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PackingStyleLabourCostingMaster.aspx.cs" Inherits="Production_Costing_Software.PackingStyleLabourCostingMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tooltip{
            display: none;
            border: solid 1px #708069;
            background-color: #289642;
            color: #fff;
            line-height: 25px;
            border-radius: 5px;
            padding: 5px 10px;
            position: absolute;
            z-index: 1001;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="fluid px-12">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Packing Style Labour Costing Master</b>
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
                                                <span class="input-group-text">Packing Category & Size</span>
                                            </div>
                                            <asp:DropDownList ID="drppcs" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drppcs" runat="server" InitialValue="0" ControlToValidate="drppcs" Display="None" ValidationGroup="g1" ErrorMessage="Select Packing Category & Size" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Packing Style Name</span>
                                            </div>
                                            <asp:DropDownList ID="drppsn" class="form-control" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drppsn" runat="server" InitialValue="0" ControlToValidate="drppsn" Display="None" ValidationGroup="g1" ErrorMessage="Select Packing Style" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Packing Size</span>
                                            </div>
                                            <asp:TextBox ID="txtpsize" class="form-control"  runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpsize" runat="server" ControlToValidate="txtpsize" Display="None" ValidationGroup="g1" ErrorMessage="Enter Packing Size" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Measurement</span>
                                            </div>
                                            <asp:DropDownList ID="drpunit" runat="server" class="form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rf_drpunit" runat="server" InitialValue="0" ControlToValidate="drpunit" Display="None" ValidationGroup="g1" ErrorMessage="Select Measurement Unit" />
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Add Labour Task</b>
                                    </div>

                                    <%-------------------- Add Labour Task--------------------------%>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Bulk Charge</span>
                                            </div>
                                            <asp:TextBox ID="txtbulkcharge" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtbulkcharge" runat="server" ControlToValidate="txtbulkcharge" Display="None" ValidationGroup="g1" ErrorMessage="Enter bulk charge" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Pouch Filling</span>
                                            </div>
                                            <asp:TextBox ID="txtpouchfilling" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpouchfilling" runat="server" ControlToValidate="txtpouchfilling" Display="None" ValidationGroup="g1" ErrorMessage="Enter Pouch filling" />
                                        </div>

                                    </div>
                                    <asp:HiddenField runat="server" ID="hdnPackingStyleLabourCostingId" />
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">BottleKeeping</span>
                                            </div>
                                            <asp:TextBox ID="txtbottlekeeping" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtbottlekeeping" runat="server" ControlToValidate="txtbottlekeeping" Display="None" ValidationGroup="g1" ErrorMessage="Enter Bottle keeping" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Lifting/ Pouch/ Bottle Wt.</span>
                                            </div>
                                            <asp:TextBox ID="txtliftpouchbttwt" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtliftpouchbttwt" runat="server" ControlToValidate="txtliftpouchbttwt" Display="None" ValidationGroup="g1" ErrorMessage="Enter Lifting/ Pouch/ Bottle Wt." />
                                        </div>
                                    </div>



                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Black linner in pouch</span>
                                            </div>
                                            <asp:TextBox ID="txtblackinnerpouch" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtblackinnerpouch" runat="server" ControlToValidate="txtblackinnerpouch" Display="None" ValidationGroup="g1" ErrorMessage="Enter Black inner pouch" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Inner Plug</span>
                                            </div>
                                            <asp:TextBox ID="txtinnerplug" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtinnerplug" runat="server" ControlToValidate="txtinnerplug" Display="None" ValidationGroup="g1" ErrorMessage="Enter Inner Plug" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Measuring Cap</span>
                                            </div>
                                            <asp:TextBox ID="txtmeasureingcap" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtmeasureingcap" runat="server" ControlToValidate="txtmeasureingcap" Display="None" ValidationGroup="g1" ErrorMessage="Enter Measureing Cap" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Caping</span>
                                            </div>
                                            <asp:TextBox ID="txtlcaping" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtlcaping" runat="server" ControlToValidate="txtlcaping" Display="None" ValidationGroup="g1" ErrorMessage="Enter Labour Caping" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Tear Down Seal</span>
                                            </div>
                                            <asp:TextBox ID="txtteardownseal" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtteardownseal" runat="server" ControlToValidate="txtteardownseal" Display="None" ValidationGroup="g1" ErrorMessage="Enter Tear Down Seal" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Induction</span>
                                            </div>
                                            <asp:TextBox ID="txtlInduction" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtlInduction" runat="server" ControlToValidate="txtlInduction" Display="None" ValidationGroup="g1" ErrorMessage="Enter IInduction" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Pouch Sealing</span>
                                            </div>
                                            <asp:TextBox ID="txtpouchsealing" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpouchsealing" runat="server" ControlToValidate="txtpouchsealing" Display="None" ValidationGroup="g1" ErrorMessage="Enter Pouch Sealing" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Bottle/ Pouch Cleaning</span>
                                            </div>
                                            <asp:TextBox ID="txtbottlepouchcleaning" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtbottlepouchcleaning" runat="server" ControlToValidate="txtbottlepouchcleaning" Display="None" ValidationGroup="g1" ErrorMessage="Enter Bottle/ Pouch Cleaning" />
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Labeling</span>
                                            </div>
                                            <asp:TextBox ID="txtlabeling" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtlabeling" runat="server" ControlToValidate="txtlabeling" Display="None" ValidationGroup="g1" ErrorMessage="Enter Labeling" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Sleeve</span>
                                            </div>
                                            <asp:TextBox ID="txtsleeve" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtsleeve" runat="server" ControlToValidate="txtsleeve" Display="None" ValidationGroup="g1" ErrorMessage="Enter Sleeve" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Inner box</span>
                                            </div>
                                            <asp:TextBox ID="txtinnerbox" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtinnerbox" runat="server" ControlToValidate="txtinnerbox" Display="None" ValidationGroup="g1" ErrorMessage="Enter Inner Box" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">SS Tin, Drum, Bucket, Bag</span>
                                            </div>
                                            <asp:TextBox ID="txtsstindrumbucketbag" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtsstindrumbucketbag" runat="server" ControlToValidate="txtsstindrumbucketbag" Display="None" ValidationGroup="g1" ErrorMessage="Enter SS Tin, Drum, Bucket, Bag" />
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Inner Box Cello Tape</span>
                                            </div>
                                            <asp:TextBox ID="txtinnerboxcellotape" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtinnerboxcellotape" runat="server" ControlToValidate="txtinnerboxcellotape" Display="None" ValidationGroup="g1" ErrorMessage="Enter Inner Box Cello Tape" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">kitchen tray</span>
                                            </div>
                                            <asp:TextBox ID="txtkitchentray" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtkitchentray" runat="server" ControlToValidate="txtkitchentray" Display="None" ValidationGroup="g1" ErrorMessage="Enter kitchen tray" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Additional (Other)</span>
                                            </div>
                                            <asp:TextBox ID="txtadditionalother" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtadditionalother" runat="server" ControlToValidate="txtadditionalother" Display="None" ValidationGroup="g1" ErrorMessage="Enter Additional (Other)" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Stapping/ Wt.</span>
                                            </div>
                                            <asp:TextBox ID="txtstappnigwt" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtstappnigwt" runat="server" ControlToValidate="txtstappnigwt" Display="None" ValidationGroup="g1" ErrorMessage="Stapping/ Wt." />
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Outer Label/ BOPP Filling/ Box Filling</span>
                                            </div>
                                            <asp:TextBox ID="txtouterlblboppboxfill" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtouterlblboppboxfill" runat="server" ControlToValidate="txtouterlblboppboxfill" Display="None" ValidationGroup="g1" ErrorMessage="Outer Label/ BOPP Filling/ Box Filling" />
                                        </div>
                                        <div class="input-group mb-3">
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" style="border-color: green">Total Labour / Task</span>
                                            </div>
                                            <asp:TextBox ID="txtTotalLabourPerTask" onchange="calculaterate();" Enabled="false" Style="border-color: green; border-width: 1px" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <%--------------------End Add Labour Task--------------------------%>


                                    <%-------------------- Add Power Details / Hour--------------------------%>

                                    <div class="col-md-12">
                                        <i class="fas fa-table me-1"></i>
                                        <b style="margin-left: 3px">Add Power Details / Hour</b>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Filling</span>
                                            </div>
                                            <asp:TextBox ID="txtpfilling" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpfilling" runat="server" ControlToValidate="txtpfilling" Display="None" ValidationGroup="g1" ErrorMessage="Filling" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Capping</span>
                                            </div>
                                            <asp:TextBox ID="txtpcapping" onchange="calculaterate();" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpcapping" runat="server" ControlToValidate="txtpcapping" Display="None" ValidationGroup="g1" ErrorMessage="Enter Capping" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Induction</span>
                                            </div>
                                            <asp:TextBox ID="txtpinduction" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpinduction" runat="server" ControlToValidate="txtpinduction" Display="None" ValidationGroup="g1" ErrorMessage="Enter Power Induction" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Labeling</span>
                                            </div>
                                            <asp:TextBox ID="txtplabeling" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtplabeling" runat="server" ControlToValidate="txtplabeling" Display="None" ValidationGroup="g1" ErrorMessage="Enter Power Labeling" />
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Shrinking</span>
                                            </div>
                                            <asp:TextBox ID="txtshrinking" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtshrinking" runat="server" ControlToValidate="txtshrinking" Display="None" ValidationGroup="g1" ErrorMessage="Enter Shrinking" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">BOPP</span>
                                            </div>
                                            <asp:TextBox ID="txtbopp" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtbopp" runat="server" ControlToValidate="txtbopp" Display="None" ValidationGroup="g1" ErrorMessage="Enter BOPP" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Stepping</span>
                                            </div>
                                            <asp:TextBox ID="txtstepping" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtstepping" runat="server" ControlToValidate="txtstepping" Display="None" ValidationGroup="g1" ErrorMessage="Enter Stepping" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Sealing M/C</span>
                                            </div>
                                            <asp:TextBox ID="txtsealingmc" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtsealingmc" runat="server" ControlToValidate="txtsealingmc" Display="None" ValidationGroup="g1" ErrorMessage="Enter Sealing M/C" />
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">PowerDetail9 /Hour</span>
                                            </div>
                                            <asp:TextBox ID="txtpowerdetail9" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpowerdetail9" runat="server" ControlToValidate="txtpowerdetail9" Display="None" ValidationGroup="g1" ErrorMessage="Enter PowerDetail9 /Hour" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">PowerDetail10 /Hour</span>
                                            </div>
                                            <asp:TextBox ID="txtpowerdetail10" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpowerdetail10" runat="server" ControlToValidate="txtpowerdetail10" Display="None" ValidationGroup="g1" ErrorMessage="Enter PowerDetail10 /Hour" />
                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Power Unit/hour</span>
                                            </div>
                                            <asp:TextBox ID="txtpowerunit" class="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtpowerunit" runat="server" ControlToValidate="txtpowerunit" Display="None" ValidationGroup="g1" ErrorMessage="Enter Power Unit/hour" />
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Other Power</span>
                                            </div>
                                            <asp:TextBox ID="txtotherpower" CssClass="form-control" onchange="calculaterate();" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txtotherpower" runat="server" ControlToValidate="txtotherpower" Display="None" ValidationGroup="g1" ErrorMessage="Other Power" />
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="input-group mb-3 mt-2">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" style="border-color: green">Total Power</span>
                                            </div>
                                            <asp:TextBox ID="txttotalpower" CssClass="form-control" onchange="calculaterate();" Enabled="false" TextMode="Number" Style="border-color: green; border-width: 1px" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rf_txttotalpower" runat="server" ControlToValidate="txttotalpower" Display="None" ValidationGroup="g1" ErrorMessage="Enter Total Power" />
                                        </div>

                                    </div>

                                    <%--------------------End  Power Details / Hour--------------------------%>
                                    <div class="col-md-6">
                                    </div>

                                    <div class="col-md-6">
                                        <asp:Button ID="btnupdate" class="btn btn-success" Visible="false" OnClick="btnupdate_Click" runat="server" Text="Update" />
                                        <asp:Button ID="btnadd" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="g1" CausesValidation="true" runat="server" Text="Add" />
                                        <asp:Button ID="btncancel" CssClass="btn btn-info " runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                        <asp:HiddenField ID="hdnUnitMeasurementId" runat="server" />
                                        <asp:HiddenField ID="hdnpmrmpriceid" runat="server" />
                                    </div>

                                    <div class="container col-12">
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>


                        </div>




                        <div class="container col-12">

                            <asp:GridView ID="gvpslcmaster" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped gridview " ClientIDMode="Static">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PackingCategoryName" HeaderText="Packing Category" />

                                    <asp:BoundField DataField="PackingStyleName" HeaderText="PackingStyle" />
                                    <asp:BoundField DataField="Unit" HeaderText="Packing Size" />
                                    <%--<asp:BoundField DataField="Task" HeaderText="Total Labour Task" />--%>
                                    <%--<asp:BoundField DataField="Power" HeaderText="Packing Total Power Details" />--%>

                                    <asp:TemplateField HeaderText="Task">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalLabourTask" runat="server" Text='<%#Eval("Task") %>' CssClass="tooltips" data-placement="right"
                                                data-html="true" title='<%# string.Format("TaskBulkCharge:{0}" +
                                                                                                                            "\r TaskPouchFilling:--{1}"+
                                                                                                                            "\r TaskBottleKeeping:--{2}"+
                                                                                                                       "\r TaskLiftingWeight:--{3}"+
                                                                                                                           "\r TaskBlacklinnerPouch:--{4}" + 
                                                                                                                           "\r TaskInnerPlug:--{5}"+
                                                                                                                        "\r TaskMesuringCap:---{6}" +
                                                                                                                        "\r TaskCaping:----{7}"+
                                                                                                                       "\r TaskTearDownSeal:----{8}" +
                                                                                                                       "\r TaskInduction:----- {9}"+
                                                                                                                         "\r TaskPouchSealing:---{10}" +
                                                                                                                         "\r TaskBottlePouchCleaning:----- {11}"+
                                                                                                                         "\r TaskLabeling:----{12}" +
                                                                                                                         "\r TaskSleeve:-----{13}"+
                                                                                                                          "\r TaskInnerbox:--------{14}" + 
                                                                                                                          "\r TaskSSTinDrumBucketBag:----{15}"+
                                                                                                                            "\r TaskInnerBoxCelloTape:--- {16}" + 
                                                                                                                            "\r TaskkitchenTray:-------{17}"+
                                                                                                                             "\r TaskOuterLabelBOPPBoxFilling:-----{18}" + 
                                                                                                                             "\r TaskStappingWeight:------- {19}"+
                                                                                                                               "\r TaskAdditionalOther:-----{20}",

    
    
                                                                                                                Eval("TaskBulkCharge"),Eval("TaskPouchFilling"),
                                                                                                                Eval("TaskBottleKeeping"),Eval("TaskLiftingWeight"),
                                                                                                                  Eval("TaskBlacklinnerPouch"),Eval("TaskInnerPlug"),
                                                                                                                    Eval("TaskMesuringCap"),Eval("TaskCaping"),
                                                                                                                      Eval("TaskTearDownSeal"),Eval("TaskInduction"),
                                                                                                                       Eval("TaskPouchSealing"),Eval("TaskBottlePouchCleaning"),
                                                                                                                        Eval("TaskLabeling"),Eval("TaskSleeve"),
                                                                                                                         Eval("TaskInnerbox"),Eval("TaskSSTinDrumBucketBag"),
                                                                                                                          Eval("TaskInnerBoxCelloTape"),Eval("TaskkitchenTray"),
                                                                                                                          Eval("TaskOuterLabelBOPPBoxFilling"),Eval("TaskStappingWeight"),
                                                                                                                            Eval("TaskAdditionalOther")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PackingTotalPowerDetails">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalPowerTask" runat="server" Text='<%#Eval("Power") %>' CssClass="tooltips" data-placement="right"
                                                data-html="true" title='<%# string.Format("PowerFilling: ----------------------------  {0}" +
                                                                                                                            "\rPowerCapping: ------------------------------   {1}"+
                                                                                                                                  "\rPowerInduction:--------------------------  {2}" +
                                                                                                                                  "\r PowerLableling:-------------------------------- {3}"+
                                                                                                                                   "\rPowerBOPP: ----------------------------------  {4}" + 
                                                                                                                                   "\r PowerShrinking:---------------------------- {5}"+
                                                                                                                                     "\rPowerStepping: -------------------------------{6}" +
                                                                                                                                     "\r PowerStealingMC:---------------------------- {7}"+
                                                                                                                                        "\rPowerDetail9: --------------------------------{8}" +
                                                                                                                                        "\r PowerDetail10:------------------------------- {9}"+
                                                                                                                                            "\r PowerUnitPerHour:------------------------------- {10}"+
                                                                                                                                            "\r PowerOther:------------------------------- {11}",
    
    
    
                                                                                                                             Eval("PowerFilling"), Eval("PowerCapping"),
                                                                                                                             Eval("PowerInduction"), Eval("PowerLableling"),
                                                                                                                                Eval("PowerBOPP"), Eval("PowerShrinking"),
                                                                                                                                 Eval("PowerStepping"), Eval("PowerStealingMC"),
                                                                                                                                  Eval("PowerDetail9"), Eval("PowerDetail10"),
                                                                                                                                  Eval("PowerUnitPerHour"), Eval("PowerOther") ) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TaskBulkCharge" Visible="false" />
                                    <asp:BoundField DataField="TaskPouchFilling" Visible="false" />
                                    <asp:BoundField DataField="TaskBottleKeeping" Visible="false" />
                                    <asp:BoundField DataField="TaskLiftingPouchBottleWt" Visible="false" />
                                    <asp:BoundField DataField="TaskBlacklinnerPouch" Visible="false" />
                                    <asp:BoundField DataField="TaskInnerPlug" Visible="false" />
                                    <asp:BoundField DataField="TaskMesuringCap" Visible="false" />
                                    <asp:BoundField DataField="TaskCaping" Visible="false" />
                                    <asp:BoundField DataField="TaskTearDownSeal" Visible="false" />
                                    <asp:BoundField DataField="TaskInduction" Visible="false" />
                                    <asp:BoundField DataField="TaskPouchSealing" Visible="false" />
                                    <asp:BoundField DataField="TaskBottlePouchCleaning" Visible="false" />
                                    <asp:BoundField DataField="TaskLabeling" Visible="false" />
                                    <asp:BoundField DataField="TaskSleeve" Visible="false" />
                                    <asp:BoundField DataField="TaskInnerbox" Visible="false" />
                                    <asp:BoundField DataField="TaskSSTinDrumBucketBag" Visible="false" />
                                    <asp:BoundField DataField="TaskInnerBoxCelloTape" Visible="false" />
                                    <asp:BoundField DataField="TaskkitchenTray" Visible="false" />
                                    <asp:BoundField DataField="TaskOuterLabelBOPPFillingBoxFilling" Visible="false" />
                                    <asp:BoundField DataField="TaskStappingWt" Visible="false" />
                                    <asp:BoundField DataField="TaskAdditionalOther" Visible="false" />
                                    <asp:BoundField DataField="TaskStappingWt" Visible="false" HeaderText="TaskStappingWt" />


                                    <asp:BoundField DataField="PowerFilling" Visible="false" HeaderText="PowerFilling" SortExpression="PowerFilling" />
                                    <asp:BoundField DataField="PowerCapping" Visible="false" HeaderText="PowerCapping" SortExpression="PowerCapping" />
                                    <asp:BoundField DataField="PowerInduction" Visible="false" HeaderText="PowerInduction" SortExpression="PowerInduction" />
                                    <asp:BoundField DataField="PowerLableling" Visible="false" HeaderText="PowerLableling" SortExpression="PowerLableling" />
                                    <asp:BoundField DataField="PowerShrinking" Visible="false" HeaderText="PowerShrinking" SortExpression="PowerShrinking" />
                                    <asp:BoundField DataField="PowerBOPP" Visible="false" HeaderText="PowerBOPP" SortExpression="PowerBOPP" />
                                    <asp:BoundField DataField="PowerStepping" Visible="false" HeaderText="PowerStepping" SortExpression="PowerStepping" />
                                    <asp:BoundField DataField="PowerStealingMC" Visible="false" HeaderText="PowerStealingMC" SortExpression="PowerStealingMC" />
                                    <asp:BoundField DataField="PowerDetail9" Visible="false" HeaderText="PowerDetail9" SortExpression="PowerDetail9" />
                                    <asp:BoundField DataField="PowerDetail10" Visible="false" HeaderText="PowerDetail10" SortExpression="PowerDetail10" />
                                    <asp:BoundField DataField="PowerUnitPerHour" Visible="false" HeaderText="PowerUnitPerHour" SortExpression="PowerDetail10" />
                                    <asp:BoundField DataField="PowerOther" Visible="false" HeaderText="PowerOther" SortExpression="PowerDetail10" />

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandArgument='<%#  Eval("PackingStyleLabourCostingId") %>' OnClick="btnEdit_Click" CssClass="btn btn-sm btn-primary" />
                                            <asp:Button ID="btnDelete" Text="Delete" runat="server" CommandArgument='<%#  Eval("PackingStyleLabourCostingId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete this item?');" CssClass="btn btn-sm btn-danger" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                        </div>
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
                bindDataTable();
                function bindDataTable() {
                    $('#<%= gvpslcmaster.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvpslcmaster.ClientID %>').find("tr:first"))).DataTable({
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
            };
        });
       

        function calculaterate() {

            //---------------------------------Labour Task-------------------------------

            var txtbulkcharge = document.getElementById('<%=txtbulkcharge.ClientID %>');
            var txtpouchfilling = document.getElementById('<%=txtpouchfilling.ClientID %>');

            var txtbottlekeeping = document.getElementById('<%=txtbottlekeeping.ClientID %>');
            var txtliftpouchbttwt = document.getElementById('<%=txtliftpouchbttwt.ClientID %>');

            var txtblackinnerpouch = document.getElementById('<%=txtblackinnerpouch.ClientID %>');
            var txtinnerplug = document.getElementById('<%=txtinnerplug.ClientID %>');

            var txtmeasureingcap = document.getElementById('<%=txtmeasureingcap.ClientID %>');
            var txtlcaping = document.getElementById('<%=txtlcaping.ClientID %>');

            var txtteardownseal = document.getElementById('<%=txtteardownseal.ClientID %>');
            var txtlInduction = document.getElementById('<%=txtlInduction.ClientID %>');

            var txtpouchsealing = document.getElementById('<%=txtpouchsealing.ClientID %>');
            var txtbottlepouchcleaning = document.getElementById('<%=txtbottlepouchcleaning.ClientID %>');

            var txtlabeling = document.getElementById('<%=txtlabeling.ClientID %>');
            var txtsleeve = document.getElementById('<%=txtsleeve.ClientID %>');

            var txtinnerbox = document.getElementById('<%=txtinnerbox.ClientID %>');
            var txtsstindrumbucketbag = document.getElementById('<%=txtsstindrumbucketbag.ClientID %>');

            var txtinnerboxcellotape = document.getElementById('<%=txtinnerboxcellotape.ClientID %>');
            var txtkitchentray = document.getElementById('<%=txtkitchentray.ClientID %>');

            var txtadditionalother = document.getElementById('<%=txtadditionalother.ClientID %>');
            var txtstappnigwt = document.getElementById('<%=txtstappnigwt.ClientID %>');

            var txtouterlblboppboxfill = document.getElementById('<%=txtouterlblboppboxfill.ClientID %>');
            var txtTotalLabourPerTask = document.getElementById('<%=txtTotalLabourPerTask.ClientID %>');



            txtbulkcharge.value = parseFloat(txtbulkcharge.value) || '0';
            txtpouchfilling.value = parseFloat(txtpouchfilling.value) || '0';
            txtbottlekeeping.value = parseFloat(txtbottlekeeping.value) || '0';
            txtliftpouchbttwt.value = parseFloat(txtliftpouchbttwt.value) || '0';
            txtblackinnerpouch.value = parseFloat(txtblackinnerpouch.value) || '0';
            txtinnerplug.value = parseFloat(txtinnerplug.value) || '0';
            txtmeasureingcap.value = parseFloat(txtmeasureingcap.value) || '0';
            txtlcaping.value = parseFloat(txtlcaping.value) || '0';
            txtteardownseal.value = parseFloat(txtteardownseal.value) || '0';
            txtlInduction.value = parseFloat(txtlInduction.value) || '0';
            txtpouchsealing.value = parseFloat(txtpouchsealing.value) || '0';
            txtbottlepouchcleaning.value = parseFloat(txtbottlepouchcleaning.value) || '0';
            txtlabeling.value = parseFloat(txtlabeling.value) || '0';
            txtsleeve.value = parseFloat(txtsleeve.value) || '0';
            txtinnerbox.value = parseFloat(txtinnerbox.value) || '0';
            txtsstindrumbucketbag.value = parseFloat(txtsstindrumbucketbag.value) || '0';
            txtinnerboxcellotape.value = parseFloat(txtinnerboxcellotape.value) || '0';
            txtkitchentray.value = parseFloat(txtkitchentray.value) || '0';
            txtadditionalother.value = parseFloat(txtadditionalother.value) || '0';
            txtstappnigwt.value = parseFloat(txtstappnigwt.value) || '0';
            txtouterlblboppboxfill.value = parseFloat(txtouterlblboppboxfill.value) || '0';


            document.getElementById('<%=txtTotalLabourPerTask.ClientID %>').value = (parseFloat(txtbulkcharge.value)) + (parseFloat(txtpouchfilling.value)) + (parseFloat(txtbottlekeeping.value)) + (parseFloat(txtliftpouchbttwt.value))
                + (parseFloat(txtblackinnerpouch.value)) + (parseFloat(txtinnerplug.value)) + (parseFloat(txtmeasureingcap.value)) + (parseFloat(txtlcaping.value)) + (parseFloat(txtteardownseal.value)) + (parseFloat(txtlInduction.value))
                + (parseFloat(txtpouchsealing.value)) + (parseFloat(txtbottlepouchcleaning.value)) + (parseFloat(txtlabeling.value)) + (parseFloat(txtsleeve.value)) + (parseFloat(txtinnerbox.value)) + (parseFloat(txtsstindrumbucketbag.value))
                + (parseFloat(txtinnerboxcellotape.value)) + (parseFloat(txtkitchentray.value)) + (parseFloat(txtadditionalother.value)) + (parseFloat(txtstappnigwt.value)) + (parseFloat(txtouterlblboppboxfill.value))

            //---------------------------------------Labour Task End-----------------------------------------------------------------------


            //--------------------------------------Start Power Task-----------------------------------------------------------------------
            var txtpfilling = document.getElementById('<%=txtpfilling.ClientID %>');
            var txtpinduction = document.getElementById('<%=txtpinduction.ClientID %>');

            var txtpcapping = document.getElementById('<%=txtpcapping.ClientID %>');

            var txtplabeling = document.getElementById('<%=txtplabeling.ClientID %>');
            var txtshrinking = document.getElementById('<%=txtshrinking.ClientID %>');

            var txtbopp = document.getElementById('<%=txtbopp.ClientID %>');
            var txtstepping = document.getElementById('<%=txtstepping.ClientID %>');

            var txtsealingmc = document.getElementById('<%=txtsealingmc.ClientID %>');
            var txtpowerdetail9 = document.getElementById('<%=txtpowerdetail9.ClientID %>');

            var txtpowerdetail10 = document.getElementById('<%=txtpowerdetail10.ClientID %>');
            var txtpowerunit = document.getElementById('<%=txtpowerunit.ClientID %>');

            var txtotherpower = document.getElementById('<%=txtotherpower.ClientID %>');
            var txttotalpower = document.getElementById('<%=txttotalpower.ClientID %>');



            txtpfilling.value = parseFloat(txtpfilling.value) || '0';
            txtpinduction.value = parseFloat(txtpinduction.value) || '0';
            txtpcapping.value = parseFloat(txtpcapping.value) || '0';

            txtplabeling.value = parseFloat(txtplabeling.value) || '0';
            txtshrinking.value = parseFloat(txtshrinking.value) || '0';

            txtbopp.value = parseFloat(txtbopp.value) || '0';
            txtstepping.value = parseFloat(txtstepping.value) || '0';

            txtsealingmc.value = parseFloat(txtsealingmc.value) || '0';
            txtpowerdetail9.value = parseFloat(txtpowerdetail9.value) || '0';

            txtpowerdetail10.value = parseFloat(txtpowerdetail10.value) || '0';
            txtpowerunit.value = parseFloat(txtpowerunit.value) || '0';

            txtotherpower.value = parseFloat(txtotherpower.value) || '0';
            txttotalpower.value = parseFloat(txttotalpower.value) || '0';

            
            document.getElementById('<%=txttotalpower.ClientID %>').value = (parseFloat(txtpfilling.value)) + (parseFloat(txtpinduction.value)) + (parseFloat(txtpcapping.value)) + (parseFloat(txtplabeling.value)) + (parseFloat(txtshrinking.value))
                + (parseFloat(txtbopp.value)) + (parseFloat(txtstepping.value)) + (parseFloat(txtsealingmc.value)) + (parseFloat(txtpowerdetail9.value)) + (parseFloat(txtpowerdetail10.value)) + (parseFloat(txtpowerunit.value))
                + (parseFloat(txtotherpower.value)) 
            //---------------------------------------Power Task End-----------------------------------------------------------------------


           
        }


    </script>
    <script language="javascript">
        $(document).ready(function () {
            $(".tooltip").closest("tr").mousemove(function (event) {
                $(this).find(".tooltip").css({
                    "left": event.pageX + 1,
                    "top": event.pageY + 1
                }).show();
            }).mouseout(function () { $(this).find(".tooltip").hide(); });;
        });
    </script>
</asp:Content>
