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
    public partial class MainCategory : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        MainCategoryDAL mc = new MainCategoryDAL();
        MainCategoryBAL cmdata = new MainCategoryBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }

        }
        private void binddata()
        {

            DataTable dt = mc.MainCategoryList(Common.ConvertInt(Session["UserId"]), 0);
            gvmaincategory.DataSource = dt;
            gvmaincategory.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvmaincategory.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvmaincategory.UseAccessibleHeader = true;
            }

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateMainCategory(2, 0);
            }
        }
        private void InsertUpdateMainCategory(int act, int MainCategoryId)
        {
        
            if (act == 3)
            {
                cmdata.MainCategoryId = Common.ConvertInt(MainCategoryId);
                cmdata.action = act;
                cmdata.UserId = Common.ConvertInt(Session["UserId"]);
                cmdata.MainCategoryName = "";

            }
            else if (act == 1)
            {
                string MainCategory = Common.ConvertString(txtmaincategory.Text.Trim());
                ReturnMessage objs = common.CheckExist("MainCategory", MainCategory, "","");
                string msgs = Common.ConvertString(objs.Message);

                if (Common.ConvertInt(objs.ReturnValue) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                    return;

                }

                else
                {
                   cmdata.MainCategoryId = Common.ConvertInt(hdnmcid.Value);
                   cmdata.action = act;
                   
                   cmdata.MainCategoryName = Common.ConvertString(txtmaincategory.Text);
                    cmdata.UserId = Common.ConvertInt(Session["UserId"]);


                }
            }
            else
            {
                cmdata.MainCategoryId = Common.ConvertInt(hdnmcid.Value);
                cmdata.action = act;
                cmdata.MainCategoryName = Common.ConvertString(txtmaincategory.Text);   
                cmdata.UserId =  Common.ConvertInt(Session["UserId"]);

            }
            ReturnMessage obj = mc.InsertUpdateMainCategory(cmdata);
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
            cmdata = new MainCategoryBAL();
            hdnmcid.Value = "0";         
            txtmaincategory.Text = "";
          
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateMainCategory(1, 0);
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
            int MainCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (MainCategoryId > 0)
            {
                DataTable dt = mc.MainCategoryList(Common.ConvertInt(Session["UserId"]), MainCategoryId);
                if (dt.Rows.Count > 0)
                {
                    hdnmcid.Value = Common.ConvertString(dt.Rows[0]["MainCategoryId"]);
                    txtmaincategory.Text = Common.ConvertString(dt.Rows[0]["MainCategoryName"]);
          

                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int MainCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (MainCategoryId > 0)
            {
                InsertUpdateMainCategory(3, MainCategoryId);
            }
        }
    }
}