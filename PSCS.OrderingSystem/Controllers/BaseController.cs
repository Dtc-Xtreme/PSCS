using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PSCS.AppLogic.Services;
using PSCS.OrderingSystem.Models;

namespace PSCS.OrderingSystem.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ISession session;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            this.session = httpContextAccessor.HttpContext.Session;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            List<OrderLine>? orderLines = session.Get<List<OrderLine>>("Order");
            ViewBag.CartAmount = orderLines?.Count();
        }
    }
}
