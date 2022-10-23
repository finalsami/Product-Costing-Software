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
    public partial class BulkProductMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        BulkProductMasterDAL bpm = new BulkProductMasterDAL();
        BulkProductMasterBAL bpmdata = new BulkProductMasterBAL();
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

            DataTable dt = bpm.Get_BulkProductMasterAll(Common.ConvertInt(Session["UserId"]), 0);
            gvbpmaster.DataSource = dt;
            gvbpmaster.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvbpmaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvbpmaster.UseAccessibleHeader = true;
            }

        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("Main_Category", "", "");

            drpmaincategory.DataSource = dtrm;
            drpmaincategory.DataTextField = "Name";
            drpmaincategory.DataValueField = "Id";
            drpmaincategory.DataBind();


            DataTable dtunit = common.DropdownList("Unit_Measurement", "", "");

            drpunit.DataSource = dtunit;
            drpunit.DataTextField = "Name";
            drpunit.DataValueField = "Id";
            drpunit.DataBind();


            DataTable dtsource = common.DropdownList("Enum_Source", "", "");

            drpsource.DataSource = dtsource;
            drpsource.DataTextField = "Name";
            drpsource.DataValueField = "Id";
            drpsource.DataBind();

            DataTable dtgst = common.DropdownList("Enum_Gst", "", "");

            drpgst.DataSource = dtgst;
            drpgst.DataTextField = "Name";
            drpgst.DataValueField = "Id";
            drpgst.DataBind();

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int BulkProductId = Common.ConvertInt(btn.CommandArgument);
            if (BulkProductId > 0)
            {
                DataTable dt = bpm.Get_BulkProductMasterAll(Common.ConvertInt(Session["UserId"]), BulkProductId);
                if (dt.Rows.Count > 0)
                {
                    hdbpmid.Value = Common.ConvertString(dt.Rows[0]["BulkProductId"]);

                    if (drpmaincategory.Items.FindByValue(Common.ConvertString(dt.Rows[0]["MainCategoryId"])) != null)
                    {
                        drpmaincategory.SelectedValue = Common.ConvertString(dt.Rows[0]["MainCategoryId"]);
                    }

                    if (drpunit.Items.FindByValue(Common.ConvertString(dt.Rows[0]["FkUnitMeasurementId"])) != null)
                    {
                        drpunit.SelectedValue = Common.ConvertString(dt.Rows[0]["FkUnitMeasurementId"]);
                    }

                    if (drpgst.Items.FindByValue(Common.ConvertString(dt.Rows[0]["FkGstId"])) != null)
                    {
                        drpgst.SelectedValue = Common.ConvertString(dt.Rows[0]["FkGstId"]);
                    }
                    if (drpsource.Items.FindByValue(Common.ConvertString(dt.Rows[0]["FkSourceId"])) != null)
                    {
                        drpsource.SelectedValue = Common.ConvertString(dt.Rows[0]["FkSourceId"]);
                    }
                
                    txtbpname.Text = Common.ConvertString(dt.Rows[0]["BulkProductName"]);
                   

                    btnadd.Visible = false;
                    btnupdate.Visible = true;

                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int BulkProductId = Common.ConvertInt(btn.CommandArgument);
            if (BulkProductId > 0)
            {
                InsertUpdateBulkProductMaster(3, BulkProductId);
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
                InsertUpdateBulkProductMaster(1, 0);
            }
        }
        private void InsertUpdateBulkProductMaster(int act, int BulkProductId)
        {

            if (act == 3)
            {
                bpmdata.BulkProductId = Common.ConvertInt(BulkProductId);
                bpmdata.action = act;
                bpmdata.UserId = Common.ConvertInt(Session["UserId"]);
                bpmdata.BulkProductName = "";
            }
            else
            {
                bpmdata.BulkProductId = Common.ConvertInt(hdbpmid.Value);
                bpmdata.action = act;
                bpmdata.BulkProductName = Common.ConvertString(txtbpname.Text);
                bpmdata.MainCategoryId = Common.ConvertInt(drpmaincategory.SelectedValue);
                bpmdata.UnitMeasurementId = Common.ConvertInt(drpunit.SelectedValue);
                bpmdata.GstId = Common.ConvertInt(drpgst.SelectedValue);
                bpmdata.FkSourceId = Common.ConvertInt(drpsource.SelectedValue);
                bpmdata.UserId = Common.ConvertInt(Session["UserId"]);

            }
            ReturnMessage obj = bpm.InsertUpdateBulkProductMaster(bpmdata);
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
            bpmdata = new BulkProductMasterBAL();
            hdbpmid.Value = "0";
            txtbpname.Text = "";
            drpmaincategory.ClearSelection();
            drpsource.ClearSelection();
            drpgst.ClearSelection();
            drpunit.ClearSelection();

        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateBulkProductMaster(2, 0);
            }
        }
    }
}