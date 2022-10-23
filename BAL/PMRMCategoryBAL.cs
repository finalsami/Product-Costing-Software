using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
   public  class PMRMCategoryBAL
    {
        public int UserId { get; set; }
        public int PMRMCategoryId { get; set; }
        public string PMRMCategoryName { get; set; }
        public bool ChkIsShipper { get; set; }
        public bool ChkIsInner { get; set; }
        public int action { get; set; }
    }
}
