using Domain.ProductAgg;
using Domain.UserAgg;
using Domain.userappAgg;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RoleAgg
{
    public class Role : IdentityRole
    {
        public Role()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string Name { get; set; }
        public int userid { get; set; }
        public int productid { get; set; }
        //public bool PermissionUpdate { get; set; }
        //public bool PermissionDelete { get; set; }
        public bool DeleteStatus { get; set; }
        //public Product product { get; set; }
        public List<Permission.Permission> permissions{ get; set; }
        //public List<Product> products { get; set; }
        //public User user { get; set; }
        public User userapp { get; set; }
    }
}
