namespace CodeThoughts.Data
{
	using System.Data;
	using System.Data.Entity;
	using System.Collections.Generic;
	using Models;

	public class EfPostRepository : IPostRepository
	{
		public BlogContext Context { get; set; }

		public EfPostRepository(BlogContext context)
		{
			Context = context;
		}

		public IList<Post> All()
		{
			return Context.Posts.Include(p => p.Blog).ToList();
		}

		public Post Find(int id)
		{
			return Context.Posts.Find(id);
		}

		public void Add(Post post)
		{
			Context.Posts.Add(post);
			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Update(Post post)
		{
			Context.Entry(post).State = EntityState.Modified;
			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Delete(Post post)
		{
			Context.Posts.Remove(post);
			Context.SaveChanges(); // todo move this line out of here.
		}
	}
}