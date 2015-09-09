using Acme.Helpers.Core.Extensions;
using Acme.Helpers.Website.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.OptionsModel;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.Helpers.Website.Controllers
{
    public class BaseController : Controller
    {
        [FromServices]
        private IOptions<AppSettings> _settings { get; set; }

        public BaseController(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

        protected IActionResult GetPagedView(int? page, int? pageSize = null, string ajaxView = null)
        {
            var Page = (page != null && page >= 0 ? (int)page : 1);
            var PageSize = (pageSize != null && pageSize >= 1) ? (int)pageSize : _settings.Options.PagerPageSize;

            var Model = SampleContext.People
                .Select(p => new BasicPersonView(p))
                .ToPagedList(Page, PageSize);

            if (Request.IsAjaxRequest())
                return PartialView(ajaxView, Model);
            else
                return View(Model);
        }

        protected async Task<IActionResult> GetPagedViewAsync(int? page, int? pageSize = null, string ajaxView = null)
            => await Task.FromResult(GetPagedView(page, pageSize, ajaxView));

        protected IActionResult GetMoreView(int? skip = 0, string ajaxView = null)
        {
            var total = SampleContext.People.Take(10).Count();
            var model = SampleContext.People.Take(10)
                .Select(p => new BasicPersonView(p))
                .ToMoreList((int)skip, _settings.Options.InfinitePageSize);

            System.Threading.Thread.Sleep(1000);

            if (Request.IsAjaxRequest())
                return PartialView(ajaxView, model);
            else
                return View(model);
        }
    }
}
