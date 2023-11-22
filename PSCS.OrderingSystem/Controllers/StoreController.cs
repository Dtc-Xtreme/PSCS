using Microsoft.AspNetCore.Mvc;
using PSCS.AppLogic.Services;
using PSCS.Domain;
using PSCS.OrderingSystem.Models;
using OrderLine = PSCS.Domain.OrderLine;

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

        [HttpPost("AddToCart")]
        public IActionResult AddToCart(int id, int qty)
        {
            List<OrderLine>? orderLines = session.Get<List<OrderLine>>("Order");
            if (orderLines == null) orderLines = new List<OrderLine>();
            orderLines.Add(new OrderLine { ProductId = id, Quantity = qty });
            orderLines = orderLines.GroupBy(o => o.ProductId)
            .Select(g => new OrderLine
            {
                ProductId = g.Key,
                Quantity = g.Sum(o => o.Quantity)
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
        public async Task<IActionResult> Cart() {
            List<OrderLine>? orderLines = session.Get<List<OrderLine>>("Order");
            if (orderLines != null)
            {
                foreach (OrderLine line in orderLines)
                {
                    line.Product = await apiService.FindProductById(line.ProductId);
                }
            }
            return View(orderLines);
        }

        [HttpPost("ChangeQuantity")]
        public IActionResult ChangeQuantity(int id, int qty)
        {
            List<OrderLine>? orderLines = session.Get<List<OrderLine>>("Order");
            orderLines.Find(c=>c.ProductId == id).Quantity = qty;
            session.Set<List<OrderLine>>("Order", orderLines);

            return RedirectToAction("Cart");
        }

    }
}
