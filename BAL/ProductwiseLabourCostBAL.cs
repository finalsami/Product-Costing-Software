using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class ProductwiseLabourCostBAL
    {
        public int UserId { get; set; }
        public int ProductwiseLaborCostId { get; set; }
        public int FkBulkProductId { get; set; }
        public int FkPackingMaterialId { get; set; }
        public string PackingDescription { get; set; }
        public int FkPackingSizeCategoryId { get; set; }
        public int FkPackingStyleId { get; set; }
        public int PMRMCategoryId { get; set; }
        public decimal StorckNosel { get; set; }
        public decimal NoselsPerFillingLine { get; set; }
        public decimal Supervisiors { get; set; }
        public decimal AdditionalCostBuffer { get; set; }
        public int action { get; set; }

    }
}
