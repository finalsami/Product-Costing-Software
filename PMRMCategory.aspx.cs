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
    public partial class PMRMCategory : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        PMRMCategoryDAL pmrm = new PMRMCategoryDAL();
        PMRMCategoryBAL pmrmcdata = new PMRMCategoryBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }
        private void binddata()
        {

            DataTable dt = pmrm.PMRMCategoryList(Common.ConvertInt(Session["UserId"]), 0);
            gvpmrmcategory.DataSource = dt;
            gvpmrmcategory.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvpmrmcategory.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvpmrmcategory.UseAccessibleHeader = true;
            }

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PMRMCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (PMRMCategoryId > 0)
            {
                InsertUpdatePMRMCategory(3, PMRMCategoryId);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PMRMCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (PMRMCategoryId > 0)
            {
                DataTable dt = pmrm.PMRMCategoryList(Common.ConvertInt(Session["UserId"]), PMRMCategoryId);
                if (dt.Rows.Count > 0)
                {
                    hdnpmrmcid.Value = Common.ConvertString(dt.Rows[0]["PMRMCategoryId"]);
                    txtpmrmcategory.Text = Common.ConvertString(dt.Rows[0]["PMRMCategoryName"]);
                    if (Common.ConvertBool(dt.Rows[0]["ChkIsShipper"]))
                    {
                        rdotype.Items[1].Selected = true;
                    }
                    else if (Common.ConvertBool(dt.Rows[0]["ChkIsInner"]))
                    {
                        rdotype.Items[2].Selected = true;
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

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePMRMCategory(2, 0);
            }
        }
        private void InsertUpdatePMRMCategory(int act, int RMCategoryId)
        {

            if (act == 3)
            {
                pmrmcdata.PMRMCategoryId = Common.ConvertInt(RMCategoryId);
                pmrmcdata.action = act;
                pmrmcdata.UserId = Common.ConvertInt(Session["UserId"]);

                pmrmcdata.PMRMCategoryName = "";
                pmrmcdata.ChkIsShipper = rdotype.Items[1].Selected;
                pmrmcdata.ChkIsInner = rdotype.Items[2].Selected;
            }
            else if (act == 1)
            {
                string PMRMCategory = Common.ConvertString(txtpmrmcategory.Text.Trim());
                ReturnMessage objs = common.CheckExist("PMRMCategory", PMRMCategory, "", "");
                string msgs = Common.ConvertString(objs.Message);

                if (Common.ConvertInt(objs.ReturnValue) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                    return;

                }

                else
                {
                    pmrmcdata.PMRMCategoryId = Common.ConvertInt(hdnpmrmcid.Value);
                    pmrmcdata.action = act;

                    pmrmcdata.PMRMCategoryName = Common.ConvertString(txtpmrmcategory.Text);
                    pmrmcdata.UserId = Common.ConvertInt(Session["UserId"]);


                }
            }
            else
            {
                pmrmcdata.PMRMCategoryId = Common.ConvertInt(hdnpmrmcid.Value);
                pmrmcdata.action = act;
                pmrmcdata.ChkIsShipper = rdotype.Items[1].Selected;
                pmrmcdata.ChkIsInner = rdotype.Items[2].Selected;
                pmrmcdata.PMRMCategoryName = Common.ConvertString(txtpmrmcategory.Text);
                pmrmcdata.UserId =  Common.ConvertInt(Session["UserId"]);

            }
            ReturnMessage obj = pmrm.InsertUpdatePMRMCategory(pmrmcdata);
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
            pmrmcdata = new PMRMCategoryBAL();
            hdnpmrmcid.Value = "0";
            txtpmrmcategory.Text = "";
            rdotype.Items[0].Selected = true;

        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePMRMCategory(1, 0);
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }
    }
}