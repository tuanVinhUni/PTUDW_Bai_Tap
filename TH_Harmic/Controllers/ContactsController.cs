using Microsoft.AspNetCore.Mvc;

namespace TH_Harmic.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostContact()
        {

            return Ok();
        }
    }
}
