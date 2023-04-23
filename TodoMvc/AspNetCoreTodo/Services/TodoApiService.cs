using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoApiService : ITodoItemService
    {
        private HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public TodoApiService(ApplicationDbContext context)
        {
            _httpClient = new()
            {
                // BaseAddress = new Uri("http://api"),
                BaseAddress = new Uri("http://localhost:8080/"),
            };
            _context = context;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<TodoItem[]>("/v1/todo-items");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new TodoItem[] {};
            }
        }

        public async Task<bool> AddItemAsync(TodoItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;

            try
            {
                using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
                    "/v1/todo-items",
                    newItem);
                response.EnsureSuccessStatusCode();
                var todo = await response.Content.ReadFromJsonAsync<TodoItem>();

                if (todo == null) return false;
                else return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
                "/mark-done/?id=" + id,
                new TodoItem());
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(TodoItem item)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.PutAsJsonAsync(
                    "/v1/todo-items",
                    item);
                response.EnsureSuccessStatusCode();
                var todo = await response.Content.ReadFromJsonAsync<TodoItem>();

                if (todo == null) return false;
                else return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
    }
}