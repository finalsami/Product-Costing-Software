using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public  class TradeDAL
    {
        DBHelper dbhelper = null;
        public TradeDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable TradeList(int UserId, int TradeId, int FkcompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_TradeAll");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@TradeId", TradeId);
                dbhelper.AddParameter("@CompanyId", FkcompanyId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdateTrade(TradeBAL Trade)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_Trade");
                dbhelper.AddParameter("@TradeId", Trade.TradeId);
                dbhelper.AddParameter("@TradeName", Trade.TradeName);
                dbhelper.AddParameter("@FkcompanyId", Trade.FkcompanyId);
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
