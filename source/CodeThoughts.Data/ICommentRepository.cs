namespace CodeThoughts.Data
{
	using System.Collections.Generic;
	using Model;

	public interface ICommentRepository
	{
		List<Comment> All();
		Comment Find(int id);
		void Add(Comment comment);
		void Update(Comment comment);
		void Delete(Comment comment);
	}
}