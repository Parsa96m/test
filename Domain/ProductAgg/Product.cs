using Domain.Permission;
using Domain.RoleAgg;
using Domain.UserAccessAgg;
using Domain.UserAgg;
using Domain.UserProductAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductAgg
{
    public class Product
    {
        public Product()
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
        public bool Is_A_Valiable { get;  set; }
        public List<UserProduct> userproducts { get;  set; }
       // public List<Role> role { get;  set; }
        public List<Role> roles { get;  set; }
        public List<UserAccess> useraccess { get; set; }
        //  public User user { get;  set; }

    }
}
