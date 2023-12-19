using Domain.RoleAgg;
using Domain.UserAccessAgg;
using Domain.UserProductAgg;
using Infrastucture.Persistent.Ef.RoleAgg;
using Infrastucture.Persistent.Ef.UesrAgg;
using Infrastucture.Persistent.Ef.UserProductAgg;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Persistent.Ef.ProductAgg
{
    public class ProductModel
    {
        public ProductModel()
        {
            DeleteStatus = false;
        }

        public int Id { get;  set; }
        public int userId { get;  set; }
        [Required]
        [MaxLength(100)]
        public string Name { get;  set; }
        public DateTime ProduceDate { get;  set; }
        [MaxLength(100)]
        public string Price { get;  set; }
        public bool DeleteStatus { get;  set; }
        [Phone]
        [MaxLength(20)]
        [Required]
        public string PhoneUser { get;  set; }
        public bool Is_A_Valiable { get; set; }
        //public bool PermissionUpdate { get; set; }
       // public bool PermissionDelete { get; set; }

        public List<UserProduct> userproducts { get;  set; }
        public List<Role> roles { get;  set; }
       // public List<RoleModel> role { get;  set; }
        public List<UserAccess> useraccess { get; set; }
    }
}
