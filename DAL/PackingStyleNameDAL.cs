using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PackingStyleNameDAL
    {
        DBHelper dbhelper = null;
        public PackingStyleNameDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable GetPackingStyleList(int UserId, int PackingStyleId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PackingStyle");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@PackingStyleId", PackingStyleId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdatePackingStyle(PackingStyleNameBAL PS)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_PackingStyle");
                dbhelper.AddParameter("@PackingStyleId", PS.PackingStyleId);
                dbhelper.AddParameter("@PackingStyleName", PS.PackingStyle);
                dbhelper.AddParameter("@action", PS.action);

                dbhelper.AddParameter("@UserId", PS.UserId);
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
