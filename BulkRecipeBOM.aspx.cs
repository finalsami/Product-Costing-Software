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
    public partial class BulkRecipeBOM : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        BulkRecipeBOMDAL bom = new BulkRecipeBOMDAL();
        BulkRecipeBOMBAL bomdata = new BulkRecipeBOMBAL();
        BulkRecipeBOMDetail bomdetaildata = new BulkRecipeBOMDetail();
        BulkRecipSPGR spgr = new BulkRecipSPGR();
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

            DataTable dt = bom.Get_BOMMaster(Common.ConvertInt(Session["UserId"]), 0);
            gvbulkrecipe.DataSource = dt;
            gvbulkrecipe.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvbulkrecipe.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvbulkrecipe.UseAccessibleHeader = true;
            }
           
        }

        private void binddropdown()
        {
            DataTable dtmc = common.DropdownList("Main_Category", "", "");

            drpmaincategory.DataSource = dtmc;
            drpmaincategory.DataTextField = "Name";
            drpmaincategory.DataValueField = "Id";
            drpmaincategory.DataBind();


            DataTable dtum = common.DropdownList("Unit_Measurement", "", "");

            drpunit.DataSource = dtum;
            drpunit.DataTextField = "Name";
            drpunit.DataValueField = "Id";
            drpunit.DataBind();

            DataTable dtbpm = common.DropdownList("BulkProduct", "", "");

            drpbpm.DataSource = dtbpm;
            drpbpm.DataTextField = "Name";
            drpbpm.DataValueField = "Id";
            drpbpm.DataBind();

            DataTable dtfm = common.DropdownList("Formulation_Master", "", "");

            drpformulation.DataSource = dtfm;
            drpformulation.DataTextField = "Name";
            drpformulation.DataValueField = "Id";
            drpformulation.DataBind();
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateBOMMaster(2, 0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                InsertUpdateBOMMaster(1, 0);
            }
        }
        private void InsertUpdateBOMMaster(int act, int BOMId)
        {

            if (act == 3)
            {
                bomdata.BOMId = Common.ConvertInt(BOMId);
                bomdata.action = act;
                bomdata.UserId = Common.ConvertInt(Session["UserId"]);

            }
            else
            {
                bomdata.BOMId = Common.ConvertInt(hdnbomid.Value);
                bomdata.action = act;
                bomdata.BatchSize = Common.ConvertInt(txtbatchsize.Text);
                bomdata.UnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                bomdata.FkBulkProductId = Common.ConvertInt(drpbpm.SelectedValue);
                bomdata.FkMainCategoryId = Common.ConvertInt(drpmaincategory.SelectedValue);
                bomdata.UserId = Common.ConvertInt(Session["UserId"]);

            }

            ReturnMessage objs = common.CheckExist("drppmrmnamechange", drpbpm.SelectedValue, "","");

            if (Common.ConvertInt(objs.ReturnValue) > 0)
            {

                ReturnMessage obj = bom.InsertUpdateBOMMaster(bomdata);
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
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + objs.Message + "')", true);
            }
        }
        private void cleardata()
        {
            bomdata = new BulkRecipeBOMBAL();
            hdnbomid.Value = "0";
            txtbatchsize.Text = "";
            drpunit.ClearSelection();
            drpmaincategory.ClearSelection();
            drpbpm.ClearSelection();

        }
        private void cleardata1()
        {
                     

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
            int BOMId = Common.ConvertInt(btn.CommandArgument);
            if (BOMId > 0)
            {
                DataTable dt = bom.Get_BOMMaster(Common.ConvertInt(Session["UserId"]), BOMId);
                if (dt.Rows.Count > 0)
                {
                    hdnbomid.Value = Common.ConvertString(dt.Rows[0]["BOMId"]);

                    if (drpmaincategory.Items.FindByValue(Common.ConvertString(dt.Rows[0]["FkMainCategoryId"])) != null)
                    {
                        drpmaincategory.SelectedValue = Common.ConvertString(dt.Rows[0]["FkMainCategoryId"]);

                    }

                    if (drpbpm.Items.FindByValue(Common.ConvertString(dt.Rows[0]["FkBulkProductId"])) != null)
                    {
                        drpbpm.SelectedValue = Common.ConvertString(dt.Rows[0]["FkBulkProductId"]);
                    }
                    if (drpunit.Items.FindByValue(Common.ConvertString(dt.Rows[0]["UnitMeasurementId"])) != null)
                    {
                        drpunit.SelectedValue = Common.ConvertString(dt.Rows[0]["UnitMeasurementId"]);
                    }

                    txtbatchsize.Text = Common.ConvertString(dt.Rows[0]["BatchSize"]);

                    btnadd.Visible = false;
                    btnupdate.Visible = true;

                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int BOMId = Common.ConvertInt(btn.CommandArgument);
            if (BOMId > 0)
            {
                InsertUpdateBOMMaster(3, BOMId);
            }
        }

        protected void btnInputTechnical_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int BOMId = Common.ConvertInt(btn.CommandArgument);
            if (BOMId > 0)
            {
                BindIngredient(BOMId);
            }
        }

        private void BindIngredient(int BOMId)
        {
            DataSet ds = bom.Get_BOMMasterDetail(Common.ConvertInt(Session["UserId"]), BOMId);
            if (ds.Tables.Count > 0)
            {
                hdnbomid.Value = Common.ConvertString(ds.Tables[0].Rows[0]["BOMId"]);
                lblBPM_Name.Text = Common.ConvertString(ds.Tables[0].Rows[0]["BulkProductName"]);
                lblMeasurement.Text = Common.ConvertString(ds.Tables[0].Rows[0]["Unit"]);
                lblBatchSize.Text = Common.ConvertString(ds.Tables[0].Rows[0]["BatchSize"]);
                lblMainCategoryName.Text = Common.ConvertString(ds.Tables[0].Rows[0]["MainCategoryName"]);
                drpformulation.SelectedValue = Common.ConvertString(ds.Tables[0].Rows[0]["FormulationId"]);
                txtbatchsizeinput.Text = Common.ConvertString(ds.Tables[0].Rows[0]["BatchSize"]);
                lblFormulationAddBuffer.Text = Common.ConvertString(ds.Tables[0].Rows[0]["FormulationCost"]);
                txtsprg.Text = Common.ConvertString(ds.Tables[0].Rows[0]["Spgr"]);
                txtformulationLost.Text = Common.ConvertString(ds.Tables[0].Rows[0]["FormulationLostPercentage"]);
                hdnMainCategoryId.Value = Common.ConvertString(ds.Tables[0].Rows[0]["FkMainCategoryId"]);
                hdnbulkproductId.Value = Common.ConvertString(ds.Tables[0].Rows[0]["FkBulkProductId"]);

                if( Common.ConvertInt(drpformulation.SelectedValue)>0)
                {
                    chkformulation.Checked = true;
                    drpformulation.Enabled = true;
                }
                else
                {
                    chkformulation.Checked = false;
                    drpformulation.Enabled = false;
                }
               
               
                gvbomdetail.DataSource = ds.Tables[1];
                gvbomdetail.DataBind();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvbomdetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvbomdetail.UseAccessibleHeader = true;
                }

                //gvbomdetail.Columns[3].FooterText = "Total:" + ds.Tables[1].Compute("sum(QuantityLtrKgDisplay)", "").ToString();
                hdnsumQuantityLtrKg.Value = ds.Tables[1].Compute("sum(QuantityLtrKgDisplay)", "").ToString();
                hdntotalsum.Value = ds.Tables[1].Compute("sum(total)", "").ToString();
                txttotalamount.Text= ds.Tables[1].Compute("sum(total)", "").ToString();
                chkSolvent.Checked = false;
                       
                lblsolvant.Text = "0.00";
                

                ChkReqruiredFormulation.Checked = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideTechnical('1');CheckChange();drpformulationchange();", true);

            }
        }

        protected void btnbomdetailcancel_Click(object sender, EventArgs e)
        {
            cleardata1();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideTechnical('1');", true);
        }

        protected void btnbomdetailadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateBOMMasterDetail(1, 0);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideTechnical('1');", true);
            }
           
        }

        protected void btnbomdetailupdate_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideTechnical('1');", true);
        }
        private void InsertUpdateBOMMasterDetail(int act, int IngredientId)
        {

            if (act == 3)
            {
                bomdetaildata.IngredientId = Common.ConvertInt(IngredientId);
                bomdetaildata.action = act;
                bomdetaildata.UserId = Common.ConvertInt(Session["UserId"]);
                bomdetaildata.IngredientName = "";
            }
            else
            {
                bomdetaildata.IngredientId = Common.ConvertInt(hdnIngredientId.Value);
                bomdetaildata.action = act;
                bomdetaildata.IngredientName = Common.ConvertString(txtsearchRM.Text);
                bomdetaildata.FkRMId = Common.ConvertInt(hdnsearchRm.Value);
                bomdetaildata.FkBOMId = Common.ConvertInt(hdnbomid.Value);

                if(chkSolvent.Checked)
                {
                    bomdetaildata.Solvant = Common.ConvertDecimal(lblsolvant.Text);
                    bomdetaildata.Formulation = 0;
                    bomdetaildata.QuantityLtrKg = 0;
                }
                else
                {
                    bomdetaildata.Solvant = 0;
                   bomdetaildata.Formulation = Common.ConvertDecimal(txtRequiredFormulation.Text);
                    bomdetaildata.QuantityLtrKg = Common.ConvertDecimal(txtInputKG.Text);
                }
                
                bomdetaildata.FkMainCategoryId = Common.ConvertInt(hdnMainCategoryId.Value);
                bomdetaildata.FkBulkProductId = Common.ConvertInt(hdnbulkproductId.Value);
                bomdetaildata.UserId = Common.ConvertInt(Session["UserId"]);

            }



            ReturnMessage obj = bom.InsertUpdateBOMMasterDetail(bomdetaildata);
            string msg = Common.ConvertString(obj.Message);
            if (Common.ConvertInt(obj.ReturnValue) > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                BindIngredient(Common.ConvertInt(hdnbomid.Value));
                cleardata1();

                btnbomdetailadd.Visible = true;
                
               
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);

            }
        }

        protected void btnIngredientEdit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideTechnical('1');", true);
        }

        protected void btnIngredientDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int IngredientId = Common.ConvertInt(btn.CommandArgument);
            if (IngredientId > 0)
            {
                InsertUpdateBOMMasterDetail(3, IngredientId);
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideTechnical('1');BomDetailCalculation();", true);
        }

        protected void btnbomdetailfinalupdate_Click(object sender, EventArgs e)
        {

            spgr.BOMId = Common.ConvertInt(hdnbomid.Value);
            spgr.action = 2;
            spgr.UserId = Common.ConvertInt(Session["userId"]);
            spgr.Spgr = Common.ConvertDecimal(txtsprg.Text);
            spgr.FormulationId = Common.ConvertInt(drpformulation.SelectedValue);
            spgr.FormulationLostPercentage = Common.ConvertDecimal(txtformulationLost.Text);

            ReturnMessage obj = bom.UpdateBOMSPGR(spgr);
            string msg = Common.ConvertString(obj.Message);
            if (Common.ConvertInt(obj.ReturnValue) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                BindIngredient(Common.ConvertInt(hdnbomid.Value));
                
            }
        }

        
    }
}