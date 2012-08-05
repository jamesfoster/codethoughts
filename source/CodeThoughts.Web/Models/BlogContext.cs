namespace CodeThoughts.Models
{
	using System.Data.Entity;

	public class BlogContext : DbContext
	{
		public BlogContext()
			: base("name=DefaultConnection")
		{
		}

		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
	}
}