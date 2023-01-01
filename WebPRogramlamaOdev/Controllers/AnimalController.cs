using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebPRogramlamaOdev.Data;

namespace WebPRogramlamaOdev.Controllers
{
    public class AnimalController : Controller
    {
        private AppDbContext _context;
        public AnimalController(AppDbContext context)
        {
            _context = context;

    
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Add(string Picture,string Name,string Story)
        {
            _context.Animals.Add(new Models.Animal() { Picture = Picture, Name = Name, Story = Story});

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Index() {

            var animals = _context.Animals.ToList();

            return View(animals);
        }


    }
}
