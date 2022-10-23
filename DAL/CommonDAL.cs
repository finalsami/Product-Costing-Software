using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommonDAL
    {
        DBHelper dbhelper = null;
        public CommonDAL()
        {


            dbhelper = new DBHelper();
        }
        public DataTable DropdownList(string type,string param1,string param2)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_GetAllDropdownList");
                dbhelper.AddParameter("@type", type);
                dbhelper.AddParameter("@param1", param1);
                dbhelper.AddParameter("@param2", param2);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
         public ReturnMessage CheckExist(string type, string param1, string param2,string param3)
        {

            ReturnMessage returnMessage = new ReturnMessage();
            try
            {
                dbhelper.SpCommand("SP_CheckExist");
                dbhelper.AddParameter("@type", type);
                dbhelper.AddParameter("@param", param1);
                dbhelper.AddParameter("@param1", param2);
                dbhelper.AddParameter("@param2", param3);
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

        public ReturnMessage CheckExistOthers(string type, string param1, string param2, string param3, string param4, string param5)
        {

            ReturnMessage returnMessage = new ReturnMessage();
            try
            {
                dbhelper.SpCommand("SP_CheckExistOther");
                dbhelper.AddParameter("@type", type);               
                dbhelper.AddParameter("@param1", param1);
                dbhelper.AddParameter("@param2", param2);
                dbhelper.AddParameter("@param3", param3);
                dbhelper.AddParameter("@param4", param4);
                dbhelper.AddParameter("@param5", param5);
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
