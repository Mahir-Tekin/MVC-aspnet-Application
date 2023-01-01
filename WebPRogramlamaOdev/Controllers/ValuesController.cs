using Microsoft.AspNetCore.Mvc;
using WebPRogramlamaOdev.Data;
using WebPRogramlamaOdev.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebPRogramlamaOdev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private AppDbContext db;
        public ValuesController(AppDbContext db)
        {
            this.db = db;
        }

        public List<Animal> Animals = new List<Animal>();
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Animal> Get()
        {
            var Animalx = db.Animals.ToList();
            return Animalx;   
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IEnumerable<Animal> Get(int id)
        {
            var Animaly = db.Animals.Where(x => x.Id == id).ToList();
            return Animaly;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
