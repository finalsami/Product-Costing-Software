using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RMPriceMasterDAL
    {
        DBHelper dbhelper = null;
        public RMPriceMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_RMPriceMaster(int RMPriceId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_RMPriceMaster");
                dbhelper.AddParameter("@RMPriceId", RMPriceId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public DataTable Get_RMPriceActualRatePerKg(string RMCatId,string RMId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_RMPriceActualRatePerKg");
                dbhelper.AddParameter("@RMId", RMId);
                dbhelper.AddParameter("@RMCatId", RMCatId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }

        public ReturnMessage InsertUpdateRMPriceMaster(RMPriceMasterBAL RMPM)
        {
            ReturnMessage returnMessage = new ReturnMessage();
            
            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_RMPriceMaster");
                dbhelper.AddParameter("@RMPriceId", RMPM.RMPriceId);
                dbhelper.AddParameter("@action", RMPM.action);
                dbhelper.AddParameter("@RMCategoryId", RMPM.FkRMCategoryId);
                dbhelper.AddParameter("@RMId", RMPM.FkRMId);
                dbhelper.AddParameter("@Ispurity", RMPM.IsPurity);
                dbhelper.AddParameter("@PurchaseDate", RMPM.PurchaseDate);
                dbhelper.AddParameter("@RateKgLtr", RMPM.RateKgLtr);
                dbhelper.AddParameter("@Quantity", RMPM.Quantity);
                dbhelper.AddParameter("@PurityPercentage", RMPM.PurityPercentage);
                dbhelper.AddParameter("@TransporationRate", RMPM.TransporationRate);
                dbhelper.AddParameter("@UserId", RMPM.UserId);

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
