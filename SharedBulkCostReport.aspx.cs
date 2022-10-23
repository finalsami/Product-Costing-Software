using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Production_Costing_Software
{
    public partial class SharedBulkCostReport : BasePage
    {
        TermsConditionDAL TC = new TermsConditionDAL();
        string SharedId = "";
        string bpmno = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SharedId = Common.ConvertString(Request.QueryString["SharedId"]);
                bpmno = Common.ConvertString(Request.QueryString["bpmno"]);


                BindData();
            }
        }
        private void BindData()
        {
            ShowHistory();
        }
        private string ShowHistory()
        {
            StringBuilder sbdata = new StringBuilder();
            //StringBuilder sbTerms = new StringBuilder();
            string sbTerms = "";

            string html = "";
            DataSet ds = TC.BulkCostSelectedReportByIdBPM(bpmno, SharedId);

            if (ds.Tables[0].Rows.Count > 0)
            {

                sbdata.Append("<table  border='1' ><tbody>");
                sbdata.Append("<tr><th  colspan='2'><i style=font-size:18px;margin-left:4px'>Report To Share : </i> <i style='font-size:20px;color:blue'>" + Common.ConvertString(ds.Tables[0].Rows[0]["ShareName"]) + "</i></th>");
                //sbdata.Append("</tr>");
                //sbdata.Append("<tr><th style='height:20px' colspan='2'></th>");
                //sbdata.Append("</tr>");
                sbdata.Append("<tr><th style='text-align:left'><p style='font-size:18px;margin-left:4px'> Date</p></th><th '><p>" + DateTime.Now.Date.ToShortDateString() + "</p></th></tr>");
                sbdata.Append("<tr><th style='text-align:left'><p style='font-size:18px;margin-left:4px'> Technical Name</p></th><th style='margin-left:4px'><p>" + Common.ConvertString(ds.Tables[0].Rows[0]["BulkProductName"]) + "</p></th></tr>");
                sbdata.Append("<tr><th style='text-align:left'><p style='font-size:18px;margin-left:4px'> Packing Type</p></th><th style='margin-left:4px'><p>" + Common.ConvertString(ds.Tables[0].Rows[0]["PMRMName"]) + "<p/></th></tr>");
                sbdata.Append("<tr><th style='text-align:left'><p style='font-size:18px;margin-left:4px'> Packing Size</p></th><th style='margin-left:4px'><p>" + Common.ConvertString(ds.Tables[0].Rows[0]["Packingsize"]) + "</p></th></tr>");
                sbdata.Append("<tr><th style='text-align:left'><p style='font-size:18px;margin-left:4px'> Price / L or Kg</p></th><th style='margin-left:4px'><p>" + Common.ConvertString(ds.Tables[0].Rows[0]["FinalPrice"]) + "</p></th></tr>");
            }
            sbdata.Append("<tr><th colspan='2'><p style='font-size:18px;margin-top:3px'>Terms & Condition</p></th>");
            sbdata.Append("</tr>");

            //sbTerms.Append("<tr><th ><p>Terms & Condition</p></th>");
            sbdata.Append("<tr style='border-bottom:white'>");

            for (int r = 0; r < ds.Tables[1].Rows.Count; r++)
            {
                sbdata.Append("<th colspan='2' style='text-align:left!important;'>" + Common.ConvertString(ds.Tables[1].Rows[r]["No"]) + ". " + Common.ConvertString(ds.Tables[1].Rows[r]["TermsCondition"]) + "</th>");
                sbdata.Append("</tr>");
                sbdata.Append("<tr style='border-bottom:white'>");


                sbTerms += " " + Common.ConvertString(ds.Tables[1].Rows[r]["No"]) + " ." + Common.ConvertString(ds.Tables[1].Rows[r]["TermsCondition"]) + "%0a";

            }

            sbdata.Append("</thead></tbody></table>");


            dvdetailcontentOnlyTable.InnerHtml = sbdata.ToString();

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "showhidemodel('1');", true);
            return sbdata.ToString();

        }
    }
}
