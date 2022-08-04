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
    public class HistorialsController : ApiController
    {
        private controlriegoBDEntities db = new controlriegoBDEntities();

        // GET: api/Historials
        public IQueryable<Historial> GetHistorial()
        {
            return db.Historial;
        }

        // GET: api/Historials/5
        [ResponseType(typeof(Historial))]
        public async Task<IHttpActionResult> GetHistorial(int id)
        {
            Historial historial = await db.Historial.FindAsync(id);
            if (historial == null)
            {
                return NotFound();
            }

            return Ok(historial);
        }

        // PUT: api/Historials/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHistorial(int id, Historial historial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != historial.id)
            {
                return BadRequest();
            }

            db.Entry(historial).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialExists(id))
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

        // POST: api/Historials
        [ResponseType(typeof(Historial))]
        public async Task<IHttpActionResult> PostHistorial(Historial historial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Historial.Add(historial);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = historial.id }, historial);
        }

        // DELETE: api/Historials/5
        [ResponseType(typeof(Historial))]
        public async Task<IHttpActionResult> DeleteHistorial(int id)
        {
            Historial historial = await db.Historial.FindAsync(id);
            if (historial == null)
            {
                return NotFound();
            }

            db.Historial.Remove(historial);
            await db.SaveChangesAsync();

            return Ok(historial);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HistorialExists(int id)
        {
            return db.Historial.Count(e => e.id == id) > 0;
        }
    }
}