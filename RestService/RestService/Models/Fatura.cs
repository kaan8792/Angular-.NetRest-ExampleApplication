using Newtonsoft.Json;
using RestService.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestService.Models
{
    public class Fatura
    {
        public Fatura()
        {
            FaturaDetays = new HashSet<FaturaDetay>();
        }
        public int FaturaID { get; set; }
        public string Musteri { get; set; }
        
        public decimal Tutar
        {
            get
            {
                try
                {
                    using (var db = new RestContext())
                    {
                        var list = db.FaturaDetays.Where(x => x.FaturaID == this.FaturaID);
                        return list.Sum(x => x.BirimFiyat * x.Miktar);
                    }
                }
                catch
                {
                    return 0;
                }
            }
        }
        
        public virtual ICollection<FaturaDetay> FaturaDetays { get; set; }
        [NotMapped]
        public DateTime? LastModifiedTime { get; set; }
    }
}