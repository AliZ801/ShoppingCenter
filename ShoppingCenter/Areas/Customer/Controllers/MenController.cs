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
    public class MenController : Controller
    {
        public readonly IUnitofWork _unitofWork;

        [BindProperty]
        public CategoryVM categoryVM { get; set; }

        public MenController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            categoryVM = new CategoryVM()
            {
                ProductsList = _unitofWork.Products.GetAll(includeProperties: "Category,ProductType,ProductSize")
            };

            return View(categoryVM);
        }

        public IActionResult Detail(int id)
        {
            var pFromDb = _unitofWork.Products.Get(id);

            return View(pFromDb);
        }
    }
}
