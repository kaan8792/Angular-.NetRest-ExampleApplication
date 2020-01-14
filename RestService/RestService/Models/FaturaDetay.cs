using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestService.Models
{
    public class FaturaDetay
    {   
        public int FaturaDetayID { get; set; }
        public int FaturaID { get; set; }
        [NotMapped]
        public virtual Fatura Fatura { get; set; }
        public string Aciklama { get; set; }
        public decimal BirimFiyat { get; set; }
        public int Miktar { get; set; }
        [NotMapped]
        public decimal ToplamTutar { get { return BirimFiyat * Miktar; } }
        [NotMapped]
        public DateTime? LastModifiedTime { get; set; }

    }
}