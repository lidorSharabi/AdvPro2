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
using AdvProg2WebApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace AdvProg2WebApp.Controllers
{
    public class UsersController : ApiController
    {
        private AdvProg2WebAppContext db = new AdvProg2WebAppContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/5&123
        //[ResponseType(typeof(User))]
        public IHttpActionResult GetConfrimUser(string id, string password)
        {
            User user = db.Users.Find(id);
            if (user == null || user.Password != ComputeHash(password))
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(User))]
        [Route("api/Users/Update")]
        public IHttpActionResult UpdateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserNameId))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            user.Password = ComputeHash(user.Password);
            db.Users.Add(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserNameId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.UserNameId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.UserNameId == id) > 0;
        }

        string ComputeHash(string input)
        {
            SHA1 sha = SHA1.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha.ComputeHash(buffer);
            string hash64 = Convert.ToBase64String(hash);
            return hash64;
        }

    }
}