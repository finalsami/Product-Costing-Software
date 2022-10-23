using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class CompanyMappingBAL
    {
        public int UserId { get; set; }
        public int BulkCompanyMappingId { get; set; }
        public int FkCompanyId { get; set; }        
        public string FkBulkProductId { get; set; }
        public int action { get; set; }
    }
}
