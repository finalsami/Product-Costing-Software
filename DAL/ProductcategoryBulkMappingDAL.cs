using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductcategoryBulkMappingDAL
    {
        DBHelper dbhelper = null;
        public ProductcategoryBulkMappingDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable CategoryBulkMapping(int CompanyId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_CategoryBulkMapping");
                dbhelper.AddParameter("@FkCompanyId", CompanyId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
        public ReturnMessage InsertCategoryBulkMapping(ProductcategoryBulkMappingBAL Product)
        {
            ReturnMessage returnMessage = new ReturnMessage();

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_ProductCategoryBulkMapping");
                dbhelper.AddParameter("@ProductCategoryBulkMappingId", Product.ProductCategoryBulkMappingId);
                dbhelper.AddParameter("@FkCompanyId", Product.FkCompanyId);
                dbhelper.AddParameter("@FkProductCategoryId", Product.FkProductCategoryId);
                dbhelper.AddParameter("@FkBulkProductId", Product.FkBulkProductId);
                dbhelper.AddParameter("@action", Product.action);
                dbhelper.AddParameter("@UserId", Product.UserId);
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
