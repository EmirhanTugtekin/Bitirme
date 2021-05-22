using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using bitirme.Models.Siniflar;
namespace bitirme.Controllers
{
    [AllowAnonymous]//bu controller giriş işlemi yapılmadan da ulaşılabilir
    public class LoginController : Controller
    {
        Context c = new Context();
        // GET: Login

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {//carinin kayıt olması için
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Partial1(Cariler p)
        {//carinin kayıt olması için
            p.Durum = true;
            c.Carilers.Add(p);
            c.SaveChanges();
            return PartialView();
        }
        [HttpGet]
        public ActionResult CariLogin1()
        {//carinin giriş yapması için
            return PartialView();
        }
        [HttpPost]
        public ActionResult CariLogin1(Cariler p)
        {//carinin giriş yapması için
            var bilgiler = c.Carilers.FirstOrDefault(x => x.Mail == p.Mail && x.CariSifre == p.CariSifre);
            if(bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Mail, false);
                Session["Mail"] = bilgiler.Mail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {//adminin giriş yapması için
            return PartialView();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {//adminin giriş yapması için
            var bilgiler = c.Admins.FirstOrDefault(x => x.KullaniciAd == p.KullaniciAd && x.Sifre == p.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAd, false);
                Session["KullaniciAd"] = bilgiler.KullaniciAd.ToString();
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }
}