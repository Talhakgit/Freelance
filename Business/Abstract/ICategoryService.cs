using Business.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResults<Categories>> GetById(int categoryId);
        Task<IDataResults<List<Categories>>> GetList();
        Task<IDataResults<List<Categories>>> GetListByAdress(string categoryName);
        Task<IResult> Add(string categoryName);
        Task<IResult> Update(Categories categories);
        Task<IResult> Delete(int categoryId);

//test
    }
}
