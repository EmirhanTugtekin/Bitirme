using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bitirme.Models.Siniflar;
using PagedList;
using PagedList.Mvc;
namespace bitirme.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        Context c = new Context();//tablolarım context sınıfında tutuluyor

        public ActionResult Index(int sayfa = 1)
        {
            //tablo içindeki verilere ulaşmalıyız
            var degerler = c.Kategoris.ToList().ToPagedList(sayfa, 4);
            return View(degerler); //bu iki satır sayesinde kategorinin index.cshtml sayfasına veritabanından veri gönderdim
        }

        [HttpGet] //form yüklenince bu metod çalışacak
        public ActionResult KategoriEkle()
        {

            return View();
        }
        [HttpPost] //formda bir butona tıklanınca bu metod çalışacak
        public ActionResult KategoriEkle(Kategori k)
        {//k paramatre olarak view e gönderilecek değerlerdir
            c.Kategoris.Add(k);//k ismindeki nesne view'den gönderilen yeni kategori adını tutacak
            c.SaveChanges();//değişiklikleri kaydet
            return RedirectToAction("Index");//iş bitince index'e yönlendirme
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg = c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        //kategori güncelleme için hem kategoriGetir hem de KategoriGüncelle ActionResult'larına ihtiyaç var
        public ActionResult KategoriGetir(int id)
        {
            var kategori = c.Kategoris.Find(id);
            return View("KategoriGetir", kategori);//kategori isimli değişkenden gelen değerle birlikte "KategoriGetir" 
            //sayfasını döndüren metot
        }
        public ActionResult KategoriGuncelle(Kategori k)
        {//k paramatre olarak view e gönderilecek değerlerdir
            var ktgr = c.Kategoris.Find(k.KategoriID);
            ktgr.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Deneme()
        {
            Class3 cs = new Class3();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "Urunid", "UrunAd");
            return View(cs);
        }
        public JsonResult UrunGetir(int p)
        {
            var urunlistesi = (from x in c.Uruns
                               join y in c.Kategoris
                               on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text = x.UrunAd,
                                   Value = x.Urunid.ToString()
                               }).ToList();
            return Json(urunlistesi, JsonRequestBehavior.AllowGet);
        }
    }
}