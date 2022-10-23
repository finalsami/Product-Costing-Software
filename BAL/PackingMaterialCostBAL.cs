using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class PackingMaterialCostBAL
    {
       
        public int PackingMaterialCostingId { get; set; }
        public int FkPackingMaterialId { get; set; }

        public int FkPMRMCategoryId { get; set; }
        public int FkPMRMId { get; set; }
    
        public int @UserId { get; set; }
        public int @action { get; set; }
    }
}
