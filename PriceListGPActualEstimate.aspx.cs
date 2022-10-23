using BAL;
using DAL;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{

    public partial class PriceListGPActualEstimate : BasePage
    {
        public static string jscode = "";
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        PriceListGPActualEstimateDAL plgp = new PriceListGPActualEstimateDAL();
        PriceListGPActualEstimateBAL plgpdata = new PriceListGPActualEstimateBAL();
        RMEstimateDetailDAL RMED = new RMEstimateDetailDAL();
        FinishGoodsPricingReportDAL FG = new FinishGoodsPricingReportDAL();

        int CompanyId = 0;
        int FkBulkProductId = 0;
        int PriceListGPActualEstimateId = 0;
        public string datajs = "";
        public string datajs2 = "";
        public static string datadisplay = "";
        public static string datadisplayfinal = "";
        public static string datadisplayEstimate = "";
        public static string datadisplayfinalEstimate = "";
        public static string IsEstimate = "0";
        decimal interestAmt = 0;
        decimal expense = 0;
        decimal interestAmtEstimate = 0;
        decimal expenseEstimate = 0;
        int TradeId = 0;

        public static string t_bmpid = "0";
        public static string t_isestimate = "0";
        public static string t_tradeid = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //binddropdown();

                //EstimateId = Common.ConvertInt(Session["EstimateId"]);
                CompanyId = Common.ConvertInt(Session["CompanyId"]);
                //FkBulkProductId = Common.ConvertInt(Session["FkBulkProductId"]);
                //IsEstimate = Common.ConvertString(Request.QueryString["IsEstimate"]);
                hdnIsEstimate.Value = IsEstimate;

                FkBulkProductId = Common.ConvertInt(Request.QueryString["bpmId"]);
                btnFkBulkproductId.Value = FkBulkProductId.ToString();
                TradeId = Common.ConvertInt(Request.QueryString["TradeId"]);

                t_bmpid = Common.ConvertString(Request.QueryString["bpmId"]);
                //t_isestimate = Common.ConvertString(Request.QueryString["IsEstimate"]);
                t_tradeid = Common.ConvertString(Request.QueryString["TradeId"]);



                PriceListGPActualEstimateId = Common.ConvertInt(Session["PriceListGPActualEstimateId"]);
                binddata(0);

                btnSwitchReadOnlyEdit_Click();

            }
            else
            {
                //binddata(0);
            }
        }
        public void btnSwitchReadOnlyEdit_Click()
        {
            //editreadonly();
        }
        private void binddata(int tp)
        {
            datajs = "";
            DataTable dt = new DataTable();

            string BPMId = Common.ConvertString(Request.QueryString["bpmId"]);
            ReturnMessage objs = common.CheckExist("GetRMPriceEstimate", BPMId, "", "");
            IsEstimate = Common.ConvertString(objs.ReturnValue);
            t_isestimate = Common.ConvertString(objs.ReturnValue);
            hdnIsEstimate.Value = IsEstimate;
            if (IsEstimate == "0")
            {
                divrdActualEstimate.Visible = false;

            }
            else
            {
                divrdActualEstimate.Visible = true;

            }

            if (IsEstimate != "0")
            {
                dt = plgp.GetPriceListGPActualEstimateByBPMId(Common.ConvertInt(Session["UserId"]), Common.ConvertInt(Request.QueryString["bpmId"]), Common.ConvertInt(Session["CompanyId"]), 0);
                if (dt.Rows.Count > 0)
                {
                    //settotalandexpense(EstimateId, FkBulkProductId, UserId);

                    //DataRow[] dract = dt.Select("status='Actual'");

                    //DataRow[] drest = dt.Select("status='Estimate'");

                    lblBulkProductName.Text = Common.ConvertString(dt.Rows[0]["BPM_Product_Name"]);
                    hdnbulkProductName.Value = Common.ConvertString(dt.Rows[0]["BPM_Product_Name"]);
                    interestAmt = Common.ConvertDecimal(dt.Rows[0]["InterestAmt"]);
                    expense = decimal.Parse(dt.Rows[0]["TotalExpense"].ToString());

                    interestAmt = Common.ConvertDecimal(dt.Rows[0]["InterestAmt"]);
                    expense = decimal.Parse(dt.Rows[0]["TotalExpense"].ToString());

                    interestAmtEstimate = Common.ConvertDecimal(dt.Rows[2]["InterestAmt"]);
                    expenseEstimate = decimal.Parse(dt.Rows[2]["TotalExpense"].ToString());

                    //for Actual
                    decimal total = interestAmt + Common.ConvertDecimal(expense);
                    datadisplay = interestAmt.ToString() + "+" + Common.ConvertDecimal(expense) + "(" + total.ToString() + ")";
                    datadisplayfinal = total.ToString();
                    //--

                    //for Estimate

                    decimal totalEstimate = interestAmtEstimate + Common.ConvertDecimal(expenseEstimate);
                    datadisplayEstimate = interestAmtEstimate.ToString() + "+" + Common.ConvertDecimal(expenseEstimate) + "(" + totalEstimate.ToString() + ")";
                    datadisplayfinalEstimate = totalEstimate.ToString();

                    gvgpactualestimate.DataSource = dt;
                    gvgpactualestimate.DataBind();
                    gvgpactualestimate.Visible = true;


                    jscode = datajs;


                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", datajs + "EnableLoader(false);", true);



                }
            }
            else
            {
                dt = plgp.GetPriceListGPActualByBPMId(Common.ConvertInt(Session["UserId"]), Common.ConvertInt(Request.QueryString["bpmId"]), Common.ConvertInt(Session["CompanyId"]));
                if (dt.Rows.Count > 0)
                {
                    //settotalandexpense(EstimateId, FkBulkProductId, UserId);
                    datadisplay = "";


                    lblBulkProductName.Text = Common.ConvertString(dt.Rows[0]["BPM_Product_Name"]);
                    interestAmt = Common.ConvertDecimal(dt.Rows[0]["InterestAmt"]);
                    expense = decimal.Parse(dt.Rows[0]["TotalExpense"].ToString());
                    hdnbulkProductName.Value = Common.ConvertString(dt.Rows[0]["BPM_Product_Name"]);


                    decimal total = interestAmt + Common.ConvertDecimal(expense);
                    datadisplay = interestAmt.ToString() + "+" + Common.ConvertDecimal(expense) + "(" + total.ToString() + ")";
                    datadisplayfinal = total.ToString();

                    gvpricelistactual.DataSource = dt;
                    gvpricelistactual.DataBind();
                    gvpricelistactual.Visible = true;



                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", datajs2, true);



                }

            }



        }
        public void settotalandexpense(int EstimateId, int FkBulkProductId, int UserId)
        {

            DataSet ds = RMED.GetRMEstimateDetailReport(FkBulkProductId, UserId);
            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[1].Rows.Count > 0)
                {
                    interestAmt = decimal.Parse(ds.Tables[0].Rows[0]["InterestAmt"].ToString());
                }

            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (hdngrdcalc.Value == "1")
            {
                rdActualEstimate.Items[0].Selected = true;
                rdActualEstimate.Items[1].Selected = false;
            }
            else
            {
                rdActualEstimate.Items[0].Selected = false;
                rdActualEstimate.Items[1].Selected = true;
            }

            if (btn.ClientID != null)
            {
                int rowno = Common.ConvertInt(btn.ClientID.Substring(btn.ClientID.LastIndexOf('_') + 1));
                GridViewRow row;
                string StateId;
                TextBox txtFinalPrice;
                TextBox txtAdditionalPD;
                string msgs = "";
                {
                    if (IsEstimate != "0")
                    {
                        row = gvgpactualestimate.Rows[rowno];
                        if (row != null)
                        {

                            foreach (GridViewRow row1 in gvgpactualestimate.Rows)
                            {

                                Label lblFk_State_Id = (Label)row.FindControl("lblFkStateId");

                                Label PriceListGPActualEStimateId = (Label)row1.FindControl("lblPriceListGPActualEstimateId");
                                StateId = Common.ConvertString((row1.FindControl("lblFkStateId") as Label).Text);
                                if (lblFk_State_Id.Text == StateId)
                                {
                                    Label lblFk_BPM_Id = (Label)row1.FindControl("lblFkBulkProductId");
                                    Label lblFkPriceTypeId = (Label)row1.FindControl("lblFkPriceTypeId");
                                    Label lblTradeName_Id = (Label)row1.FindControl("lblTradeId");
                                    Label lblPackingMaterialId = (Label)row1.FindControl("lblPackingMaterialId");
                                    Label lblStatus = (Label)row1.FindControl("lblStatus");

                                    Label lblEnumDescription = (Label)row1.FindControl("lblEnumDescription");


                                    TextBox txtTOD = (TextBox)row1.FindControl("TODtxt");
                                    TextBox txtPD = (TextBox)row1.FindControl("PDtxt");
                                    TextBox txtQD = (TextBox)row1.FindControl("QDtxt");
                                    TextBox txtProfitPer = (TextBox)row1.FindControl("ProfitPertxt");




                                    plgpdata.TOD = Common.ConvertDecimal(txtTOD.Text);
                                    plgpdata.PD = Common.ConvertDecimal(txtPD.Text);
                                    plgpdata.QD = Common.ConvertDecimal(txtQD.Text);
                                    plgpdata.ProfitPer = Common.ConvertDecimal(txtProfitPer.Text);

                                    if (lblStatus.Text == "Estimate" && lblEnumDescription.Text.ToUpper() == "NCR")
                                    {
                                        GridViewRow gptemp = gvgpactualestimate.Rows[row1.RowIndex - 1];

                                        txtFinalPrice = (TextBox)gptemp.FindControl("FinalPricettxt");
                                        txtAdditionalPD = (TextBox)gptemp.FindControl("AdditionalPDtxt");

                                        plgpdata.FinalPrice = Common.ConvertDecimal(txtFinalPrice.Text);
                                        plgpdata.AdditionalPD = Common.ConvertDecimal(txtAdditionalPD.Text);
                                    }
                                    else
                                    {
                                        if (lblEnumDescription.Text.ToUpper() == "NCR")
                                        {
                                            GridViewRow gptemp = gvgpactualestimate.Rows[row1.RowIndex - 1];

                                            txtFinalPrice = (TextBox)gptemp.FindControl("FinalPricettxt");
                                            txtAdditionalPD = (TextBox)gptemp.FindControl("AdditionalPDtxt");

                                            plgpdata.FinalPrice = Common.ConvertDecimal(txtFinalPrice.Text);
                                            plgpdata.AdditionalPD = Common.ConvertDecimal(txtAdditionalPD.Text);
                                        }
                                        else
                                        {

                                            txtFinalPrice = (TextBox)row1.FindControl("FinalPricettxt");
                                            txtAdditionalPD = (TextBox)row1.FindControl("AdditionalPDtxt");
                                            plgpdata.FinalPrice = Common.ConvertDecimal(txtFinalPrice.Text);
                                            plgpdata.AdditionalPD = Common.ConvertDecimal(txtAdditionalPD.Text);
                                        }
                                    }


                                    plgpdata.FkPackingMaterialId = Common.ConvertInt(lblPackingMaterialId.Text);
                                    plgpdata.Status = lblStatus.Text;
                                    plgpdata.FkStateId = Common.ConvertInt(lblFk_State_Id.Text);
                                    plgpdata.FkBulkProductId = Common.ConvertInt(lblFk_BPM_Id.Text);
                                    plgpdata.FkPriceTypeId = Common.ConvertInt(lblFkPriceTypeId.Text);
                                    plgpdata.FkTradeId = Common.ConvertInt(Request.QueryString["TradeId"]);

                                    plgpdata.EstimateId = Common.ConvertInt(Session["EstimateId"]);
                                    plgpdata.UserId = Common.ConvertInt(Session["UserId"]);

                                    plgpdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
                                    if (Common.ConvertInt(PriceListGPActualEStimateId.Text) == 0)
                                    {
                                        plgpdata.action = 1;
                                    }
                                    else
                                    {
                                        plgpdata.PriceListGPActualEstimateId = Common.ConvertInt(PriceListGPActualEStimateId.Text);

                                        plgpdata.action = 2;
                                    }
                                    ReturnMessage obj = null;
                                    obj = plgp.InsertUpdatePriceListGPActualEstimate(plgpdata);
                                    string msg = msgs = Common.ConvertString(obj.Message);
                                    
                                }

                            }


                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "');", true);

                            binddata(0);

                        }

                    }
                    else
                    {
                        row = gvpricelistactual.Rows[rowno];
                        if (row != null)
                        {

                            foreach (GridViewRow row1 in gvpricelistactual.Rows)
                            {

                                Label lblFk_State_Id = (Label)row.FindControl("lblFkStateId");

                                Label PriceListGPActualEStimateId = (Label)row1.FindControl("lblPriceListGPActualEstimateId");
                                StateId = Common.ConvertString((row1.FindControl("lblFkStateId") as Label).Text);
                                if (lblFk_State_Id.Text == StateId)
                                {
                                    Label lblFk_BPM_Id = (Label)row1.FindControl("lblFkBulkProductId");
                                    Label lblFkPriceTypeId = (Label)row1.FindControl("lblFkPriceTypeId");
                                    Label lblTradeName_Id = (Label)row1.FindControl("lblTradeName_Id");
                                    Label lblPackingMaterialId = (Label)row1.FindControl("lblPackingMaterialId");
                                    Label lblStatus = (Label)row1.FindControl("lblStatus");


                                    TextBox txtTOD = (TextBox)row1.FindControl("TODtxt");
                                    TextBox txtPD = (TextBox)row1.FindControl("PDtxt");
                                    TextBox txtQD = (TextBox)row1.FindControl("QDtxt");
                                    TextBox txtProfitPer = (TextBox)row1.FindControl("ProfitPertxt");



                                    plgpdata.TOD = Common.ConvertDecimal(txtTOD.Text);
                                    plgpdata.PD = Common.ConvertDecimal(txtPD.Text);
                                    plgpdata.QD = Common.ConvertDecimal(txtQD.Text);
                                    plgpdata.ProfitPer = Common.ConvertDecimal(txtProfitPer.Text);
                                    txtFinalPrice = (TextBox)row1.FindControl("FinalPricettxt");
                                    txtAdditionalPD = (TextBox)row1.FindControl("AdditionalPDtxt");
                                    plgpdata.FinalPrice = Common.ConvertDecimal(txtFinalPrice.Text);
                                    plgpdata.AdditionalPD = Common.ConvertDecimal(txtAdditionalPD.Text);
                                    plgpdata.FkPackingMaterialId = Common.ConvertInt(lblPackingMaterialId.Text);
                                    plgpdata.FkStateId = Common.ConvertInt(lblFk_State_Id.Text);
                                    plgpdata.FkBulkProductId = Common.ConvertInt(lblFk_BPM_Id.Text);
                                    plgpdata.FkPriceTypeId = Common.ConvertInt(lblFkPriceTypeId.Text);
                                    plgpdata.FkTradeId = Common.ConvertInt(Request.QueryString["TradeId"]);

                                    plgpdata.EstimateId = Common.ConvertInt(Session["EstimateId"]);
                                    plgpdata.UserId = Common.ConvertInt(Session["UserId"]);

                                    if (plgpdata.FkPriceTypeId == 8)
                                    {
                                        plgpdata.Status = "Actual";
                                    }
                                    else
                                    {
                                        plgpdata.Status = "Estimate";

                                    }


                                    plgpdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
                                    if (Common.ConvertInt(PriceListGPActualEStimateId.Text) == 0)
                                    {
                                        plgpdata.action = 1;
                                    }
                                    else
                                    {
                                        plgpdata.PriceListGPActualEstimateId = Common.ConvertInt(PriceListGPActualEStimateId.Text);

                                        plgpdata.action = 2;
                                    }

                                    ReturnMessage obj = null;
                                    obj = plgp.InsertUpdatePriceListGPActualEstimate(plgpdata);
                                    string msg = msgs = Common.ConvertString(obj.Message);

                                }
                            }
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "');", true);

                            binddata(0);


                        }

                    }


                }


            }

        }



        protected void GPActualEstimateFinalBtn_Click(object sender, EventArgs e)
        {
            if (Common.ConvertInt(Session["EstimateId"]) == 0)
            {
                Response.Redirect("~/PriceListGP.aspx");

            }
            else
            {
                Response.Redirect("~/PriceListGP.aspx");

            }


        }



        //private void editreadonly()
        //{

        //    if (btnSwitchReadOnlyEdit.SelectedValue == "1")

        //    {
        //        foreach (GridViewRow row2 in gvgpactualestimate.Rows)
        //        {

        //            (row2.FindControl("ProfitAmttxt_lbl") as Label).Enabled = true;

        //            (row2.FindControl("GrossProfitAmounttxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("lblGrossProfitPer_lbl") as Label).Enabled = true;
        //            (row2.FindControl("TotalExpencetxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("NetProfitAmounttxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("lblNetProfitAmtPer_lbl") as Label).Enabled = true;
        //            (row2.FindControl("SuggestedPriceWithPDttxt_lbl") as Label).Enabled = true;
        //            //(row2.FindControl("FinalPricettxt") as TextBox).Enabled = true;
        //            if ("Estimate" == (row2.FindControl("lblStatus") as Label).Text)
        //            {
        //                (row2.FindControl("PDtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("QDtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("TODtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("ProfitPertxt") as TextBox).Enabled = true;
        //                (row2.FindControl("FinalPricettxt") as TextBox).Enabled = true;
        //                (row2.FindControl("AdditionalPDtxt") as TextBox).Enabled = true;
        //            }
        //            else
        //            {
        //                (row2.FindControl("PDtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("QDtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("TODtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("ProfitPertxt") as TextBox).Enabled = true;
        //                (row2.FindControl("FinalPricettxt") as TextBox).Enabled = true;
        //                (row2.FindControl("AdditionalPDtxt") as TextBox).Enabled = true;
        //            }
        //            Label lblEnumDescription = (Label)row2.FindControl("lblEnumDescription");
        //            Label lblNewStatus = (Label)row2.FindControl("lblNewStatus");
        //            TextBox PDtxt = (TextBox)row2.FindControl("PDtxt");


        //            if (lblEnumDescription.Text.ToUpper() == "RPL" && lblNewStatus.Text.ToUpper() == "RPL")
        //            {
        //                datajs += "calculaterate('1','" + PDtxt.ClientID + "','1');";

        //            }

        //        }
        //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", datajs, true);

        //    }
        //    else
        //    {

        //        foreach (GridViewRow row2 in gvgpactualestimate.Rows)
        //        {

        //            (row2.FindControl("ProfitAmttxt_lbl") as Label).Enabled = true;

        //            (row2.FindControl("GrossProfitAmounttxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("lblGrossProfitPer_lbl") as Label).Enabled = true;
        //            (row2.FindControl("TotalExpencetxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("NetProfitAmounttxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("lblNetProfitAmtPer_lbl") as Label).Enabled = true;
        //            (row2.FindControl("SuggestedPriceWithPDttxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("FinalPricettxt") as TextBox).Enabled = false;

        //            (row2.FindControl("PDtxt") as TextBox).Enabled = false;
        //            (row2.FindControl("QDtxt") as TextBox).Enabled = false;
        //            (row2.FindControl("TODtxt") as TextBox).Enabled = false;
        //            (row2.FindControl("ProfitPertxt") as TextBox).Enabled = false;
        //            (row2.FindControl("FinalPricettxt") as TextBox).Enabled = false;
        //            (row2.FindControl("AdditionalPDtxt") as TextBox).Enabled = false;

        //            Label lblEnumDescription = (Label)row2.FindControl("lblEnumDescription");
        //            Label lblNewStatus = (Label)row2.FindControl("lblNewStatus");
        //            TextBox PDtxt = (TextBox)row2.FindControl("PDtxt");
        //            if (lblEnumDescription.Text.ToUpper() == "RPL" && lblNewStatus.Text.ToUpper() == "RPL")
        //            {
        //                datajs += "calculaterate('1','" + PDtxt.ClientID + "','1');";

        //            }

        //        }
        //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", datajs, true);

        //    }
        //}

        //protected void btnSwitchReadOnlyEdit_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (btnSwitchReadOnlyEdit.SelectedValue == "1")

        //    {
        //        foreach (GridViewRow row2 in gvgpactualestimate.Rows)
        //        {

        //            (row2.FindControl("ProfitAmttxt_lbl") as Label).Enabled = true;

        //            (row2.FindControl("GrossProfitAmounttxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("lblGrossProfitPer_lbl") as Label).Enabled = true;
        //            (row2.FindControl("TotalExpencetxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("NetProfitAmounttxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("lblNetProfitAmtPer_lbl") as Label).Enabled = true;
        //            (row2.FindControl("SuggestedPriceWithPDttxt_lbl") as Label).Enabled = true;
        //            //(row2.FindControl("FinalPricettxt") as TextBox).Enabled = true;
        //            if ("Estimate" == (row2.FindControl("lblStatus") as Label).Text)
        //            {
        //                (row2.FindControl("PDtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("QDtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("TODtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("ProfitPertxt") as TextBox).Enabled = true;
        //                (row2.FindControl("FinalPricettxt") as TextBox).Enabled = true;
        //                (row2.FindControl("AdditionalPDtxt") as TextBox).Enabled = true;


        //            }
        //            else
        //            {
        //                (row2.FindControl("PDtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("QDtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("TODtxt") as TextBox).Enabled = true;
        //                (row2.FindControl("ProfitPertxt") as TextBox).Enabled = true;
        //                (row2.FindControl("FinalPricettxt") as TextBox).Enabled = true;
        //                (row2.FindControl("AdditionalPDtxt") as TextBox).Enabled = true;
        //            }
        //            Label lblEnumDescription = (Label)row2.FindControl("lblEnumDescription");
        //            Label lblNewStatus = (Label)row2.FindControl("lblNewStatus");
        //            TextBox PDtxt = (TextBox)row2.FindControl("PDtxt");


        //            if (lblEnumDescription.Text.ToUpper() == "RPL" && lblNewStatus.Text.ToUpper() == "RPL")
        //            {
        //                datajs += "calculaterate('1','" + PDtxt.ClientID + "','1');";

        //            }


        //        }
        //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", datajs, true);

        //    }
        //    else
        //    {

        //        foreach (GridViewRow row2 in gvgpactualestimate.Rows)
        //        {

        //            (row2.FindControl("ProfitAmttxt_lbl") as Label).Enabled = true;

        //            (row2.FindControl("GrossProfitAmounttxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("lblGrossProfitPer_lbl") as Label).Enabled = true;
        //            (row2.FindControl("TotalExpencetxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("NetProfitAmounttxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("lblNetProfitAmtPer_lbl") as Label).Enabled = true;
        //            (row2.FindControl("SuggestedPriceWithPDttxt_lbl") as Label).Enabled = true;
        //            (row2.FindControl("FinalPricettxt") as TextBox).Enabled = false;

        //            (row2.FindControl("PDtxt") as TextBox).Enabled = true;
        //            (row2.FindControl("QDtxt") as TextBox).Enabled = true;
        //            (row2.FindControl("TODtxt") as TextBox).Enabled = true;
        //            (row2.FindControl("ProfitPertxt") as TextBox).Enabled = true;
        //            (row2.FindControl("FinalPricettxt") as TextBox).Enabled = true;
        //            (row2.FindControl("AdditionalPDtxt") as TextBox).Enabled = true;

        //            Label lblEnumDescription = (Label)row2.FindControl("lblEnumDescription");
        //            Label lblNewStatus = (Label)row2.FindControl("lblNewStatus");
        //            TextBox PDtxt = (TextBox)row2.FindControl("PDtxt");
        //            if (lblEnumDescription.Text.ToUpper() == "RPL" && lblNewStatus.Text.ToUpper() == "RPL")
        //            {
        //                datajs += "calculaterate('1','" + PDtxt.ClientID + "','1');";

        //            }

        //        }
        //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", datajs, true);

        //    }
        //}

        protected void gvgpactualestimate_RowDataBound1(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataRowView dr = (DataRowView)e.Row.DataItem;

                Label lblEnumDescription = (Label)e.Row.FindControl("lblEnumDescription");
                Label lblNewStatus = (Label)e.Row.FindControl("lblNewStatus");
                TextBox PDtxt = (TextBox)e.Row.FindControl("PDtxt");
                TextBox QDtxt = (TextBox)e.Row.FindControl("QDtxt");
                TextBox TODtxt = (TextBox)e.Row.FindControl("TODtxt");
                TextBox ProfitPertxt = (TextBox)e.Row.FindControl("ProfitPertxt");


                if (dr["status"].ToString() == "Estimate")
                {
                    PDtxt.Enabled = false;
                    QDtxt.Enabled = false;
                    TODtxt.Enabled = false;
                    ProfitPertxt.Enabled = false;
                }
                else
                {
                    PDtxt.Enabled = true;
                    QDtxt.Enabled = true;
                    TODtxt.Enabled = true;
                    ProfitPertxt.Enabled = true;
                }

                if (lblEnumDescription.Text.ToUpper() == "RPL") //&& lblNewStatus.Text.ToUpper() == "RPL"
                {
                    datajs += "calculaterate('1','" + PDtxt.ClientID + "','1');";

                }

                if (dr["status"].ToString() == "Estimate" && lblNewStatus.Text.ToString() == "NCR" && lblEnumDescription.Text.ToString() == "RPL")
                {
                    Label lblStaffExpense = (Label)(e.Row.FindControl("lblStaffExpense"));
                    Label lblMarketing = (Label)(e.Row.FindControl("lblMarketing"));
                    Label lblIncentive = (Label)(e.Row.FindControl("lblIncentive"));
                    Label lblInterest = (Label)(e.Row.FindControl("lblInterest"));
                    Label lblDepoExpence = (Label)(e.Row.FindControl("lblDepoExpence"));
                    Label lblOther = (Label)(e.Row.FindControl("lblOther"));

                    int ind = e.Row.RowIndex - 1;
                    GridViewRow gvRow = gvgpactualestimate.Rows[ind];

                    Label lblStaffExpense_old = (Label)gvRow.FindControl("lblStaffExpense");
                    Label lblMarketing_old = (Label)(gvRow.FindControl("lblMarketing"));
                    Label lblIncentive_old = (Label)(gvRow.FindControl("lblIncentive"));
                    Label lblInterest_old = (Label)(gvRow.FindControl("lblInterest"));
                    Label lblDepoExpence_old = (Label)(gvRow.FindControl("lblDepoExpence"));
                    Label lblOther_old = (Label)(gvRow.FindControl("lblOther"));


                    lblStaffExpense.Text = lblStaffExpense_old.Text;
                    lblMarketing.Text = lblMarketing_old.Text;
                    lblIncentive.Text = lblIncentive_old.Text;
                    lblInterest.Text = lblInterest_old.Text;
                    lblDepoExpence.Text = lblDepoExpence_old.Text;
                    lblOther.Text = lblOther_old.Text;
                }

                /*
                if (dr["status"].ToString() == "Actual" && lblEnumDescription.Text.ToUpper() == "NCR")
                {
                    (e.Row.FindControl("TODtxt") as TextBox).Attributes.Add("style", "display:none");
                    (e.Row.FindControl("QDtxt") as TextBox).Attributes.Add("style", "display:none");
                    (e.Row.FindControl("PDtxt") as TextBox).Attributes.Add("style", "display:none");
                    (e.Row.FindControl("lblNewStatus") as Label).Attributes.Add("style", "display:none");
                    (e.Row.FindControl("lblEnumDescription") as Label).Attributes.Add("style", "display:none");
                }
                if (dr["status"].ToString() == "Estimate" && lblEnumDescription.Text.ToUpper() == "RPL")
                {
                    (e.Row.FindControl("TODtxt") as TextBox).Attributes.Add("style", "display:none");
                    (e.Row.FindControl("QDtxt") as TextBox).Attributes.Add("style", "display:none");
                    (e.Row.FindControl("PDtxt") as TextBox).Attributes.Add("style", "display:none");
                    (e.Row.FindControl("lblNewStatus") as Label).Attributes.Add("style", "display:none");
                    (e.Row.FindControl("lblEnumDescription") as Label).Attributes.Add("style", "display:none");

                }
                */
            }



            for (int rowIndex = gvgpactualestimate.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = gvgpactualestimate.Rows[rowIndex];
                GridViewRow gvPreviousRow = gvgpactualestimate.Rows[rowIndex + 1];

                Label lblPrevStateName = gvPreviousRow.FindControl("lblStateName") as Label;
                Label lblStateName = gvRow.FindControl("lblStateName") as Label;

                if (lblPrevStateName.Text == lblStateName.Text)
                {
                    if (gvPreviousRow.Cells[0].RowSpan < 2)
                    {
                        gvRow.Cells[0].RowSpan = 2;
                        //e.Row.Cells[0].CssClass = "locked";
                    }
                    else
                    {
                        gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                    }
                    gvRow.Cells[0].Attributes.Add("class", "sticky-col first-col");

                    gvPreviousRow.Cells[0].Visible = false;
                }

                Label lblPrevStatus = gvPreviousRow.FindControl("lblStatus") as Label;
                Label lblStatus = gvRow.FindControl("lblStatus") as Label;
                if (lblPrevStatus.Text == lblStatus.Text)
                {
                    if (gvPreviousRow.Cells[1].RowSpan < 2)
                    {
                        gvRow.Cells[1].RowSpan = 2;
                        //e.Row.Cells[0].CssClass = "locked";
                    }
                    else
                    {
                        gvRow.Cells[1].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                    }
                    gvRow.Cells[1].Attributes.Add("class", "sticky-col second-col");

                    gvPreviousRow.Cells[1].Visible = false;
                }


                Label lblPrevNRV = gvPreviousRow.FindControl("lblNRV") as Label;
                Label lblNRV = gvRow.FindControl("lblNRV") as Label;
                if (lblPrevNRV.Text == lblNRV.Text)
                {
                    if (gvPreviousRow.Cells[2].RowSpan < 2)
                    {
                        gvRow.Cells[2].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[2].RowSpan = gvPreviousRow.Cells[2].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[2].Visible = false;
                }


                Label lblPrevTransport = gvPreviousRow.FindControl("lblTransport") as Label;
                Label lblTransport = gvRow.FindControl("lblTransport") as Label;
                if (lblPrevNRV.Text == lblNRV.Text)
                {
                    if (gvPreviousRow.Cells[3].RowSpan < 2)
                    {
                        gvRow.Cells[3].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[3].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[3].Visible = false;
                }

                Label lblPrevFinalNRV = gvPreviousRow.FindControl("lblFinalNRV") as Label;
                Label lblFinalNRV = gvRow.FindControl("lblFinalNRV") as Label;
                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[4].RowSpan < 2)
                    {
                        gvRow.Cells[4].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[4].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[4].Visible = false;
                }
                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[5].RowSpan < 2)
                    {
                        gvRow.Cells[5].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[5].RowSpan = gvPreviousRow.Cells[5].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[5].Visible = false;
                }

                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[13].RowSpan < 2)
                    {
                        gvRow.Cells[13].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[13].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[13].Visible = false;
                }

                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[14].RowSpan < 2)
                    {
                        gvRow.Cells[14].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[14].RowSpan = gvPreviousRow.Cells[14].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[14].Visible = false;
                }
                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    gvRow.Cells[15].BackColor = System.Drawing.Color.Yellow;
                    if (gvPreviousRow.Cells[15].RowSpan < 2)
                    {
                        gvRow.Cells[15].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[15].RowSpan = gvPreviousRow.Cells[15].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[15].Visible = false;
                }

                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {
                    if (gvPreviousRow.Cells[16].RowSpan < 2)
                    {
                        gvRow.Cells[16].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[16].RowSpan = gvPreviousRow.Cells[16].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[16].Visible = false;
                }

                if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                {


                    if (gvPreviousRow.Cells[17].RowSpan < 2)
                    {
                        gvRow.Cells[17].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[17].RowSpan = gvPreviousRow.Cells[17].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[17].Visible = false;
                }

                if (lblPrevStatus.Text == lblStatus.Text)
                {
                    gvRow.Cells[18].BackColor = System.Drawing.Color.LightGreen;
                    if (gvPreviousRow.Cells[18].RowSpan < 2)
                    {
                        gvRow.Cells[18].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[18].RowSpan = gvPreviousRow.Cells[18].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[18].Visible = false;
                }
                if (lblPrevStatus.Text == lblStatus.Text)
                {
                    if (gvPreviousRow.Cells[19].RowSpan < 2)
                    {
                        gvRow.Cells[19].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[19].RowSpan = gvPreviousRow.Cells[19].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[19].Visible = false;
                }

                if (lblPrevStateName.Text == lblStateName.Text)
                {
                    if (gvPreviousRow.Cells[20].RowSpan < 2)
                    {
                        gvRow.Cells[20].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[20].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[20].Visible = false;
                }
            }

        }

        protected void gvpricelistactual_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox PDtxt = (TextBox)e.Row.FindControl("PDtxt");
                datajs2 += "calculaterate_actual('0','" + (e.Row.FindControl("PDtxt") as TextBox).ClientID + "');";

                DataTable dtNew = ViewState["dtNew"] as DataTable;
                for (int i = 5; i < e.Row.Cells.Count; i++)

                    lblDynamicColumnCount.Text = (e.Row.Cells.Count).ToString();

            }




            for (int rowIndex = gvpricelistactual.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = gvpricelistactual.Rows[rowIndex];
                GridViewRow gvPreviousRow = gvpricelistactual.Rows[rowIndex + 1];
                for (int cellCount = 0; cellCount < gvRow.Cells.Count; cellCount++)
                {
                    Label lblPrevStateName = gvPreviousRow.FindControl("lblStateName") as Label;
                    Label lblStateName = gvRow.FindControl("lblStateName") as Label;

                    if (lblPrevStateName.Text == lblStateName.Text)
                    {
                        if (gvPreviousRow.Cells[0].RowSpan < 2)
                        {
                            gvRow.Cells[0].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                        }
                        gvRow.Cells[0].Attributes.Add("class", "sticky-col first-col");

                        gvPreviousRow.Cells[0].Visible = false;
                    }

                    Label lblPrevStatus = gvPreviousRow.FindControl("lblStatus") as Label;
                    Label lblStatus = gvRow.FindControl("lblStatus") as Label;
                    if (lblPrevStatus.Text == lblStatus.Text)
                    {
                        if (gvPreviousRow.Cells[1].RowSpan < 2)
                        {
                            gvRow.Cells[1].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[1].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                        }
                        gvRow.Cells[1].Attributes.Add("class", "sticky-col second-col");

                        gvPreviousRow.Cells[1].Visible = false;
                    }

                    Label lblPrevNRV = gvPreviousRow.FindControl("lblNRV") as Label;
                    Label lblNRV = gvRow.FindControl("lblNRV") as Label;
                    if (lblPrevNRV.Text == lblNRV.Text)
                    {
                        if (gvPreviousRow.Cells[2].RowSpan < 2)
                        {
                            gvRow.Cells[2].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[2].RowSpan = gvPreviousRow.Cells[2].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[2].Visible = false;
                    }

                    Label lblPrevTransport = gvPreviousRow.FindControl("lblTransport") as Label;
                    Label lblTransport = gvRow.FindControl("lblTransport") as Label;
                    if (lblPrevTransport.Text == lblTransport.Text)
                    {
                        if (gvPreviousRow.Cells[3].RowSpan < 2)
                        {
                            gvRow.Cells[3].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[3].RowSpan = gvPreviousRow.Cells[3].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[3].Visible = false;
                    }
                    Label lblPrevFinalNRV = gvPreviousRow.FindControl("lblFinalNRV") as Label;
                    Label lblFinalNRV = gvRow.FindControl("lblFinalNRV") as Label;
                    if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                    {
                        if (gvPreviousRow.Cells[4].RowSpan < 2)
                        {
                            gvRow.Cells[4].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[4].RowSpan = gvPreviousRow.Cells[4].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[4].Visible = false;
                    }

                    if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                    {
                        if (gvPreviousRow.Cells[5].RowSpan < 2)
                        {
                            gvRow.Cells[5].RowSpan = 2;

                        }
                        else
                        {
                            gvRow.Cells[5].RowSpan = gvPreviousRow.Cells[5].RowSpan + 1;

                        }
                        gvPreviousRow.Cells[5].Visible = false;

                    }
                    gvRow.Cells[14].BackColor = System.Drawing.Color.Yellow;
                    gvRow.Cells[17].BackColor = System.Drawing.Color.LightGreen;


                    if (lblPrevFinalNRV.Text == lblFinalNRV.Text)
                    {
                        if (gvPreviousRow.Cells[19].RowSpan < 2)
                        {
                            gvRow.Cells[19].RowSpan = 2;
                            gvRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan = 2;

                        }
                        else
                        {
                            gvRow.Cells[19].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                            gvRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan = gvPreviousRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].RowSpan + 1;

                        }
                        gvPreviousRow.Cells[19].Visible = false;
                        gvPreviousRow.Cells[Convert.ToInt32(lblDynamicColumnCount.Text) - 1].Visible = false;

                    }
                }
            }

        }

        public void gvgpAcutalEStimate()
        {
            string Status;
            string NewStatus;
            string FinalPricettxt;
            string AdditionalPD;
            string SuggestedPriceWithPD;
            string LastSharedPrice;
            string EnumDescription;
            string TOD;
            string PD;
            string QD;
            string ProfitPer;
            datajs = "";
            foreach (GridViewRow row1 in gvgpactualestimate.Rows)
            {


                Status = (row1.FindControl("lblStatus") as Label).Text;
                NewStatus = (row1.FindControl("lblNewStatus") as Label).Text;

                EnumDescription = (row1.FindControl("lblEnumDescription") as Label).Text;
                TOD = (row1.FindControl("TODtxt") as TextBox).Text;
                PD = (row1.FindControl("PDtxt") as TextBox).Text;
                QD = (row1.FindControl("QDtxt") as TextBox).Text;
                ProfitPer = (row1.FindControl("ProfitPertxt") as TextBox).Text;
                if (row1.RowIndex == 0)
                    row1.Style.Add("height", "100px");
                lblDynamicColumnCount.Text = (row1.Cells.Count).ToString();




                if (EnumDescription.ToUpper() == "RPL" && NewStatus.ToUpper() == "RPL")
                {
                    datajs += "calculaterate('1','" + (row1.FindControl("PDtxt") as TextBox).ClientID + "','1');";

                }

                if (Status == "Estimate" && NewStatus == "NCR" && EnumDescription == "NCR")
                {
                    GridViewRow FinalRow = gvgpactualestimate.Rows[row1.RowIndex - 1];

                    //(FinalRow.FindControl("FinalPricettxt") as TextBox).Text = FinalPricettxt;
                    //(FinalRow.FindControl("SuggestedPriceWithPDttxt_lbl") as Label).Text = SuggestedPriceWithPD;
                    //(FinalRow.FindControl("lblLast_Shared_Final_Price") as Label).Text = LastSharedPrice;
                    //(FinalRow.FindControl("AdditionalPDtxt") as TextBox).Text = AdditionalPD;

                    GridViewRow FinalRow2 = gvgpactualestimate.Rows[row1.RowIndex - 2];

                    (FinalRow2.FindControl("ProfitPertxt") as TextBox).Enabled = true;
                    (FinalRow2.FindControl("QDtxt") as TextBox).Text = QD;
                    (FinalRow2.FindControl("PDtxt") as TextBox).Text = PD;
                    (FinalRow2.FindControl("TODtxt") as TextBox).Text = TOD;
                    (FinalRow2.FindControl("ProfitPertxt") as TextBox).Text = ProfitPer;

                    datajs += "calculaterate('1','" + (FinalRow2.FindControl("PDtxt") as TextBox).ClientID + "','1');";

                }
                if (Status == "Actual" && NewStatus == "NCR")
                {

                    (row1.FindControl("TODtxt") as TextBox).Attributes.Add("style", "display:none");
                    (row1.FindControl("QDtxt") as TextBox).Attributes.Add("style", "display:none");
                    (row1.FindControl("PDtxt") as TextBox).Attributes.Add("style", "display:none");
                    (row1.FindControl("lblNewStatus") as Label).Attributes.Add("style", "display:none");
                    (row1.FindControl("lblEnumDescription") as Label).Attributes.Add("style", "display:none");
                }
                if (Status == "Estimate" && NewStatus == "RPL")
                {
                    (row1.FindControl("TODtxt") as TextBox).Attributes.Add("style", "display:none");
                    (row1.FindControl("QDtxt") as TextBox).Attributes.Add("style", "display:none");
                    (row1.FindControl("PDtxt") as TextBox).Attributes.Add("style", "display:none");
                    (row1.FindControl("lblNewStatus") as Label).Attributes.Add("style", "display:none");
                    (row1.FindControl("lblEnumDescription") as Label).Attributes.Add("style", "display:none");

                }
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", datajs, true);

        }
        public void gvgpAcutalgrid()
        {
            string Status;
            string NewStatus;
            string EnumDescription;
            //hdnActual.Value = "Actual";

            foreach (GridViewRow row1 in gvgpactualestimate.Rows)
            {


                NewStatus = (row1.FindControl("lblNewStatus") as Label).Text;

                EnumDescription = (row1.FindControl("lblEnumDescription") as Label).Text;
                TextBox PDtxt = (row1.FindControl("PDtxt") as TextBox);


                if (EnumDescription.ToUpper() == "RPL" && NewStatus.ToUpper() == "RPL")
                {
                    //datajs += "calculaterate('1','" + (row1.FindControl("PDtxt") as TextBox).ClientID + "','1');";
                    datajs += "calculaterate_actual('0','" + (row1.FindControl("PDtxt") as TextBox).ClientID + "');";

                }
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", datajs, true);

        }


        protected void rdActualEstimate_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (rdActualEstimate.SelectedValue == "0")
            {
                hdngrdcalc.Value = "1";
                binddata(0);
                // Response.Redirect("~/PriceListGPActualEstimate.aspx?bpmId=" + t_bmpid + "&IsEstimate=" + t_isestimate + "&TradeId=" + t_tradeid + "&act=1");
            }
            else
            {
                hdngrdcalc.Value = "0";
                binddata(0);
                // Response.Redirect("~/PriceListGPActualEstimate.aspx?bpmId=" + t_bmpid + "&IsEstimate=" + t_isestimate + "&TradeId=" + t_tradeid);
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

        protected void btnupdateall_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (hdngrdcalc.Value == "1")
            {
                rdActualEstimate.Items[0].Selected = true;
                rdActualEstimate.Items[1].Selected = false;
            }
            else
            {
                rdActualEstimate.Items[0].Selected = false;
                rdActualEstimate.Items[1].Selected = true;
            }

            if (btn.ClientID != null)
            {
                //int rowno = Common.ConvertInt(btn.ClientID.Substring(btn.ClientID.LastIndexOf('_') + 1));
                GridViewRow row;
                string StateId;
                TextBox txtFinalPrice;
                TextBox txtAdditionalPD;
                string msgs = "";
                {
                    if (IsEstimate != "0")
                    {
                       // row = gvgpactualestimate.Rows[rowno];
                        

                            foreach (GridViewRow row1 in gvgpactualestimate.Rows)
                            {

                                Label lblFk_State_Id = (Label)row1.FindControl("lblFkStateId");

                                Label PriceListGPActualEStimateId = (Label)row1.FindControl("lblPriceListGPActualEstimateId");
                                StateId = Common.ConvertString((row1.FindControl("lblFkStateId") as Label).Text);
                                //if (lblFk_State_Id.Text == StateId)
                                //{
                                    Label lblFk_BPM_Id = (Label)row1.FindControl("lblFkBulkProductId");
                                    Label lblFkPriceTypeId = (Label)row1.FindControl("lblFkPriceTypeId");
                                    Label lblTradeName_Id = (Label)row1.FindControl("lblTradeId");
                                    Label lblPackingMaterialId = (Label)row1.FindControl("lblPackingMaterialId");
                                    Label lblStatus = (Label)row1.FindControl("lblStatus");

                                    Label lblEnumDescription = (Label)row1.FindControl("lblEnumDescription");


                                    TextBox txtTOD = (TextBox)row1.FindControl("TODtxt");
                                    TextBox txtPD = (TextBox)row1.FindControl("PDtxt");
                                    TextBox txtQD = (TextBox)row1.FindControl("QDtxt");
                                    TextBox txtProfitPer = (TextBox)row1.FindControl("ProfitPertxt");




                                    plgpdata.TOD = Common.ConvertDecimal(txtTOD.Text);
                                    plgpdata.PD = Common.ConvertDecimal(txtPD.Text);
                                    plgpdata.QD = Common.ConvertDecimal(txtQD.Text);
                                    plgpdata.ProfitPer = Common.ConvertDecimal(txtProfitPer.Text);

                                    if (lblStatus.Text == "Estimate" && lblEnumDescription.Text.ToUpper() == "NCR")
                                    {
                                        GridViewRow gptemp = gvgpactualestimate.Rows[row1.RowIndex - 1];

                                        txtFinalPrice = (TextBox)gptemp.FindControl("FinalPricettxt");
                                        txtAdditionalPD = (TextBox)gptemp.FindControl("AdditionalPDtxt");

                                        plgpdata.FinalPrice = Common.ConvertDecimal(txtFinalPrice.Text);
                                        plgpdata.AdditionalPD = Common.ConvertDecimal(txtAdditionalPD.Text);
                                    }
                                    else
                                    {
                                        if (lblEnumDescription.Text.ToUpper() == "NCR")
                                        {
                                            GridViewRow gptemp = gvgpactualestimate.Rows[row1.RowIndex - 1];

                                            txtFinalPrice = (TextBox)gptemp.FindControl("FinalPricettxt");
                                            txtAdditionalPD = (TextBox)gptemp.FindControl("AdditionalPDtxt");

                                            plgpdata.FinalPrice = Common.ConvertDecimal(txtFinalPrice.Text);
                                            plgpdata.AdditionalPD = Common.ConvertDecimal(txtAdditionalPD.Text);
                                        }
                                        else
                                        {

                                            txtFinalPrice = (TextBox)row1.FindControl("FinalPricettxt");
                                            txtAdditionalPD = (TextBox)row1.FindControl("AdditionalPDtxt");
                                            plgpdata.FinalPrice = Common.ConvertDecimal(txtFinalPrice.Text);
                                            plgpdata.AdditionalPD = Common.ConvertDecimal(txtAdditionalPD.Text);
                                        }
                                    }


                                    plgpdata.FkPackingMaterialId = Common.ConvertInt(lblPackingMaterialId.Text);
                                    plgpdata.Status = lblStatus.Text;
                                    plgpdata.FkStateId = Common.ConvertInt(lblFk_State_Id.Text);
                                    plgpdata.FkBulkProductId = Common.ConvertInt(lblFk_BPM_Id.Text);
                                    plgpdata.FkPriceTypeId = Common.ConvertInt(lblFkPriceTypeId.Text);
                                    plgpdata.FkTradeId = Common.ConvertInt(Request.QueryString["TradeId"]);

                                    plgpdata.EstimateId = Common.ConvertInt(Session["EstimateId"]);
                                    plgpdata.UserId = Common.ConvertInt(Session["UserId"]);

                                    plgpdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
                                    if (Common.ConvertInt(PriceListGPActualEStimateId.Text) == 0)
                                    {
                                        plgpdata.action = 1;
                                    }
                                    else
                                    {
                                        plgpdata.PriceListGPActualEstimateId = Common.ConvertInt(PriceListGPActualEStimateId.Text);

                                        plgpdata.action = 2;
                                    }
                                    ReturnMessage obj = null;
                                    obj = plgp.InsertUpdatePriceListGPActualEstimate(plgpdata);
                                    string msg = msgs = Common.ConvertString(obj.Message);

                                //}

                            }


                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "');", true);

                            binddata(0);

                        

                    }
                    else
                    {
                        //row = gvpricelistactual.Rows[rowno];
                       

                            foreach (GridViewRow row1 in gvpricelistactual.Rows)
                            {

                                Label lblFk_State_Id = (Label)row1.FindControl("lblFkStateId");

                                Label PriceListGPActualEStimateId = (Label)row1.FindControl("lblPriceListGPActualEstimateId");
                                StateId = Common.ConvertString((row1.FindControl("lblFkStateId") as Label).Text);
                                //if (lblFk_State_Id.Text == StateId)
                                //{
                                    Label lblFk_BPM_Id = (Label)row1.FindControl("lblFkBulkProductId");
                                    Label lblFkPriceTypeId = (Label)row1.FindControl("lblFkPriceTypeId");
                                    Label lblTradeName_Id = (Label)row1.FindControl("lblTradeName_Id");
                                    Label lblPackingMaterialId = (Label)row1.FindControl("lblPackingMaterialId");
                                    Label lblStatus = (Label)row1.FindControl("lblStatus");


                                    TextBox txtTOD = (TextBox)row1.FindControl("TODtxt");
                                    TextBox txtPD = (TextBox)row1.FindControl("PDtxt");
                                    TextBox txtQD = (TextBox)row1.FindControl("QDtxt");
                                    TextBox txtProfitPer = (TextBox)row1.FindControl("ProfitPertxt");



                                    plgpdata.TOD = Common.ConvertDecimal(txtTOD.Text);
                                    plgpdata.PD = Common.ConvertDecimal(txtPD.Text);
                                    plgpdata.QD = Common.ConvertDecimal(txtQD.Text);
                                    plgpdata.ProfitPer = Common.ConvertDecimal(txtProfitPer.Text);
                                    txtFinalPrice = (TextBox)row1.FindControl("FinalPricettxt");
                                    txtAdditionalPD = (TextBox)row1.FindControl("AdditionalPDtxt");
                                    plgpdata.FinalPrice = Common.ConvertDecimal(txtFinalPrice.Text);
                                    plgpdata.AdditionalPD = Common.ConvertDecimal(txtAdditionalPD.Text);
                                    plgpdata.FkPackingMaterialId = Common.ConvertInt(lblPackingMaterialId.Text);
                                    plgpdata.FkStateId = Common.ConvertInt(lblFk_State_Id.Text);
                                    plgpdata.FkBulkProductId = Common.ConvertInt(lblFk_BPM_Id.Text);
                                    plgpdata.FkPriceTypeId = Common.ConvertInt(lblFkPriceTypeId.Text);
                                    plgpdata.FkTradeId = Common.ConvertInt(Request.QueryString["TradeId"]);

                                    plgpdata.EstimateId = Common.ConvertInt(Session["EstimateId"]);
                                    plgpdata.UserId = Common.ConvertInt(Session["UserId"]);

                                    if (plgpdata.FkPriceTypeId == 8)
                                    {
                                        plgpdata.Status = "Actual";
                                    }
                                    else
                                    {
                                        plgpdata.Status = "Estimate";

                                    }


                                    plgpdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
                                    if (Common.ConvertInt(PriceListGPActualEStimateId.Text) == 0)
                                    {
                                        plgpdata.action = 1;
                                    }
                                    else
                                    {
                                        plgpdata.PriceListGPActualEstimateId = Common.ConvertInt(PriceListGPActualEStimateId.Text);

                                        plgpdata.action = 2;
                                    }

                                    ReturnMessage obj = null;
                                    obj = plgp.InsertUpdatePriceListGPActualEstimate(plgpdata);
                                    string msg = msgs = Common.ConvertString(obj.Message);

                                //}
                            }
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "');", true);

                            binddata(0);


                        

                    }


                }


            }
        }
    }
}
