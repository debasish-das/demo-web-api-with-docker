using Microsoft.AspNetCore.Mvc;

using TodoApi.Services;
using TodoApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("v1/todo-items")]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<TodoItem[]>> GetActionAsync()
        {
            var items = await _todoItemService.GetIncompleteItemsAsync();
            Array.Sort(items);
            return items;           
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<TodoItem>> GetActionAsync([FromBody] TodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var successful = await _todoItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }

            return newItem;
        }

        [HttpPost]
        [Route("/mark-done")]
        public async Task<ActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id is not found");
            }

            var successful = await _todoItemService.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            // return id.ToString();
            return Json( new { success = id + " marked as done." });
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<TodoItem>> UpdateItem(TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var successful = await _todoItemService.UpdateItemAsync(item);
            if (!successful)
            {
                return BadRequest("Could not update item.");
            }

            return item;
        }
    }
}