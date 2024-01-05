using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class StoreService : IStoreService
    {
        private readonly Db _db;


        public StoreService(Db db)
        {
            _db = db;
        }
        public IQueryable<StoreModel> Query()
        {
            return _db.Stores.OrderByDescending(s => s.Name).Select(s => new StoreModel
            {
                Name = s.Name,
                Id = s.Id,
            });
        }
    }
}
