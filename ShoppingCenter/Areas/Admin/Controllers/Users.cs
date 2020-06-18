using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCenter.DataAccess.Data.Repository.IRepository;
using ShoppingCenter.Utility;

namespace ShoppingCenter.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Admin)]
    public class Users : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public Users(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View(_unitofWork.User.GetAll());
        }

        public IActionResult Lock(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            _unitofWork.User.LockUser(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult UnLock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _unitofWork.User.UnLockUser(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
