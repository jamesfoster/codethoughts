namespace CodeThoughts.Data
{
	using System;
	using System.Data;
	using System.Data.Entity;
	using System.Linq;
	using MarkdownSharp;
	using Model;

	public class EfPostRepository : IPostRepository
	{
		public BlogContext Context { get; set; }

		public EfPostRepository(BlogContext context)
		{
			Context = context;
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
			post.ContentHTML = TransformHTML(post.Content);

			Context.Posts.Add(post);
			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Update(Post p)
		{
			var post = Find(p.Id);

			post.Title = p.Title;
			post.Published = p.Published;
			post.Content = p.Content;
			post.ContentHTML = TransformHTML(post.Content);

			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Delete(Post post)
		{
			Context.Posts.Remove(post);
			Context.SaveChanges(); // todo move this line out of here.
		}

		static string TransformHTML(string content)
		{
			var markdown = new Markdown(new MarkdownOptions
				{
					AutoHyperlink = true
				});

			return markdown.Transform(content);
		}
	}
}