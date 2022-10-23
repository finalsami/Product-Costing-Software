using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class PackingSizeCategoryBAL
    {
        public int UserId { get; set; }
        public int PackingSizeCategoryId { get; set; }
        public int FkPackingCategoryId { get; set; }
        public decimal PackingSize { get; set; }
        public int FkUnitMeasurementId { get; set; }
        public int action { get; set; }
    }
}
