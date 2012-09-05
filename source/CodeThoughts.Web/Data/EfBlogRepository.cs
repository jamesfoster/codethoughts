namespace CodeThoughts.Data
{
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using Models;

	public class EfBlogRepository : IBlogRepository
	{
		public BlogContext Context { get; set; }

		public EfBlogRepository(BlogContext context)
		{
			Context = context;
		}

		public IList<Blog> All()
		{
			return Context.Blogs.ToList();
		}

		public Blog Find(int id)
		{
			return Context.Blogs.Find(id);
		}

		public void Add(Blog blog)
		{
			Context.Blogs.Add(blog);
			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Update(Blog blog)
		{
			Context.Entry(blog).State = EntityState.Modified;
			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Delete(Blog blog)
		{
			Context.Blogs.Remove(blog);
			Context.SaveChanges(); // todo move this line out of here.
		}
	}
}