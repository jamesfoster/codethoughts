namespace CodeThoughts.Data
{
	using System;
	using System.Data.Entity;
	using System.Linq;
	using Infrastructure.Formatters;
	using Model;

	public class EfPostRepository : IPostRepository
	{
		public BlogContext Context { get; set; }
		public MarkdownService Markdown { get; set; }

		public EfPostRepository(BlogContext context, MarkdownService markdown)
		{
			Context = context;
			Markdown = markdown;
		}

		public IQueryable<Post> All()
		{
			return Context.Posts.Include(p => p.Blog);
		}

		public Post Find(int id)
		{
			return Context.Posts.Find(id);
		}

		public void Add(Post post)
		{
			post.DateCreated = DateTime.Now;
			post.ContentHTML = Markdown.ToHtml(post.Content);

			Context.Posts.Add(post);
			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Update(Post p)
		{
			var post = Find(p.Id);

			post.Title = p.Title;
			post.Published = p.Published;
			post.Content = p.Content;
			post.ContentHTML = Markdown.ToHtml(post.Content);

			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Delete(Post post)
		{
			Context.Posts.Remove(post);
			Context.SaveChanges(); // todo move this line out of here.
		}
	}
}