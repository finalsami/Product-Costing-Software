using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using BAL;
using DAL;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Spire.Pdf.Exporting.XPS.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TableCell = System.Web.UI.WebControls.TableCell;

namespace Production_Costing_Software
{
    public partial class PriceListGP : System.Web.UI.Page
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        PriceListGPActualEstimateDAL plgp = new PriceListGPActualEstimateDAL();
        PriceListGPActualEstimateBAL plgpdata = new PriceListGPActualEstimateBAL();
        //int FkBulkProductId = 0;
        //int rowID = 0;
        string[] ArrRemoveColumns = { "BPM_Product_Name", "Date_Last_Shared_Price", "PriceTypeId", "FkBulkProductId", "EnumDescription", "Action", "PriceListGPActualEstimateId", "RMPriceEstimateId", "TradeId" };
        int rowID = 0;
        int CheckCount = 0;
        int CompanyId = 0;
        string BPM_Id = "";
        string BPMName = "";
        public string datajs = "";

        string[] ArrRemoveColumnsNew = { "BulkProductName", "FkBulkProductId", "TradeName", "PackMeasure", "EnumDescription", "StateName", "AsOnDate", "RPL", "NCR" };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CompanyId = Common.ConvertInt(Session["CompanyId"]);
                binddropdown();

