namespace CodeThoughts.Data.Tests
{
	using System.Collections.Generic;
	using Model;
	using Moq;
	using NUnit.Framework;
	using System.Linq;

	[TestFixture]
	public class PublishedPostRepositoryTests
	{
		protected PublishedPostRepository Repository { get; set; }

		protected Mock<StubPostRepository> InnerRepository { get; set; }

		[SetUp]
		public void SetUp()
		{
			var posts = GetPosts();

			InnerRepository = new Mock<StubPostRepository>(MockBehavior.Loose, posts);

			Repository = new PublishedPostRepository(InnerRepository.Object);
		}

		IEnumerable<Post> GetPosts()
		{
			return new[]
				{
					new Post{Id = 1, Url = "post1", Published = true}, 
					new Post{Id = 2, Url = "post2", Published = true}, 
					new Post{Id = 3, Url = "post3", Published = false}, 
					new Post{Id = 4, Url = "post4", Published = true},
					new Post{Id = 5, Url = "post5", Published = false},
					new Post{Id = 6, Url = "post6", Published = false}
				};
		}

		[Test]
		public void AllReturnsOnlyPublishedPosts()
		{
			var posts = Repository.All().ToList();

			var ids = posts.Select(p => p.Id);

			Assert.That(ids, Is.EquivalentTo(new[] { 1, 2, 4 }));
		}

		[Test]
		public void FindReturnsPostIfPublished()
		{
			var post = Repository.Find(1);

			Assert.That(post, Is.Not.Null);
		}

		[Test]
		public void FindReturnsNullIfUnpublished()
		{
			var post = Repository.Find(3);

			Assert.That(post, Is.Null);
		}

		[Test]
		public void FindByUrlReturnsPostIfPublished()
		{
			var post = Repository.FindByUrl("post1");

			Assert.That(post, Is.Not.Null);
		}

		[Test]
		public void FindByUrlReturnsNullIfUnpublished()
		{
			var post = Repository.FindByUrl("post3");

			Assert.That(post, Is.Null);
		}

		[Test]
		public void AddWillPassThroughToInnerRepository()
		{
			var post = new Post();

			Repository.Add(post);

			InnerRepository.Verify(r => r.Add(post));
		}

		[Test]
		public void DeleteWillPassThroughToInnerRepository()
		{
			var post = new Post();

			Repository.Delete(post);

			InnerRepository.Verify(r => r.Delete(post));
		}

		[Test]
		public void UpdateWillPassThroughToInnerRepository()
		{
			var post = new Post();

			Repository.Update(post);

			InnerRepository.Verify(r => r.Update(post));
		}
	}
}
