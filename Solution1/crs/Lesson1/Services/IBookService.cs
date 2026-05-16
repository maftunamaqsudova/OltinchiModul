using Lesson1.Dtos;

namespace Lesson1.Servers
{
    public interface IBookService
    {
        Task<long> AddAsync(BookCreateDto bookCreateDto);
        Task<List<BookDto>> GetAllAsync();
    }
}