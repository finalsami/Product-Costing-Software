using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OtherCompanyPriceListDAL
    {
        DBHelper dbhelper = null;
        public OtherCompanyPriceListDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable GetOtherCompanyPriceList(int CompanyId,int UserId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_OtherCompanyPriceList");
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
        public ReturnMessage UpdateOtheCompanyPricelist(OtherCompanyPriceListBAL companyPrice)
        {

            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_OtherCompanyPriceList");
                dbhelper.AddParameter("@OtherComapnyPriceListId", companyPrice.OtherComapnyPriceListId);
                dbhelper.AddParameter("@Interest", companyPrice.Interest);
                dbhelper.AddParameter("@AdditionalBufferPM", companyPrice.AdditionalBufferPM);
                dbhelper.AddParameter("@AdditionalBufferLabour", companyPrice.AdditionalBufferLabour);
                dbhelper.AddParameter("@LossPercentage", companyPrice.LossPercentage);
                dbhelper.AddParameter("@MarketedChargePercentage", companyPrice.MarketedChargePercentage);
                dbhelper.AddParameter("@FactoryExpensePercentage", companyPrice.FactoryExpensePercentage);
                dbhelper.AddParameter("@OtherPercentage", companyPrice.OtherPercentage);
                dbhelper.AddParameter("@ProfitPercentage", companyPrice.ProfitPercentage);
                dbhelper.AddParameter("@FinalPriceUnit", companyPrice.FinalPriceUnit);
                dbhelper.AddParameter("@action", companyPrice.action);
                dbhelper.AddParameter("@UserId", companyPrice.UserId);
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
    }
}
