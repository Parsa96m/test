using Domain.ProductAgg.Repository;
using Infrastucture.Persistent.Ef;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.ProductAgg;
using Infrastucture.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Infrastucture.Repositories
{
    public class ProductRepositories : IProductRepository
    {
        private readonly UserManager<Userapp> _userManager;
        private readonly SignInManager<Userapp> _signInManager;
        private readonly MyDBContex _contex;
        private readonly IEmailSend _emailSend;
        private readonly IViewRenderService _viewRenderService;

        public IProductRepository _prosuctRepository { get; }
        public ProductRepositories(IProductRepository productRepository, MyDBContex contex, UserManager<Userapp> userManager, SignInManager<Userapp> signInManager, IEmailSend emailsend)
        {
            _prosuctRepository = productRepository;
            _contex = contex;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSend = emailsend;
        }


        //public void DeleteProduct(int id)
        //{
        //    var q = _contex.product.Where(i => i.id == id).FirstOrDefault();
        //    if (q != null)
        //    {
        //        string userid;
        //        userid = _userManager.GetUserId(User);
        //        if (Convert.ToString(q.user.id) == userid)
        //        {
        //            q.DeleteStatus = true;
        //            _contex.SaveChanges();
        //        }
        //    }
        //}





        //public void UpdateProduct(int id, [FromBody] Data.Entitties.productModel product)
        //{
        //    string userid;
        //    userid = _userManager.GetUserId(User);
        //    int productuser;
        //    productuser = product.user.id;
        //    if (Convert.ToString(productuser) == userid)
        //    {
        //        var q = _contex.product.Where(i => i.id == id).FirstOrDefault();
        //        if (q != null)
        //        {
        //            q.Name = product.Name;
        //            q.Price = product.Price;
        //            q.Is_A_Valiable = product.Is_A_Valiable;
        //            _contex.SaveChanges();
        //        }
        //    }
        //}





        List<Product> ReadProductByUser(string username)
        {

            var q = _contex.userproduct.Where(i => i.username == username).Select(i=>i.products);
            var a = q.Include(i => i);
            if (a != null)
            {
                return a.ToList();
            }
            return null;
        }

        List<Product> FilterProductByUserName(string username)
        {
            var q = _contex.userproduct.Where(i => i.username == username).Select(i => i.products);
            var a = q.Include(i => i);
            return a.ToList();
        }

        List<Product> ReadProductByUserNow(int iduser)
        {
            //  string userid;
            //  int iduser;
            //  userid = _userManager.GetUserId(User);
            //  iduser = Convert.ToInt32(userid);
            var username = _contex.user.Where(i => i.id == iduser).Select(i => i.UserName).FirstOrDefault();
            var q = _contex.userproduct.Where(i => i.username == username).Select(i => i.products);
            var a = q.Select(i => i).ToList();
            if (q != null)
            {
                return a;
            }
            return null;
        }

        List<Product> ReadProduct()
        {
            var q = _contex.product.Where(i => i.DeleteStatus == false).ToList();
            if (q != null)
            {
                return q;
            }
            return null;
        }

        public void GetById(int id)
        {
            throw new NotImplementedException();
        }

        void IProductRepository.CreateProduct([FromBody] Product product)
        {
            product.ProduceDate = DateTime.Now;
            product.Is_A_Valiable = true;
            _contex.product.Add(product);
            _contex.SaveChanges();
        }

        public void CreateProduct(Product product)
        {
            product.ProduceDate = DateTime.Now;
            product.Is_A_Valiable = true;
            _contex.product.Add(product);
            _contex.SaveChanges();
        }

        List<Product> IProductRepository.FilterProductByUserName(string username)
        {
            var q = _contex.userproduct.Where(i => i.username == username).Select(i => i.products);
            var a = q.Include(i => i);
            return a.ToList();
        }

        List<Product> IProductRepository.ReadProductByUserNow(string iduse)
        {
            //string userid;
            //int iduser;
            //userid = _userManager.GetUserId(User);
            int iduser = Convert.ToInt32(iduse);
            var username = _contex.user.Where(i => i.id == iduser).Select(i => i.UserName).FirstOrDefault();
            var q = _contex.userproduct.Where(i => i.username == username).Select(i => i.products);
            var a = q.Select(i => i).ToList();
            if (q != null)
            {
                return a;
            }
            return null;
        }

        List<Product> IProductRepository.ReadProduct()
        {
                var q = _contex.product.Where(i => i.DeleteStatus == false).ToList();
                if (q != null)
                {
                    return q;
                }
                return null;

        }

        List<Product> IProductRepository.ReadProductByUser(string username)
        {
                var q = _contex.userproduct.Where(i => i.username == username).Select(i => i.products);
                var a = q.Include(i => i);
                if (a != null)
                {
                    return a.ToList();
                }
                return null;
        }
    }
}
