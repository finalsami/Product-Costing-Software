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
    public partial class PackingStyleLabourCostingMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        PackingStyleLabourCostingMasterDAL pslc = new PackingStyleLabourCostingMasterDAL();
        PackingStyleLabourCostingMasterBAL pslcdata = new PackingStyleLabourCostingMasterBAL();
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

            DataTable dt = pslc.GetPackingStyleLabourCostingMaster(Common.ConvertInt(Session["UserId"]), 0);
            gvpslcmaster.DataSource = dt;
            gvpslcmaster.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvpslcmaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvpslcmaster.UseAccessibleHeader = true;
            }
        }
        private void binddropdown()
        {
            DataTable dtpc = common.DropdownList("PackingCategoryAndSize", "", "");

            drppcs.DataSource = dtpc;
            drppcs.DataTextField = "Name";
            drppcs.DataValueField = "Id";
            drppcs.DataBind();


            DataTable dtps = common.DropdownList("PackingStyle", "", "");

            drppsn.DataSource = dtps;
            drppsn.DataTextField = "Name";
            drppsn.DataValueField = "Id";
            drppsn.DataBind();

            DataTable dtunit = common.DropdownList("Unit_Measurement", "", "");

            drpunit.DataSource = dtunit;
            drpunit.DataTextField = "Name";
            drpunit.DataValueField = "Id";
            drpunit.DataBind();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PackingStyleLabourCostingId = Common.ConvertInt(btn.CommandArgument);
            if (PackingStyleLabourCostingId > 0)
            {
                InsertUpdatePackingStyleLabourCostingMaster(3, PackingStyleLabourCostingId);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PackingStyleLabourCostingId = Common.ConvertInt(btn.CommandArgument);
            if (PackingStyleLabourCostingId > 0)
            {
                DataTable dt = pslc.GetPackingStyleLabourCostingMaster(Common.ConvertInt(Session["UserId"]), PackingStyleLabourCostingId);
                if (dt.Rows.Count > 0)
                {

                    hdnPackingStyleLabourCostingId.Value = Common.ConvertString(dt.Rows[0]["PackingStyleLabourCostingId"]);
                    drppcs.SelectedValue = Common.ConvertString(dt.Rows[0]["FkPackingSizeCategoryId"]);
                    drppsn.SelectedValue = Common.ConvertString(dt.Rows[0]["FkPackingStyleId"]);
                    drpunit.SelectedValue = Common.ConvertString(dt.Rows[0]["FkUnitMeasurementId"]);
                    txtpsize.Text= Common.ConvertString(dt.Rows[0]["PackingSize"]);

                    txtbulkcharge.Text = Common.ConvertString(dt.Rows[0]["TaskBulkCharge"]);
                    txtpouchfilling.Text = Common.ConvertString(dt.Rows[0]["TaskPouchFilling"]);
                    txtliftpouchbttwt.Text = Common.ConvertString(dt.Rows[0]["TaskLiftingWeight"]);
                    txtbottlekeeping.Text = Common.ConvertString(dt.Rows[0]["TaskBottleKeeping"]);
                    txtblackinnerpouch.Text = Common.ConvertString(dt.Rows[0]["TaskBlacklinnerPouch"]);
                    txtinnerplug.Text = Common.ConvertString(dt.Rows[0]["TaskInnerPlug"]);
                    txtmeasureingcap.Text = Common.ConvertString(dt.Rows[0]["TaskMesuringCap"]);
                    txtlcaping.Text = Common.ConvertString(dt.Rows[0]["TaskCaping"]);
                    txtteardownseal.Text = Common.ConvertString(dt.Rows[0]["TaskTearDownSeal"]);
                    txtlInduction.Text = Common.ConvertString(dt.Rows[0]["TaskInduction"]);
                    txtpouchsealing.Text = Common.ConvertString(dt.Rows[0]["TaskPouchSealing"]);
                    txtbottlepouchcleaning.Text = Common.ConvertString(dt.Rows[0]["TaskBottlePouchCleaning"]);
                    txtlabeling.Text = Common.ConvertString(dt.Rows[0]["TaskLabeling"]);
                    txtsleeve.Text = Common.ConvertString(dt.Rows[0]["TaskSleeve"]);
                    txtinnerbox.Text = Common.ConvertString(dt.Rows[0]["TaskInnerbox"]);
                    txtsstindrumbucketbag.Text = Common.ConvertString(dt.Rows[0]["TaskSSTinDrumBucketBag"]);
                    txtinnerboxcellotape.Text = Common.ConvertString(dt.Rows[0]["TaskInnerBoxCelloTape"]);
                    txtkitchentray.Text = Common.ConvertString(dt.Rows[0]["TaskKitchenTray"]);
                    txtadditionalother.Text = Common.ConvertString(dt.Rows[0]["TaskAdditionalOther"]);
                    txtstappnigwt.Text = Common.ConvertString(dt.Rows[0]["TaskStappingWeight"]);
                    txtouterlblboppboxfill.Text = Common.ConvertString(dt.Rows[0]["TaskOuterLabelBOPPBoxFilling"]);

                    //power---
                    txtpfilling.Text = Common.ConvertString(dt.Rows[0]["PowerFilling"]);
                    txtpinduction.Text = Common.ConvertString(dt.Rows[0]["PowerInduction"]);
                    txtpcapping.Text = Common.ConvertString(dt.Rows[0]["PowerCapping"]);
                    txtplabeling.Text = Common.ConvertString(dt.Rows[0]["PowerLableling"]);
                    txtshrinking.Text = Common.ConvertString(dt.Rows[0]["PowerShrinking"]);
                    txtbopp.Text = Common.ConvertString(dt.Rows[0]["PowerBOPP"]);
                    txtstepping.Text = Common.ConvertString(dt.Rows[0]["PowerStepping"]);
                    txtsealingmc.Text = Common.ConvertString(dt.Rows[0]["PowerStealingMC"]);
                    txtpowerdetail9.Text = Common.ConvertString(dt.Rows[0]["PowerDetail9"]);
                    txtpowerdetail10.Text = Common.ConvertString(dt.Rows[0]["PowerDetail10"]);
                    txtpowerunit.Text = Common.ConvertString(dt.Rows[0]["PowerUnitPerHour"]);
                    txtotherpower.Text = Common.ConvertString(dt.Rows[0]["PowerOther"]);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "calculaterate()", true);




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
                InsertUpdatePackingStyleLabourCostingMaster(1, 0);
            }
        }
        private void InsertUpdatePackingStyleLabourCostingMaster(int act, int PackingStyleLabourCostingId)
        {

            if (act == 3)
            {
                pslcdata.PackingStyleLabourCostingId = Common.ConvertInt(PackingStyleLabourCostingId);
                pslcdata.action = act;
                pslcdata.UserId = Common.ConvertInt(Session["UserId"]);
            }
            else
            {
                if (act == 1)
                {

                    string PackingCatgory = Common.ConvertString(drppcs.SelectedValue);
                    string PackingStyle = Common.ConvertString(drppsn.SelectedValue);
                    string PackingSize = Common.ConvertString(txtpsize.Text);
                    ReturnMessage objs = common.CheckExist("PackingStyleLabourCostingMaster", PackingCatgory, PackingStyle, PackingSize);
                    string msgs = Common.ConvertString(objs.Message);
                    pslcdata.UserId = Common.ConvertInt(Session["UserId"]);
                    if (Common.ConvertInt(objs.ReturnValue) == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                        cleardata();
                        return;
                    }
                    else
                    {
                        pslcdata.PackingStyleLabourCostingId = Common.ConvertInt(hdnPackingStyleLabourCostingId.Value);
                        pslcdata.action = act;
                        pslcdata.FkUnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                        pslcdata.FkPackingStyleId = Common.ConvertInt(drppsn.SelectedValue);
                        pslcdata.FkPackingSizeCategoryId = Common.ConvertInt(drppcs.SelectedValue);
                        pslcdata.PackingSize = Common.ConvertDecimal(txtpsize.Text);

                        //Labout Task------------
                        pslcdata.TaskBulkCharge = Common.ConvertInt(txtbulkcharge.Text);
                        pslcdata.TaskPouchFilling = Common.ConvertInt(txtpouchfilling.Text);
                        pslcdata.TaskBottleKeeping = Common.ConvertInt(txtbottlekeeping.Text);
                        pslcdata.TaskLiftingWeight = Common.ConvertInt(txtliftpouchbttwt.Text);
                        pslcdata.TaskBlacklinnerPouch = Common.ConvertInt(txtblackinnerpouch.Text);
                        pslcdata.TaskInnerPlug = Common.ConvertInt(txtinnerplug.Text);
                        pslcdata.TaskMesuringCap = Common.ConvertInt(txtmeasureingcap.Text);
                        pslcdata.TaskCaping = Common.ConvertInt(txtlcaping.Text);
                        pslcdata.TaskTearDownSeal = Common.ConvertInt(txtteardownseal.Text);
                        pslcdata.TaskInduction = Common.ConvertInt(txtlInduction.Text);
                        pslcdata.TaskPouchSealing = Common.ConvertInt(txtpouchsealing.Text);
                        pslcdata.TaskBottlePouchCleaning = Common.ConvertInt(txtbottlepouchcleaning.Text);
                        pslcdata.TaskLabeling = Common.ConvertInt(txtlabeling.Text);
                        pslcdata.TaskSleeve = Common.ConvertInt(txtsleeve.Text);
                        pslcdata.TaskInnerbox = Common.ConvertInt(txtinnerbox.Text);
                        pslcdata.TaskSSTinDrumBucketBag = Common.ConvertInt(txtsstindrumbucketbag.Text);
                        pslcdata.TaskInnerBoxCelloTape = Common.ConvertInt(txtinnerboxcellotape.Text);
                        pslcdata.TaskKitchenTray = Common.ConvertInt(txtkitchentray.Text);
                        pslcdata.TaskAdditionalOther = Common.ConvertInt(txtadditionalother.Text);
                        pslcdata.TaskStappingWeight = Common.ConvertInt(txtstappnigwt.Text);
                        pslcdata.TaskOuterLabelBOPPBoxFilling = Common.ConvertInt(txtouterlblboppboxfill.Text);


                        // Labour Task End---------------------------------------

                        //Power Unit ----------------------------------------
                        pslcdata.PowerFilling = Common.ConvertDecimal(txtpfilling.Text);
                        pslcdata.PowerInduction = Common.ConvertDecimal(txtpinduction.Text);
                        pslcdata.PowerCapping = Common.ConvertDecimal(txtpcapping.Text);
                        pslcdata.PowerLableling = Common.ConvertDecimal(txtplabeling.Text);
                        pslcdata.PowerShrinking = Common.ConvertDecimal(txtshrinking.Text);
                        pslcdata.PowerBOPP = Common.ConvertDecimal(txtbopp.Text);
                        pslcdata.PowerStepping = Common.ConvertDecimal(txtstepping.Text);
                        pslcdata.PowerStealingMC = Common.ConvertDecimal(txtsealingmc.Text);
                        pslcdata.PowerDetail9 = Common.ConvertDecimal(txtpowerdetail9.Text);
                        pslcdata.PowerDetail10 = Common.ConvertDecimal(txtpowerdetail10.Text);
                        pslcdata.PowerUnitPerHour = Common.ConvertDecimal(txtpowerunit.Text);
                        pslcdata.PowerOther = Common.ConvertDecimal(txtotherpower.Text);

                        //Power End -----

                    }
                }
                else
                {
                    pslcdata.PackingStyleLabourCostingId = Common.ConvertInt(hdnPackingStyleLabourCostingId.Value);
                    pslcdata.action = act;
                    pslcdata.FkUnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                    pslcdata.FkPackingStyleId = Common.ConvertInt(drppsn.SelectedValue);
                    pslcdata.FkPackingSizeCategoryId = Common.ConvertInt(drppcs.SelectedValue);
                    pslcdata.PackingSize = Common.ConvertDecimal(txtpsize.Text);

                    //Labout Task------------
                    pslcdata.TaskBulkCharge = Common.ConvertInt(txtbulkcharge.Text);
                    pslcdata.TaskPouchFilling = Common.ConvertInt(txtpouchfilling.Text);
                    pslcdata.TaskBottleKeeping = Common.ConvertInt(txtbottlekeeping.Text);
                    pslcdata.TaskLiftingWeight = Common.ConvertInt(txtliftpouchbttwt.Text);
                    pslcdata.TaskBlacklinnerPouch = Common.ConvertInt(txtblackinnerpouch.Text);
                    pslcdata.TaskInnerPlug = Common.ConvertInt(txtinnerplug.Text);
                    pslcdata.TaskMesuringCap = Common.ConvertInt(txtmeasureingcap.Text);
                    pslcdata.TaskCaping = Common.ConvertInt(txtlcaping.Text);
                    pslcdata.TaskTearDownSeal = Common.ConvertInt(txtteardownseal.Text);
                    pslcdata.TaskInduction = Common.ConvertInt(txtlInduction.Text);
                    pslcdata.TaskPouchSealing = Common.ConvertInt(txtpouchsealing.Text);
                    pslcdata.TaskBottlePouchCleaning = Common.ConvertInt(txtbottlepouchcleaning.Text);
                    pslcdata.TaskLabeling = Common.ConvertInt(txtlabeling.Text);
                    pslcdata.TaskSleeve = Common.ConvertInt(txtsleeve.Text);
                    pslcdata.TaskInnerbox = Common.ConvertInt(txtinnerbox.Text);
                    pslcdata.TaskSSTinDrumBucketBag = Common.ConvertInt(txtsstindrumbucketbag.Text);
                    pslcdata.TaskInnerBoxCelloTape = Common.ConvertInt(txtinnerboxcellotape.Text);
                    pslcdata.TaskKitchenTray = Common.ConvertInt(txtkitchentray.Text);
                    pslcdata.TaskAdditionalOther = Common.ConvertInt(txtadditionalother.Text);
                    pslcdata.TaskStappingWeight = Common.ConvertInt(txtstappnigwt.Text);
                    pslcdata.TaskOuterLabelBOPPBoxFilling = Common.ConvertInt(txtouterlblboppboxfill.Text);


                    // Labour Task End---------------------------------------

                    //Power Unit ----------------------------------------
                    pslcdata.PowerFilling = Common.ConvertDecimal(txtpfilling.Text);
                    pslcdata.PowerInduction = Common.ConvertDecimal(txtpinduction.Text);
                    pslcdata.PowerCapping = Common.ConvertDecimal(txtpcapping.Text);
                    pslcdata.PowerLableling = Common.ConvertDecimal(txtplabeling.Text);
                    pslcdata.PowerShrinking = Common.ConvertDecimal(txtshrinking.Text);
                    pslcdata.PowerBOPP = Common.ConvertDecimal(txtbopp.Text);
                    pslcdata.PowerStepping = Common.ConvertDecimal(txtstepping.Text);
                    pslcdata.PowerStealingMC = Common.ConvertDecimal(txtsealingmc.Text);
                    pslcdata.PowerDetail9 = Common.ConvertDecimal(txtpowerdetail9.Text);
                    pslcdata.PowerDetail10 = Common.ConvertDecimal(txtpowerdetail10.Text);
                    pslcdata.PowerUnitPerHour = Common.ConvertDecimal(txtpowerunit.Text);
                    pslcdata.PowerOther = Common.ConvertDecimal(txtotherpower.Text);
                    pslcdata.UserId = Common.ConvertInt(Session["UserId"]);

                    //Power End -----

                }

            }
            ReturnMessage obj = pslc.InsertUpdatePackingStyleLabourCostingMaster(pslcdata);
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
            pslc = new PackingStyleLabourCostingMasterDAL();
            hdnPackingStyleLabourCostingId.Value = "0";
            txtpsize.Text = "0.00";
            drppcs.ClearSelection();
            drppsn.ClearSelection();
            drpunit.ClearSelection();
            txtTotalLabourPerTask.Text = "0";
            txttotalpower.Text = "0";

            //Labout Task------------
            txtbulkcharge.Text="0";
           txtpouchfilling.Text="0";
            txtbottlekeeping.Text="0";
           txtliftpouchbttwt.Text="0";
            txtblackinnerpouch.Text="0";
           txtinnerplug.Text="0";
            txtmeasureingcap.Text="0";
            txtlcaping.Text="0";
           txtteardownseal.Text="0";
            txtlInduction.Text="0";
           txtpouchsealing.Text="0";
            txtbottlepouchcleaning.Text="0";
           txtlabeling.Text="0";
            txtsleeve.Text="0";
           txtinnerbox.Text="0";
           txtsstindrumbucketbag.Text="0";
           txtinnerboxcellotape.Text="0";
            txtkitchentray.Text="0";
           txtadditionalother.Text="0";
           txtstappnigwt.Text="0";
           txtouterlblboppboxfill.Text="0";


            // Labour Task End---------------------------------------

            //Power Unit ----------------------------------------
            txtpfilling.Text="0";
            txtpinduction.Text="0";
           txtpcapping.Text="0";
            txtplabeling.Text="0";
            txtshrinking.Text="0";
            txtbopp.Text="0";
            txtstepping.Text="0";
           txtsealingmc.Text="0";
            txtpowerdetail9.Text="0";
            txtpowerdetail10.Text="0";
            txtpowerunit.Text="0";
            txtotherpower.Text="0";

        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePackingStyleLabourCostingMaster(2, 0);
            }
        }
    }
}