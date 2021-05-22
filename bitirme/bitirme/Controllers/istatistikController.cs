using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bitirme.Models.Siniflar;
namespace bitirme.Controllers
{
    public class istatistikController : Controller
    {
        Context c = new Context();
        // GET: istatistik
        public ActionResult Index()
        {
            return View();
        }
    }
}