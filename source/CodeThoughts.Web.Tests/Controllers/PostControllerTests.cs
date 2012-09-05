namespace CodeThoughts.Web.Tests.Controllers
{
	using System.Web.Mvc;
	using CodeThoughts.Controllers;
	using Data;
	using Moq;
	using NUnit.Framework;

	[TestFixture]
	public class PostControllerTests
	{
		[Test]
		public void IndexReturnsViewResult()
		{
			var postRepository = new Mock<IPostRepository>();

			var controller = new PostController(postRepository.Object);

			var result = controller.Index();

			Assert.That(result, Is.AssignableFrom<ViewResult>());
		}
	}
}