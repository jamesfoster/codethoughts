namespace CodeThoughts.Data.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Model;

	public abstract class StubPostRepository : IPostRepository
	{
		List<Post> Posts { get; set; }

		protected StubPostRepository(IEnumerable<Post> posts)
		{
			Posts = new List<Post>(posts);
		}

		public IQueryable<Post> All()
		{
			return Posts.AsQueryable();
		}

		public Post Find(int id)
		{
			return Posts.FirstOrDefault(p => p.Id == id);
		}

		public Post FindByUrl(string url)
		{
			return Posts.FirstOrDefault(p => p.Url == url);
		}

		public abstract void Add(Post post);
		public abstract void Update(Post post);
		public abstract void Delete(Post post);
	}
}