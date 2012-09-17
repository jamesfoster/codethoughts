namespace CodeThoughts.Data
{
	using System;
	using System.Data.Entity;
	using System.Linq;
	using System.Text.RegularExpressions;
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

		public Post FindByUrl(string url)
		{
			return Context.Posts.FirstOrDefault(p => p.Url == url.ToLower());
		}

		public void Add(Post post)
		{
			post.DateCreated = DateTime.Now;
			post.ContentHTML = Markdown.ToHtml(post.Content);
			post.Url = GenerateUrl(post);

			Context.Posts.Add(post);
			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Update(Post p)
		{
			var post = Find(p.Id);

			if (!post.Published && p.Published)
				post.DateCreated = DateTime.Now;

			post.Title = p.Title;
			post.Published = p.Published;
			post.Content = p.Content;
			post.ContentHTML = Markdown.ToHtml(post.Content);
			post.Url = GenerateUrl(p);

			Context.SaveChanges(); // todo move this line out of here.
		}

		public void Delete(Post post)
		{
			Context.Posts.Remove(post);
			Context.SaveChanges(); // todo move this line out of here.
		}

		string GenerateUrl(Post post)
		{
			var invalidChars = new Regex(@"([^\w\d]|[_])+");
			
			var url = post.Title.ToLower().Replace("'", "");

			url = invalidChars.Replace(url, "-");

			var posts = Context.Posts.Where(p => p.Url.StartsWith(url)).ToList();

			// 1. no post with the same url
			if (posts.All(p => p.Url != url))
				return url;

			var existing = posts.FirstOrDefault(p => p.Id == post.Id);

			// 2. post with same url is same post.
			if (existing != null && existing.Url == url)
				return url;

			// 3. generate unique url
			var index = 2;
			while (posts.Any(p => p.Url == url + index))
				index++;

			return url + index;
		}
	}
}