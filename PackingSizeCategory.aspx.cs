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
    public partial class PackingSizeCategory : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        PackingSizeCategoryDAL psc = new PackingSizeCategoryDAL();
        PackingSizeCategoryBAL pscdata = new PackingSizeCategoryBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddropdown();

                binddata();
            }
        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("PackingCategory", "", "");

            drppackingcategory.DataSource = dtrm;
            drppackingcategory.DataTextField = "Name";
            drppackingcategory.DataValueField = "Id";
            drppackingcategory.DataBind();


            DataTable dtunit = common.DropdownList("Unit_Measurement", "", "");

            drpunit.DataSource = dtunit;
            drpunit.DataTextField = "Name";
            drpunit.DataValueField = "Id";
            drpunit.DataBind();
        }
        private void binddata()
        {

            DataTable dt = psc.PackingSizeCategoryList(Common.ConvertInt(Session["UserId"]), 0);
            gvpackingsizecategory.DataSource = dt;
            gvpackingsizecategory.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvpackingsizecategory.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvpackingsizecategory.UseAccessibleHeader = true;
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
                InsertUpdatePackingSizeCategory(1, 0);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePackingSizeCategory(2, 0);
            }
        }
        private void InsertUpdatePackingSizeCategory(int act, int PackingSizeCategoryId)
        {

            if (act == 3)
            {
                pscdata.PackingSizeCategoryId = Common.ConvertInt(PackingSizeCategoryId);
                pscdata.action = act;
                pscdata.UserId = Common.ConvertInt(Session["UserId"]);
                pscdata.PackingSize = 0;
            }
            else if (act == 1)
            {
                string PackingSizeCategory = Common.ConvertString(txtpackingsize.Text.Trim());
                string PackingCategory = Common.ConvertString(drppackingcategory.SelectedValue);
                ReturnMessage objs = common.CheckExist("PackingSizeCategory", PackingSizeCategory, PackingCategory, "");
                string msgs = Common.ConvertString(objs.Message);

                if (Common.ConvertInt(objs.ReturnValue) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                    return;

                }
            }
            else
            {
                pscdata.PackingSizeCategoryId = Common.ConvertInt(hdpscid.Value);
                pscdata.action = act;
                pscdata.PackingSize = Common.ConvertDecimal(txtpackingsize.Text);
                pscdata.FkPackingCategoryId = Common.ConvertInt(drppackingcategory.SelectedValue);
                pscdata.FkUnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                pscdata.UserId = Common.ConvertInt(Session["UserId"]);
            }
            ReturnMessage obj = psc.InsertUpdatePackingSizeCategory(pscdata);
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
            pscdata = new PackingSizeCategoryBAL();
            hdpscid.Value = "0";
            txtpackingsize.Text = "";
            drppackingcategory.ClearSelection();
            drpunit.ClearSelection();
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PackingSizeCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (PackingSizeCategoryId > 0)
            {
                DataTable dt = psc.PackingSizeCategoryList(Common.ConvertInt(Session["UserId"]), PackingSizeCategoryId);
                if (dt.Rows.Count > 0)
                {
                    hdpscid.Value = Common.ConvertString(dt.Rows[0]["PackingSizeCategoryId"]);
                    txtpackingsize.Text = Common.ConvertString(dt.Rows[0]["PackingSize"]);
                    drppackingcategory.SelectedValue= Common.ConvertString(dt.Rows[0]["FkPackingCategoryId"]);
                    drpunit.SelectedValue= Common.ConvertString(dt.Rows[0]["FkUnitMeasurementId"]);


                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PackingSizeCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (PackingSizeCategoryId > 0)
            {
                InsertUpdatePackingSizeCategory(3, PackingSizeCategoryId);
            }
        }
    }
}