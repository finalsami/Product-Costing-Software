using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    
    public partial class Site : System.Web.UI.MasterPage
    {
        public static string year = DateTime.Now.Year.ToString();
        public static string userName = "";

        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Common.ConvertInt(Session["UserId"])>0)
            {
                userName = Common.ConvertString(Session["UserName"]);
            }          
        
           
            if (!IsPostBack)
            {
                binddropdown();
                drpcompany.SelectedValue = Common.ConvertString(Session["CompanyId"]);
            }
        }
        private void binddropdown()
        {
            DataTable dtcompany = common.DropdownList("company", "", "");

            drpcompany.DataSource = dtcompany;
            drpcompany.DataTextField = "Name";
            drpcompany.DataValueField = "Id";
            drpcompany.DataBind();

            
        }
        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Session["UserName"] = string.Empty;
            Session["Password"] = string.Empty;
            Session["UserId"] = string.Empty;
            Session["GroupId"] = string.Empty;
            Session["CompanyId"] = string.Empty;
            Session["CompanyName"] = string.Empty;
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/login.aspx");
        }

        protected void drpcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CompanyId"] = drpcompany.SelectedValue;
            Session["CompanyName"] = drpcompany.SelectedItem.Text;
            if (Common.ConvertInt(Session["CompanyId"]) > 0)
            {
                Response.Redirect("~/welcomecompany.aspx");
            }
            else
            {
                Response.Redirect("~/welcome.aspx");
            }

        }

        protected void btnPCSSitemap_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Server.MapPath("~/UserManual")))
            {
                Directory.CreateDirectory(Server.MapPath("~/UserManual"));
            }

            //if (File.Exists(Server.MapPath("~/UserManual/PCS_Manual.pdf")))
            //{
            //    File.Delete(Server.MapPath("~/UserManual/PCS_Manual.pdf"));
            //}

            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=PCSnew_Manual.pdf");
            Response.TransmitFile(Server.MapPath("~/UserManual/PCSnew_Manual.pdf"));
            Response.End();
        }
    }
}