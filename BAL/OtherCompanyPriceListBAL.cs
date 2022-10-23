using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class OtherCompanyPriceListBAL
    {
        public int OtherComapnyPriceListId { get; set; }
		public decimal Interest { get; set; }
		public decimal AdditionalBufferPM { get; set; }
		public decimal AdditionalBufferLabour { get; set; }
		public decimal LossPercentage { get; set; }
		public decimal MarketedChargePercentage { get; set; }
		public decimal FactoryExpensePercentage { get; set; }
		public decimal OtherPercentage { get; set; }
		public decimal ProfitPercentage { get; set; }
		public decimal FinalPriceUnit { get; set; }
		public int UserId { get; set; }
		public int action { get; set; }
	
	}
}
