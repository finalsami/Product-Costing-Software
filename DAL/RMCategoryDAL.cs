﻿using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
 public   class RMCategoryDAL
    {
        DBHelper dbhelper = null;
        public RMCategoryDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable RMCategoryList(int UserId, int RMCategoryId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_RMCategoryAll");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@RMCategoryId", RMCategoryId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdateRMCategory(RMCategoryBAL RC)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_RMCategory");
                dbhelper.AddParameter("@RMCategoryId", RC.RMCategoryId);
                dbhelper.AddParameter("@RMCategoryName", RC.RMCategoryName);
                dbhelper.AddParameter("@action", RC.action);

                dbhelper.AddParameter("@UserId", RC.UserId);
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
