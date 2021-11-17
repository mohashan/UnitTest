using Library.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Data.Services
{
    public class BookServices : IBookServices
    {
        private List<Book> books = new List<Book>();
        public BookServices()
        {
            books.Add(new Book
            {
                Author = "j.Hardy",
                Description = "",
                Id = new Guid("ba66ec07-1a1e-4a13-83a5-1ede4215f111"),
                Title = "Hate Me"
            });

            books.Add(new Book
            {
                Author = "j.Softy",
                Description = "",
                Id = new Guid("ba66ec07-1a1e-4a13-83a5-1ede4215f48a"),
                Title = "Love Me"
            });
        }
        public Book Add(Book newBook)
        {
            newBook.Id = Guid.NewGuid();
            books.Add(newBook);
            return newBook;
        }

        public IEnumerable<Book> GetAll() => books;

        public Book GetById(Guid id)
        {
            if (id == default)
                return null;

            return books.Find(c => c.Id == id);
        }

        public void Remove(Guid id)
        {
            var book = books.Find(c => c.Id == id);
            if (book == null)
                return;
            books.Remove(book);
        }
    }
}
