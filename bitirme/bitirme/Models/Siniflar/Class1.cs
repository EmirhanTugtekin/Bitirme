using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bitirme.Models.Siniflar
{
    public class Class1
    {//bir view sayfasında birden fazla tablodan veri çekebilmek için bu tablo lazım
        public IEnumerable<Urun> Deger1{ get; set; }
        public IEnumerable<Detay> Deger2{ get; set; }
    }
}