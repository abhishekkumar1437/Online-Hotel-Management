
using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyAppModels;
using PlanMyTripProject.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PlanMyTripProject.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
       // private ApplicationDbContext context;
        
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index()
        {
            IEnumerable<Hotel> hotels = _unitOfWork.Hotel.GetAll(includeProperties: "Category");

            return View(hotels);
        }

        [HttpGet]
        public IActionResult Search(string locSearch)
        {
            if (locSearch == null)
            {
                TempData["error"] = "Please enter valid Location";
               return RedirectToAction("Index");
            }
            else
            {
                var hotels = _unitOfWork.Hotel.GetAll(includeProperties: "Category");
                return View(hotels.Where(x => x.Location.Contains(locSearch)));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int? HotelId)
        {
            Cart cart = new Cart()
            {
                Hotel = _unitOfWork.Hotel.GetT(x => x.Id == HotelId, includeProperties: "Category"),
                Count = 1,
                HotelId = (int)HotelId
            };
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Details(Cart cart)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            cart.ApplicationUserId = claims.Value;
            var cartItem=_unitOfWork.Cart.GetT(x => x.HotelId == cart.HotelId && x.ApplicationUserId==claims.Value);

                if (cartItem == null)
                {
                    _unitOfWork.Cart.Add(cart);
                    TempData["success"] = "Item added to cart";
                }
                else
                {
                   _unitOfWork.Cart.IncrementCartItem(cartItem,cart.Count);
                }
                _unitOfWork.Save();

                
            }
            return RedirectToAction("Index");
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult BookHotel()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}