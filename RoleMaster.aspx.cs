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
    public partial class RoleMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        RoleMasterDAL group = new RoleMasterDAL();
        GroupMasterBAL groupdata = new GroupMasterBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }
        private void binddata()
        {

            DataTable dt = group.Get_GroupMaster();
            gvrolemaster.DataSource = dt;
            gvrolemaster.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvrolemaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvrolemaster.UseAccessibleHeader = true;
            }

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Button btn = (Button)sender;
                int GroupId = Common.ConvertInt(btn.CommandArgument);
                if (GroupId > 0)
                {
                    DataTable dt = group.Get_RoleDataById(GroupId);
                    if (dt.Rows.Count > 0)
                    {
                        hdnroleid.Value = Common.ConvertString(dt.Rows[0]["GroupId"]);
                        txtrole.Text = Common.ConvertString(dt.Rows[0]["GroupName"]);

                        IsActive.Checked = Common.ConvertBool(dt.Rows[0]["IsActive"]);

                        btnadd.Visible = false;
                        btnupdate.Visible = true;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "popupUserrights('2');", true);

                    }
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int GroupId = Common.ConvertInt(btn.CommandArgument);
            if (GroupId > 0)
            {
                Group_CreateUpdate(3, GroupId);
            }
        }

        protected void btnopenrol_Click(object sender, EventArgs e)
        {
            Cleardata();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "popupUserrights('1');", true);
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                Group_CreateUpdate(1, 0);

            }
        }
        public void Group_CreateUpdate()
        {

        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Group_CreateUpdate(2, 0);

            }
        }
        public void Group_CreateUpdate(int act, int GroupId)
        {
            if (act == 1)
            {
                groupdata.GroupId = GroupId;
                groupdata.GroupName = Common.ConvertString(txtrole.Text);
                groupdata.IsActive = IsActive.Checked;
                groupdata.UserId = Common.ConvertInt(Session["userId"]);
                groupdata.action = act;
            }
            else
            {
                groupdata.GroupId = GroupId;
                groupdata.GroupName = Common.ConvertString(txtrole.Text);
                groupdata.IsActive = IsActive.Checked;
                groupdata.UserId = Common.ConvertInt(Session["userId"]);
                groupdata.action = act;

            }



            ReturnMessage obj = group.Group_CreateUpdate(groupdata);
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
        public void Cleardata()
        {
            groupdata = new GroupMasterBAL();
            txtrole.Text = "";
            hdnroleid.Value = "0";
            IsActive.Checked = false;

            btnadd.Visible = true;
            btnupdate.Visible = false;
        }
    }
}