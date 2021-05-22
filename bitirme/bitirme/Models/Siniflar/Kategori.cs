using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bitirme.Models.Siniflar
{
    public class Kategori
    {
        [Key]
        public int KategoriID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string KategoriAd { get; set; }

        public ICollection<Urun> Uruns{ get; set; }//int yerine birden fazla 
        //ürünü bir arada tutabilen bir koleksiyona, bir tutucuya ihtiyaç var
        //her kategori içinde birden fazla ürün olabilir
    }
}