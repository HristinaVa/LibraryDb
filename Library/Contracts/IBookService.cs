using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task AddBookAsync(AddBookViewModel model);
        Task AddBookToCollection(string userId, BookViewModel book);
        Task<IEnumerable<BookInfoViewModel>> GetAllBooksAsync();
        Task<BookViewModel?> GetBookByIdAsync(int bookId);
        Task<IEnumerable<BookInfoViewModel>> GetMyBooksAsync(string userId);
        Task<AddBookViewModel> GetNewBookModelAsync();
        Task RemoveBookFromCollection(string userId, BookViewModel book);
    }
}
