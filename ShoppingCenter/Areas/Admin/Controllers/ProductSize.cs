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
    public class ProductSize : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public CategoryVM categoryVM { get; set; }

        public ProductSize(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            categoryVM = new CategoryVM() { ProductSize = new Models.ProductSize() };

            if(id != null)
            {
                categoryVM.ProductSize = _unitofWork.ProductSize.Get(id.GetValueOrDefault());
            }

            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if(categoryVM.ProductSize.Id == 0)
                {
                    _unitofWork.ProductSize.Add(categoryVM.ProductSize);
                }
                else
                {
                    _unitofWork.ProductSize.update(categoryVM.ProductSize);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(categoryVM);
            }
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.ProductSize.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sizeFromDb = _unitofWork.ProductSize.Get(id);

            if(sizeFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Product Size!" });
            }

            _unitofWork.ProductSize.Remove(sizeFromDb);

            _unitofWork.Save();

            return Json(new { success = true, message = "Product Size Deleted Successfully!" });
        }

        #endregion
    }
}
