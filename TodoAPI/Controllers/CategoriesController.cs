using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.DTOs;
using TodoAPI.Models;
using TodoAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ICategoryService _categoryService;

        public CategoriesController(TodoContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<JsonResult> GetAsync()
        {
            return new JsonResult(await _categoryService.getCategories());
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<JsonResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            return new JsonResult(await _categoryService.postCategory(categoryDTO));
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            return new JsonResult(await _categoryService.putCategory(categoryDTO));
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await _categoryService.deleteCategory(id);
                return new JsonResult("Deleted successfully");
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }
        }
    }
}
