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
    public partial class ReportTransportingCosting : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        TrasportationCostMasterDAL tcm = new TrasportationCostMasterDAL();
        TrasportationCostMasterBAL tcmdata = new TrasportationCostMasterBAL();
        TrasportationCostFactorBAL tcfdata = new TrasportationCostFactorBAL();
        int StateId;
        int TransportationCostId;
        int CompanyId;
        int UserId;
        int IsMasterpack = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            StateId = Common.ConvertInt(Session["StateId"]);
            TransportationCostId = Common.ConvertInt(Session["TransportationCostId"]);
            CompanyId = Common.ConvertInt(Session["CompanyId"]);
            UserId = Common.ConvertInt(Session["UserId"]);
            IsMasterpack = Common.ConvertInt(drpismasterpack.SelectedValue);
            binddata();
        }
        private void binddata()
        {
            DataTable dt = tcm.Get_ReportTransportationFactor(Common.ConvertInt(Session["UserId"]), Common.ConvertInt(Session["TransportationCostId"]), Common.ConvertInt(Session["CompanyId"]), Common.ConvertInt(Session["StateId"]), 1);
            if (dt.Rows.Count > 0)
            {
                lblStateName.Text = Common.ConvertString(dt.Rows[0]["StateName"]);
                gvreport.DataSource = dt;
                gvreport.DataBind();
                if (dt.Rows.Count > 0)
                {

                    gvreport.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvreport.UseAccessibleHeader = true;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "Report Not Found !", true);

            }
        }


        //protected void drpismasterpack_SelectedIndexChanged(object sender, EventArgs e)
        //{
  
        //    DataTable dt = tcm.Get_ReportTransportationFactor(UserId, TransportationCostId, CompanyId, StateId, IsMasterpack);
        //    if (dt.Rows.Count>0)
        //    {
        //        lblStateName.Text = Common.ConvertString(dt.Rows[0]["StateName"]);
        //        gvreport.DataSource = dt;
        //        gvreport.DataBind();
        //        if (dt.Rows.Count > 0)
        //        {

        //            gvreport.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            gvreport.UseAccessibleHeader = true;
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "Report Not Found !", true);

        //    }

        //}
    }
}