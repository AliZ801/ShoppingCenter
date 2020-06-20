using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Extensions;
using ShoppingCenter.Models;
using ShoppingCenter.Models.ViewModels;
using ShoppingCenter.Utility;

namespace ShoppingCenter.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class Cart : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public CartVM cartVM { get; set; }

        public Cart(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;

            cartVM = new CartVM()
            {
                ProductsList = new List<Products>(),
                OrderHeader = new Models.OrderHeader()
            };
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);

                foreach(int ProductId in sessionList)
                {
                    cartVM.ProductsList.Add(_unitofWork.Products.GetFirstOrDefault(u => u.Id == ProductId, includeProperties: "Category,ProductType,ProductSize"));
                }
            }

            return View(cartVM);
        }

        public IActionResult Remove(int ProductId)
        {
            List<int> sessionList = new List<int>();
            sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            sessionList.Remove(ProductId);
            HttpContext.Session.SetObject(SD.SessionCart, sessionList);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);

                foreach (int ProductId in sessionList)
                {
                    cartVM.ProductsList.Add(_unitofWork.Products.GetFirstOrDefault(u => u.Id == ProductId, includeProperties: "Category,ProductType,ProductSize"));
                }
            }

            return View(cartVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPOST() 
        { 
            if(HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                cartVM.ProductsList = new List<Products>();

                foreach(int productId in sessionList)
                {
                    cartVM.ProductsList.Add(_unitofWork.Products.Get(productId));
                }
            }

            if (!ModelState.IsValid)
            {
                return View(cartVM);
            }
            else
            {
                cartVM.OrderHeader.OrderDate = DateTime.Now;
                cartVM.OrderHeader.Status = SD.OrderSubmitted;
                cartVM.OrderHeader.ServiceCount = cartVM.ProductsList.Count;

                _unitofWork.OrderHeader.Add(cartVM.OrderHeader);
                _unitofWork.Save();

                foreach(var item in cartVM.ProductsList)
                {
                    OrderDetails orderDetails = new OrderDetails
                    {
                        ProductId = item.Id,
                        OrderHeaderId = cartVM.OrderHeader.Id,
                        ProductName = item.Name,
                        Price = item.Price
                    };

                    _unitofWork.OrderDetails.Add(orderDetails);
                }

                _unitofWork.Save();

                HttpContext.Session.SetObject(SD.SessionCart, new List<int>());

                return RedirectToAction("OrderConfirmation", "Cart", new { id = cartVM.OrderHeader.Id });
            }
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
    }
}
