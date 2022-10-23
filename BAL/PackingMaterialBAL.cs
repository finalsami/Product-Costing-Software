
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class PackingMaterialBAL
    {

        public int PackingMaterialId { get; set; }
        public int FkBulkProductId { get; set; }
        public string PackingName { get; set; }
        public decimal PackingSize { get; set; }
        public int FkPackingCategoryId { get; set; }
        public int PackingMeasurementId { get; set; }
        public decimal ShipperSize { get; set; }
        public int UnitMeasurementId { get; set; }
        public int FkPMRMCategoryId { get; set; }
        public bool IsMasterPacking { get; set; }
        public int InnerPackingCategoryId { get; set; }
        public decimal InnerSize { get; set; }
        public int InnerPackingMeasurementId { get; set; }
        public int @UserId { get; set; }
        public int @action { get; set; }

       
    }
}
