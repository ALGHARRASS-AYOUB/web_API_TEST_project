using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExerciceWebAPI.Controllers
{
    public class EtudiantController : ApiController
    {
        Entities db = new Entities();
        public EtudiantController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/Etudiant/Getetudiants")]
        public IEnumerable<Etudiant> Getetudiants()
        {
            var students = from s in db.Etudiant select s;
          
           return students;
        }
        [Route("api/Etudiant/Getetudiant/{ID}")]
        public IHttpActionResult Getetudiant(int Id)
        {
            Etudiant etudiant = db.Etudiant.Find(Id);
            if (etudiant == null)
            {
                return NotFound();
            }
            return Ok(etudiant);
        }
        [Route("api/Etudiant/Postetudiant")]
        public IHttpActionResult Postetudiant(Etudiant etudiant)
        {
            Etudiant etudiant1 = db.Etudiant.Find(etudiant.Id);
            if (etudiant == null || etudiant1 != null)
            {
                return BadRequest("Invalid passed data");
            }
            db.Etudiant.Add(etudiant);
            db.SaveChanges();
            return Ok();
        }





        [Route("api/Etudiant/PutEtudiant")]
        public IHttpActionResult PutEtudiant(int id, Etudiant etudiant)
        {
            try
            {
                var existingEtudiant = db.Etudiant.FirstOrDefault(e => e.Id == id);
                if (existingEtudiant == null)
                { return NotFound(); }
                else
                {
                    existingEtudiant.firstName = etudiant.firstName;
                    existingEtudiant.lastName = etudiant.lastName;
                    existingEtudiant.filiere_id = etudiant.filiere_id;
                    db.SaveChanges();
                    return Ok(existingEtudiant);
                }
            }
            catch (Exception e)
            { return BadRequest(); }
        }


        [Route("api/Etudiant/PutEtudiant")]
        public HttpResponseMessage PutEtudiant(Etudiant etudiant)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not a valid model");
            }
            var existingEtudiant = db.Etudiant.Where(e => e.Id == etudiant.Id).FirstOrDefault<Etudiant>();
            if (existingEtudiant != null)
            {
                existingEtudiant.firstName = etudiant.firstName;
                existingEtudiant.lastName = etudiant.lastName;
                existingEtudiant.filiere_id = etudiant.filiere_id;
                db.SaveChanges();
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Etudiant modifié");
        }







        [Route("api/Etudiant/Deleteetudiant")]
        public IHttpActionResult Deleteetudiant(int id)
        {
            if (id <= 0)
            {
                return BadRequest("invalid id");
            }
            var stud = db.Etudiant.Where(e => e.Id == id).FirstOrDefault();
            db.Entry(stud).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }
    }

}