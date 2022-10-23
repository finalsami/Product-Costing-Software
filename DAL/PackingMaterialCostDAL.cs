using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PackingMaterialCostDAL
    {
        DBHelper dbhelper = null;
        public PackingMaterialCostDAL()
        {
            dbhelper = new DBHelper();
        }
       
        public ReturnMessage InsertUpdatePackingMaterialMaster(PackingMaterialCostBAL PMMC)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_PackingMaterialCost");
                dbhelper.AddParameter("@PackingMaterialCostingId", PMMC.PackingMaterialCostingId);
                dbhelper.AddParameter("@FkPackingMaterialId", PMMC.FkPackingMaterialId);
                dbhelper.AddParameter("@FkPMRMCategoryId", PMMC.FkPMRMCategoryId);
                dbhelper.AddParameter("@FkPMRMId", PMMC.FkPMRMId);               
                dbhelper.AddParameter("@UserId", PMMC.UserId);
                dbhelper.AddParameter("@action", PMMC.action);
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

       
        public DataTable ProductPackingMaterialCostingList(int UserId, int BulkProductId, int PMRMIdPackingMaterialId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_ProductPackingMaterialMasterDetail");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@BulkProductId", BulkProductId);
                dbhelper.AddParameter("@PackingMaterialId", PMRMIdPackingMaterialId);
                
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;

        }

        public DataTable PackingMaterialDetail(int PackingMaterialId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PackingMaterialDetail");
                dbhelper.AddParameter("@PackingMaterialId", PackingMaterialId);

                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;

        }
    }
}
