using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PackingDifferenceDAL
    {
        DBHelper dbhelper = null;
        public PackingDifferenceDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable ProductPackingMaterialCostingList(int UserId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_GET_PackingDifferenceAll");
                dbhelper.AddParameter("@UserId", UserId);              

                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;

        }
        public void InsertUpdatePackingDifferenceMaster(PackingDifferenceBAL PD)
        {
            
            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_PackingDifference");
                dbhelper.AddParameter("@FkPackingMaterialIId", PD.FkPackingMaterialIId);
                dbhelper.AddParameter("@FkCompanyId", PD.FkCompanyId);
                dbhelper.AddParameter("@SuggestedDifference", PD.SuggestedDifference);
                dbhelper.AddParameter("@CompanyDifference", PD.CompanyDifference);
                dbhelper.AddParameter("@UserId", PD.UserId);                     
                dbhelper.ExecuteNonQuery();

               

            }
            catch (Exception ex)
            {
                
            }
            
        }

    }
}
