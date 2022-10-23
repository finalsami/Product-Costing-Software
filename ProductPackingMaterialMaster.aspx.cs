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
    public partial class ProductPackingMaterialMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();     
        PackingMaterialBAL PMMdata = new PackingMaterialBAL();

        PackingMaterialDAL PMM = new PackingMaterialDAL();

        PackingMaterialCostBAL PMMCostdata = new PackingMaterialCostBAL();

        PackingMaterialCostDAL PMMCost = new PackingMaterialCostDAL();

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
            DataTable dtrm = common.DropdownList("BulkProduct", "", "");

            drpproduct.DataSource = dtrm;
            drpproduct.DataTextField = "Name";
            drpproduct.DataValueField = "Id";
            drpproduct.DataBind();

            DataTable dtunit = common.DropdownList("Unit_Measurement", "", "");

            drppackingmeasurement.DataSource = dtunit;
            drppackingmeasurement.DataTextField = "Name";
            drppackingmeasurement.DataValueField = "Id";
            drppackingmeasurement.DataBind();
            

            drpunitmeasurement.DataSource = dtunit;
            drpunitmeasurement.DataTextField = "Name";
            drpunitmeasurement.DataValueField = "Id";
            drpunitmeasurement.DataBind();

            drpinnerunit.DataSource = dtunit;
            drpinnerunit.DataTextField = "Name";
            drpinnerunit.DataValueField = "Id";
            drpinnerunit.DataBind();           


            DataTable dtshipper = common.DropdownList("Shipper", "", "");

            drpshippertype.DataSource = dtshipper;
            drpshippertype.DataTextField = "Name";
            drpshippertype.DataValueField = "Id";
            drpshippertype.DataBind();

            DataTable dtinner = common.DropdownList("Inner", "", "");

            drpinnertype.DataSource = dtinner;
            drpinnertype.DataTextField = "Name";
            drpinnertype.DataValueField = "Id";
            drpinnertype.DataBind();

            DataTable dtcategory = common.DropdownList("PMRMMaster", "", "");

            drpackingcategory.DataSource = dtcategory;
            drpackingcategory.DataTextField = "Name";
            drpackingcategory.DataValueField = "Id";
            drpackingcategory.DataBind();

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateProductPackingMaster(2, 0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateProductPackingMaster(1, 0);
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }
        private void InsertUpdateProductPackingMaster(int act, int PackingMaterialId)
        {

            if (act == 3)
            {
                PMMdata.PackingMaterialId = Common.ConvertInt(PackingMaterialId);
                PMMdata.action = act;
                PMMdata.UserId = Common.ConvertInt(Session["UserId"]);
                PMMdata.FkPackingCategoryId = 0;
                PMMdata.FkBulkProductId = 0;
                PMMdata.PackingName = "";
                PMMdata.PackingSize = 0;
                PMMdata.PackingMeasurementId = 0;
                PMMdata.ShipperSize = 0;
                PMMdata.UnitMeasurementId = 0;
                PMMdata.FkPMRMCategoryId = 0;
                PMMdata.IsMasterPacking = false;
                PMMdata.InnerPackingCategoryId = 0;
                PMMdata.InnerSize = 0;
                PMMdata.InnerPackingMeasurementId = 0;
            }
            else
            {              

                PMMdata.PackingMaterialId = Common.ConvertInt(hdnmaterialid.Value);
                PMMdata.FkBulkProductId = Common.ConvertInt(drpproduct.SelectedValue);
                PMMdata.PackingName = Common.ConvertString(txtpackingname.Text);
                PMMdata.PackingSize = Common.ConvertDecimal(txtpackingsize.Text);
                PMMdata.PackingMeasurementId = Common.ConvertInt(drppackingmeasurement.SelectedValue);
                PMMdata.ShipperSize = Common.ConvertDecimal(txtshippersize.Text);
                PMMdata.UnitMeasurementId = Common.ConvertInt(drpunitmeasurement.SelectedValue);
                PMMdata.FkPMRMCategoryId = Common.ConvertInt(drpshippertype.SelectedValue);
                PMMdata.IsMasterPacking = Common.ConvertBool(chkismaster.Checked);
                PMMdata.InnerPackingCategoryId = Common.ConvertInt(drpinnertype.SelectedValue);
                PMMdata.InnerSize = Common.ConvertDecimal(txtinnersize.Text);
                PMMdata.InnerPackingMeasurementId = Common.ConvertInt(drpinnerunit.SelectedValue);
                PMMdata.UserId = Common.ConvertInt(Session["UserId"]);
                PMMdata.action = act;
                PMMdata.FkPackingCategoryId = Common.ConvertInt(drpackingcategory.SelectedValue);

            }
            ReturnMessage obj = PMM.InsertUpdatePackingMaterialMaster(PMMdata);
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
        private void binddata()
        {

            DataTable dt = PMM.ProductPackingMaterialMasterList(0,0,1);
            gvpackingmaster.DataSource = dt;
            gvpackingmaster.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvpackingmaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvpackingmaster.UseAccessibleHeader = true;
            }

        }

        private void cleardata()
        {
            hdnmaterialid.Value = "";
            drpproduct.SelectedValue = "0";
            drpackingcategory.SelectedValue = "0";
            txtpackingname.Text = "";
            txtpackingsize.Text = "";
            drppackingmeasurement.SelectedValue="0";    
            chkismaster.Checked=false;
            lblmasterpackingname.Text = "";
            chkshippertype.Checked = false;
            drpshippertype.SelectedValue = "0";
            txtshippersize.Text = "";
            drpunitmeasurement.SelectedValue = "0";
            lblunitshipper.Text = "";
            chkinner.Checked= false;
            drpinnertype.SelectedValue="0";
            txtinnersize.Text = "";
            drpinnerunit.SelectedValue="0";
            txtinnerunitshipper.Text = "";

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PackingMaterialId = Common.ConvertInt(btn.CommandArgument);
            if (PackingMaterialId > 0)
            {
                DataTable dt = PMM.ProductPackingMaterialMasterList(Common.ConvertInt(Session["UserId"]), PackingMaterialId,0);
                if (dt.Rows.Count > 0)
                {
                    hdnmaterialid.Value = Common.ConvertString(dt.Rows[0]["PackingMaterialId"]);
                    if (drpproduct.Items.FindByValue(Common.ConvertString(dt.Rows[0]["FkBulkProductId"])) != null)
                    {
                        drpproduct.SelectedValue = Common.ConvertString(dt.Rows[0]["FkBulkProductId"]);

                    }                  
                   
                    txtpackingname.Text = Common.ConvertString(dt.Rows[0]["packingName"]);                   
                                       
                    if (drppackingmeasurement.Items.FindByValue(Common.ConvertString(dt.Rows[0]["PackingMeasurementId"])) != null)
                    {
                        drppackingmeasurement.SelectedValue = Common.ConvertString(dt.Rows[0]["PackingMeasurementId"]);

                    }

                    if (drpackingcategory.Items.FindByValue(Common.ConvertString(dt.Rows[0]["FkPackingCategoryId"])) != null)
                    {
                        drpackingcategory.SelectedValue = Common.ConvertString(dt.Rows[0]["FkPackingCategoryId"]);

                    }

                    txtpackingsize.Text = Common.ConvertString(dt.Rows[0]["packingSize"]);

                    if (Common.ConvertBool(dt.Rows[0]["isMasterPacking"])==false)
                    {
                        chkismaster.Checked = false;
                        chkismaster.Enabled = true;
                    }
                    else
                    {
                        chkismaster.Enabled = false;
                        chkismaster.Checked = true;
                    }

                    lblmasterpackingname.Text = Common.ConvertString(dt.Rows[0]["masterpack"]);

                    if (Common.ConvertInt(dt.Rows[0]["isShipper"]) == 0)
                    {
                        chkshippertype.Checked = false;
                    }
                    else
                    {
                        chkshippertype.Checked = true;
                    }

                    

                    if (drpshippertype.Items.FindByValue(Common.ConvertString(dt.Rows[0]["FkPMRMCategoryId"])) != null)
                    {
                        drpshippertype.SelectedValue = Common.ConvertString(dt.Rows[0]["FkPMRMCategoryId"]);

                    }
                   
                    txtshippersize.Text = Common.ConvertString(dt.Rows[0]["shipperSize"]);

                    if (drpunitmeasurement.Items.FindByValue(Common.ConvertString(dt.Rows[0]["UnitMeasurementId"])) != null)
                    {
                        drpunitmeasurement.SelectedValue = Common.ConvertString(dt.Rows[0]["UnitMeasurementId"]);

                    }                    


                    if (Common.ConvertInt(dt.Rows[0]["isInner"]) == 0)
                    {
                        chkinner.Checked = false;
                    }
                    else
                    {
                        chkinner.Checked = true;
                    }
                  

                    if (drpinnertype.Items.FindByValue(Common.ConvertString(dt.Rows[0]["InnerPackingCategoryId"])) != null)
                    {
                        drpinnertype.SelectedValue = Common.ConvertString(dt.Rows[0]["InnerPackingCategoryId"]);

                    }
                   
                    txtinnersize.Text = Common.ConvertString(dt.Rows[0]["innersize"]);

                    if (drpinnerunit.Items.FindByValue(Common.ConvertString(dt.Rows[0]["InnerPackingMeasurementId"])) != null)
                    {
                        drpinnerunit.SelectedValue = Common.ConvertString(dt.Rows[0]["InnerPackingMeasurementId"]);

                    }


                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "CalculateShipper();", true);
                    

                    btnadd.Visible = false;
                    btnupdate.Visible = true;

                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int PackingMaterialId = Common.ConvertInt(btn.CommandArgument);
            if (PackingMaterialId > 0)
            {
                InsertUpdateProductPackingMaster(3, PackingMaterialId);
            }
        }

        protected void btnEdit_Click1(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var result = Common.ConvertString(btn.CommandArgument);

            PackingMaterialCostingMaster(0, Common.ConvertInt(result.Split('@')[1]));

            hdnmatid.Value = Common.ConvertString(result.Split('@')[1]);
            hdnprdid.Value= Common.ConvertString(result.Split('@')[0]);

            SetDisplay(Common.ConvertInt(hdnmatid.Value));

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideCosting('1');TypeSelect('0')", true);
        }

        private void SetDisplay(int PackingMaterialId)
        {
            DataTable dt = PMMCost.PackingMaterialDetail(PackingMaterialId);
            if(dt.Rows.Count>0)
            {
                lblpname.Text = Common.ConvertString(dt.Rows[0]["BulkProductName"]);
                lblpack.Text = Common.ConvertString(dt.Rows[0]["pack"]);
                lblcat.Text = Common.ConvertString(dt.Rows[0]["Shipper"]);
            }
        }

        private void PackingMaterialCostingMaster(int ProductId,int MaterialId)
        {
            DataTable dt = PMMCost.ProductPackingMaterialCostingList(Common.ConvertInt(Session["UserId"]), ProductId, MaterialId);
            gvcosting.DataSource = dt;
            gvcosting.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvcosting.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvcosting.UseAccessibleHeader = true;


                hdntotalcostunit.Value = dt.Compute("sum(CostPerUnit)", "").ToString();
                hdntotalcostltr.Value = dt.Compute("sum(FinalPrice)", "").ToString();

                //gvcosting.Columns[4].FooterText = "Total Cost / Ltr:" + dt.Compute("sum(FinalPrice)", "").ToString();
                //gvcosting.Columns[5].FooterText = "Total Cost /Unit:" + dt.Compute("sum(CostPerUnit)", "").ToString();
            }

        }
        protected void btnEdit_ing_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int result = Common.ConvertInt(btn.CommandArgument);
            DataTable dtcost = PMMCost.ProductPackingMaterialCostingList(Common.ConvertInt(Session["UserId"]), result, Common.ConvertInt(hdnmatid.Value)); 
            if(dtcost.Rows.Count>0)
            {

                hdncostid.Value = Common.ConvertString(dtcost.Rows[0]["PackingMaterialCostingID"]);

                int type = 0;
                if (Common.ConvertBool(dtcost.Rows[0]["isShipper"]))
                {
                    type = 1;
                   // bindshipper("Shipper");
                }
                else if (Common.ConvertBool(dtcost.Rows[0]["isInner"]))
                {
                    type = 2;
                   // bindshipper("Inner");
                }
                else
                {
                    type = 0;
                   // bindshipper("ShipperOthers");
                }

                rdotype.SelectedValue = type.ToString();

                // drpshipper.SelectedValue = dtcost.Rows[0]["FkPMRMCategoryId"].ToString();

                btncostadd.Visible = false;
                btncostupdate.Visible = true;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideCosting('1');SelectCosting('" + type + "','" + dtcost.Rows[0]["FkPMRMCategoryId"].ToString() + "','" + dtcost.Rows[0]["FkPMRMId"].ToString() + "');", true);
            }
        }
        
        private void bindshipper(string type)
        {
            DataTable dtshipper = common.DropdownList(type, "", "");

            drpshipper.DataSource = dtshipper;
            drpshipper.DataTextField = "Name";
            drpshipper.DataValueField = "Id";
            drpshipper.DataBind();

        }

        protected void btnDelete_ing_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Button btn = (Button)sender;
                int result = Common.ConvertInt(btn.CommandArgument);
                InsertUpdateProductPackingCostingMaster(3, result);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideCosting('1');", true);
            }
        }

        protected void btncostupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateProductPackingCostingMaster(2, Common.ConvertInt(hdnmatid.Value));
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideCosting('1');", true);
            }
        }

        protected void btncostadd_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                InsertUpdateProductPackingCostingMaster(1, 0);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideCosting('1');", true);
            }
        }

        protected void btncostcancel_Click(object sender, EventArgs e)
        {
            clearcost();
            btncostadd.Visible = true;
            btncostupdate.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhideCosting('1'); bindshipper(0, 0);", true);
        }

        private void clearcost()
        {
            rdotype.Items[0].Selected = true;         
            lblcostunit.Text = "";
            lblfinalcost.Text = "";
        }
        private void InsertUpdateProductPackingCostingMaster(int act, int PackingMaterialCostingId)
        {

            if (act == 3)
            {
                PMMCostdata.PackingMaterialCostingId = Common.ConvertInt(PackingMaterialCostingId);
                PMMCostdata.action = act;
            }
            else
            {
                PMMCostdata.PackingMaterialCostingId = Common.ConvertInt(PackingMaterialCostingId);
                PMMCostdata.FkPackingMaterialId = Common.ConvertInt(hdnmatid.Value);
                PMMCostdata.FkPMRMCategoryId = Common.ConvertInt(hdsp.Value);
                PMMCostdata.FkPMRMId = Common.ConvertInt(hdrm.Value);
                PMMCostdata.UserId = Common.ConvertInt(Session["UserId"]);
                PMMCostdata.action = act;

            }

            hdnreload.Value = "1";

            ReturnMessage obj = PMMCost.InsertUpdatePackingMaterialMaster(PMMCostdata);
            string msg = Common.ConvertString(obj.Message);
            if (Common.ConvertInt(obj.ReturnValue) > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                cleardata();

                hdrm.Value = "";
                hdsp.Value = "";

                btncostadd.Visible = true;
                btncostupdate.Visible = false;

                PackingMaterialCostingMaster(0, Common.ConvertInt(hdnmatid.Value));
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);

            }
        }
    }
}