namespace CodeThoughts.Data
{
	using System;
	using System.Linq;
	using Model;

	public class PublishedPostRepository : IPostRepository
	{
		public IPostRepository Inner { get; set; }

		public PublishedPostRepository(IPostRepository inner)
		{
			Inner = inner;
		}

		public IQueryable<Post> All()
		{
			return Inner.All().Where(p => p.Published);
		}

		public Post Find(int id)
		{
			return FindInternal(() => Inner.Find(id));
		}

		public Post FindByUrl(string url)
		{
			return FindInternal(() => Inner.FindByUrl(url));
		}

		Post FindInternal(Func<Post> getter)
		{
			var post = getter();

			if (post == null || !post.Published)
				return null;

			return post;
		}

		public void Add(Post post)
		{
			Inner.Add(post);
		}

		public void Update(Post post)
		{
			Inner.Update(post);
		}

		public void Delete(Post post)
		{
			Inner.Delete(post);
		}
	}
}