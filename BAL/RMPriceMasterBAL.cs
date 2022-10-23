using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class RMPriceMasterBAL
    {
        public int UserId { get; set; }
        public int RMPriceId { get; set; }
        public int FkRMCategoryId { get; set; }
        public int FkRMId { get; set; }
        public string PurchaseDate { get; set; }
        public bool IsPurity { get; set; }
        public decimal RateKgLtr { get; set; }
        public int Quantity { get; set; }
        public decimal PurityPercentage { get; set; }
        public decimal TransporationRate { get; set; }
        public int action { get; set; }
    }
}
