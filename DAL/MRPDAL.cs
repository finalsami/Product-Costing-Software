using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MRPDAL
    {
        DBHelper dbhelper = null;
        public MRPDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataTable GET_MRPAll(int UserId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_GET_MRPAll");
                dbhelper.AddParameter("@UserId", UserId);

                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;

        }
        public void InsertUpdatePackingDifferenceMaster(MRPBAL PD)
        {

            try
            {
                dbhelper.SpCommand("SP_InsertUpdate_PackingDifference");
                dbhelper.AddParameter("@FkPackingMaterialId", PD.FkPackingMaterialId);
                dbhelper.AddParameter("@FkCompanyId", PD.FkCompanyId);
                dbhelper.AddParameter("@PercentOfMRP", PD.PercentOfMRP);
                dbhelper.AddParameter("@CompanyMRP", PD.CompanyMRP);
                dbhelper.AddParameter("@UserId", PD.UserId);
                dbhelper.ExecuteNonQuery();



            }
            catch (Exception ex)
            {

            }

        }

    }
}
