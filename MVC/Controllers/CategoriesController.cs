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
    public class CategoriesController : Controller
    {
        // TODO: Add service injections here
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        [Authorize]
        public IActionResult Index()
        {
            List<CategoryModel> categoryList = _categoryService.GetList(); // TODO: Add get list service logic here
            return View(categoryList);
        }

        // GET: Categories/Details/5
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            CategoryModel category = _categoryService.GetItem(id); // TODO: Add get item service logic here
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        
	}
}
