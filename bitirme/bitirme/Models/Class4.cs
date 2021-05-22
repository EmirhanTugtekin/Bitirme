using bitirme.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class Class4 //bir view sayfasında birden fazla sınıftan değer alabilmek için (class1 gibi)
    {//bir view sayfasında birden fazla sınıftan değer alabilmek için 
        public IEnumerable<Faturalar> deger1 { get; set; }
        public IEnumerable<FaturaKalem> deger2 { get; set; }
    }
}