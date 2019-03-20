using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiNeumaticos.Models;

namespace ApiNeumaticos.Controllers
{
    //[Route("gpschile/catalogo/neumatico/{controller}")]
    public class MarcaNeumaticoController : ApiController
    {
        private Model db = new Model();

        // GET: api/MarcaNeumatico
        public IQueryable<PAR_MarcaNeumatico> GetPAR_MarcaNeumatico()
        {
            return db.PAR_MarcaNeumatico;
        }

        // GET: api/MarcaNeumatico/5
        [ResponseType(typeof(PAR_MarcaNeumatico))]
        public IHttpActionResult GetPAR_MarcaNeumatico(string id)
        {
            PAR_MarcaNeumatico pAR_MarcaNeumatico = db.PAR_MarcaNeumatico.Find(id);
            if (pAR_MarcaNeumatico == null)
            {
                return NotFound();
            }

            return Ok(pAR_MarcaNeumatico);
        }

        // PUT: api/MarcaNeumatico/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPAR_MarcaNeumatico(string id, PAR_MarcaNeumatico pAR_MarcaNeumatico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pAR_MarcaNeumatico.Id_MarcaNeumatico)
            {
                return BadRequest();
            }

            db.Entry(pAR_MarcaNeumatico).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PAR_MarcaNeumaticoExists(id))
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

        // POST: api/MarcaNeumatico
        [ResponseType(typeof(PAR_MarcaNeumatico))]
        public IHttpActionResult PostPAR_MarcaNeumatico(PAR_MarcaNeumatico pAR_MarcaNeumatico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PAR_MarcaNeumatico.Add(pAR_MarcaNeumatico);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PAR_MarcaNeumaticoExists(pAR_MarcaNeumatico.Id_MarcaNeumatico))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pAR_MarcaNeumatico.Id_MarcaNeumatico }, pAR_MarcaNeumatico);
        }

        // DELETE: api/MarcaNeumatico/5
        [ResponseType(typeof(PAR_MarcaNeumatico))]
        public IHttpActionResult DeletePAR_MarcaNeumatico(string id)
        {
            PAR_MarcaNeumatico pAR_MarcaNeumatico = db.PAR_MarcaNeumatico.Find(id);
            if (pAR_MarcaNeumatico == null)
            {
                return NotFound();
            }

            db.PAR_MarcaNeumatico.Remove(pAR_MarcaNeumatico);
            db.SaveChanges();

            return Ok(pAR_MarcaNeumatico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PAR_MarcaNeumaticoExists(string id)
        {
            return db.PAR_MarcaNeumatico.Count(e => e.Id_MarcaNeumatico == id) > 0;
        }
    }
}