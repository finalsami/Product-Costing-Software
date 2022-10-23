using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class BulkCostBAL
    {
        public int FkBulkProductId { get; set; }
        public string ShareName { get; set; }
        public string Mobile { get; set; }
        public int FkCompanyId { get; set; }
        public int UserId { get; set; }
        public int Action { get; set; }
        public int PackingType { get; set; }
        public decimal Packingsize { get; set; }
        public decimal ProfitPer { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal AddDiscount { get; set; }
        public string ReportImage { get; set; }
    }
    public class ShareforBulkCostBAL
    {
        public int ShareNameId { get; set; }
        public string ShareName { get; set; }
        public int FkBulkProductId { get; set; }
        public string Mobile { get; set; }
        public int FkCompanyId { get; set; }
        public int UserId { get; set; }
        public int Action { get; set; }

        public int PackingType { get; set; }
        public decimal Packingsize { get; set; }
        public decimal ProfitPer { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal AddDiscount { get; set; }
        public string TermsCondId { get; set; }
    }
}
