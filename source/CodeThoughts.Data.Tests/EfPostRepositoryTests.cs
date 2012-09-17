namespace CodeThoughts.Data.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;
	using Infrastructure.Formatters;
	using Model;
	using Moq;
	using NUnit.Framework;

	[TestFixture]
	public class EfPostRepositoryTests
	{
		protected EfPostRepository Repository { get; set; }
		protected Mock<IMarkdownService> Markdown { get; set; }
		protected Mock<IBlogContext> Context { get; set; }
		protected InMemoryDbSet<Post> Posts { get; set; }

		[SetUp]
		public void SetUp()
		{
			Markdown = new Mock<IMarkdownService>();
			Context = new Mock<IBlogContext>();

			Repository = new EfPostRepository(Context.Object, Markdown.Object);

			var posts = GetPosts().ToList();
			Posts = new InMemoryDbSet<Post>(posts);

			Context
				.SetupGet(c => c.Posts)
				.Returns(Posts);

			Markdown
				.Setup(m => m.ToHtml(It.IsAny<string>()))
				.Returns<string>(s => s == null ? null : s.Replace("*", "!"));
		}

		static IEnumerable<Post> GetPosts()
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
		public void AllIncludesBlog()
		{
			Repository.All();

			Assert.That(Posts.Includes, Has.Member("Blog"));
		}

		[Test]
		public void AllReturnsAllPosts()
		{
			var result = Repository.All().ToList();

			Assert.That(result, Has.Count.EqualTo(6));
		}

		[Test]
		public void WhenAddingPostSetDateCreated()
		{
			var post = new Post { Title = "Hello World" };

			Repository.Add(post);

			Assert.That(post.DateCreated, Is.Not.Null);
			Assert.That(post.DateCreated, Is.Not.EqualTo(default(DateTime)));
			Assert.That(post.DateCreated, Is.GreaterThan(DateTime.Now.Subtract(TimeSpan.FromMilliseconds(150))));
			Assert.That(post.DateCreated, Is.LessThanOrEqualTo(DateTime.Now));
		}

		[Test]
		public void WhenAddingPostSetContentHtml()
		{
			var post = new Post { Title = "Hello World", Content = "Test *content*" };

			Repository.Add(post);

			Assert.That(post.ContentHTML, Is.Not.Null);
		}

		[Test]
		public void WhenAddingPostUseMarkdownServiceToGenerateContentHtml()
		{
			var post = new Post { Title = "Hello World", Content = "Test *content*" };

			Repository.Add(post);

			Markdown.Verify(m => m.ToHtml("Test *content*"));
			Assert.That(post.ContentHTML, Is.EqualTo("Test !content!"));
		}

		[Test]
		public void WhenAddingPostGenerateUrl()
		{
			var post = new Post { Title = "Hello World", Content = "Test *content*" };

			Repository.Add(post);

			Assert.That(post.Url, Is.EqualTo("hello-world"));
		}

		[Test]
		public void WhenAddingPostGenerateUniqueUrl()
		{
			var post1 = new Post { Title = "Hello World" };
			var post2 = new Post { Title = "Hello World" };

			Repository.Add(post1);
			Repository.Add(post2);

			Assert.That(post1.Url, Is.EqualTo("hello-world"));
			Assert.That(post2.Url, Is.EqualTo("hello-world2"));
		}

		[Test]
		public void GeneratedUrlRemovesInvalidCharacters()
		{
			var post = new Post { Id = 1, Title = @"Hello !""£$%^&*()_+<>?:@~{},./;'#[]|\ World" };

			Repository.Add(post);

			Assert.That(post.Url, Is.EqualTo("hello-world"));
		}

		[Test]
		public void WhenPublishingPostSetDateCreated()
		{
			var post = new Post { Id = 3, Title = "Hello World", Published = true };

			Repository.Update(post);
			post = Repository.Find(3);

			Assert.That(post.DateCreated, Is.Not.Null);
			Assert.That(post.DateCreated, Is.Not.EqualTo(default(DateTime)));
			Assert.That(post.DateCreated, Is.GreaterThan(DateTime.Now.Subtract(TimeSpan.FromMilliseconds(150))));
			Assert.That(post.DateCreated, Is.LessThanOrEqualTo(DateTime.Now));
		}

		[Test]
		public void WhenPublishingPostAlreadyPublishedDoesNotSetDateCreated()
		{
			var post = new Post { Id = 4, Title = "Hello World", Published = true };

			Repository.Update(post);
			post = Repository.Find(4);

			Assert.That(post.DateCreated, Is.Not.Null);
			Assert.That(post.DateCreated, Is.EqualTo(default(DateTime)));
		}

		[Test]
		public void WhenUpdatingPostGeneratesUrl()
		{
			var post = new Post { Id = 4, Title = "Hello World", Published = true };

			Repository.Update(post);
			post = Repository.Find(4);

			Assert.That(post.Url, Is.EqualTo("hello-world"));
		}

	}
}