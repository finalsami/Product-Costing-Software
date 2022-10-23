using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RoleMasterDAL
    {
        DBHelper dbhelper = null;
        public RoleMasterDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_GroupMaster()
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_GroupMasterGrid");
      
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public DataTable Get_RoleDataById(int GroupId)
        {
            DataTable objdt = new DataTable();

            try
            {
                dbhelper.SpCommand("SP_Get_RoleDataById");
                dbhelper.AddParameter("@GroupId", GroupId);
              
                dbhelper.ExecuteNonQuery();
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;
        }
        public ReturnMessage Group_CreateUpdate(GroupMasterBAL GM)
        {

            ReturnMessage returnMessage = new ReturnMessage();
            try
            {
                if (GM.action==3)
                {
                    dbhelper.SpCommand("SP_Delete_GroupFromRoleMaster");
                    dbhelper.AddParameter("@GroupId", GM.GroupId);

                }
                else
                {
                    dbhelper.SpCommand("SP_Group_CreateUpdate");
                    dbhelper.AddParameter("@FkCompanyId", GM.FkCompanyId);
                    dbhelper.AddParameter("@GroupId", GM.GroupId);
                    dbhelper.AddParameter("@GroupName", GM.GroupName);
                    dbhelper.AddParameter("@IsActive", GM.IsActive);
                    dbhelper.AddParameter("@UpdatedBy", GM.UserId);
                    dbhelper.Command.Parameters.Add("@OUTVAL", System.Data.SqlDbType.Int);
                    dbhelper.Command.Parameters["@OUTVAL"].Direction = System.Data.ParameterDirection.Output;
                    dbhelper.Command.Parameters.Add("@OUTMESSAGE", System.Data.SqlDbType.VarChar, 500);
                    dbhelper.Command.Parameters["@OUTMESSAGE"].Direction = System.Data.ParameterDirection.Output;
                   
                }              
          

             
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
