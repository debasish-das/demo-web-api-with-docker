using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using Microsoft.AspNetCore.Identity;

namespace TodoApi.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync();

        Task<bool> AddItemAsync(TodoItem newItem);

        Task<bool> MarkDoneAsync(Guid id);

        Task<bool> UpdateItemAsync(TodoItem item);
    }
}