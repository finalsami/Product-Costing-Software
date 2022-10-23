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
    public partial class PackingDifferenceMaster : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        PackingDifferenceDAL pd = new PackingDifferenceDAL();
        PackingDifferenceBAL pddata = new PackingDifferenceBAL();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindData();
            }
        }
        private void BindData()
        {
            dt = pd.ProductPackingMaterialCostingList(Common.ConvertInt(Session["UserId"]));

            gvpackingdiff.DataSource = dt;
            gvpackingdiff.DataBind();
        }

        protected void gvpackingdiff_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            string[] strcols = new string[] { };

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;

                int materialId = Common.ConvertInt(dr[5]);
                
                for (int colIndex = 0; colIndex < e.Row.Cells.Count; colIndex++)
                {
                    if (colIndex<=6)
                    {
                        if (colIndex == 3 || colIndex == 6)
                        {
                            e.Row.Cells[colIndex].Visible = false;
                        }
                    }
                    else
                    {
                       var r =colIndex>=1?colIndex-1:colIndex;
                        int inx = e.Row.RowIndex;
                        TextBox txtName = new TextBox();
                        txtName.CssClass = "form-control";                
                        txtName.Text = dr[r].ToString();
                        txtName.Attributes.Add("type", "Number");
                        if (Common.ConvertBool(dr["isMasterPacking"]))
                        {
                            txtName.Enabled = false;
                        }

                        if (colIndex==7)
                        {
                            txtName.ID = "_txt_suggest_" + inx;
                            txtName.Attributes.Add("onchange", "updatedata('" + txtName.ID + "');");

                            Label label = new Label();
                            label.ID = "_lbl_suggest_" + inx;
                            label.Attributes.Add("style", "display:none");
                            label.Text = dr[r].ToString();
                             e.Row.Cells[colIndex].Controls.Add(label);


                        }
                        if (colIndex>=8)
                        {
                            txtName.ID = "_txt_" + inx + "_" + dt.Columns[r].ToString();
                           

                            Label label = new Label();
                            label.ID= "_lbl_" + inx + "_" + dt.Columns[r].ToString();
                            label.Attributes.Add("style", "display:none");
                            label.Text = dr[r].ToString();
                            e.Row.Cells[colIndex].Controls.Add(label);

                            txtName.Attributes.Add("onchange", "updatedatatolbl('" + txtName.ID + "','"+ label.ID + "');");

                            HiddenField hdn = new HiddenField();
                            hdn.ID = "company_"+ dt.Columns[r].ToString().Split('_')[1];
                            hdn.Value = dt.Columns[r].ToString().Split('_')[1]+"~"+ materialId.ToString();
                            e.Row.Cells[colIndex].Controls.Add(hdn);
                        }

                        e.Row.Cells[colIndex].Controls.Add(txtName);
                    }
                   

                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int colIndex = 0; colIndex < e.Row.Cells.Count; colIndex++)
                {
                    if (colIndex >= 8)
                    {
                        if (e.Row.Cells[colIndex].Text.IndexOf("_")>=2)
                        {
                            e.Row.Cells[colIndex].Text = e.Row.Cells[colIndex].Text.Split('_')[0];
                        }
                        else
                        {
                            e.Row.Cells[colIndex].Text = e.Row.Cells[colIndex].Text.Replace("_", " ");
                        }
                    }
                    else
                    {
                        if(colIndex==3 || colIndex==6)
                        {
                            e.Row.Cells[colIndex].Visible = false;
                        }
                        e.Row.Cells[colIndex].Text = e.Row.Cells[colIndex].Text.Replace("_", " ");
                    }
                }
            }
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            BindData();

        }

    }
}