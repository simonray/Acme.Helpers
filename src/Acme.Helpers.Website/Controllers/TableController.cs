using Microsoft.AspNet.Mvc;
using Microsoft.Framework.OptionsModel;
using System.Threading.Tasks;

namespace Acme.Helpers.Website.Controllers
{
    public class TableController : BaseController
    {
        public TableController(IOptions<AppSettings> settings)
            : base(settings) { }

        public IActionResult Index()
            => View();

        public IActionResult Basic(int page, int pageSize)
            => GetPagedView(page, pageSize);

        public async Task<IActionResult> Ajax(int page, int pageSize = 5)
            => await GetPagedViewAsync(page, pageSize, "_AjaxTable");

        public IActionResult Markup(int page, int pageSize)
            => GetPagedView(page, pageSize);
    }
}
