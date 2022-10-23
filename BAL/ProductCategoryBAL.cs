using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class ProductCategoryBAL
    {
        public int ProductCategoryId { get; set; }
        public string  ProductCategoryName { get; set; }
        public int FkCompanyId { get; set; }
        public int UserId { get; set; }
        public int action { get; set; }


    }
}
