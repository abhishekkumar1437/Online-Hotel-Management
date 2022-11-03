using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyAppModels.Models;
using MyAppModels.ViewModels;

namespace PlanMyTripProject.Controllers
{
    [Area("Admin")]
   [Authorize]
    public class CategoryController : Controller
    {
       // private ApplicationDbContext _context;
        private IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
              _unitOfWork = unitOfWork;
        }
        //View  category
        public IActionResult Category()
        {
            CategoryVM categoryVM = new CategoryVM();   

            categoryVM.Categories = _unitOfWork.Category.GetAll();
            return View(categoryVM);
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
            CategoryVM vm = new CategoryVM();
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                 vm.Category = _unitOfWork.Category.GetT(x => x.Id == id);
                if (vm.Category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
            }
        }
        // Edit Category POST Method i.e for update after edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CategoryVM vm)
        {
            //Server side validation for empty field
            if (ModelState.IsValid)
            {
                if (vm.Category.Id == 0)
                {
                    _unitOfWork.Category.Add(vm.Category);
                    TempData["success"] = "Category Created Successfully";
                }
                else
                {
                    _unitOfWork.Category.Update(vm.Category);
                    TempData["success"] = "Category Updated Successfully";
                }
                _unitOfWork.Save();
              
                return RedirectToAction("Category");
            }
            return View(vm);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
             _unitOfWork.Category.Delete(category);
            _unitOfWork.Save();
            return RedirectToAction("Category");
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)
        {
            //Server side validation for empty field
            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (category==null)
            {
                return NotFound();
            }
          _unitOfWork.Category.Delete(category);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Category");
        }
    }
}
