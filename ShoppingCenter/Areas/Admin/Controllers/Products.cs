using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Models.ViewModels;

namespace ShoppingCenter.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Products : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public CategoryVM categoryVM { get; set; }

        public Products(IUnitofWork unitofWork, IWebHostEnvironment hostEnvironment)
        {
            _unitofWork = unitofWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            categoryVM = new CategoryVM()
            {
                Products = new Models.Products(),
                CategoryList = _unitofWork.Category.GetCategoryListForDropDown(),
                ProductTypeList = _unitofWork.ProductType.GetProductTypeListForDropDown(),
                ProductSizeList = _unitofWork.ProductSize.GetProductSizeForDropDownList()
            };

            if(id != null)
            {
                categoryVM.Products = _unitofWork.Products.Get(id.GetValueOrDefault());
            }

            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if(categoryVM.Products.Id == 0)
                {
                    //New Product
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\Products\");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    categoryVM.Products.ImageURL = @"\Images\Products\" + fileName + extension;

                    _unitofWork.Products.Add(categoryVM.Products);
                }
                else
                {
                    //Edit Product
                    var pFromDb = _unitofWork.Products.Get(categoryVM.Products.Id);

                    if(files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"\Images\Services\");
                        var extension_new = Path.Combine(files[0].FileName);

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }

                        categoryVM.Products.ImageURL = @"\Images\Products\" + fileName + extension_new;
                    }
                    else
                    {
                        categoryVM.Products.ImageURL = pFromDb.ImageURL;
                    }

                    _unitofWork.Products.Update(categoryVM.Products);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                categoryVM.CategoryList = _unitofWork.Category.GetCategoryListForDropDown();
                categoryVM.ProductTypeList = _unitofWork.ProductType.GetProductTypeListForDropDown();
                categoryVM.ProductSizeList = _unitofWork.ProductSize.GetProductSizeForDropDownList();

                return View(categoryVM);
            }
        }

        public IActionResult Detail(int id)
        {
            var pFromDb = _unitofWork.Products.GetFirstOrDefault(includeProperties: "Category,ProductType,ProductSize", filter: p => p.Id == id);

            return View(pFromDb);
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Products.GetAll(includeProperties: "Category,ProductType") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var pFromDb = _unitofWork.Products.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, pFromDb.ImageURL.TrimStart('\\'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if(pFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Product!" });
            }

            _unitofWork.Products.Remove(pFromDb);

            _unitofWork.Save();

            return Json(new { success = true, message = "Product Deleted Successfully!" });
        }

        #endregion
    }
}
