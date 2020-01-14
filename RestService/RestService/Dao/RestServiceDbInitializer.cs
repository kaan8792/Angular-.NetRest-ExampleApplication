using RestService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestService.Dao
{
    public class RestServiceDbInitializer : DropCreateDatabaseAlways<RestContext>
    {
        protected override void Seed(RestContext context)
        {
            IList<Fatura> faturas = new List<Fatura>();
            Fatura fatura1 = new Fatura { FaturaID = 1, Musteri = "Mehmet" };
            Fatura fatura2 = new Fatura { FaturaID = 2, Musteri = "Ahmet" };
            Fatura fatura3 = new Fatura { FaturaID = 3, Musteri = "Kemal" };
            faturas.Add(fatura1);
            faturas.Add(fatura2);
            faturas.Add(fatura3);


            IList<FaturaDetay> faturaDetays = new List<FaturaDetay>();
            faturaDetays.Add(new FaturaDetay { FaturaID = 1, BirimFiyat = 12, Miktar = 10, Aciklama = "Muz" });
            faturaDetays.Add(new FaturaDetay { FaturaID = 1, BirimFiyat = 6, Miktar = 12, Aciklama = "Domates" });
            faturaDetays.Add(new FaturaDetay { FaturaID = 1, BirimFiyat = 4, Miktar = 20, Aciklama = "Patates" });


            faturaDetays.Add(new FaturaDetay { FaturaID = 2, BirimFiyat = 12, Miktar = 5, Aciklama = "Muz" });
            faturaDetays.Add(new FaturaDetay { FaturaID = 2, BirimFiyat = 12, Miktar = 8, Aciklama = "Kabak" });
            faturaDetays.Add(new FaturaDetay { FaturaID = 2, BirimFiyat = 4, Miktar = 4, Aciklama = "Soğan" });


            faturaDetays.Add(new FaturaDetay { FaturaID = 3, BirimFiyat = 8, Miktar = 6, Aciklama = "Portakal" });
            faturaDetays.Add(new FaturaDetay { FaturaID = 3, BirimFiyat = 3, Miktar = 10, Aciklama = "Ispanak" });
            faturaDetays.Add(new FaturaDetay { FaturaID = 3, BirimFiyat = 5, Miktar = 3, Aciklama = "Maydanoz" });

            context.Faturas.AddRange(faturas);
            context.FaturaDetays.AddRange(faturaDetays);

            base.Seed(context);
        }
    }
}