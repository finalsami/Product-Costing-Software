using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RMMasterDAL
    {
        DBHelper dbhelper = null;
        public RMMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable RMMasterList(int UserId,int RMId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_RMMasterAll");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@RMId", RMId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdateRMMaster(RMMasterBAL RM)
        {   
            ReturnMessage returnMessage = new ReturnMessage();
            
            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_RMMaster");
                dbhelper.AddParameter("@RMId", RM.RMId);
                dbhelper.AddParameter("@action", RM.action);
                dbhelper.AddParameter("@RMName", RM.RMName);
                dbhelper.AddParameter("@RMCategoryId", RM.RMCategoryId);
                dbhelper.AddParameter("@Ispurity", RM.IsPurity);
                dbhelper.AddParameter("@UnitMeasurementId", RM.UnitMeasurementId);
                dbhelper.AddParameter("@UserId", RM.UserId);
                dbhelper.Command.Parameters.Add("@OUTVAL", System.Data.SqlDbType.Int);
                dbhelper.Command.Parameters["@OUTVAL"].Direction = System.Data.ParameterDirection.Output;
                dbhelper.Command.Parameters.Add("@OUTMESSAGE", System.Data.SqlDbType.VarChar,500);
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
