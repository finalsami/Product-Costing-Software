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
    public partial class ProductwiseLabourCostMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        ProductwiseLabourCostDAL pwlc = new ProductwiseLabourCostDAL();
        ProductwiseLabourCostBAL pwlcdata = new ProductwiseLabourCostBAL();
        CostVariableMasterDAL cv = new CostVariableMasterDAL();
        CostVariableMasterBAL cvdata = new CostVariableMasterBAL();
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

            DataTable dt = pwlc.Get_ProductwiseLabourCost(Common.ConvertInt(Session["UserId"]), 0);
            gvpwlc.DataSource = dt;
            gvpwlc.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvpwlc.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvpwlc.UseAccessibleHeader = true;
            }
            //CostVariableMaster
            DataTable dtcv = cv.GetOtherVariable(Common.ConvertInt(Session["UserId"]), 0);
            hdnunloadingltr.Value = Common.ConvertString(dtcv.Rows[0]["UnloadingExpenseLtr"]);
            hdnloadingltr.Value = Common.ConvertString(dtcv.Rows[0]["LoadingExpenceLtr"]);
            hdnmachineltr.Value = Common.ConvertString(dtcv.Rows[0]["MachinaryExpenceLtr"]);
            hdnadminltr.Value = Common.ConvertString(dtcv.Rows[0]["AdminExpenceLtr"]);
            hdnextraltr.Value = Common.ConvertString(dtcv.Rows[0]["ExtraExpenceLtr"]);
            hdnLcharge.Value = Common.ConvertString(dtcv.Rows[0]["LabourCharge"]);
            hdnSvcharge.Value = Common.ConvertString(dtcv.Rows[0]["SuperVisorCharge"]);
            lblNetshifthour.Text= Common.ConvertString(dtcv.Rows[0]["NetShiftHours"]);
            hdnNetshiftHour.Value = Common.ConvertString(lblNetshifthour.Text);
        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("BulkProductWithPMRMCat", "", "");


            drpbpm.DataSource = dtrm;
            drpbpm.DataTextField = "Name";
            drpbpm.DataValueField = "Id";
            drpbpm.DataBind();

        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            InsertUpdateProductwiseLabourCost(1, 0);

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
        
                InsertUpdateProductwiseLabourCost(2, 0);
            
        }
        private void cleardata()
        {
            pwlcdata = new ProductwiseLabourCostBAL();
            hdnpwlcId.Value = "0";
            hdnbpmid.Value = "0";
            hdnextraltr.Value = "0";
            hdnLcharge.Value = "0";
            hdnloadingltr.Value = "0";
            hdnmachineltr.Value = "0";
            hdnPackingsizeCatId.Value = "0";
            hdnPackingStyleCatId.Value = "0";
            hdnpackingstyleId.Value = "0";
            hdnpacksize.Value = "0";
            hdnPMRMCategoryId.Value = "0";
            hdnSvcharge.Value = "0";
            hdnunloadingltr.Value = "0";

            txtfinalltrlabourcost.Text = "0";
            txtfinalunitlabourcost.Text = "0";
            txtnetlabourcostltr.Text = "0";
            txtnoofworker.Text = "0";
            txtnoselfillingline.Text = "0";
            txtpackdiscription.Text = "0";
            txtpacktsylepowerXUnitcost.Text = "0";
            txtstocknosel.Text = "0";
            txttotalcosting.Text = "0";
            txttotalopinltr.Text = "0";
            txtttllaboursupercost.Text = "0";
            txtsupervisor.Text = "0";

            txtttloutminutfilling.Text="0";
            txtoutputinliterkgshift.Text="0";
            txtttlopbottlenetshifthour.Text = "0";
            txttotalpowercost.Text = "0";
            txtunloading.Text = "0";
            txtloading.Text="0";
            txtmachinemaintanance.Text = "0";
            txtadminexpence.Text = "0";
            txtextraexpence.Text="0";
            txtadditionalcost.Text = "0";

            drpbpm.ClearSelection();
            drppacksize.ClearSelection();
            drpps.ClearSelection();
            drppsc.ClearSelection();


        }
        private void InsertUpdateProductwiseLabourCost(int act, int ProductwiseLaborCostId)
        {

            if (act == 3)
            {
                pwlcdata.ProductwiseLaborCostId = Common.ConvertInt(ProductwiseLaborCostId);
                pwlcdata.action = act;
                pwlcdata.UserId = Common.ConvertInt(Session["UserId"]);
                pwlcdata.PackingDescription = "";
            }
            else
            {
                pwlcdata.ProductwiseLaborCostId = Common.ConvertInt(hdnpwlcId.Value);
                pwlcdata.action = act;
                pwlcdata.StorckNosel = Common.ConvertInt(txtstocknosel.Text);
                pwlcdata.NoselsPerFillingLine = Common.ConvertInt(txtnoselfillingline.Text);
                pwlcdata.FkPackingStyleId = Common.ConvertInt(hdnpackingstyleId.Value);
                pwlcdata.FkPackingSizeCategoryId = Common.ConvertInt(hdnPackingsizeCatId.Value);
                pwlcdata.FkBulkProductId = Common.ConvertInt(hdnbpmid.Value);
                pwlcdata.FkPackingMaterialId = Common.ConvertInt(hdnpacksize.Value);
                pwlcdata.PMRMCategoryId = Common.ConvertInt(hdnPMRMCategoryId.Value);
                pwlcdata.Supervisiors = Common.ConvertDecimal(txtsupervisor.Text);
                pwlcdata.AdditionalCostBuffer = Common.ConvertDecimal(txtadditionalcost.Text);
                pwlcdata.PackingDescription = Common.ConvertString(txtpackdiscription.Text);
                pwlcdata.UserId = Common.ConvertInt(Session["UserId"]);

            }
            ReturnMessage obj = pwlc.InsertUpdateProductwiseLabourCost(pwlcdata);
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int ProductwiseLaborCostId = Common.ConvertInt(btn.CommandArgument);
            if (ProductwiseLaborCostId > 0)
            {
                DataTable dt = pwlc.Get_ProductwiseLabourCost(Common.ConvertInt(Session["UserId"]), ProductwiseLaborCostId);
                if (dt.Rows.Count > 0)
                {
                    hdnpwlcId.Value = Common.ConvertString(dt.Rows[0]["ProductwiseLaborCostId"]);
                    hdnbpmid.Value = Common.ConvertString(dt.Rows[0]["FkBulkProductId"]);
                    binddropdown();

                    string BulkIdAndCate = dt.Rows[0]["FkBulkProductId"].ToString() + "-" + dt.Rows[0]["PMRMCategoryId"].ToString();
                   string dbPackSize = Common.ConvertString(dt.Rows[0]["PackingSize"]);
                    string dbMeasurementName = Common.ConvertString(dt.Rows[0]["EnumDescription"]);

                    hdnpacksize.Value = dbPackSize;
                    hdnunitmeasurementName.Value = dbMeasurementName;
                    drpbpm.SelectedValue = BulkIdAndCate;

                     txtsize.Text = hdnpacksize.Value+"-"+ dbMeasurementName;

                    txtstocknosel.Text = Common.ConvertString(dt.Rows[0]["StorckNosel"]);
                    txtnoselfillingline.Text = Common.ConvertString(dt.Rows[0]["NoselsPerFillingLine"]);
                    hdnPMRMCategoryId.Value = Common.ConvertString(dt.Rows[0]["PMRMCategoryId"]);
                     txtsupervisor.Text = Common.ConvertString(dt.Rows[0]["Supervisiors"]);
                    txtadditionalcost.Text = Common.ConvertString(dt.Rows[0]["AdditionalCostBuffer"]);
                    txtadditionalcost.Text = Common.ConvertString(dt.Rows[0]["AdditionalCostBuffer"]);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "  setvalues('" + Common.ConvertString(dt.Rows[0]["FkPackingMaterialId"]) + "','" + Common.ConvertString(dt.Rows[0]["FkPackingSizeCategoryId"]) + "','" + Common.ConvertString(dt.Rows[0]["FkPackingStyleId"]) + "');calculaterate();", true);
                
                    btnadd.Visible = false;
                    btnupdate.Visible = true;

                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int ProductwiseLaborCostId = Common.ConvertInt(btn.CommandArgument);
            if (ProductwiseLaborCostId > 0)
            {
                InsertUpdateProductwiseLabourCost(3, ProductwiseLaborCostId);
            }
        }
    }
}