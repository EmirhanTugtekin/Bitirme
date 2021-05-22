using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bitirme.Models.Siniflar;

namespace bitirme.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun

        Context c = new Context();//tablolarım context sınıfında tutuluyor

        public ActionResult Index(string p)
        {
            var urunler = from x in c.Uruns select x;
            if(!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(y => y.UrunAd.Contains(p));
            }
            return View(urunler.ToList());// bu yapı ile ürünler içerisinde arama yapıyorum, 
            //ürünleri seçip eğer textbox içerisi boş değilse o ürünleri getirtiyorum
        }

        [HttpGet]//form yüklenince bu metod çalışacak
        public ActionResult YeniUrun()
        {
            //combobox'a ürünün olası kategorilerini çekmek için
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem //bir öğeyi seçtim
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList(); //seçmiş olduğum öğrenin text ve value değerlerini aldım
            ViewBag.dgr1 = deger1;
            //ViewBag controller tarafından view tarafına değer taşımak için kullanılır
            return View();
        }//bir öğeyi seçip seçmiş olduğum öğrenin

        [HttpPost]//formda bir butona tıklanınca bu metod çalışacak
        public ActionResult YeniUrun(Urun p)
        {
            if (Request.Files.Count > 0)//eğer yaptığım işlemler içerisinde bir dosya tutuyorsam, isteklerimden biri bir dosya ise
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.UrunGorsel = "/Image/" + dosyaadi + uzanti;
            }// bu kısımda personel resimlerini bilgisayardan seçtim

            c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem //bir öğeyi seçtim
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList(); //seçmiş olduğum öğrenin text ve value değerlerini aldım
            ViewBag.dgr1 = deger1;
            //ViewBag controller tarafından view tarafına değer taşımak için kullanılır

            var urunDeger = c.Uruns.Find(id);
            return View("UrunGetir", urunDeger);
        }
        public ActionResult UrunGuncelle(Urun p)
        {
            if (Request.Files.Count > 0)//eğer yaptığım işlemler içerisinde bir dosya tutuyorsam, isteklerimden biri bir dosya ise
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.UrunGorsel = "/Image/" + dosyaadi + uzanti;
            }// bu kısımda personel resimlerini bilgisayardan seçtim

            var urn = c.Uruns.Find(p.Urunid);

            urn.AlisFiyat = p.AlisFiyat;
            urn.SatisFiyat = p.SatisFiyat;
            urn.Durum = p.Durum;
            urn.Kategoriid = p.Kategoriid;
            urn.Marka = p.Marka;
            urn.Stok = p.Stok;
            urn.UrunAd = p.UrunAd;
            urn.UrunGorsel = p.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            var deger1 = c.Uruns.Find(id);
            ViewBag.dgr1 = deger1.Urunid;
            ViewBag.dgr2 = deger1.SatisFiyat;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index","Satis");
            
        }
    }
}