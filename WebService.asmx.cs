using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;
using DAL;
using Newtonsoft;
using Newtonsoft.Json;
using BAL;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Reflection.Emit;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI.HtmlControls;

namespace Production_Costing_Software
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        FinishGoodsPricingReportDAL FG = new FinishGoodsPricingReportDAL();
        RMEstimateDetailDAL RMED = new RMEstimateDetailDAL();
        TermsConditionDAL TC = new TermsConditionDAL();


        [WebMethod]
        public CascadingDropDownNameValue[] BindRMName(string knownCategoryValues, string category)
        {
            string categoryId = AjaxControlToolkit.CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["undefined"];

            DataTable dtrm = common.DropdownList("RMMasterByRMCategory", categoryId.ToString(), "");

            List<CascadingDropDownNameValue> RMMaster = new List<CascadingDropDownNameValue>();
            if (dtrm.Rows.Count > 0)
            {
                foreach (DataRow DR in dtrm.Rows)
                {
                    string RMId = DR["Id"].ToString();
                    string RMName = DR["Name"].ToString();
                    RMMaster.Add(new CascadingDropDownNameValue(RMName, RMId));
                }
            }
            return RMMaster.ToArray();
        }

        [WebMethod]
        public List<DrpModel> BindPackSize(string BpmId)
        {
            DataTable dtrm = common.DropdownList("PacksizeByBPM", BpmId.ToString(), "");
            List<DrpModel> drp = new List<DrpModel>();

            foreach (DataRow dr in dtrm.Rows)
            {
                drp.Add(new DrpModel()
                {
                    Id = dr["Id"].ToString(),
                    Name = dr["Name"].ToString(),
                });

            }

            return drp;
        }

        [WebMethod]
        public List<DrpModel> PackStyleCatByPackSize(string PackSize)
        {

            DataTable dtrm = common.DropdownList("PackStyleCatByPackSize", PackSize.ToString(), "");
            List<DrpModel> drp = new List<DrpModel>();

            foreach (DataRow dr in dtrm.Rows)
            {
                drp.Add(new DrpModel()
                {
                    Id = dr["Id"].ToString(),
                    Name = dr["Name"].ToString()
                });

            }

            return drp;
        }
        [WebMethod]
        public List<DrpModel> PackStyleByPackstyleCatagory(string PackSizeCatId)
        {
            DataTable dtrm = common.DropdownList("PackStyleByPackstyleCatagory", PackSizeCatId.ToString(), "");
            List<DrpModel> drp = new List<DrpModel>();

            foreach (DataRow dr in dtrm.Rows)
            {
                drp.Add(new DrpModel()
                {
                    Id = dr["Id"].ToString(),
                    Name = dr["Name"].ToString()
                });

            }

            return drp;
        }

        [WebMethod]
        public string GetIsFullPurityByRM(string RMId)
        {
            DataTable dtrm = common.DropdownList("IsPurityByRM", RMId.ToString(), "");
            string isFullPurity;
            isFullPurity = dtrm.Rows[0]["Name"].ToString();

            return isFullPurity;

        }
        [WebMethod]
        public string GetActualRateKgByRM(string RMCat, string RMId)
        {
            string powerwork = "0.00";

            RMPriceEstimateDAL rmp = new RMPriceEstimateDAL();
            DataTable dtrpm = rmp.Get_RMPriceActualRatePerKg(RMCat, RMId);
            if (dtrpm.Rows.Count > 0)
            {
                powerwork = dtrpm.Rows[0]["RateKgLtr"].ToString() + "~" + dtrpm.Rows[0]["TransporationRate"].ToString() + "~" + dtrpm.Rows[0]["Quantity"].ToString() + "~" + dtrpm.Rows[0]["RMPriceId"].ToString() +
                    "~" + dtrpm.Rows[0]["IsPurity"].ToString() + "~" + dtrpm.Rows[0]["PurityPercentage"].ToString();

            }

            return powerwork;

        }
        [WebMethod]
        public string GetFormulationCharges(string FormaulationId)
        {
            string FinalcostLtr = "0.00";
            if (FormaulationId != "0")
            {
                DataTable dtrm = common.DropdownList("Formulation_Total", FormaulationId.ToString(), "");

                FinalcostLtr = dtrm.Rows[0]["FinalcostLtr"].ToString();


            }
            return FinalcostLtr;

        }
        [WebMethod]
        public string GetBulkCostPerLtr(string BulkProductId)
        {
            string BulkCostPerLtr = "0.00";
            if (BulkCostPerLtr != "0")
            {
                DataTable dtbulk = common.DropdownList("BulkCostPerLtr", BulkProductId.ToString(), "");

                BulkCostPerLtr = dtbulk.Rows[0]["finalcost"].ToString();


            }
            return BulkCostPerLtr;

        }
        [WebMethod]
        public string CostPerLtrByPackMaterial(string BulkProductId)
        {
            string CostPerLtr = "0.00";
            if (CostPerLtr != "0")
            {
                DataTable dtbulk = common.DropdownList("CostPerLtrByPackMaterial", BulkProductId.ToString(), "");

                CostPerLtr = dtbulk.Rows[0]["CostPerLtr"].ToString();


            }
            return CostPerLtr;

        }
        [WebMethod]

        public string GetBulkCostPerLtrWithMasterPack(string BulkProductId)
        {
            string MasterPack = "";
            if (MasterPack != "0")
            {
                DataTable dtrm = common.DropdownList("GetBulkCostPerLtrWithMasterPack", BulkProductId.ToString(), "");

                MasterPack = dtrm.Rows[0]["MasterPack"].ToString() + "~" + dtrm.Rows[0]["FkBulkProductId"].ToString();


            }
            return MasterPack;

        }
        [WebMethod]

        public List<DrpModel> BindRM(string RMId)
        {
            DataTable dtrm = common.DropdownList("RMMasterByRMCategory", RMId.ToString(), "");

            List<DrpModel> drp = new List<DrpModel>();

            foreach (DataRow dr in dtrm.Rows)
            {
                drp.Add(new DrpModel()
                {
                    Id = dr["Id"].ToString(),
                    Name = dr["Name"].ToString()
                });

            }

            return drp;
        }
        [WebMethod]

        public string GetDataByPackStyleCatPackStyle(string PackSizeCatId, string PackStyleId)
        {
            string powerwork = "0.00";

            ProductwiseLabourCostDAL pwlc = new ProductwiseLabourCostDAL();
            DataTable dtPWLC = pwlc.GetDataByPackStyleCatPackStyle(PackSizeCatId, PackStyleId);
            if (dtPWLC.Rows.Count > 0)
            {
                powerwork = dtPWLC.Rows[0]["Worker"].ToString() + "~" + dtPWLC.Rows[0]["LabourCharge"].ToString() + "~" + dtPWLC.Rows[0]["PackingStylePowerUnit"].ToString() + "~" + dtPWLC.Rows[0]["TotalPowerCost"].ToString() + "~" + dtPWLC.Rows[0]["Lcharge"] + "~" + dtPWLC.Rows[0]["Svcharge"].ToString();

            }

            return powerwork;
        }

        [WebMethod]

        public List<DrpModel> BindPMRMName(string PMRMCatId)
        {
            DataTable dtrm = common.DropdownList("PMRMNameByPMRMCategory", PMRMCatId.ToString(), "");

            List<DrpModel> drp = new List<DrpModel>();

            foreach (DataRow dr in dtrm.Rows)
            {
                drp.Add(new DrpModel()
                {
                    Id = dr["Id"].ToString(),
                    Name = dr["Name"].ToString()
                });


            }

            return drp;
        }
        [WebMethod]

        public string BindUnitByPMRMName(string PMRMCatId)
        {
            string PMRM_Unit = "0.00";
            //string Measurement = "0.00";

            if (PMRM_Unit != "0")
            {
                DataTable dtrm = common.DropdownList("UnitByPMRMName", PMRMCatId.ToString(), "");

                PMRM_Unit = dtrm.Rows[0]["Unit"].ToString() + "~" + dtrm.Rows[0]["EnumDescription"] + "~" + dtrm.Rows[0]["FkUnitMeasurementId"].ToString();


            }
            return PMRM_Unit;
        }
        [WebMethod]
        public string[] RMsearch(string searchtxt)
        {
            List<string> customers = new List<string>();

            DataTable dtrm = common.DropdownList("RMsearch", searchtxt.ToString(), "");

            foreach (DataRow dr in dtrm.Rows)
            {
                customers.Add(string.Format("{0}~~{1}", dr["Id"], dr["Name"]));
            }

            return customers.ToArray();
        }

        [WebMethod]
        public List<DrpModel> Shipper(string type)
        {
            List<DrpModel> drp = new List<DrpModel>();

            DataTable dtrm = common.DropdownList(type, "", "");

            foreach (DataRow dr in dtrm.Rows)
            {
                drp.Add(new DrpModel()
                {
                    Id = dr["Id"].ToString(),
                    Name = dr["Name"].ToString()
                });

            }

            return drp;
        }

        [WebMethod]
        public List<DrpModel> ShippeProduct(int type)
        {
            List<DrpModel> drp = new List<DrpModel>();

            DataTable dtrm = common.DropdownList("ShippeProduct", type.ToString(), "");

            foreach (DataRow dr in dtrm.Rows)
            {
                drp.Add(new DrpModel()
                {
                    Id = dr["Id"].ToString(),
                    Name = dr["Name"].ToString()
                });

            }

            return drp;
        }

        [WebMethod]
        public string PMRMCost(string pmrmid)
        {
            string cost = "0.00~~0.00";

            DataTable dtrm = common.DropdownList("PMRMCost", pmrmid.ToString(), "");

            if (dtrm.Rows.Count > 0)
            {
                cost = Common.ConvertString(dtrm.Rows[0]["costperUnit"]) + "~~" + Common.ConvertString(dtrm.Rows[0]["FinalPrice"]);
            }

            return cost;
        }


        [WebMethod]
        public List<DrpModel> GetTermsCondition(string FkBulkProductId)
        {

            List<DrpModel> drp = new List<DrpModel>();
            DataTable dtrm = common.DropdownList("GetTermsCondition", "", "");
            foreach (DataRow dr in dtrm.Rows)
            {
                drp.Add(new DrpModel()
                {
                    Id = dr["Id"].ToString(),
                    Name = dr["Name"].ToString()
                });

            }

            return drp;
        }

        [WebMethod]
        public string GetPriceByPMRMCatId(string pmrmid)
        {
            string Price = "0.00";

            DataTable dtrm = common.DropdownList("GetPMRMPriceFromPMRMId", pmrmid.ToString(), "");

            if (dtrm.Rows.Count > 0)
            {
                Price = Common.ConvertString(dtrm.Rows[0]["Price"]);
            }

            return Price;
        }
        [WebMethod]
        public string AddShareName(string FkBulkProductId, string ShareName, string Mobile, string FkCompanyId, string PackingType, string Packingsize, string ProfitPer, string FinalPrice, string AddDiscount, string TermsCondId)
        {
            DBHelper dbhelper = new DBHelper();
            CommonDAL common = new CommonDAL();
            BulkCostDAL pds = new BulkCostDAL();
            ShareforBulkCostBAL sbdata = new ShareforBulkCostBAL();

            sbdata.ShareName = ShareName;
            sbdata.FkBulkProductId = Common.ConvertInt(FkBulkProductId);
            sbdata.Mobile = Mobile;
            sbdata.FkCompanyId = Common.ConvertInt(FkCompanyId);
            sbdata.PackingType = Common.ConvertInt(PackingType);
            sbdata.Packingsize = Common.ConvertDecimal(Packingsize);
            sbdata.ProfitPer = Common.ConvertDecimal(ProfitPer);
            sbdata.FinalPrice = Common.ConvertDecimal(FinalPrice);
            sbdata.AddDiscount = Common.ConvertDecimal(AddDiscount);
            sbdata.TermsCondId = Common.ConvertString(TermsCondId);


            if (ShareName == "" || Mobile == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Name or Mobile')", true);
                return "0";
            }
            sbdata.FkBulkProductId = Common.ConvertInt(FkBulkProductId);

            sbdata.FkCompanyId = Common.ConvertInt(FkCompanyId);
            sbdata.Mobile = Mobile;
            sbdata.Action = 1;
            pds.InsertUpdateShareforBulkCost(sbdata);
            return "1";
        }
        [WebMethod]
        public string SaveBulkCost(string FkBulkProductId, string FkCompanyId, string PackingType, string Packingsize, string ProfitPer, string FinalPrice, string AddDiscount)
        {
            DBHelper dbhelper = new DBHelper();
            CommonDAL common = new CommonDAL();
            BulkCostDAL pds = new BulkCostDAL();
            BulkCostBAL sbdata = new BulkCostBAL();


            sbdata.FkBulkProductId = Common.ConvertInt(FkBulkProductId);
            sbdata.FkCompanyId = Common.ConvertInt(FkCompanyId);
            //sbdata.Mobile = Mobile;
            //sbdata.ShareName = ShareName;
            sbdata.PackingType = Common.ConvertInt(PackingType);
            sbdata.Packingsize = Common.ConvertDecimal(Packingsize);
            sbdata.ProfitPer = Common.ConvertDecimal(ProfitPer);
            sbdata.FinalPrice = Common.ConvertDecimal(FinalPrice);
            sbdata.AddDiscount = Common.ConvertDecimal(AddDiscount);

            sbdata.Action = 1;

            //if (ShareName == "" || Mobile == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Name or Mobile')", true);
            //    return "0";
            //}
            if (PackingType!=""&& Packingsize!="0.00"&& FinalPrice!="0.00")
            {
                pds.InsertUpdateBulkCost(sbdata);
                return "1";
            }
            else
            {
                return "0";
            }


            
        }

        [WebMethod]
        public string ShowHistory(string FkBulkProductId)
        {
            DBHelper dbhelper = new DBHelper();
            CommonDAL common = new CommonDAL();
            TermsConditionDAL tc = new TermsConditionDAL();
            TermsConditionBAL tcddata = new TermsConditionBAL();

            int BPMId = Common.ConvertInt(FkBulkProductId);


            StringBuilder sbdata = new StringBuilder();
            StringBuilder Totaldbdata = new StringBuilder();


            DataSet ds = tc.ShowSharedAllReportBPMId(FkBulkProductId);

            if (ds.Tables[0].Rows.Count > 0)
            {


                sbdata.Append("<table class='table table-bordered table-striped gridview dataTable no-footer dtr-inline col-sm-12'><tbody>"
           + "<tr>"
               + "<th scope='col'>No</th>"
               + "<th scope='col'>BulkProductName</th>"
               + "<th scope='col'>ShareName</th>"
               + "<th scope='col'>Mobile</th>"
               + "<th scope='col'>AsOnDate</th>"
               + "<th scope='col'>Packing Size</th>"
               + "<th scope='col'>Packing Type</th>"
               + "<th scope='col'>Profit (%)</th>"
               + "<th scope='col'>FinalPrice</th>"
               + "<th scope='col'>Report</th>"
           + "</tr>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string ShareNameId = Common.ConvertString(ds.Tables[0].Rows[i]["ShareNameId"]);
                    string BulkProductId = Common.ConvertString(ds.Tables[0].Rows[i]["FkBulkProductId"]);
                    int t = i + 1;
                    sbdata.Append("<tr>"
                        + "<td>" + t + "</td>"
                        + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["BulkProductName"]) + "</td>"
                        + "<td >" + Common.ConvertString(ds.Tables[0].Rows[i]["ShareName"]) + "</td>"
                        + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["Mobile"]) + "</td>"
                        + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["AsOnDate"]) + "</td>"
                        + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["Packingsize"]) + "</td>"
                        + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["PMRMName"]) + "</td>"
                        + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["ProfitPer"]) + "</td>"
                        + "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["FinalPrice"]) + "</td>"
                        //+ "<td>" + Common.ConvertString(ds.Tables[0].Rows[i]["FkBulkProductId"]) + "</td>"
                        + "<td> <a onclick=viewreport(" + ShareNameId + "," + BulkProductId + ");  class='btn btn-primary id=" + ShareNameId + ""+ BulkProductId + " runat=server'>View</a> </td>"
                    + "</tr>");
                }


            }
            sbdata.Append("</thead></tbody></table>");

            return sbdata.ToString();


        }

        [WebMethod]
        public string AddTermsCondition(string FkBulkProductId, string FkCompanyId, string Id)
        {
            DBHelper dbhelper = new DBHelper();
            CommonDAL common = new CommonDAL();
            TermsConditionDAL pds = new TermsConditionDAL();
            TermsConditionBAL sbdata = new TermsConditionBAL();

            string TermsconditionId = Id;
            int BPMId = Common.ConvertInt(FkBulkProductId);
            sbdata.FkCompanyId = Common.ConvertInt(FkCompanyId);

            //checked for Exist Terms by BPM
            ReturnMessage objs = common.CheckExist("GetTermsBulkCostByBPM", FkBulkProductId, "", "");
            string msgs = Common.ConvertString(objs.Message);

            if (Common.ConvertInt(objs.ReturnValue) == 0)
            {
                sbdata.action = 2;
            }
            else
            {
                sbdata.action = 1;

            }
            //pds.InsertUpdateTermsConditionBulkCost(sbdata, BPMId, TermsconditionId);


            return "1";
        }

        [WebMethod]
        public string PDSave(string pd)
        {
            DBHelper dbhelper = new DBHelper();
            CommonDAL common = new CommonDAL();
            PackingDifferenceDAL pds = new PackingDifferenceDAL();
            PackingDifferenceBAL pddata = new PackingDifferenceBAL();

            if (pd.Length > 0)
            {
                string[] lst = pd.Split(',');
                for (int i = 0; i < lst.Length; i++)
                {
                    pddata = new PackingDifferenceBAL();
                    if (lst[i] != "")
                    {
                        string[] data = lst[i].Split('$');
                        for (int j = 0; j < data.Length; j++)
                        {
                            if (j == 0)
                            {
                                pddata.SuggestedDifference = Common.ConvertDecimal(data[j].Replace("~", ""));
                            }
                            else
                            {
                                string[] company = data[j].Split('~');
                                if (company.Length == 3)
                                {
                                    pddata.FkCompanyId = Common.ConvertInt(company[0]);
                                    pddata.FkPackingMaterialIId = Common.ConvertInt(company[1]);
                                    pddata.CompanyDifference = Common.ConvertDecimal(company[2]);

                                    pds.InsertUpdatePackingDifferenceMaster(pddata);
                                }
                            }

                        }



                    }
                }
            }
            return "1";
        }

        [WebMethod]
        public string PDMRPSave(string pd)
        {
            DBHelper dbhelper = new DBHelper();
            CommonDAL common = new CommonDAL();
            MRPDAL mp = new MRPDAL();
            MRPBAL MRPdata = new MRPBAL();

            if (pd.Length > 0)
            {
                string[] lst = pd.Split(',');
                for (int i = 0; i < lst.Length; i++)
                {
                    MRPdata = new MRPBAL();
                    if (lst[i] != "")
                    {
                        string[] data = lst[i].Split('$');
                        for (int j = 0; j < data.Length; j++)
                        {
                            if (j == 0)
                            {
                                MRPdata.CompanyMRP = Common.ConvertDecimal(data[j].Replace("~", ""));
                            }
                            else
                            {
                                string[] company = data[j].Split('~');
                                if (company.Length == 3)
                                {
                                    MRPdata.FkCompanyId = Common.ConvertInt(company[0]);
                                    MRPdata.FkPackingMaterialId = Common.ConvertInt(company[1]);
                                    MRPdata.CompanyMRP = Common.ConvertDecimal(company[2]);

                                    mp.InsertUpdatePackingDifferenceMaster(MRPdata);
                                }
                            }

                        }



                    }
                }
            }
            return "1";
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
        [WebMethod]
        public string BulkCostShareReport(string FkBulkProductId, string PackingSize, string PackingType, string FinalPrice, string TermsCondId)
        {

            StringBuilder sbdata = new StringBuilder();
            //StringBuilder sbTerms = new StringBuilder();
            string sbTerms = "";


            DataSet ds = TC.TermsConditionListByBPMId(FkBulkProductId, PackingSize, PackingType, TermsCondId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    sbdata.Append("<table  border='1' ><tbody>");
                    sbdata.Append("<tr><th  colspan='2'><i style='color:blue;font-size:20px'>Report To Share</i></th>");
                    //sbdata.Append("</tr>");
                    //sbdata.Append("<tr><th style='height:20px' colspan='2'></th>");
                    //sbdata.Append("</tr>");
                    sbdata.Append("<tr><th style='text-align:left'><p style='font-size:18px;margin-left:4px'> Date</p></th><th '><p>" + DateTime.Now.Date.ToShortDateString() + "</p></th></tr>");
                    sbdata.Append("<tr><th style='text-align:left'><p style='font-size:18px;margin-left:4px'> Technical Name</p></th><th style='margin-left:4px'><p>" + Common.ConvertString(ds.Tables[1].Rows[0]["BulkProductName"]) + "</p></th></tr>");
                    sbdata.Append("<tr><th style='text-align:left'><p style='font-size:18px;margin-left:4px'> Packing Type</p></th><th style='margin-left:4px'><p>" + Common.ConvertString(ds.Tables[2].Rows[0]["PMRMName"]) + "<p/></th></tr>");
                    sbdata.Append("<tr><th style='text-align:left'><p style='font-size:18px;margin-left:4px'> Packing Size</p></th><th style='margin-left:4px'><p>" + PackingSize + "</p></th></tr>");
                    sbdata.Append("<tr><th style='text-align:left'><p style='font-size:18px;margin-left:4px'> Price / L or Kg</p></th><th style='margin-left:4px'><p>" + FinalPrice + "</p></th></tr>");
                }
                //Terms & Condition
                //sbdata.Append("<table  border='1' style='text-align:center'><tbody>");
                //sbdata.Append("<tr><th style='height:20px' colspan='2'></th>");
                //sbdata.Append("</tr>");
                sbdata.Append("<tr><th colspan='2'><p style='font-size:18px;margin-top:3px'>Terms & Condition</p></th>");
                sbdata.Append("</tr>");

                //sbTerms.Append("<tr><th ><p>Terms & Condition</p></th>");
                sbdata.Append("<tr style='border-bottom:white'>");

                for (int r = 0; r < ds.Tables[0].Rows.Count; r++)
                {
                    sbdata.Append("<th colspan='2' style='text-align:left!important;'>" + Common.ConvertString(ds.Tables[0].Rows[r]["No"]) + ". " + Common.ConvertString(ds.Tables[0].Rows[r]["TermsCondition"]) + "</th>");
                    sbdata.Append("</tr>");
                    sbdata.Append("<tr style='border-bottom:white'>");


                    sbTerms += " " + Common.ConvertString(ds.Tables[0].Rows[r]["No"]) + " ." + Common.ConvertString(ds.Tables[0].Rows[r]["TermsCondition"]) + "%0a";

                }

                sbdata.Append("</thead></tbody></table>");



            }

            return sbdata.ToString() + "~" + ds.Tables[1].Rows[0]["BulkProductName"] + "~" + Common.ConvertString(ds.Tables[2].Rows[0]["PMRMName"]) + "~" + PackingSize + "~" + FinalPrice + "~" + DateTime.Now.Date.ToShortDateString() + "~" + sbTerms.ToString();

        }

        [WebMethod]
        public string GetReportbyBPMId(string productid, string IsEstimate)
        {
            StringBuilder sbdata = new StringBuilder();
            if (IsEstimate != "0")
            {

                DataSet ds = RMED.GetRMEstimateDetailReport(Convert.ToInt32(productid), 1);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //exampleModalLabel.InnerHtml = Common.ConvertString(ds.Tables[0].Rows[0]["BulkproductName"]);


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
                            //exampleModalLabel.InnerHtml += " [ <span class='text-primary'>" + masterpackname + "</span> ]";
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
                            decimal unit = 0;
                            decimal CostPerLtr = Common.ConvertDecimal(ds.Tables[0].Rows[0]["final"]);

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
                    return sbdata.ToString();
                }


            }
            else
            {


                DataSet ds = FG.FinishedGoodReportDetailActual(1, Convert.ToInt32(productid));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //exampleModalLabel.InnerHtml = Common.ConvertString(ds.Tables[0].Rows[0]["BulkproductName"]);


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
                            //exampleModalLabel.InnerHtml += " [ <span class='text-primary'>" + masterpackname + "</span> ]";
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
                    return sbdata.ToString();
                }

            }
            return sbdata.ToString(); ;
        }
        [WebMethod]
        public string PackingExits(string productid, string packsize, string packunit, string packcategory, string packingid)
        {
            string ret = "0";

            ReturnMessage objs = common.CheckExistOthers("Packing", productid, packsize, packunit, packcategory, packingid);
            ret = objs.ReturnValue.ToString();
            return ret;
        }
        public class DrpModel
        {
            public string Id { get; set; }
            public string Name { get; set; }

        }
    }
}