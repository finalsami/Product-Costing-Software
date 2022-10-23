using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class ProductcategoryBulkMappingBAL
    {
        public int ProductCategoryBulkMappingId { get; set; }
        public int FkProductCategoryId { get; set; }
        public string FkBulkProductId { get; set; }
        public int FkCompanyId { get; set; }
        public int UserId { get; set; }     
        public int action { get; set; }

    }
}
