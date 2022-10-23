using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PackingCategoryDAL
    {
        DBHelper dbhelper = null;
        public PackingCategoryDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable PackingCategoryList(int UserId, int PackingCategoryId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PackingCategoryAll");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@PackingCategoryId", PackingCategoryId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdatePackingCategory(PackingCategoryBAL PMRM)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_PackingCategory");
                dbhelper.AddParameter("@PackingCategoryId", PMRM.PackingCategoryId);
                dbhelper.AddParameter("@PackingCategoryName", PMRM.PackingCategoryName);

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
