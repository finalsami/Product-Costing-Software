using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL
    {
        DBHelper dbhelper = null;
        public UserDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable UserLogIn(UserBAL usd)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("sp_UserLogin");
                dbhelper.AddParameter("@UserName", usd.UserName);
                dbhelper.AddParameter("@Password", usd.Password);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }

        public DataTable Get_UserMasterAll()
        {
            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("sp_Get_UserMasterDataAll");
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt; ;
        }
        public DataTable Get_UserMasterById(int UserId)
        {
            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("sp_Get_UserMasterDataById");
                dbhelper.AddParameter("@UserId", UserId);

                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;

        }
        public ReturnMessage InsertUpdate_UserMaster(UserMasterBAL USR)
        {

            ReturnMessage returnMessage = new ReturnMessage();
            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_UserMaster");
                dbhelper.AddParameter("@FkCompanyId", USR.FkCompanyId);
                dbhelper.AddParameter("@FirstName", USR.FirstName);
                dbhelper.AddParameter("@LastName", USR.LastName);
                dbhelper.AddParameter("@UserName", USR.UserName);
                dbhelper.AddParameter("@MobileNo", USR.MobileNo);
                dbhelper.AddParameter("@GroupId", USR.GroupId);
                dbhelper.AddParameter("@Password", USR.Password);
                dbhelper.AddParameter("@Email", USR.Email);
                dbhelper.AddParameter("@OTP", USR.OTP);
                dbhelper.AddParameter("@RefreshToken", USR.RefreshToken);
                dbhelper.AddParameter("@IsActive", USR.IsActive);
                dbhelper.AddParameter("@IsCompanyAdmin", USR.IsCompanyAdmin);
                dbhelper.AddParameter("@IsChangePassword", USR.IsChangePassword);
                dbhelper.AddParameter("@action", USR.action);
                dbhelper.AddParameter("@UserId", USR.UserId);
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
