using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IDataResults<List<Products>>> GetById(int productId);
        Task<IDataResults<List<Products>>> GetList();
        Task<IDataResults<List<Products>>> GetListByAdress(string categoryName);
        Task<IResult> Add(ProductAddDto productAddDto);
        Task<IResult> Update(Products products);
        Task<IResult> Delete(int productId);
        Task<IDataResults<List<Products>>> GetProductByCategoryId(int categoryId);
    }
}
