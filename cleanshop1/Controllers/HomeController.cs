//using DNTCaptcha.Core;
using Infrastucture.Persistent.Ef;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Reflection;
using Microsoft.AspNetCore.Session;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using static System.Collections.Specialized.BitVector32;
using System.Diagnostics;
using System;
using Newtonsoft.Json;
using SkiaSharp;
using NPOI.SS.Formula.Functions;
//using BotDetect;
using System.Text;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Drawing.Imaging;
using Domain.ProductAgg;
using Infrastucture.Persistent.Ef;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Domain.Login.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//using BotDetect.Infrastructure.Common;
using Microsoft.VisualBasic;
using Domain.Login;

namespace cleanshop1.Controllers
{
    public class HomeController : Controller
    {
       // private readonly UserManager<userapp> _userManager;
        //private readonly SignInManager<userapp> _signInManager;
        private readonly ILogger<HomeController> _logger;
        //private IDNTCaptchaValidatorService _validatorService;
        //private DNTCaptchaOptions _captchaOptions;
        private  ILoginRepository _repositories;
        //, IDNTCaptchaValidatorService validatorService
        //, IOptions<DNTCaptchaOptions> options
        public HomeController(ILogger<HomeController> logger )
        {
          //  _signInManager = signInManager;
            //_userManager = userManager;
          //  _validatorService = validatorService;
            _logger = logger;
           // _captchaOptions = options == null ? throw new ArgumentException(nameof(options)) : options.Value;
        }

        public IActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Login(LoginModel captcha)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Language.English, DisplayMode.ShowDigits
        //        if (!_validatorService.HasRequestValidCaptchaEntry())
        //        {
        //            //this.ModelState.AddModelError(_captchaOptions.CaptchaComponent.CaptchaInputName, "Plese ennter valid captcha.");
        //            return View("sgvdsd");
        //        }
        //    }
        //    return View("11111");
        //}


   

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrObject { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}



        
        //public ActionResult CaptchaImage()
        //{
        //    var bitmap = new Bitmap(50, 30, PixelFormat.Format24bppRgb);
        //    var graphic = Graphics.FromImage(bitmap);

        //    var random = new Random();
        //    var captchaNum = random.Next(1234, 9999);

        //    graphic.FillRectangle(new SolidBrush(Color.Black), 0, 0, 50, 30f);
        //    graphic.DrawString(captchaNum.ToString(), new Font("Tahoma", 10, FontStyle.Bold),
        //                       new SolidBrush(Color.White), 4, 8);

        //    var memoryStream = new MemoryStream();

        //    q = captchaNum.ToString();

        //    HttpContext.Session.SetString("Captcha", captchaNum.ToString());

        //    bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

        //    return File(memoryStream.ToArray(), "image/png");

        //}
        //public ActionResult SubmitCaptchaImage(Domain.Login.LoginModel login)
        //{
        //    //{      if (q==login.Captcha)
        ////    {

        ////    }

        //    if (HttpContext.Session.GetString("Captcha") == null || HttpContext.Session.GetString("Captcha").ToString() != login.Captcha.ToString())
        //    {
        //        ModelState.AddModelError("Captcha", "مجموع اشتباه است");
        //        return View();
        //    }
        //    else
        //    {
        //        string captcha;
        //        captcha= login.Captcha.ToString();
        //        string username;
        //        username = login.UserName.ToString();
        //        _repositories.CreateCaptchaImage(username, captcha);
        //        return View();
        //    }

        //}






        // <img alt="Captcha" id="imgcpatcha" src="@Url.Action("CaptchaImage2","Home")" style="" />
//private static string q;
//        [HttpGet]
//        [Route("api/User/captchaimage")]
//        public ActionResult CaptchaImage(string prefix, bool noisy = true)
//        {
//            var rand = new Random((int)DateTime.Now.Ticks);
//            //generate new question
//            int a = rand.Next(10, 99);
//            int b = rand.Next(0, 9);
//            var captcha = string.Format("{0} + {1} = ?", a, b);

//            //store answer
//            var e = a + b;
//            //HttpContext.Session.SetString("Captcha", prefix);
//            HttpContext.Session.SetString("Captcha",e.ToString());

//            //image stream
//            FileContentResult img = null;

//            using (var mem = new MemoryStream())
//            using (var bmp = new Bitmap(130, 30))
//            using (var gfx = Graphics.FromImage((System.Drawing.Image)bmp))
//            {
//                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
//                gfx.SmoothingMode = SmoothingMode.AntiAlias;
//                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

//                //add noise
//                if (noisy)
//                {
//                    int i, r, x, y;
//                    var pen = new Pen(Color.Yellow);
//                    for (i = 1; i < 10; i++)
//                    {
//                        pen.Color = Color.FromArgb(
//                        (rand.Next(0, 255)),
//                        (rand.Next(0, 255)),
//                        (rand.Next(0, 255)));

//                        r = rand.Next(0, (130 / 3));
//                        x = rand.Next(0, 130);
//                        y = rand.Next(0, 30);

//                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
//                    }
//                }

//                //add question
//                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

//                //render as Jpeg
//                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
//                img = this.File(mem.GetBuffer(), "image/Jpeg");
//            }

//            return img;
      
//        }
//        [HttpPost]
//        [Route("api/Home/login")]
//        public void login(LoginModel model)
//        {
//            if(HttpContext.Session.GetString("Captcha") == null || HttpContext.Session.GetString("Captcha").ToString() != model.Captcha.ToString())
//            {
//                ModelState.AddModelError("Captcha", "مجموع اشتباه است");
//            }
//            else
//            {

//            }
//        }


    }
}