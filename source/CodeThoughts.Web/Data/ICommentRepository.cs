namespace CodeThoughts.Data
{
	using System.Collections.Generic;

	public interface ICommentRepository
	{
		List<Comment> All();
		Comment Find(int id);
		void Add(Comment comment);
		void Update(Comment comment);
		void Delete(Comment comment);
	}
}