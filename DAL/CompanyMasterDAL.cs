using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CompanyMasterDAL
    {
        DBHelper dbhelper = null;

        public CompanyMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable GetCompanyMaster(int UserId, int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_CompanyMaster");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@CompanyId", CompanyId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdateCompanyMaster(CompanyMasterBAL COMP)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_CompanyMaster");
                dbhelper.AddParameter("@CompanyId", COMP.CompanyId);
                dbhelper.AddParameter("@CompanyName", COMP.CompanyName);
                dbhelper.AddParameter("@IsPackingMaster", COMP.IsPackingMaster);
                dbhelper.AddParameter("@action", COMP.action);              
                dbhelper.AddParameter("@UserId", COMP.UserId);
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
