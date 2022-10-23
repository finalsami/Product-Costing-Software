using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class MRPBAL
    {
        public int FkPackingMaterialId { get; set; }
        public int FkCompanyId { get; set; }
        public decimal PercentOfMRP { get; set; }
        public decimal  CompanyMRP { get; set; }
        public int UserId { get; set; }
    }
}
