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
    public partial class StateWiseCostFactor : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        StateWiseCostFactorDAL swcf = new StateWiseCostFactorDAL();
        StateWiseCostFactorBAL swcfdata = new StateWiseCostFactorBAL();
        int CompanyId;
        int UserId;
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

            DataTable dt = swcf.Get_StateWiseCostFactor(Common.ConvertInt(Session["UserId"]), 0, Common.ConvertInt(Session["CompanyId"]));
            gvswcf.DataSource = dt;
            gvswcf.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvswcf.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvswcf.UseAccessibleHeader = true;
            }
        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("State", "", "");

            drpstate.DataSource = dtrm;
            drpstate.DataTextField = "Name";
            drpstate.DataValueField = "Id";
            drpstate.DataBind();


            DataTable dtpro = common.DropdownList("productcategory", "", "");

            drpproductcat.DataSource = dtpro;
            drpproductcat.DataTextField = "Name";
            drpproductcat.DataValueField = "Id";
            drpproductcat.DataBind();


        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_StateWiseCostFactor(2, 8);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_StateWiseCostFactor(1, 8);
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardatarpl();
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }
        public void cleardatarpl()
        {
            swcf = new StateWiseCostFactorDAL();
            hdnswcf.Value = "0";
            drpstate.ClearSelection();
            drpproductcat.ClearSelection();

            txtrplstaff.Text = "";
            txtrpldepo.Text = "";
            txtrplIncentive.Text = "";
            txtrplinterest.Text = "";
            txtrplmarket.Text = "";
            txtrplother.Text = "";
            txtrpltotal.Text = "";
            drpstate.Enabled = true;
            drpproductcat.Enabled = true;

            btnadd.Visible = true;
            btnupdate.Visible = false;

        }
        public void cleardatancr()
        {
            swcf = new StateWiseCostFactorDAL();
            hdnswcf.Value = "0";
            drpstate.ClearSelection();
            drpproductcat.ClearSelection();

            txtncrdepo.Text = "";
            txtncrincentive.Text = "";
            txtncrinterest.Text = "";
            txtncrmarket.Text = "";
            txtncrother.Text = "";
            txtncrtotal.Text = "";
            txtncrstaff.Text = "";
            drpstate.Enabled = true;
            drpproductcat.Enabled = true;

            btnncradd.Visible = true;
            btnncrupdate.Visible = false;
        }

        private void InsertUpdate_StateWiseCostFactor(int act, int Type)
        {
            swcfdata.UserId = Common.ConvertInt(Session["UserId"]);
            swcfdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);

            if (act == 3)
            {

                swcfdata.StateWiseCostFactorId = Common.ConvertInt(hdnswcf.Value);
                swcfdata.action = act;
            }


            else
            {
                if (act == 1 && Type == 8)
                {


                    swcfdata.action = act;
                    swcfdata.StaffExpense = Common.ConvertDecimal(txtrplstaff.Text);
                    swcfdata.DepoExpence = Common.ConvertDecimal(txtrpldepo.Text);
                    swcfdata.Incentive = Common.ConvertInt(txtrplIncentive.Text);
                    swcfdata.Interest = Common.ConvertInt(txtrplinterest.Text);
                    swcfdata.Marketing = Common.ConvertInt(txtrplmarket.Text);
                    swcfdata.Other = Common.ConvertInt(txtrplother.Text);
                    swcfdata.FkProductCategoryId = Common.ConvertInt(drpproductcat.SelectedValue);
                    swcfdata.FkStateId = Common.ConvertInt(drpstate.SelectedValue);
                    swcfdata.FkPriceTypeId = Type;


                }
                else if (act == 1 && Type == 9)
                {
                    swcfdata.action = act;
                    swcfdata.StaffExpense = Common.ConvertDecimal(txtncrstaff.Text);
                    swcfdata.DepoExpence = Common.ConvertDecimal(txtncrdepo.Text);
                    swcfdata.Incentive = Common.ConvertInt(txtncrincentive.Text);
                    swcfdata.Interest = Common.ConvertInt(txtncrinterest.Text);
                    swcfdata.Marketing = Common.ConvertInt(txtncrmarket.Text);
                    swcfdata.Other = Common.ConvertInt(txtncrother.Text);
                    swcfdata.FkProductCategoryId = Common.ConvertInt(drpproductcat.SelectedValue);
                    swcfdata.FkStateId = Common.ConvertInt(drpstate.SelectedValue);

                    swcfdata.FkPriceTypeId = Type;

                }


                else if (act == 2 && Type == 8)
                {
                    swcfdata.StateWiseCostFactorId = Common.ConvertInt(hdnswcf.Value);
                    swcfdata.action = act;
                    swcfdata.StaffExpense = Common.ConvertDecimal(txtrplstaff.Text);
                    swcfdata.DepoExpence = Common.ConvertDecimal(txtrpldepo.Text);
                    swcfdata.Incentive = Common.ConvertInt(txtrplIncentive.Text);
                    swcfdata.Interest = Common.ConvertInt(txtrplinterest.Text);
                    swcfdata.Marketing = Common.ConvertInt(txtrplmarket.Text);
                    swcfdata.Other = Common.ConvertInt(txtrplother.Text);
                    swcfdata.FkProductCategoryId = Common.ConvertInt(drpproductcat.SelectedValue);
                    swcfdata.FkStateId = Common.ConvertInt(drpstate.SelectedValue);

                    swcfdata.FkPriceTypeId = Type;

                }
                else if (act == 2 && Type == 9)
                {
                    swcfdata.StateWiseCostFactorId = Common.ConvertInt(hdnswcf.Value);

                    swcfdata.action = act;
                    swcfdata.StaffExpense = Common.ConvertDecimal(txtncrstaff.Text);
                    swcfdata.DepoExpence = Common.ConvertDecimal(txtncrdepo.Text);
                    swcfdata.Incentive = Common.ConvertInt(txtncrincentive.Text);
                    swcfdata.Interest = Common.ConvertInt(txtncrinterest.Text);
                    swcfdata.Marketing = Common.ConvertInt(txtncrmarket.Text);
                    swcfdata.Other = Common.ConvertInt(txtncrother.Text);
                    swcfdata.FkProductCategoryId = Common.ConvertInt(drpproductcat.SelectedValue);
                    swcfdata.FkStateId = Common.ConvertInt(drpstate.SelectedValue);

                    swcfdata.FkPriceTypeId = Type;


                }

            }
            ReturnMessage obj = swcf.InsertUpdate_StateWiseCostFactor(swcfdata);
            string msg = Common.ConvertString(obj.Message);
            if (Common.ConvertInt(obj.ReturnValue) > 0)
            {


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                cleardatarpl();
                cleardatancr();

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
            cleardatancr();
            cleardatarpl();
            Button btn = (Button)sender;
            int StateWiseCostFactorId = Common.ConvertInt(btn.CommandArgument);
            int CompanyId = Common.ConvertInt(Session["CompanyId"]);
            int UserId = Common.ConvertInt(Session["UserId"]);
            if (StateWiseCostFactorId > 0)
            {
                DataTable dt = swcf.Get_StateWiseCostFactor(UserId, StateWiseCostFactorId, CompanyId);
                if (dt.Rows.Count > 0)
                {
                    int Type = Common.ConvertInt(dt.Rows[0]["FkPriceTypeId"]);
                    hdnswcf.Value = Common.ConvertString(dt.Rows[0]["StateWiseCostFactorId"]);

                    if (Type == 8)
                    {
                        drpstate.SelectedValue = Common.ConvertString(dt.Rows[0]["FkStateId"]);
                        txtrplstaff.Text = Common.ConvertString(dt.Rows[0]["StaffExpense"]);
                        txtrpldepo.Text = Common.ConvertString(dt.Rows[0]["DepoExpence"]);
                        txtrplIncentive.Text = Common.ConvertString(dt.Rows[0]["Incentive"]);
                        txtrplinterest.Text = Common.ConvertString(dt.Rows[0]["Interest"]);
                        txtrplother.Text = Common.ConvertString(dt.Rows[0]["Other"]);
                        txtrplmarket.Text = Common.ConvertString(dt.Rows[0]["Marketing"]);
                        drpproductcat.SelectedValue = Common.ConvertString(dt.Rows[0]["FkProductCategoryId"]);
                        drpstate.SelectedValue = Common.ConvertString(dt.Rows[0]["FkStateId"]);

                        btnadd.Visible = false;
                        btnupdate.Visible = true;

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "calculaterate()", true);
                    }
                    else
                    {
                        drpstate.SelectedValue = Common.ConvertString(dt.Rows[0]["FkStateId"]);
                        txtncrstaff.Text = Common.ConvertString(dt.Rows[0]["StaffExpense"]);
                        txtncrdepo.Text = Common.ConvertString(dt.Rows[0]["DepoExpence"]);
                        txtncrincentive.Text = Common.ConvertString(dt.Rows[0]["Incentive"]);
                        txtncrinterest.Text = Common.ConvertString(dt.Rows[0]["Interest"]);
                        txtncrother.Text = Common.ConvertString(dt.Rows[0]["Other"]);
                        txtncrmarket.Text = Common.ConvertString(dt.Rows[0]["Marketing"]);
                        drpproductcat.SelectedValue = Common.ConvertString(dt.Rows[0]["FkProductCategoryId"]);
                        drpstate.SelectedValue = Common.ConvertString(dt.Rows[0]["FkStateId"]);

                        btnncradd.Visible = false;
                        btnncrupdate.Visible = true;

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "calculaterate()", true);
                    }


                    drpstate.Enabled = false;


                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int StateCostfactorId = Common.ConvertInt(btn.CommandArgument);
            if (StateCostfactorId > 0)
            {
                hdnswcf.Value = Common.ConvertString(StateCostfactorId);
                InsertUpdate_StateWiseCostFactor(3, 0);
            }
        }

        protected void btnncrupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_StateWiseCostFactor(2, 9);
            }
        }

        protected void btnncradd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_StateWiseCostFactor(1, 9);
            }
        }

        protected void btnncrcancel_Click(object sender, EventArgs e)
        {
            cleardatancr();
            btnncradd.Visible = true;
            btnncrupdate.Visible = false;
        }
    }
}