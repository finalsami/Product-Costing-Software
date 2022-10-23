using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class ProductInterestMasterBAL
    {
        public int UserId { get; set; }
        public int BulkProductInterestId { get; set; }
        public int FkBulkProductId { get; set; }
        public decimal Interest { get; set; }
        public int action { get; set; }

    }
}
