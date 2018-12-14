using ASD.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace ASD.WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private User[] users = new User[]
        {
            new User { Id = 1, name = "Haleemah Redfern", email = "email1@mail.com", phone = "01111111", role = 1},
            new User { Id = 2, name = "Aya Bostock", email = "email2@mail.com", phone = "01111111", role = 1},
            new User { Id = 3, name = "Sohail Perez", email = "email3@mail.com", phone = "01111111", role = 1},
            new User { Id = 4, name = "Merryn Peck", email = "email4@mail.com", phone = "01111111", role = 2},
            new User { Id = 5, name = "Cairon Reynolds", email = "email5@mail.com", phone = "01111111", role = 3}
        };

        // GET: api/Users
        [ResponseType(typeof(IEnumerable<User>))]
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET: api/Users/5
        public IHttpActionResult Get(int id)
        {
            var user = users.FirstOrDefault((x) => x.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/Users
        public void Post([FromBody]User user)
        {
            //users.Add(user);
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
