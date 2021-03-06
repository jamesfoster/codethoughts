namespace CodeThoughts.Data
{
	using System.Collections.Generic;
	using System.Linq;
	using Model;

	public interface IPostRepository
	{
		IQueryable<Post> All();
		Post Find(int id);
		Post FindByUrl(string url);
		void Add(Post post);
		void Update(Post post);
		void Delete(Post post);
	}
}