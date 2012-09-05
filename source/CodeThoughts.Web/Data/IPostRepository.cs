namespace CodeThoughts.Data
{
	using System.Collections.Generic;

	public interface IPostRepository
	{
		IList<Post> All();
		Post Find(int id);
		void Add(Post post);
		void Update(Post post);
		void Delete(Post post);
	}
}