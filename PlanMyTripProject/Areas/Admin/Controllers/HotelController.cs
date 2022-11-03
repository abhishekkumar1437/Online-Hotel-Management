using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyAppModels.ViewModels;

namespace PlanMyTripProject.Controllers
{
    [Area("Admin")]
   [Authorize]
    public class HotelController : Controller
    {
        // private ApplicationDbContext _context;
        private IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _hostingEnvironment;

        public HotelController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }
        //for datatable
        #region APICALL
        public IActionResult AllHotel()
        {
            var hotels = _unitOfWork.Hotel.GetAll( includeProperties:"Category");
            return Json(new {data=hotels});
        }
        #endregion
        //View  Hotels
        public IActionResult Hotels()
        {
            HotelVM hotelVM = new HotelVM();

            hotelVM.Hotels = _unitOfWork.Hotel.GetAll();
            return View(hotelVM);
        }
      

        //Create category
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category category)
        //{
        //    //Server side validation for empty field
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Category.Add(category);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Category Created Successfully";   // It show only one time on frontend
        //        return RedirectToAction("Category");
        //    }
        //    return View(category);
        //}



        //Edit category Get Method i.e to edit

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            HotelVM vm = new HotelVM()
            {
                Hotel = new(),
                Categories= _unitOfWork.Category.GetAll().Select(x=> 
                new SelectListItem()
                {
                    Text =x.Name,
                    Value= x.Id.ToString()
                })
            };
            
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Hotel = _unitOfWork.Hotel.GetT(x => x.Id == id);
                if (vm.Hotel == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
            }
        }
        // Edit hotle POST Method i.e for update after edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(HotelVM vm,IFormFile? file)
        {
            //Server side validation for empty field
            if (ModelState.IsValid)
            {
               string fileName=string.Empty;
                if (file!= null)
                {
                    string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "HotelImage");
                    fileName = Guid.NewGuid().ToString()+"-"+file.FileName;
                    string filePath= Path.Combine(uploadDir, fileName);
                    if (vm.Hotel.ImageUrl!=null)
                    {
                        var oldImagePath= Path.Combine(_hostingEnvironment.WebRootPath,vm.Hotel.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);

                        }
                    }
                   
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    vm.Hotel.ImageUrl = @"\HotelImage\" + fileName;
                }
                if (vm.Hotel.Id == 0)
                {
                    _unitOfWork.Hotel.Add(vm.Hotel);
                    TempData["success"] = "Hotel Added Successfully";
                }
                else
                {
                    _unitOfWork.Hotel.Update(vm.Hotel);
                    TempData["success"] = "Hotel Updated Successfully";
                }
                _unitOfWork.Save();

                return RedirectToAction("Hotels");
            }
            return View(vm);
        }


        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var category = _unitOfWork.Category.GetT(x => x.Id == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Category.Delete(category);
        //    _unitOfWork.Save();
        //    return RedirectToAction("Category");
        //}


        #region DeleteAPICALL

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            //Server side validation for empty field
            var hotel = _unitOfWork.Hotel.GetT(x => x.Id == id);
            if (hotel == null)
            {
                return Json(new { success = false, message = "Error in fetching data" });
            }
            else
            {
                var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, hotel.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);

                }
                _unitOfWork.Hotel.Delete(hotel);
                _unitOfWork.Save();
                return Json(new { success = false, message = "Hotel Deleted !! Please refresh it once" });
            }
        }
        #endregion
    }
}
