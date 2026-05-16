using Lesson1.Data;
using Lesson1.Dtos;
using Lesson1.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lesson1.Servers
{
    public class BookService : IBookService
    {
        private readonly AppDBContext _context;

        public BookService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<long> AddAsync(BookCreateDto bookCreateDto)
        {
            var book = new Book()
            {
                Author = bookCreateDto.Author,
                Title = bookCreateDto.Title,
                Price = bookCreateDto.Price
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return book.BookId;
        }
        

        public async Task<List<BookDto>> GetAllAsync()
        {
            var books = await _context.Books.ToListAsync();
            return books.Select(b => new BookDto()
            {
                Author = b.Author,
                BookId = b.BookId,
                Title = b.Title,
                Price = b.Price
            }).ToList();
        }
    }
}
