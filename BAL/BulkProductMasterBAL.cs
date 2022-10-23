using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class BulkProductMasterBAL
    {
        public int UserId { get; set; }
        public int BulkProductId { get; set; }
        public int MainCategoryId { get; set; }
        public int GstId { get; set; }
        public string  BulkProductName { get; set; }
        public int FkSourceId { get; set; }
        public int action { get; set; }
        public int UnitMeasurementId { get; set; }
    }
}
