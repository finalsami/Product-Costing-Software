using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductwiseLabourCostDAL
    {
        DBHelper dbhelper = null;
        public ProductwiseLabourCostDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_ProductwiseLabourCost(int UserId, int ProductwiseLaborCostId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_ProductwiseLabourCost");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@ProductwiseLaborCostId", ProductwiseLaborCostId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public DataTable GetDataByPackStyleCatPackStyle(string PackSizeCatId, string PackStyleId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_DataByPackStyleCatPackStyle");
                dbhelper.AddParameter("@PackSizeCatId", PackSizeCatId);
                dbhelper.AddParameter("@PackStyleId", PackStyleId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }

        public ReturnMessage InsertUpdateProductwiseLabourCost(ProductwiseLabourCostBAL PWLC)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_ProductwiseLabourCost");
                
                dbhelper.AddParameter("@ProductwiseLaborCostId", PWLC.ProductwiseLaborCostId);
                dbhelper.AddParameter("@FkBulkProductId", PWLC.FkBulkProductId);
                dbhelper.AddParameter("@FkPackingMaterialId", PWLC.FkPackingMaterialId);
                dbhelper.AddParameter("@PackingDescription", PWLC.PackingDescription);
                dbhelper.AddParameter("@FkPackingSizeCategoryId", PWLC.FkPackingSizeCategoryId);
                dbhelper.AddParameter("@FkPackingStyleId", PWLC.FkPackingStyleId);
                dbhelper.AddParameter("@PMRMCategoryId", PWLC.PMRMCategoryId);
                dbhelper.AddParameter("@StorckNosel", PWLC.StorckNosel);
                dbhelper.AddParameter("@NoselsPerFillingLine", PWLC.NoselsPerFillingLine);
                dbhelper.AddParameter("@Supervisiors", PWLC.Supervisiors);
                dbhelper.AddParameter("@AdditionalCostBuffer", PWLC.AdditionalCostBuffer);

                dbhelper.AddParameter("@action", PWLC.action);
                dbhelper.AddParameter("@UserId", PWLC.UserId);
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
