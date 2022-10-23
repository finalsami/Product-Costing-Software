using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class PMRMPriceMasterBAL
    {
        public int UserId { get; set; }
        public int PMRMPriceId { get; set; }
        public int FkPMRMCategoryId { get; set; }
        public int FkPMRMId { get; set; }
        public decimal Price { get; set; }
        public decimal TrasportationCost { get; set; }
        public decimal Loss { get; set; }
        public int UnitMeasurementId { get; set; }
        public int action { get; set; }

    }
}
