using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.SqlDbContext;

namespace TodoApi.Data
{
    public class SqlTodoData : ITodo
    {
        private readonly GlobalDbContext _context;
        public SqlTodoData(GlobalDbContext context)
        {
            _context = context;
        }
        public async Task<Todo> CreateTodo(Todo todo)
        {
            await _context.Todo.AddAsync(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        public void DeleteTodo(Todo todo)   
        {
           _context.Todo.Remove(todo);
           _context.SaveChanges();
           // return todo;
                   
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            return await _context.Todo.ToListAsync();
        }

        public async Task<Todo> GetTodo(int id)
        {
            return await _context.Todo.FirstOrDefaultAsync(val => val.Id == id);
        }

        public async Task<Todo> UpdateTodo(Todo exisitngTodo, Todo updatedTodo)
        {
            exisitngTodo.Title = updatedTodo.Title;
            exisitngTodo.Description = updatedTodo.Description;
            exisitngTodo.IsDone = updatedTodo.IsDone;

            await _context.SaveChangesAsync();

            return exisitngTodo;
        }
    }
}
