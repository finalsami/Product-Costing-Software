using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Production_Costing_Software
{
    public partial class FinishGoodsPricingReport : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        FinishGoodsPricingReportDAL FG = new FinishGoodsPricingReportDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        private void BindData()
        {
            DataTable dt = FG.FinishedGoodReport(Common.ConvertInt(Session["UserId"]));

            gvfinishgood.DataSource = dt;
            gvfinishgood.DataBind();
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
        protected void btnreport_Click1(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int ProductId = Common.ConvertInt(btn.CommandArgument);
            if (ProductId > 0)
            {

                string html = "";
                string bulk = "";

                Common.GetFinishedGood(Common.ConvertInt(Session["UserId"]), ProductId, out html, out bulk);

                exampleModalLabel.InnerHtml = bulk;

                dvdetailcontent.InnerHtml = html;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "showhidemodel('1');", true);

               
            }
        }
    }
}