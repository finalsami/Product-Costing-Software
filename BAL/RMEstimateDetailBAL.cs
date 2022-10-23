using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class RMEstimateDetailBAL
    {
        public int FKRMEstimateId { get; set; }

        public int RMEstimateDetailId { get; set; }

        public string FkRMPriceId { get; set; }

        public decimal RMNewPrice { get; set; }

        public int UserId { get; set; }

        public int action { get; set; }
        

    }
}
