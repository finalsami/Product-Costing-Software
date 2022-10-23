using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class RMEstimateBAL
    {

        public int RMEstimateId { get; set; }

        public string EstimateDate { get; set; }

        public string EstimateName { get; set; }

        public int FkCompanyId { get; set; }

        public int UserId { get; set; }

        public int action { get; set; }
       
    }
}
