using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
   public class TradeBAL
    {
       
            public int UserId { get; set; }
            public int TradeId { get; set; }
            public string TradeName { get; set; }
            public int FkcompanyId { get; set; }
            public int action { get; set; }

        
    }
}
