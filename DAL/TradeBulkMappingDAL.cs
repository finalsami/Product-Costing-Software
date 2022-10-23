using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TradeBulkMappingDAL
    {
        DBHelper dbhelper = null;
        public TradeBulkMappingDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable BulkTradeMapping(int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_BulkTradeMapping");
                dbhelper.AddParameter("@FkCompanyId", CompanyId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertBulkTradeMapping(TradeBulkMappingBAL Trade)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_BulkTradeMapping");
                dbhelper.AddParameter("@TradeBulkMappingId", Trade.TradeBulkMappingId);
                dbhelper.AddParameter("@FkCompanyId", Trade.FkCompanyId);
                dbhelper.AddParameter("@FkTradeId", Trade.FkTradeId);
                dbhelper.AddParameter("@FkBulkProductId", Trade.FkBulkProductId);
                dbhelper.AddParameter("@action", Trade.action);
                dbhelper.AddParameter("@UserId", Trade.UserId);
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
