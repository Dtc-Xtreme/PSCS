using Microsoft.AspNetCore.Mvc;
using PSCS.AppLogic.Services;
using PSCS.OrderingSystem.Models;

namespace PSCS.OrderingSystem.Controllers
{
    [Route("[controller]")]
    public class StoreController : BaseController
    {
        private readonly IApiService apiService;

        public StoreController(IHttpContextAccessor httpContextAccessor, IApiService api) : base(httpContextAccessor)
        {
            this.apiService = api;
        }

        [HttpGet]
        [HttpGet("{Search?}")]
        public async Task<IActionResult> Index(string? search)
        {
            StoreSearchViewModel vm = new StoreSearchViewModel
            {
                Products = await apiService.GetAllProducts(),
                Search = search
            };

            if (!string.IsNullOrEmpty(search)) vm.Products = vm.Products?.Where(c => c.Name.ToLower().Contains(search.ToLower()) || c.Id.ToString().ToLower().Contains(search.ToLower())).ToList();

            return View(vm);
        }

        [HttpGet("AddToCart/{item}/{amount}")]
        public IActionResult AddToCart(int item, int amount)
        {
            List<OrderLine>? orderLines = session.Get<List<OrderLine>>("Order");
            if (orderLines == null) orderLines = new List<OrderLine>();
            orderLines.Add(new OrderLine { ProductId = item, Amount = amount });
            orderLines = orderLines.GroupBy(o => o.ProductId)
            .Select(g => new OrderLine
            {
                ProductId = g.Key,
                Amount = g.Sum(o => o.Amount)
            })
            .ToList();
            session.Set<List<OrderLine>>("Order", orderLines);
            return RedirectToAction("Index");
        }

        [HttpGet("ClearCart")]
        public IActionResult ClearCart()
        {
            session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet("Cart")]
        public IActionResult Cart() {
            List<OrderLine>? orderLines = session.Get<List<OrderLine>>("Order");
            return View(orderLines);
        }
    }
}
