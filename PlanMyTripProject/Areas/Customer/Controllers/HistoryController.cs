using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyAppModels.ViewModels;
using System.Security.Claims;

namespace PlanMyTripProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class HistoryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public HistoryVM historyList { get; set; }
        public HistoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM itemList = new CartVM()
            {
                ListOfCart = _unitOfWork.Cart.GetAll(x => x.ApplicationUserId == claims.Value, includeProperties: "Hotel")
            };

            foreach (var item in itemList.ListOfCart)
            {
                itemList.Total += (item.Hotel.Price * item.Count);
            }
            return View(itemList);
        }
        public IActionResult delete(int id)
        {
            var cart = _unitOfWork.Cart.GetT(x => x.Id == id);
            _unitOfWork.Cart.Delete(cart);
            _unitOfWork.Save();
            TempData["success"] = "Hotel History deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
