using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bitirme.Models.Siniflar;
namespace bitirme.Controllers
{
    public class KargoController : Controller
    {
        Context c = new Context();
        // GET: Kargo
        public ActionResult Index(string p)
        {
            var k = from x in c.KargoDetays select x;
            
            if (!string.IsNullOrEmpty(p))
            {
                k = k.Where(y => y.TakipKodu.Contains(p));
            }
            return View(k.ToList());// bu yapı ile kargolar içerisinde arama yapıyorum, 
            //kargolar seçip eğer textbox içerisi boş değilse o ürünleri getirtiyorum
        }
        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D" };
            int k1, k2, k3;
            k1 = rnd.Next(0, 4);
            k2 = rnd.Next(0, 4);
            k3 = rnd.Next(0, 4);
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);
            string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            ViewBag.takipkod = kod;
            //bu kod kargo takibi için rastgele kod oluşturuyor.
            return View();
        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay d)
        {//d paramatre olarak view e gönderilecek değerlerdir
            c.KargoDetays.Add(d);
            c.SaveChanges();//değişiklikleri kaydet
            return RedirectToAction("Index");//iş bitince index'e yönlendirme
        }
        public ActionResult KargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            
            return View(degerler);
        }
    }
}