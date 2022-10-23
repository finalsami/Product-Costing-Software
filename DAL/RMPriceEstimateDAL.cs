using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RMPriceEstimateDAL
    {
        DBHelper dbhelper = null;
        public RMPriceEstimateDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_RMPriceEstimateMaster(int RMPriceEstimateId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_RMPriceEstimateMaster");
                dbhelper.AddParameter("@RMPriceEstimateId", RMPriceEstimateId);
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

        public ReturnMessage InsertUpdateRMPriceEstimateMaster(RMPriceEstimateBAL RMPM)
        {
            ReturnMessage returnMessage = new ReturnMessage();
            
            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_RMPriceEstimate");
                dbhelper.AddParameter("@FkRMPriceId", RMPM.FkRMPriceId);
                dbhelper.AddParameter("@RMPriceEstimateId", RMPM.RMPriceEstimateId);
                
                dbhelper.AddParameter("@action", RMPM.action);
                dbhelper.AddParameter("@PurchaseDate", RMPM.PurchaseDate);
                dbhelper.AddParameter("@EstimatePrice", RMPM.EstimatePrice);
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
