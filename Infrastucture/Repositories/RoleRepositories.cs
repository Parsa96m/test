using Azure.Security.KeyVault.Certificates;
using Domain.Permission;
using Domain.ProductAgg;
using Domain.RoleAgg;
using Domain.RoleAgg.Repository;
using Domain.UserAgg;
using Infrastucture.Persistent.Ef;
using Infrastucture.Persistent.Ef.PermissionAgg;
using Infrastucture.Persistent.Ef.ProductAgg;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.Record;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repositories
{
    public class RoleRepositories : IRoleRepository
    {
        private readonly MyDBContex _contex;
        public RoleRepositories(MyDBContex contex)
        {
            _contex = contex;
        }
        public void Exist(User user)
        {
            if (_contex.roles.Count() == 0)
            {
                Role role = new Role();
                role.Name = "Admin";
                role.userid = user.id;
                role.userapp = user;
                _contex.roles.Add(role);
            }
        }
        public void CreateRole(Role Role)
        {
            var q = _contex.roles.Where(i => i.Name == Role.Name);
            if(q == null)
            {
                _contex.Roles.Add(Role);
                _contex.SaveChanges();
            }
        }
      
        public void DeleteAccessRole(int id)
        {
            var q = _contex.roles.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                q.DeleteStatus = true;
                _contex.SaveChanges();
            }
        }

        public void DeleteProduct(int ProductId, int Userid)
        {
            var r = _contex.product.Where(i=>i.userId==Userid).FirstOrDefault();
           
            var a = _contex.product.Where(i => i.Id == ProductId).FirstOrDefault();
            var q = _contex.user.Include("role").Where(p => p.id == Userid).FirstOrDefault();
            var w = _contex.permissions.Where(p => p.productid == ProductId && p.roleid == q.id).FirstOrDefault();
            //q.role.Select(i => i.permissions);
            if(r != null)
            {
                a.DeleteStatus = true;
                _contex.SaveChanges();
            }
            else if (w.PermissionDelete)
            {
                a.DeleteStatus = true;
                _contex.SaveChanges();
            }

        }

        public Role ReadByIdUserAndProduct(int productid, int userid)
        {
            var q = _contex.roles.Where(i => i.userid == userid && i.productid == productid).FirstOrDefault();
            return q;
        }

        public List<Role> ReadRole()
        {
            var q = _contex.roles.Where(i => i.DeleteStatus == false).ToList();
            return q;
        }

        public void UpdateRole(int id, Role Role)
        {
            var q = _contex.roles.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                q.userid = Role.userid;
                q.productid = Role.productid;
                q.userapp = Role.userapp;
                _contex.SaveChanges();
            }
        }
        public void DeleteRole(int id, Role Role)
        {
            var q = _contex.roles.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                q.DeleteStatus = true;
                _contex.SaveChanges();
            }
        }

        public void UpdateProduct(int ProductId, int UserId, Product product)
        {
            var r = _contex.product.Where(i => i.userId == UserId).FirstOrDefault();
            var a = _contex.product.Where(i => i.Id == ProductId).FirstOrDefault();
            var q = _contex.user.Include("role").Where(p => p.id == UserId).FirstOrDefault();
            var w = _contex.permissions.Where(p => p.productid == ProductId && p.roleid == q.id).FirstOrDefault();
            //q.role.Select(i => i.permissions);
            if (r != null)
            {
                a.Name = product.Name;
                a.PhoneUser = product.PhoneUser;
                a.Price = product.Price;
                _contex.SaveChanges();
            }
            else if (w.PermissionUpdate)
            {
                a.Name = product.Name;
                a.PhoneUser = product.PhoneUser;
                a.Price = product.Price;
                _contex.SaveChanges();
            }
        }
        public void CreatePermission(int ProductId, string role,bool update, bool delete)
        {
            Permission permission = new Permission();
            var a = _contex.product.Where(i => i.Id == ProductId).FirstOrDefault();
            var q = _contex.roles.Where(p => p.Name == role).FirstOrDefault();
            permission.roles = q;
            permission.product = a;
            permission.productid = a.Id;
            permission.roleid = q.id;
            permission.PermissionDelete = delete;
            permission.PermissionUpdate = update;
            _contex.permissions.Add(permission);
            _contex.SaveChanges();
        }
        public void DeletePermission(int id)
        {
            var q = _contex.permissions.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                q.DeleteStatus = true;
                _contex.SaveChanges();
            }
        }
        public void UpdatePermission(int id, Permission permission)
        {
            var q = _contex.permissions.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                q.roles = permission.roles;
                q.roleid = permission.roleid;
                q.PermissionDelete = permission.PermissionDelete;
                q.PermissionUpdate = permission.PermissionUpdate;
                q.product = permission.product;
                q.productname = permission.productname;
                q.productid = permission.productid;
                _contex.SaveChanges();
            }
        }
        List<Permission> IRoleRepository.GetAllPermission() 
        {
            var q = _contex.permissions.Select(i => i).Where(p => p.DeleteStatus == false).ToList();
            return q;
        }
        Permission IRoleRepository.GetByIdPermission(int id)
        {
            var q = _contex.permissions.Select(i => i).Where(p => p.DeleteStatus == false && p.id == id).FirstOrDefault();
            return q;
        }
    }
}
