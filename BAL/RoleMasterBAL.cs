using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class RoleMasterBAL
    {

        public int RoleId { get; set; }

        public int MenuId { get; set; }
        public int SubMenuId { get; set; }
        public int CanView { get; set; }
        public int CanEdit { get; set; }
        public int CanDelete { get; set; }
        public int action { get; set; }
    }
    public class GroupMasterBAL
    {
        public int FkCompanyId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
        public int action { get; set; }


    }
}
