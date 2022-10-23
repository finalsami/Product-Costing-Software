using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Production_Costing_Software
{
    
    public class Common
    {
       
        public static string ConvertString(object obj)
        {
            string ret = "";
            if (string.IsNullOrEmpty(Convert.ToString(obj)))
            {
                return ret;
            }
            else
            {
                return Convert.ToString(obj);
            }
        }
        public static int ConvertInt(object obj)
        {           
            int IVar = 0;
            int.TryParse(Convert.ToString(obj), out IVar);
            return IVar;
        }
        public static bool ConvertBool(object obj)
        {
            bool IVar =false;
            bool.TryParse(Convert.ToString(obj), out IVar);
            return IVar;
        }
        public static Decimal ConvertDecimal(object obj)
        {
            decimal IVar = 0;
            decimal.TryParse(Convert.ToString(obj), out IVar);
            IVar = decimal.Round(IVar, 2);
            return IVar;
        }
        public static float ConvertFloat(object obj)
        {
            float IVar = 0;
            float.TryParse(Convert.ToString(obj), out IVar);
            return IVar;
        }

        private static string changedisplayname(string name)
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

        public static void GetFinishedGood(int UserID,int ProductID, out string html, out string bulkname)
        {

            html = "";
            bulkname = "";

            DBHelper dbhelper = new DBHelper();
            CommonDAL common = new CommonDAL();
            FinishGoodsPricingReportDAL FG = new FinishGoodsPricingReportDAL();

            DataSet ds = FG.FinishedGoodReportDetail(Common.ConvertInt(UserID), ProductID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                bulkname = Common.ConvertString(ds.Tables[0].Rows[0]["BulkproductName"]);

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
                    decimal labourcost = 0;
                    decimal packingloss = 0;
                    string masterpackname = "";

                    DataRow[] dr = ds.Tables[2].Select("isMasterpacking=True");
                    if (dr.Length > 0)
                    {
                        masterpackname = changedisplayname(dr[0]["packingdetaildisplay"].ToString());
                        bulkname += " [ <span class='text-primary'>" + masterpackname + "</span> ]";
                    }


                    StringBuilder sblabour = new StringBuilder();
                    StringBuilder sbloss = new StringBuilder();
                    StringBuilder sbtotal = new StringBuilder();

                    StringBuilder sbPerLtrUnit = new StringBuilder();
                    StringBuilder sbPerLtr = new StringBuilder();
                    StringBuilder sbPerUnit = new StringBuilder();

                    StringBuilder sblossfinal = new StringBuilder();

                    StringBuilder sblossfinal_row = new StringBuilder();

                    int UnitMeasure = 0;
                    decimal PackingSize = 0;


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

                        UnitMeasure = Common.ConvertInt(ds.Tables[2].Rows[k]["packingMeasurementId"]);
                        PackingSize = Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingsize"]);
                        decimal CostPerLtr = Common.ConvertDecimal(ds.Tables[0].Rows[0]["final"]);
      
                        decimal unit = 0;

                      decimal    TotalPMCost = Common.ConvertDecimal(ds.Tables[1].Compute("sum(" + ds.Tables[2].Rows[k]["packingdetaildisplay"] + ")", ""));

                        if (PackingSize > 1 && UnitMeasure == 1 || UnitMeasure == 2)
                        {
                            labourcost = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]) * PackingSize);
                            packingloss = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]) * PackingSize);


                            unit = (CostPerLtr * ( PackingSize)) + TotalPMCost + (labourcost) + (packingloss);
                            //unit = Common.ConvertDecimal(ds.Tables[2].Rows[k]["final"]) * PackingSize;

                        }
                        else if (PackingSize < 1000 && UnitMeasure == 6 || UnitMeasure == 7)
                        {
                            labourcost = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]) );
                            packingloss = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]) );
                            unit = (CostPerLtr / (1000 / PackingSize)) + TotalPMCost + (labourcost )+(packingloss) ;

                        }
                        else if (PackingSize == 1 && UnitMeasure == 1 || UnitMeasure == 2)
                        {
                            labourcost = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]) * PackingSize);
                            packingloss = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]) * PackingSize);
                            unit = CostPerLtr + TotalPMCost + (labourcost) + (packingloss);

                        }
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
                    sbPerLtrUnit.Append(sbPerUnit);

                    sbPerLtrUnit.Append(sbPerLtr);

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
                html = sbdata.ToString();
               
            }
        }
        public static void GetFinishedGoodEstimate(int UserID, int ProductID, out string html, out string bulkname)
        {

            html = "";
            bulkname = "";

            DBHelper dbhelper = new DBHelper();
            CommonDAL common = new CommonDAL();
            RMEstimateDetailDAL RMED = new RMEstimateDetailDAL();

            DataSet ds = RMED.GetRMEstimateDetailReport(Convert.ToInt32(ProductID), 1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                bulkname = Common.ConvertString(ds.Tables[0].Rows[0]["BulkproductName"]);

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
                    decimal labourcost = 0;
                    decimal packingloss = 0;
                    string masterpackname = "";

                    DataRow[] dr = ds.Tables[2].Select("isMasterpacking=True");
                    if (dr.Length > 0)
                    {
                        masterpackname = changedisplayname(dr[0]["packingdetaildisplay"].ToString());
                        bulkname += " [ <span class='text-primary'>" + masterpackname + " (Estimate)</span> ]";
                    }


                    StringBuilder sblabour = new StringBuilder();
                    StringBuilder sbloss = new StringBuilder();
                    StringBuilder sbtotal = new StringBuilder();

                    StringBuilder sbPerLtrUnit = new StringBuilder();
                    StringBuilder sbPerLtr = new StringBuilder();
                    StringBuilder sbPerUnit = new StringBuilder();

                    StringBuilder sblossfinal = new StringBuilder();

                    StringBuilder sblossfinal_row = new StringBuilder();
                    StringBuilder sblossfinal_expense = new StringBuilder();

                    StringBuilder sbFactoryExpense = new StringBuilder();
                    StringBuilder sbMarkettedByCharges = new StringBuilder();
                    StringBuilder sbOther = new StringBuilder();
                    StringBuilder sbTotalExpence = new StringBuilder();
                    StringBuilder sbProfit = new StringBuilder();

                    int UnitMeasure = 0;
                    decimal PackingSize = 0;

                    sbdata.Append("<h5 class='text-primary'>Other Costing/Unit</h5><table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                    sbPerLtrUnit.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                    sblossfinal.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");
                    sblossfinal_expense.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                    sblabour.Append("<tr>");
                    sbloss.Append("<tr>");
                    sbtotal.Append("<thead class='thead-light'><tr>");

                    sbdata.Append("<th>");
                    sbdata.Append("Packing Size");
                    sbdata.Append("</th>");

                    sbPerLtrUnit.Append("<th>Packing Size</th>");
                    sblossfinal.Append("<th>Packing Size</th>");
                    sblossfinal_expense.Append("<th>Expense</th>");

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

                        sblossfinal_expense.Append("<th>");
                        sblossfinal_expense.Append(changedisplayname(Common.ConvertString(ds.Tables[2].Rows[k]["packingdetaildisplay"])));
                        sblossfinal_expense.Append("</th>");

                        if (k == 0)
                        {
                            sblabour.Append("<td>Labour</td>");
                            sbloss.Append("<td>Packing Loss</td>");
                            sbtotal.Append("<th>Total</th>");
                            sbPerLtr.Append("<td>Per Ltr Costing</td>");
                            sbPerUnit.Append("<td>Per Unit Costing</td>");
                            sblossfinal_row.Append("<td>Difference</td>");
                            sbFactoryExpense.Append("<td>Factory Expense</td>");
                            sbMarkettedByCharges.Append("<td>Marketted By Charges</td>");
                            sbOther.Append("<td>Other</td>");
                            sbTotalExpence.Append("<td>Total Expence</td>");
                            sbProfit.Append("<td>Profit</td>");
                        }

                        sblabour.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["labourcost"]) + "</td>");
                        sbloss.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["packingloss"]) + "</td>");

                        sbPerLtr.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["final"]) + "</td>");
                        UnitMeasure = Common.ConvertInt(ds.Tables[2].Rows[k]["packingMeasurementId"]);
                        PackingSize = Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingsize"]);
                        decimal CostPerLtr = Common.ConvertDecimal(ds.Tables[0].Rows[0]["final"]);

                        decimal unit = 0;

                        decimal TotalPMCost = Common.ConvertDecimal(ds.Tables[1].Compute("sum(" + ds.Tables[2].Rows[k]["packingdetaildisplay"] + ")", ""));

                        if (PackingSize > 1 && UnitMeasure == 1 || UnitMeasure == 2)
                        {
                            labourcost = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]) * PackingSize);
                            packingloss = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]) * PackingSize);


                            unit = (CostPerLtr * (PackingSize)) + TotalPMCost + (labourcost) + (packingloss);
                            //unit = Common.ConvertDecimal(ds.Tables[2].Rows[k]["final"]) * PackingSize;

                        }
                        else if (PackingSize < 1000 && UnitMeasure == 6 || UnitMeasure == 7)
                        {
                            labourcost = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]));
                            packingloss = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]));
                            unit = (CostPerLtr / (1000 / PackingSize)) + TotalPMCost + (labourcost) + (packingloss);

                        }
                        else if (PackingSize == 1 && UnitMeasure == 1 || UnitMeasure == 2)
                        {
                            labourcost = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]));
                            packingloss = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]));
                            unit = CostPerLtr + TotalPMCost + (labourcost) + (packingloss);

                        }
                        sbPerUnit.Append("<td>" + Common.ConvertDecimal(unit) + "</td>");

                        Decimal total = Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]) + Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]);

                        sbtotal.Append("<th>" + Common.ConvertDecimal(total.ToString()) + "</th>");

                        sblossfinal_row.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["diff"]) + "</td>");

                        sbFactoryExpense.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["FactoryExpense"] + "(" + ds.Tables[3].Rows[k]["FactoryExpensePercentage"] + " %)") + "</td>");
                        sbMarkettedByCharges.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["MarketedCharge"] + "(" + ds.Tables[3].Rows[k]["MarketedChargePercentage"] + " %)") + "</td>");
                        sbOther.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["Other"] + "(" + ds.Tables[3].Rows[k]["OtherPercentage"] + " %)") + "</td>");
                        sbTotalExpence.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["TotalExpense"]) + "</td>");
                        sbProfit.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["Profit"] + "(" + ds.Tables[3].Rows[k]["ProfitPercentage"] + " %)") + "</td>");

                    }

                    sblabour.Append("</tr>");
                    sbloss.Append("</tr>");
                    sbtotal.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");

                    sbPerLtr.Append("</tr>");
                    sbPerUnit.Append("</tr>");

                    sbPerLtrUnit.Append("</thead></tr><h5 class='text-primary'>Final Cost Packing Wise ( BULK + PM + Other )</h5>");
                    sbPerLtrUnit.Append(sbPerUnit);
                    sbPerLtrUnit.Append(sbPerLtr);

                    sbPerLtrUnit.Append("</tbody></table><h5 class='text-primary'>Packing Difference from Master Pack [" + masterpackname + "]</h5>");

                    sbdata.Append(sblabour);
                    sbdata.Append(sbloss);
                    sbdata.Append(sbtotal);

                    sbdata.Append(sbPerLtrUnit);

                    sblossfinal.Append("</tr>");


                    sblossfinal.Append(sblossfinal_row);

                    //sblossfinal.Append("</tbody></table>");
                    sbdata.Append(sblossfinal);


                    sbdata.Append("</tbody></table><h5 class='text-primary'>Gujarat Pesticide Factory Expence</h5>");
                    sbdata.Append("</tr>");

                    sbdata.Append(sblossfinal_expense);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append(sbFactoryExpense);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append(sbMarkettedByCharges);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append(sbOther);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append(sbTotalExpence);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append(sbProfit);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    //sbdata.Append(sblossfinal_expense);


                }
                html = sbdata.ToString();

            }
        }
        public static void FinishedGoodReportDetailActual(int UserID, int ProductID, out string html, out string bulkname)
        {

            html = "";
            bulkname = "";

            DBHelper dbhelper = new DBHelper();
            CommonDAL common = new CommonDAL();
            FinishGoodsPricingReportDAL FG = new FinishGoodsPricingReportDAL();

            DataSet ds = FG.FinishedGoodReportDetailActual( 1, Convert.ToInt32(ProductID));

            if (ds.Tables[0].Rows.Count > 0)
            {
                bulkname = Common.ConvertString(ds.Tables[0].Rows[0]["BulkproductName"]);
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
                    decimal labourcost = 0;
                    decimal packingloss = 0;
                    string masterpackname = "";

                    DataRow[] dr = ds.Tables[2].Select("isMasterpacking=True");
                    if (dr.Length > 0)
                    {
                        masterpackname = changedisplayname(dr[0]["packingdetaildisplay"].ToString());
                        bulkname += " [ <span class='text-primary'>" + masterpackname + " (Actual)</span> ]";
                    }


                    StringBuilder sblabour = new StringBuilder();
                    StringBuilder sbloss = new StringBuilder();
                    StringBuilder sbtotal = new StringBuilder();

                    StringBuilder sbPerLtrUnit = new StringBuilder();
                    StringBuilder sbPerLtr = new StringBuilder();
                    StringBuilder sbPerUnit = new StringBuilder();

                    StringBuilder sblossfinal = new StringBuilder();

                    StringBuilder sblossfinal_row = new StringBuilder();
                    StringBuilder sblossfinal_expense = new StringBuilder();

                    StringBuilder sbFactoryExpense = new StringBuilder();
                    StringBuilder sbMarkettedByCharges = new StringBuilder();
                    StringBuilder sbOther = new StringBuilder();
                    StringBuilder sbTotalExpence = new StringBuilder();
                    StringBuilder sbProfit = new StringBuilder();

                    int UnitMeasure = 0;
                    decimal PackingSize = 0;

                    sbdata.Append("<h5 class='text-primary'>Other Costing/Unit</h5><table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                    sbPerLtrUnit.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                    sblossfinal.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");
                    sblossfinal_expense.Append("<table class='table table-bordered gridview dataTable no-footer dtr-inline'><tbody><thead class='thead-light'><tr>");

                    sblabour.Append("<tr>");
                    sbloss.Append("<tr>");
                    sbtotal.Append("<thead class='thead-light'><tr>");

                    sbdata.Append("<th>");
                    sbdata.Append("Packing Size");
                    sbdata.Append("</th>");

                    sbPerLtrUnit.Append("<th>Packing Size</th>");
                    sblossfinal.Append("<th>Packing Size</th>");
                    sblossfinal_expense.Append("<th>Expense</th>");

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

                        sblossfinal_expense.Append("<th>");
                        sblossfinal_expense.Append(changedisplayname(Common.ConvertString(ds.Tables[2].Rows[k]["packingdetaildisplay"])));
                        sblossfinal_expense.Append("</th>");

                        if (k == 0)
                        {
                            sblabour.Append("<td>Labour</td>");
                            sbloss.Append("<td>Packing Loss</td>");
                            sbtotal.Append("<th>Total</th>");
                            sbPerLtr.Append("<td>Per Ltr Costing</td>");
                            sbPerUnit.Append("<td>Per Unit Costing</td>");
                            sblossfinal_row.Append("<td>Difference</td>");
                            sbFactoryExpense.Append("<td>Factory Expense</td>");
                            sbMarkettedByCharges.Append("<td>Marketted By Charges</td>");
                            sbOther.Append("<td>Other</td>");
                            sbTotalExpence.Append("<td>Total Expence</td>");
                            sbProfit.Append("<td>Profit</td>");
                        }

                        sblabour.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["labourcost"]) + "</td>");
                        sbloss.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["packingloss"]) + "</td>");

                        sbPerLtr.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["final"]) + "</td>");
                        UnitMeasure = Common.ConvertInt(ds.Tables[2].Rows[k]["packingMeasurementId"]);
                        PackingSize = Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingsize"]);
                        decimal CostPerLtr = Common.ConvertDecimal(ds.Tables[0].Rows[0]["final"]);

                        decimal unit = 0;

                        decimal TotalPMCost = Common.ConvertDecimal(ds.Tables[1].Compute("sum(" + ds.Tables[2].Rows[k]["packingdetaildisplay"] + ")", ""));

                        if (PackingSize > 1 && UnitMeasure == 1 || UnitMeasure == 2)
                        {
                            labourcost = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]) * PackingSize);
                            packingloss = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]) * PackingSize);


                            unit = (CostPerLtr * (PackingSize)) + TotalPMCost + (labourcost) + (packingloss);
                            //unit = Common.ConvertDecimal(ds.Tables[2].Rows[k]["final"]) * PackingSize;

                        }
                        else if (PackingSize < 1000 && UnitMeasure == 6 || UnitMeasure == 7)
                        {
                            labourcost = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]));
                            packingloss = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]));
                            unit = (CostPerLtr / (1000 / PackingSize)) + TotalPMCost + (labourcost) + (packingloss);

                        }
                        else if (PackingSize == 1 && UnitMeasure == 1 || UnitMeasure == 2)
                        {
                            labourcost = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]));
                            packingloss = (Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]));
                            unit = CostPerLtr + TotalPMCost + (labourcost) + (packingloss);

                        }
                        sbPerUnit.Append("<td>" + Common.ConvertDecimal(unit) + "</td>");

                        Decimal total = Common.ConvertDecimal(ds.Tables[2].Rows[k]["labourcost"]) + Common.ConvertDecimal(ds.Tables[2].Rows[k]["packingloss"]);

                        sbtotal.Append("<th>" + Common.ConvertDecimal(total.ToString()) + "</th>");

                        sblossfinal_row.Append("<td>" + Common.ConvertString(ds.Tables[2].Rows[k]["diff"]) + "</td>");

                        sbFactoryExpense.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["FactoryExpense"] + "(" + ds.Tables[3].Rows[k]["FactoryExpensePercentage"] + " %)") + "</td>");
                        sbMarkettedByCharges.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["MarketedCharge"] + "(" + ds.Tables[3].Rows[k]["MarketedChargePercentage"] + " %)") + "</td>");
                        sbOther.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["Other"] + "(" + ds.Tables[3].Rows[k]["OtherPercentage"] + " %)") + "</td>");
                        sbTotalExpence.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["TotalExpense"]) + "</td>");
                        sbProfit.Append("<td>" + Common.ConvertString(ds.Tables[3].Rows[k]["Profit"] + "(" + ds.Tables[3].Rows[k]["ProfitPercentage"] + " %)") + "</td>");

                    }

                    sblabour.Append("</tr>");
                    sbloss.Append("</tr>");
                    sbtotal.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");

                    sbPerLtr.Append("</tr>");
                    sbPerUnit.Append("</tr>");

                    sbPerLtrUnit.Append("</thead></tr><h5 class='text-primary'>Final Cost Packing Wise ( BULK + PM + Other )</h5>");
                    sbPerLtrUnit.Append(sbPerUnit);
                    sbPerLtrUnit.Append(sbPerLtr);

                    sbPerLtrUnit.Append("</tbody></table><h5 class='text-primary'>Packing Difference from Master Pack [" + masterpackname + "]</h5>");

                    sbdata.Append(sblabour);
                    sbdata.Append(sbloss);
                    sbdata.Append(sbtotal);

                    sbdata.Append(sbPerLtrUnit);

                    sblossfinal.Append("</tr>");


                    sblossfinal.Append(sblossfinal_row);

                    sblossfinal.Append("</tbody></table>");
                    sbdata.Append(sblossfinal);


                    sbdata.Append("</tbody></table><h5 class='text-primary'>Gujarat Pesticide Factory Expence</h5>");
                    sbdata.Append("</tr>");

                    sbdata.Append(sblossfinal_expense);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append(sbFactoryExpense);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append(sbMarkettedByCharges);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append(sbOther);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append(sbTotalExpence);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append(sbProfit);
                    sbdata.Append("</tr>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</thead></tr>");
                    sbdata.Append("</thead></tr>");
                    //sbdata.Append(sblossfinal_expense);


                }
                html = sbdata.ToString();
            }
        }

    }
}