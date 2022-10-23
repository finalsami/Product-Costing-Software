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
    public partial class FactoryExpense : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        FactoryexpenceDAL fact = new FactoryexpenceDAL();
        FactoryexpenceBAL factdata = new FactoryexpenceBAL();
        int CompanyId = 0;
        int UserId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CompanyId = Common.ConvertInt(Session["CompanyId"]);
                UserId = Common.ConvertInt(Session["UserId"]);
                binddata();
                binddropdown();

            }
        }
        private void binddata()
        {

            DataTable dt = fact.Get_FactoryExpence(0, Common.ConvertInt(Session["CompanyId"]));
            gvcwfe.DataSource = dt;
            gvcwfe.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvcwfe.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvcwfe.UseAccessibleHeader = true;
            }
        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("BulkProductFromPackingMaterial", "", "");

            drpbpmaster.DataSource = dtrm;
            drpbpmaster.DataTextField = "Name";
            drpbpmaster.DataValueField = "Id";
            drpbpmaster.DataBind();


        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_FactoryExpence(2, 0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_FactoryExpence(1, 0);
            }
        }
        private void InsertUpdate_FactoryExpence(int act, int FactoryExpenseId)
        {

            if (act == 3)
            {
                factdata.FactoryExpenseId = Common.ConvertInt(FactoryExpenseId);
                factdata.action = act;
                factdata.UserId = Common.ConvertInt(Session["UserId"]);
                factdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
            }
            else
            {
                factdata.FkPackingMaterialId = Common.ConvertInt(drpbpmaster.SelectedValue);
                if (act == 1)
                {

                    string BpmId = Common.ConvertString(factdata.FkPackingMaterialId);
                    ReturnMessage objs = common.CheckExist("BulkProductInterestMaster", BpmId, "", "");
                    string msgs = Common.ConvertString(objs.Message);

                    if (Common.ConvertInt(objs.ReturnValue) == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);

                    }
                    else
                    {
                        factdata.action = act;
                        factdata.FactoryExpensePercentage = Common.ConvertInt(txtfactexpercent.Text);
                        factdata.MarketedChargePercentage = Common.ConvertInt(txtmrktchrgepercent.Text);
                        factdata.OtherPercentage = Common.ConvertInt(txtotherpercent.Text);
                        factdata.ProfitPercentage = Common.ConvertInt(txtprofitpercent.Text);
                        factdata.FkPackingMaterialId = Common.ConvertInt(drpbpmaster.SelectedValue);
                        factdata.FkBulkProductId = Common.ConvertInt(hdnBulkProductId.Value);
                        factdata.UserId = Common.ConvertInt(Session["UserId"]);
                        factdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);

                    }
                }
                else
                {
                    factdata.action = act;
                    factdata.FactoryExpenseId = Common.ConvertInt(hdncwfeid.Value);
                    factdata.FactoryExpensePercentage = Common.ConvertInt(txtfactexpercent.Text);
                    factdata.MarketedChargePercentage = Common.ConvertInt(txtmrktchrgepercent.Text);
                    factdata.OtherPercentage = Common.ConvertInt(txtotherpercent.Text);
                    factdata.ProfitPercentage = Common.ConvertInt(txtprofitpercent.Text);
                    factdata.FkBulkProductId = Common.ConvertInt(hdnBulkProductId.Value);
                    factdata.UserId = Common.ConvertInt(Session["UserId"]);
                    factdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);


                }



            }
            ReturnMessage obj = fact.InsertUpdate_FactoryExpence(factdata);
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
        public void cleardata()
        {
            fact = new FactoryexpenceDAL();
            hdncwfeid.Value = "0";
            txtBulkcost.Text = "";
            drpbpmaster.ClearSelection();
            txtfactexpAmt.Text = "";
            txtfactexpercent.Text = "";
            txtmrktchrgeAmt.Text = "";
            txtmrktchrgepercent.Text = "";
            txtotherpercent.Text = "";
            txtotherAmt.Text = "";
            txtprofitpercent.Text = "";
            txtprofitAmt.Text = "";
            txttotalcostltr.Text = "";
            txttotalexp.Text = "";
            drpbpmaster.Enabled = true;
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
            int FactoryExpenseId = Common.ConvertInt(btn.CommandArgument);
            int CompanyId= Common.ConvertInt(Session["CompanyId"]);
            if (FactoryExpenseId > 0)
            {
                DataTable dt = fact.Get_FactoryExpence(FactoryExpenseId, CompanyId);
                if (dt.Rows.Count > 0)
                {


                    hdncwfeid.Value = Common.ConvertString(dt.Rows[0]["FactoryExpenseId"]);
                    drpbpmaster.SelectedValue = Common.ConvertString(dt.Rows[0]["FkPackingMaterialId"]);
                    hdnBulkProductId.Value = Common.ConvertString(dt.Rows[0]["FkBulkProductId"]);

                    txtBulkcost.Text = Common.ConvertString(dt.Rows[0]["CostPerLtr"]);

                    txtfactexpercent.Text = Common.ConvertString(dt.Rows[0]["FactoryExpensePercentage"]);

                    txtmrktchrgepercent.Text = Common.ConvertString(dt.Rows[0]["MarketedChargePercentage"]);

                    txtotherpercent.Text = Common.ConvertString(dt.Rows[0]["OtherPercentage"]);

                    txtprofitpercent.Text = Common.ConvertString(dt.Rows[0]["ProfitPercentage"]);
             

                    lblmasterpack.Text = Common.ConvertString(dt.Rows[0]["MasterPack"]);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "calculaterate()", true);

                    drpbpmaster.Enabled = false;

                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int FactoryExpenceId = Common.ConvertInt(btn.CommandArgument);
            if (FactoryExpenceId > 0)
            {
                InsertUpdate_FactoryExpence(3, FactoryExpenceId);
            }
        }

        
    }
}