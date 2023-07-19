using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppFactoryTodoItem.Data;
using AppFactoryTodoItem.Models;
using AppFactoryTodoItem.Services;

namespace AppFactoryTodoItem.Controllers
{
    public class TodoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ApplicationDbContext context, ITodoItemService todoItemService)
        {
            _context = context;
            _todoItemService = todoItemService;
        }

        // GET: TodoItems
        public async Task<IActionResult> Index()
        {
            var data= await _todoItemService.GetAll();
            return View(data);      
        }

        // GET: TodoItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.TodoItems == null)
            {
                return NotFound();
            }

            var todoItem =await _todoItemService.Get((Guid)id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // GET: TodoItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                await _todoItemService.Add(todoItem);
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TodoItems == null)
            {
                return NotFound();
            }

            var todoItem = await _todoItemService.Get((Guid)id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        // POST: TodoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Title")] TodoItem todoItem)
        {
            if (id != todoItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _todoItemService.Update(todoItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(todoItem.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: TodoItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.TodoItems == null)
            {
                return NotFound();
            }

            var todoItem = await _todoItemService.Get((Guid)id);
               
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.TodoItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TodoItems'  is null.");
            }
            var todoItem = await _todoItemService.Get((Guid)id);
            if (todoItem != null)
            {
               await _todoItemService.Delete(id);
            }
            
         
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(Guid id)
        {
          return (_context.TodoItems?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
