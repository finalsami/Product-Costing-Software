using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using DAL;

namespace Production_Costing_Software
{
    public partial class Login : System.Web.UI.Page
    {
        UserBAL user = new UserBAL();
        UserDAL userData = new UserDAL();
        DBHelper dbhelper = new DBHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                user.UserName = txtusername.Text;
                user.Password = txtpassword.Text;
                DataTable dt = new DataTable();
                dt = userData.UserLogIn(user);

                if (dt.Rows.Count > 0)
                {
                    Session["UserName"] = Common.ConvertString(dt.Rows[0]["UserName"]);
                    Session["Password"] = Common.ConvertString(dt.Rows[0]["Password"]);
                    Session["UserId"] = Common.ConvertInt(dt.Rows[0]["UserId"]);
                    Session["GroupId"] = Common.ConvertInt(dt.Rows[0]["GroupId"]);
                    Session["CompanyName"] = "";
                    Session["CompanyId"] = "";
                    Response.Redirect("~/welcome.aspx");

                }
                else
                {
                    dverror.Style.Add("display", "");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}