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

    public partial class ProductInterestMaster :BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        ProductInterestMasterDAL bpim = new ProductInterestMasterDAL();
        ProductInterestMasterBAL bpimdata = new ProductInterestMasterBAL();
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

            DataTable dt = bpim.GetProductInterestMaster( 0);
            gvbpimaster.DataSource = dt;
            gvbpimaster.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvbpimaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvbpimaster.UseAccessibleHeader = true;
            }
        }
        private void binddropdown()
        {
       

            DataTable dtbpm = common.DropdownList("BulkProduct", "", "");

            drpbpmaster.DataSource = dtbpm;
            drpbpmaster.DataTextField = "Name";
            drpbpmaster.DataValueField = "Id";
            drpbpmaster.DataBind();

          
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int BulkProductId = Common.ConvertInt(btn.CommandArgument);
            if (BulkProductId > 0)
            {
                InsertUpdateBulkProductInterestMaster(3, BulkProductId);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int BulkProductId = Common.ConvertInt(btn.CommandArgument);
            if (BulkProductId > 0)
            {
                DataTable dt = bpim.GetProductInterestMaster(BulkProductId);
                if (dt.Rows.Count > 0)
                {
                    hdnbpmid.Value = Common.ConvertString(dt.Rows[0]["BulkProductId"]);
                    txtInterestPercent.Text = Common.ConvertString(dt.Rows[0]["Interest"]);
                    txtBulkcost.Text = Common.ConvertString(dt.Rows[0]["finalcost"]);
                    txtinterestAmt.Text = Common.ConvertString(dt.Rows[0]["InterestAmount"]);
                    txttotalamt.Text = Common.ConvertString(dt.Rows[0]["TotalAmount"]);
                    drpbpmaster.SelectedValue = Common.ConvertString(dt.Rows[0]["BulkProductId"]);
                    drpbpmaster.Enabled = false;

                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateBulkProductInterestMaster(2, 0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateBulkProductInterestMaster(1, 0);
            }
        }
        private void InsertUpdateBulkProductInterestMaster(int act, int BulkProductId)
        {

            if (act == 3)
            {
                bpimdata.FkBulkProductId = Common.ConvertInt(BulkProductId);
                bpimdata.action = act;
                bpimdata.UserId = Common.ConvertInt(Session["UserId"]);
            }
            else
            {
                bpimdata.FkBulkProductId = Common.ConvertInt(drpbpmaster.SelectedValue);
                if (act==1)
                {

                    string BpmId = Common.ConvertString(bpimdata.FkBulkProductId);
                    ReturnMessage objs = common.CheckExist("BulkProductInterestMaster", BpmId, "", "");
                    string msgs = Common.ConvertString(objs.Message);

                    if (Common.ConvertInt(objs.ReturnValue) == 0)
                    {
                      ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);

                    }
                    else
                    {
                        bpimdata.FkBulkProductId = Common.ConvertInt(hdnbpmid.Value);
                        bpimdata.action = act;
                        bpimdata.Interest = Common.ConvertInt(txtInterestPercent.Text);
                        bpimdata.UserId = Common.ConvertInt(txtInterestPercent.Text);
                        bpimdata.UserId = Common.ConvertInt(Session["UserId"]);

                    }
                }
                else
                {
                    bpimdata.FkBulkProductId = Common.ConvertInt(hdnbpmid.Value);
                    bpimdata.action = act;
                    bpimdata.Interest = Common.ConvertInt(txtInterestPercent.Text);
                    bpimdata.UserId = Common.ConvertInt(Session["UserId"]);

                }



            }
            ReturnMessage obj = bpim.InsertUpdateBulkProductInterestMaster(bpimdata);
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
            bpim = new ProductInterestMasterDAL();
            hdnbpmid.Value = "0";
            txtInterestPercent.Text = "";
            drpbpmaster.ClearSelection();
            txtinterestAmt.Text = "";
            txttotalamt.Text = "";
            txtBulkcost.Text = "";
            drpbpmaster.Enabled = true;

        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }
    }
}