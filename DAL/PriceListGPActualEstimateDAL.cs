using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PriceListGPActualEstimateDAL
    {
        DBHelper dbhelper = null;
        public PriceListGPActualEstimateDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable GetPriceListGPActualEstimateByBPMId(int UserId, int FkBulkProductId, int CompanyId,int PriceListGPActualEstimateId)
        {

            DataTable objdt = new DataTable();
            try
            {
                
                dbhelper.SpCommand("SP_Get_PriceListGPActualEstimateByBPMId");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@FkBulkProductId", FkBulkProductId);
                dbhelper.AddParameter("@CompanyId", CompanyId);
                dbhelper.AddParameter("@PriceListGPActualEstimateId", PriceListGPActualEstimateId);

                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public DataTable GetPriceListGPActualByBPMId(int UserId, int FkBulkProductId, int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PriceListGPActualByBPMId");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@FkBulkProductId", FkBulkProductId);
                dbhelper.AddParameter("@CompanyId", CompanyId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }

        public DataTable GetPriceListGPActual(int UserId, int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PriceListGPActual");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@FkCompanyId", CompanyId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }

        public DataTable GetPriceListReportGP( int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PriceListReportGP");
                dbhelper.AddParameter("@CompanyId", CompanyId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }

        public DataTable GetPriceListReportGPData(string PriceList,string StateId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PriceListReportGPData");
                dbhelper.AddParameter("@PriceList", PriceList);
                dbhelper.AddParameter("@StateId", StateId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public DataTable GetPriceListGPActualEstimateByStateWiseReport(int FkStateId, int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                //dbhelper.SpCommand("SP_Get_PriceListGPActualEstimateByStateWiseReport");
                dbhelper.SpCommand("SP_Get_PriceListGPActualEstimateByStateWiseReportNew");
                dbhelper.AddParameter("@FkStateId", FkStateId);
                dbhelper.AddParameter("@CompanyId", CompanyId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }

        public DataTable GetPriceListGPActualEstimate(int UserId, int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PriceListGPActualEstimate");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@FkCompanyId", 1);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }

        public ReturnMessage InsertUpdatePriceListGPActualEstimate(PriceListGPActualEstimateBAL PL)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_PriceListGPActualEstimate");
                dbhelper.AddParameter("@FkBulkProductId", PL.FkBulkProductId);
                dbhelper.AddParameter("@PriceListGPActualEstimateId", PL.PriceListGPActualEstimateId);
                dbhelper.AddParameter("@EstimateId", PL.EstimateId);
                dbhelper.AddParameter("@FkPackingMaterialId", PL.FkPackingMaterialId);
                dbhelper.AddParameter("@FkCompanyId", PL.FkCompanyId);
                dbhelper.AddParameter("@FkPriceTypeId", PL.FkPriceTypeId);
                dbhelper.AddParameter("@FkStateId", PL.FkStateId);
                dbhelper.AddParameter("@FkTradeId", PL.FkTradeId);
                dbhelper.AddParameter("@TOD", PL.TOD);
                dbhelper.AddParameter("@PD", PL.PD);
                dbhelper.AddParameter("@QD", PL.QD);
                dbhelper.AddParameter("@ProfitPer", PL.ProfitPer);
                dbhelper.AddParameter("@FinalPrice", PL.FinalPrice);
                dbhelper.AddParameter("@AdditionalPD", PL.AdditionalPD);
                dbhelper.AddParameter("@action", PL.action);
                dbhelper.AddParameter("@Status", PL.Status);

                dbhelper.AddParameter("@UserId", PL.UserId);
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
        public ReturnMessage InsertUpdate_CreatePriceList(string FkBulkProductId, PriceListGPActualEstimateBAL PL)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_CreatePriceList");
                dbhelper.AddParameter("@FkBulkProductId", FkBulkProductId);          
                dbhelper.AddParameter("@FkCompanyId", PL.FkCompanyId);     
                dbhelper.AddParameter("@PriceListName", PL.PriceListName);
                dbhelper.AddParameter("@action", PL.action);            
                dbhelper.AddParameter("@UserId", PL.UserId);
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
