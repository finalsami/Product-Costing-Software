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

    public partial class CompanyMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        CompanyMasterDAL comp = new CompanyMasterDAL();
        CompanyMasterBAL compdata = new CompanyMasterBAL();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                binddata();


            }
        }
        private void binddata()
        {

            DataTable dt = comp.GetCompanyMaster(Common.ConvertInt(Session["UserId"]), 0);
            gvcompany.DataSource = dt;
            gvcompany.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvcompany.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvcompany.UseAccessibleHeader = true;
            }

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int CompanyId = Common.ConvertInt(btn.CommandArgument);
            if (CompanyId > 0)
            {
                DataTable dt = comp.GetCompanyMaster(Common.ConvertInt(Session["UserId"]), CompanyId);
                if (dt.Rows.Count > 0)
                {
                    hdncompid.Value = Common.ConvertString(dt.Rows[0]["CompanyId"]);
                    txtcname.Text = Common.ConvertString(dt.Rows[0]["CompanyName"]);
                    if (Common.ConvertBool(dt.Rows[0]["IsPackingMaster"]))
                    {
                        rdotype.Items[1].Selected = true;
                    }
                    else
                    {
                        rdotype.Items[0].Selected = true;
                    }
                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int CompanyId = Common.ConvertInt(btn.CommandArgument);
            if (CompanyId > 0)
            {
                InsertUpdateCompanyMaster(3, CompanyId);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateCompanyMaster(2, 0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateCompanyMaster(1, 0);
            }
        }
        private void InsertUpdateCompanyMaster(int act, int CompanyId)
        {

            if (act == 3)
            {
                compdata.CompanyId = Common.ConvertInt(CompanyId);
                compdata.action = act;
                compdata.UserId = Common.ConvertInt(Session["UserId"]);
                compdata.CompanyName = "";
            }
            else if (act == 1)
            {
                string CompanyName = Common.ConvertString(txtcname.Text.Trim());
                ReturnMessage objs = common.CheckExist("Company", CompanyName, "", "");
                string msgs = Common.ConvertString(objs.Message);

                if (Common.ConvertInt(objs.ReturnValue) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                    return;

                }
                else
                {
                    compdata.CompanyId = Common.ConvertInt(hdncompid.Value);
                    compdata.action = act;
                    compdata.CompanyName = Common.ConvertString(txtcname.Text);
                    compdata.IsPackingMaster = rdotype.Items[1].Selected;
                    compdata.UserId = Common.ConvertInt(Session["UserId"]);

                }
            }
            else
            {
                {
                    compdata.CompanyId = Common.ConvertInt(hdncompid.Value);
                    compdata.action = act;
                    compdata.CompanyName = Common.ConvertString(txtcname.Text);
                    compdata.IsPackingMaster = rdotype.Items[1].Selected;
                    compdata.UserId = Common.ConvertInt(Session["UserId"]);

                }
            }
            ReturnMessage obj = comp.InsertUpdateCompanyMaster(compdata);
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
            compdata = new CompanyMasterBAL();
            hdncompid.Value = "0";
            rdotype.Items[0].Selected = true;
            txtcname.Text = "";

        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }


    }
}