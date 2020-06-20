using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Models.ViewModels;
using ShoppingCenter.Utility;

namespace ShoppingCenter.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class Order : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public OrdersVM ordersVM { get; set; }

        public Order(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            ordersVM = new OrdersVM()
            {
                OrderHeader = _unitofWork.OrderHeader.Get(id),
                OrderDetails = _unitofWork.OrderDetails.GetAll(filter: o => o.OrderHeaderId == id)
            };

            return View(ordersVM);
        }

        public IActionResult Approved(int id)
        {
            var orderFromDb = _unitofWork.OrderHeader.Get(id);

            if(orderFromDb == null)
            {
                return NotFound();
            }

            _unitofWork.OrderHeader.ChangeOrderStatus(id, SD.OrderApproved);

            return View(nameof(Index));
        }

        public IActionResult Completed(int id)
        {
            var orderFromDb = _unitofWork.OrderHeader.Get(id);

            if (orderFromDb == null)
            {
                return NotFound();
            }

            _unitofWork.OrderHeader.ChangeOrderStatus(id, SD.OrderCompleted);

            return View(nameof(Index));
        }

        public IActionResult Rejected(int id)
        {
            var orderFromDb = _unitofWork.OrderHeader.Get(id);

            if (orderFromDb == null)
            {
                return NotFound();
            }

            _unitofWork.OrderHeader.ChangeOrderStatus(id, SD.OrderRejected);

            return View(nameof(Index));
        }

        #region API CALLS

        public IActionResult GetAllOrders()
        {
            return Json(new { data = _unitofWork.OrderHeader.GetAll() });
        }

        public IActionResult GetAllPendingOrders()
        {
            return Json(new { data = _unitofWork.OrderHeader.GetAll(filter: o => o.Status == SD.OrderSubmitted) });
        }

        public IActionResult GetAllApprovedOrders()
        {
            return Json(new { data = _unitofWork.OrderHeader.GetAll(filter: o => o.Status == SD.OrderApproved) });
        }

        public IActionResult GetAllCompletedOrders()
        {
            return Json(new { data = _unitofWork.OrderHeader.GetAll(filter: o => o.Status == SD.OrderCompleted) });
        }

        #endregion
    }
}
