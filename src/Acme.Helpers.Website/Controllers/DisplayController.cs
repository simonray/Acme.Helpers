using Acme.Helpers.Website.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Acme.Helpers.Website.Controllers
{
    public class DisplayController : Controller
    {
        public IActionResult Index()
            => View();

        public IActionResult Basic()
            => View(SampleContext.People.Select(p => new DetailedPersonView(p)).First());
    }
}
