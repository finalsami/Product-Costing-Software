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
    public partial class PackingStyleName : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        PackingStyleNameDAL ps = new PackingStyleNameDAL();
        PackingStyleNameBAL psdata = new PackingStyleNameBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }
        private void binddata()
        {

            DataTable dt = ps.GetPackingStyleList(Common.ConvertInt(Session["UserId"]), 0);
            gvpackingstyle.DataSource = dt;
            gvpackingstyle.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvpackingstyle.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvpackingstyle.UseAccessibleHeader = true;
            }

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PackingStyleId = Common.ConvertInt(btn.CommandArgument);
            if (PackingStyleId > 0)
            {
                DataTable dt = ps.GetPackingStyleList(Common.ConvertInt(Session["UserId"]), PackingStyleId);
                if (dt.Rows.Count > 0)
                {
                    hdnpsid.Value = Common.ConvertString(dt.Rows[0]["PackingStyleId"]);
                    txtpsname.Text = Common.ConvertString(dt.Rows[0]["PAckingStyleName"]);


                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PackingStyleId = Common.ConvertInt(btn.CommandArgument);
            if (PackingStyleId > 0)
            {
                InsertUpdatePackingStyle(3, PackingStyleId);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePackingStyle(2, 0);
            }
        }
        private void InsertUpdatePackingStyle(int act, int PackingStyleId)
        {

            if (act == 3)
            {
                psdata.PackingStyleId = Common.ConvertInt(PackingStyleId);
                psdata.action = act;
                psdata.UserId = Common.ConvertInt(Session["UserId"]);
                psdata.PackingStyle = "";

            }

            else if (act == 1)
            {
                string PackingStyle = Common.ConvertString(txtpsname.Text.Trim());
                ReturnMessage objs = common.CheckExist("PackingStyle", PackingStyle, "", "");
                string msgs = Common.ConvertString(objs.Message);
                psdata.UserId = Common.ConvertInt(Session["UserId"]);

                if (Common.ConvertInt(objs.ReturnValue) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                    return;

                }

                else
                {
                    psdata.PackingStyleId = Common.ConvertInt(hdnpsid.Value);
                    psdata.action = act;

                    psdata.PackingStyle = Common.ConvertString(txtpsname.Text);
                    psdata.UserId = Common.ConvertInt(Session["UserId"]);


                }
            }
            else
            {
                psdata.PackingStyleId = Common.ConvertInt(hdnpsid.Value);
                psdata.action = act;
                psdata.PackingStyle = Common.ConvertString(txtpsname.Text);
                psdata.UserId = Common.ConvertInt(Session["UserId"]);

            }
            ReturnMessage obj = ps.InsertUpdatePackingStyle(psdata);
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

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePackingStyle(1, 0);
            }
        }
        private void cleardata()
        {
            psdata = new PackingStyleNameBAL();
            hdnpsid.Value = "0";
            txtpsname.Text = "";

        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }
    }
}