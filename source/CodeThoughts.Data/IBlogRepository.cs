namespace CodeThoughts.Data
{
	using System.Collections.Generic;
	using Model;

	public interface IBlogRepository
	{
		IList<Blog> All();
		Blog Find(int id);
		void Add(Blog blog);
		void Update(Blog blog);
		void Delete(Blog blog);
	}
}