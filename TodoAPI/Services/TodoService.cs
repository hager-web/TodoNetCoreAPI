using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Services
{
    public interface ITodoService
    {
        abstract Task<List<TodoDTO>> getTodos();
        abstract Task<TodoDTO> postTodo(TodoDTO todo);
        abstract Task<TodoDTO> putTodo(TodoDTO newtodo);
        abstract Task<bool> deleteTodo(int id);

    }
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }
        public async Task<List<TodoDTO>> getTodos()
        {
            var todos = await Task.FromResult(_context.Todos.ToList());
            var todosDtO = (from t in todos
                            join c in _context.Categories on t.CategoryID equals c.CategoryID
                            select new TodoDTO
                            {
                                todoID = t.TodoID,
                                title = t.Title,
                                completed = t.Completed,
                                categoryID = t.CategoryID,
                                categoryName = c.Name
                            }).ToList();
            return todosDtO;
        }
        public async Task<TodoDTO> postTodo(TodoDTO newtodo)

        {
            var todo = new Todo
            {
                Title = newtodo.title,
                Completed = newtodo.completed,
                CategoryID = newtodo.categoryID
            };
            _context.Todos.Add(todo);

            await saveChanges();

            newtodo.todoID = todo.TodoID;
            newtodo.categoryName = getCategory(newtodo.categoryID).Result.Name;
            return newtodo;
        }

        public async Task<TodoDTO> putTodo(TodoDTO newtodo)

        {
            var todo = await getTodoByID(newtodo.todoID);
            todo.Title = newtodo.title;
            todo.Completed = newtodo.completed;
            todo.CategoryID = newtodo.categoryID;

            await saveChanges();

            newtodo.categoryName = getCategory(newtodo.categoryID).Result.Name;
            return newtodo;
        }
        public async Task<bool> deleteTodo(int id)
        {
            var todo = await getTodoByID(id);
            _context.Todos.Remove(todo);

            await saveChanges();

            return true;
        }

        private bool todoExists(int id) =>
             _context.Todos.Any(t => t.TodoID == id);
        

        private async Task<Todo> getTodoByID(int todoID)
        {
            var todo = await _context.Todos.FindAsync(todoID);
            if (todo == null)
            {
                throw new Exception("Bad Request");
            }
            return todo;
        }
        private async Task<bool> saveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private async Task<Category> getCategory(int categoryID)
        {
            return await _context.Categories.FindAsync(categoryID);
        }
    }
}
