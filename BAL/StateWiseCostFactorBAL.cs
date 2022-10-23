using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class StateWiseCostFactorBAL

    {
        public int UserId { get; set; }
        public int StateWiseCostFactorId { get; set; }
        public int FkStateId { get; set; }
        public int FkProductCategoryId { get; set; }
        public int FkPriceTypeId { get; set; }
        public decimal StaffExpense { get; set; }
        public decimal DepoExpence { get; set; }
        public decimal Incentive { get; set; }
        public decimal Interest { get; set; }
        public decimal Other { get; set; }
        public decimal Marketing { get; set; }
        public int FkCompanyId { get; set; }
        public int action { get; set; }

    }
}
