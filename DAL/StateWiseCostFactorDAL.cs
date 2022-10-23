using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StateWiseCostFactorDAL
    {
        DBHelper dbhelper = null;
        public StateWiseCostFactorDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_StateWiseCostFactor(int UserId, int StateWiseCostFactorId, int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_StateWiseCostFactor");
                dbhelper.AddParameter("@StateWiseCostFactorId", StateWiseCostFactorId);
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
        public ReturnMessage InsertUpdate_StateWiseCostFactor(StateWiseCostFactorBAL SWCF)
        {

            ReturnMessage returnMessage = new ReturnMessage();
            try
            {

                dbhelper.SpCommand("SP_InsertUpdate_StateWiseCostFactor");
                dbhelper.AddParameter("@StateWiseCostFactorId", SWCF.StateWiseCostFactorId);
                dbhelper.AddParameter("@FkStateId", SWCF.FkStateId);
                dbhelper.AddParameter("@FkCompanyId", SWCF.FkCompanyId);
                dbhelper.AddParameter("@FkPriceTypeId", SWCF.FkPriceTypeId);
                dbhelper.AddParameter("@FkProductCategoryId", SWCF.FkProductCategoryId);
                dbhelper.AddParameter("@StaffExpense", SWCF.StaffExpense);
                dbhelper.AddParameter("@DepoExpence", SWCF.DepoExpence);
                dbhelper.AddParameter("@Incentive", SWCF.Incentive);
                dbhelper.AddParameter("@Interest", SWCF.Interest);
                dbhelper.AddParameter("@Marketing", SWCF.Marketing);
                dbhelper.AddParameter("@Other", SWCF.Other);
                dbhelper.AddParameter("@action", SWCF.action);


                dbhelper.AddParameter("@UserId", SWCF.UserId);
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

        public DataTable Get_ReportTransportationFactor(int UserId, int TransportationCostId, int CompanyId, int StateId, int IsMasterPack)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_ReportTransportationFactor");
                dbhelper.AddParameter("@TransportationCostId", TransportationCostId);
                dbhelper.AddParameter("@FkCompanyId", CompanyId);
                dbhelper.AddParameter("@FkStateId", StateId);
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@IsMasterPack", IsMasterPack);


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
