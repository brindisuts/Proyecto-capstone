using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using webAPI;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class bombasController : ApiController
    {
        private controlriegoBDEntities db = new controlriegoBDEntities();

        // GET: api/bombas
        public IQueryable<bomba> Getbomba()
        {
            return db.bomba;
        }

        // GET: api/bombas/5
        [ResponseType(typeof(bomba))]
        public async Task<IHttpActionResult> Getbomba(int id)
        {
            bomba bomba = await db.bomba.FindAsync(id);
            if (bomba == null)
            {
                return NotFound();
            }

            return Ok(bomba);
        }

        // PUT: api/bombas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putbomba(int id, bomba bomba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bomba.id)
            {
                return BadRequest();
            }

            db.Entry(bomba).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bombaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/bombas
        [ResponseType(typeof(bomba))]
        public async Task<IHttpActionResult> Postbomba(bomba bomba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bomba b = db.bomba.Find(bomba.id);
            b.status = bomba.status;

            //db.bomba.Add(bomba);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (bombaExists(bomba.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bomba.id }, bomba);
        }

        // DELETE: api/bombas/5
        [ResponseType(typeof(bomba))]
        public async Task<IHttpActionResult> Deletebomba(int id)
        {
            bomba bomba = await db.bomba.FindAsync(id);
            if (bomba == null)
            {
                return NotFound();
            }

            db.bomba.Remove(bomba);
            await db.SaveChangesAsync();

            return Ok(bomba);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bombaExists(int id)
        {
            return db.bomba.Count(e => e.id == id) > 0;
        }
    }
}