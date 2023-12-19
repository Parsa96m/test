using Domain.Permission;
using Domain.ProductAgg;
using Domain.RoleAgg;
using Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RoleAgg.Repository
{
    public interface IRoleRepository
    {
        void CreateRole(Role Role);
        List<Role> ReadRole();
        void UpdateRole(int id, Role Role);
        void DeleteRole(int id, Role Role);
        public void Exist(User user);
        void DeleteAccessRole(int id);
        void UpdateProduct(int ProductId, int UserId, Product product);
        void DeleteProduct(int ProductId, int UserId);
        Role ReadByIdUserAndProduct(int productid, int userid);
        void CreatePermission(int ProductId, string role, bool update, bool delete);
        void DeletePermission(int id);
        void UpdatePermission(int id, Permission.Permission permission);
        List<Permission.Permission> GetAllPermission();
        Permission.Permission GetByIdPermission(int id);
    }
}
