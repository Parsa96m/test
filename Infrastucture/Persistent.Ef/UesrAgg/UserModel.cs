using Infrastucture.Persistent.Ef.PermissionAgg;
using Infrastucture.Persistent.Ef.ProductAgg;
using Infrastucture.Persistent.Ef.RoleAgg;
using Infrastucture.Persistent.Ef.UserAccessAgg;
using Infrastucture.Persistent.Ef.UserProductAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Persistent.Ef.UesrAgg
{
    public class UserModel
    {
        public UserModel()
        {
            DeleteStatus = false;
            IsRegister = false;
        }

        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }
        public bool DeleteStatus { get; set; }
        [Phone]
        [Required]
        [MaxLength(20)]
        public string PhoneUser { get; set; }
        [EmailAddress]
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRegister { get; set; }
        public List<UserAccessModel> useraccess { get; set; }
        public RoleModel role { get; set; }
    //    public List<PermissionModel> permissions { get; set; }
        public List<UserProductModel> userproducts { get; set; }

    }
}
