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
    public partial class RMCategory : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        RMCategoryDAL rmc = new RMCategoryDAL();
        RMCategoryBAL rmcdata = new RMCategoryBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }
        private void binddata()
        {

            DataTable dt = rmc.RMCategoryList(Common.ConvertInt(Session["UserId"]), 0);
            gvrmcategory.DataSource = dt;
            gvrmcategory.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvrmcategory.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvrmcategory.UseAccessibleHeader = true;
            }

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (RMCategoryId > 0)
            {
                InsertUpdateRMCategory(3, RMCategoryId);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (RMCategoryId > 0)
            {
                DataTable dt = rmc.RMCategoryList(Common.ConvertInt(Session["UserId"]), RMCategoryId);
                if (dt.Rows.Count > 0)
                {
                    hdnmcid.Value = Common.ConvertString(dt.Rows[0]["RMCategoryId"]);
                    txtrmcategory.Text = Common.ConvertString(dt.Rows[0]["RMCategoryName"]);


                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
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
                InsertUpdateRMCategory(1, 0);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateRMCategory(2, 0);
            }
        }
        private void InsertUpdateRMCategory(int act, int RMCategoryId)
        {
            rmcdata.UserId = Common.ConvertInt(Session["UserId"]);

            if (act == 3)
            {
                rmcdata.RMCategoryId = Common.ConvertInt(RMCategoryId);
                rmcdata.action = act;
                rmcdata.RMCategoryName = "";

            }
            else if (act == 1)
            {
                string RMCategory = Common.ConvertString(txtrmcategory.Text.Trim());
                ReturnMessage objs = common.CheckExist("RMCategory", RMCategory, "", "");
                string msgs = Common.ConvertString(objs.Message);

                if (Common.ConvertInt(objs.ReturnValue) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                    return;

                }

                else
                {
                    rmcdata .RMCategoryId= Common.ConvertInt(hdnmcid.Value);
                    rmcdata.action = act;

                    rmcdata.RMCategoryName = Common.ConvertString(txtrmcategory.Text);


                }
            }
            else
            {
                rmcdata.RMCategoryId = Common.ConvertInt(hdnmcid.Value);
                rmcdata.action = act;
                rmcdata.RMCategoryName = Common.ConvertString(txtrmcategory.Text);
            }
            ReturnMessage obj = rmc.InsertUpdateRMCategory(rmcdata);
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
            rmcdata = new RMCategoryBAL();
            hdnmcid.Value = "0";
            txtrmcategory.Text = "";

        }
    }
}