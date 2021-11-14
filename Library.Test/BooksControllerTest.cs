using Library.API.Controllers;
using Library.API.Data.Models;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [ClassData(typeof(ValidBookClassData))]
        [ClassData(typeof(InvalidBookClassData))]
        public void AddBookTest(listTestBooks books)
        {
            //Arrange

            //Act
                var postResult = _controller.Post(books.book);

            //Assert
            if (books.IsValid)
            {

                Assert.IsType<CreatedAtActionResult>(postResult.Result);
                var resBook = postResult.Result as CreatedAtActionResult;
                Assert.IsType<Book>(resBook.Value);
                var newBook = resBook.Value as Book;
                Assert.Equal(books.book.Author, newBook.Author);
                Assert.Equal(books.book.Title, newBook.Title);
                Assert.Equal(books.book.Description, newBook.Description);
            }
            else
            {
                _controller.ModelState.AddModelError("Title", "Title is a required field");
                postResult = _controller.Post(books.book);

                Assert.IsType<BadRequestObjectResult>(postResult.Result);
            }
        }

        [Theory]
        [InlineData("ba66ec07-1a1e-4a13-83a5-1ede4215f48a", "Love Me", "ba66ec07-1a1e-4a13-83a5-1ede4215f111")]
        public void GetOneTest(string Id1, string title, string Id2)
        {
            //Arrange
            var validId = new Guid(Id1);
            var invalidId = new Guid(Id2);

            //Act
            var SuccessResult = _controller.Get(validId);
            var FailedResult = _controller.Get(invalidId);

            //Assert
            Assert.IsType<OkObjectResult>(SuccessResult.Result);
            Assert.IsType<NotFoundResult>(FailedResult.Result);
            var resBook = SuccessResult.Result as OkObjectResult;
            Assert.IsType<Book>(resBook.Value);
            var retBook = resBook.Value as Book;
            Assert.Equal(validId, retBook.Id);
            Assert.Equal(title, retBook.Title);
        }

        public class listTestBooks
        {
            public Book book { get; set; }
            public bool IsValid { get; set; }
        }

        public class ValidBookClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new listTestBooks{ book = new Book { Title = "test" },IsValid = true }

                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        }

        public class InvalidBookClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new listTestBooks{ book = new Book { Description = "test" },IsValid = false }

                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        }


    }
}