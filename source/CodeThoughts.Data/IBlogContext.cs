namespace CodeThoughts.Data
{
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using Model;

	public interface IBlogContext
	{
		IDbSet<Blog> Blogs { get; set; }
		IDbSet<Post> Posts { get; set; }
		IDbSet<Comment> Comments { get; set; }
		Database Database { get; }
		int SaveChanges();
		DbEntityEntry Entry(object entity);
	}
}