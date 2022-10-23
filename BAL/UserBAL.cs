using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class UserBAL
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime DOB { get; set; }
    }

    public class UserMasterBAL
    {
        public int FkCompanyId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string GroupId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string OTP { get; set; }
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompanyAdmin { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsChangePassword { get; set; }
        public int action { get; set; }

    }
}
