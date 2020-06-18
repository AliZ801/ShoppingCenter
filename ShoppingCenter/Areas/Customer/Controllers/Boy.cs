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
    public class Boy : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public CategoryVM categoryVM { get; set; }

        public Boy(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "ProductType,ProductSize", filter: p => p.Category.CatName == "Boy")
            };

            return View(categoryVM);
        }

        public IActionResult Detail(int? id)
        {
            int count = 5;

            categoryVM = new CategoryVM()
            {
                Products = _unitofWork.Products.Get(id.GetValueOrDefault()),
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "Category,PropertyType,PropertySize", filter: p => p.Category.CatName == "Boy").Take(count).ToList()
            };

            return View(categoryVM);
        }

        public IActionResult TShirts()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "Category,PropertySize,PropertyType", filter: p => p.ProductType.Type == "T-Shirt" && p.Category.CatName == "Boy")
            };

            return View(categoryVM);
        }

        public IActionResult Shirts()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "Category,PropertySize,PropertyType", filter: p => p.ProductType.Type == "Shirt" && p.Category.CatName == "Boy")
            };

            return View(categoryVM);
        }

        public IActionResult Pants()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "Category,PropertySize,PropertyType", filter: p => p.ProductType.Type == "Pants" && p.Category.CatName == "Boy")
            };

            return View(categoryVM);
        }

        public IActionResult Shoes()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "Category,PropertySize,PropertyType", filter: p => p.ProductType.Type == "Shoes" && p.Category.CatName == "Boy")
            };

            return View(categoryVM);
        }

        public IActionResult Assessories()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "Category,PropertySize,PropertyType", filter: p => p.ProductType.Type == "Assessories" && p.Category.CatName == "Boy")
            };

            return View(categoryVM);
        }
    }
}
