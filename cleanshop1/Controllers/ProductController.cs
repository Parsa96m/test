using Domain;
using Domain.ProductAgg;
using Domain.ProductAgg.Repository;
using Domain.UserAccessAgg;
using Domain.UserAgg;
using Domain.UserProductAgg;
using Domain.UserProductAgg.Repository;
using Infrastucture.Persistent.Ef;
using Infrastucture.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NPOI.SS.Formula.Functions;
using System.Security.Claims;

namespace cleanshop1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase//BaseController ControllerBase
    {
        private readonly UserManager<Userapp> _userManager;
        private readonly SignInManager<Userapp> _signInManager;
        private readonly MyDBContex _contex;
        //private readonly IEmailSend _emailSend;
        //private readonly IViewRenderService _viewRenderService;
        public ProductRepositories _productRepository { get; }
        public UserRepositories _userRepository { get; }
        private ClaimsPrincipal User;
        public UserAccessRepositories _useraccessRepository { get; }
        //public userpro _userproductRepository { get; }
        public ProductController(UserRepositories userRepositories, UserAccessRepositories userAccessRepositories, ProductRepositories productRepository, MyDBContex contex, UserManager<Userapp> userManager, SignInManager<Userapp> signInManager)
        {
            _productRepository = productRepository;
           // _userproductRepository = userProductRepository;
            _contex = contex;
            _userManager = userManager;
            _signInManager = signInManager;
            _useraccessRepository = userAccessRepositories;
            _userRepository = userRepositories;
        }


        [HttpPost]
        [Route("api/User/CreateProduct")]
        public async Task<string> CreateProduct([FromBody] Product product)

        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productRepository.CreateProduct(product);
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
            
        }

        [HttpPatch]
        [Authorize]
        [Route("api/User/UpdateProduct")]
        public async Task<string> UpdateProduct(int id, [FromBody] Product product)
        {
            UserAccess userProduct = new UserAccess();
            Domain.UserAgg.User usermt = new Domain.UserAgg.User();
            userProduct.productId = id;
            string user;
            user = _userManager.GetUserName(User);
            usermt = _userRepository.GetByusername(user);
            userProduct.userId = usermt.id;
            if (ModelState.IsValid)
            {
                bool access = _useraccessRepository.ReadAccessToUpdate(userProduct);
                if (access)
                {
                    _useraccessRepository.UpdateProduct(id, usermt.id, product);
                    return "";
                }
                return "";
            }
            return "";
        }

        [HttpPost]
        [Authorize]
        [Route("api/User/DeleteProuduct")]
        public async Task<string> DeleteProduct(int id)
        {
            UserAccess userProduct = new UserAccess();
            User usermt = new User();
            userProduct.productId = id;
            string user;
            user = _userManager.GetUserName(User);
            usermt = _userRepository.GetByusername(user);
            userProduct.userId = usermt.id;
            if (ModelState.IsValid)
            {
                bool access = _useraccessRepository.ReadAccessToUpdate(userProduct);
                if (access)
                {
                    _useraccessRepository.DeleteProduct(id, usermt.id);
                    return "";
                }
                return "";
            }
            return "";
        }

        [HttpGet]
        [Route("api/User/ReadProductByUser")]
        public async Task<List<Product>> ReadProductByUser(string username)
        {
            _productRepository._prosuctRepository.ReadProductByUser(username);
            return null;
        }
        [HttpGet]
        [Route("api/User/FilterProductByUserName")]
        public async Task<List<Product>> FilterProductByUserName(string username)
        {
            if (ModelState.IsValid)
            {
                return _productRepository._prosuctRepository.FilterProductByUserName(username);
            }
            return null;
        }

        [HttpGet]
        [Route("api/User/ReadProductByUserNow ")]
        [Authorize]
        public async Task<List<Product>> ReadProductByUserNow()
        {
            if (ModelState.IsValid)
            {
                string id = _userManager.GetUserId(User);
                return  _productRepository._prosuctRepository.ReadProductByUserNow(id);
            }
            return null;
        }
        [HttpGet]
        [Route("api/User/ReadProduct")]
        public async Task<List<Product>> ReadProduct()
        {
            if (!ModelState.IsValid)
            {
                return _productRepository._prosuctRepository.ReadProduct();
            }
            return null;
        }
        [HttpGet]
        [Route("api/User/EnableAccessToDelete")]
        public async Task<string> EnableAccessToDelete()
        {
            if (!ModelState.IsValid)
            {
                UserAccess userProduct = new UserAccess();
                User user = new User();
                string username = _userManager.GetUserName(User);
                user = _userRepository.GetByusername(username);
                userProduct.userId = user.id;
                userProduct.username = user.UserName;
                _useraccessRepository.EnableAccessToDelete(userProduct);
                return "";
            }
            return "";
        }
        [HttpGet]
        [Route("api/User/EnableAccessToUpdate")]
        public async Task<string> EnableAccessToUpdate()
        {
            if (!ModelState.IsValid)
            {
                UserAccess userProduct = new UserAccess();
                User user = new User();
                string username = _userManager.GetUserName(User);
                user = _userRepository.GetByusername(username);
                userProduct.userId = user.id;
                userProduct.username = user.UserName;
                _useraccessRepository.EnableAccessToUpdate(userProduct);
                return "";
            }
            return "";
        }
        [HttpGet]
        [Route("api/User/DisableAccessToDelete")]
        public async Task<string> DisableAccessToDelete()
        {
            if (!ModelState.IsValid)
            {
                UserAccess userProduct = new UserAccess();
                User user = new User();
                string username = _userManager.GetUserName(User);
                user = _userRepository.GetByusername(username);
                userProduct.userId = user.id;
                userProduct.username = user.UserName;
                _useraccessRepository.DisableAccessToDelete(userProduct);
                return ""; 
            }
            return "";
        }
        [HttpGet]
        [Route("api/User/DisableAccessToUpdate")]
        public async Task<string> DisbleAccessToUpdate()
        {
            if (!ModelState.IsValid)
            {
                UserAccess userProduct = new UserAccess();
                User user = new User();
                string username = _userManager.GetUserName(User);
                user = _userRepository.GetByusername(username);
                userProduct.userId = user.id;
                userProduct.username = user.UserName;
                _useraccessRepository.DisableAccessToUpdate(userProduct);
                return "";
            }
            return "";
        }


    }
}
