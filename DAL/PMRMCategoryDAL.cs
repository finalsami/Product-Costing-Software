using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public  class PMRMCategoryDAL
    {
        DBHelper dbhelper = null;
        public PMRMCategoryDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable PMRMCategoryList(int UserId, int PMRMCategoryId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_PMRMCategoryAll");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@PMRMCategoryId", PMRMCategoryId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdatePMRMCategory(PMRMCategoryBAL PMRM)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_PMRMCategory");
                dbhelper.AddParameter("@PMRMCategoryId", PMRM.PMRMCategoryId);
                dbhelper.AddParameter("@PMRMCategoryName", PMRM.PMRMCategoryName);
                dbhelper.AddParameter("@ChkIsShipper", PMRM.ChkIsShipper);
                dbhelper.AddParameter("@ChkIsInner", PMRM.ChkIsInner);
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

