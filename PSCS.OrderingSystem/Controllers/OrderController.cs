using Microsoft.AspNetCore.Mvc;
using PSCS.AppLogic.Services;
using PSCS.Domain;
using PSCS.OrderingSystem.Models;

namespace PSCS.OrderingSystem.Controllers
{
    [Route("[controller]")]
    public class OrderController : BaseController
    {
        private readonly IApiService apiService;

        public OrderController(IHttpContextAccessor httpContextAccessor, IApiService api) : base(httpContextAccessor)
        {
            this.apiService = api;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IList<Order>? orders = await apiService.GetAllOrders();

            OrderViewModel vm = new OrderViewModel
            {
                Orders = orders
            };

            return View(vm);
        }
    }
}
