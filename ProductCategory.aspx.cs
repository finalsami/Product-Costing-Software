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
    public partial class ProductCategory : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        ProductCategoryDAL pc = new ProductCategoryDAL();
        ProductCategoryBAL pcdata = new ProductCategoryBAL();
        int CompanyId;
        int UserId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CompanyId = Common.ConvertInt(Session["CompanyId"]);
                UserId = Common.ConvertInt(Session["UserId"]);
                binddata();

            }
        }
        private void binddata()
        {

            DataTable dt = pc.Get_ProductCategoryMaster(Common.ConvertInt(Session["UserId"]), 0, Common.ConvertInt(Session["CompanyId"]));
            gvproductcategory.DataSource = dt;
            gvproductcategory.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvproductcategory.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvproductcategory.UseAccessibleHeader = true;
            }
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_ProductCategoryMaster(2,0);
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                InsertUpdate_ProductCategoryMaster(1,0);
            }
        }
        private void clear()
        {
            pcdata = new ProductCategoryBAL();
            hdnpcid.Value = "0";
            txtproductcat.Text = "";

        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            clear();
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }


        private void InsertUpdate_ProductCategoryMaster(int act, int ProductCategoryId)
        {
            pcdata.UserId = Common.ConvertInt(Session["UserId"]);
            pcdata.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
            if (act == 3)
            {
                pcdata.ProductCategoryId = Common.ConvertInt(ProductCategoryId);
                pcdata.action = act;
            
                pcdata.ProductCategoryName = "";

            }
            else if (act == 1)
            {
                string ProductCategory = Common.ConvertString(txtproductcat.Text.Trim());
                ReturnMessage objs = common.CheckExist("ProductCategory", ProductCategory, "", "");
                string msgs = Common.ConvertString(objs.Message);

                if (Common.ConvertInt(objs.ReturnValue) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                    return;

                }

                else
                {
                    pcdata.ProductCategoryId = Common.ConvertInt(hdnpcid.Value);
                    pcdata.action = act;

                    pcdata.ProductCategoryName = Common.ConvertString(txtproductcat.Text);


                }
            }
            else
            {
                pcdata.ProductCategoryId = Common.ConvertInt(hdnpcid.Value);
                pcdata.action = act;

                pcdata.ProductCategoryName = Common.ConvertString(txtproductcat.Text);
            }
            ReturnMessage obj = pc.InsertUpdate_ProductCategoryMaster(pcdata);
            string msg = Common.ConvertString(obj.Message);
            if (Common.ConvertInt(obj.ReturnValue) > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                clear();

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
            CompanyId = Common.ConvertInt(Session["CompanyId"]);
            UserId = Common.ConvertInt(Session["UserId"]);
            Button btn = (Button)sender;
            int ProductCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (ProductCategoryId > 0)
            {
                DataTable dt = pc.Get_ProductCategoryMaster(Common.ConvertInt(Session["UserId"]), ProductCategoryId, CompanyId);
                if (dt.Rows.Count > 0)
                {
                    hdnpcid.Value = Common.ConvertString(dt.Rows[0]["ProductCategoryId"]);
                    txtproductcat.Text = Common.ConvertString(dt.Rows[0]["ProductCategoryName"]);


                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int ProductCategoryId = Common.ConvertInt(btn.CommandArgument);
            if (ProductCategoryId > 0)
            {
                InsertUpdate_ProductCategoryMaster(3, ProductCategoryId);
            }
        }
    }
}