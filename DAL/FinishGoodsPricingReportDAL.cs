using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FinishGoodsPricingReportDAL
    {
        DBHelper dbhelper = null;
        public FinishGoodsPricingReportDAL()
        {
            dbhelper = new DBHelper();
        }
        public DataSet FinishedGoodReportDetail(int UserId,int FkBulkProductId)
        {

            DataSet objds = new DataSet();
            try
            {
                dbhelper.SpCommand("SP_Get_FinishedGoodReportDetail");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@FkBulkProductId", FkBulkProductId);
                objds = dbhelper.GetDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objds;


        }
        public DataSet FinishedGoodReportDetailActual(int UserId, int FkBulkProductId)
        {

            DataSet objds = new DataSet();
            try
            {
                dbhelper.SpCommand("SP_Get_FinishedGoodReportDetailActual");
                dbhelper.AddParameter("@UserId", UserId);
                dbhelper.AddParameter("@FkBulkProductId", FkBulkProductId);
                objds = dbhelper.GetDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objds;


        }

        public DataTable FinishedGoodReport(int UserId)
        {

            DataTable objdt = new DataTable();
            try
            {
                dbhelper.SpCommand("SP_Get_FinishedGoodReport");
                dbhelper.AddParameter("@UserId", UserId);
                objdt = dbhelper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objdt;


        }
    }
}
