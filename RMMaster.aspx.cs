using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using DAL;

namespace Production_Costing_Software
{

    public partial class RMMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        RMMasterDAL rm = new RMMasterDAL();
        RMMasterBAL rmdata = new RMMasterBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                binddropdown();
                binddata();
            }
        }

        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("RMCategory", "", "");

            drprmcategory.DataSource = dtrm;
            drprmcategory.DataTextField = "Name";
            drprmcategory.DataValueField = "Id";
            drprmcategory.DataBind();


            DataTable dtunit = common.DropdownList("Unit_Measurement", "", "");

            drpunit.DataSource = dtunit;
            drpunit.DataTextField = "Name";
            drpunit.DataValueField = "Id";
            drpunit.DataBind();
        }

        private void binddata()
        {
           
            DataTable dt = rm.RMMasterList(Common.ConvertInt(Session["UserId"]),0);
            gvrmmaster.DataSource = dt;
            gvrmmaster.DataBind();
            if (dt.Rows.Count > 0)
            {
                
                gvrmmaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvrmmaster.UseAccessibleHeader = true;
            }

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateRMMaster(2,0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                InsertUpdateRMMaster(1,0);
            }            
        }

        private void InsertUpdateRMMaster(int act,int RMId)
        {
            rmdata.UserId = Common.ConvertInt(Session["UserId"]);

            if (act == 3)
            {
                rmdata.RMId = Common.ConvertInt(RMId);
                rmdata.action = act;
                rmdata.RMCategoryId = 0;
                rmdata.RMName = "";
                rmdata.UnitMeasurementId = 0;
                rmdata.IsPurity = false;             
                rmdata.RMCategoryName = "";
            }
            else if (act == 1)
            {
                string RMName = Common.ConvertString(txtrmname.Text.Trim());
                string RMCategory = Common.ConvertString(drprmcategory.SelectedValue);
                ReturnMessage objs = common.CheckExist("RMMaster", RMName, RMCategory,"");
                string msgs = Common.ConvertString(objs.Message);

                if (Common.ConvertInt(objs.ReturnValue) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                    return;

                }
                else
                {
                    rmdata.action = act;
                    rmdata.RMCategoryId = Common.ConvertInt(drprmcategory.SelectedValue);
                    rmdata.RMName = Common.ConvertString(txtrmname.Text);
                    rmdata.UnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                    rmdata.IsPurity = chkpurity.Checked;
                }
            }
            else
            {
                rmdata.RMId = Common.ConvertInt(hdnrmid.Value);
                rmdata.action = act;
                rmdata.RMCategoryId = Common.ConvertInt(drprmcategory.SelectedValue);
                rmdata.RMName = Common.ConvertString(txtrmname.Text);
                rmdata.UnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                rmdata.IsPurity = chkpurity.Checked;
                rmdata.RMCategoryName = "";
            }
            ReturnMessage obj = rm.InsertUpdateRMMaster(rmdata);
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
            rmdata = new RMMasterBAL();
            hdnrmid.Value = "0";
            drprmcategory.ClearSelection();
            drpunit.ClearSelection();
            txtrmname.Text = "";
            chkpurity.Checked = false;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMId = Common.ConvertInt(btn.CommandArgument);
            if (RMId > 0)
            {
                DataTable dt = rm.RMMasterList(Common.ConvertInt(Session["UserId"]), RMId);
                if(dt.Rows.Count > 0)
                {
                    hdnrmid.Value = Common.ConvertString(dt.Rows[0]["RMId"]);
                    txtrmname.Text = Common.ConvertString(dt.Rows[0]["RMName"]);
                    if (drprmcategory.Items.FindByValue(Common.ConvertString(dt.Rows[0]["CategoryId"]))!=null)
                    {
                        drprmcategory.SelectedValue = Common.ConvertString(dt.Rows[0]["CategoryId"]);
                    }
                    if (drpunit.Items.FindByValue(Common.ConvertString(dt.Rows[0]["UnitMeasurementId"])) != null)
                    {
                        drpunit.SelectedValue = Common.ConvertString(dt.Rows[0]["UnitMeasurementId"]);
                    }
                    chkpurity.Checked = Common.ConvertBool(dt.Rows[0]["IsPurity"]);

                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMId  = Common.ConvertInt(btn.CommandArgument);
            if (RMId > 0)
            {
                InsertUpdateRMMaster(3, RMId);
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