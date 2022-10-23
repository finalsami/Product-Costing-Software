using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class PMRMMasterBAL
    {
        public int UserId { get; set; }
        public int PMRMId { get; set; }
        public string PMRMName { get; set; }
        public int FkPMRMCategoryId { get; set; }
        public int FkUnitMeasurementId { get; set; }
        public int Unit { get; set; }
        public decimal TotalWeight { get; set; }
        public int action { get; set; }

    }
}
