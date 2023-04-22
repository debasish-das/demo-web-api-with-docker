using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<IdentityUser> _userManager;

        public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            // Get to-do items from database
            var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);

            Array.Sort(items);

            // Put items into a model
            var model = new TodoViewModel()
            {
                Items = items
            };

            //Setting error
            ViewBag.AddTodoError = TempData["AddErrors"];
            ViewBag.EditTodoError = TempData["UpdateErrors"];

            // Render view using the model
            return View(model);           
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> modelErrors = ModelState.Values.SelectMany(v => v.Errors);
                TempData["AddErrors"] = GetErrors(modelErrors);
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            newItem.UserId = currentUser.Id;

            var successful = await _todoItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var successful = await _todoItemService.MarkDoneAsync(id, currentUser);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateItem(TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> modelErrors = ModelState.Values.SelectMany(v => v.Errors);
                TempData["UpdateErrors"] = GetErrors(modelErrors);
                return RedirectToAction("Index");
            }
            
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            item.UserId = currentUser.Id;

            var successful = await _todoItemService.UpdateItemAsync(item);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }

        public List<string> GetErrors(IEnumerable<ModelError> modelErrors)
        {
            modelErrors = ModelState.Values.SelectMany(v => v.Errors);
            List<string> errors = new List<string>();
            foreach (var item in modelErrors)
            {
                errors.Add(item.ErrorMessage);
            }
            return errors;
        }
    }
}