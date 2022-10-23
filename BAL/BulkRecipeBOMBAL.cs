using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class BulkRecipeBOMBAL
    {
        public int UserId { get; set; }
        public int BOMId { get; set; }
        public int FkMainCategoryId { get; set; }
        public int FkBulkProductId { get; set; }
        public int BatchSize { get; set; }
        public int UnitMeasurementId { get; set; }
        public int FormulationId { get; set; }
        public decimal Spgr { get; set; }
        public decimal FormulationLostPercentage { get; set; }

        public int action { get; set; }
    }
    public class BulkRecipeBOMDetail
    {
        public int UserId { get; set; }

        public int IngredientId { get; set; }
        public int FkBOMId { get; set; }
        public int FkRMId { get; set; }
        public int FkBulkProductId { get; set; }
        public int FkMainCategoryId { get; set; }
        public int FkRMPriceId { get; set; }
        public string IngredientName { get; set; }
        public decimal QuantityLtrKg { get; set; }
        public decimal Formulation { get; set; }
        public decimal Solvant { get; set; }
        public int action { get; set; }


    }
    public class BulkRecipSPGR
    {
        public int UserId { get; set; }
        public int BOMId { get; set; }      
        public int FormulationId { get; set; }
        public decimal Spgr { get; set; }
        public decimal FormulationLostPercentage { get; set; }

        public int action { get; set; }

    }
}
