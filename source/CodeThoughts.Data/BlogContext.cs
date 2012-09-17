namespace CodeThoughts.Data
{
	using System.Data.Entity;
	using Model;

	public class BlogContext : DbContext, IBlogContext
	{
		public BlogContext() : base("name=DefaultConnection")
		{
		}

		public IDbSet<Blog> Blogs { get; set; }
		public IDbSet<Post> Posts { get; set; }
		public IDbSet<Comment> Comments { get; set; }
	}
}