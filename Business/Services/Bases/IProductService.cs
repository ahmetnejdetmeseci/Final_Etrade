using Business.Models;
using Business.Results.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Bases
{
    public interface IProductService
    {
        IQueryable<ProductModel> Query();

        Result Add(ProductModel model);

        Result Update(ProductModel model);

        Result Delete(int id);
    }
}
