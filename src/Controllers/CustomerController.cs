using System.Linq;
using frontend.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MyAppDbContext _context = null;

        public CustomersController(MyAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }
    }
}
