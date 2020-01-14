using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using RestService.Dao;
using RestService.Models;

namespace RestService.Controllers
{
    [EnableCors(origins: "http://localhost:8080", headers:"*", methods:"*")]
    public class FaturaController : ApiController
    {
        private RestContext db = new RestContext();

        // GET: api/Fatura
        public List<Fatura> GetFaturas()
        {
            return db.Faturas.ToList();
        }

        // POST: api/Fatura
        [ResponseType(typeof(Fatura))]
        public IHttpActionResult PostFatura(Fatura fatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Faturas.Add(fatura);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fatura.FaturaID }, fatura);
        }

        // GET: api/Fatura/{id}
        //[ResponseType(typeof(Fatura))]
        public IHttpActionResult GetFatura(int id)
        {
            Fatura fatura = db.Faturas.Find(id);
            if (fatura == null)
            {
                return NotFound();
            }

            return Ok(fatura);
        }

        // POST: api/Fatura/{id}
        [HttpPost]
        public IHttpActionResult PostFaturaDetails(int id, Fatura fatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (id != fatura.FaturaID )
            {
                return BadRequest();
            }

            var curFatura = db.Faturas.FirstOrDefault(x => x.FaturaID == id);
            var silinecekler = new List<FaturaDetay>();
            foreach (var fd in curFatura.FaturaDetays)
            {
                if(fatura.FaturaDetays.FirstOrDefault(x=>x.FaturaDetayID == fd.FaturaDetayID) == null)
                {
                    silinecekler.Add(fd);
                }
            }

            foreach (var fd in silinecekler)
            {
                db.FaturaDetays.Remove(fd);
                db.SaveChanges();
            }

            foreach (var fd in fatura.FaturaDetays)
            {
                if(fd.FaturaDetayID == 0)
                {
                    db.FaturaDetays.Add(fd);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        fd.LastModifiedTime = DateTime.Now;
                        db.FaturaDetays.Attach(fd);
                        db.Entry(fd).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch{}
                }
            }

            try
            {
                fatura.LastModifiedTime = DateTime.Now;
                db.Entry(fatura).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch{}
            return StatusCode(HttpStatusCode.OK);
        }

        // DELETE: api/Fatura/{id}
        public IHttpActionResult DeleteFatura(int id)
        {
            Fatura fatura = db.Faturas.Find(id);
            if (fatura == null)
            {
                return NotFound();
            }
            var list = db.FaturaDetays.Where(x => x.FaturaID == fatura.FaturaID).ToList();
            list.ForEach(x => db.FaturaDetays.Remove(x));
            db.Faturas.Remove(fatura);
            db.SaveChanges();

            return Ok("OK");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FaturaExists(int id)
        {
            return db.Faturas.Count(e => e.FaturaID == id) > 0;
        }
    }
}