                binddata();
                binddataExcel();
            }
            else
            {
                DataTable dtNew = ViewState["dtNew"] as DataTable;
                //rowID = 0;
                gvpricelistgpactual.DataSource = dtNew;
                gvpricelistgpactual.DataBind();


                DataTable dtNewExcel = ViewState["dtNewExcel"] as DataTable;
                //rowID = 0;
                gvpricelistgpactualExcel.DataSource = dtNewExcel;
                gvpricelistgpactualExcel.DataBind();
                //DataTable dtNewReport = ViewState["dtNewReport"] as DataTable;

                //gvstatewisereport.DataSource = dtNewReport;
                //gvstatewisereport.DataBind();
                //gvstatewisereport.Visible = true;
            }

        }
        private void binddata()
        {
            datajs = "";
            DataTable dt = new DataTable();

            dt = plgp.GetPriceListGPActual(0, Common.ConvertInt(Session["CompanyId"]));

            DataTable dtNew = new DataTable();
            dtNew.Clear();
            //dtNew.Columns.Add("No", typeof(string));

            dtNew.Columns.Add("BPM_Product_Name", typeof(string));

            dtNew.Columns.Add("FkBulkProductId", typeof(int));
            //dtNew.Columns.Add("Status", typeof(string));

            dtNew.Columns.Add("PriceTypeId", typeof(int));
            dtNew.Columns.Add("Date_Last_Shared_Price", typeof(string));
            dtNew.Columns.Add("EnumDescription", typeof(string));
            dtNew.Columns.Add("PriceListGPActualEstimateId", typeof(string));
            dtNew.Columns.Add("RMPriceEstimateId", typeof(string));
            dtNew.Columns.Add("TradeId", typeof(string));

            dtNew.AcceptChanges();
            var distinctBPMValues = dt.AsEnumerable().Select(row => new { FkBulkProductId = row.Field<Int32>("FkBulkProductId"), }).Distinct();
            //var distinctEnumValues = dt.AsEnumerable().Select(row => new { EnumValues = row.Field<Int32>("EnumDescription"), }).Distinct();

            foreach (var itemBPM in distinctBPMValues)
            {
                foreach (DataColumn item in dt.Columns)
                {
                    if (!Array.Exists(ArrRemoveColumns, element => element == item.ColumnName))
                    {
                        var Data = dt.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId  && row.Field<string>(item.ColumnName) != null && !string.IsNullOrEmpty(row.Field<string>(item.ColumnName).ToString()));
                       
                        
                        DataRow dr;
                        if (Data.Any())
                        {
                            DataTable objdtData = Data.CopyToDataTable();

                            if (!dtNew.Columns.Contains(item.ColumnName))
                            {
                                dtNew.Columns.Add(item.ColumnName, typeof(decimal));
                                dtNew.Columns.Add(item.ColumnName + "New", typeof(string));
                                dtNew.AcceptChanges();
                            }



                            foreach (DataRow drData in objdtData.Rows)
                            {

                                var DataNCROrRPL = dtNew.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId && row.Field<int>("PriceTypeId") == Convert.ToInt32(drData["PriceTypeId"]) );

                                if (DataNCROrRPL.Any())
                                {
                                    foreach (var row in DataNCROrRPL)
                                    {
                                        row.SetField(item.ColumnName, Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[0]));
                                        row.SetField(item.ColumnName + "New", Convert.ToString(drData[item.ColumnName].ToString().Split('|')[1]));
                                    }
                                }
                                else
                                {
                                    dr = dtNew.NewRow();
                                    //dr["No"] = Convert.ToString(drData["No"]);

                                    dr["BPM_Product_Name"] = Convert.ToString(drData["BPM_Product_Name"]);

                                    dr["FkBulkProductId"] = Convert.ToInt32(drData["FkBulkProductId"]);
                                    dr["PriceTypeId"] = Convert.ToInt32(drData["PriceTypeId"]);
                                    dr["Date_Last_Shared_Price"] = Convert.ToString(drData["Date_Last_Shared_Price"]);
                                    dr["EnumDescription"] = Convert.ToString(drData["EnumDescription"]);
                                    dr["PriceListGPActualEstimateId"] = Convert.ToInt32(drData["PriceListGPActualEstimateId"]);
                                    dr["RMPriceEstimateId"] = Convert.ToInt32(drData["RMPriceEstimateId"]);
                                    dr["TradeId"] = Convert.ToInt32(drData["TradeId"]);

                                    //dr[item.ColumnName] = Convert.ToDecimal(drData[item.ColumnName]);
                                    dr[item.ColumnName] = Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[0]);
                                    dr[item.ColumnName + "New"] = Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[1]);
                                    dtNew.Rows.Add(dr);
                                }
                                dtNew.AcceptChanges();
                            }
                        }
                        else
                        {
                            var EmptyData = dt.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId);
                            if (EmptyData.Any())
                            {
                                DataTable objdtItem = EmptyData.CopyToDataTable();

                                foreach (DataRow drnonState in objdtItem.Rows)
                                {
                                    var IsDataAvailable = dtNew.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId && row.Field<int>("PriceTypeId") == Convert.ToInt32(drnonState["PriceTypeId"]) && row.Field<string>("EnumDescription") == Convert.ToString(drnonState["EnumDescription"]));
                                    if (!IsDataAvailable.Any())
                                    {
                                        dr = dtNew.NewRow();
                                        //dr["No"] = Convert.ToString(drnonState["No"]);

                                        dr["BPM_Product_Name"] = Convert.ToString(drnonState["BPM_Product_Name"]);
                                        dr["FkBulkProductId"] = Convert.ToInt32(itemBPM.FkBulkProductId);
                                        dr["PriceTypeId"] = Convert.ToInt32(drnonState["PriceTypeId"]);
                                        dr["Date_Last_Shared_Price"] = Convert.ToString(drnonState["Date_Last_Shared_Price"]);
                                        dr["EnumDescription"] = Convert.ToString(drnonState["EnumDescription"]);
                                        dr["PriceListGPActualEstimateId"] = Convert.ToInt32(drnonState["PriceListGPActualEstimateId"]);
                                        dr["RMPriceEstimateId"] = Convert.ToInt32(drnonState["RMPriceEstimateId"]);
                                        dr["TradeId"] = Convert.ToInt32(drnonState["TradeId"]);


                                        dtNew.Rows.Add(dr);

                                    }
                                }

                            }
                        }
                    }
                }
            }

            dtNew.Columns.Add("Action", typeof(string));
            dtNew.AcceptChanges();
            ViewState["dtNew"] = dtNew;
            //rowID = 0;
            gvpricelistgpactual.DataSource = dtNew;

            gvpricelistgpactual.DataBind();

        }
        private void binddataExcel()
        {
            datajs = "";
            DataTable dt = new DataTable();

            dt = plgp.GetPriceListGPActual(0, Common.ConvertInt(Session["CompanyId"]));

            DataTable dtNewExcel = new DataTable();
            dtNewExcel.Clear();
            //dtNewExcel.Columns.Add("No", typeof(string));

            dtNewExcel.Columns.Add("BPM_Product_Name", typeof(string));

            dtNewExcel.Columns.Add("FkBulkProductId", typeof(int));
            //dtNewExcel.Columns.Add("Status", typeof(string));

            dtNewExcel.Columns.Add("PriceTypeId", typeof(int));
            dtNewExcel.Columns.Add("Date_Last_Shared_Price", typeof(string));
            dtNewExcel.Columns.Add("EnumDescription", typeof(string));
            dtNewExcel.Columns.Add("PriceListGPActualEstimateId", typeof(string));
            dtNewExcel.Columns.Add("RMPriceEstimateId", typeof(string));
            dtNewExcel.Columns.Add("TradeId", typeof(string));

            dtNewExcel.AcceptChanges();
            var distinctBPMValues = dt.AsEnumerable().Select(row => new { FkBulkProductId = row.Field<Int32>("FkBulkProductId"), }).Distinct();
            //var distinctEnumValues = dt.AsEnumerable().Select(row => new { EnumValues = row.Field<Int32>("EnumDescription"), }).Distinct();

            foreach (var itemBPM in distinctBPMValues)
            {
                foreach (DataColumn item in dt.Columns)
                {
                    if (!Array.Exists(ArrRemoveColumns, element => element == item.ColumnName))
                    {
                        var Data = dt.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId && row.Field<string>(item.ColumnName) != null && !string.IsNullOrEmpty(row.Field<string>(item.ColumnName).ToString()));


                        DataRow dr;
                        if (Data.Any())
                        {
                            DataTable objdtData = Data.CopyToDataTable();

                            if (!dtNewExcel.Columns.Contains(item.ColumnName))
                            {
                                dtNewExcel.Columns.Add(item.ColumnName, typeof(decimal));
                                dtNewExcel.Columns.Add(item.ColumnName + "New", typeof(string));
                                dtNewExcel.AcceptChanges();
                            }



                            foreach (DataRow drData in objdtData.Rows)
                            {

                                var DataNCROrRPL = dtNewExcel.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId && row.Field<int>("PriceTypeId") == Convert.ToInt32(drData["PriceTypeId"]));

                                if (DataNCROrRPL.Any())
                                {
                                    foreach (var row in DataNCROrRPL)
                                    {
                                        row.SetField(item.ColumnName, Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[0]));
                                        row.SetField(item.ColumnName + "New", Convert.ToString(drData[item.ColumnName].ToString().Split('|')[1]));
                                    }
                                }
                                else
                                {
                                    dr = dtNewExcel.NewRow();
                                    //dr["No"] = Convert.ToString(drData["No"]);

                                    dr["BPM_Product_Name"] = Convert.ToString(drData["BPM_Product_Name"]);

                                    dr["FkBulkProductId"] = Convert.ToInt32(drData["FkBulkProductId"]);
                                    dr["PriceTypeId"] = Convert.ToInt32(drData["PriceTypeId"]);
                                    dr["Date_Last_Shared_Price"] = Convert.ToString(drData["Date_Last_Shared_Price"]);
                                    dr["EnumDescription"] = Convert.ToString(drData["EnumDescription"]);
                                    dr["PriceListGPActualEstimateId"] = Convert.ToInt32(drData["PriceListGPActualEstimateId"]);
                                    dr["RMPriceEstimateId"] = Convert.ToInt32(drData["RMPriceEstimateId"]);
                                    dr["TradeId"] = Convert.ToInt32(drData["TradeId"]);

                                    //dr[item.ColumnName] = Convert.ToDecimal(drData[item.ColumnName]);
                                    dr[item.ColumnName] = Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[0]);
                                    dr[item.ColumnName + "New"] = Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[1]);
                                    dtNewExcel.Rows.Add(dr);
                                }
                                dtNewExcel.AcceptChanges();
                            }
                        }
                        else
                        {
                            var EmptyData = dt.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId);
                            if (EmptyData.Any())
                            {
                                DataTable objdtItem = EmptyData.CopyToDataTable();

                                foreach (DataRow drnonState in objdtItem.Rows)
                                {
                                    var IsDataAvailable = dtNewExcel.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId && row.Field<int>("PriceTypeId") == Convert.ToInt32(drnonState["PriceTypeId"]) && row.Field<string>("EnumDescription") == Convert.ToString(drnonState["EnumDescription"]));
                                    if (!IsDataAvailable.Any())
                                    {
                                        dr = dtNewExcel.NewRow();
                                        //dr["No"] = Convert.ToString(drnonState["No"]);

                                        dr["BPM_Product_Name"] = Convert.ToString(drnonState["BPM_Product_Name"]);
                                        dr["FkBulkProductId"] = Convert.ToInt32(itemBPM.FkBulkProductId);
                                        dr["PriceTypeId"] = Convert.ToInt32(drnonState["PriceTypeId"]);
                                        dr["Date_Last_Shared_Price"] = Convert.ToString(drnonState["Date_Last_Shared_Price"]);
                                        dr["EnumDescription"] = Convert.ToString(drnonState["EnumDescription"]);
                                        dr["PriceListGPActualEstimateId"] = Convert.ToInt32(drnonState["PriceListGPActualEstimateId"]);
                                        dr["RMPriceEstimateId"] = Convert.ToInt32(drnonState["RMPriceEstimateId"]);
                                        dr["TradeId"] = Convert.ToInt32(drnonState["TradeId"]);


                                        dtNewExcel.Rows.Add(dr);

                                    }
                                }

                            }
                        }
                    }
                }
            }

            //dtNewExcel.Columns.Add("Action", typeof(string));
            dtNewExcel.AcceptChanges();
            ViewState["dtNewExcel"] = dtNewExcel;
            //rowID = 0;
            gvpricelistgpactualExcel.DataSource = dtNewExcel;

            gvpricelistgpactualExcel.DataBind();

        }

        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("GetStateforPriceListGP", "", "");

            drpstate.DataSource = dtrm;
            drpstate.DataTextField = "Name";
            drpstate.DataValueField = "Id";
            drpstate.DataBind();




        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/PriceListGPActualEstimate.aspx");

        }

        protected void gvpricelistgpactual_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;

            e.Row.Cells[2].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                int Cellcount = e.Row.Cells.Count;
                DataTable dtNewEstimate = ViewState["dtNew"] as DataTable;
                //for (int i = 6; i < e.Row.Cells.Count; i++)
                //{
                //    rowID += 1;
                //    string strColumnName = dtNewEstimate.Columns[i].ColumnName;
                //    if (e.Row.Cells[i].Text == "&nbsp;")
                //    {

                //            System.Web.UI.WebControls.CheckBox chkSelect = new System.Web.UI.WebControls.CheckBox();

                //            chkSelect.ID = "chkSelect_" + rowID;
                //            chkSelect.AutoPostBack = true;
                //            chkSelect.CheckedChanged += new EventHandler(chkDynamicEstimate_CheckedChanged);

                //            e.Row.Cells[i].Controls.Add(chkSelect);


                //    }


                //    lblDynamicColumnCount.Text = (e.Row.Cells.Count).ToString();
                //}
                for (int colIndex = 0; colIndex < e.Row.Cells.Count; colIndex++)
                {

                    if (colIndex == Cellcount - 1)
                    {
                        Button btn;
                        btn = new Button();
                        btn.Text = "Edit";
                        btn.ID = dr[1].ToString() + "~" + dr[5].ToString()  + "~" + dr[7].ToString();
                        btn.CommandArgument = btn.ID;
                        btn.CommandName = "click";

                        HtmlAnchor a = new HtmlAnchor();
                        //a.Target = "_blank";
                        a.InnerHtml = "Edit";
                        a.HRef = "PriceListGPActualEstimate.aspx?bpmId=" + dr[1].ToString()  + "&TradeId=" + dr[7].ToString();
                        a.Attributes.Add("class", "btn btn-primary btn-block m-1");

                        //btn.Click += Btn_Click1;
                        //btn.ValidationGroup = "g2";
                        //btn.Font.Size = FontUnit.Point(9);
                        //btn.ControlStyle.CssClass = "btn btn-primary btn-block m-1";
                        e.Row.Cells[colIndex].Controls.Add(a);
                        lblDynamicColumnCount.Text = (e.Row.Cells.Count).ToString();

                    }


                    else
                    {

                        Label label = new Label();
                        var r = colIndex >= 1 ? colIndex : colIndex;
                        label.Text = dr[r].ToString();

                        e.Row.Cells[colIndex].Controls.Add(label);
                    }
                    if (colIndex == Cellcount - 1)
                    {
                        CheckBox chk;
                        chk = new CheckBox();
                        chk.ID = Common.ConvertString(dr[1]);

                        //HtmlAnchor a = new HtmlAnchor();
                        //a.Target = "_blank";
                        //a.InnerHtml = "Edit";
                        //a.HRef = "PriceListGPActualEstimate.aspx?bpmId=" + dr[1].ToString() + "&ActualEstimateId=" + dr[5].ToString();
                        //a.Attributes.Add("class", "btn btn-primary btn-block m-1");

                        //btn.Click += Btn_Click1;
                        //btn.ValidationGroup = "g2";
                        //btn.Font.Size = FontUnit.Point(9);
                        //btn.ControlStyle.CssClass = "btn btn-primary btn-block m-1";
                        e.Row.Cells[colIndex].Controls.Add(chk);
                    }
                }

            }
            for (int rowIndex = gvpricelistgpactual.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = gvpricelistgpactual.Rows[rowIndex];
                GridViewRow gvPreviousRow = gvpricelistgpactual.Rows[rowIndex + 1];
                for (int cellCount = 1; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    if (gvRow.Cells[0].Text == gvPreviousRow.Cells[0].Text)
                    {
                        if (gvPreviousRow.Cells[0].RowSpan < 2)
                        {
                            gvRow.Cells[0].RowSpan = 2;
                            gvRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan = 2;

                        }
                        else
                        {
                            gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                            gvRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;


                        }
                        gvPreviousRow.Cells[0].Visible = false;
                        gvRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Visible = false;


                    }

                }

            }


        }

        private void chkDynamicEstimate_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_Click1(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string dt = btn.CommandArgument;
            string id = Regex.Split(dt, @"\D+")[0];
            string IsEstimate = Regex.Split(dt, @"~")[1];
            string TradeId = Regex.Split(dt, @"~")[2];
            (Session["FkBulkProductId"]) = id;

            (Session["IsEstimate"]) = IsEstimate;
            (Session["TradeId"]) = TradeId;
            (Session["CmpId"]) = Common.ConvertString(Request.QueryString["CmpId"]);
            Response.Redirect("PriceListGPActualEstimate.aspx");

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.CommandArgument;

        }

        protected void gvpricelistgpactual_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Visible = false;
                DataTable dtNewActual = ViewState["dtNew"] as DataTable;
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Insert);
                GridViewRow HeaderGridRowCurrentNew = new GridViewRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Insert);
                foreach (DataColumn item in dtNewActual.Columns)
                {
                    if (!item.ColumnName.Contains("New") && !Array.Exists(ArrRemoveColumns, element => element == item.ColumnName))
                    {
                        TableCell HeaderCell = new TableCell();
                        HeaderCell.Text = item.ColumnName;
                        HeaderCell.ColumnSpan = 2;
                        HeaderCell.CssClass = "customCellMergeTH";
                        HeaderGridRow.Cells.Add(HeaderCell);

                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Current";
                        HeaderCell.CssClass = "customCellMergeTH";
                        HeaderGridRowCurrentNew.Cells.Add(HeaderCell);

                        HeaderCell = new TableCell();
                        HeaderCell.Text = "New";
                        HeaderCell.CssClass = "customCellMergeTH";
                        HeaderGridRowCurrentNew.Cells.Add(HeaderCell);
                        TableCell Cell = new TableCell();
                    }
                    else if (!item.ColumnName.Contains("New"))
                    {
                        if (item.ColumnName != "FkBulkProductId" && item.ColumnName != "PriceTypeId" && item.ColumnName != "PriceListGPActualEstimateId" && item.ColumnName != "RMPriceEstimateId" && item.ColumnName != "TradeId")
                        {
                            TableCell HeaderCell = new TableCell();

                            if (item.ColumnName == "Date_Last_Shared_Price")
                            {
                                HeaderCell.Text = "Last Date Shared Price";
                            }
                            else if (item.ColumnName == "EnumDescription")
                            {
                                HeaderCell.Text = "RPL / NCR";
                            }
                            else if (item.ColumnName == "BPM_Product_Name")
                            {
                                HeaderCell.Text = "BPM Product Name";
                            }
                            else
                            {
                                HeaderCell.Text = item.ColumnName;
                            }

                            HeaderCell.CssClass = "customCellMergeTH";
                            HeaderGridRow.Cells.Add(HeaderCell);

                            HeaderCell = new TableCell();
                            HeaderCell.Text = " ";
                            HeaderCell.CssClass = "customCellMergeTH";
                            HeaderGridRowCurrentNew.Cells.Add(HeaderCell);
                        }
                    }

                }
                gvpricelistgpactual.Controls[0].Controls.AddAt(0, HeaderGridRow);
                gvpricelistgpactual.Controls[0].Controls.AddAt(1, HeaderGridRowCurrentNew);

            }
        }

        protected void btnReportAllState_Click(object sender, EventArgs e)
        {
            if (drpreportcat.SelectedValue == "1")
            {
                //for excel report

                binddataExcel();
                ExportGridToExcelAllState(gvpricelistgpactualExcel);
            }


            if (drpreportcat.SelectedValue == "0")
            {
                //for pdf report

                ExportGridToPDFAllState();
            }

        }
        private void ExportGridToExcelAllState(Control GridView)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "GP" + " All State  :" + DateTime.Now.Date.ToShortDateString() + ".xls"));
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gvpricelistgpactualExcel.AllowPaging = false;
            gvpricelistgpactualExcel.RenderControl(htw);

            string headerTable = @"<Table><tr><td ><h3>" + "GP All State :-" + DateTime.Now.Date.ToShortDateString() + "</h3></td></tr></Table>";
            Response.Write(headerTable);
            Response.Write(sw.ToString());
            Response.End();

        }
        private void ExportGridToPDFAllState()
        {
            datajs = "";
            DataTable dt = new DataTable();

            dt = plgp.GetPriceListGPActual(0, Common.ConvertInt(Session["CompanyId"]));

            DataTable dtNew = new DataTable();
            dtNew.Clear();
            //dtNew.Columns.Add("No", typeof(string));

            dtNew.Columns.Add("BPM_Product_Name", typeof(string));

            dtNew.Columns.Add("FkBulkProductId", typeof(int));
            //dtNew.Columns.Add("Status", typeof(string));

            dtNew.Columns.Add("PriceTypeId", typeof(int));
            dtNew.Columns.Add("Date_Last_Shared_Price", typeof(string));
            dtNew.Columns.Add("EnumDescription", typeof(string));
            dtNew.Columns.Add("PriceListGPActualEstimateId", typeof(string));
            dtNew.Columns.Add("RMPriceEstimateId", typeof(string));
            dtNew.Columns.Add("TradeId", typeof(string));

            dtNew.AcceptChanges();
            var distinctBPMValues = dt.AsEnumerable().Select(row => new { FkBulkProductId = row.Field<Int32>("FkBulkProductId"), }).Distinct();
            //var distinctEnumValues = dt.AsEnumerable().Select(row => new { EnumValues = row.Field<Int32>("EnumDescription"), }).Distinct();

            foreach (var itemBPM in distinctBPMValues)
            {
                foreach (DataColumn item in dt.Columns)
                {
                    if (!Array.Exists(ArrRemoveColumns, element => element == item.ColumnName))
                    {
                        var Data = dt.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId && row.Field<string>(item.ColumnName) != null && !string.IsNullOrEmpty(row.Field<string>(item.ColumnName).ToString()));


                        DataRow dr;
                        if (Data.Any())
                        {
                            DataTable objdtData = Data.CopyToDataTable();

                            if (!dtNew.Columns.Contains(item.ColumnName))
                            {
                                dtNew.Columns.Add(item.ColumnName, typeof(decimal));
                                dtNew.Columns.Add(item.ColumnName + "New", typeof(string));
                                dtNew.AcceptChanges();
                            }



                            foreach (DataRow drData in objdtData.Rows)
                            {

                                var DataNCROrRPL = dtNew.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId && row.Field<int>("PriceTypeId") == Convert.ToInt32(drData["PriceTypeId"]));

                                if (DataNCROrRPL.Any())
                                {
                                    foreach (var row in DataNCROrRPL)
                                    {
                                        row.SetField(item.ColumnName, Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[0]));
                                        row.SetField(item.ColumnName + "New", Convert.ToString(drData[item.ColumnName].ToString().Split('|')[1]));
                                    }
                                }
                                else
                                {
                                    dr = dtNew.NewRow();
                                    //dr["No"] = Convert.ToString(drData["No"]);

                                    dr["BPM_Product_Name"] = Convert.ToString(drData["BPM_Product_Name"]);

                                    dr["FkBulkProductId"] = Convert.ToInt32(drData["FkBulkProductId"]);
                                    dr["PriceTypeId"] = Convert.ToInt32(drData["PriceTypeId"]);
                                    dr["Date_Last_Shared_Price"] = Convert.ToString(drData["Date_Last_Shared_Price"]);
                                    dr["EnumDescription"] = Convert.ToString(drData["EnumDescription"]);
                                    dr["PriceListGPActualEstimateId"] = Convert.ToInt32(drData["PriceListGPActualEstimateId"]);
                                    dr["RMPriceEstimateId"] = Convert.ToInt32(drData["RMPriceEstimateId"]);
                                    dr["TradeId"] = Convert.ToInt32(drData["TradeId"]);

                                    //dr[item.ColumnName] = Convert.ToDecimal(drData[item.ColumnName]);
                                    dr[item.ColumnName] = Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[0]);
                                    dr[item.ColumnName + "New"] = Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[1]);
                                    dtNew.Rows.Add(dr);
                                }
                                dtNew.AcceptChanges();
                            }
                        }
                        else
                        {
                            var EmptyData = dt.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId);
                            if (EmptyData.Any())
                            {
                                DataTable objdtItem = EmptyData.CopyToDataTable();

                                foreach (DataRow drnonState in objdtItem.Rows)
                                {
                                    var IsDataAvailable = dtNew.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId && row.Field<int>("PriceTypeId") == Convert.ToInt32(drnonState["PriceTypeId"]) && row.Field<string>("EnumDescription") == Convert.ToString(drnonState["EnumDescription"]));
                                    if (!IsDataAvailable.Any())
                                    {
                                        dr = dtNew.NewRow();
                                        //dr["No"] = Convert.ToString(drnonState["No"]);

                                        dr["BPM_Product_Name"] = Convert.ToString(drnonState["BPM_Product_Name"]);
                                        dr["FkBulkProductId"] = Convert.ToInt32(itemBPM.FkBulkProductId);
                                        dr["PriceTypeId"] = Convert.ToInt32(drnonState["PriceTypeId"]);
                                        dr["Date_Last_Shared_Price"] = Convert.ToString(drnonState["Date_Last_Shared_Price"]);
                                        dr["EnumDescription"] = Convert.ToString(drnonState["EnumDescription"]);
                                        dr["PriceListGPActualEstimateId"] = Convert.ToInt32(drnonState["PriceListGPActualEstimateId"]);
                                        dr["RMPriceEstimateId"] = Convert.ToInt32(drnonState["RMPriceEstimateId"]);
                                        dr["TradeId"] = Convert.ToInt32(drnonState["TradeId"]);


                                        dtNew.Rows.Add(dr);

                                    }
                                }

                            }
                        }
                    }
                }
            }

            dtNew.Columns.Add("Action", typeof(string));
            dtNew.AcceptChanges();
            ViewState["dtNew"] = dtNew;
            //rowID = 0;
            gvpricelistgpactual.DataSource = dtNew;

            gvpricelistgpactual.DataBind();

            if (!Directory.Exists(Server.MapPath("~/TempUse")))
            {
                Directory.CreateDirectory(Server.MapPath("~/TempUse"));
            }

            //if (File.Exists(Server.MapPath("~/TempUse/PriceList.pdf")))
            //{
            //    File.Delete(Server.MapPath("~/TempUse/PriceList.pdf"));
            //}

            using (var stream = new FileStream(Server.MapPath("~/TempUse/PriceList.pdf"), FileMode.Create))
            {
                Document document = new Document(PageSize.A2, 5, 5, 15, 5);
                PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
                document.Open();
                iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);
                iTextSharp.text.Font font10Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10, 1);
                iTextSharp.text.Font font14Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 14, 1);

                PdfPTable tableHeader = new PdfPTable(2);
                float[] Headerwidths = new float[] { 50f, 50f };
                tableHeader.SetWidths(Headerwidths);
                tableHeader.WidthPercentage = 100;

                PdfPCell cell = new PdfPCell(new Phrase("Price List All State :  " + DateTime.Now.ToShortDateString() , font14Bold));
                cell.HorizontalAlignment = 1;
                cell.Colspan = 2;
                cell.Border = 0;
                cell.PaddingBottom = 10;
                tableHeader.AddCell(cell);
                string[] ArrRemoveColumns = { "BPM_Product_Name", "Date_Last_Shared_Price", "Status", "PriceTypeId", "FkBulkProductId", "EnumDescription", "Action", "PriceListGPActualEstimateId", "RMPriceEstimateId", "TradeId" };


                cell = new PdfPCell(new Phrase("", font10Bold));
                cell.HorizontalAlignment = 1;
                cell.Border = 0;
                cell.PaddingBottom = 10;
                tableHeader.AddCell(cell);


                int iTableWidth = 3;
                float[] widths= new float[] {};
                foreach (DataColumn dtCol in dtNew.Columns)
                {
                    if (!dtCol.ColumnName.Contains("New") && !Array.Exists(ArrRemoveColumns, element => element == dtCol.ColumnName))
                    {
                        iTableWidth += 2;

                    }
                }
                PdfPTable table = new PdfPTable(iTableWidth);
             


                table.WidthPercentage = 100;

                cell = new PdfPCell(new Phrase("No", font10Bold));
                cell.Rowspan = 2;
                cell.HorizontalAlignment = 1;

                cell.VerticalAlignment = ((int)VerticalAlign.Middle);
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Product Name", font10Bold));
                cell.Rowspan = 2;
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                table.AddCell(cell);

                //cell = new PdfPCell(new Phrase("Last Shared Price Date", font10Bold));
                //cell.Rowspan = 2;
                //cell.HorizontalAlignment = 1;
                //cell.VerticalAlignment = 1;
                //table.AddCell(cell); 

                cell = new PdfPCell(new Phrase("RPL/NCR", font10Bold));
                cell.Rowspan = 2;
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;
                table.AddCell(cell);


                foreach (DataColumn dtCol in dtNew.Columns)
                {
                    if (!dtCol.ColumnName.Contains("New") && !Array.Exists(ArrRemoveColumns, element => element == dtCol.ColumnName))
                    {
                        cell = new PdfPCell(new Phrase(dtCol.ColumnName, font10Bold));
                        cell.Colspan = 2;
                        cell.HorizontalAlignment = 1;
                        cell.VerticalAlignment = 1;
                        table.AddCell(cell);
                    }
                }

                foreach (DataColumn dtCol in dtNew.Columns)
                {
                    if (!dtCol.ColumnName.Contains("New") && !Array.Exists(ArrRemoveColumns, element => element == dtCol.ColumnName))
                    {
                        table.AddCell(new Phrase("Current", font10Bold));
                        table.AddCell(new Phrase("New", font10Bold));
                    }
                }

                int iCounter = 1;
                int fk_BPM_Id = 0;
                foreach (DataRow dr in dtNew.Rows)
                {
                    int Currentfk_BPM_Id = Convert.ToInt32(dr["FkBulkProductId"].ToString());
                    if (fk_BPM_Id != Currentfk_BPM_Id)
                    {
                        fk_BPM_Id = Convert.ToInt32(dr["FkBulkProductId"].ToString());
                        int Count = dtNew.AsEnumerable().Where(r => r.Field<int>("FkBulkProductId") == Convert.ToInt32(dr["FkBulkProductId"].ToString())).Count();

                        cell = new PdfPCell(new Phrase(iCounter.ToString(), font5));
                        cell.Rowspan = Count;
                        cell.HorizontalAlignment = 1;
                        cell.VerticalAlignment = 1;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(dr["BPM_Product_Name"].ToString(), font5));
                        cell.Rowspan = Count;
                        cell.HorizontalAlignment = 1;
                        cell.VerticalAlignment = 1;
                        table.AddCell(cell);



                        iCounter++;
                    }

                    //table.AddCell(new Phrase(dr["Date_Last_Shared_Price"].ToString(), font5));

                    table.AddCell(new Phrase(dr["EnumDescription"].ToString(), font5));

                    foreach (DataColumn dtCol in dtNew.Columns)
                    {
                        if (!Array.Exists(ArrRemoveColumns, element => element == dtCol.ColumnName))
                        {
                            table.AddCell(new Phrase(dr[dtCol.ColumnName].ToString(), font5));
                        }
                    }
                }

                document.Add(tableHeader);
                //document.Add(tableFooter);
                document.Add(table);
                document.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Price List GP All State " + DateTime.Now.Date.ToShortDateString() + ".pdf");

                //Response.TransmitFile(Server.MapPath("~/TempUse/PriceList.pdf"));
                Response.End();



            }


        }


        public void Grid_Estimate_GP_StatewiseFinal()
        {
            int FkStateId = 0;
            if (drpstate.SelectedValue != "Select")
            {
                DataTable dt = new DataTable();
                FkStateId = Common.ConvertInt(drpstate.SelectedValue);
                dt = plgp.GetPriceListGPActualEstimateByStateWiseReport(FkStateId, 1);
                if (dt.Rows.Count > 0)
                {
                    lblStateName.Text = dt.Rows[0]["StateName"].ToString();
                    //GP_Estimate_StatewiseFinal.Visible = true;



                    if (!Directory.Exists(Server.MapPath("~/TempUse")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/TempUse"));
                    }

                    if (File.Exists(Server.MapPath("~/TempUse/PriceList.pdf")))
                    {
                        File.Delete(Server.MapPath("~/TempUse/PriceList.pdf"));
                    }
                    if (Common.ConvertInt(Session["CompanyId"]) == 1 || Common.ConvertInt(Session["CompanyId"]) == 0)
                    {
                        using (var stream = new FileStream(Server.MapPath("~/TempUse/PriceList.pdf"), FileMode.Create))
                        {
                            Document document = new Document(PageSize.A4, 5, 5, 15, 5);

                            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
                            document.Open();
                            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);
                            iTextSharp.text.Font font10Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10, 1);
                            iTextSharp.text.Font font14Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 14, 1);

                            PdfPTable tableHeader = new PdfPTable(2);
                            float[] Headerwidths = new float[] { 50f, 50f };
                            tableHeader.SetWidths(Headerwidths);
                            tableHeader.WidthPercentage = 100;
                            PdfPCell cell = new PdfPCell(new Phrase("Price List Report of " + "GP", font14Bold));
                            cell.HorizontalAlignment = 1;
                            cell.Colspan = 2;
                            cell.Border = 0;
                            cell.PaddingBottom = 10;
                            tableHeader.AddCell(cell);

                            //cell = new PdfPCell(new Phrase("Price list Name : " , font10Bold));
                            //cell.HorizontalAlignment = 3;
                            //cell.Border = 0;
                            //cell.PaddingBottom = 10;
                            //tableHeader.AddCell(cell);

                            cell = new PdfPCell(new Phrase("State : " + lblStateName.Text, font10Bold));
                            cell.HorizontalAlignment = 1;
                            cell.Border = 0;
                            cell.PaddingBottom = 10;
                            tableHeader.AddCell(cell);

                            cell = new PdfPCell(new Phrase("As on date	 : " + dt.Rows[0]["AsOnDate"], font10Bold));
                            cell.HorizontalAlignment = 3;
                            cell.Border = 0;
                            cell.PaddingBottom = 10;
                            tableHeader.AddCell(cell);

                            cell = new PdfPCell(new Phrase(" ", font10Bold));
                            cell.HorizontalAlignment = 1;
                            cell.Border = 0;
                            cell.PaddingBottom = 10;
                            tableHeader.AddCell(cell);

                            PdfPTable table = new PdfPTable(10);
                            float[] widths = new float[] { 4, 15, 15, 12, 8, 8, 8, 8, 8, 8 };

                            table.SetWidths(widths);

                            table.WidthPercentage = 100;
                            cell = new PdfPCell(new Phrase("No", font10Bold));
                            cell.Rowspan = 2;
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = ((int)VerticalAlign.Middle);
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Product Name ", font10Bold));
                            cell.Rowspan = 2;
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = 1;
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Technical Name", font10Bold));
                            cell.Rowspan = 2;
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = 1;
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Packing Size", font10Bold));
                            cell.Rowspan = 2;
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = 1;
                            table.AddCell(cell);


                            cell = new PdfPCell(new Phrase("NCR", font10Bold));
                            cell.Colspan = 3;
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = 1;
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase("RPL", font10Bold));
                            cell.HorizontalAlignment = 1;
                            cell.VerticalAlignment = 1;
                            cell.Colspan = 3;
                            table.AddCell(cell);

                            //cell = new PdfPCell(new Phrase("MRP", font10Bold));
                            //cell.Rowspan = 2;
                            //cell.HorizontalAlignment = 1;
                            //cell.VerticalAlignment = 1;
                            //table.AddCell(cell);

                            table.AddCell(new Phrase("Price/ L or KG", font10Bold));
                            table.AddCell(new Phrase("PD", font10Bold));
                            table.AddCell(new Phrase("QD", font10Bold));
                            table.AddCell(new Phrase("Price/ L or KG", font10Bold));
                            table.AddCell(new Phrase("PD", font10Bold));
                            table.AddCell(new Phrase("QD", font10Bold));

                            int iCounter = 1;
                            int FkBulkProductId = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                int CurrentFkBulkProductId = Convert.ToInt32(dr["FkBulkProductId"].ToString());
                                if (FkBulkProductId != CurrentFkBulkProductId)
                                {
                                    FkBulkProductId = Convert.ToInt32(dr["FkBulkProductId"].ToString());
                                    int Count = dt.AsEnumerable().Where(r => r.Field<int>("FkBulkProductId") == Convert.ToInt32(dr["FkBulkProductId"].ToString())).Count();

                                    cell = new PdfPCell(new Phrase(iCounter.ToString(), font5));
                                    cell.Rowspan = Count;
                                    cell.HorizontalAlignment = 1;
                                    cell.VerticalAlignment = 1;
                                    table.AddCell(cell);

                                    cell = new PdfPCell(new Phrase(dr["TradeName"].ToString(), font5));
                                    cell.Rowspan = Count;
                                    cell.HorizontalAlignment = 1;
                                    cell.VerticalAlignment = 1;
                                    table.AddCell(cell);

                                    cell = new PdfPCell(new Phrase(dr["BulkProductName"].ToString(), font5));
                                    cell.Rowspan = Count;
                                    cell.HorizontalAlignment = 1;
                                    cell.VerticalAlignment = 1;
                                    table.AddCell(cell);
                                    iCounter++;
                                }
                                table.AddCell(new Phrase(dr["PackMeasure"].ToString(), font5));

                                table.AddCell(new Phrase(dr["NCR"].ToString().Split('|')[0], font5));
                                table.AddCell(new Phrase(dr["NCR"].ToString().Split('|')[1], font5));
                                table.AddCell(new Phrase(dr["NCR"].ToString().Split('|')[2], font5));
                                table.AddCell(new Phrase(dr["RPL"].ToString().Split('|')[0], font5));
                                table.AddCell(new Phrase(dr["RPL"].ToString().Split('|')[1], font5));
                                table.AddCell(new Phrase(dr["RPL"].ToString().Split('|')[2], font5));

                                //table.AddCell(new Phrase(dr["MRP"].ToString(), font5));

                            }



                            //PdfPTable tableFooter = new PdfPTable(1);
                            //float[] Footerwidths = new float[] {5f};

                            //tableFooter.TotalWidth = document.PageSize.Width - document.LeftMargin;
                            //tableFooter.AddCell("Page");
                            //tableFooter.WriteSelectedRows(0, -1, document.LeftMargin + 50, document.PageSize.Height - 30, writer.DirectContent);
                            //cell.Border = 0;
                            //tableFooter.AddCell(cell);

                            document.Add(tableHeader);
                            //document.Add(tableFooter);
                            document.Add(table);
                            document.Close();

                        }
                    }
                    //else
                    //{
                    //    using (var stream = new FileStream(Server.MapPath("~/TempUse/PriceList.pdf"), FileMode.Create))
                    //    {
                    //        Document document = new Document(PageSize.A4, 5, 5, 15, 5);
                    //        PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
                    //        document.Open();
                    //        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    //        iTextSharp.text.Font font10Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10, 1);
                    //        iTextSharp.text.Font font14Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 14, 1);

                    //        PdfPTable tableHeader = new PdfPTable(2);
                    //        float[] Headerwidths = new float[] { 50f, 50f };
                    //        tableHeader.SetWidths(Headerwidths);
                    //        tableHeader.WidthPercentage = 100;
                    //        PdfPCell cell = new PdfPCell(new Phrase("Price List Report of " + dt.Rows[0]["CompanyName"], font14Bold));
                    //        cell.HorizontalAlignment = 1;
                    //        cell.Colspan = 2;
                    //        cell.Border = 0;
                    //        cell.PaddingBottom = 10;
                    //        tableHeader.AddCell(cell);

                    //        cell = new PdfPCell(new Phrase("Price list Name : " + dt.Rows[0]["PriceListName"], font10Bold));
                    //        cell.HorizontalAlignment = 3;
                    //        cell.Border = 0;
                    //        cell.PaddingBottom = 10;
                    //        tableHeader.AddCell(cell);

                    //        //cell = new PdfPCell(new Phrase("State : " + dt.Rows[0]["StateName"], font10Bold));
                    //        //cell.HorizontalAlignment = 2;
                    //        //cell.Border = 0;
                    //        //cell.PaddingBottom = 10;
                    //        //tableHeader.AddCell(cell);

                    //        cell = new PdfPCell(new Phrase("As on date	 : " + dt.Rows[0]["AsOnDate"], font10Bold));
                    //        cell.HorizontalAlignment = 3;
                    //        cell.Border = 0;
                    //        cell.PaddingBottom = 10;
                    //        tableHeader.AddCell(cell);

                    //        cell = new PdfPCell(new Phrase(" ", font10Bold));
                    //        cell.HorizontalAlignment = 1;
                    //        cell.Border = 0;
                    //        cell.PaddingBottom = 10;
                    //        tableHeader.AddCell(cell);

                    //        PdfPTable table = new PdfPTable(8);
                    //        float[] widths = new float[] { 4, 20, 12, 8, 8, 8, 8, 8 };

                    //        table.SetWidths(widths);

                    //        table.WidthPercentage = 100;
                    //        cell = new PdfPCell(new Phrase("No", font10Bold));

                    //        cell.HorizontalAlignment = 1;
                    //        cell.VerticalAlignment = ((int)VerticalAlign.Middle);
                    //        table.AddCell(cell);

                    //        cell = new PdfPCell(new Phrase("Product Name with Technical Name", font10Bold));
                    //        cell.HorizontalAlignment = 1;
                    //        cell.VerticalAlignment = 1;
                    //        table.AddCell(cell);

                    //        cell = new PdfPCell(new Phrase("Category Name", font10Bold));

                    //        cell.HorizontalAlignment = 1;
                    //        cell.VerticalAlignment = 1;
                    //        table.AddCell(cell);

                    //        cell = new PdfPCell(new Phrase("Packing Size", font10Bold));

                    //        cell.HorizontalAlignment = 1;
                    //        cell.VerticalAlignment = 1;
                    //        table.AddCell(cell);



                    //        table.AddCell(new Phrase("Price/ L or KG", font10Bold));
                    //        table.AddCell(new Phrase("Price Per Unit", font10Bold));
                    //        table.AddCell(new Phrase("Unit Price with GST", font10Bold));

                    //        cell = new PdfPCell(new Phrase("MRP", font10Bold));
                    //        cell.HorizontalAlignment = 1;
                    //        cell.VerticalAlignment = 1;
                    //        table.AddCell(cell);

                    //        int iCounter = 1;
                    //        int fk_BPM_Id = 0;
                    //        foreach (DataRow dr in dt.Rows)
                    //        {
                    //            int Currentfk_BPM_Id = Convert.ToInt32(dr["fk_BPM_Id"].ToString());
                    //            if (fk_BPM_Id != Currentfk_BPM_Id)
                    //            {
                    //                fk_BPM_Id = Convert.ToInt32(dr["fk_BPM_Id"].ToString());
                    //                int Count = dt.AsEnumerable().Where(r => r.Field<int>("fk_BPM_Id") == Convert.ToInt32(dr["fk_BPM_Id"].ToString())).Count();

                    //                cell = new PdfPCell(new Phrase(iCounter.ToString(), font5));
                    //                cell.Rowspan = Count;
                    //                cell.HorizontalAlignment = 1;
                    //                cell.VerticalAlignment = 1;
                    //                table.AddCell(cell);

                    //                cell = new PdfPCell(new Phrase(dr["BPM_Product_Name"].ToString(), font5));
                    //                cell.Rowspan = Count;
                    //                cell.HorizontalAlignment = 1;
                    //                cell.VerticalAlignment = 1;
                    //                table.AddCell(cell);

                    //                cell = new PdfPCell(new Phrase(dr["ProductCategoryName"].ToString(), font5));
                    //                cell.Rowspan = Count;
                    //                cell.HorizontalAlignment = 1;
                    //                cell.VerticalAlignment = 1;
                    //                table.AddCell(cell);
                    //                iCounter++;
                    //            }
                    //            table.AddCell(new Phrase(dr["PackMeasure"].ToString(), font5));

                    //            table.AddCell(new Phrase(dr["FinalPrice"].ToString().Split('|')[0], font5));
                    //            table.AddCell(new Phrase(dr["FinalPrice"].ToString().Split('|')[1], font5));
                    //            table.AddCell(new Phrase(dr["FinalPrice"].ToString().Split('|')[2], font5));

                    //            table.AddCell(new Phrase(dr["MRP"].ToString(), font5));

                    //        }



                    //        //PdfPTable tableFooter = new PdfPTable(1);
                    //        //float[] Footerwidths = new float[] {5f};

                    //        //tableFooter.TotalWidth = document.PageSize.Width - document.LeftMargin;
                    //        //tableFooter.AddCell("Page");
                    //        //tableFooter.WriteSelectedRows(0, -1, document.LeftMargin + 50, document.PageSize.Height - 30, writer.DirectContent);
                    //        //cell.Border = 0;
                    //        //tableFooter.AddCell(cell);

                    //        document.Add(tableHeader);
                    //        //document.Add(tableFooter);
                    //        document.Add(table);
                    //        document.Close();

                    //    }

                    //}


                    if (drpreportcat.SelectedValue == "1")
                    {

                        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "GP" + "_" + lblStateName.Text + "_Pricelist_Statewise_" + DateTime.Now.Date.ToShortDateString() + ".xls"));
                        Response.ContentType = "application/excel";

                    }
                    else
                    {
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "GP" + "_" + lblStateName.Text + "_Pricelist_Statewise_" + DateTime.Now.Date.ToShortDateString() + ".pdf"));

                        Response.TransmitFile(Server.MapPath("~/TempUse/PriceList.pdf"));
                    }

                    Response.End();
                }
            }




        }

        protected void gvstatewisereport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int rowIndex = gvstatewisereport.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = gvstatewisereport.Rows[rowIndex];
                GridViewRow gvPreviousRow = gvstatewisereport.Rows[rowIndex + 1];
                for (int cellCount = 1; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    if (gvRow.Cells[0].Text == gvPreviousRow.Cells[0].Text)
                    {
                        if (gvPreviousRow.Cells[0].RowSpan < 2)
                        {
                            gvRow.Cells[0].RowSpan = 2;
                            gvRow.Cells[1].RowSpan = 2;
                          


                        }
                        else
                        {
                            gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                            gvRow.Cells[1].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
      


                        }
                        gvPreviousRow.Cells[0].Visible = false;
                        gvPreviousRow.Cells[1].Visible = false;
       



                    }

                }

            }

        }
        private void ExportGridToExcelFinal(Control GridView)
        {
            if (drpstate.SelectedValue != "0")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "GP" + "_" + lblStateName.Text + "_Prricelist_StateWise_" + DateTime.Now.Date.ToString("dd/MMM/yyyy") + ".xls"));
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvstatewisereport.AllowPaging = false;

                gvstatewisereport.RenderControl(htw);

                string headerTable = @"<Table ><tr><td><h3 class='text-primary'>" + lblStateName.Text + "</h3></td><td ><h3 class='text-primary'>" + DateTime.Now.Date.ToString("dd/MMM/yyyy") + "</h3></td><td></td></tr></Table >" +
                    "<Table><tr><td></td><td></td><td ></td><td><td class='text-primary'><h4 >RPL</h4></td><td></td><td></td><td class='text-primary'><h4>NCR</h4></td></tr></Table>";
                Response.Write(headerTable);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select State!')", true);

            }
        }

        [Obsolete]
        protected void btnstatewisereport_Click(object sender, EventArgs e)
        {
            if (drpreportcat.SelectedValue == "1")
            {

                Grid_Estimate_GP_StatewiseFinalExcel();

                ExportGridToExcelFinal(gvstatewisereport);
            }


            if (drpreportcat.SelectedValue == "0")
            {
                //for pdf report

                Grid_Estimate_GP_StatewiseFinal();

                ExportGridToPDF();
            }

        }
        private void Grid_Estimate_GP_StatewiseFinalExcel()
        {
            int FkStateId = 0;
            if (drpstate.SelectedValue != "Select")
            {
                DataTable dt = new DataTable();
                FkStateId = Common.ConvertInt(drpstate.SelectedValue);
                dt = plgp.GetPriceListGPActualEstimateByStateWiseReport(FkStateId, 1);
                if (dt.Rows.Count>0)
                {
                    lblStateName.Text = dt.Rows[0]["StateName"].ToString();
                    DataTable dtNew = new DataTable();
                    dtNew.Clear();
                    //dtNew.Columns.Add("No", typeof(string));

                    dtNew.Columns.Add("BulkProductName", typeof(string));
                    dtNew.Columns.Add("FkBulkProductId", typeof(int));
                    dtNew.Columns.Add("TradeName", typeof(string));
                    dtNew.Columns.Add("PackMeasure", typeof(string));
                    dtNew.Columns.Add("EnumDescription", typeof(string));
                    dtNew.Columns.Add("StateName", typeof(string));
                    dtNew.Columns.Add("AsOnDate", typeof(string));
                    dtNew.Columns.Add("RPL", typeof(string));
                    dtNew.Columns.Add("NCR", typeof(string));

                    dtNew.AcceptChanges();
                    var distinctBPMValues = dt.AsEnumerable().Select(row => new { FkBulkProductId = row.Field<Int32>("FkBulkProductId"), }).Distinct();

                    foreach (var itemBPM in distinctBPMValues)
                    {
                        foreach (DataColumn item in dt.Columns)
                        {
                            if (!Array.Exists(ArrRemoveColumnsNew, element => element == item.ColumnName))
                            {
                                var Data = dt.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId && row.Field<string>(item.ColumnName) != null && !string.IsNullOrEmpty(row.Field<string>(item.ColumnName).ToString()));
                                DataRow dr;
                                if (Data.Any())
                                {
                                    DataTable objdtData = Data.CopyToDataTable();

                                    if (!dtNew.Columns.Contains(item.ColumnName))
                                    {
                                        dtNew.Columns.Add(item.ColumnName, typeof(decimal));
                                        dtNew.Columns.Add(item.ColumnName + "Price/L or KG", typeof(string));
                                        dtNew.Columns.Add(item.ColumnName + "PD", typeof(string));
                                        dtNew.Columns.Add(item.ColumnName + "QD", typeof(string));
                                        dtNew.AcceptChanges();
                                    }

                                    foreach (DataRow drData in objdtData.Rows)
                                    {
                                        var DataNCROrRPL = dtNew.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId && row.Field<string>("EnumDescription") == Convert.ToString(drData["EnumDescription"]) && row.Field<string>("TradeName") == Convert.ToString(drData["TradeName"]) && row.Field<string>("PackMeasure") == Convert.ToString(drData["PackMeasure"]));

                                        if (DataNCROrRPL.Any())
                                        {
                                            foreach (var row in DataNCROrRPL)
                                            {
                                                row.SetField(item.ColumnName + "Price/L or KG", Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[0]));
                                                row.SetField(item.ColumnName + "PD", Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[1]));
                                                row.SetField(item.ColumnName + "QD", Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[2]));
                                            }
                                        }
                                        else
                                        {
                                            dr = dtNew.NewRow();
                                            //dr["No"] = Convert.ToString(drData["No"]);

                                            dr["BulkProductName"] = Convert.ToString(drData["BulkProductName"]);
                                            dr["FkBulkProductId"] = Convert.ToInt32(drData["FkBulkProductId"]);
                                            dr["TradeName"] = Convert.ToString(drData["TradeName"]);
                                            dr["PackMeasure"] = Convert.ToString(drData["PackMeasure"]);
                                            dr["EnumDescription"] = Convert.ToString(drData["EnumDescription"]);
                                            dr["StateName"] = Convert.ToString(drData["StateName"]);
                                            dr["AsOnDate"] = Convert.ToString(drData["AsOnDate"]);
                                            dr["RPL"] = Convert.ToString(drData["RPL"]);
                                            dr["NCR"] = Convert.ToString(drData["NCR"]);

                                            //dr[item.ColumnName] = Convert.ToDecimal(drData[item.ColumnName]);
                                            dr[item.ColumnName + "Price/L or KG"] = Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[0]);
                                            dr[item.ColumnName + "PD"] = Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[1]);
                                            dr[item.ColumnName + "QD"] = Convert.ToDecimal(drData[item.ColumnName].ToString().Split('|')[2]);
                                            dtNew.Rows.Add(dr);
                                        }
                                        dtNew.AcceptChanges();
                                    }
                                }
                                else
                                {
                                    var EmptyData = dt.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId);
                                    if (EmptyData.Any())
                                    {
                                        DataTable objdtItem = EmptyData.CopyToDataTable();

                                        foreach (DataRow drnonState in objdtItem.Rows)
                                        {
                                            var IsDataAvailable = dtNew.AsEnumerable().Where(row => row.Field<Int32>("FkBulkProductId") == itemBPM.FkBulkProductId && row.Field<string>("EnumDescription") == Convert.ToString(drnonState["EnumDescription"]) && row.Field<string>("TradeName") == Convert.ToString(drnonState["TradeName"]) && row.Field<string>("PackMeasure") == Convert.ToString(drnonState["PackMeasure"]));
                                            if (!IsDataAvailable.Any())
                                            {
                                                dr = dtNew.NewRow();
                                                //dr["No"] = Convert.ToString(drnonState["No"]);

                                                dr["BulkProductName"] = Convert.ToString(drnonState["BulkProductName"]);
                                                dr["FkBulkProductId"] = Convert.ToInt32(drnonState["FkBulkProductId"]);
                                                dr["TradeName"] = Convert.ToString(drnonState["TradeName"]);
                                                dr["PackMeasure"] = Convert.ToString(drnonState["PackMeasure"]);
                                                dr["EnumDescription"] = Convert.ToString(drnonState["EnumDescription"]);
                                                dr["StateName"] = Convert.ToString(drnonState["StateName"]);
                                                dr["AsOnDate"] = Convert.ToString(drnonState["AsOnDate"]);
                                                dr["RPL"] = Convert.ToDecimal(drnonState["RPL"]);
                                                dr["NCR"] = Convert.ToDecimal(drnonState["NCR"]);


                                                dtNew.Rows.Add(dr);

                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }

                    //dtNew.Columns.Add("Action", typeof(string));
                    dtNew.AcceptChanges();
                    ViewState["dtNewReport"] = dtNew;
                    //rowID = 0;
                    gvstatewisereport.DataSource = dtNew;

                    gvstatewisereport.DataBind();
                    gvstatewisereport.Visible = true;
                }
                
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }


        [Obsolete]
        private void ExportGridToPDF()
        {
            if (drpstate.SelectedValue != "Select")
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {

                        gvstatewisereport.AllowPaging = false;

                        gvstatewisereport.RenderControl(hw);

                    }


                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    try
                    {
                        pdfDoc.Close();

                    }
                    catch (Exception)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('State not Found!')", true);
                        return;

                    }

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename= gp" + "_" + lblStateName.Text + "_pricelist_" + DateTime.Now.Date.ToShortDateString() + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select State!')", true);

            }

        }

        protected void btnsaveCreate_Click(object sender, EventArgs e)
        {
            string msgs = "";
            foreach (GridViewRow gvr in gvpricelistgpactual.Rows)
            {
                GridViewRow sdf = gvr;
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)(gvr.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Controls[1]);

                string str = chk.Checked.ToString();
                if (chk.Checked == true)
                {
                    CheckCount = CheckCount + 1;
                    string FkBulkProductId = gvr.Cells[1].Text;
                    string strBPMName = gvr.Cells[0].Text;
                    //TradeName_Id = gvr.Cells[8].Text;
                    BPMName = strBPMName;
                    plgpdata.UserId = Common.ConvertInt(Session["UserId"]);
                    plgpdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);

                    ReturnMessage objs = common.CheckExist("CreatePriceList", FkBulkProductId, "", "");

                    if (Common.ConvertInt(objs.ReturnValue) == 1)
                    {
                        plgpdata.action = 1;
                    }
                    else
                    {
                        plgpdata.action = 2;
                    }



                    plgpdata.PriceListName = txtcrtpricelist.Text;
                    DataTable dtCheck = new DataTable();


                    ReturnMessage obj = null;
                    obj = plgp.InsertUpdate_CreatePriceList(FkBulkProductId, plgpdata);

                    string msg = msgs = Common.ConvertString(obj.Message);

                }

            }
            if (msgs != "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "');", true);

            }

        }

        protected void txtcrtpricelist_TextChanged(object sender, EventArgs e)
        {
            if (txtcrtpricelist.Text != "")
            {
                btnsaveCreate.Enabled = true;
            }
            else
            {
                btnsaveCreate.Enabled = false;

            }
        }

        protected void gvpricelistgpactualExcel_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Visible = false;
                DataTable dtNewActualExcel = ViewState["dtNewExcel"] as DataTable;
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Insert);
                GridViewRow HeaderGridRowCurrentNew = new GridViewRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Insert);
                foreach (DataColumn item in dtNewActualExcel.Columns)
                {
                    if (!item.ColumnName.Contains("New") && !Array.Exists(ArrRemoveColumns, element => element == item.ColumnName))
                    {
                        TableCell HeaderCell = new TableCell();
                        HeaderCell.Text = item.ColumnName;
                        HeaderCell.ColumnSpan = 2;
                        HeaderCell.CssClass = "customCellMergeTH";
                        HeaderGridRow.Cells.Add(HeaderCell);

                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Current";
                        HeaderCell.CssClass = "customCellMergeTH";
                        HeaderGridRowCurrentNew.Cells.Add(HeaderCell);

                        HeaderCell = new TableCell();
                        HeaderCell.Text = "New";
                        HeaderCell.CssClass = "customCellMergeTH";
                        HeaderGridRowCurrentNew.Cells.Add(HeaderCell);
                        TableCell Cell = new TableCell();
                    }
                    else if (!item.ColumnName.Contains("New"))
                    {
                        if (item.ColumnName != "FkBulkProductId" && item.ColumnName != "PriceTypeId" && item.ColumnName != "PriceListGPActualEstimateId" && item.ColumnName != "RMPriceEstimateId" && item.ColumnName != "TradeId" && item.ColumnName != "Date_Last_Shared_Price")
                        {
                            TableCell HeaderCell = new TableCell();

                            if (item.ColumnName == "Date_Last_Shared_Price")
                            {
                                HeaderCell.Text = "Last Date Shared Price";
                            }
                            else if (item.ColumnName == "EnumDescription")
                            {
                                HeaderCell.Text = "RPL / NCR";
                            }
                            else if (item.ColumnName == "BPM_Product_Name")
                            {
                                HeaderCell.Text = "BPM Product Name";
                            }
                            else
                            {
                                HeaderCell.Text = item.ColumnName;
                            }

                            HeaderCell.CssClass = "customCellMergeTH";
                            HeaderGridRow.Cells.Add(HeaderCell);

                            HeaderCell = new TableCell();
                            HeaderCell.Text = " ";
                            HeaderCell.CssClass = "customCellMergeTH";
                            HeaderGridRowCurrentNew.Cells.Add(HeaderCell);
                        }
                    }

                }
                gvpricelistgpactualExcel.Controls[0].Controls.AddAt(0, HeaderGridRow);
                gvpricelistgpactualExcel.Controls[0].Controls.AddAt(1, HeaderGridRowCurrentNew);

            }

        }

        protected void gvpricelistgpactualExcel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;

            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
    


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                int Cellcount = e.Row.Cells.Count;
              
                for (int colIndex = 0; colIndex < e.Row.Cells.Count; colIndex++)
                {

                 

                        Label label = new Label();
                        var r = colIndex >= 1 ? colIndex : colIndex;
                        label.Text = dr[r].ToString();

                        e.Row.Cells[colIndex].Controls.Add(label);
                    
                }

            }
        }
    }
}
