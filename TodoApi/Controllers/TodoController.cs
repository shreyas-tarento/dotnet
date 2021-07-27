using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private ITodo _iTodo;
        public TodoController(ITodo iTodo)
        {
            _iTodo = iTodo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodo()
        {
            var todo = await _iTodo.GetAllTodos();
            return Ok(todo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo([FromRoute] int id)
        {
            var existingTodo = await _iTodo.GetTodo(id);
            if (existingTodo == null)
                return NotFound();

            return Ok(existingTodo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] Todo todo)
        {
            if (ModelState.IsValid)
            {
                await _iTodo.CreateTodo(todo);
                return CreatedAtAction("GetTodo", new { todo.Id }, todo);
            }

            return new JsonResult("Somting went wrong") { StatusCode = 500};
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] int id, [FromBody] Todo todo)
        {
            if (id != todo.Id)
                return BadRequest();

            var existingTodo = await _iTodo.GetTodo(id);
            if (existingTodo == null)
                return NotFound();

            var newTodo = await _iTodo.UpdateTodo(existingTodo, todo);

            return Ok(newTodo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            var existingTodo = await _iTodo.GetTodo(id);
            if (existingTodo == null)
                return NotFound();

            _iTodo.DeleteTodo(existingTodo);
            return Ok(existingTodo);
        }

    }
}
