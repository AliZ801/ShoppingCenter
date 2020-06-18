using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Models.ViewModels;

namespace ShoppingCenter.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class Girl : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public CategoryVM categoryVM { get; set; }

        public Girl(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "ProductType,ProductSize", filter: p => p.Category.CatName == "Girl")
            };

            return View(categoryVM);
        }

        public IActionResult Detail(int? id)
        {
            int count = 5;

            categoryVM = new CategoryVM()
            {
                Products = _unitofWork.Products.Get(id.GetValueOrDefault()),
                ProductsList = _unitofWork.Products.GetAll(filter: p => p.Category.CatName == "Girl").Take(count).ToList()
            };

            return View(categoryVM);
        }

        public IActionResult TShirts()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "ProductType,ProductSize", filter: p => p.Category.CatName == "Girl" && p.ProductType.Type == "T-Shirt")
            };

            return View(categoryVM);
        }

        public IActionResult Shirts()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "ProductType,ProductSize", filter: p => p.Category.CatName == "Girl" && p.ProductType.Type == "Shirt")
            };

            return View(categoryVM);
        }

        public IActionResult Pants()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "ProductType,ProductSize", filter: p => p.Category.CatName == "Girl" && p.ProductType.Type == "Pants")
            };

            return View(categoryVM);
        }

        public IActionResult Shoes()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "ProductType,ProductSize", filter: p => p.Category.CatName == "Girl" && p.ProductType.Type == "Shoes")
            };

            return View(categoryVM);
        }

        public IActionResult Assessories()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "ProductType,ProductSize", filter: p => p.Category.CatName == "Girl" && p.ProductType.Type == "Assessories")
            };

            return View(categoryVM);
        }
    }
}
