using Library.Contracts;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> All()
        {
            var model = await _bookService.GetAllBooksAsync();
            return View(model);
        }
        public async Task<IActionResult> Mine()
        {
            var model = await _bookService.GetMyBooksAsync(GetUserId());
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddBookViewModel model = await _bookService.GetNewBookModelAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _bookService.AddBookAsync(model);
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> AddToCollection(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }
            var userId = GetUserId();
            await _bookService.AddBookToCollection(userId, book);
            return RedirectToAction(nameof(All));

        }
        public async Task<IActionResult> RemoveFromCollectionAsync(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book != null)
            {
                var userId = GetUserId();
                await _bookService.RemoveBookFromCollection(userId, book);
            }
            return RedirectToAction(nameof(Mine));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
