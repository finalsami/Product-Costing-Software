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
    public partial class TradeBulkMapping :BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();

        TradeBulkMappingBAL trade = new TradeBulkMappingBAL();
        TradeBulkMappingDAL tradedata = new TradeBulkMappingDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                binddata();
                binddropdown();
                lblcompany.InnerHtml = Common.ConvertString(Session["CompanyName"]);
            }
        }

        private void binddropdown()
        {
            //DataTable dttrade = common.DropdownList("Trade",Common.ConvertString(Session["CompanyId"]), "");

            //drptrade.DataSource = dttrade;
            //drptrade.DataTextField = "Name";
            //drptrade.DataValueField = "Id";
            //drptrade.DataBind();

            DataTable dtrm = common.DropdownList("BulkMappingselect", Common.ConvertString(Session["CompanyId"]), "");

            drpproduct.DataSource = dtrm;
            drpproduct.DataTextField = "Name";
            drpproduct.DataValueField = "Id";
            drpproduct.DataBind();
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                InsertUpdateTradeBulkMapping(1, 0);
            }
        }
        private void binddata()
        {
            DataTable dt = tradedata.BulkTradeMapping(Common.ConvertInt(Session["CompanyId"]));
            gvbulk.DataSource = dt;
            gvbulk.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvbulk.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvbulk.UseAccessibleHeader = true;
            }
        }
        private void cleardata()
        {
            drptrade.ClearSelection();
            drpproduct.ClearSelection();
            drptrade.Items.Clear();
        }
        private void InsertUpdateTradeBulkMapping(int act, int TradeBulkMappingId)
        {
            trade.UserId = Common.ConvertInt(Session["UserId"]);
            trade.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);

            if (act == 3)
            {
                trade.TradeBulkMappingId = Common.ConvertInt(TradeBulkMappingId);
                trade.FkTradeId = "";
                trade.action = act;
                trade.FkCompanyId = 0;
                trade.FkBulkProductId = Common.ConvertInt(drpproduct.SelectedValue);
            }
            else if (act == 1)
            {

                trade.TradeBulkMappingId = 0;
                trade.FkBulkProductId = Common.ConvertInt(drpproduct.SelectedValue);
                trade.action = act;

                string lst = Common.ConvertString(Request.Form[drptrade.UniqueID]);
                trade.FkTradeId = lst.Length > 0 ? lst : "";


            }

            ReturnMessage obj = tradedata.InsertBulkTradeMapping(trade);
            string msg = Common.ConvertString(obj.Message);
            if (Common.ConvertInt(obj.ReturnValue) > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                cleardata();
                binddata();
                binddropdown();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);

            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int TradeBulkMappingId = Common.ConvertInt(btn.CommandArgument);
            if (TradeBulkMappingId > 0)
            {
                InsertUpdateTradeBulkMapping(3, TradeBulkMappingId);
            }
        }

        protected void drptrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            drptrade.ClearSelection();
            if(Common.ConvertInt(drpproduct.SelectedValue)>0)
            {
                DataTable dttrade = common.DropdownList("Trademapping", Common.ConvertString(Session["CompanyId"]), Common.ConvertString(drpproduct.SelectedValue));
                drptrade.DataSource = dttrade;
                drptrade.DataTextField = "Name";
                drptrade.DataValueField = "Id";
                drptrade.DataBind();
            }
            else
            {
                drptrade.Items.Clear();
            }
           // binddata();
        }
    }
}