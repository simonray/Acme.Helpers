using Microsoft.AspNet.Mvc;

namespace Acme.Helpers.Website.Controllers
{
    public class ConfigController : Controller
    {
        public IActionResult Index()
            => View();

        public IActionResult Demo()
            => View();

        public IActionResult Pager()
            => View();

        public IActionResult Table()
            => View();
    }
}
