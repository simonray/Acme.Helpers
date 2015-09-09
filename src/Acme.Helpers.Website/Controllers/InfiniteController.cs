using Microsoft.AspNet.Mvc;
using Microsoft.Framework.OptionsModel;

namespace Acme.Helpers.Website.Controllers
{
    public class InfiniteController : BaseController
    {
        public InfiniteController(IOptions<AppSettings> settings)
            : base(settings) { }

        public IActionResult Index()
            => View();

        public IActionResult Basic(int? skip = 0)
            => GetMoreView(skip, "_Basic");

        public IActionResult Animated(int? skip = 0)
            => GetMoreView(skip, "_Animated");
    }
}
