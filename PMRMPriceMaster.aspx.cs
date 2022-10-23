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
    public partial class PMRMPriceMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        PMRMPriceMasterDAL pmrmprice = new PMRMPriceMasterDAL();
        PMRMPriceMasterBAL pmrmpricedata = new PMRMPriceMasterBAL();
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

            DataTable dt = pmrmprice.Get_PMRMPriceMaster(Common.ConvertInt(Session["UserId"]), 0);
            gvpmrmpricemaster.DataSource = dt;
            gvpmrmpricemaster.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvpmrmpricemaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvpmrmpricemaster.UseAccessibleHeader = true;
            }
        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("PMRMCategory", "", "");

            drppmrmcat.DataSource = dtrm;
            drppmrmcat.DataTextField = "Name";
            drppmrmcat.DataValueField = "Id";
            drppmrmcat.DataBind();


            DataTable dtunit = common.DropdownList("PMRMMaster", "", "");

            drppmrmname.DataSource = dtunit;
            drppmrmname.DataTextField = "Name";
            drppmrmname.DataValueField = "Id";
            drppmrmname.DataBind();

           
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePMRMPriceMaste(2, 0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePMRMPriceMaste(1, 0);
            }
        }
        private void InsertUpdatePMRMPriceMaste(int act, int PMRMPriceId)
        {

            if (act == 3)
            {
                pmrmpricedata.PMRMPriceId = Common.ConvertInt(PMRMPriceId);
                pmrmpricedata.action = act;
                pmrmpricedata.UserId = Common.ConvertInt(Session["UserId"]);

            }
            else
            {
                if (act == 1)
                {

                    string PMRMCategoryId = Common.ConvertString(drppmrmcat.SelectedValue);
                    string PMRMId = Common.ConvertString(drppmrmname.SelectedValue);
                    ReturnMessage objs = common.CheckExist("PMRMPriceMaster", PMRMCategoryId, PMRMId,"");
                    string msgs = Common.ConvertString(objs.Message);
                    pmrmpricedata.UserId = Common.ConvertInt(Session["UserId"]);
                    if (Common.ConvertInt(objs.ReturnValue) == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                        cleardata();
                        return;
                    }
                    else
                    {
                        pmrmpricedata.PMRMPriceId = Common.ConvertInt(hdnpmrmpriceid.Value);
                        pmrmpricedata.action = act;
                        pmrmpricedata.FkPMRMCategoryId= Common.ConvertInt(drppmrmcat.SelectedValue);
                        pmrmpricedata.FkPMRMId= Common.ConvertInt(drppmrmname.SelectedValue);
                        pmrmpricedata.Price = Common.ConvertDecimal(txtprice.Text);
                        pmrmpricedata.TrasportationCost = Common.ConvertDecimal(txttransport.Text);
                        pmrmpricedata.Loss = Common.ConvertDecimal(txtloss.Text);
                        pmrmpricedata.UnitMeasurementId = Common.ConvertInt(hdnUnitMeasurementId.Value);

                    }
                }
                else
                {
                    pmrmpricedata.PMRMPriceId = Common.ConvertInt(hdnpmrmpriceid.Value);
                    pmrmpricedata.action = act;
                    pmrmpricedata.FkPMRMCategoryId = Common.ConvertInt(drppmrmcat.SelectedValue);
                    pmrmpricedata.FkPMRMId = Common.ConvertInt(drppmrmname.SelectedValue);
                    pmrmpricedata.Price = Common.ConvertDecimal(txtprice.Text);
                    pmrmpricedata.TrasportationCost = Common.ConvertDecimal(txttransport.Text);
                    pmrmpricedata.Loss = Common.ConvertDecimal(txtloss.Text);
                    pmrmpricedata.UnitMeasurementId = Common.ConvertInt(hdnUnitMeasurementId.Value);
                    pmrmpricedata.UserId = Common.ConvertInt(Session["UserId"]);

                }



            }
            ReturnMessage obj = pmrmprice.SP_InsertUpdate_PMRMPriceMaster(pmrmpricedata);
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
            pmrmprice = new PMRMPriceMasterDAL();
            hdnpmrmpriceid.Value = "0";
            txtloss.Text = "0.00";
            drppmrmcat.ClearSelection();
            drppmrmname.ClearSelection();
            txtnoofunit.Text = "0.00";
            txtPerunit.Text = "0.00";
            txtprice.Text = "0.00";
            txttransport.Text = "0.00";
            drppmrmname.Enabled = false;

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
            int PMRMPriceId = Common.ConvertInt(btn.CommandArgument);
            if (PMRMPriceId > 0)
            {
                DataTable dt = pmrmprice.Get_PMRMPriceMaster(Common.ConvertInt(Session["UserId"]),PMRMPriceId);
                if (dt.Rows.Count > 0)
                {
                    hdnpmrmpriceid.Value = Common.ConvertString(dt.Rows[0]["PMRMPriceId"]);
                    txtprice.Text = Common.ConvertString(dt.Rows[0]["Price"]);
                    txtnoofunit.Text = Common.ConvertString(dt.Rows[0]["Unit"]);
                    txtPerunit.Text = Common.ConvertString(dt.Rows[0]["FinalPrice"]);
                    txttransport.Text = Common.ConvertString(dt.Rows[0]["TrasportationCost"]);
                    txtloss.Text = Common.ConvertString(dt.Rows[0]["Loss"]);
                    drppmrmname.SelectedValue = Common.ConvertString(dt.Rows[0]["FkPMRMId"]);
                    drppmrmcat.SelectedValue = Common.ConvertString(dt.Rows[0]["FkPMRMCategoryId"]);
                    hdnUnitMeasurementId.Value = Common.ConvertString(dt.Rows[0]["UnitMeasurementId"]);
                    
                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMPriceId = Common.ConvertInt(btn.CommandArgument);
            if (RMPriceId > 0)
            {
                InsertUpdatePMRMPriceMaste(3, RMPriceId);
            }
        }
    }
}