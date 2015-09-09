using Microsoft.AspNet.Mvc;
using Microsoft.Framework.OptionsModel;
using System.Threading.Tasks;

namespace Acme.Helpers.Website.Controllers
{
    public class PagerController : BaseController
    {
        public PagerController(IOptions<AppSettings> settings)
            : base(settings)
        { }

        public IActionResult Index()
            => View();

        public IActionResult Basic(int page, int? pageSize = 5)
            => GetPagedView(page, pageSize);

        public async Task<IActionResult> Ajax(int page, int pageSize = 5)
            => await GetPagedViewAsync(page, pageSize, "_AjaxTable");
    }
}
