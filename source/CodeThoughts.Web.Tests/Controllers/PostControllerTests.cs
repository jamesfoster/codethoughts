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

			const string postUrl = "the-post";

			postRepository.Setup(r => r.FindByUrl(postUrl)).Returns(new Post { Url = postUrl });

			var controller = new PostController(postRepository.Object);

			var result = controller.Details(postUrl);

			Assert.That(result, Is.AssignableFrom<ViewResult>());
		}

		[Test]
		public void DetailsReturns404IfNotFound()
		{
			var postRepository = new Mock<IPostRepository>();

			var controller = new PostController(postRepository.Object);

			var result = controller.Details("the-post");

			Assert.That(result, Is.AssignableFrom<HttpNotFoundResult>());
		}
	}
}