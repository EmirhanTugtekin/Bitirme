using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bitirme.Models.Siniflar;

namespace bitirme.Controllers
{
    public class DepartmanController : Controller
    {
        Context c = new Context();
        // GET: Departman
        public ActionResult Index()
        {
            var degerler = c.Departmans.Where(x=>x.Durum==true).ToList();
            return View(degerler);
        }
        [Authorize(Roles ="A")]//sadece A yetkisine sahip adminler bu işlemi yapabilir
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
        {//d paramatre olarak view e gönderilecek değerlerdir
            d.Durum = true;
            c.Departmans.Add(d);//k ismindeki nesne view'den gönderilen yeni departman adını tutacak
            c.SaveChanges();//değişiklikleri kaydet
            return RedirectToAction("Index");//iş bitince index'e yönlendirme
        }
        public ActionResult DepartmanSil(int id)
        {
            var dep = c.Departmans.Find(id);
            dep.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {
            var departman = c.Departmans.Find(id);
            return View("DepartmanGetir", departman);//departman isimli değişkenden gelen değerle birlikte "DepartmanGetir" 
            //sayfasını döndüren metot
        }
        public ActionResult DepartmanGuncelle(Departman d)
        {//d paramatre olarak view e gönderilecek değerlerdir
            var dprtmn = c.Departmans.Find(d.Departmanid);
            dprtmn.DepartmanAd = d.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personels.Where(x=>x.Departmanid==id).ToList();
            var dpt = c.Departmans.Where
                (x => x.Departmanid == id).Select(y => y.DepartmanAd).FirstOrDefault();
            //ToList kullanmadım çünkü tek bir değer çekmek istiyorum
            ViewBag.d = dpt;//d sanal bir değişken
            return View(degerler);
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Personelid == id).ToList();
            var per = c.Personels.Where(x => x.Personelid == id).Select(y => y.PersonelAd + " " +y.PersonelSoyad).FirstOrDefault();
            ViewBag.dpers = per;
            return View(degerler);
        }
    }
}