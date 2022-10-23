using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class CompanyMasterBAL
    {
        public int UserId { get; set; }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool IsPackingMaster { get; set; }
        public int action { get; set; }
    }
}
