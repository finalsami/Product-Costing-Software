using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class PriceListGPActualEstimateBAL
    {
        public int UsretId { get; set; }
        public int PriceListGPActualEstimateId { get; set; }
        public int FkBulkProductId { get; set; }
        public int FkPackingMaterialId { get; set; }
        public int FkStateId { get; set; }
        public int FkTradeId { get; set; }
        public int FkPriceTypeId { get; set; }
        public int FkCompanyId { get; set; }
        public decimal TOD { get; set; }
        public decimal PD { get; set; }
        public decimal QD { get; set; }
        public decimal ProfitPer { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal AdditionalPD { get; set; }
        public int EstimateId { get; set; }
        public int action { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }

        public string PriceListName { get; set; }

    }
}
