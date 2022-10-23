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
    public partial class PackingCategory :BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        PackingCategoryDAL pc = new PackingCategoryDAL();
        PackingCategoryBAL pcdata = new PackingCategoryBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }
        private void binddata()
        {

            DataTable dt = pc.PackingCategoryList(Common.ConvertInt(Session["UserId"]), 0);
            gvpackingcategory.DataSource = dt;
            gvpackingcategory.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvpackingcategory.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvpackingcategory.UseAccessibleHeader = true;
            }

        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePackingCategory(2, 0);
            }
        }
        private void InsertUpdatePackingCategory(int act, int PackingCategoryId)
        {

            if (act == 3)
            {
                pcdata.PackingCategoryId = Common.ConvertInt(PackingCategoryId);
                pcdata.action = act;
                pcdata.UserId = Common.ConvertInt(Session["UserId"]);
                pcdata.PackingCategoryName = "";
            }
            else if (act == 1)
            {
                string PackingCategory = Common.ConvertString(txtpackingcategory.Text.Trim());
                ReturnMessage objs = common.CheckExist("PackingCategory", PackingCategory, "","");
                string msgs = Common.ConvertString(objs.Message);

                if (Common.ConvertInt(objs.ReturnValue) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                    return;

                }
            }

            else
            {
                pcdata.PackingCategoryId = Common.ConvertInt(hdnmcid.Value);
                pcdata.action = act;
                pcdata.PackingCategoryName = Common.ConvertString(txtpackingcategory.Text);
                pcdata.UserId = Common.ConvertInt(Session["UserId"]);
            }
            ReturnMessage obj = pc.InsertUpdatePackingCategory(pcdata);
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
            pcdata = new PackingCategoryBAL();
            hdnmcid.Value = "0";
            txtpackingcategory.Text = "";

        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePackingCategory(1, 0);
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PackingCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (PackingCategoryId > 0)
            {
                InsertUpdatePackingCategory(3, PackingCategoryId);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PackingCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (PackingCategoryId > 0)
            {
                DataTable dt = pc.PackingCategoryList(Common.ConvertInt(Session["UserId"]), PackingCategoryId);
                if (dt.Rows.Count > 0)
                {
                    hdnmcid.Value = Common.ConvertString(dt.Rows[0]["PackingCategoryId"]);
                    txtpackingcategory.Text = Common.ConvertString(dt.Rows[0]["PackingCategoryName"]);


                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }
    }
}