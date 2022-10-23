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
    public partial class UserMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        UserDAL user = new UserDAL();
        UserMasterBAL userdata = new UserMasterBAL();
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

            DataTable dt = user.Get_UserMasterAll();
            gvusermaster.DataSource = dt;
            gvusermaster.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvusermaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvusermaster.UseAccessibleHeader = true;
            }

        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("Role", "", "");

            drprole.DataSource = dtrm;
            drprole.DataTextField = "Name";
            drprole.DataValueField = "Id";
            drprole.DataBind();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int UserId = Common.ConvertInt(btn.CommandArgument);
            if (UserId > 0)
            {
                Insert_UserMaster(3, UserId);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Button btn = (Button)sender;
                int UserId = Common.ConvertInt(btn.CommandArgument);
                if (UserId > 0)
                {
                    DataTable dt = user.Get_UserMasterById(UserId);
                    if (dt.Rows.Count > 0)
                    {
                        hdnuserid.Value = Common.ConvertString(dt.Rows[0]["UserId"]);
                        txtfname.Text = Common.ConvertString(dt.Rows[0]["FirstName"]);
                        txtlname.Text = Common.ConvertString(dt.Rows[0]["LastName"]);
                        txtusername.Text = Common.ConvertString(dt.Rows[0]["UserName"]);
                        txtpassword.Text = Common.ConvertString(dt.Rows[0]["Password"]);
                        txtemail.Text = Common.ConvertString(dt.Rows[0]["Email"]);
                        txtmobile.Text = Common.ConvertString(dt.Rows[0]["MobileNo"]);
                        if (drprole.Items.FindByValue(Common.ConvertString(dt.Rows[0]["GroupId"])) != null)
                        {
                            drprole.SelectedValue = Common.ConvertString(dt.Rows[0]["GroupId"]);
                        }
                        IsActive.Checked = Common.ConvertBool(dt.Rows[0]["IsActive"]);

                        btnadd.Visible = false;
                        btnupdate.Visible = true;

                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "userpopup('2');", true);
            }
        }
        private void Insert_UserMaster(int act, int UserId)
        {
            if (act == 3)
            {
                userdata.action = act;
                userdata.UserId = UserId;
                userdata.GroupId = "0";
                userdata.FirstName = "";
                userdata.IsActive = false;
                userdata.LastName = "";
                userdata.Email = "";
                userdata.MobileNo = "";
                userdata.Password = "";
                userdata.RefreshToken = "";
                userdata.OTP = "";
                userdata.IsChangePassword = false;
                userdata.IsCompanyAdmin = false;
               userdata.UserName = "";
            }
            else if (act == 1)
            {
                string UserName = Common.ConvertString(txtusername.Text.Trim());
                string Role = Common.ConvertString(drprole.SelectedValue);
                ReturnMessage objs = common.CheckExist("UserMaster", UserName, Role, "");
                string msgs = Common.ConvertString(objs.Message);

                if (Common.ConvertInt(objs.ReturnValue) == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msgs + "')", true);
                    return;

                }
                else
                {
                    userdata.action = act;
                    userdata.GroupId = Common.ConvertString(drprole.SelectedValue);
                    userdata.FirstName = Common.ConvertString(txtfname.Text);
                    userdata.LastName = Common.ConvertString(txtlname.Text);
                    userdata.UserName = Common.ConvertString(txtusername.Text);
                    userdata.Email = Common.ConvertString(txtemail.Text);
                    userdata.MobileNo = Common.ConvertString(txtmobile.Text);
                    userdata.Password = Common.ConvertString(txtpassword.Text);
                    userdata.OTP = "";
                    userdata.RefreshToken = "";
                    userdata.IsActive = IsActive.Checked;
                    userdata.UserId = Common.ConvertInt(Session["UserId"]);


                    userdata.IsChangePassword = false;
                    userdata.IsCompanyAdmin = false;
                }
            }
            else
            {
                userdata.UserId = Common.ConvertInt(hdnuserid.Value);
                userdata.action = act;
                userdata.GroupId = Common.ConvertString(drprole.SelectedValue);
                userdata.FirstName = Common.ConvertString(txtfname.Text);
                userdata.LastName = Common.ConvertString(txtlname.Text);
                userdata.UserName = Common.ConvertString(txtusername.Text);

                userdata.Email = Common.ConvertString(txtemail.Text);
                userdata.MobileNo = Common.ConvertString(txtmobile.Text);
                userdata.Password = Common.ConvertString(txtpassword.Text);

                userdata.IsActive = IsActive.Checked;
                userdata.UserId = Common.ConvertInt(Session["UserId"]);

                userdata.RefreshToken = "";
                userdata.OTP = "";
                userdata.IsChangePassword = false;
                userdata.IsCompanyAdmin = false;
            }
            ReturnMessage obj = user.InsertUpdate_UserMaster(userdata);
            string msg = Common.ConvertString(obj.Message);
            if (Common.ConvertInt(obj.ReturnValue) > 0)
            {
                Response.Redirect(Request.Url.AbsoluteUri);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                Cleardata();

                btnadd.Visible = true;
                btnupdate.Visible = false;

                binddata();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);

            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Insert_UserMaster(1, 0);
            }
        }

        protected void btnopenuser_Click(object sender, EventArgs e)
        {

            Cleardata();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "userpopup('1');", true);

        }
        public void Cleardata()
        {
            userdata = new UserMasterBAL();
            txtusername.Text = "";
            txtpassword.Text = "";
            txtusername.Text = "";
            txtfname.Text = "";
            txtlname.Text = "";
            txtcpassword.Text = "";
            txtemail.Text = "";
            txtmobile.Text = "";
            IsActive.Checked = false;
            hdnuserid.Value = "0";
            btnadd.Visible = true;
            btnupdate.Visible = false;
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Insert_UserMaster(2, 0);

            }

        }
    }
}