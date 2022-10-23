<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PackingDifferenceMaster.aspx.cs" Inherits="Production_Costing_Software.PackingDifferenceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card-body">
        <div class="contain">
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    <b>Packing Difference </b>
                </div>
                <div class="row">
                    <div class="col-12">
                        <br />
                    </div>
                </div>



                <div class="row">
                    <div class="col-12">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <ContentTemplate>



                                <asp:Button ID="UpdateBtn" OnClick="UpdateBtn_Click" OnClientClick="return getdata();" runat="server" Text="Update" Style="z-index: 999999999999; margin-left: 5px" CssClass="btn btn-primary position-fixed mt-1" />
                                <div class="container col-12">
                                    <asp:GridView ID="gvpackingdiff" runat="server" AutoGenerateColumns="true" OnRowDataBound="gvpackingdiff_RowDataBound" class="table table-bordered table-striped gridview" ClientIDMode="Static">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
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
        </div>
    </div>
    <script type="text/javascript">

        var table = "";

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
                $('#<%= gvpackingdiff.ClientID %>').prepend($("<thead></thead>").append($('#<%= gvpackingdiff.ClientID %>').find("tr:first"))).DataTable({
                    "destroy": true,
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": false,
                    "autoWidth": false,
                    //"responsive": true,
                    fixedHeader: {
                        header: true,
                        footer: false
                    },
                    "pageLength": 50
                });
                table = $('#<%= gvpackingdiff.ClientID %>').DataTable();

            }
        });

        function getdata() {
            try {
                var nodes = table.rows({ page: 'all' }).nodes();
                const list = [];
                var lst_data = "";
                for (var row = 0; row < nodes.length; row++) {
                    var data = "";
                    for (var i = 5; i < nodes[row].cells.length; i++) {

                        var ctrl = nodes[row].cells[i].getElementsByTagName("input");
                        if (ctrl.length > 0) {

                            if (i == 5) {
                                var txt = ctrl[0];
                                if (txt != null) {
                                    data += "~" + txt.value + "$";
                                }
                            }
                            else if (i == 6) {
                                if (ctrl.length == 2) {

                                    data += "" + ctrl[0].value;
                                    data += "~" + ctrl[1].value;
                                }
                            }
                            else {
                                if (ctrl.length == 2) {

                                    data += "$" + ctrl[0].value;
                                    data += "~" + ctrl[1].value;
                                }
                            }

                        }

                    }

                    lst_data += "," + data;
                }
                var theIds = JSON.stringify(list);

                $.ajax({
                    url: 'WebService.asmx/PDSave',
                    data: { pd: lst_data },
                    method: 'POST',
                    async: false,
                    //dataType: 'json',
                    success: function (r) {
                        if (r.all[0].textContent == '1') {
                            alert('Packing Difference Updated Successfully');
                            return true;
                        }

                    }
                });
            }
            catch (ex) {
                alert('Error while save Data');
                return false;
            }


        }

        function updatedata(id) {

            var grd = '<%=gvpackingdiff.ClientID%>';
            var index = id.substring(parseInt(id.lastIndexOf("_")) + 1);
            document.getElementById(id).value = Number(document.getElementById(id).value).toFixed(2).toString();
            var val = document.getElementById(id).value;
            var tr = document.getElementById(id).parentNode.parentNode;
            var cells = tr.cells;
            for (var i = 0; i < cells.length; i++) {

                var txt = cells[i].getElementsByTagName("input")[1];
                if (txt != null) {
                    txt.value = val;

                    var lbl = txt.id.replace("_txt_", "_lbl_");
                    document.getElementById(lbl).innerHTML = val;
                }
            }
            var lblmain = id.replace("_txt_", "_lbl_");
            document.getElementById(lblmain).innerHTML = val;
        }
        function updatedatatolbl(from, to) {

            document.getElementById(to).innerHTML = document.getElementById(from).value;

        }


    </script>
    <style>
        #float {
            position: fixed;
            top: 3em;
            right: 2em;
            z-index: 100;
        }
    </style>
</asp:Content>
