using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            var item1 = new TodoItem
            {
                Title = "Learn ASP.NET Core",
                StartDate = DateTimeOffset.Now.AddDays(30),
                DueAt = DateTimeOffset.Now.AddDays(60),
                NumberOfDays = 4,
                Priority = 4
            };

            var item2 = new TodoItem
            {
                Title = "Build awesome apps",
                StartDate = DateTimeOffset.Now.AddDays(60),
                DueAt = DateTimeOffset.Now.AddDays(120),
                NumberOfDays = 20,
                Priority = 4
            };

            return Task.FromResult(new[] {
                item1,
                item2,
                new TodoItem {
                    Title = "Learn Typescript",
                    StartDate = DateTimeOffset.Now.AddDays(1),
                    DueAt = DateTimeOffset.Now.AddDays(5),
                    NumberOfDays = 5,
                    Priority = 4
                },
                new TodoItem {
                    Title = "Learn Caesar cipher: Private Key and Public Key",
                    StartDate = DateTimeOffset.Now.AddDays(6),
                    DueAt = DateTimeOffset.Now.AddDays(10),
                    NumberOfDays = 10,
                    Priority = 3
                },
                new TodoItem {
                    Title = "Learn Javascript",
                    StartDate = DateTimeOffset.Now.AddDays(6),
                    DueAt = DateTimeOffset.Now.AddDays(30),
                    NumberOfDays = 20,
                    Priority = 3
                },
                new TodoItem {
                    Title = "Update a linkedIn account",
                    StartDate = DateTimeOffset.Now.AddDays(30),
                    DueAt = DateTimeOffset.Now.AddDays(120),
                    NumberOfDays = 2,
                    Priority = 5
                },
                new TodoItem {
                    Title = "Submit Grading Spreadsheet",
                    StartDate = DateTimeOffset.Now.AddDays(1),
                    DueAt = DateTimeOffset.Now.AddDays(2),
                    NumberOfDays = 2,
                    Priority = 1
                },
                new TodoItem {
                    Title = "Submit Mini-Project for Adv. Application course",
                    StartDate = DateTimeOffset.Now.AddDays(1),
                    DueAt = DateTimeOffset.Now.AddDays(1),
                    NumberOfDays = 1,
                    Priority = 1
                },
                new TodoItem {
                    Title = "Submit Mini-Project for Game Programming course",
                    StartDate = DateTimeOffset.Now.AddDays(1),
                    DueAt = DateTimeOffset.Now.AddDays(3),
                    NumberOfDays = 2,
                    Priority = 2
                },
                new TodoItem {
                    Title = "Class Preparation",
                    StartDate = DateTimeOffset.Now.AddDays(1),
                    DueAt = DateTimeOffset.Now.AddDays(3),
                    NumberOfDays = 2,
                    Priority = 4
                },
                new TodoItem {
                    Title = "Practice CSS",
                    StartDate = DateTimeOffset.Now.AddDays(60),
                    DueAt = DateTimeOffset.Now.AddDays(120),
                    NumberOfDays = 60,
                    Priority = 4
                },
            });
        }

        public Task<bool> AddItemAsync(TodoItem newItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkDoneAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(TodoItem item)
        {
            throw new NotImplementedException();
        }
    }
}