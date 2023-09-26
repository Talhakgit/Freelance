using Business.Abstract;
using Business.Contants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoriesDal _categoriesDal;

        public CategoryManager(ICategoriesDal categoriesDal)
        {
            _categoriesDal = categoriesDal;
        }
        public async Task<IResult> Add(string categoryName)
        {
            try
            {
                var category = new Categories()
                {
                    CategoryName = categoryName
                };
                _categoriesDal.Add(category);
                return new SuccessResult(Messages.CategoryAdded);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }

        }

        public async Task<IResult> Delete(int categoryId)
        {
            try
            {
                var category = _categoriesDal.Get(x => x.CategoryId == categoryId);
                _categoriesDal.delete(category);
                return new SuccessResult(Messages.CategoryDeleted);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }

        }

        public async Task<IDataResults<Categories>> GetById(int categoryId)
        {
            try
            {
                var categories = _categoriesDal.Get(p => p.CategoryId == categoryId);
                if (categories != null)
                {
                    return new SuccessDataResult<Categories>(categories,"OK");
                }
                return new ErrorDataResult<Categories>(new Categories(), "OK");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<Categories>(new Categories(), e.Message);
            }
           
        }
        [CacheAspect(duration: 1)]
        public async Task<IDataResults<List<Categories>>> GetList()
        {
            try
            {
                var categories = _categoriesDal.GetList().ToList();
                if (categories != null)
                {
                    return new SuccessDataResult<List<Categories>>(categories,"OK");
                }
                return new SuccessDataResult<List<Categories>>(new List<Categories>(),"OK");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<Categories>>(new List<Categories>(), e.Message);
            }
           
        }
        [CacheAspect(duration: 1)]
        public async Task<IDataResults<List<Categories>>> GetListByAdress(string categoryName)
        {
            try
            {
                var categoriesList = _categoriesDal.GetList(p => p.CategoryName == categoryName).ToList();
                if (categoriesList != null)
                {
                    return new SuccessDataResult<List<Categories>>(categoriesList,"OK");
                }
                return new SuccessDataResult<List<Categories>>(new List<Categories>(),"Ok");
            }
            catch (Exception e) 
            {
                return new ErrorDataResult<List<Categories>>(new List<Categories>(),e.Message);
            }        
        }
        public async Task<IResult> Update(Categories categories)
        {
            try
            {
                _categoriesDal.update(categories);
                return new SuccessResult(Messages.CategoryUpdated);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
            
        }
    }
}
