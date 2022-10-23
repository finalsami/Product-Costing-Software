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
    public partial class BulkComapnyMapping : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        CompanyMappingDAL cm = new CompanyMappingDAL();
        CompanyMappingBAL cmdata = new CompanyMappingBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                binddropdown();
                binddata();
                lblcompany.InnerHtml = Common.ConvertString(Session["CompanyName"]);
            }
        }

        private void binddata()
        {
            DataTable dt = cm.BulkComapnyMapping(Common.ConvertInt(Session["CompanyId"]));
            gvbulk.DataSource = dt;
            gvbulk.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvbulk.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvbulk.UseAccessibleHeader = true;
            }
        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("BulkCompanyMapping", Common.ConvertString(Session["CompanyId"]), "");

            drpproductId.DataSource = dtrm;
            drpproductId.DataTextField = "Name";
            drpproductId.DataValueField = "Id";
            drpproductId.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int BulkCompanyMappingId = Common.ConvertInt(btn.CommandArgument);
            if (BulkCompanyMappingId > 0)
            {
                InsertUpdatePackingCategory(3, BulkCompanyMappingId);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdatePackingCategory(1, 0);
            }
        }
        private void InsertUpdatePackingCategory(int act, int BulkCompanyMappingId)
        {
            if (act == 3)
            {
                cmdata.BulkCompanyMappingId = Common.ConvertInt(BulkCompanyMappingId);
                cmdata.action = act;
                cmdata.UserId = Common.ConvertInt(Session["UserId"]);
                cmdata.FkCompanyId = 0;
                cmdata.FkBulkProductId = "";
            }
            else if (act == 1)
            {

                cmdata.BulkCompanyMappingId = 0;
                cmdata.action = act;
                cmdata.UserId = Common.ConvertInt(Session["UserId"]);
                cmdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);

                string lst = Common.ConvertString(Request.Form[drpproductId.UniqueID]);
                cmdata.FkBulkProductId = lst.Length > 0 ? lst : "";             
                    
             
            }

            ReturnMessage obj = cm.InsertBulkComapnyMapping(cmdata);
            string msg = Common.ConvertString(obj.Message);
            if (Common.ConvertInt(obj.ReturnValue) > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                cleardata();                             

                binddata();
                binddropdown();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);

            }
        }
        private void cleardata()
        {
           drpproductId.ClearSelection();
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
        }
    }
}