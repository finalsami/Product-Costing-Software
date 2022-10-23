using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
   
    public class RMMasterBAL
    {
        public int UserId { get; set; }
        public int RMId { get; set; }
        public string RMName { get; set; }
        public bool IsPurity { get; set; }
        public int RMCategoryId { get; set; }
        public string RMCategoryName { get; set; }
        public int UnitMeasurementId { get; set; }

        public int action { get; set; }

    }

    
}
