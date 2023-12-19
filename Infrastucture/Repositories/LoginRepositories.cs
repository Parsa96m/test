using Domain.Login.Repository;
using Infrastucture.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repositories
{
    public class LoginRepositories : ILoginRepository
    {
        private readonly MyDBContex _contex;
        public LoginRepositories(MyDBContex Contex)
        {
            _contex = Contex;
        }
   
        public string CreateCaptchaImage(string username, string captchaImage)
        {
            var q = _contex.login.Where(i => i.UserName == username).FirstOrDefault();
            q.CaptchaImage = captchaImage;
            q.IsHumen = true;
            _contex.SaveChanges();
            return "ok";
        }
        public string ReadCaptchaImage(string username)
        {
            var q = _contex.login.Where(i => i.UserName == username).FirstOrDefault();
                return q.CaptchaImage;
        }
        public void SubmitCaptcha(string username, string captcha)
        {
            var q = _contex.login.Where(i => i.UserName == username).FirstOrDefault();
            if (q.CaptchaImage == captcha)
            {
                q.IsHumen = true;
            }
            else
                q.IsHumen = false;
            _contex.SaveChanges();  
        }
        public void UpdateCaptchaImage(string CaptchaImage, string PreviousCaptcha)
        {

        }
    }
}
