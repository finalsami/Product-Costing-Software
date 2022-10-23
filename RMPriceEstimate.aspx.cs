using BAL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class RMPriceEstimate : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        RMPriceEstimateBAL rmpmdata = new RMPriceEstimateBAL();
        RMPriceEstimateDAL rmpm = new RMPriceEstimateDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddropdown();
                binddata();
            }
        }
        private void binddata()
        {

            DataTable dt = rmpm.Get_RMPriceEstimateMaster(0);
            gvrmpriceestimate.DataSource = dt;
            gvrmpriceestimate.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvrmpriceestimate.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvrmpriceestimate.UseAccessibleHeader = true;
            }

        }

        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("RMCategory", "", "");

            drprmcategory.DataSource = dtrm;
            drprmcategory.DataTextField = "Name";
            drprmcategory.DataValueField = "Id";
            drprmcategory.DataBind();
        }
        private void InsertUpdateRMPriceEstimateMaster(int act, int RMPriceEstimateId)
        {
            rmpmdata.UserId = Common.ConvertInt(Session["UserId"]);

            if (act == 3)
            {
                rmpmdata.RMPriceEstimateId = Common.ConvertInt(RMPriceEstimateId);
                rmpmdata.action = act;

            }
            else
            {
                rmpmdata.RMPriceEstimateId = Common.ConvertInt(hdrmpemid.Value);
                rmpmdata.action = act;
                rmpmdata.PurchaseDate = Common.ConvertString(txtdop.Text);             
                rmpmdata.EstimatePrice = Common.ConvertDecimal(txtratekgltr.Text);
                rmpmdata.FkRMPriceId = Common.ConvertInt(lblRMPriceId.Text);

            }
            ReturnMessage obj = rmpm.InsertUpdateRMPriceEstimateMaster(rmpmdata);
            string msg = Common.ConvertString(obj.Message);
            if (Common.ConvertInt(obj.ReturnValue) > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                cleardata();

                btnadd.Visible = true;
                btnupdate.Visible = false;

                binddata();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);

            }
        }
        private void cleardata()
        {
            rmpmdata = new RMPriceEstimateBAL();
            hdrmpemid.Value = "0";
            txtdop.Text = "";
            txtpuritypercent.Text = "";
            txtratekgltr.Text = "";
            txtquantity.Text = "";
            txtactualprice.Text = "";
            txttransport.Text = "";
            txttotalrateperltr.Text = "";
            drprmcategory.ClearSelection();
            drprmname.Items.Clear();
            drprmname.ClearSelection();
            chkpurity.Checked = false;
            lblActualRatePerKg.Text = "";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMPriceEstimateId = Common.ConvertInt(btn.CommandArgument);
            if (RMPriceEstimateId > 0)
            {
                InsertUpdateRMPriceEstimateMaster(3, RMPriceEstimateId);
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateRMPriceEstimateMaster(1, 0);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateRMPriceEstimateMaster(2, 0);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMPriceEstimateId = Common.ConvertInt(btn.CommandArgument);
            if (RMPriceEstimateId > 0)
            {
                DataTable dt = rmpm.Get_RMPriceEstimateMaster(RMPriceEstimateId);
                if (dt.Rows.Count > 0)
                {
                    hdrmpemid.Value = Common.ConvertString(dt.Rows[0]["RMPriceEstimateId"]);

                    if (drprmcategory.Items.FindByValue(Common.ConvertString(dt.Rows[0]["RMCategoryId"])) != null)
                    {
                        drprmcategory.SelectedValue = Common.ConvertString(dt.Rows[0]["RMCategoryId"]);
                        DataTable dtrm = common.DropdownList("RMMasterByRMCategory", drprmcategory.SelectedValue.ToString(), "");
                        drprmname.DataSource = dtrm;
                        drprmname.DataTextField = "Name";
                        drprmname.DataValueField = "Id";
                        drprmname.DataBind();
                    }

                    if (drprmname.Items.FindByValue(Common.ConvertString(dt.Rows[0]["RMId"])) != null)
                    {
                        drprmname.SelectedValue = Common.ConvertString(dt.Rows[0]["RMId"]);
                    }

                    txtdop.Text = DateTime.Parse(dt.Rows[0]["PurchaseDate1"].ToString()).ToString("yyyy-MM-dd");
                    chkpurity.Checked = Common.ConvertBool(dt.Rows[0]["IsPurity"]);
                    txtratekgltr.Text = Common.ConvertString(dt.Rows[0]["RateKgLtr"]);
                    txtquantity.Text = Common.ConvertString(dt.Rows[0]["Quantity"]);
                    txtpuritypercent.Text = Common.ConvertString(dt.Rows[0]["PurityPercentage"]);
                    txttransport.Text = Common.ConvertString(dt.Rows[0]["TransporationRate"]);
                    txtactualprice.Text = Common.ConvertString(dt.Rows[0]["ActualPrice"]);
                    txttotalrateperltr.Text = Common.ConvertString(dt.Rows[0]["Total"]);
                    lblRMPriceId.Text = Common.ConvertString(dt.Rows[0]["FkRMPriceId"]);
                    lblActualRatePerKg.Text = Common.ConvertString(dt.Rows[0]["RMPriceRateKgLtr"]);
                    


                    btnadd.Visible = false;
                    btnupdate.Visible = true;

                }
            }
        }
    }
}