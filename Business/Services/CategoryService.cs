using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly Db _db;


        public CategoryService(Db db)
        {
            _db = db;
        }

        public CategoryModel GetItem(int id)
        {
            return Query().SingleOrDefault(c => c.Id == id);
        }

        public List<CategoryModel> GetList()
        {
            return Query().ToList();
        }

        public IQueryable<CategoryModel> Query()
        {
            return _db.Categories.Include(c => c.Products).Select(c => new CategoryModel
            {
                Id = c.Id,
                Name = c.Name,
                ProductCountOutput = c.Products.Count,
            });
        }
    }
}
