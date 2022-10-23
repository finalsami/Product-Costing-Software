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
    public partial class Report_FectoryExpenceMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        FactoryexpenceDAL fact = new FactoryexpenceDAL();
        FactoryexpenceBAL factdata = new FactoryexpenceBAL();
        int CompanyId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CompanyId = Common.ConvertInt(Session["CompanyId"]);
                binddata();
                binddropdown();

            }
        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("BulkProductFromPackingMaterial", "", "");

            //lstbulkproduct.DataSource = dtrm;
            //lstbulkproduct.DataTextField = "Name";
            //lstbulkproduct.DataValueField = "Id";
            //lstbulkproduct.DataBind();


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
        protected void btnadd_Click(object sender, EventArgs e)
        {

        }
    }
}