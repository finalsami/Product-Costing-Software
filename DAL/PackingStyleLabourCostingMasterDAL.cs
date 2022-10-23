using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PackingStyleLabourCostingMasterDAL
    {
        DBHelper dbhelper = null;
        public PackingStyleLabourCostingMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable GetPackingStyleLabourCostingMaster(int UserId, int PackingStyleLabourCostingId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PackingStyleLabourCostingMaster");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@PackingStyleLabourCostingId", PackingStyleLabourCostingId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdatePackingStyleLabourCostingMaster(PackingStyleLabourCostingMasterBAL PSLC)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_PackingStyleLabourCostingMaster");
                dbhelper.AddParameter("@PackingStyleLabourCostingId", PSLC.PackingStyleLabourCostingId);
                dbhelper.AddParameter("@FkPackingSizeCategoryId", PSLC.FkPackingSizeCategoryId);
                dbhelper.AddParameter("@FkPackingStyleId", PSLC.FkPackingStyleId);
                dbhelper.AddParameter("@PackingSize", PSLC.PackingSize);
                dbhelper.AddParameter("@FkUnitMeasurementId", PSLC.FkUnitMeasurementId);
                dbhelper.AddParameter("@TaskBulkCharge", PSLC.TaskBulkCharge);
                dbhelper.AddParameter("@TaskPouchFilling", PSLC.TaskPouchFilling);
                dbhelper.AddParameter("@TaskBottleKeeping", PSLC.TaskBottleKeeping);
                dbhelper.AddParameter("@TaskLiftingWeight", PSLC.TaskLiftingWeight);
                dbhelper.AddParameter("@TaskBlacklinnerPouch", PSLC.TaskBlacklinnerPouch);
                dbhelper.AddParameter("@TaskInnerPlug", PSLC.TaskInnerPlug);
                dbhelper.AddParameter("@TaskMesuringCap", PSLC.TaskMesuringCap);
                dbhelper.AddParameter("@TaskCaping", PSLC.TaskCaping);
                dbhelper.AddParameter("@TaskTearDownSeal", PSLC.TaskTearDownSeal);
                dbhelper.AddParameter("@TaskInduction", PSLC.TaskInduction);
                dbhelper.AddParameter("@TaskPouchSealing", PSLC.TaskPouchSealing);
                dbhelper.AddParameter("@TaskBottlePouchCleaning", PSLC.TaskBottlePouchCleaning);
                dbhelper.AddParameter("@TaskLabeling", PSLC.TaskLabeling);
                dbhelper.AddParameter("@TaskSleeve", PSLC.TaskSleeve);
                dbhelper.AddParameter("@TaskInnerbox", PSLC.TaskInnerbox);
                dbhelper.AddParameter("@TaskSSTinDrumBucketBag", PSLC.TaskSSTinDrumBucketBag);
                dbhelper.AddParameter("@TaskInnerBoxCelloTape", PSLC.TaskInnerBoxCelloTape);
                dbhelper.AddParameter("@TaskKitchenTray", PSLC.TaskKitchenTray);
                dbhelper.AddParameter("@TaskOuterLabelBOPPBoxFilling", PSLC.TaskOuterLabelBOPPBoxFilling);
                dbhelper.AddParameter("@TaskStappingWeight", PSLC.TaskStappingWeight);
                dbhelper.AddParameter("@TaskAdditionalOther", PSLC.TaskAdditionalOther);
                dbhelper.AddParameter("@PowerFilling", PSLC.PowerFilling);
                dbhelper.AddParameter("@PowerCapping", PSLC.PowerCapping);
                dbhelper.AddParameter("@PowerInduction", PSLC.PowerInduction);
                dbhelper.AddParameter("@PowerLableling", PSLC.PowerLableling);
                dbhelper.AddParameter("@PowerShrinking", PSLC.PowerShrinking);
                dbhelper.AddParameter("@PowerBOPP", PSLC.PowerBOPP);
                dbhelper.AddParameter("@PowerStepping", PSLC.PowerStepping);
                dbhelper.AddParameter("@PowerStealingMC", PSLC.PowerStealingMC);
                dbhelper.AddParameter("@PowerDetail9", PSLC.PowerDetail9);
                dbhelper.AddParameter("@PowerDetail10", PSLC.PowerDetail10);
                dbhelper.AddParameter("@PowerUnitPerHour", PSLC.PowerUnitPerHour);
                dbhelper.AddParameter("@PowerOther", PSLC.PowerOther);
                dbhelper.AddParameter("@action", PSLC.action);
                dbhelper.AddParameter("@UserId", PSLC.UserId);
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
