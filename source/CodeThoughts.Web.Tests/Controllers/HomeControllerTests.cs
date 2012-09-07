namespace CodeThoughts.Web.Tests.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;
	using CodeThoughts.Controllers;
	using Data;
	using Model;
	using Moq;
	using NUnit.Framework;
	using NUnit.Framework.Constraints;

	[TestFixture]
	public class HomeControllerTests
	{
		Mock<IPostRepository> PostRepository { get; set; }
		HomeController Controller { get; set; }

		[SetUp]
		public void SetUp()
		{
			PostRepository = new Mock<IPostRepository>();

			var posts = CreatePosts(15);

			PostRepository
				.Setup(r => r.All())
				.Returns(posts.AsQueryable());

			Controller = new HomeController(PostRepository.Object);
		}

		static IEnumerable<Post> CreatePosts(int count)
		{
			var posts = new Post[count];

			for (int i = 0; i < count; i++)
			{
				posts[i] = new Post
					{
						Id = i,
						DateCreated = DateTime.Now.AddDays(i)
					};
			}
			return posts;
		}

		[Test]
		public void IndexReturnsViewResult()
		{
			var result = Controller.Index();

			Assert.That(result, Is.AssignableFrom<ViewResult>());
		}

		[Test]
		public void IndexCallsRepositoryAll()
		{
			Controller.Index();

			PostRepository.Verify(r => r.All());
		}

		[Test]
		public void IndexReturns10Posts()
		{
			var result = (ViewResult) Controller.Index();

			var model = result.Model as IList<Post>;

			Assert.That(model.Count, Is.EqualTo(10));
		}

		[Test]
		public void IndexReturnsLast10PostsLatestFirst()
		{
			var result = (ViewResult) Controller.Index();

			var model = (IList<Post>) result.Model;

			var ids = model.Select(p => p.Id);

			Assert.That(ids, Is.EquivalentTo(new[] {14, 13, 12, 11, 10, 9, 8, 7, 6, 5}));
		}
	}
}