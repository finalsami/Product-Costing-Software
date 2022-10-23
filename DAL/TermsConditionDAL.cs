using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TermsConditionDAL
    {
        DBHelper dbhelper = null;
        public TermsConditionDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable TermsConditionList(int UserId, int TermsconditionId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_TermsConditionList");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@TermsconditionId", TermsconditionId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public DataSet TermsConditionListByBPMId(string FkBulkProductId,string PackingSize,string PackingType,string TermsCondId)
        {

            DataSet objds = new DataSet();
            try
            {
      

                dbhelper.SpCommand("SP_Get_TermsConditionListByBPMId");
                dbhelper.AddParameter("@UserId", 1);
                dbhelper.AddParameter("@FkBulkProductId", FkBulkProductId);
                dbhelper.AddParameter("@PackingType", PackingType);
                dbhelper.AddParameter("@TermsCondId", TermsCondId);
                objds = dbhelper.GetDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objds;


        }
        public DataSet BulkCostSelectedReportByIdBPM(string FkBulkProductId, string SharedId)
        {

            DataSet objds = new DataSet();
            try
            {


                dbhelper.SpCommand("SP_Get_BulkCostSelectedReportByIdBPM");
                dbhelper.AddParameter("@UserId", 1);
                dbhelper.AddParameter("@FkBulkProductId", FkBulkProductId);
                dbhelper.AddParameter("@ShareNameId", SharedId);
                objds = dbhelper.GetDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objds;


        }
        public DataSet ShowSharedAllReportBPMId(string FkBulkProductId)
        {

            DataSet objds = new DataSet();
            try
            {


                dbhelper.SpCommand("SP_Get_ShowSharedAllReportBPMId");
                dbhelper.AddParameter("@UserId", 1);
                dbhelper.AddParameter("@FkBulkProductId", FkBulkProductId);

                objds = dbhelper.GetDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objds;


        }
        public ReturnMessage InsertUpdateTermsCondition(TermsConditionBAL TC )
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_TermsCondition");
                dbhelper.AddParameter("@TermsconditionId", TC.TermsconditionId);
                dbhelper.AddParameter("@TermsCondition", TC.TermsCondition);
                dbhelper.AddParameter("@FkCompanyId", TC.FkCompanyId);
                
                dbhelper.AddParameter("@action", TC.action);

                dbhelper.AddParameter("@UserId", TC.UserId);
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
        public ReturnMessage InsertUpdateTermsConditionBulkCost(TermsConditionBAL TC, int BPMId, string TermsconditionId)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_TermsConditionBulkCost");
                dbhelper.AddParameter("@TermsBulkCostId", TermsconditionId);
                dbhelper.AddParameter("@FkBulkProductId", BPMId);
                dbhelper.AddParameter("@FkCompanyId", TC.FkCompanyId);

                dbhelper.AddParameter("@action", TC.action);

                dbhelper.AddParameter("@UserId", 1);
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
