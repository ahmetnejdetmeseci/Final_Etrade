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
using Elfie.Serialization;
using System.ComponentModel.Design;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class ProductsController : Controller
    {
        // TODO: Add service injections here
        private readonly IProductService _productService;
        private readonly IStoreService _storeService;
        private readonly ICategoryService _categoryService;
        public ProductsController(IProductService productService, IStoreService storeService, ICategoryService categoryService)
        {
            _productService = productService;
            _storeService = storeService;
            _categoryService = categoryService;
        }

        // GET: Products
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<ProductModel> productList = _productService.Query().ToList(); // TODO: Add get list service logic here
            return View(productList);
        }

        // GET: Products/Details/5
        [Authorize]
        public IActionResult Details(int id)
        {
            ProductModel product = _productService.Query().SingleOrDefault(p => p.Id == id); // TODO: Add get item service logic here
            if (product == null)
            {
                return View("_Error", "Product not found!");
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["CategoryId"] = new SelectList(_categoryService.Query().ToList(), "Id", "Name");
            ViewBag.StoreId = new MultiSelectList(_storeService.Query().ToList(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var result = _productService.Add(product);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            ViewData["CategoryId"] = new MultiSelectList(_categoryService.Query().ToList(), "Id", "Name");
			ViewBag.StoreId = new MultiSelectList(_storeService.Query().ToList(), "Id", "Name");
			return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            ProductModel product = _productService.Query().SingleOrDefault(p => p.Id == id); // TODO: Add get item service logic here
            if (product == null)
            {
                return View("_Error", "Resource not found!");
            }
			ViewData["CategoryId"] = new MultiSelectList(_categoryService.Query().ToList(), "Id", "Name");
			ViewBag.StoreId = new MultiSelectList(_storeService.Query().ToList(), "Id", "Name"); 
            return View(product);
        }

        // POST: Products/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var result = _productService.Update(product);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    
                    return RedirectToAction(nameof(Details), new { id = product.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
			ViewData["CategoryId"] = new MultiSelectList(_categoryService.Query().ToList(), "Id", "Name");
			ViewBag.StoreId = new MultiSelectList(_storeService.Query().ToList(), "Id", "Name"); 
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
