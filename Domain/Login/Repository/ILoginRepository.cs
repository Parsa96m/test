using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Login.Repository
{
    public interface ILoginRepository
    {
        string CreateCaptchaImage(string username, string captchaImage);
        void SubmitCaptcha(string username, string captcha);
        string ReadCaptchaImage(string username);
        void UpdateCaptchaImage(string CaptchaImage, string PreviousCaptcha);
    }
}
