using Domain.ProductAgg;
using Domain.ProductAgg.Repository;
using Domain.UserAccessAgg.Repository;
using Domain.UserAgg.Repository;
using Domain.UserAccessAgg;
using Infrastucture.Persistent.Ef;
using Infrastucture.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Infrastucture.Repositories
{
    public class UserAccessRepositories : IUserAccessRepository
    {
        private readonly UserManager<Userapp> _userManager;
        private readonly SignInManager<Userapp> _signInManager;
        private readonly MyDBContex _contex;
        private readonly IViewRenderService _viewRenderService;

        public IProductRepository _prosuctRepository { get; }
        public UserAccessRepositories(IProductRepository productRepository, MyDBContex contex, UserManager<Userapp> userManager, SignInManager<Userapp> signInManager, IEmailSend emailsend)
        {
            _prosuctRepository = productRepository;
            _contex = contex;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public void CreateUserAccessRole(UserAccess userAccess)
        {
            _contex.userAccess.Add(userAccess);
            _contex.SaveChanges();
        }
        public List<UserAccess> ReadUserAccessRole()
        {
            var q = _contex.userAccess.Where(i => i.DeleteStatus == false).Select(i => i).ToList();
            return q; 
        }
        public void UpdateUserAccessRole(int id, UserAccess userAccess)
        {
            var q = _contex.userAccess.Where(i => i.Id == id).FirstOrDefault();
            if (q != null)
            {
                q.AccessUpdate = userAccess.AccessUpdate;
                q.AccessDelete = userAccess.AccessDelete;
                q.Role = userAccess.Role;
                q.username = userAccess.username;
                q.users = userAccess.users;
                q.userId = userAccess.userId;
                _contex.SaveChanges();
            }
        }
        public void DeleteUserAccessRole(int id)
        {
            var q = _contex.userAccess.Where(i => i.Id == id).FirstOrDefault();
            if (q != null)
            {
                q.DeleteStatus = false;
                _contex.SaveChanges();
            }
        }
        public void DisableAccessToDelete(UserAccess userProduct)
        {
            var q = _contex.userAccess.Select(i => i).Where(i => i.userId == userProduct.userId || i.username == userProduct.username);
            foreach(var item in q)
            {
                item.AccessDelete = false;
                _contex.SaveChanges();
            }
        }
        public void DisableAccessToUpdate(UserAccess userProduct)
        {
            var q = _contex.userAccess.Select(i => i).Where(i => i.userId == userProduct.userId || i.username == userProduct.username);
            foreach (var item in q)
            {
                item.AccessUpdate = false;
                _contex.SaveChanges();
            }
        }

        public void EnableAccessToDelete(UserAccess userProduct)
        {
            var q = _contex.userAccess.Select(i => i).Where(i => i.userId == userProduct.userId || i.username == userProduct.username);
            foreach (var item in q)
            {
                item.AccessDelete = true;
                _contex.SaveChanges();
            }
        }
        public void UserAccessToDelete(UserAccess userProduct)
        {
            var q = _contex.userAccess.Select(i => i).Where(i => i.userId == userProduct.userId || i.username == userProduct.username);
            foreach (var item in q)
            {
                item.AccessDelete = true;
                _contex.SaveChanges();
            }
        }
        public void UserAccessToUpdate(UserAccess userProduct)
        {
            var q = _contex.userAccess.Select(i => i).Where(i => i.userId == userProduct.userId || i.username == userProduct.username);
            foreach (var item in q)
            {
                item.AccessUpdate = true;
                _contex.SaveChanges();
            }
        }
        public bool ReadAccessToUpdate(UserAccess userProduct)
        {
            var q = _contex.userAccess.Where(i => i.userId == userProduct.userId && i.productId == userProduct.productId).FirstOrDefault();
            if (q.AccessUpdate == true)
            {
                return true;
            }
            return false;
        }
        public bool RaedAccessToDelete(UserAccess userProduct)
        {
            var q = _contex.userAccess.Where(i => i.userId == userProduct.userId && i.productId == userProduct.productId).FirstOrDefault();
            if (q.AccessDelete == true)
            {
                return true;
            }
            return false;
        }
        public void UpdateProduct(int ProductId, int UserId, [FromBody] Product product)
        {
            var user = ReadById(ProductId, UserId);
            bool update;
            update = ReadAccessToUpdate(user);
            if (update == true)
            {
                var q = _contex.product.Where(i => i.Id == ProductId).FirstOrDefault();
                if (q != null)
                {
                    q.Name = product.Name;
                    q.Price = product.Price;
                    _contex.SaveChanges();
                }
            }

        }
        public void DeleteProduct(int ProductId, int UserId)
        {
            var user = ReadById(ProductId, UserId);
            bool delete;
            delete = RaedAccessToDelete(user);
            if (delete == true)
            {
                var q = _contex.product.Where(i => i.Id == ProductId).FirstOrDefault();
                if (q != null)
                {
                    q.DeleteStatus = true;
                    _contex.SaveChanges();
                }
            }
        }


        public void EnableAccessToUpdate(UserAccess userProduct)
        {
         var q = _contex.userAccess.Select(i => i).Where(i => i.userId == userProduct.userId || i.username == userProduct.username);
            foreach (var item in q)
            {
                item.AccessUpdate = false;
                _contex.SaveChanges();
            }
        }

        UserAccess IUserAccessRepository.ReadById(int productid, int userid)
        {
            var q = _contex.userAccess.Where(i => i.userId == productid && i.productId == userid).FirstOrDefault();
            return q;
        }
        UserAccess ReadById(int productid, int userid)
        {
            var q = _contex.userAccess.Where(i => i.userId == productid && i.productId == userid).FirstOrDefault();
            return q;
        }
    }
}
