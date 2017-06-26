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
using WebApi.Models;

namespace WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private WebApiContext db = new WebApiContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users.OrderByDescending(c => (c.Wins - c.Losses));
        }

        // GET: api/Users/5
        [HttpGet]
        [Route("api/Users/{name}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string name)
        {
            User user = db.Users.Find(name);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult Connect(string name, string pass)
        {
            if (UserExists(name))
            {
                return CreatedAtRoute("DefaultApi", new { id = name }, name);
            }
            return CreatedAtRoute("DefaultApi", null, "not exists");
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!UserExists(user.Name))
            {
                db.Users.Add(user);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = user.Name }, user);
            }
            return CreatedAtRoute("DefaultApi", null, "exists");
        }


        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(string name)
        {
            User user = await db.Users.FindAsync(name);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

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

        private bool UserExists(string name)
        {
            return db.Users.Count(e => e.Name == name) > 0;
        }
    }
}