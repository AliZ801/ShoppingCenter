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
    public class ProductType : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public CategoryVM categoryVM { get; set; }

        public ProductType(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            categoryVM = new CategoryVM() 
            { 
                ProductType = new Models.ProductType()
            };

            if(id != null)
            {
                categoryVM.ProductType = _unitofWork.ProductType.Get(id.GetValueOrDefault());
            }

            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if(categoryVM.ProductType.Id == 0)
                {
                    _unitofWork.ProductType.Add(categoryVM.ProductType);
                }
                else
                {
                    _unitofWork.ProductType.Update(categoryVM.ProductType);
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
            return Json(new { data = _unitofWork.ProductType.GetAll(includeProperties:"Category") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var pTypeFromDb = _unitofWork.ProductType.Get(id);

            if(pTypeFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Property Type!" });
            }

            _unitofWork.ProductType.Remove(pTypeFromDb);

            _unitofWork.Save();

            return Json(new { success = true, message = "Property Type Deleted Successfully!" });
        }

        #endregion
    }
}
