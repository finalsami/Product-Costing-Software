using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Production_Costing_Software
{
    public class BasePage: System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if(Common.ConvertInt(HttpContext.Current.Session["UserId"])==0)
            {
                Response.Redirect("~/Login.aspx");
            }
            base.OnLoad(e);

        }

        void Page_Error(object sender, EventArgs e)
        {

            //Server.Transfer("Error.aspx");

        }
    }
}