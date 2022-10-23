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
    public partial class TrasportationCostMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        TrasportationCostMasterDAL tcm = new TrasportationCostMasterDAL();
        TrasportationCostMasterBAL tcmdata = new TrasportationCostMasterBAL();
        TrasportationCostFactorBAL tcfdata = new TrasportationCostFactorBAL();
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

            DataTable dt = tcm.Get_TrasportationCostMaster(Common.ConvertInt(Session["UserId"]), 0, Common.ConvertInt(Session["CompanyId"]));
            gvtcm.DataSource = dt;
            gvtcm.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvtcm.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvtcm.UseAccessibleHeader = true;
            }
        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("State", "", "");

            drpstate.DataSource = dtrm;
            drpstate.DataTextField = "Name";
            drpstate.DataValueField = "Id";
            drpstate.DataBind();


            DataTable dtunit = common.DropdownList("Unit_Measurement", "", "");

            drpunit.DataSource = dtunit;
            drpunit.DataTextField = "Name";
            drpunit.DataValueField = "Id";
            drpunit.DataBind();

            DataTable dtunload = common.DropdownList("Unit_Measurement", "", "");

            drpunitunload.DataSource = dtunload;
            drpunitunload.DataTextField = "Name";
            drpunitunload.DataValueField = "Id";
            drpunitunload.DataBind();


            DataTable dtcartage = common.DropdownList("Unit_Measurement", "", "");

            drpunitlocal.DataSource = dtunload;
            drpunitlocal.DataTextField = "Name";
            drpunitlocal.DataValueField = "Id";
            drpunitlocal.DataBind();
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_TransportationCostMaster(2, 0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_TransportationCostMaster(1, 0);
            }
        }
        private void InsertUpdate_TransportationCostMaster(int act, int TransportationCostId)
        {

            if (act == 3)
            {
                tcmdata.TransportationCostId = Common.ConvertInt(TransportationCostId);
                tcmdata.action = act;
                tcmdata.UserId = Common.ConvertInt(Session["UserId"]);
                tcmdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
            }
            else
            {
                tcmdata.FkStateId = Common.ConvertInt(drpstate.SelectedValue);
                if (act == 1)
                {

                    string FkStateId = Common.ConvertString(tcmdata.FkStateId);
                    ReturnMessage objs = common.CheckExist("TransportationCostMaster", FkStateId, "", "");
                    string msgs = Common.ConvertString(objs.Message);

                    if (Common.ConvertInt(objs.ReturnValue) == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);

                    }
                    else
                    {
                        tcmdata.action = act;
                        tcmdata.TruckLoadCharges = Common.ConvertDecimal(txttruckload.Text);
                        tcmdata.NoCarton = Common.ConvertDecimal(txtNoCarton.Text);
                        tcmdata.FkStateId = Common.ConvertInt(drpstate.SelectedValue);

                        tcmdata.UserId = Common.ConvertInt(Session["UserId"]);
                        tcmdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);

                    }
                }
                else
                {
                    tcmdata.action = act;
                    tcmdata.TransportationCostId = Common.ConvertInt(hdntcmid.Value);
                    tcmdata.action = act;
                    tcmdata.TruckLoadCharges = Common.ConvertDecimal(txttruckload.Text);
                    tcmdata.NoCarton = Common.ConvertDecimal(txtNoCarton.Text);
                    tcmdata.FkStateId = Common.ConvertInt(drpstate.SelectedValue);

                    tcmdata.UserId = Common.ConvertInt(Session["UserId"]);
                    tcmdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);


                }



            }
            ReturnMessage obj = tcm.InsertUpdate_TransportationCostMaster(tcmdata);
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
            tcm = new TrasportationCostMasterDAL();
            hdntcmid.Value = "0";
            drpstate.ClearSelection();
            txttruckload.Text = "";
            txtNoCarton.Text = "";
            txt1cartoncharge.Text = "";
            drpstate.Enabled = true;


           

        }
        public void cleardatafactor() {

            txtunloadstart.Text = "";
            txtunloadend.Text = "";
            txtunloadamount.Text = "";
            drpunitunload.SelectedValue = "0";

            txtaveragestart.Text = "";
            txtaverageend.Text = "";
            txtaverageAmt.Text = "";
            drpunit.SelectedValue = "0";

            txtlocalstart.Text = "";
            txtlocalend.Text = "";
            txtlocalamount.Text = "";
            drpunitlocal.SelectedValue = "0";
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
            int TransportationCostId = Common.ConvertInt(btn.CommandArgument);
            int CompanyId = Common.ConvertInt(Session["CompanyId"]);
            int UserId = Common.ConvertInt(Session["UserId"]);
            if (TransportationCostId > 0)
            {
                DataTable dt = tcm.Get_TrasportationCostMaster(UserId, TransportationCostId, CompanyId);
                if (dt.Rows.Count > 0)
                {


                    hdntcmid.Value = Common.ConvertString(dt.Rows[0]["TransportationCostId"]);
                    drpstate.SelectedValue = Common.ConvertString(dt.Rows[0]["FkStateId"]);
                    txtNoCarton.Text = Common.ConvertString(dt.Rows[0]["NoCarton"]);
                    txttruckload.Text = Common.ConvertString(dt.Rows[0]["TruckLoadCharges"]);
                    txt1cartoncharge.Text = Common.ConvertString(dt.Rows[0]["CartonChrage"]);



                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "calculaterate()", true);

                    drpstate.Enabled = false;

                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int TransportationCostId = Common.ConvertInt(btn.CommandArgument);
            if (TransportationCostId > 0)
            {
                InsertUpdate_TransportationCostMaster(3, TransportationCostId);
            }
        }

        protected void btnaddaverage_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_TransportationCostFactor(1, 0, Common.ConvertInt(hdntcmid.Value), 1);
            }
        }
        private void InsertUpdate_TransportationCostFactor(int act, int CostfactorId, int CostMasterId, int Type)
        {

            if (act == 3 && Type == 1)
            {
                tcfdata.TransportationCostFactorId = Common.ConvertInt(CostfactorId);
                tcfdata.FkTransportationCostId = Common.ConvertInt(CostMasterId);

                tcfdata.action = act;
                tcfdata.UserId = Common.ConvertInt(Session["UserId"]);
                tcfdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
            }
            else if (act == 3 && Type == 2)
            {
                tcfdata.UnloadingCostFactorId = Common.ConvertInt(CostfactorId);
                tcfdata.FkTransportationCostId = Common.ConvertInt(CostMasterId);
                tcfdata.action = act;
                tcfdata.UserId = Common.ConvertInt(Session["UserId"]);
                tcfdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
            }
            else if (act == 3 && Type == 3)
            {
                tcfdata.CartageCostFactorId = Common.ConvertInt(CostfactorId);
                tcfdata.FkTransportationCostId = Common.ConvertInt(CostMasterId);

                tcfdata.action = act;
                tcfdata.UserId = Common.ConvertInt(Session["UserId"]);
                tcfdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
            }


            else
            {
                if (act == 1 && Type == 1)
                {


                    tcfdata.action = act;
                    tcfdata.Start = Common.ConvertDecimal(txtaveragestart.Text);
                    tcfdata.End = Common.ConvertDecimal(txtaverageend.Text);
                    tcfdata.Amount = Common.ConvertInt(txtaverageAmt.Text);
                    tcfdata.FkUnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                    tcfdata.FkTransportationCostId = Common.ConvertInt(CostMasterId);

                    tcfdata.TransportationCostFactorId = 0;
                    tcfdata.UnloadingCostFactorId = 0;
                    tcfdata.CartageCostFactorId = 0;
                    tcfdata.UserId = Common.ConvertInt(Session["UserId"]);
                    tcfdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);

                }
                else if (act == 1 && Type == 2)
                {

                    tcfdata.action = act;
                    tcfdata.Start = Common.ConvertDecimal(txtunloadstart.Text);
                    tcfdata.End = Common.ConvertDecimal(txtunloadend.Text);
                    tcfdata.Amount = Common.ConvertInt(txtunloadamount.Text);
                    tcfdata.FkUnitMeasurementId = Common.ConvertInt(drpunitunload.SelectedValue);
                    tcfdata.FkTransportationCostId = Common.ConvertInt(CostMasterId);

                    tcfdata.TransportationCostFactorId = 0;
                    tcfdata.UnloadingCostFactorId = 0;
                    tcfdata.CartageCostFactorId = 0;
                    tcfdata.UserId = Common.ConvertInt(Session["UserId"]);
                    tcfdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
                }
                else if (act == 1 && Type == 3)
                {
                    tcfdata.action = act;
                    tcfdata.Start = Common.ConvertDecimal(txtlocalstart.Text);
                    tcfdata.End = Common.ConvertDecimal(txtlocalend.Text);
                    tcfdata.Amount = Common.ConvertInt(txtlocalamount.Text);
                    tcfdata.FkUnitMeasurementId = Common.ConvertInt(drpunitlocal.SelectedValue);
                    tcfdata.FkTransportationCostId = Common.ConvertInt(CostMasterId);

                    tcfdata.TransportationCostFactorId = 0;
                    tcfdata.UnloadingCostFactorId = 0;
                    tcfdata.CartageCostFactorId = 0;
                    tcfdata.UserId = Common.ConvertInt(Session["UserId"]);
                    tcfdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
                }

                else if (act == 2 && Type == 1)
                {
                    tcfdata.action = act;
                    tcfdata.Start = Common.ConvertDecimal(txtaveragestart.Text);
                    tcfdata.End = Common.ConvertDecimal(txtaverageend.Text);
                    tcfdata.Amount = Common.ConvertInt(txtaverageAmt.Text);
                    tcfdata.FkUnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                    tcfdata.TransportationCostFactorId = Common.ConvertInt(hdnAveragechargeId.Value);
                    tcfdata.FkTransportationCostId = Common.ConvertInt(CostMasterId);

                    tcfdata.UnloadingCostFactorId = 0;
                    tcfdata.CartageCostFactorId = 0;
                    tcfdata.UserId = Common.ConvertInt(Session["UserId"]);
                    tcfdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
                }
                else if (act == 2 && Type == 2)
                {
                    tcfdata.action = act;
                    tcfdata.Start = Common.ConvertDecimal(txtunloadstart.Text);
                    tcfdata.End = Common.ConvertDecimal(txtunloadend.Text);
                    tcfdata.Amount = Common.ConvertInt(txtunloadamount.Text);
                    tcfdata.FkUnitMeasurementId = Common.ConvertInt(drpunitunload.SelectedValue);
                    tcfdata.FkTransportationCostId = Common.ConvertInt(CostMasterId);

                    tcfdata.TransportationCostFactorId = 0;
                    tcfdata.UnloadingCostFactorId = Common.ConvertInt(hdnunloadchargeId.Value);
                    tcfdata.CartageCostFactorId = 0;
                    tcfdata.UserId = Common.ConvertInt(Session["UserId"]);
                    tcfdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
                }
                else if (act == 2 && Type == 3)
                {
                    tcfdata.action = act;
                    tcfdata.Start = Common.ConvertDecimal(txtlocalstart.Text);
                    tcfdata.End = Common.ConvertDecimal(txtlocalend.Text);
                    tcfdata.Amount = Common.ConvertInt(txtlocalamount.Text);
                    tcfdata.FkUnitMeasurementId = Common.ConvertInt(drpunitlocal.SelectedValue);
                    tcfdata.FkTransportationCostId = Common.ConvertInt(CostMasterId);

                    tcfdata.TransportationCostFactorId = 0;
                    tcfdata.UnloadingCostFactorId = 0;
                    tcfdata.CartageCostFactorId = Common.ConvertInt(hdnlocalchargeId.Value);
                    tcfdata.UserId = Common.ConvertInt(Session["UserId"]);
                    tcfdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
                }

            }
            ReturnMessage obj = tcm.InsertUpdate_TransportationCostFactor(tcfdata, Type);
            string msg = Common.ConvertString(obj.Message);
            if (Common.ConvertInt(obj.ReturnValue) > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                cleardatafactor();

                btnadd.Visible = true;
                btnupdate.Visible = false;
                this.Response.Redirect(this.Request.Url.ToString());

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);

            }

            if (Type == 3)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "popup(3)", true);

            }
            else if (Type == 2)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "popup(2)", true);

            }
            else if (Type == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "popup(1)", true);
            }
        }

        protected void btnaveragepopup_Click(object sender, EventArgs e)
        {
            int CompanyId = Common.ConvertInt(Session["CompanyId"]);
            int UserId = Common.ConvertInt(Session["UserId"]);
            Button btn = (Button)sender;
            int TransportationCostfactorId = Common.ConvertInt(btn.CommandArgument);
            if (TransportationCostfactorId > 0)
            {
                DataTable dt = tcm.Get_TrasportationCostFactor(UserId, TransportationCostfactorId, 1, CompanyId);
                if (dt.Rows.Count > 0)
                {
                    lblstatename.Text = Common.ConvertString(dt.Rows[0]["StateName"]);
                    hdntcmid.Value = Common.ConvertString(dt.Rows[0]["FkTransportationCostId"]);
                    gvaverage.DataSource = dt;
                    gvaverage.DataBind();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "popup(1)", true);

                }

            }
        }

        protected void btnunloadingpopup_Click(object sender, EventArgs e)
        {
            int CompanyId = Common.ConvertInt(Session["CompanyId"]);
            int UserId = Common.ConvertInt(Session["UserId"]);
            Button btn = (Button)sender;
            int TransportationCostfactorId = Common.ConvertInt(btn.CommandArgument);
            if (TransportationCostfactorId > 0)
            {
                DataTable dt = tcm.Get_TrasportationCostFactor(UserId, TransportationCostfactorId, 2, CompanyId);
                if (dt.Rows.Count > 0)
                {
                    lblstatenameunload.Text = Common.ConvertString(dt.Rows[0]["StateName"]);
                    hdntcmid.Value = Common.ConvertString(dt.Rows[0]["FkTransportationCostId"]);

                    gvunloading.DataSource = dt;
                    gvunloading.DataBind();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "popup(2)", true);

                }

            }
        }

        protected void btnlocalchargeopup_Click(object sender, EventArgs e)
        {
            int CompanyId = Common.ConvertInt(Session["CompanyId"]);
            int UserId = Common.ConvertInt(Session["UserId"]);
            Button btn = (Button)sender;
            int TransportationCostfactorId = Common.ConvertInt(btn.CommandArgument);
            if (TransportationCostfactorId > 0)
            {
                DataTable dt = tcm.Get_TrasportationCostFactor(UserId, TransportationCostfactorId, 3, CompanyId);
                if (dt.Rows.Count > 0)
                {
                    lblstatenamecartage.Text = Common.ConvertString(dt.Rows[0]["StateName"]);
                    hdntcmid.Value = Common.ConvertString(dt.Rows[0]["FkTransportationCostId"]);

                    gvcartage.DataSource = dt;
                    gvcartage.DataBind();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "popup(3)", true);

                }

            }
        }

        protected void TransportReport_Click(object sender, EventArgs e)
        {
            int CompanyId = Common.ConvertInt(Session["CompanyId"]);
            int UserId = Common.ConvertInt(Session["UserId"]);
            Button btn = (Button)sender;
            int TransportationCostMasterId = Common.ConvertInt(btn.CommandArgument);
            if (TransportationCostMasterId > 0)
            {
                DataTable dt = tcm.Get_TrasportationCostFactor(UserId, TransportationCostMasterId, 1, CompanyId);
                if (dt.Rows.Count > 0)
                {
                    lblstatename.Text = Common.ConvertString(dt.Rows[0]["StateName"]);
                    hdntcmid.Value = Common.ConvertString(dt.Rows[0]["FkTransportationCostId"]);
                    int StateId = Common.ConvertInt(dt.Rows[0]["FkStateId"]);
                    gvaverage.DataSource = dt;
                    gvaverage.DataBind();
                    Session["StateId"] = StateId;
                    Session["TransportationCostId"] = hdntcmid.Value;


                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "redirectToReport()", true);

                }

            }
        }



        protected void btnDeleteAverage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int CostfactorId = Common.ConvertInt(btn.CommandArgument);
            if (CostfactorId > 0)
            {
                InsertUpdate_TransportationCostFactor(3, CostfactorId, Common.ConvertInt(hdntcmid.Value), 1);
            }
        }


        protected void btnDeleteUnload_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int CostfactorId = Common.ConvertInt(btn.CommandArgument);
            if (CostfactorId > 0)
            {
                InsertUpdate_TransportationCostFactor(3, CostfactorId, Common.ConvertInt(hdntcmid.Value), 2);
            }
        }


        protected void btnDeleteLocal_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int CostfactorId = Common.ConvertInt(btn.CommandArgument);
            if (CostfactorId > 0)
            {
                InsertUpdate_TransportationCostFactor(3, CostfactorId, Common.ConvertInt(hdntcmid.Value), 3);
            }
        }

        protected void btnAddunload_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_TransportationCostFactor(1, 0, Common.ConvertInt(hdntcmid.Value), 2);
            }
        }

        protected void btnAddlocal_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_TransportationCostFactor(1, 0, Common.ConvertInt(hdntcmid.Value), 3);
            }
        }
    }
}