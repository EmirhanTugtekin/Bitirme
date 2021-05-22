using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bitirme.Models.Siniflar
{
    public class SatisHareket
    {
        [Key]
        public int Satisid { get; set; }
        //ürün
        //cari
        //personel

        public DateTime Tarih { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal ToplamTutar { get; set; }

        public int Urunid { get; set; }
        public int Cariid { get; set; }
        public int Personelid { get; set; }
        
        public virtual Urun Urun { get; set; }//bir urun birden fazla kez satışta bulunabilir
        public virtual Cariler Cariler { get; set; }//bir cari birden fazla kez satışta bulunabilir
        public virtual Personel Personel { get; set; }//bir personel birden fazla kez satışta bulunabilir
        
    }
}