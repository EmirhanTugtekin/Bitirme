using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using bitirme.Models.Siniflar;
namespace bitirme.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();
        // GET: CariPanel
        [Authorize]
        public ActionResult Index()
        {
            var cariMail = (string)Session["Mail"];// sisteme giriş yapan mail adresini session a atadım
            var degerler = c.Carilers.Where(x => x.Mail == cariMail).ToList();
            ViewBag.m = cariMail;

            //mail'i cari profiline taşıma
            var mailid = c.Carilers.Where(x => x.Mail == cariMail).Select(y => y.Cariid).FirstOrDefault();
            ViewBag.mid = mailid;

            //toplam satış adetini cari profiline taşıma
            var toplamsatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.toplamsatis = toplamsatis;

            if (toplamsatis != 0)//kendisine hiç satış yapılmamış cari söz konusu olduğunda hata almamak için bunu yazdım.
            {
                //toplam harcanan parayı cari profiline taşıma
                var toplamtutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.ToplamTutar);
                ViewBag.toplamtutar = toplamtutar;

                //toplam ürün adetini cari profiline taşıma
                var toplamurunsayisi = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
                ViewBag.toplamurunsayisi = toplamurunsayisi;
            }
            else
            {
                ViewBag.toplamtutar = 0;
                ViewBag.toplaurunsayisi = 0;
            }
            return View(degerler);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var cariMail = (string)Session["Mail"];// sisteme giriş yapan mail adresini session a atadım
            var id = c.Carilers.Where(x => x.Mail == cariMail.ToString()).Select(y => y.Cariid).FirstOrDefault();
            //id isimli değişkene sisteme giriş yapan mail adresinin sahibinin id'sini aldım
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            //satış hareket tablosundan sisteme giriş yapan mail adresinin sahibinin id'sinin verilerini degerler değikenine atadım
            return View(degerler);
        }
        [Authorize]
        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoDetays select x;
            k = k.Where(y => y.TakipKodu.Contains(p));

            return View(k.ToList());// bu yapı ile kargolar içerisinde arama yapıyorum, 
            //kargolar seçip eğer textbox içerisi boş değilse o ürünleri getirtiyorum
        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();

            return View(degerler);
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        [Authorize]
        public PartialViewResult Partial1()
        {
            var cariMail = (string)Session["Mail"];
            var id = c.Carilers.Where(x => x.Mail == cariMail).Select(y => y.Cariid).FirstOrDefault();
            var cariBul = c.Carilers.Find(id);
            return PartialView("Partial1", cariBul);
        }
        [Authorize]
        public ActionResult CariBilgiGuncelle(Cariler cr)
        {
            var cari = c.Carilers.Find(cr.Cariid);
            cari.CariAd = cr.CariAd;
            cari.CariSoyad = cr.CariSoyad;
            cari.Mail = cr.Mail;
            cari.CariSehir = cr.CariSehir;
            cari.CariSifre = cr.CariSifre;

            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}