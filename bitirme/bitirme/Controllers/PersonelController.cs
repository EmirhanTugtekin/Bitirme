using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bitirme.Models.Siniflar;
namespace bitirme.Controllers
{
    public class PersonelController : Controller
    {
        Context c = new Context();
        // GET: Personel
        public ActionResult Index()
        {
            var personelListe = c.Personels.Include("Departman").ToList();
            return View(personelListe);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            
            //combobox'a ürünün olası kategorilerini çekmek için
            List<SelectListItem> deger1 = (from x in c.Departmans.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem //bir öğeyi seçtim
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList(); //seçmiş olduğum öğrenin text ve value değerlerini aldım
            ViewBag.dgr1 = deger1;
            //ViewBag controller tarafından view tarafına değer taşımak için kullanılır
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            if (Request.Files.Count > 0)//eğer yaptığım işlemler içerisinde bir dosya tutuyorsam, isteklerimden biri bir dosya ise
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }// bu kısımda personel resimlerini bilgisayardan seçtim
            c.Personels.Add(p);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem //bir öğeyi seçtim
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList(); //seçmiş olduğum öğrenin text ve value değerlerini aldım
            ViewBag.dgr1 = deger1;
            var prs = c.Personels.Find(id);
            return View("PersonelGetir", prs);
        }
        public ActionResult PersonelGuncelle(Personel p)
        {
            if (Request.Files.Count > 0)//eğer yaptığım işlemler içerisinde bir dosya tutuyorsam, isteklerimden biri bir dosya ise
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }// bu kısımda personel resimlerini bilgisayardan seçtim

            var prsn = c.Personels.Find(p.Personelid);
            prsn.PersonelAd = p.PersonelAd;
            prsn.PersonelSoyad = p.PersonelSoyad;
            prsn.PersonelGorsel = p.PersonelGorsel;
            prsn.Departmanid = p.Departmanid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelListe()
        {
            var sorgu = c.Personels.ToList();
            return View(sorgu);
        }
    }
}