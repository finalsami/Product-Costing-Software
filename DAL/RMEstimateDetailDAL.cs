using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RMEstimateDetailDAL
    {
        DBHelper dbhelper = null;
        public RMEstimateDetailDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable GetProductListByRMEstimate(int RMEstimateId, int UserId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_ProductListByRMEstimate");
                dbhelper.AddParameter("@RMEstimateId", RMEstimateId);
                dbhelper.AddParameter("@UserId", UserId);
 
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public DataTable GetCountProductwithCompanyByRMEstimate(int RMEstimateId, int UserId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_CountProductwithCompanyByRMEstimate");
                dbhelper.AddParameter("@RMEstimateId", RMEstimateId);
                dbhelper.AddParameter("@UserId", UserId);

                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public DataSet GetRMEstimateDetailReport(int FkBulkProductId, int UserId)
        {

            DataSet objds = new DataSet();
            try
            {
                dbhelper.SpCommand("SP_Get_RMEstimateDetailReport");
                dbhelper.AddParameter("@FkBulkProductId", FkBulkProductId);
                dbhelper.AddParameter("@UserId", UserId);
                objds = dbhelper.GetDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objds;


        }
        public ReturnMessage InsertUpdateRMEstimateDetail(RMEstimateDetailBAL RMED)

        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_RMEstimateDetail");
                dbhelper.AddParameter("@RMEstimateDetailId", RMED.RMEstimateDetailId);
                dbhelper.AddParameter("@FKRMEstimateId", RMED.FKRMEstimateId);
                dbhelper.AddParameter("@FkRMPriceId", RMED.FkRMPriceId);
                dbhelper.AddParameter("@RMNewPrice", RMED.RMNewPrice);
                dbhelper.AddParameter("@UserId", RMED.UserId);
                dbhelper.AddParameter("@action", RMED.action);
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
