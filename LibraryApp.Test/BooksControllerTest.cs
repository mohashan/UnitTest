using LibraryApp.Controllers;
using LibraryApp.Data.MockData;
using LibraryApp.Data.Models;
using LibraryApp.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace LibraryApp.Test
{
    public class BooksControllerTest
    {
        [Fact]
        public void IndexUnitTest()
        {
            //Arrange
            var mockRepo = new Mock<IBookService>();
            mockRepo.Setup(n => n.GetAll()).Returns(MockData.GetTestBookItems);
            var controller = new BooksController(mockRepo.Object);
            //Act
            var result = controller.Index();
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewResultBooks = Assert.IsAssignableFrom<List<Book>>(viewResult.Model);
            Assert.Equal(5, viewResultBooks.Count);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c211")]
        public void DetailsUnitTest(string validGuid,string invalidGuid)
        {
            //Arrange
            var mockRepo = new Mock<IBookService>();
            var validId = new Guid(validGuid);
            var invalidId = new Guid(invalidGuid);
            mockRepo.Setup(n => n.GetById(validId)).Returns(MockData.GetTestBookItems().FirstOrDefault(c=>c.Id == validId));
            var controller = new BooksController(mockRepo.Object);
            //Act
            var result = controller.Details(validId);
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewResultBooks = Assert.IsAssignableFrom<Book>(viewResult.Model);
            Assert.Equal("Managing Oneself", viewResultBooks.Title);


            //Arrange
            mockRepo.Setup(n => n.GetById(invalidId)).Returns(MockData.GetTestBookItems().FirstOrDefault(c => c.Id == invalidId));
            //Act
            result = controller.Details(invalidId);
            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public void CreateTest()
        {
            //Arrange
            var mockRepo = new Mock<IBookService>();
            var newValidBook = new Book { Author = "newAuthor", Description = "new Description", Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c211"), Title = "newBook" };
            mockRepo.Setup(n => n.Add(newValidBook)).Returns(newValidBook);
            var controller = new BooksController(mockRepo.Object);
            //Act
            var result = controller.Create(newValidBook);
            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), viewResult.ActionName);
            Assert.Null(viewResult.ControllerName);

            //Arrange
            var newInvalidBook = new Book { Author = "newAuthor2", Description = "new Description 2", Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c222") };
            mockRepo.Setup(n => n.Add(newInvalidBook)).Returns(newInvalidBook);
            controller.ModelState.AddModelError("Title", "Title field is required");
            //Act
            result = controller.Create(newValidBook);
            //Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badResult.Value);


        }


        [Fact]
        public void RemoveTest()
        {
            //Arrange
            var mockRepo = new Mock<IBookService>();
            var validId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c211");
            mockRepo.Setup(n => n.Remove(validId));
            var controller = new BooksController(mockRepo.Object);
            //Act
            var result = controller.Delete(validId,null);
            //Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), viewResult.ActionName);
            Assert.Null(viewResult.ControllerName);

        }
    }
}