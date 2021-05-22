using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace bitirme.Models.Siniflar
{
    public class Context : DbContext //dbcontext entity framework'den gelir. DbSet kullanabilmek için lazım
    {
        //tabloları bu sınıf üzerinden sql'e yansıtıyorum
        //sınıf isimleri ile tablo isimleri karışmasın diye sonlara -s koyacağım
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cariler> Carilers { get; set; }
        public DbSet<Departman> Departmans { get; set; }
        public DbSet<FaturaKalem> FaturaKalems { get; set; }
        public DbSet<Faturalar> Faturalars { get; set; }
        public DbSet<Gider> Giders { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<SatisHareket> SatisHarekets { get; set; }
        public DbSet<Urun> Uruns { get; set; }      
        public DbSet<Detay> Detays { get; set; }      
        public DbSet<KargoDetay> KargoDetays { get; set; }      
        public DbSet<KargoTakip> KargoTakips { get; set; }      
              
    }
}