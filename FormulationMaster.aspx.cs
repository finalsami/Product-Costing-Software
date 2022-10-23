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
    public partial class FormulationMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        FormulationMasterDAL fm = new FormulationMasterDAL();
        FormulationMasterBAL fmdata = new FormulationMasterBAL();
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

            DataTable dt = fm.Get_FormulationMaster(Common.ConvertInt(Session["UserId"]), 0);
            txtlabourcost.Text = Common.ConvertString(dt.Rows[0]["labourcharge"]);
            hdntxtsvc.Value = Common.ConvertString(dt.Rows[0]["supervisorcharge"]);
            txtpowercharge.Text = Common.ConvertString(dt.Rows[0]["powerunitprice"]);
            gvformulationmaster.DataSource = dt;
            gvformulationmaster.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvformulationmaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvformulationmaster.UseAccessibleHeader = true;
            }

        }
        private void binddropdown()
        {

            DataTable dtunit = common.DropdownList("Unit_Measurement", "", "");

            drpunit.DataSource = dtunit;
            drpunit.DataTextField = "Name";
            drpunit.DataValueField = "Id";
            drpunit.DataBind();




        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int FormulationId = Common.ConvertInt(btn.CommandArgument);
            if (FormulationId > 0)
            {
                DataTable dt = fm.Get_FormulationMaster(Common.ConvertInt(Session["UserId"]), FormulationId);
                if (dt.Rows.Count > 0)
                {
                    hdnfmid.Value = Common.ConvertString(dt.Rows[0]["FormulationId"]);

                    if (drpunit.Items.FindByValue(Common.ConvertString(dt.Rows[0]["UnitMeasurementId"])) != null)
                    {
                        drpunit.SelectedValue = Common.ConvertString(dt.Rows[0]["UnitMeasurementId"]);
                    }

                   
                    txtfmname.Text = Common.ConvertString(dt.Rows[0]["FormulationName"]);
                    txtbatchsize.Text = Common.ConvertString(dt.Rows[0]["BatchSize"]);
                    txtlabours.Text = Common.ConvertString(dt.Rows[0]["Labours"]);
                    txtsupervisors.Text = Common.ConvertString(dt.Rows[0]["Supervisors"]);
                    txtpowerunit.Text = Common.ConvertString(dt.Rows[0]["PowerUnits"]);
                    txtmaintenence.Text = Common.ConvertString(dt.Rows[0]["MaintenanceCost"]);
                    txtothercost.Text = Common.ConvertString(dt.Rows[0]["OtherCost"]);
                    txtbuffer.Text = Common.ConvertString(dt.Rows[0]["AdditionalBuffer"]);
                    txtlabourcharge.Text = Common.ConvertString(dt.Rows[0]["totallabourcharge"]);
                    txttotalpower.Text = Common.ConvertString(dt.Rows[0]["totalpowercost"]);
                    txttotalcost.Text = Common.ConvertString(dt.Rows[0]["totalcost"]);
                    txtCostLtrBatchsize.Text = Common.ConvertString(dt.Rows[0]["costPerLtr"]);
                    txtfinalcostltrbatchsize.Text = Common.ConvertString(dt.Rows[0]["FinalcostLtr"]);



                    btnadd.Visible = false;
                    btnupdate.Visible = true;

                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int FormulationId = Common.ConvertInt(btn.CommandArgument);
            if (FormulationId > 0)
            {
                InsertUpdateFormulationMaster(3, FormulationId);
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
                InsertUpdateFormulationMaster(1, 0);
            }
        }
        private void InsertUpdateFormulationMaster(int act, int FormulationId)
        {

            if (act == 3)
            {
                fmdata.FormulationId = Common.ConvertInt(FormulationId);
                fmdata.action = act;
                fmdata.UserId = Common.ConvertInt(Session["UserId"]);
                fmdata.FormulationName = "";
            }
            else
            {
                fmdata.FormulationId = Common.ConvertInt(hdnfmid.Value);
                fmdata.action = act;
                fmdata.FormulationName = Common.ConvertString(txtfmname.Text);
                fmdata.UnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                fmdata.BatchSize = Common.ConvertInt(txtbatchsize.Text);
                fmdata.Labours = Common.ConvertInt(txtlabours.Text);
                fmdata.Supervisors = Common.ConvertInt(txtsupervisors.Text);
                fmdata.PowerUnits = Common.ConvertInt(txtpowerunit.Text);
                fmdata.MaintenanceCost = Common.ConvertInt(txtmaintenence.Text);
                fmdata.OtherCost = Common.ConvertInt(txtothercost.Text);
                fmdata.AdditionalBuffer = Common.ConvertInt(txtbuffer.Text);
                fmdata.UserId = Common.ConvertInt(Session["UserId"]);

            }
            ReturnMessage obj = fm.InsertUpdateFormulationMaster(Common.ConvertInt(Session["UserId"]), fmdata);
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
            fmdata = new FormulationMasterBAL();
            hdnfmid.Value = "0";
            txtfmname.Text = "";
            drpunit.ClearSelection();
            txtbatchsize.Text = "";
            txtlabours.Text = "";
            txtsupervisors.Text = "";
            txtpowerunit.Text = "";
            txtmaintenence.Text = "";
            txtothercost.Text = "";
            txtbuffer.Text = "";
            txtlabourcharge.Text = "";
            txttotalcost.Text = "";
            txtCostLtrBatchsize.Text = "";
            txtfinalcostltrbatchsize.Text = "";
            txttotalpower.Text = "";
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateFormulationMaster(2, 0);
            }
        }
    }
}