using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface ITodo
    {
        Task<List<Todo>> GetAllTodos();

        Task<Todo> GetTodo(int id);

        Task<Todo> CreateTodo(Todo todo);

        Task<Todo> UpdateTodo(Todo exisitngTodo, Todo updatedTodo);

        void DeleteTodo(Todo todo);
    }
}
