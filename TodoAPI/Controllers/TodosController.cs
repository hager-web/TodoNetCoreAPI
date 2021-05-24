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
    public class TodosController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ITodoService _todoService;

        public TodosController(TodoContext context, ITodoService todoService)
        {
            _context = context;
            _todoService = todoService;
        }
        // GET: api/<TodosController>
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return new JsonResult(await _todoService.getTodos());
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TodosController>
        [HttpPost]
        public async Task<JsonResult> Post([FromBody] TodoDTO todoDTO)
        {
            return new JsonResult( await _todoService.postTodo(todoDTO));
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, [FromBody] TodoDTO todoDTO)
        {
            if (id != todoDTO.todoID) { 
                return new JsonResult(BadRequest()); 
            }
            return new JsonResult(await _todoService.putTodo(todoDTO));
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await _todoService.deleteTodo(id);
                return new JsonResult("Deleted successfully");
            }catch(Exception e)
            {
                return new JsonResult(e.Message);
            }
            
        }
    }
}
