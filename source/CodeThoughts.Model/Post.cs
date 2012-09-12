namespace CodeThoughts.Model
{
	using System;
	using System.Collections.Generic;

	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime DateCreated { get; set; }
		public bool Published { get; set; }

		public string Content { get; set; }
		public string ContentHTML { get; set; }

		public int BlogId { get; set; }
		public Blog Blog { get; set; }

		public ICollection<Comment> Comments { get; set; }
	}
}