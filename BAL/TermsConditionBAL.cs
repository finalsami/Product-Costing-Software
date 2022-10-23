using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
   public  class TermsConditionBAL
    {
        public int UserId { get; set; }
        public string TermsconditionId { get; set; }
        public string TermsCondition { get; set; }
        public int FkCompanyId { get; set; }
        public int action { get; set; }
    }
}
