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

        [HttpGet("{Search?}")]
        public async Task<IActionResult> Index(string? search)
        {
            StoreSearchViewModel vm = new StoreSearchViewModel
            {
                Products = search != null && search != string.Empty ? await apiService.FindAllByNameOrId(search) : await apiService.GetAllProducts(),
                //Products = search != null && search != string.Empty ? await apiService.FindAllByNameOrId(search) : null,
                Search = search
            };

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
            ConfirmViewModel vm = new ConfirmViewModel
            {
                OrderLines = orderLines
            };

            ViewBag.Zones = await apiService.GetAllZones();

            return View(vm);
        }

        [HttpPost("ChangeQuantity")]
        public IActionResult ChangeQuantity(int id, int qty)
        {
            List<OrderLine>? orderLines = session.Get<List<OrderLine>>("Order");
            orderLines.Find(c=>c.ProductId == id).Quantity = qty;
            session.Set<List<OrderLine>>("Order", orderLines);

            return RedirectToAction("Cart");
        }

        [HttpGet("ClearOrderLine/{line}")]
        public IActionResult ClearOrderLine(int line)
        {
            List<OrderLine>? orderLines = session.Get<List<OrderLine>>("Order");
            orderLines.RemoveAt(line);
            session.Set<List<OrderLine>>("Order", orderLines);

            return RedirectToAction("Cart");
        }

        [HttpPost("Confirm")]
        public IActionResult Confirm(ConfirmViewModel vm)
        {
            return RedirectToAction("Index");
        }

    }
}
