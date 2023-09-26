using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly  IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _productService.GetList();
            if (result.Result.Success)
            {
                return Ok(result.Result.Data);
            }
            return BadRequest(result.Result.Message);
        }
        [HttpGet("getlistbyadress")]
        public IActionResult GetListByAdress(string productName)
        {
            var result = _productService.GetListByAdress(productName);
            if (result.Result.Success)
            {
                return Ok(result.Result.Data);
            }
            return BadRequest(result.Result.Message);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.Result.Success)
            {
                return Ok(result.Result.Data);
            }
            return BadRequest(result.Result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(ProductAddDto productAddDto)
        {
            var result = _productService.Add(productAddDto);
            if(result.Result.Success)
            {
                return Ok(result.Result.Message);
            }
            return BadRequest(result.Result.Message);
        }
        [HttpPost("delete")]
        public IActionResult Delete(int productId)
        {
            var result = _productService.Delete(productId);
            if (result.Result.Success)
            {
                return Ok(result.Result.Message);
            }
            return BadRequest(result.Result.Message);
        }
        [HttpPost("update")]
        public IActionResult Update(Products products)
        {
            var result = _productService.Update(products);
            if (result.Result.Success)
            {
                return Ok(result.Result.Message);
            }
            return BadRequest(result.Result.Message);
        }
        [HttpGet("getbycategoryid")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            var result = _productService.GetProductByCategoryId(categoryId);
            if (result.Result.Success)
            {
                return Ok(result.Result.Data);
            }
            return BadRequest(result.Result.Message);
        }
    }
}
