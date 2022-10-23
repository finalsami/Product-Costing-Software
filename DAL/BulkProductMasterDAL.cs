using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BulkProductMasterDAL
    {
        DBHelper dbhelper = null;
        public BulkProductMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_BulkProductMasterAll(int UserId, int BPMId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_BulkProductMasterAll");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@BulkProductId", BPMId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdateBulkProductMaster(BulkProductMasterBAL BPM)
        {
            ReturnMessage returnMessage = new ReturnMessage();
   
            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_BulkProductMaster");
                dbhelper.AddParameter("@BulkProductId",BPM.BulkProductId);
                dbhelper.AddParameter("@action", BPM.action);
                dbhelper.AddParameter("@MainCategoryId", BPM.MainCategoryId);
                dbhelper.AddParameter("@GstId", BPM.GstId);
                dbhelper.AddParameter("@BulkProductName", BPM.BulkProductName);
                dbhelper.AddParameter("@SourceId",BPM.FkSourceId);
                dbhelper.AddParameter("@UnitMeasurementId", BPM.UnitMeasurementId);
                dbhelper.AddParameter("@UserId", BPM.UserId);
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
