using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class TradeBulkMappingBAL
    {

		public int UserId { get; set; }
		public int TradeBulkMappingId { get; set; }
		public int FkCompanyId { get; set; }
		public int FkBulkProductId { get; set; }
		public string FkTradeId { get; set; }
		public int action { get; set; }

	}
}
