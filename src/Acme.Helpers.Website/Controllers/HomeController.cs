using Acme.Helpers.Website.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Acme.Helpers.Website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
            => View();

        public IActionResult DisplayBasicPerson(int id)
            => View(new BasicPersonView(SampleContext.People.First(p => p.Id == id)));

        public IActionResult Error()
            => View("~/Views/Shared/Error.cshtml");
    }
}
