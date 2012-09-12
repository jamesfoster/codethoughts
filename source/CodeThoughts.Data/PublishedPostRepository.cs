namespace CodeThoughts.Data
{
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
			var post = Inner.Find(id);

			return post.Published ? post : null;
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