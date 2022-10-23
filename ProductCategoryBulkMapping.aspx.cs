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
    public partial class ProductCategoryBulkMapping : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();

        ProductcategoryBulkMappingBAL category = new ProductcategoryBulkMappingBAL();
        ProductcategoryBulkMappingDAL categorydata = new ProductcategoryBulkMappingDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!IsPostBack)
                {
                    binddata();
                    binddropdown();
                    lblcompany.InnerHtml = Common.ConvertString(Session["CompanyName"]);
                }

            }
        }
        private void binddropdown()
        {           

            DataTable dtrm = common.DropdownList("productcategory", Common.ConvertString(Session["CompanyId"]), "");

            drpcategory.DataSource = dtrm;
            drpcategory.DataTextField = "Name";
            drpcategory.DataValueField = "Id";
            drpcategory.DataBind();
        }
        private void binddata()
        {
            DataTable dt = categorydata.CategoryBulkMapping(Common.ConvertInt(Session["CompanyId"]));
            gvbulk.DataSource = dt;
            gvbulk.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvbulk.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvbulk.UseAccessibleHeader = true;
            }
        }
        protected void drpcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpbulk.ClearSelection();
            if (Common.ConvertInt(drpcategory.SelectedValue) > 0)
            {
                DataTable dtbulk = common.DropdownList("productcategorymapping", Common.ConvertString(Session["CompanyId"]), Common.ConvertString(drpcategory.SelectedValue));
                drpbulk.DataSource = dtbulk;
                drpbulk.DataTextField = "Name";
                drpbulk.DataValueField = "Id";
                drpbulk.DataBind();
            }
            else
            {
                drpbulk.Items.Clear();
            }
        }
        private void InsertUpdateCategoryBulkMapping(int act, int ProductCategoryBulkMappingId)
        {
            if (act == 3)
            {
                category.ProductCategoryBulkMappingId = Common.ConvertInt(ProductCategoryBulkMappingId);
                category.FkProductCategoryId = Common.ConvertInt(drpcategory.SelectedValue);
                category.action = act;
                category.UserId = Common.ConvertInt(Session["userId"]);
                category.FkCompanyId = Common.ConvertInt(Session["CompanyId"]); 
                category.FkBulkProductId = "";
            }
            else if (act == 1)
            {

                category.ProductCategoryBulkMappingId = 0;
                category.FkProductCategoryId = Common.ConvertInt(drpcategory.SelectedValue);
                category.action = act;
                category.UserId = Common.ConvertInt(Session["userId"]);
                category.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);

                string lst = Common.ConvertString(Request.Form[drpbulk.UniqueID]);
                category.FkBulkProductId = lst.Length > 0 ? lst : "";


            }

            ReturnMessage obj = categorydata.InsertCategoryBulkMapping(category);
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
            drpcategory.ClearSelection();
            drpbulk.ClearSelection();
            drpbulk.Items.Clear();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int ProductCategoryBulkMappingId = Common.ConvertInt(btn.CommandArgument);
            if (ProductCategoryBulkMappingId > 0)
            {
                InsertUpdateCategoryBulkMapping(3, ProductCategoryBulkMappingId);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdateCategoryBulkMapping(1, 0);
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            cleardata();
        }       
    }
}