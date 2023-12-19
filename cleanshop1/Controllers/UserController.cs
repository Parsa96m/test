//using DNTCaptcha.Core;
using Infrastucture.Persistent.Ef;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Text;
using Infrastucture;
using Microsoft.EntityFrameworkCore;
using Domain.Login;
//using Infrastucture.Tools;
using Infrastucture.Repositories;
using Infrastucture.Persistent.Ef.UesrAgg;
using Domain.UserAgg;
using Infrastucture.Tools;
using DNTCaptcha.Core;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing;
using NPOI.SS.UserModel;
using Domain;

namespace cleanshop1.Controllers
{
    public class UserController : Controller
    {
 
        public IActionResult Index()
        {
            ViewBag.IsSent = false;
            return View();
        }
        // private readonly IGoogleRecaptcha _Recaptcha;
        private readonly ILogger<HomeController> _logger;
        private IDNTCaptchaValidatorService _validatorService;
        private DNTCaptchaOptions _captchaOptions;
        private readonly UserManager<Userapp> _userManager;
        private readonly SignInManager<Userapp> _signInManager;
        private readonly MyDBContex _contex;
        private readonly IEmailSend _emailSend;
        private readonly IViewRenderService _viewRenderService;
        public UserRepositories _userRepositories;
        
        public UserController(ILogger<HomeController> logger, IDNTCaptchaValidatorService validatorService, IOptions<DNTCaptchaOptions> options, UserRepositories userRepositories, UserManager<Userapp> userManager, SignInManager<Userapp> signInManager, MyDBContex contex)
        {
           // _validatorService = validatorService;
            _logger = logger;
           // _captchaOptions = options == null ? throw new ArgumentException(nameof(options)) : options.Value;
           // _userRepositories = userRepositories;
            _contex = contex;
            _userManager = userManager;
            _signInManager = signInManager;
            // _Recaptcha = recaptcha;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewBag.ReturnUrl = returnUrl;
            return View("/user/login");
        }


        [HttpGet]
        [Route("api/User/captchaimage")]
        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer
            var e = a + b;
            //HttpContext.Session.SetString("Captcha", prefix);
            HttpContext.Session.SetString("Captcha", e.ToString());

            //image stream
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((System.Drawing.Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;

        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/User/Login")]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            //if(!await _Recaptcha.IsSatisfy())
            //{
            //    ModelState.AddModelError("", "اعتبارسنجی انجام نشد");
            //    return View("Index");
            //}
            if (HttpContext.Session.GetString("Captcha") == null || HttpContext.Session.GetString("Captcha").ToString() != model.Captcha.ToString())
            {
                ModelState.AddModelError("Captcha", "مجموع اشتباه است");
            }
            else
            {

            

            returnUrl ??= Url.Content("~/");
            if (!ModelState.IsValid) return View(model);
            if (ModelState.IsValid)
            {
                if (!_validatorService.HasRequestValidCaptchaEntry())
                {
                    //this.ModelState.AddModelError(_captchaOptions.CaptchaComponent.CaptchaInputName, "Plese ennter valid captcha.");
                    return View("sgvdsd");
                }
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "کاربری با این مشخصات یافت نشد");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                        Response.Cookies.Append("username", user.UserName, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = Request.IsHttps,
                            Path = Request.PathBase.HasValue ? Request.PathBase.ToString() : "/",
                            Expires = DateTime.Now.AddHours(1),
                        });

                    }
                else
                {
                    ModelState.AddModelError(string.Empty, "تلاش برای ورود نامعتبر است!");
                }

                return View("/user/login");
            
            }
            }
            return View("jhgv");
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/User/LogOute")]
        public async Task<IActionResult> LogOute()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [Route("/api/User/createUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(User u)
        {
           


                //UserBLL bll = new UserBLL();
                if (u.Password == u.Password_Again)
                {
                    var user = new Userapp
                    {
                        UserName = u.UserName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneUser,
                        firstname = u.Name
                    };
                    User use = new User();
                    use.Email = u.Email;
                    use.UserName = u.UserName;
                    use.Name = u.Name;
                    use.PhoneUser = u.PhoneUser;
                    use.IsRegister = true;
                    use.Password = u.Password;
                    _userRepositories.Create(use);
                    _contex.SaveChanges();
                    var addresult = await _userManager.CreateAsync(user, u.Password);
                    if (!addresult.Succeeded)
                    {
                        foreach (var err in addresult.Errors)
                        {
                            ModelState.AddModelError(key: string.Empty, errorMessage: err.Description);
                            return View();
                        }
                    }
                    var uuser = await _userManager.FindByNameAsync(u.UserName);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(uuser);
                    token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    string? callBackUrl = Url.ActionLink("confirmemail", "account", new { userId = uuser.Id, token = token }, Request.Scheme);
                    string body = await _viewRenderService.RenderToStringAsync("_RegisterEmail", callBackUrl);
                    await _emailSend.SendEmailAsync(new EmailModel(uuser.Email, "تایید حساب", body));
                    ViewBag.IsSent = true;
                }
            
            return View("../Home/Index");



        }

        //SendEmail
        #region Remote Validations

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsAnyUserName(string username)
        {
            bool any = await _userManager.Users.AnyAsync(i => i.UserName == username);
            if (!any)
                return Json(true);

            return Json("نام کاربری تکراری است");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsAnyEmail(string email)
        {
            bool any = await _userManager.Users.AnyAsync(i => i.Email == email);
            if (!any)
                return Json(true);

            return Json(" ایمیل تکراری است");

        }
        #endregion
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            ViewBag.IsConfirmed = result.Succeeded ? true : false;
            return View("~/");
        }
    }
}
