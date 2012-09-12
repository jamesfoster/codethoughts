namespace CodeThoughts.Web.Tests.Controllers
{
	using System.Web.Mvc;
	using CodeThoughts.Controllers;
	using Data;
	using Model;
	using Moq;
	using NUnit.Framework;

	[TestFixture]
	public class PostControllerTests
	{
		[Test]
		public void DetailsReturnsViewResultIfFound()
		{
			var postRepository = new Mock<IPostRepository>();

			var postId = 1;

			postRepository.Setup(r => r.Find(postId)).Returns(new Post { Id = postId });

			var controller = new PostController(postRepository.Object);

			var result = controller.Details(postId);

			Assert.That(result, Is.AssignableFrom<ViewResult>());
		}

		[Test]
		public void DetailsReturns404IfNotFound()
		{
			var postRepository = new Mock<IPostRepository>();

			var controller = new PostController(postRepository.Object);

			var result = controller.Details(1);

			Assert.That(result, Is.AssignableFrom<HttpNotFoundResult>());
		}
	}
}