using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryexpenceDAL
    {
        DBHelper dbhelper = null;
        public FactoryexpenceDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable Get_FactoryExpence(int FactoryExpenseId,int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_FactoryExpence");
                dbhelper.AddParameter("@FactoryExpenseId", FactoryExpenseId);
                dbhelper.AddParameter("@CompanyId", CompanyId);

                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertUpdate_FactoryExpence(FactoryexpenceBAL FE)
        {

            ReturnMessage returnMessage = new ReturnMessage();
            try
            {

                dbhelper.SpCommand("SP_InsertUpdate_FactoryExpence");
                dbhelper.AddParameter("@FactoryExpenseId", FE.FactoryExpenseId);
                dbhelper.AddParameter("@FkBulkProductId", FE.FkBulkProductId);
                dbhelper.AddParameter("@FkPackingMaterialId", FE.FkPackingMaterialId);
                dbhelper.AddParameter("@FactoryExpensePercentage", FE.FactoryExpensePercentage);
                dbhelper.AddParameter("@MarketedChargePercentage", FE.MarketedChargePercentage);
                dbhelper.AddParameter("@OtherPercentage", FE.OtherPercentage);
                dbhelper.AddParameter("@ProfitPercentage", FE.ProfitPercentage);
                dbhelper.AddParameter("@FkCompanyId", FE.FkCompanyId);
                dbhelper.AddParameter("@action", FE.action);
                
                dbhelper.AddParameter("@UserId", FE.UserId);
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