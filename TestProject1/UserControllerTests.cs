using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers; // Add this line
using MyMvcApp.Models; // Add this line

namespace TestProject1
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController controller;
        private List<User> users;

        [SetUp]
        public void Setup()
        {
            users = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com", Password = "password123" },
                new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Password = "password456" }
            };

            controller = new UserController();
            UserController.userlist = users; // Asignar la lista de usuarios al controlador
        }

        [Test]
        public void Index_ReturnsViewWithUsers()
        {
            // Act
            var result = controller.Index() as ViewResult;
            var model = result.Model as List<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, model.Count);
        }

        [Test]
        public void Details_UserExists_ReturnsViewWithUser()
        {
            // Act
            var result = controller.Details(1) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, model.Id);
        }

        [Test]
        public void Details_UserDoesNotExist_ReturnsHttpNotFound()
        {
            // Act
            var result = controller.Details(3);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Create_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var newUser = new User { Id = 3, Name = "New User", Email = "new@example.com", Password = "password789" };

            // Act
            var result = controller.Create(newUser) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual(3, UserController.userlist.Count);
        }

        [Test]
        public void Edit_UserExists_ReturnsViewWithUser()
        {
            // Act
            var result = controller.Edit(1) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, model.Id);
        }

        [Test]
        public void Edit_UserDoesNotExist_ReturnsHttpNotFound()
        {
            // Act
            var result = controller.Edit(3);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Edit_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var updatedUser = new User { Id = 1, Name = "Updated User", Email = "updated@example.com", Password = "newpassword123" };

            // Act
            var result = controller.Edit(1, updatedUser) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Updated User", UserController.userlist.First(u => u.Id == 1).Name);
        }

        [Test]
        public void Delete_UserExists_ReturnsViewWithUser()
        {
            // Act
            var result = controller.Delete(1) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, model.Id);
        }

        [Test]
        public void Delete_UserDoesNotExist_ReturnsHttpNotFound()
        {
            // Act
            var result = controller.Delete(3);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DeleteConfirmed_UserExists_RedirectsToIndex()
        {
            // Act
            var result = controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual(1, UserController.userlist.Count);
        }
    }
}