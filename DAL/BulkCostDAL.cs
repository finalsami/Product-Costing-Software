using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class BulkCostDAL
    {
        DBHelper dbhelper = null;
        public BulkCostDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable GetBulkCostList(int CompanyId, int UserId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_BulkCost");
                dbhelper.AddParameter("@FkCompanyId", CompanyId);
                dbhelper.AddParameter("@UserId", UserId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }

        public DataTable GetBulkCostHistory(int FkBulkProductId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_BulkCostHistoryByBPM");
                dbhelper.AddParameter("@FkBulkProductId", FkBulkProductId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdateShareforBulkCost(ShareforBulkCostBAL BC)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_ShareforBulkCost");
                dbhelper.AddParameter("@ShareName", BC.ShareName);
                dbhelper.AddParameter("@FkBulkProductId", BC.FkBulkProductId);
                dbhelper.AddParameter("@FkCompanyId", BC.FkCompanyId);
                dbhelper.AddParameter("@Mobile", BC.Mobile);
                dbhelper.AddParameter("@PackingType", BC.PackingType);
                dbhelper.AddParameter("@Packingsize", BC.Packingsize);
                dbhelper.AddParameter("@ProfitPer", BC.ProfitPer);
                dbhelper.AddParameter("@FinalPrice", BC.FinalPrice);
                dbhelper.AddParameter("@AddDiscount", BC.AddDiscount);
                dbhelper.AddParameter("@TermsCondId", BC.TermsCondId);
                dbhelper.AddParameter("@action", BC.Action);


                dbhelper.AddParameter("@UserId", 1);
                dbhelper.Command.Parameters.Add("@OUTVAL", System.Data.SqlDbType.Int);
                dbhelper.Command.Parameters["@OUTVAL"].Direction = System.Data.ParameterDirection.Output;
                dbhelper.Command.Parameters.Add("@OUTMESSAGE", System.Data.SqlDbType.VarChar, 500);
                dbhelper.Command.Parameters["@OUTMESSAGE"].Direction = System.Data.ParameterDirection.Output;
                dbhelper.ExecuteNonQuery();

                returnMessage.ReturnValue = Convert.ToInt16(dbhelper.Command.Parameters["@OUTVAL"].Value);
                returnMessage.Message = Convert.ToString(dbhelper.Command.Parameters["@OUTMESSAGE"].Value);

            }
            catch (Exception ex)
            {
                returnMessage.ReturnValue = -1;
                returnMessage.Message = Convert.ToString(ex.Message);
            }
            return returnMessage;
        }
        public ReturnMessage InsertUpdateBulkCost(BulkCostBAL BC)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_BulkCost");
                dbhelper.AddParameter("@ShareName", "");
                dbhelper.AddParameter("@FkBulkProductId", BC.FkBulkProductId);
                dbhelper.AddParameter("@FkCompanyId", BC.FkCompanyId);
                dbhelper.AddParameter("@Mobile", "");
                dbhelper.AddParameter("@PackingType", BC.PackingType);
                dbhelper.AddParameter("@Packingsize", BC.Packingsize);
                dbhelper.AddParameter("@ProfitPer", BC.ProfitPer);
                dbhelper.AddParameter("@FinalPrice", BC.FinalPrice);
                dbhelper.AddParameter("@AddDiscount", BC.AddDiscount);
                //dbhelper.AddParameter("@ReportImage", "");
   
                dbhelper.AddParameter("@action", BC.Action);

                dbhelper.AddParameter("@UserId", 1);
                dbhelper.Command.Parameters.Add("@OUTVAL", System.Data.SqlDbType.Int);
                dbhelper.Command.Parameters["@OUTVAL"].Direction = System.Data.ParameterDirection.Output;
                dbhelper.Command.Parameters.Add("@OUTMESSAGE", System.Data.SqlDbType.VarChar, 500);
                dbhelper.Command.Parameters["@OUTMESSAGE"].Direction = System.Data.ParameterDirection.Output;
                dbhelper.ExecuteNonQuery();

                returnMessage.ReturnValue = Convert.ToInt16(dbhelper.Command.Parameters["@OUTVAL"].Value);
                returnMessage.Message = Convert.ToString(dbhelper.Command.Parameters["@OUTMESSAGE"].Value);

            }
            catch (Exception ex)
            {
                returnMessage.ReturnValue = -1;
                returnMessage.Message = Convert.ToString(ex.Message);
            }
            return returnMessage;
        }


        //public ReturnMessage UpdateOtheCompanyPricelist(OtherCompanyPriceListBAL companyPrice)
        //{

        //    ReturnMessage returnMessage = new ReturnMessage();

        //    try
        //    {
        //        dbhelper.SpCommand("SP_InsertUpdate_OtherCompanyPriceList");
        //        dbhelper.AddParameter("@OtherComapnyPriceListId", companyPrice.OtherComapnyPriceListId);
        //        dbhelper.AddParameter("@Interest", companyPrice.Interest);
        //        dbhelper.AddParameter("@AdditionalBufferPM", companyPrice.AdditionalBufferPM);
        //        dbhelper.AddParameter("@AdditionalBufferLabour", companyPrice.AdditionalBufferLabour);
        //        dbhelper.AddParameter("@LossPercentage", companyPrice.LossPercentage);
        //        dbhelper.AddParameter("@MarketedChargePercentage", companyPrice.MarketedChargePercentage);
        //        dbhelper.AddParameter("@FactoryExpensePercentage", companyPrice.FactoryExpensePercentage);
        //        dbhelper.AddParameter("@OtherPercentage", companyPrice.OtherPercentage);
        //        dbhelper.AddParameter("@ProfitPercentage", companyPrice.ProfitPercentage);
        //        dbhelper.AddParameter("@FinalPriceUnit", companyPrice.FinalPriceUnit);
        //        dbhelper.AddParameter("@action", companyPrice.action);
        //        dbhelper.AddParameter("@UserId", companyPrice.UserId);
        //        dbhelper.Command.Parameters.Add("@OUTVAL", System.Data.SqlDbType.Int);
        //        dbhelper.Command.Parameters["@OUTVAL"].Direction = System.Data.ParameterDirection.Output;
        //        dbhelper.Command.Parameters.Add("@OUTMESSAGE", System.Data.SqlDbType.VarChar, 500);
        //        dbhelper.Command.Parameters["@OUTMESSAGE"].Direction = System.Data.ParameterDirection.Output;
        //        dbhelper.ExecuteNonQuery();

        //        returnMessage.ReturnValue = Convert.ToInt16(dbhelper.Command.Parameters["@OUTVAL"].Value);
        //        returnMessage.Message = Convert.ToString(dbhelper.Command.Parameters["@OUTMESSAGE"].Value);

        //    }
        //    catch (Exception ex)
        //    {
        //        returnMessage.ReturnValue = -1;
        //        returnMessage.Message = Convert.ToString(ex.Message);
        //    }
        //    return returnMessage;
        //}
    }
}
