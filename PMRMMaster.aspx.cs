using BAL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class PMRMMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        PMRMMasterDAL pmrm = new PMRMMasterDAL();
        PMRMMasterBAL pmrmdata = new PMRMMasterBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddropdown();
                binddata();
            }
        }
        private void binddata()
        {

            DataTable dt = pmrm.Get_PMRMMaster(Common.ConvertInt(Session["UserId"]), 0);
            gvpmrmmaster.DataSource = dt;
            gvpmrmmaster.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvpmrmmaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvpmrmmaster.UseAccessibleHeader = true;
            }

        }

        private void binddropdown()
        {
            DataTable dtmc = common.DropdownList("PMRMCategory", "", "");

            drppmrmcat.DataSource = dtmc;
            drppmrmcat.DataTextField = "Name";
            drppmrmcat.DataValueField = "Id";
            drppmrmcat.DataBind();


            DataTable dtunit = common.DropdownList("Unit_Measurement", "", "");

            drpunit.DataSource = dtunit;
            drpunit.DataTextField = "Name";
            drpunit.DataValueField = "Id";
            drpunit.DataBind();


        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePMRMMaster(2, 0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePMRMMaster(1, 0);
            }
        }
        private void InsertUpdatePMRMMaster(int act, int PMRMId)
        {

            if (act == 3)
            {
                pmrmdata.PMRMId = Common.ConvertInt(PMRMId);
                pmrmdata.action = act;
                pmrmdata.UserId = Common.ConvertInt(Session["UserId"]);
                pmrmdata.PMRMName = "";
            }

            else if (act == 1)
            {
                string PMRMName = Common.ConvertString(txtpmrmname.Text.Trim());
                ReturnMessage objs = common.CheckExist("PMRMMaster", PMRMName, "", "");
                string msgs = Common.ConvertString(objs.Message);

                if (Common.ConvertInt(objs.ReturnValue) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                    return;

                }

                else
                {
                    pmrmdata.PMRMId = Common.ConvertInt(hdnpmrmid.Value);
                    pmrmdata.action = act;
                    pmrmdata.FkUnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                    pmrmdata.FkPMRMCategoryId = Common.ConvertInt(drppmrmcat.SelectedValue);
                    pmrmdata.Unit = Common.ConvertInt(txtnoofunit.Text);
                    pmrmdata.TotalWeight = Common.ConvertDecimal(txttotalweight.Text);
                    pmrmdata.PMRMName = Common.ConvertString(txtpmrmname.Text);
                    pmrmdata.UserId = Common.ConvertInt(Session["UserId"]);

                }

            }
            else
            {
                pmrmdata.PMRMId = Common.ConvertInt(hdnpmrmid.Value);
                pmrmdata.action = act;
                pmrmdata.FkUnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                pmrmdata.FkPMRMCategoryId = Common.ConvertInt(drppmrmcat.SelectedValue);
                pmrmdata.Unit = Common.ConvertInt(txtnoofunit.Text);
                pmrmdata.TotalWeight = Common.ConvertDecimal(txttotalweight.Text);
                pmrmdata.PMRMName = Common.ConvertString(txtpmrmname.Text);

                pmrmdata.UserId = Common.ConvertInt(Session["UserId"]);
            }



            ReturnMessage obj = pmrm.InsertUpdatePMRMMaster(pmrmdata);
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
        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
            btnadd.Visible = true;

            btnupdate.Visible = false;
            DivNoofunit.Visible = true;
            DivTotalWeight.Visible = true;
            DivPerunitkg.Visible = true;
            DivUnitPerkg.Visible = true;
            txtnoofunit.Enabled = true;
            txttotalweight.Enabled = true;
            drpunit.Enabled = true;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PMRMId = Common.ConvertInt(btn.CommandArgument);
            if (PMRMId > 0)
            {
                DataTable dt = pmrm.Get_PMRMMaster(Common.ConvertInt(Session["UserId"]), PMRMId);
                if (dt.Rows.Count > 0)
                {
                    hdnpmrmid.Value = Common.ConvertString(dt.Rows[0]["PMRMId"]);

                    if (drppmrmcat.Items.FindByValue(Common.ConvertString(dt.Rows[0]["FkPMRMCategoryId"])) != null)
                    {
                        drppmrmcat.SelectedValue = Common.ConvertString(dt.Rows[0]["FkPMRMCategoryId"]);

                    }

                    if (drpunit.Items.FindByValue(Common.ConvertString(dt.Rows[0]["FkUnitMeasurementId"])) != null)
                    {
                        drpunit.SelectedValue = Common.ConvertString(dt.Rows[0]["FkUnitMeasurementId"]);
                    }
  
                    txtpmrmname.Text = Common.ConvertString(dt.Rows[0]["PMRMName"]);
                    txtnoofunit.Text = Common.ConvertString(dt.Rows[0]["Unit"]);
                    txttotalweight.Text = Common.ConvertString(dt.Rows[0]["TotalWeight"]);
                    txtPerunitkg.Text = Common.ConvertString(dt.Rows[0]["PerUnitWeight"]);
                    txtunitkg.Text = Common.ConvertString(dt.Rows[0]["UnitKG"]);

                    drpunit.Enabled = false;
                    if (drpunit.SelectedValue=="3")
                    {
                        txtnoofunit.Enabled = false;
                        txttotalweight.Enabled = false;
                        DivNoofunit.Visible = false;
                        DivNoofunit.Visible = false;
                        DivTotalWeight.Visible = false;
                        DivPerunitkg.Visible = false;
                        DivUnitPerkg.Visible = false;
                    }
                    else
                    {
                        txtnoofunit.Enabled = true;
                        txttotalweight.Enabled = true;
                    }
                    btnadd.Visible = false;
                    btnupdate.Visible = true;

                }
            }
        }
        private void cleardata()
        {
            pmrmdata = new PMRMMasterBAL();
            hdnpmrmid.Value = "0";
            txtpmrmname.Text = "";
            txtnoofunit.Text = "";
            txttotalweight.Text = "";
            txtPerunitkg.Text = "";
            txtunitkg.Text = "";
            drpunit.ClearSelection();
            drppmrmcat.ClearSelection();
            drpunit.ClearSelection();
            txtnoofunit.Enabled = true;
            txttotalweight.Enabled = true;
            drpunit.Enabled = true;


        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PMRMId = Common.ConvertInt(btn.CommandArgument);
            if (PMRMId > 0)
            {
                InsertUpdatePMRMMaster(3, PMRMId);
            }
        }
    }
}