using Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Permission
{
    public class Permission
    {
        public Permission()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string productname { get; set; }
        public int productid { get; set; }
        public int roleid { get; set; }
        public bool DeleteStatus { get; set; }
        public bool PermissionUpdate { get; set; }
        public bool PermissionDelete { get; set; }
        public RoleAgg.Role roles { get; set; }
        public Product product { get; set; }
    }
}
