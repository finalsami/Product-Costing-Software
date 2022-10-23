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
    public partial class TradeMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        TradeDAL tr = new TradeDAL();
        TradeBAL trdata = new TradeBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
                lblcompany.InnerHtml = Common.ConvertString(Session["CompanyName"]);
            }
        }
        private void binddata()
        {

            DataTable dt = tr.TradeList(Common.ConvertInt(Session["UserId"]), 0,Common.ConvertInt(Session["CompanyId"]));
            gvtrade.DataSource = dt;
            gvtrade.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvtrade.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvtrade.UseAccessibleHeader = true;
            }

        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateTrade(2, 0);
            }
        }
        private void InsertUpdateTrade(int act, int TradeId)
        {
            trdata.UserId = Common.ConvertInt(Session["UserId"]);
            trdata.FkcompanyId = Common.ConvertInt(Session["CompanyId"]);

            if (act == 3)
            {
                trdata.TradeId = Common.ConvertInt(TradeId);
                trdata.action = act;
                trdata.TradeName = "";

            }
            else if (act == 1)
            {

                trdata.TradeId = Common.ConvertInt(hdnmcid.Value);
                trdata.action = act;
                trdata.TradeName = Common.ConvertString(txttrade.Text);
            }
            else
            {
                trdata.TradeId = Common.ConvertInt(hdnmcid.Value);
                trdata.action = act;
                trdata.TradeName = Common.ConvertString(txttrade.Text);
               
            }
            ReturnMessage obj = tr.InsertUpdateTrade(trdata);
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
            trdata = new TradeBAL();
            hdnmcid.Value = "0";
            txttrade.Text = "";

        }
        protected void btnadd_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                InsertUpdateTrade(1, 0);
            }
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
            int tradeid = Common.ConvertInt(btn.CommandArgument);
            if (tradeid > 0)
            {
                DataTable dt = tr.TradeList(Common.ConvertInt(Session["UserId"]), tradeid, Common.ConvertInt(Session["CompanyId"]));
                if (dt.Rows.Count > 0)
                {
                    hdnmcid.Value = Common.ConvertString(dt.Rows[0]["TradeId"]);
                    txttrade.Text = Common.ConvertString(dt.Rows[0]["TradeName"]);


                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int tradeid = Common.ConvertInt(btn.CommandArgument);
            if (tradeid > 0)
            {
                InsertUpdateTrade(3, tradeid);
            }
        }
    }
}