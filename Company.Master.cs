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
    public partial class Company : System.Web.UI.MasterPage
    {
        public static string year = DateTime.Now.Year.ToString();
        public static string userName = "";

        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Common.ConvertInt(Session["UserId"]) > 0)
            {
                userName = Common.ConvertString(Session["UserName"]);
            }
          
            if (!IsPostBack)
            {
                binddropdown();
                drpcompany.SelectedValue = Common.ConvertString(Session["CompanyId"]);
                lblCompanyName.Text = Common.ConvertString(Session["CompanyName"]);

                setmenu();
            }
            
        }
        private void setmenu()
        {
            if(Common.ConvertInt(Session["CompanyId"])==1)
            {
                limenugp.Visible = true;
                limenuothers.Visible = false;
                limenugpreport.Visible = true;
                limenuothers2.Visible = false;
                limenuGpBulkcost.Visible = false;
                limenuTermsCondition.Visible = false;
            }
            else if (Common.ConvertInt(Session["CompanyId"])==6)
            {
                limenuGpBulkcost.Visible = true;
                limenuothers.Visible = false;
                limenuothers2.Visible = false;
                limenugp.Visible = false;
                limenugpreport.Visible = false;
                limenuTermsCondition.Visible = true;

            }
            else
            {
                limenugp.Visible = false;
                limenuothers.Visible = true;
                limenugpreport.Visible = false;
                limenuothers2.Visible = true;
                limenuGpBulkcost.Visible = false;
                limenuTermsCondition.Visible = false;

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
            if (Common.ConvertInt(Session["CompanyId"])>0)
            {
                lblCompanyName.Text= drpcompany.SelectedItem.Text;
                Response.Redirect("~/welcomecompany.aspx");
            }
            else
            {
                Response.Redirect("~/welcome.aspx");
            }
        }

        protected void btnPCSCompanySitemap_Click(object sender, EventArgs e)
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
            Response.AppendHeader("Content-Disposition", "attachment; filename=PCSCompany_Manual.pdf");
            Response.TransmitFile(Server.MapPath("~/UserManual/PCSCompany_Manual.pdf"));
            Response.End();
        }
    }
}