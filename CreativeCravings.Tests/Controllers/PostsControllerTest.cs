using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreativeCravings.Controllers;
using System.Web.Mvc;
using CreativeCravings.Models;
using Moq;
using System.Linq;
using CreativeCravings.DAL;

namespace CreativeCravings.Tests.Controllers
{

    [TestClass]
    public class PostsControllerTest
    {
        [TestMethod]
        public void Index_Contains_ListOfPosts_Model()
        {
            // Arrange
            Mock<UnitOfWork> postMock = new Mock<UnitOfWork>();

            postMock.Setup(m => m.PostRepository.Items).Returns(new Post[]
            {
                new Post {ID=1, Title="Test post 1",Body="This is the body for test 1", AuthorID="1"},
                new Post {ID=2, Title="Test post 2",Body="This is the body for test 2", AuthorID="1"},
                new Post {ID=3, Title="Test post 3",Body="This is the body for test 3", AuthorID="1"}
            }.AsQueryable());

            PostsController controller = new PostsController(postMock.Object);

            // Act
            var actual = (List<Post>)controller.Index().Model;

            // Assert
            Assert.IsInstanceOfType(actual, typeof(List<Post>));
        }

    }
}
