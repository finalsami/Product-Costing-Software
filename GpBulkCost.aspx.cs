using BAL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class GpBulkCost : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        BulkCostDAL bc = new BulkCostDAL();
        PMRMMasterDAL pmrm = new PMRMMasterDAL();
        BulkCostBAL bcdata = new BulkCostBAL();

        public string datajs = "";

        int tempbpm = 0;
        int tid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddropdown();

                binddata();
            }
        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("PMRMNameByPMRMCategoryBulkCost", "", "");

            //drppmrm.DataSource = dtrm;
            //drppmrm.DataTextField = "Name";
            //drppmrm.DataValueField = "Id";
            //drppmrm.DataBind();


            //DataTable dtunit = common.DropdownList("PMRMMaster", "", "");

            //drppmrmname.DataSource = dtunit;
            //drppmrmname.DataTextField = "Name";
            //drppmrmname.DataValueField = "Id";
            //drppmrmname.DataBind();


        }

        private void binddata()
        {
            datajs = "";
            DataTable dt = bc.GetBulkCostList(Common.ConvertInt(Session["CompanyId"]), Common.ConvertInt(Session["UserId"]));
            hdnCompanyId.Value = Common.ConvertString(Session["CompanyId"]);
            if (dt.Rows.Count > 0)
            {
                gvbulkcost.DataSource = dt;
                gvbulkcost.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", datajs, true);


        }

      
        protected void gvbulkcost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drppmrm = (e.Row.FindControl("drppmrm") as DropDownList);
                DataTable dtrm = common.DropdownList("PMRMNameByPMRMCategoryBulkCost", "", "");
                Label lblBulkCostId = (Label)e.Row.FindControl("lblBulkCostId");
                HtmlButton btnReport = e.Row.FindControl("ReportActual") as HtmlButton;
                HtmlButton btnReportEstimate = e.Row.FindControl("ReportEstimate") as HtmlButton;

                TextBox txtPackingsize = (TextBox)e.Row.FindControl("txtPackingsize");

                DataRowView dr = (DataRowView)e.Row.DataItem;

                drppmrm.DataSource = dtrm;

                drppmrm.DataTextField = "Name";
                drppmrm.DataValueField = "Id";
                drppmrm.DataBind();

         

                drppmrm.Attributes.Add("onchange", "drpchangeval('" + drppmrm.ClientID + "');");

                //drppmrm.Items.Insert(0, new ListItem("--Select --", "0"));

                Label id = e.Row.FindControl("lblid") as Label;

                if (Convert.ToInt32(lblBulkCostId.Text) != tempbpm)
                {
                    tid = tid + 1;
                    id.Text = tid.ToString();
                    tempbpm = Convert.ToInt32(lblBulkCostId.Text);
                }

                if (e.Row.RowIndex >= 1)
                {
                    int index = e.Row.RowIndex - 1;
                    GridViewRow drold = gvbulkcost.Rows[index];
                    HiddenField hdntempbkid = (HiddenField)drold.FindControl("hdntempbkid");

                    if (dr.Row["FkBulkProductId"].ToString() == hdntempbkid.Value)
                    {
                        btnReport.Style.Add("display", "none");
                    }

                }

                if (dr.Row["type"].ToString() == "Actual")
                {
                    btnReportEstimate.Style.Add("display", "none");

                    datajs += "calculatePricelist('" + txtPackingsize.ClientID + "');";
                }
            }
        }

        protected void gvbulkcost_DataBound(object sender, EventArgs e)
        {
            int t = 0;
            for (int i = 1; i < gvbulkcost.Rows.Count; i++)
            {

                GridViewRow row = gvbulkcost.Rows[i - 1];
                GridViewRow previousRow = gvbulkcost.Rows[i];

                Label statusPrevious = previousRow.FindControl("lblBulkCostId") as Label;
                Label statuscurrent = row.FindControl("lblBulkCostId") as Label;

                //Label id = row.FindControl("lblid") as Label;

                if (statusPrevious.Text == statuscurrent.Text)
                {

                    if (row.Cells[0].RowSpan == 0)
                    {
                        if (row.Cells[0].RowSpan == 0)
                        {
                            row.Cells[0].RowSpan += 2;
                            //row.Cells[1].RowSpan += 2;
                            row.Cells[3].RowSpan += 2;
                            row.Cells[5].RowSpan += 2;
                            row.Cells[6].RowSpan += 2;
                            row.Cells[7].RowSpan += 2;
                            row.Cells[8].RowSpan += 2;
                            row.Cells[10].RowSpan += 2;
                            row.Cells[13].RowSpan += 2;
                            row.Cells[14].RowSpan += 2;
                            row.Cells[15].RowSpan += 2;
                            row.Cells[17].RowSpan += 2;
                            row.Cells[18].RowSpan += 2;
                            row.Cells[19].RowSpan += 2;
                            row.Cells[20].RowSpan += 2;


                            //t++;
                            //id.Text = t.ToString();

                        }
                        else
                        {
                            row.Cells[0].RowSpan = row.Cells[0].RowSpan + 1;
                            //row.Cells[1].RowSpan = row.Cells[1].RowSpan + 1;
                            row.Cells[3].RowSpan = row.Cells[3].RowSpan + 1;
                            row.Cells[5].RowSpan = row.Cells[5].RowSpan + 1;
                            row.Cells[6].RowSpan = row.Cells[5].RowSpan + 1;
                            row.Cells[7].RowSpan = row.Cells[6].RowSpan + 1;
                            row.Cells[8].RowSpan = row.Cells[8].RowSpan + 1;
                            row.Cells[10].RowSpan = row.Cells[10].RowSpan + 1;
                            row.Cells[13].RowSpan = row.Cells[13].RowSpan + 1;
                            row.Cells[14].RowSpan = row.Cells[14].RowSpan + 1;
                            row.Cells[15].RowSpan = row.Cells[15].RowSpan + 1;
                            row.Cells[17].RowSpan = row.Cells[17].RowSpan + 1;
                            row.Cells[18].RowSpan = row.Cells[18].RowSpan + 1;
                            row.Cells[19].RowSpan = row.Cells[19].RowSpan + 1;
                            row.Cells[20].RowSpan = row.Cells[20].RowSpan + 1;

                        }
                        previousRow.Cells[0].Visible = false;
                        //previousRow.Cells[1].Visible = false;
                        previousRow.Cells[3].Visible = false;
                        previousRow.Cells[5].Visible = false;
                        previousRow.Cells[6].Visible = false;
                        previousRow.Cells[7].Visible = false;
                        previousRow.Cells[8].Visible = false;
                        previousRow.Cells[10].Visible = false;
                        previousRow.Cells[13].Visible = false;
                        previousRow.Cells[14].Visible = false;
                        previousRow.Cells[15].Visible = false;
                        previousRow.Cells[17].Visible = false;
                        previousRow.Cells[18].Visible = false;
                        previousRow.Cells[19].Visible = false;
                        previousRow.Cells[20].Visible = false;

                    }

                }


            }
        }
        /*
        protected void drppmrm_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList btn = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            hdnpmrmid.Value = (gvr.FindControl("drppmrm") as DropDownList).SelectedValue;

            //Label Bpmid = (Label)sender;
            //GridViewRow gvrBpm = (GridViewRow)Bpmid.NamingContainer;
            string BpmId = (gvr.FindControl("lblbpmid") as Label).Text;
            DataTable dt = pmrm.Get_PMRMMaster(Common.ConvertInt(Session["UserId"]), Common.ConvertInt(hdnpmrmid.Value));
            hdnUnitPerKg.Value = Common.ConvertString(dt.Rows[0]["PerUnitWeight"]);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", datajs + "calculatePricelist(hdnpmrmid.Value);", true);

        }
        */


        protected void btnTerms_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int FkBulkProductId = Common.ConvertInt(btn.CommandArgument);

            if (FkBulkProductId > 0)
            {
                //Common.FinishedGoodReportDetailActual(Common.ConvertInt(Session["UserId"]), ProductId, out html, out bulk);
                DataTable dtterm = common.DropdownList("GetTermsCondition", "", "");
                //ListBox TermsConditionListbox = (e.Row.FindControl("TermsConditionListbox") as ListBox);

                TermsConditionListbox.DataSource = dtterm;

                TermsConditionListbox.DataTextField = "Name";
                TermsConditionListbox.DataValueField = "Id";
                TermsConditionListbox.DataBind();

                //binddata();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "showhidemodel('1');", true);
            }
        }


    }
}