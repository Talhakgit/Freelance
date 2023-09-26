using Business.Abstract;
using Business.Contants;
using Core.Aspects.Autofac.Caching;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        public async Task<IResult> Add(ProductAddDto dto)
        {
            try
            {
                var category = await _categoryService.GetById(dto.CategoryId);
                if(category.Success == false)
                {
                    return new ErrorResult("Category not found!");
                }
                var products = new Products()
                {
                    CategoryId = dto.CategoryId,
                    ProductName = dto.ProductName,
                };
                _productDal.Add(products);
                return new SuccessResult(Messages.ProductAdded);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public async Task<IResult> Delete(int productId)
        {
            try
            {
                var product = _productDal.Get(x => x.ProductId == productId);
                if(product == null) 
                {
                    return new ErrorResult("Product not found!");
                }
                _productDal.delete(product);
                return new SuccessResult(Messages.ProductDeleted);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
        [CacheAspect(duration: 1)]
        public async Task<IDataResults<List<Products>>> GetById(int productId)
        {
            try
            {
                var products = _productDal.GetList(x => x.ProductId == productId).ToList();
                if (products != null)
                {
                    return new SuccessDataResult<List<Products>>(products, "OK");
                }
                return new SuccessDataResult<List<Products>>(new List<Products>(), "OK");
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<Products>>(new List<Products>(), e.Message);
            }
        }
        [CacheAspect(duration: 1)]
        public async Task<IDataResults<List<Products>>> GetList()
        {
            try
            {
                var products = _productDal.GetList().ToList();
                return new SuccessDataResult<List<Products>>(products, "OK");
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<Products>>(new List<Products>(), e.Message);
            }
        }
        [CacheAspect(duration: 1)]
        public async Task<IDataResults<List<Products>>> GetListByAdress(string productName)
        {
            try
            {
                var products = _productDal.GetList(x => x.ProductName == productName).ToList();
                return new SuccessDataResult<List<Products>>(products, "OK");
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<Products>>(new List<Products>(), e.Message);
            }
        }

        public async Task<IDataResults<List<Products>>> GetProductByCategoryId(int categoryId)
        {
            try
            {
                var productList = _productDal.GetList(x => x.CategoryId == categoryId).ToList();
                return new SuccessDataResult<List<Products>>(productList, "OK");

            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<Products>>(new List<Products>(),e.Message);
            }
        }

        public async Task<IResult> Update(Products products)
        {
            try
            {
                var category = await _categoryService.GetById(products.CategoryId);
                if (category.Success == false)
                {
                    return new ErrorResult("Category not found!");
                }
                _productDal.update(products);
                return new SuccessResult(Messages.ProductUpdated);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}
