using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PMRMMasterDAL
    {
        DBHelper dbhelper = null;
        public PMRMMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_PMRMMaster(int UserId, int PMRMId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PMRMMaster");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@PMRMId", PMRMId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdatePMRMMaster(PMRMMasterBAL PMRM)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_PMRMMaster");
                dbhelper.AddParameter("@PMRMId", PMRM.PMRMId);
                dbhelper.AddParameter("@PMRMName", PMRM.PMRMName);
                dbhelper.AddParameter("@FkPMRMCategoryId", PMRM.FkPMRMCategoryId);
                dbhelper.AddParameter("@FkUnitMeasurementId", PMRM.FkUnitMeasurementId);
                dbhelper.AddParameter("@Unit", PMRM.Unit);
                dbhelper.AddParameter("@TotalWeight", PMRM.TotalWeight);

                dbhelper.AddParameter("@action", PMRM.action);

                dbhelper.AddParameter("@UserId", PMRM.UserId);
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
