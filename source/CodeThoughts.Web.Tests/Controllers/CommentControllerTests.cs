namespace CodeThoughts.Web.Tests.Controllers
{
	using System.Web.Mvc;
	using CodeThoughts.Controllers;
	using Data;
	using Moq;
	using NUnit.Framework;

	[TestFixture]
	public class CommentControllerTests
	{
		[Test]
		public void IndexReturnsViewResult()
		{
			var commentRepository = new Mock<ICommentRepository>();
			var postRepository = new Mock<IPostRepository>();

			var controller = new CommentController(commentRepository.Object, postRepository.Object);

			var result = controller.Index();

			Assert.That(result, Is.AssignableFrom<ViewResult>());
		}
	}
}