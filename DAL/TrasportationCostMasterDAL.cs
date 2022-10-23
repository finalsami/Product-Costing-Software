using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TrasportationCostMasterDAL
    {

        DBHelper dbhelper = null;
        public TrasportationCostMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_TrasportationCostMaster(int UserId, int TrasportationCostId, int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_TrasportationCostMaster");
                dbhelper.AddParameter("@TrasportationCostId", TrasportationCostId);
                dbhelper.AddParameter("@CompanyId", CompanyId);
                dbhelper.AddParameter("@UserId", UserId);

                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdate_TransportationCostMaster(TrasportationCostMasterBAL TCM)
        {

            ReturnMessage returnMessage = new ReturnMessage();
            try
            {

                dbhelper.SpCommand("SP_InsertUpdate_TransportationCostMaster");
                dbhelper.AddParameter("@TransportationCostId", TCM.TransportationCostId);
                dbhelper.AddParameter("@FkStateId", TCM.FkStateId);
                dbhelper.AddParameter("@FkCompanyId", TCM.FkCompanyId);
                dbhelper.AddParameter("@TruckLoadCharges", TCM.TruckLoadCharges);
                dbhelper.AddParameter("@NoCarton", TCM.NoCarton);
                dbhelper.AddParameter("@action", TCM.action);


                dbhelper.AddParameter("@UserId", TCM.UserId);
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
