using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BulkRecipeBOMDAL
    {
        DBHelper dbhelper = null;
        public BulkRecipeBOMDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_BOMMaster(int UserId, int BOMId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_BOMMaster");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@BOMId", BOMId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public DataSet Get_BOMMasterDetail(int UserId, int BOMId)
        {

            DataSet objdt = new DataSet();
            try
            {
                dbhelper.SpCommand("SP_Get_BOMMasterDetail");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@BOMId", BOMId);
                objdt = dbhelper.GetDataSet();

                DataSet ds = new DataSet();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }

        public ReturnMessage InsertUpdateBOMMaster(BulkRecipeBOMBAL BOM)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_BOMMaster");
                dbhelper.AddParameter("@BOMId", BOM.BOMId);
                dbhelper.AddParameter("@action", BOM.action);
                dbhelper.AddParameter("@MainCategoryId", BOM.FkMainCategoryId);
                dbhelper.AddParameter("@BulkProductId", BOM.FkBulkProductId);
                dbhelper.AddParameter("@UnitMeasurementId", BOM.UnitMeasurementId);
                dbhelper.AddParameter("@FormulationId", BOM.FormulationId);
                dbhelper.AddParameter("@Spgr", BOM.Spgr);
                dbhelper.AddParameter("@FormulationLostPercentage", BOM.FormulationLostPercentage);
                dbhelper.AddParameter("@BatchSize", BOM.BatchSize);
                dbhelper.AddParameter("@UserId", BOM.UserId);
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

        public ReturnMessage UpdateBOMSPGR(BulkRecipSPGR spgr)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_Update_BOMSPGR");
                dbhelper.AddParameter("@BOMId", spgr.BOMId);
                dbhelper.AddParameter("@action", spgr.action);               
                dbhelper.AddParameter("@FormulationId", spgr.FormulationId);
                dbhelper.AddParameter("@Spgr", spgr.Spgr);
                dbhelper.AddParameter("@Loss", spgr.FormulationLostPercentage);
                dbhelper.AddParameter("@UserId", spgr.UserId);
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

        public ReturnMessage InsertUpdateBOMMasterDetail(BulkRecipeBOMDetail BOM)
        {
            ReturnMessage returnMessage = new ReturnMessage();
            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_BOMMasterDetail");
                dbhelper.AddParameter("@IngredientId", BOM.IngredientId);
                dbhelper.AddParameter("@FkBOMId", BOM.FkBOMId);
                dbhelper.AddParameter("@FkRMId", BOM.FkRMId);
                dbhelper.AddParameter("@FkBulkProductId", BOM.FkBulkProductId);
                dbhelper.AddParameter("@IngredientName", BOM.IngredientName);
                dbhelper.AddParameter("@QuantityLtrKg", BOM.QuantityLtrKg);
                dbhelper.AddParameter("@Formulation", BOM.Formulation);
                dbhelper.AddParameter("@FkMainCategoryId", BOM.FkMainCategoryId);
                dbhelper.AddParameter("@Solvant", BOM.Solvant);
                dbhelper.AddParameter("@FkRMPriceId", BOM.FkRMPriceId);

                dbhelper.AddParameter("@action", BOM.action);      
                dbhelper.AddParameter("@UserId", BOM.UserId);
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
