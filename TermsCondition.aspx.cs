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
    public partial class TermsCondition :BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        TermsConditionDAL tc = new TermsConditionDAL();
        TermsConditionBAL tcdata = new TermsConditionBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }
        private void binddata()
        {

            DataTable dt = tc.TermsConditionList(Common.ConvertInt(Session["UserId"]), 0);
            gvtermscondition.DataSource = dt;
            gvtermscondition.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvtermscondition.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvtermscondition.UseAccessibleHeader = true;
            }

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateTermsCondition(2, 0);
            }
        }
        private void InsertUpdateTermsCondition(int act, int TermsconditionId)
        {

            if (act == 3)
            {
                tcdata.TermsconditionId = Common.ConvertString(TermsconditionId);
                tcdata.action = act;
                tcdata.UserId = Common.ConvertInt(Session["UserId"]);
                tcdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
                tcdata.TermsCondition = "";

            }
            else if (act == 1)
            {
              
                {
                    tcdata.TermsconditionId = Common.ConvertString(hdntcid.Value);
                    tcdata.action = act;

                    tcdata.TermsCondition = Common.ConvertString(txttermscondition.Text);
                    tcdata.UserId = Common.ConvertInt(Session["UserId"]);
                    tcdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);


                }
            }
            else
            {
                tcdata.TermsconditionId = Common.ConvertString(hdntcid.Value);
                tcdata.action = act;
                tcdata.TermsCondition = Common.ConvertString(txttermscondition.Text);
                tcdata.UserId = Common.ConvertInt(Session["UserId"]);
                tcdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);

            }
            ReturnMessage obj = tc.InsertUpdateTermsCondition(tcdata);
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
            tcdata = new TermsConditionBAL();
            hdntcid.Value = "0";
            txttermscondition.Text = "";

        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateTermsCondition(1, 0);
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
            int TermsconditionId = Common.ConvertInt(btn.CommandArgument);
            if (TermsconditionId > 0)
            {
                DataTable dt = tc.TermsConditionList(Common.ConvertInt(Session["UserId"]), TermsconditionId);
                if (dt.Rows.Count > 0)
                {
                    hdntcid.Value = Common.ConvertString(dt.Rows[0]["TermsconditionId"]);
                    txttermscondition.Text = Common.ConvertString(dt.Rows[0]["Termscondition"]);


                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int TermsconditionId = Common.ConvertInt(btn.CommandArgument);
            if (TermsconditionId > 0)
            {
                InsertUpdateTermsCondition(3, TermsconditionId);
            }
        }
    }
}