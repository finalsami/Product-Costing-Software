using BAL;
using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class RMEstimate : BasePage
    {

        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        RMEstimateBAL RMEData = new RMEstimateBAL();
        RMEstimateDAL RME = new RMEstimateDAL();

        RMEstimateDetailBAL RMEDData = new RMEstimateDetailBAL();
        RMEstimateDetailDAL RMED = new RMEstimateDetailDAL();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddropdown();
                binddata();
                txtestdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        private void binddropdown()
        {
            DataTable dtrm = common.DropdownList("RMCategory", "", "");

            drpcategory.DataSource = dtrm;
            drpcategory.DataTextField = "Name";
            drpcategory.DataValueField = "Id";
            drpcategory.DataBind();

        }
        private void binddata()
        {

            DataTable dt = RME.GetRMEstimate(0, Common.ConvertInt(Session["UserId"]), Common.ConvertInt(Session["CompanyId"]), 1);
            gvRMEstimate.DataSource = dt;
            gvRMEstimate.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvRMEstimate.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvRMEstimate.UseAccessibleHeader = true;
            }

        }

        private void binddetail(int RMEstimateId, int isbinddetail)
        {

            DataTable dt = RME.GetRMEstimate(RMEstimateId, Common.ConvertInt(Session["UserId"]), Common.ConvertInt(Session["CompanyId"]), 2);
            gvdetail.DataSource = dt;
            gvdetail.DataBind();
            if (dt.Rows.Count > 0)
            {
                btnupdatePrice.Visible = true;

                if (isbinddetail == 1)
                {
                    hdnestimateid.Value = Common.ConvertString(dt.Rows[0]["RMEstimateId"]);
                    txtestdate.Text = DateTime.Parse(dt.Rows[0]["EstimateDate1"].ToString()).ToString("yyyy-MM-dd");
                    txtname.Text = Common.ConvertString(dt.Rows[0]["EstimateName"]);
                }

                gvdetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvdetail.UseAccessibleHeader = true;
            }

        }

        protected void drpcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Common.ConvertInt(drpcategory.SelectedValue) > 0)
            {
                DataTable dtrm = common.DropdownList("RMMasterall", drpcategory.SelectedValue.ToString(), "");

                drprmlist.DataSource = dtrm;
                drprmlist.DataTextField = "Name";
                drprmlist.DataValueField = "Id";
                drprmlist.DataBind();
            }
            else
            {
                drprmlist.Items.Clear();
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Common.ConvertInt(hdnestimateid.Value) > 0)
                {
                    InsertUpdateRMEstimate(2, Common.ConvertInt(hdnestimateid.Value));
                }
                else
                {
                    InsertUpdateRMEstimate(1, 0);
                }
            }

        }
        private void InsertUpdateRMEstimate(int act, int RMEstimateId)
        {
            RMEData.UserId = Common.ConvertInt(Session["UserId"]);

            if (act == 3)
            {
                RMEData.RMEstimateId = Common.ConvertInt(RMEstimateId);
                RMEData.EstimateName = "";
                RMEData.action = act;
                RMEData.FkCompanyId = Common.ConvertInt(Session["CompanyId"]);
                RMEData.EstimateDate = null;
            }
            else
            {
                RMEData.RMEstimateId = RMEstimateId;
                RMEData.EstimateName = Common.ConvertString(txtname.Text);
                RMEData.action = act;
                RMEData.EstimateDate = Common.ConvertString(txtestdate.Text);
            }
            ReturnMessage obj = RME.InsertUpdateRMEstimate(RMEData);
            string msg = Common.ConvertString(obj.Message);
            if (Common.ConvertInt(obj.ReturnValue) > 0)
            {
                hdnestimateid.Value = obj.ReturnValue.ToString();

                if (act == 1)
                {
                    RMEDData.FKRMEstimateId = Common.ConvertInt(obj.ReturnValue);
                    RMEDData.RMEstimateDetailId = 0;
                    RMEDData.action = 1;
                    RMEDData.RMNewPrice = 0;
                    string lst = Common.ConvertString(Request.Form[drprmlist.UniqueID]);
                    RMEDData.FkRMPriceId = lst.Length > 0 ? lst : "";
                    ReturnMessage objdetail = RMED.InsertUpdateRMEstimateDetail(RMEDData);
                    binddetail(Common.ConvertInt(obj.ReturnValue.ToString()), 1);
                    binddata();
                }
                if (act == 2)
                {
                    string lst = Common.ConvertString(Request.Form[drprmlist.UniqueID]);
                    if (lst.Length > 0)
                    {
                        RMEDData.FKRMEstimateId = Common.ConvertInt(RMEstimateId);
                        RMEDData.RMEstimateDetailId = 0;
                        RMEDData.action = 1;
                        RMEDData.RMNewPrice = 0;
                        RMEDData.FkRMPriceId = lst.Length > 0 ? lst : "";
                        ReturnMessage objdetail = RMED.InsertUpdateRMEstimateDetail(RMEDData);
                        binddetail(Common.ConvertInt(RMEstimateId), 1);
                        binddata();
                    }
                }
                if (act == 3)
                {
                    binddata();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
                }
                cleardata();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);

            }
        }
        private void cleardata()
        {
            drpcategory.ClearSelection();
            drprmlist.Items.Clear();


        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("RMEstimate.aspx");
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMEstimateId = Common.ConvertInt(btn.CommandArgument);
            if (RMEstimateId > 0)
            {
                btnupdatePrice.Visible = true;
                binddetail(Common.ConvertInt(RMEstimateId), 1);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMEstimateId = Common.ConvertInt(btn.CommandArgument);
            if (RMEstimateId > 0)
            {
                InsertUpdateRMEstimate(3, RMEstimateId);
            }
        }

        protected void btnDeletePrice_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int RMEstimateDetaileId = Common.ConvertInt(btn.CommandArgument);
            if (RMEstimateDetaileId > 0)
            {

                RMEDData.RMEstimateDetailId = Common.ConvertInt(RMEstimateDetaileId);
                RMEDData.UserId = Common.ConvertInt(Session["UserId"]);
                RMEDData.action = 2;
                RMEDData.RMNewPrice = 0;
                RMEDData.FkRMPriceId = "";
                ReturnMessage objdetail = RMED.InsertUpdateRMEstimateDetail(RMEDData);
                if (Common.ConvertInt(hdnestimateid.Value) > 0)
                {
                    binddetail(Common.ConvertInt(hdnestimateid.Value), 1);
                }
            }
        }
        protected void btnupdatePrice_Click(object sender, EventArgs e)
        {
            if (Common.ConvertInt(hdnestimateid.Value) > 0)
            {
                foreach (GridViewRow row in gvdetail.Rows)
                {
                    TextBox txtprice = (TextBox)row.FindControl("txtprice");
                    TextBox txtEstimatedetailid = (TextBox)row.FindControl("txtpid");

                    RMEDData.FKRMEstimateId = Common.ConvertInt(hdnestimateid.Value);
                    RMEDData.RMEstimateDetailId = Common.ConvertInt(txtEstimatedetailid.Text);
                    RMEDData.UserId = Common.ConvertInt(Session["UserId"]);
                    RMEDData.action = 2;
                    RMEDData.RMNewPrice = Common.ConvertDecimal(txtprice.Text);
                    RMEDData.FkRMPriceId = "";
                    ReturnMessage objdetail = RMED.InsertUpdateRMEstimateDetail(RMEDData);
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Price Updated Successfully')", true);
            }
        }

        protected void btnDynamic_Click(object sender, EventArgs e)
        {

        }
        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            string str = hfDynamicCompId.Value;
            Button btn = (Button)sender;

            string id = btn.ID;
        }
        protected void btnreport_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int GridEstimateId = Common.ConvertInt(btn.CommandArgument);
            hdnestimateid.Value = Common.ConvertString(GridEstimateId);




            DataTable dtBulkCount = new DataTable();
            DataTable dtEstimate = new DataTable();
            dtEstimate = RMED.GetProductListByRMEstimate(GridEstimateId, Common.ConvertInt(Session["UserId"]));
            dtBulkCount = RMED.GetCountProductwithCompanyByRMEstimate(GridEstimateId, Common.ConvertInt(Session["UserId"]));
            lblRMEstimateName.Text = dtEstimate.Rows[0]["EstimateName"].ToString();
            hdnestimateid.Value = dtEstimate.Rows[0]["RMEstimateId"].ToString();
            
            EstimateHeader.Text = lblRMEstimateName.Text;
            Button ButtonChange;
            for (int i = 0; i < dtBulkCount.Rows.Count; i++)
            {
                ButtonChange = new Button();
                ContentPlaceHolder content = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");

                lblStatus.Text = "Estimate";

                lblCompanyName.Text = dtBulkCount.Rows[i]["CompanyName"].ToString();

                string DynamicCompBtnName = lblCompanyName.Text + " (" + dtBulkCount.Rows[i]["BulkCount"] + ")";
                ButtonChange.Text = DynamicCompBtnName;
                ButtonChange.ID = "change_" + i.ToString();
                ButtonChange.Font.Size = FontUnit.Point(9);
                ButtonChange.ControlStyle.CssClass = "btn btn-primary btn-block m-1";
                ButtonChange.OnClientClick = "return DynamicClick(" + hdnCompanyId.Value + ",'" + hdnestimateid.Value + "','" + lblStatus.Text + "');";
                ButtonChange.Click += new EventHandler(ButtonChange_Click);
                Panel1.Controls.Add(ButtonChange);
            }
            //DataTable dtRM = new DataTable();
            //dtRM = cls.GET_BULK_FOR_RM_ESTIMATE(GridEstimateName);
            gvestimate.DataSource = dtEstimate;
            gvestimate.DataBind();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ShowRMEstimateReport()", true);
        }
        private string changedisplayname(string name)
        {
            string ret = "";
            string[] pack = name.Split('_');
            if (pack.Length == 2)
            {
                ret = Common.ConvertString(pack[1]) + "-" + Common.ConvertString(pack[0]);
            }
            else
            {
                ret = Common.ConvertString(pack[1]) + "-" + Common.ConvertString(pack[0]) + "(";
                for (int i = 2; i < pack.Length; i++)
                {
                    ret += pack[i] + " ";
                }
                ret += ")";
            }

            return ret;
        }
        protected void btnRMEstimatReport_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int ProductId = Common.ConvertInt(btn.CommandArgument);
            if (ProductId > 0)
            {
                DataSet ds = RMED.GetRMEstimateDetailReport(ProductId,Common.ConvertInt(Session["UserId"]));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    exampleModalLabel.InnerHtml = Common.ConvertString(ds.Tables[0].Rows[0]["BulkproductName"]);

                    StringBuilder sbdata = new StringBuilder();

                    //Ingredient detail
                    sbdata.Append("<table class='table table-bordered table-striped gridview dataTable no-footer dtr-inline'><tbody>"
                    + "<tr>"
                        + "<th scope='col'>No</th>"
                        + "<th scope='col'>Ingredient</th>"
                        + "<th scope='col'>Formulation(%)</th>"
                        + "<th scope='col'>QTY</th>"
                        + "<th scope='col'>Rate/KG</th>"
                        + "<th scope='col'>Transportation</th>"
                        + "<th scope='col'>Amount</th>"
                    + "</tr>");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int t = i + 1;
                        sbdata.Append("<tr>"
                            + "<td>" + t + "</td>"
                            + "<td style='text-align:left!important'>" + Common.ConvertString(ds.Tables[0].Rows[i]["IngredientName"]) + "</td>"
                            + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["Formulation"]) + "</td>"
                            + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["QuantityLtrKgDisplay"]) + "</td>"
                            + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["RmPrice"]) + "</td>"
                            + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["TransporationRate"]) + "</td>"
                            + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["total"]) + "</td>"
                        + "</tr>");
                    }

                    sbdata.Append("<tr>"
                           + "<th></th>"
                           + "<th>Total</th>"
                           + "<th>" + Common.ConvertString(ds.Tables[0].Compute("sum(Formulation)", "")) + " </th>"
                           + "<th>" + Common.ConvertString(ds.Tables[0].Compute("sum(QuantityLtrKgDisplay)", "")) + "</th>"
                           + "<th>" + Common.ConvertString(ds.Tables[0].Compute("sum(RmPrice)", "")) + "</th>"
                           + "<th>" + Common.ConvertString(ds.Tables[0].Compute("sum(TransporationRate)", "")) + "</th>"
                           + "<th>" + Common.ConvertString(ds.Tables[0].Compute("sum(total)", "")) + "</th>"
                       + "</tr>");

                    sbdata.Append("</tbody></table>");

                    decimal cost = Common.ConvertDecimal(ds.Tables[0].Rows[0]["finalbulkcost"]);
                    decimal interest = Common.ConvertDecimal(ds.Tables[0].Rows[0]["Interest"]);
                    decimal rate = cost * interest / 100;

                    //spgr and bulk detail

                    sbdata.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'>");
                    sbdata.Append("<tr><th scope='col' style='text-align:left!important' class='col-md-8'>Formulation (" + Common.ConvertString(ds.Tables[0].Rows[0]["formulationName"]) + "), Batch Size (" + Common.ConvertString(ds.Tables[0].Rows[0]["BatchSize"]) + ")</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertString(ds.Tables[0].Rows[0]["FormulationCost"]) + "</th></tr>");
                    sbdata.Append("<tr><th scope='col' style='text-align:left!important' class='col-md-8'>SPGR</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertString(ds.Tables[0].Rows[0]["spgr"]) + "</th></tr>");
                    sbdata.Append("<tr><th scope='col' style='text-align:left!important' class='col-md-8'>Final OutPut</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertString(ds.Tables[0].Rows[0]["finalout"]) + "</th></tr>");
                    sbdata.Append("<tr><th scope='col' style='text-align:left!important' class='col-md-8'>Final Bulk Cost/Ltr or Kg</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertString(ds.Tables[0].Rows[0]["finalbulkcost"]) + "</th></tr>");
                    sbdata.Append("<tr><th scope='col' style='text-align:left!important' class='col-md-8'>Interest</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertDecimal(rate).ToString() + "(" + Common.ConvertString(ds.Tables[0].Rows[0]["Interest"]) + "%)</th></tr>");
                    sbdata.Append("<tr><th scope='col' style='text-align:left!important'  class='col-md-8'>Final Cost/Ltr or Kg</th><th scope='col' style='text-align:left!important' class='col-md-4'>" + Common.ConvertString(ds.Tables[0].Rows[0]["final"]) + "</th></tr>");
                    sbdata.Append("</thead></tbody></table>");

                    //PM costing                    

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        sbdata.Append("<h5 class='text-primary'>PM Costing/Unit</h5><table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                        for (int col = 0; col < ds.Tables[1].Columns.Count; col++)
                        {
                            if (col == 0)
                            {
                                sbdata.Append("<th scope='col' >" + Common.ConvertString(ds.Tables[1].Columns[col].ColumnName.Replace("_", " ")) + "</th>");
                            }
                            else
                            {
                                string[] pack = Common.ConvertString(ds.Tables[1].Columns[col].ColumnName).Split('_');
                                if (pack.Length == 2)
                                {
                                    sbdata.Append("<th scope='col'>" + Common.ConvertString(pack[1]) + "-" + Common.ConvertString(pack[0]) + "</th>");
                                }
                                else
                                {
                                    sbdata.Append("<th scope='col'>" + changedisplayname(ds.Tables[1].Columns[col].ColumnName) + "</th>");
                                }

                            }
                        }
                        sbdata.Append("</tr></thead>");

                        for (int r = 0; r < ds.Tables[1].Rows.Count; r++)
                        {
                            sbdata.Append("<tr>");
                            for (int col = 0; col < ds.Tables[1].Columns.Count; col++)
                            {
                                sbdata.Append("<td>");
                                sbdata.Append(Common.ConvertString(ds.Tables[1].Rows[r][col]) == "" ? "-" : Common.ConvertString(ds.Tables[1].Rows[r][col]));
                                sbdata.Append("</td>");
                            }
                            sbdata.Append("</tr>");

                            if (r == ds.Tables[1].Rows.Count - 1)
                            {
                                sbdata.Append("<thead class='thead-light'><tr>");
                                for (int col = 0; col < ds.Tables[1].Columns.Count; col++)
                                {
                                    if (col == 0)
                                    {
                                        sbdata.Append("<th>");
                                        sbdata.Append("Total");
                                        sbdata.Append("</th>");
                                    }
                                    else
                                    {

                                        sbdata.Append("<th>");
                                        sbdata.Append(Common.ConvertString(ds.Tables[1].Compute("sum(" + ds.Tables[1].Columns[col].ColumnName + ")", "")));
                                        sbdata.Append("</th>");
                                    }
                                }
                                sbdata.Append("</thead></tr>");
                            }
                        }

                        sbdata.Append("</tbody></table>");
                    }

                    //Other Costing

                    if (ds.Tables[2].Rows.Count > 0)
                    {

                        string masterpackname = "";

                        DataRow[] dr = ds.Tables[2].Select("isMasterpacking=True");
                        if (dr.Length > 0)
                        {
                            masterpackname = changedisplayname(dr[0]["packingdetaildisplay"].ToString());
                            exampleModalLabel.InnerHtml += " [ <span class='text-primary'>" + masterpackname + "</span> ]";
                        }


                        StringBuilder sblabour = new StringBuilder();
                        StringBuilder sbloss = new StringBuilder();
                        StringBuilder sbtotal = new StringBuilder();

                        StringBuilder sbPerLtrUnit = new StringBuilder();
                        StringBuilder sbPerLtr = new StringBuilder();
                        StringBuilder sbPerUnit = new StringBuilder();

                        StringBuilder sblossfinal = new StringBuilder();

                        StringBuilder sblossfinal_row = new StringBuilder();

                        sbdata.Append("<h5 class='text-primary'>Other Costing/Unit</h5><table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                        sbPerLtrUnit.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                        sblossfinal.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                        sblabour.Append("<tr>");
                        sbloss.Append("<tr>");
                        sbtotal.Append("<thead class='thead-light'><tr>");

                        sbdata.Append("<th>");
                        sbdata.Append("Packing Size");
                        sbdata.Append("</th>");

                        sbPerLtrUnit.Append("<th>Packing Size</th>");
                        sblossfinal.Append("<th>Packing Size</th>");

                        sblossfinal_row.Append("<tr>");

                        for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                        {
                            sbdata.Append("<th>");
                            sbdata.Append(changedisplayname(Common.ConvertString(ds.Tables[2].Rows[k]["packingdetaildisplay"])));
                            sbdata.Append("</th>");

                            sbPerLtrUnit.Append("<th>");
                            sbPerLtrUnit.Append(changedisplayname(Common.ConvertString(ds.Tables[2].Rows[k]["packingdetaildisplay"])));
                            sbPerLtrUnit.Append("</th>");

                            sblossfinal.Append("<th>");
                            sblossfinal.Append(changedisplayname(Common.ConvertString(ds.Tables[2].Rows[k]["packingdetaildisplay"])));
                            sblossfinal.Append("</th>");

                            if (k == 0)
                            {
                                sblabour.Append("<td>Labour</td>");
                                sbloss.Append("<td>Packing Loss</td>");
                                sbtotal.Append("<th>Total</th>");
                                sbPerLtr.Append("<td>Per Ltr Costing</td>");
                                sbPerUnit.Append("<td>Per Unit Costing</td>");
                                sblossfinal_row.Append("<td>Difference</td>");
                            }

                            sblabour.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["labourcost"]) + "</td>");
                            sbloss.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["packingloss"]) + "</td>");

                            sbPerLtr.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["final"]) + "</td>");

                            Decimal unit = Common.ConvertDecimal(ds.Tables[2].Rows[k]["final"]) / Common.ConvertDecimal(ds.Tables[2].Rows[k]["CostPerLtrFector"]);

                            sbPerUnit.Append("<td>" + Common.ConvertDecimal(unit) + "</td>");

                            Decimal total = Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]) + Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]);

                            sbtotal.Append("<th>" + Common.ConvertDecimal(total.ToString()) + "</th>");

                            sblossfinal_row.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["diff"]) + "</td>");
                        }

                        sblabour.Append("</tr>");
                        sbloss.Append("</tr>");
                        sbtotal.Append("</thead></tr>");
                        sbdata.Append("</thead></tr>");

                        sbPerLtr.Append("</tr>");
                        sbPerUnit.Append("</tr>");

                        sbPerLtrUnit.Append("</thead></tr><h5 class='text-primary'>Final Cost Packing Wise ( BULK + PM + Other )</h5>");

                        sbPerLtrUnit.Append(sbPerLtr);
                        sbPerLtrUnit.Append(sbPerUnit);

                        sbPerLtrUnit.Append("</tbody></table><h5 class='text-primary'>Packing Difference from Master Pack [" + masterpackname + "]</h5>");

                        sbdata.Append(sblabour);
                        sbdata.Append(sbloss);
                        sbdata.Append(sbtotal);

                        sbdata.Append(sbPerLtrUnit);

                        sblossfinal.Append("</tr>");


                        sblossfinal.Append(sblossfinal_row);

                        sblossfinal.Append("</tbody></table>");

                        sbdata.Append(sblossfinal);

                        sbdata.Append("</tbody></table>");

                    }
                    dvdetailcontent.InnerHtml = sbdata.ToString();
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showhidemodel('1');", true);
            }
        }

    }
}
