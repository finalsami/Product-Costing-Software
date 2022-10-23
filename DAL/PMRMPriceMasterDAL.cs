using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PMRMPriceMasterDAL
    {
        DBHelper dbhelper = null;
        public PMRMPriceMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_PMRMPriceMaster(int UserId, int PMRMPriceId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PMRMPriceMaster");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@PMRMPriceId", PMRMPriceId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage SP_InsertUpdate_PMRMPriceMaster(PMRMPriceMasterBAL PMRM)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_PMRMPriceMaster");
                dbhelper.AddParameter("@PMRMPriceId", PMRM.PMRMPriceId);
                dbhelper.AddParameter("@FkPMRMCategoryId", PMRM.FkPMRMCategoryId);
                dbhelper.AddParameter("@FkPMRMId", PMRM.FkPMRMId);
                dbhelper.AddParameter("@UnitMeasurementId", PMRM.UnitMeasurementId);
                dbhelper.AddParameter("@Price", PMRM.Price);
                dbhelper.AddParameter("@TrasportationCost", PMRM.TrasportationCost);
                dbhelper.AddParameter("@Loss", PMRM.Loss);

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
