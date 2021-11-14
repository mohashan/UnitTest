using Library.API.Controllers;
using Library.API.Data.Models;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Library.Test
{
    public class BooksControllerTest
    {
        BooksController _controller;
        IBookServices _service;

        public BooksControllerTest()
        {
            _service = new BookServices();
            _controller = new BooksController(_service);
        }

        [Fact]
        public void GetAllTest()
        {
            // Arrange

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Book>>(list.Value);
            var bookList = list.Value as List<Book>;
            Assert.Equal(2, bookList.Count);
        }

        [Theory]
        [ClassData(typeof(BookClassData))]
        public void AddBookTest(Book book)
        {
            //Arrange

            //Act
            var postResult = _controller.Post(book);
            //Assert
            Assert.IsType<CreatedAtActionResult>(postResult.Result);
            var resBook = postResult.Result as CreatedAtActionResult;
            Assert.IsType<Book>(resBook.Value);
            var newBook = resBook.Value as Book;
            Assert.NotNull(newBook.Id);
        }


        public class BookClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new Book { Title = "test" }

                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

           
        }


    }
}