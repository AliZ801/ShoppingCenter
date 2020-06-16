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
    public class Women : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public CategoryVM categoryVM { get; set; }

        public Women(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "ProductType,ProductSize")
            };

            return View(categoryVM);
        }

        public IActionResult Detail(int id)
        {
            categoryVM = new CategoryVM()
            {
                Products = _unitofWork.Products.GetFirstOrDefault(includeProperties: "ProductType,ProductSize", filter: p => p.Id == id),
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "ProductType,ProductSize")
            };

            return View(categoryVM);
        }
    }
}
