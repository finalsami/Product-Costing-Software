using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public  class MainCategoryDAL
    {
        DBHelper dbhelper = null;
        public MainCategoryDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable MainCategoryList(int UserId, int MainCategoryId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_MainCategoryAll");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@MainCategoryId", MainCategoryId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdateMainCategory(MainCategoryBAL MC)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_MainCategory");
                dbhelper.AddParameter("@MainCategoryId", MC.MainCategoryId);
                dbhelper.AddParameter("@MainCategoryName", MC.MainCategoryName);
                dbhelper.AddParameter("@action", MC.action);

                dbhelper.AddParameter("@UserId", MC.UserId);
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
