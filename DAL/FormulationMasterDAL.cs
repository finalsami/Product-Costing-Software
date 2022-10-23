using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FormulationMasterDAL
    {
        DBHelper dbhelper = null;
        public FormulationMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_FormulationMaster(int UserId, int FormulationId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_FormulationMaster");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@FormulationId", FormulationId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdateFormulationMaster(int UserId, FormulationMasterBAL FM)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_FormulationMaster");
                dbhelper.AddParameter("@FormulationId", FM.FormulationId);
                dbhelper.AddParameter("@action", FM.action);
                dbhelper.AddParameter("@FormulationName", FM.FormulationName);
                dbhelper.AddParameter("@BatchSize", FM.BatchSize);
                dbhelper.AddParameter("@UnitMeasurementId", FM.UnitMeasurementId);
                dbhelper.AddParameter("@Labours", FM.Labours);
                dbhelper.AddParameter("@Supervisors", FM.Supervisors);
                dbhelper.AddParameter("@PowerUnits", FM.PowerUnits);
                dbhelper.AddParameter("@MaintenanceCost", FM.MaintenanceCost);
                dbhelper.AddParameter("@AdditionalBuffer", FM.AdditionalBuffer);
                
                dbhelper.AddParameter("@OtherCost", FM.OtherCost);

                dbhelper.AddParameter("@UserId", UserId);
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

