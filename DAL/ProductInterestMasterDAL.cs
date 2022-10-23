using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductInterestMasterDAL
    {
        DBHelper dbhelper = null;
        public ProductInterestMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable GetProductInterestMaster(int BulkProductId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_ProductInterestMaster");
                
                dbhelper.AddParameter("@BulkProductId", BulkProductId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdateBulkProductInterestMaster(ProductInterestMasterBAL BPIM)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_BulkProductInterestMaster");
                dbhelper.AddParameter("@BulkProductInterestId", BPIM.BulkProductInterestId);
                dbhelper.AddParameter("@FkBulkProductId", BPIM.FkBulkProductId);
                dbhelper.AddParameter("@Interest", BPIM.Interest);
                dbhelper.AddParameter("@action", BPIM.action);

                dbhelper.AddParameter("@UserId", BPIM.UserId);
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
