using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bitirme.Models.Siniflar;
namespace bitirme.Controllers
{
    public class FaturaController : Controller
    {
        Context c = new Context();//tablolarım context sınıfında tutuluyor
        // GET: Fatura
        public ActionResult Index()
        {
            var urunler = c.Faturalars.ToList();
            return View(urunler);
        }
        [HttpGet] //form yüklenince bu metod çalışacak
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost] //formda bir butona tıklanınca bu metod çalışacak
        public ActionResult FaturaEkle(Faturalar f)
        {//f paramatre olarak view e gönderilecek değerlerdir
            c.Faturalars.Add(f);//f ismindeki nesne view'den gönderilen yeni kategori adını tutacak
            c.SaveChanges();//değişiklikleri kaydet
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var Fatura = c.Faturalars.Find(id);
            return View("FaturaGetir", Fatura);
        }
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var fatura = c.Faturalars.Find(f.Faturaid);
           
            fatura.FaturaSiraNo = f.FaturaSiraNo;
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.Tarih = f.Tarih;
            fatura.VergiDairesi = f.VergiDairesi;
            fatura.Saat = f.Saat;
            fatura.TeslimEden = f.TeslimEden;
            fatura.TeslimAlan = f.TeslimAlan;

            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}