using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RMEstimateDAL
    {
        DBHelper dbhelper = null;
        public RMEstimateDAL()
        {
            dbhelper = new DBHelper();
        }

        public DataTable GetRMEstimate(int RMEstimateId, int UserId,int FkCompanyId,int type)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_RMEstimate");
                dbhelper.AddParameter("@RMEstimateId", RMEstimateId);
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@FkCompanyId", FkCompanyId);
                dbhelper.AddParameter("@type", type);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }

        public ReturnMessage InsertUpdateRMEstimate(RMEstimateBAL RME)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_RMEstimate");
                dbhelper.AddParameter("@RMEstimateId", RME.RMEstimateId);               
                dbhelper.AddParameter("@EstimateDate", RME.EstimateDate);
                dbhelper.AddParameter("@EstimateName", RME.EstimateName);
                dbhelper.AddParameter("@FkCompanyId", RME.FkCompanyId);                
                dbhelper.AddParameter("@UserId", RME.UserId);
                dbhelper.AddParameter("@action", RME.action);
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
