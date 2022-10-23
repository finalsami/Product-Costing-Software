using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class RMPriceEstimateBAL
    {
        public int UserId { get; set; }
        public int FkRMPriceId { get; set; }
        public int RMPriceEstimateId { get; set; }
        public decimal EstimatePrice { get; set; }
        public string PurchaseDate { get; set; }

        public int action { get; set; }
    }
}
