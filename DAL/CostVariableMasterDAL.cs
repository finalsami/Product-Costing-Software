using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CostVariableMasterDAL
    {
        DBHelper dbhelper = null;
        public CostVariableMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable GetOtherVariable(int UserId, int OtherVariableId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_OtherVariable");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@OtherVariableId", OtherVariableId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdateOtherVariable(CostVariableMasterBAL CV)
        {
            ReturnMessage returnMessage = new ReturnMessage();
            

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_OtherVariable");
                dbhelper.AddParameter("@OtherVariableId", CV.OtherVariableId);
                dbhelper.AddParameter("@ShiftHours", CV.ShiftHours);
                dbhelper.AddParameter("@BreakHours", CV.BreakHours);
                dbhelper.AddParameter("@NetShiftHours", CV.NetShiftHours);
                dbhelper.AddParameter("@PowerUnitPrice", CV.PowerUnitPrice);
                dbhelper.AddParameter("@LabourCharge", CV.LabourCharge);
                dbhelper.AddParameter("@SuperVisorCharge", CV.SuperVisorCharge);
                dbhelper.AddParameter("@UnloadingExpenseLtr", CV.UnloadingExpenseLtr);
                dbhelper.AddParameter("@UnloadingExpenseKg", CV.UnloadingExpenseKg);
                dbhelper.AddParameter("@UnloadingExpenseUnit", CV.UnloadingExpenseUnit);
                dbhelper.AddParameter("@LoadingExpenceLtr", CV.LoadingExpenceLtr);
                dbhelper.AddParameter("@LoadingExpenceKg", CV.LoadingExpenceKg);
                dbhelper.AddParameter("@LoadingExpenceUnit", CV.LoadingExpenceUnit);
                dbhelper.AddParameter("@MachinaryExpenceLtr", CV.MachinaryExpenceLtr);
                dbhelper.AddParameter("@MachinaryExpenceKg", CV.MachinaryExpenceKg);
                dbhelper.AddParameter("@MachinaryExpenceUnit", CV.MachinaryExpenceUnit);
                dbhelper.AddParameter("@AdminExpenceLtr", CV.AdminExpenceLtr);
                dbhelper.AddParameter("@AdminExpenceKg", CV.AdminExpenceKg);
                dbhelper.AddParameter("@AdminExpenceUnit", CV.AdminExpenceUnit);
                dbhelper.AddParameter("@ExtraExpenceLtr", CV.ExtraExpenceLtr);
                dbhelper.AddParameter("@ExtraExpenceKg", CV.ExtraExpenceKg);
                dbhelper.AddParameter("@ExtraExpenceUnit", CV.ExtraExpenceUnit);
               
                dbhelper.AddParameter("@action", CV.action);
                dbhelper.AddParameter("@UserId", CV.UserId);
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
