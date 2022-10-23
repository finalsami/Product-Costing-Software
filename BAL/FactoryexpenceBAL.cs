using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class FactoryexpenceBAL
    {
        public int FactoryExpenseId { get; set; }
        public int UserId { get; set; }
        public int FkBulkProductId { get; set; }
        public int FkPackingMaterialId { get; set; }
        public int FkCompanyId { get; set; }
        public decimal FactoryExpensePercentage { get; set; }
        public decimal MarketedChargePercentage { get; set; }
        public decimal OtherPercentage { get; set; }
        public decimal ProfitPercentage { get; set; }
        
        public int action { get; set; }
    }
}
