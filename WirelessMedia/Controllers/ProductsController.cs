using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WirelessMedia.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) => _mediator = mediator;

            public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Store()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "";
                return RedirectToAction(nameof(Create));
            }

            // _mediator.Send(new CreateProductCommand{});
            // execute command
            return View();
        }
    }
}