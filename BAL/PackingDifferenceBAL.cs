using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class PackingDifferenceBAL
    {
        public int FkPackingMaterialIId { get; set; }
        public int FkCompanyId { get; set; }
        public decimal SuggestedDifference { get; set; }
        public decimal CompanyDifference { get; set; }
        public int UserId { get; set; }

    }
}
