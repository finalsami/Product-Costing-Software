using BAL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Label = System.Web.UI.WebControls.Label;

namespace Production_Costing_Software
{
    public partial class OtherCompanyPriceList : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        OtherCompanyPriceListDAL companyPrice = new OtherCompanyPriceListDAL();
        OtherCompanyPriceListBAL companyPriceData = new OtherCompanyPriceListBAL();
        FinishGoodsPricingReportDAL FG = new FinishGoodsPricingReportDAL();
        RMEstimateDetailDAL RMED = new RMEstimateDetailDAL();


        public string datajs = "";

        int tempbpm = 0;
        int tid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }
        private void binddata()
        {
            datajs = "";
            DataTable dt = companyPrice.GetOtherCompanyPriceList(Common.ConvertInt(Session["CompanyId"]), Common.ConvertInt(Session["UserId"]));
            if (dt.Rows.Count > 0)
            {
                gvothercompany.DataSource = dt;
                gvothercompany.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", datajs, true);


        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.ClientID != null)
            {
                int rowno = Common.ConvertInt(btn.ClientID.Substring(btn.ClientID.LastIndexOf('_') + 1));
                int OtherComapnyPriceListId = Common.ConvertInt(btn.CommandArgument);
                GridViewRow row = gvothercompany.Rows[rowno];

                if (row != null)
                {
                    TextBox txtbulkinterest = (TextBox)row.FindControl("txtbulkinterest");
                    TextBox txtaddpm = (TextBox)row.FindControl("txtaddpm");
                    TextBox txtaddlabour = (TextBox)row.FindControl("txtaddlabour");
                    TextBox txtlossper = (TextBox)row.FindControl("txtlossper");
                    TextBox txtmarketper = (TextBox)row.FindControl("txtmarketper");
                    TextBox txtfactexpper = (TextBox)row.FindControl("txtfactexpper");
                    TextBox txtotherper = (TextBox)row.FindControl("txtotherper");
                    TextBox txtprofitper = (TextBox)row.FindControl("txtprofitper");
                    TextBox txtfinal = (TextBox)row.FindControl("txtfinal");

                    companyPriceData.OtherComapnyPriceListId = OtherComapnyPriceListId;
                    companyPriceData.Interest = Common.ConvertDecimal(txtbulkinterest.Text);
                    companyPriceData.AdditionalBufferPM = Common.ConvertDecimal(txtaddpm.Text);
                    companyPriceData.AdditionalBufferLabour = Common.ConvertDecimal(txtaddlabour.Text);
                    companyPriceData.LossPercentage = Common.ConvertDecimal(txtlossper.Text);
                    companyPriceData.MarketedChargePercentage = Common.ConvertDecimal(txtmarketper.Text);
                    companyPriceData.FactoryExpensePercentage = Common.ConvertDecimal(txtfactexpper.Text);
                    companyPriceData.OtherPercentage = Common.ConvertDecimal(txtotherper.Text);
                    companyPriceData.ProfitPercentage = Common.ConvertDecimal(txtprofitper.Text);
                    companyPriceData.FinalPriceUnit = Common.ConvertDecimal(txtfinal.Text);
                    companyPriceData.UserId = Common.ConvertInt(Session["UserId"]);
                    companyPriceData.action = 2;

                    ReturnMessage obj = companyPrice.UpdateOtheCompanyPricelist(companyPriceData);

                    if (Common.ConvertInt(obj.ReturnValue) > 0)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + obj.Message + "')", true);

                        //ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", "alert('Error Message');", true);

                        // ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "alertScript", "alert('Error Message');", true);

                        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "$(function () { alert('" + obj.Message + "');});", true);

                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alertMessage", "<script type='text/javascript'> alert('" + obj.Message + "'); </script>", true);

                        //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "alert", "alert('" + obj.Message + "');", true);

                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + obj.Message + "');", true);
                        //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "alert", "alert('" + obj.Message + "');", true);

                        binddata();
                    }
                }

            }
        }

        protected void gvothercompany_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtbulkinterest = (TextBox)e.Row.FindControl("txtbulkinterest");

                Button btnReport = (Button)e.Row.FindControl("btnReport");
                Button btnReportEstimate = (Button)e.Row.FindControl("btnReportEstimate");

                DataRowView dr = (DataRowView)e.Row.DataItem;

                Label lbothercompanyId = (Label)e.Row.FindControl("lbothercompanyId");

                if (e.Row.RowIndex > 0)
                {
                    Label id = e.Row.FindControl("lblid") as Label;

                    if (Convert.ToInt32(lbothercompanyId.Text) != tempbpm)
                    {
                        tid = tid + 1;
                        id.Text = tid.ToString();
                        tempbpm = Convert.ToInt32(lbothercompanyId.Text);
                    }

                }


                if (e.Row.RowIndex >= 1)
                {
                    int index = e.Row.RowIndex - 1;
                    GridViewRow drold = gvothercompany.Rows[index];
                    HiddenField hdntempbkid = (HiddenField)drold.FindControl("hdntempbkid");

                    if (dr.Row["fkbulkproductid"].ToString() == hdntempbkid.Value)
                    {
                        btnReport.Style.Add("display", "none");
                    }

                }
                if (dr.Row["type"].ToString() == "Actual")
                {
                    btnReportEstimate.Style.Add("display", "none");

                    datajs += "calculatePricelist('" + txtbulkinterest.ClientID + "');";
                }

            }
        }
        private string changedisplayname(string name)
        {
            string ret = "";
            string[] pack = name.Split('_');
            if (pack.Length == 2)
            {
                ret = Common.ConvertString(pack[1]) + "-" + Common.ConvertString(pack[0]);
            }
            else
            {
                ret = Common.ConvertString(pack[1]) + "-" + Common.ConvertString(pack[0]) + "(";
                for (int i = 2; i < pack.Length; i++)
                {
                    ret += pack[i] + " ";
                }
                ret += ")";
            }

            return ret;
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int ProductId = Common.ConvertInt(btn.CommandArgument);
            string html = "";
            string bulk = "";
            if (ProductId > 0)
            {
                Common.FinishedGoodReportDetailActual(Common.ConvertInt(Session["UserId"]), ProductId, out html, out bulk);




                exampleModalLabel.InnerHtml = bulk;

                dvdetailcontent.InnerHtml = html;

                binddata();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "showhidemodel('1');", true);
            }
        }

        protected void gvothercompany_DataBound(object sender, EventArgs e)
        {
            int t = 0;
            for (int i = 1; i < gvothercompany.Rows.Count; i++)
            {

                GridViewRow row = gvothercompany.Rows[i - 1];
                GridViewRow previousRow = gvothercompany.Rows[i];

                Label statusPrevious = previousRow.FindControl("lbothercompanyId") as Label;
                Label statuscurrent = row.FindControl("lbothercompanyId") as Label;

                Label id = row.FindControl("lblid") as Label;

                if (statusPrevious.Text != statuscurrent.Text)
                {
                    // t++;
                    //id.Text = t.ToString();

                }
                if (statusPrevious.Text == statuscurrent.Text)
                {

                    if (row.Cells[0].RowSpan == 0)
                    {
                        if (row.Cells[0].RowSpan == 0)
                        {
                            row.Cells[0].RowSpan += 2;
                            //row.Cells[1].RowSpan += 2;
                            row.Cells[2].RowSpan += 2;
                            row.Cells[3].RowSpan += 2;
                            row.Cells[4].RowSpan += 2;
                            row.Cells[20].RowSpan += 2;
                            row.Cells[24].RowSpan += 2;

                            //t++;
                            //id.Text = t.ToString();

                        }
                        else
                        {
                            row.Cells[0].RowSpan = row.Cells[0].RowSpan + 1;
                            //row.Cells[1].RowSpan = row.Cells[1].RowSpan + 1;
                            row.Cells[2].RowSpan = row.Cells[2].RowSpan + 1;
                            row.Cells[3].RowSpan = row.Cells[3].RowSpan + 1;
                            row.Cells[4].RowSpan = row.Cells[4].RowSpan + 1;
                            row.Cells[20].RowSpan = row.Cells[20].RowSpan + 1;
                            row.Cells[24].RowSpan = row.Cells[20].RowSpan + 1;
                        }
                        previousRow.Cells[0].Visible = false;
                        //previousRow.Cells[1].Visible = false;
                        previousRow.Cells[2].Visible = false;
                        previousRow.Cells[3].Visible = false;
                        previousRow.Cells[4].Visible = false;
                        previousRow.Cells[20].Visible = false;
                        previousRow.Cells[24].Visible = false;


                    }

                }

            }
        }

        protected void btnReportEstimate_Click(object sender, EventArgs e)
        {


            Button btn = (Button)sender;
            int ProductId = Common.ConvertInt(btn.CommandArgument);
            if (ProductId > 0)
            {

                string html = "";
                string bulk = "";

                Common.GetFinishedGoodEstimate(Common.ConvertInt(Session["UserId"]), ProductId, out html, out bulk);

                exampleModalLabel.InnerHtml = bulk;

                dvdetailcontent.InnerHtml = html;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "showhidemodel('1');", true);
                //if (ProductId > 0)
                //{
                //    DataSet ds = RMED.GetRMEstimateDetailReport(Convert.ToInt32(ProductId), 1);

                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        exampleModalLabel.InnerHtml = Common.ConvertString(ds.Tables[0].Rows[0]["BulkproductName"]);


                //        //Ingredient detail
                //        sbdata.Append("<table class='table table-bordered table-striped gridview dataTable no-footer dtr-inline'><tbody>"
                //        + "<tr>"
                //            + "<th scope='col'>No</th>"
                //            + "<th scope='col'>Ingredient</th>"
                //            + "<th scope='col'>Formulation(%)</th>"
                //            + "<th scope='col'>QTY</th>"
                //            + "<th scope='col'>Rate/KG</th>"
                //            + "<th scope='col'>Transportation</th>"
                //            + "<th scope='col'>Amount</th>"
                //        + "</tr>");
                //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //        {
                //            int t = i + 1;
                //            sbdata.Append("<tr>"
                //                + "<td>" + t + "</td>"
                //                + "<td style='text-align:left!important'>" + Common.ConvertString(ds.Tables[0].Rows[i]["IngredientName"]) + "</td>"
                //                + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["Formulation"]) + "</td>"
                //                + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["QuantityLtrKgDisplay"]) + "</td>"
                //                + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["RmPrice"]) + "</td>"
                //                + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["TransporationRate"]) + "</td>"
                //                + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["total"]) + "</td>"
                //            + "</tr>");
                //        }

                //        sbdata.Append("<tr>"
                //               + "<th></th>"
                //               + "<th>Total</th>"
                //               + "<th>" + Common.ConvertString(ds.Tables[0].Compute("sum(Formulation)", "")) + " </th>"
                //               + "<th>" + Common.ConvertString(ds.Tables[0].Compute("sum(QuantityLtrKgDisplay)", "")) + "</th>"
                //               + "<th>" + Common.ConvertString(ds.Tables[0].Compute("sum(RmPrice)", "")) + "</th>"
                //               + "<th>" + Common.ConvertString(ds.Tables[0].Compute("sum(TransporationRate)", "")) + "</th>"
                //               + "<th>" + Common.ConvertString(ds.Tables[0].Compute("sum(total)", "")) + "</th>"
                //        + "</tr>");

                //        sbdata.Append("</tbody></table>");

                //        decimal cost = Common.ConvertDecimal(ds.Tables[0].Rows[0]["finalbulkcost"]);
                //        decimal interest = Common.ConvertDecimal(ds.Tables[0].Rows[0]["Interest"]);
                //        decimal rate = cost * interest / 100;

                //        //spgr and bulk detail

                //        sbdata.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'>");
                //        sbdata.Append("<tr><th scope='col' style='text-align:left!important' class='col-md-8'>Formulation (" + Common.ConvertString(ds.Tables[0].Rows[0]["formulationName"]) + "), Batch Size (" + Common.ConvertString(ds.Tables[0].Rows[0]["BatchSize"]) + ")</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertString(ds.Tables[0].Rows[0]["FormulationCost"]) + "</th></tr>");
                //        sbdata.Append("<tr><th scope='col' style='text-align:left!important' class='col-md-8'>SPGR</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertString(ds.Tables[0].Rows[0]["spgr"]) + "</th></tr>");
                //        sbdata.Append("<tr><th scope='col' style='text-align:left!important' class='col-md-8'>Final OutPut</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertString(ds.Tables[0].Rows[0]["finalout"]) + "</th></tr>");
                //        sbdata.Append("<tr><th scope='col' style='text-align:left!important' class='col-md-8'>Final Bulk Cost/Ltr or Kg</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertString(ds.Tables[0].Rows[0]["finalbulkcost"]) + "</th></tr>");
                //        sbdata.Append("<tr><th scope='col' style='text-align:left!important' class='col-md-8'>Interest</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertDecimal(rate).ToString() + "(" + Common.ConvertString(ds.Tables[0].Rows[0]["Interest"]) + "%)</th></tr>");
                //        sbdata.Append("<tr><th scope='col' style='text-align:left!important'  class='col-md-8'>Final Cost/Ltr or Kg</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertString(ds.Tables[0].Rows[0]["final"]) + "</th></tr>");
                //        sbdata.Append("</thead></tbody></table>");

                //        //PM costing                    

                //        if (ds.Tables[1].Rows.Count > 0)
                //        {
                //            sbdata.Append("<h5 class='text-primary'>PM Costing/Unit</h5><table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                //            for (int col = 0; col < ds.Tables[1].Columns.Count; col++)
                //            {
                //                if (col == 0)
                //                {
                //                    sbdata.Append("<th scope='col' >" + Common.ConvertString(ds.Tables[1].Columns[col].ColumnName.Replace("_", " ")) + "</th>");
                //                }
                //                else
                //                {
                //                    string[] pack = Common.ConvertString(ds.Tables[1].Columns[col].ColumnName).Split('_');
                //                    if (pack.Length == 2)
                //                    {
                //                        sbdata.Append("<th scope='col'>" + Common.ConvertString(pack[1]) + "-" + Common.ConvertString(pack[0]) + "</th>");
                //                    }
                //                    else
                //                    {
                //                        sbdata.Append("<th scope='col'>" + changedisplayname(ds.Tables[1].Columns[col].ColumnName) + "</th>");
                //                    }

                //                }
                //            }
                //            sbdata.Append("</tr></thead>");

                //            for (int r = 0; r < ds.Tables[1].Rows.Count; r++)
                //            {
                //                sbdata.Append("<tr>");
                //                for (int col = 0; col < ds.Tables[1].Columns.Count; col++)
                //                {
                //                    sbdata.Append("<td>");
                //                    sbdata.Append(Common.ConvertString(ds.Tables[1].Rows[r][col]) == "" ? "-" : Common.ConvertString(ds.Tables[1].Rows[r][col]));
                //                    sbdata.Append("</td>");
                //                }
                //                sbdata.Append("</tr>");

                //                if (r == ds.Tables[1].Rows.Count - 1)
                //                {
                //                    sbdata.Append("<thead class='thead-light'><tr>");
                //                    for (int col = 0; col < ds.Tables[1].Columns.Count; col++)
                //                    {
                //                        if (col == 0)
                //                        {
                //                            sbdata.Append("<th>");
                //                            sbdata.Append("Total");
                //                            sbdata.Append("</th>");
                //                        }
                //                        else
                //                        {

                //                            sbdata.Append("<th>");
                //                            sbdata.Append(Common.ConvertString(ds.Tables[1].Compute("sum(" + ds.Tables[1].Columns[col].ColumnName + ")", "")));
                //                            sbdata.Append("</th>");
                //                        }
                //                    }
                //                    sbdata.Append("</thead></tr>");
                //                }
                //            }

                //            sbdata.Append("</tbody></table>");
                //        }

                //        //Other Costing

                //        if (ds.Tables[2].Rows.Count > 0)
                //        {

                //            string masterpackname = "";

                //            DataRow[] dr = ds.Tables[2].Select("isMasterpacking=True");
                //            if (dr.Length > 0)
                //            {
                //                masterpackname = changedisplayname(dr[0]["packingdetaildisplay"].ToString());
                //                exampleModalLabel.InnerHtml += " [ <span class='text-primary'>" + masterpackname + " (Estimate)"+"</span> ]";
                //            }


                //            StringBuilder sblabour = new StringBuilder();
                //            StringBuilder sbloss = new StringBuilder();
                //            StringBuilder sbtotal = new StringBuilder();

                //            StringBuilder sbPerLtrUnit = new StringBuilder();
                //            StringBuilder sbPerLtr = new StringBuilder();
                //            StringBuilder sbPerUnit = new StringBuilder();

                //            StringBuilder sblossfinal = new StringBuilder();

                //            StringBuilder sblossfinal_row = new StringBuilder();
                //            StringBuilder sblossfinal_expense = new StringBuilder();

                //            StringBuilder sbFactoryExpense = new StringBuilder();
                //            StringBuilder sbMarkettedByCharges = new StringBuilder();
                //            StringBuilder sbOther = new StringBuilder();
                //            StringBuilder sbTotalExpence = new StringBuilder();
                //            StringBuilder sbProfit = new StringBuilder();

                //            int UnitMeasure = 0;
                //            decimal PackingSize = 0;

                //            sbdata.Append("<h5 class='text-primary'>Other Costing/Unit</h5><table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                //            sbPerLtrUnit.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                //            sblossfinal.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");
                //            sblossfinal_expense.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                //            sblabour.Append("<tr>");
                //            sbloss.Append("<tr>");
                //            sbtotal.Append("<thead class='thead-light'><tr>");

                //            sbdata.Append("<th>");
                //            sbdata.Append("Packing Size");
                //            sbdata.Append("</th>");

                //            sbPerLtrUnit.Append("<th>Packing Size</th>");
                //            sblossfinal.Append("<th>Packing Size</th>");
                //            sblossfinal_expense.Append("<th>Expense</th>");

                //            sblossfinal_row.Append("<tr>");

                //            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                //            {
                //                sbdata.Append("<th>");
                //                sbdata.Append(changedisplayname(Common.ConvertString(ds.Tables[2].Rows[k]["packingdetaildisplay"])));
                //                sbdata.Append("</th>");

                //                sbPerLtrUnit.Append("<th>");
                //                sbPerLtrUnit.Append(changedisplayname(Common.ConvertString(ds.Tables[2].Rows[k]["packingdetaildisplay"])));
                //                sbPerLtrUnit.Append("</th>");

                //                sblossfinal.Append("<th>");
                //                sblossfinal.Append(changedisplayname(Common.ConvertString(ds.Tables[2].Rows[k]["packingdetaildisplay"])));
                //                sblossfinal.Append("</th>");

                //                sblossfinal_expense.Append("<th>");
                //                sblossfinal_expense.Append(changedisplayname(Common.ConvertString(ds.Tables[2].Rows[k]["packingdetaildisplay"])));
                //                sblossfinal_expense.Append("</th>");

                //                if (k == 0)
                //                {
                //                    sblabour.Append("<td>Labour</td>");
                //                    sbloss.Append("<td>Packing Loss</td>");
                //                    sbtotal.Append("<th>Total</th>");
                //                    sbPerLtr.Append("<td>Per Ltr Costing</td>");
                //                    sbPerUnit.Append("<td>Per Unit Costing</td>");
                //                    sblossfinal_row.Append("<td>Difference</td>");
                //                    sbFactoryExpense.Append("<td>Factory Expense</td>");
                //                    sbMarkettedByCharges.Append("<td>Marketted By Charges</td>");
                //                    sbOther.Append("<td>Other</td>");
                //                    sbTotalExpence.Append("<td>Total Expence</td>");
                //                    sbProfit.Append("<td>Profit</td>");
                //                }

                //                sblabour.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["labourcost"]) + "</td>");
                //                sbloss.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["packingloss"]) + "</td>");

                //                sbPerLtr.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["final"]) + "</td>");
                //                UnitMeasure = Common.ConvertInt(ds.Tables[2].Rows[k]["packingMeasurementId"]);
                //                PackingSize = Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingsize"]);
                //                decimal unit = 0;
                //                if (PackingSize > 1 && UnitMeasure == 1 || UnitMeasure == 2)
                //                {

                //                    unit = Common.ConvertDecimal(ds.Tables[2].Rows[k]["final"]) * PackingSize;

                //                }
                //                else if (PackingSize < 1000 && UnitMeasure == 6 || UnitMeasure == 7)
                //                {
                //                    unit = Common.ConvertDecimal(ds.Tables[2].Rows[k]["final"]) / (1000 / PackingSize);

                //                }
                //                else if (PackingSize == 1 && UnitMeasure == 1 || UnitMeasure == 2)
                //                {

                //                    unit = Common.ConvertDecimal(ds.Tables[2].Rows[k]["final"]) / Common.ConvertDecimal(ds.Tables[2].Rows[k]["CostPerLtrFector"]);

                //                }
                //                sbPerUnit.Append("<td>" + Common.ConvertDecimal(unit) + "</td>");

                //                Decimal total = Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]) + Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]);

                //                sbtotal.Append("<th>" + Common.ConvertDecimal(total.ToString()) + "</th>");

                //                sblossfinal_row.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["diff"]) + "</td>");

                //                sbFactoryExpense.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["FactoryExpense"] + "(" + ds.Tables[3].Rows[k]["FactoryExpensePercentage"] + " %)") + "</td>");
                //                sbMarkettedByCharges.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["MarketedCharge"] + "(" + ds.Tables[3].Rows[k]["MarketedChargePercentage"] + " %)") + "</td>");
                //                sbOther.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["Other"] + "(" + ds.Tables[3].Rows[k]["OtherPercentage"] + " %)") + "</td>");
                //                sbTotalExpence.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["TotalExpense"]) + "</td>");
                //                sbProfit.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["Profit"] + "(" + ds.Tables[3].Rows[k]["ProfitPercentage"] + " %)") + "</td>");

                //            }

                //            sblabour.Append("</tr>");
                //            sbloss.Append("</tr>");
                //            sbtotal.Append("</thead></tr>");
                //            sbdata.Append("</thead></tr>");

                //            sbPerLtr.Append("</tr>");
                //            sbPerUnit.Append("</tr>");

                //            sbPerLtrUnit.Append("</thead></tr><h5 class='text-primary'>Final Cost Packing Wise ( BULK + PM + Other )</h5>");
                //            sbPerLtrUnit.Append(sbPerUnit);
                //            sbPerLtrUnit.Append(sbPerLtr);

                //            sbPerLtrUnit.Append("</tbody></table><h5 class='text-primary'>Packing Difference from Master Pack [" + masterpackname + "]</h5>");

                //            sbdata.Append(sblabour);
                //            sbdata.Append(sbloss);
                //            sbdata.Append(sbtotal);

                //            sbdata.Append(sbPerLtrUnit);

                //            sblossfinal.Append("</tr>");


                //            sblossfinal.Append(sblossfinal_row);

                //            sblossfinal.Append("</tbody></table>");
                //            sbdata.Append(sblossfinal);


                //            sbdata.Append("</tbody></table><h5 class='text-primary'>Gujarat Pesticide Factory Expence</h5>");
                //            sbdata.Append("</tr>");

                //            sbdata.Append(sblossfinal_expense);
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</thead></tr>");
                //            sbdata.Append("</thead></tr>");
                //            sbdata.Append(sbFactoryExpense);
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</thead></tr>");
                //            sbdata.Append("</thead></tr>");
                //            sbdata.Append(sbMarkettedByCharges);
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</thead></tr>");
                //            sbdata.Append("</thead></tr>");
                //            sbdata.Append(sbOther);
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</thead></tr>");
                //            sbdata.Append("</thead></tr>");
                //            sbdata.Append(sbTotalExpence);
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</thead></tr>");
                //            sbdata.Append("</thead></tr>");
                //            sbdata.Append(sbProfit);
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</tr>");
                //            sbdata.Append("</thead></tr>");
                //            sbdata.Append("</thead></tr>");
                //            //sbdata.Append(sblossfinal_expense);


                //        }
                //    }
                //    dvdetailcontent.InnerHtml = sbdata.ToString();
                //    binddata();
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "showhidemodel('1');", true);


                //}
            }
        }
    }
}