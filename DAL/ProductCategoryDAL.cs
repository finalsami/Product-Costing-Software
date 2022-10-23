using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductCategoryDAL
    {
        DBHelper dbhelper = null;
        public ProductCategoryDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_ProductCategoryMaster(int UserId, int ProductCategoryId, int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_ProductCategoryMaster");
                dbhelper.AddParameter("@ProductCategoryId", ProductCategoryId);
                dbhelper.AddParameter("@FkCompanyId", CompanyId);
                dbhelper.AddParameter("@UserId", UserId);

                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdate_ProductCategoryMaster(ProductCategoryBAL PC)
        {

            ReturnMessage returnMessage = new ReturnMessage();
            try
            {

                dbhelper.SpCommand("SP_InsertUpdate_ProductCategoryMaster");
                dbhelper.AddParameter("@ProductCategoryId", PC.ProductCategoryId);
                dbhelper.AddParameter("@FkCompanyId", PC.FkCompanyId);
                dbhelper.AddParameter("@ProductCategoryName", PC.ProductCategoryName);
          
                dbhelper.AddParameter("@action", PC.action);


                dbhelper.AddParameter("@UserId", PC.UserId);
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
        public DataTable Get_TrasportationCostFactor(int UserId, int TransportationCostMasterId, int Type, int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_TrasportationCostFactor");
                dbhelper.AddParameter("@TransportationCostMasterId", TransportationCostMasterId);
                dbhelper.AddParameter("@FkCompanyId", CompanyId);
                dbhelper.AddParameter("@Type", Type);
                dbhelper.AddParameter("@UserId", UserId);

                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdate_TransportationCostFactor(TrasportationCostFactorBAL TCF, int Type)
        {

            ReturnMessage returnMessage = new ReturnMessage();
            try
            {

                dbhelper.SpCommand("SP_InsertUpdate_TransportationCostFactor");
                dbhelper.AddParameter("@TransportationCostFactorId", TCF.TransportationCostFactorId);
                dbhelper.AddParameter("@UnloadingCostFactorId", TCF.UnloadingCostFactorId);
                dbhelper.AddParameter("@CartageCostFactorId", TCF.CartageCostFactorId);
                dbhelper.AddParameter("@TransportationCostId", TCF.FkTransportationCostId);

                dbhelper.AddParameter("@Start", TCF.Start);
                dbhelper.AddParameter("@End", TCF.End);
                dbhelper.AddParameter("@FkCompanyId", TCF.FkCompanyId);
                dbhelper.AddParameter("@FkUnitMeasurementId", TCF.FkUnitMeasurementId);
                dbhelper.AddParameter("@Amount", TCF.Amount);
                dbhelper.AddParameter("@action", TCF.action);
                dbhelper.AddParameter("@Type", Type);


                dbhelper.AddParameter("@UserId", TCF.UserId);
                dbhelper.Command.Parameters.Add("@OUTVAL", System.Data.SqlDbType.Int);
                dbhelper.Command.Parameters["@OUTVAL"].Direction = System.Data.ParameterDirection.Output;
                dbhelper.Command.Parameters.Add("@OUTMESSAGE", System.Data.SqlDbType.VarChar, 500);
                dbhelper.Command.Parameters["@OUTMESSAGE"].Direction = System.Data.ParameterDirection.Output;
                returnMessage.ReturnValue = Convert.ToInt16(dbhelper.Command.Parameters["@OUTVAL"].Value);
                returnMessage.Message = Convert.ToString(dbhelper.Command.Parameters["@OUTMESSAGE"].Value);

                dbhelper.ExecuteNonQuery();



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
