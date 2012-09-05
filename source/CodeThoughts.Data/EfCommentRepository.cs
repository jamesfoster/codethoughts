namespace CodeThoughts.Data
{
	using System.Data;
	using System.Data.Entity;
	using System.Collections.Generic;
	using System.Linq;
	using Model;

	public class EfCommentRepository : ICommentRepository
	{
		public BlogContext Context { get; set; }

		public EfCommentRepository(BlogContext context)
		{
			Context = context;
		}

		public List<Comment> All()
		{
			return Context.Comments.Include(c => c.Post).ToList();
		}

		public Comment Find(int id)
		{
			return Context.Comments.Find(id);
		}

		public void Add(Comment comment)
		{
			Context.Comments.Add(comment);
			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Update(Comment comment)
		{
			Context.Entry(comment).State = EntityState.Modified;
			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Delete(Comment comment)
		{
			Context.Comments.Remove(comment);
			Context.SaveChanges(); // todo move this line out of here.
		}
	}
}