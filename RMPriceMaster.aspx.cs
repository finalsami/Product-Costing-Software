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
    public partial class RMPriceMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        RMPriceMasterBAL rmpmdata = new RMPriceMasterBAL();
        RMPriceMasterDAL rmpm  = new RMPriceMasterDAL();
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

            DataTable dt = rmpm.Get_RMPriceMaster(0);
            gvrmpricemaster.DataSource = dt;
            gvrmpricemaster.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvrmpricemaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvrmpricemaster.UseAccessibleHeader = true;
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
            protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateRMPriceMaster(2, 0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateRMPriceMaster(1, 0);
            }
        }
        private void InsertUpdateRMPriceMaster(int act, int RMPriceId)
        {
            rmpmdata.UserId = Common.ConvertInt(Session["UserId"]);

            if (act == 3)
            {
                rmpmdata.RMPriceId = Common.ConvertInt(RMPriceId);
                rmpmdata.action = act;

            }
            else
            {
                rmpmdata.RMPriceId = Common.ConvertInt(hdrmpmid.Value);
                rmpmdata.action = act;
                rmpmdata.PurchaseDate = Common.ConvertString(txtdop.Text);
                rmpmdata.FkRMCategoryId = Common.ConvertInt(drprmcategory.SelectedValue);
                rmpmdata.FkRMId = Common.ConvertInt(hdnRMid.Value);
                rmpmdata.RateKgLtr = Common.ConvertDecimal(txtratekgltr.Text);
                rmpmdata.Quantity = Common.ConvertInt(txtquantity.Text);
                rmpmdata.PurityPercentage = Common.ConvertDecimal(txtpuritypercent.Text);
                rmpmdata.TransporationRate = Common.ConvertDecimal(txttransport.Text);
                rmpmdata.IsPurity = Common.ConvertBool(chkpurity.Checked);

            }
            ReturnMessage obj = rmpm.InsertUpdateRMPriceMaster(rmpmdata);
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
            rmpmdata = new RMPriceMasterBAL();
            hdrmpmid.Value = "0";
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
            hdnRMid.Value = "0";
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMPriceId = Common.ConvertInt(btn.CommandArgument);
            if (RMPriceId > 0)
            {
                DataTable dt = rmpm.Get_RMPriceMaster(RMPriceId);
                if (dt.Rows.Count > 0)
                {
                    hdrmpmid.Value = Common.ConvertString(dt.Rows[0]["RMPriceId"]);

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
                        hdnRMid.Value = drprmname.SelectedValue;
                    }

                    txtdop.Text = DateTime.Parse(dt.Rows[0]["PurchaseDate1"].ToString()).ToString("yyyy-MM-dd");
                    chkpurity.Checked = Common.ConvertBool(dt.Rows[0]["IsPurity"]);
                    txtratekgltr.Text = Common.ConvertString(dt.Rows[0]["RateKgLtr"]);
                    txtquantity.Text = Common.ConvertString(dt.Rows[0]["Quantity"]);
                    txtpuritypercent.Text = Common.ConvertString(dt.Rows[0]["PurityPercentage"]);
                    txttransport.Text = Common.ConvertString(dt.Rows[0]["TransporationRate"]);
                    txtactualprice.Text = Common.ConvertString(dt.Rows[0]["ActualPrice"]);
                    txttotalrateperltr.Text = Common.ConvertString(dt.Rows[0]["Total"]);

                   

                    btnadd.Visible = false;
                    btnupdate.Visible = true;

                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMPriceId = Common.ConvertInt(btn.CommandArgument);
            if (RMPriceId > 0)
            {
                InsertUpdateRMPriceMaster(3, RMPriceId);
            }
        }
    }
}