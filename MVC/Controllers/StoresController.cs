#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Services.Bases;
using Business.Models;
using Microsoft.AspNetCore.Authorization;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class StoresController : Controller
    {
        // TODO: Add service injections here
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: Stores
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<StoreModel> storeList = _storeService.Query().ToList(); // TODO: Add get list service logic here
            return View(storeList);
        }    
    }
}
