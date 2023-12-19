using Domain.ProductAgg;
using Domain.RoleAgg;
using Infrastucture.Persistent.Ef.ProductAgg;
using Infrastucture.Persistent.Ef.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Persistent.Ef.PermissionAgg
{
    public class PermissionModel
    {
        public PermissionModel()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public int productid { get; set; }
        public int roleid { get; set; }
        public string productname { get; set; }
        public bool DeleteStatus { get; set; }
        public bool PermissionUpdate { get; set; }
        public bool PermissionDelete { get; set; }
        public Role roles { get; set; }
        public Product product { get; set; }
    }
}
