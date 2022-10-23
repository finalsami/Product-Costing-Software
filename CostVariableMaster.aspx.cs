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
    public partial class CostVariableMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        CostVariableMasterDAL cv = new CostVariableMasterDAL();
        CostVariableMasterBAL cvdata = new CostVariableMasterBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }
        private void binddata()
        {

            DataTable dt = cv.GetOtherVariable(Common.ConvertInt(Session["UserId"]), 0);
            gvcvmaster.DataSource = dt;
            gvcvmaster.DataBind();
            if (dt.Rows.Count > 0)
            {
                hdncvmId.Value = Common.ConvertString(dt.Rows[0]["OtherVariableId"]);

                txtshittime.Text = Common.ConvertString(dt.Rows[0]["ShiftHours"]);
                txtbreakandothertimming.Text = Common.ConvertString(dt.Rows[0]["BreakHours"]);
                txtnetshifthour.Text = Common.ConvertString(dt.Rows[0]["NetShiftHours"]);
                txtpowerunitprice.Text = Common.ConvertString(dt.Rows[0]["PowerUnitPrice"]);
                txtlchargeperday.Text = Common.ConvertString(dt.Rows[0]["LabourCharge"]);
                txtsupervisorcost.Text = Common.ConvertString(dt.Rows[0]["SuperVisorCharge"]);
                txtunloadingexpenceltr.Text = Common.ConvertString(dt.Rows[0]["UnloadingExpenseLtr"]);
                txtunloadingexpencekg.Text = Common.ConvertString(dt.Rows[0]["UnloadingExpenseKg"]);
                txtunloadingexpenceunit.Text = Common.ConvertString(dt.Rows[0]["UnloadingExpenseUnit"]);
                txtloadingexpenceperltr.Text = Common.ConvertString(dt.Rows[0]["LoadingExpenceLtr"]);
                txtloadingexpenceperkg.Text = Common.ConvertString(dt.Rows[0]["LoadingExpenceKg"]);
                txtloadingexpenceperunit.Text = Common.ConvertString(dt.Rows[0]["LoadingExpenceUnit"]);
                txtmachinmaitexpenceltr.Text = Common.ConvertString(dt.Rows[0]["MachinaryExpenceLtr"]);
                txtmachinmaitexpencekg.Text = Common.ConvertString(dt.Rows[0]["MachinaryExpenceKg"]);
                txtmachinmaitexpenceunit.Text = Common.ConvertString(dt.Rows[0]["MachinaryExpenceUnit"]);
                txtadminexpenceltr.Text = Common.ConvertString(dt.Rows[0]["AdminExpenceLtr"]);
                txtadminexpencekg.Text = Common.ConvertString(dt.Rows[0]["AdminExpenceKg"]);
                txtadminexpenceunit.Text = Common.ConvertString(dt.Rows[0]["AdminExpenceLtr"]);
                txtextraexpenceltr.Text = Common.ConvertString(dt.Rows[0]["ExtraExpenceLtr"]);
                txtextraexpencekg.Text = Common.ConvertString(dt.Rows[0]["ExtraExpenceKg"]);
                txtextraexpenceunit.Text = Common.ConvertString(dt.Rows[0]["ExtraExpenceUnit"]);

                btnupdate.Visible = true;
                btnadd.Visible = false;

            }
            else
            {
                btnupdate.Visible = false;
                btnadd.Visible = true;
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateOtherVariable(2, 0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateOtherVariable(1, 0);
            }
        }
        private void InsertUpdateOtherVariable(int act, int OtherVariableId)
        {


            if (act == 1)
            {
                cvdata.action = act;
                cvdata.ShiftHours=Common.ConvertDecimal(txtshittime.Text);
                cvdata.BreakHours=Common.ConvertDecimal(txtbreakandothertimming.Text);
                cvdata.NetShiftHours=Common.ConvertDecimal(txtnetshifthour.Text);
                cvdata.PowerUnitPrice=Common.ConvertDecimal(txtpowerunitprice.Text);
                cvdata.LabourCharge=Common.ConvertDecimal(txtlchargeperday.Text);
                cvdata.SuperVisorCharge=Common.ConvertDecimal(txtsupervisorcost.Text);
                cvdata.UnloadingExpenseLtr=Common.ConvertDecimal(txtunloadingexpenceltr.Text);
                cvdata.UnloadingExpenseKg=Common.ConvertDecimal(txtunloadingexpencekg.Text);
                cvdata.UnloadingExpenseUnit=Common.ConvertDecimal(txtunloadingexpenceunit.Text);
                cvdata.LoadingExpenceLtr=Common.ConvertDecimal(txtloadingexpenceperltr.Text);
                cvdata.LoadingExpenceKg=Common.ConvertDecimal(txtloadingexpenceperkg.Text);
                cvdata.LoadingExpenceUnit=Common.ConvertDecimal(txtloadingexpenceperunit.Text);
                cvdata.MachinaryExpenceLtr=Common.ConvertDecimal(txtmachinmaitexpenceltr.Text);
                cvdata.MachinaryExpenceKg=Common.ConvertDecimal(txtmachinmaitexpencekg.Text);
                cvdata.MachinaryExpenceUnit=Common.ConvertDecimal(txtmachinmaitexpenceunit.Text);
                cvdata.AdminExpenceLtr=Common.ConvertDecimal(txtadminexpenceltr.Text);
                cvdata.AdminExpenceKg=Common.ConvertDecimal(txtadminexpencekg.Text);
                cvdata.AdminExpenceUnit=Common.ConvertDecimal(txtadminexpenceunit.Text);
                cvdata.ExtraExpenceLtr=Common.ConvertDecimal(txtextraexpenceltr.Text);
                cvdata.ExtraExpenceKg=Common.ConvertDecimal(txtextraexpencekg.Text);
                cvdata.ExtraExpenceUnit=Common.ConvertDecimal(txtextraexpenceunit.Text); 
                cvdata.UserId= Common.ConvertInt(Session["UserId"]);
            }

            else
            {
                cvdata.OtherVariableId = Common.ConvertInt(hdncvmId.Value);
                cvdata.action = act;
                cvdata.ShiftHours = Common.ConvertDecimal(txtshittime.Text);
                cvdata.BreakHours = Common.ConvertDecimal(txtbreakandothertimming.Text);
                cvdata.NetShiftHours = Common.ConvertDecimal(txtnetshifthour.Text);
                cvdata.PowerUnitPrice = Common.ConvertDecimal(txtpowerunitprice.Text);
                cvdata.LabourCharge = Common.ConvertDecimal(txtlchargeperday.Text);
                cvdata.SuperVisorCharge = Common.ConvertDecimal(txtsupervisorcost.Text);
                cvdata.UnloadingExpenseLtr = Common.ConvertDecimal(txtunloadingexpenceltr.Text);
                cvdata.UnloadingExpenseKg = Common.ConvertDecimal(txtunloadingexpencekg.Text);
                cvdata.UnloadingExpenseUnit = Common.ConvertDecimal(txtunloadingexpenceunit.Text);
                cvdata.LoadingExpenceLtr = Common.ConvertDecimal(txtloadingexpenceperltr.Text);
                cvdata.LoadingExpenceKg = Common.ConvertDecimal(txtloadingexpenceperkg.Text);
                cvdata.LoadingExpenceUnit = Common.ConvertDecimal(txtloadingexpenceperunit.Text);
                cvdata.MachinaryExpenceLtr = Common.ConvertDecimal(txtmachinmaitexpenceltr.Text);
                cvdata.MachinaryExpenceKg = Common.ConvertDecimal(txtmachinmaitexpencekg.Text);
                cvdata.MachinaryExpenceUnit = Common.ConvertDecimal(txtmachinmaitexpenceunit.Text);
                cvdata.AdminExpenceLtr = Common.ConvertDecimal(txtadminexpenceltr.Text);
                cvdata.AdminExpenceKg = Common.ConvertDecimal(txtadminexpencekg.Text);
                cvdata.AdminExpenceUnit = Common.ConvertDecimal(txtadminexpenceunit.Text);
                cvdata.ExtraExpenceLtr = Common.ConvertDecimal(txtextraexpenceltr.Text);
                cvdata.ExtraExpenceKg = Common.ConvertDecimal(txtextraexpencekg.Text);
                cvdata.ExtraExpenceUnit = Common.ConvertDecimal(txtextraexpenceunit.Text);
                cvdata.UserId = Common.ConvertInt(Session["UserId"]);

            }


            ReturnMessage obj = cv.InsertUpdateOtherVariable(cvdata);
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
            cv = new CostVariableMasterDAL();
            hdncvmId.Value = "0";


            txtshittime.Text = "0";
            txtbreakandothertimming.Text = "0";
            txtnetshifthour.Text = "0";
            txtpowerunitprice.Text = "0";
            txtlchargeperday.Text = "0";
            txtsupervisorcost.Text = "0";
            txtunloadingexpenceltr.Text = "0";
            txtunloadingexpencekg.Text = "0";
            txtunloadingexpenceunit.Text = "0";
            txtloadingexpenceperltr.Text = "0";
            txtloadingexpenceperkg.Text = "0";
            txtloadingexpenceperunit.Text = "0";
            txtmachinmaitexpenceltr.Text = "0";
            txtmachinmaitexpencekg.Text = "0";
            txtmachinmaitexpenceunit.Text = "0";
            txtadminexpenceltr.Text = "0";
            txtadminexpencekg.Text = "0";
            txtadminexpenceunit.Text = "0";
            txtextraexpenceltr.Text = "0";
            txtextraexpencekg.Text = "0";
            txtextraexpenceunit.Text = "0";
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {

        }
    }
}