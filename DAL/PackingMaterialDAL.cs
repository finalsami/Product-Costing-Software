using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PackingMaterialDAL
    {
        DBHelper dbhelper = null;
        public PackingMaterialDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable ProductPackingMaterialMasterList(int UserId, int BulkProductId, int type)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_ProductPackingMaterialMaster");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@BulkProductId", BulkProductId);
                dbhelper.AddParameter("@type", type);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;

        }
       
        public ReturnMessage InsertUpdatePackingMaterialMaster(PackingMaterialBAL PMM)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_PackingMaterialMaster");
                dbhelper.AddParameter("@PackingMaterialId", PMM.PackingMaterialId);
                dbhelper.AddParameter("@FkPackingCategoryId", PMM.FkPackingCategoryId);
                dbhelper.AddParameter("@FkBulkProductId", PMM.FkBulkProductId);
                dbhelper.AddParameter("@PackingName", PMM.PackingName);
                dbhelper.AddParameter("@PackingSize", PMM.PackingSize);
                dbhelper.AddParameter("@PackingMeasurementId", PMM.PackingMeasurementId);
                dbhelper.AddParameter("@ShipperSize", PMM.ShipperSize);
                dbhelper.AddParameter("@UnitMeasurementId", PMM.UnitMeasurementId);
                dbhelper.AddParameter("@FkPMRMCategoryId", PMM.FkPMRMCategoryId);
                dbhelper.AddParameter("@IsMasterPacking", PMM.IsMasterPacking);
                dbhelper.AddParameter("@InnerPackingCategoryId", PMM.InnerPackingCategoryId);
                dbhelper.AddParameter("@InnerSize", PMM.InnerSize);
                dbhelper.AddParameter("@InnerPackingMeasurementId", PMM.InnerPackingMeasurementId);
                dbhelper.AddParameter("@UserId", PMM.UserId);
                dbhelper.AddParameter("@action", PMM.action);
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
