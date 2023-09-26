using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("getproductdata")]
        public async Task<IActionResult> GetProductData()
        {
            // Create an instance of HttpClient
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Specify the URL you want to send a GET request to
                    string url = "https://localhost:44356/api/Category/getall";

                    // Send the GET request
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the content of the response as a string
                        string content = await response.Content.ReadAsStringAsync();
                        List<Categories> aa = JsonConvert.DeserializeObject<List<Categories>>(content);
                        return Ok(aa);

                    }
                    return new BadRequestResult();
                }
                catch (HttpRequestException e)
                {
                    return new BadRequestResult();
                }
            }
        }
        [HttpGet("getall")]
        //[Authorize]
        public IActionResult GetList()
        {
            var result = _categoryService.GetList();
            if (result.Result.Success)
            {
                return Ok(result.Result.Data);
            }
            return BadRequest(result.Result.Message);
        }
        [HttpGet("getlistcategories")]
        public IActionResult GetListByAdress(string categoryName)
        {
            var result = _categoryService.GetListByAdress(categoryName);
            if (result.Result.Success)
            {
                return Ok(result.Result.Data);
            }
            return BadRequest(result.Result.Message);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int categoryId)
        {
            var result = _categoryService.GetById(categoryId);
            if (result.Result.Success)
            {
                return Ok(result.Result.Data);
            }
            return BadRequest(result.Result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(string categoryName)
        {
            var result = _categoryService.Add(categoryName);
            if(result.Result.Success)
            {
                return Ok(result.Result.Message);
            }
            return BadRequest(result.Result.Message);
        }
        [HttpPost("delete")]
        public IActionResult Delete(int categoryId)
        {
            var result = _categoryService.Delete(categoryId);
            if (result.Result.Success)
            {
                return Ok(result.Result.Message);
            }
            return BadRequest(result.Result.Message);
        }
        [HttpPost("update")]
        public IActionResult Update(Categories stor)
        {
            var result = _categoryService.Update(stor);
            if (result.Result.Success)
            {
                return Ok(result.Result.Message);
            }
            return BadRequest(result.Result.Message);
        }
    }
}
