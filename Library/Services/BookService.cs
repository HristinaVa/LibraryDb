using Library.Contracts;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _dbContext;
        public BookService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            Book book =  new Book
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.Url,
                Rating = model.Rating,
                CategoryId = model.CategoryId,
            };
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

        }

        public async Task AddBookToCollection(string userId, BookViewModel book)
        {
            bool alreadyhave = await _dbContext.IdentityUserBooks
                 .AnyAsync(x => x.CollectorId == userId && x.BookId == book.Id);
            if (alreadyhave == false)
            {
                var bookCollection =new IdentityUserBook()
                {
                    BookId = book.Id,
                    CollectorId = userId
                };
                await _dbContext.IdentityUserBooks.AddAsync(bookCollection);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookInfoViewModel>> GetAllBooksAsync()
        {
            return await _dbContext.Books.Select(x => new BookInfoViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Rating = x.Rating,
                ImageUrl = x.ImageUrl,
                Category = x.Category.Name
            }).ToListAsync();
        }

        public async Task<BookViewModel?> GetBookByIdAsync(int id)
        {
            return await _dbContext.Books.Where(x => x.Id == id).Select(x => new BookViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Rating = x.Rating,
                ImageUrl = x.ImageUrl,
                CategoryId = x.Category.Id,
                Description = x.Description
            }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookInfoViewModel>> GetMyBooksAsync(string userId)
        {
            return await _dbContext.IdentityUserBooks.Where(a => a.CollectorId == userId)
                .Select(x => new BookInfoViewModel
                {
                    Id = x.BookId,
                    Title = x.Book.Title,
                    Author = x.Book.Author,
                    Description = x.Book.Description,
                    ImageUrl = x.Book.ImageUrl,
                    Category = x.Book.Category.Name
                }).ToListAsync();
        }

        public async Task<AddBookViewModel> GetNewBookModelAsync()
        {
            var categories = await _dbContext.Categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            var bookModel = new AddBookViewModel
            {
                Categories = categories
            };
            return bookModel;
        }

        public async Task RemoveBookFromCollection(string userId, BookViewModel book)
        {
            var thisBook = await _dbContext.IdentityUserBooks
                .Where(x => x.CollectorId == userId && x.BookId == book.Id).
                FirstOrDefaultAsync();
            if (thisBook != null)
            {
                _dbContext.IdentityUserBooks.Remove(thisBook);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
