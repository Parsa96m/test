using Infrastucture.Persistent.Ef.PermissionAgg;
using Infrastucture.Persistent.Ef.ProductAgg;
using Infrastucture.Persistent.Ef.UesrAgg;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Persistent.Ef.RoleAgg
{
    public class RoleModel : IdentityRole
    {
        public RoleModel()
        {
            DeleteStatus = true;
        }
        public int id { get; set; }
        public string Name { get; set; }
        public int userid { get; set; }
        public int productid { get; set; }
        //public bool PermissionUpdate { get; set; }
        //public bool PermissionDelete { get; set; }
        public bool DeleteStatus { get; set; }
       // public List<ProductModel> products { get; set; }
        public PermissionModel permission { get; set; }
        // public UserModel user { get; set; }
        
        public UserModel user { get; set; }
    }
}
