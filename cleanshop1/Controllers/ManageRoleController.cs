using Domain.RoleAgg;
using Domain.UserAccessAgg.Repository;
using Domain.UserAccessAgg;
using Domain.UserAgg.Repository;
using Domain.UserAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.ProductAgg;
using Infrastucture.Repositories;

namespace cleanshop1.Controllers
{
[Authorize(Roles ="Admin")]
    public class ManageRoleController : Controller
    {
        private RoleManager<Role> _roleManager;
        private UserManager<User> _userManager;
        private readonly UserRepositories _userRepository;
        private readonly RoleRepositories _roleRepositories;
        private readonly ProductRepositories _productRepositories;
        private readonly UserAccessRepositories _useraccessRepository;

        public ManageRoleController(RoleManager<Role> roleManager ,UserAccessRepositories userAccessRepositories ,RoleRepositories roleRepositories ,UserRepositories userRepositories ,UserManager<User> userManager, ProductRepositories productRepositories )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _productRepositories = productRepositories;
            _userRepository = userRepositories;
            _roleRepositories = roleRepositories;
            _useraccessRepository = userAccessRepositories;
        }

        public IActionResult Index()
        {
            var rols = _roleManager.Roles.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>AddRole(string name)
        {
            var role = new Role { Name = name };
            var result = await _roleManager.CreateAsync(role);
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if(role == null)
            {
                return NotFound();
            }
            _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditRole(string id)
        {
            var role = _roleManager.FindByIdAsync(id);
            if(role == null)
            {
                return NotFound();
            }

            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = name;
            var result = await _roleManager.UpdateAsync(role);
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(role);
        }
        // [HttpGet]
        //public async Task<IActionResult> EditUser(string id)
        //{
        //    var user = await _roleManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    role.Name = name;
        //    var result = await _roleManager.UpdateAsync(role);
        //    if(result.Succeeded)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    foreach(var error in result.Errors)
        //    {
        //        ModelState.AddModelError(string.Empty, error.Description);
        //    }

        //    return View(role);
        //}
        [HttpPatch]
        [Authorize]
        [Route("api/User/UpdateProduct/permission")]
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
                    _roleRepositories.UpdateProduct(id, usermt.id, product);
                    return "";
                }
                return "";
            }
            return "";
        }

        [HttpPost]
        [Authorize]
        [Route("api/User/DeleteProuduct/permission")]
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
                    _roleRepositories.DeleteProduct(id, usermt.id);
                    return "";
                }
                return "";
            }
            return "";
        }
    }
}
