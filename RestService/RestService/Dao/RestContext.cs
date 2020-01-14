using RestService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestService.Dao
{
    public class RestContext : DbContext
    {
        public RestContext(): base("name=RestServiceDBConnectionString")
        {
            Database.SetInitializer<RestContext>(null);
            //Database.SetInitializer<RestContext>(new RestServiceDbInitializer());
        }


        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<FaturaDetay> FaturaDetays { get; set; }
    }
}