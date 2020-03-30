using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SessionManagement.Models
{
    public class UserRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserRole { get; set; }
    }
}