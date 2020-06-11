using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Models.ViewModels;

namespace ShoppingCenter.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Category : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public CategoryVM categoryVM { get; set; }

        public Category(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            categoryVM = new CategoryVM() { Category = new Models.Category() };

            if(id != null)
            {
                categoryVM.Category = _unitofWork.Category.Get(id.GetValueOrDefault());
            }

            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if(categoryVM.Category.Id == 0)
                {
                    //Create Category
                    _unitofWork.Category.Add(categoryVM.Category);
                }
                else
                {
                    //Update Category
                    _unitofWork.Category.Update(categoryVM.Category);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(categoryVM);
            }
        }

        #region

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Category.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var catFromDb = _unitofWork.Category.Get(id);

            if(catFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting Category!" });
            }

            _unitofWork.Category.Remove(catFromDb);

            _unitofWork.Save();

            return Json(new { success = true, message = "Category Deleted Successfully!" });
        }

        #endregion
    }
}
