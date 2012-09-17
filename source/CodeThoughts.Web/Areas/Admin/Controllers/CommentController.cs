namespace CodeThoughts.Areas.Admin.Controllers
{
	using System.Web.Mvc;
	using Data;
	using Model;

	public class CommentController : AdminController
	{
		public ICommentRepository Comments { get; set; }
		public IPostRepository Posts { get; set; }

		public CommentController(ICommentRepository commentRepository, IPostRepository postRepository)
		{
			Comments = commentRepository;
			Posts = postRepository;
		}

		//
		// GET: /admin/blog/hello-world/comments/

		public ActionResult Index()
		{
			return View(Comments.All());
		}

		//
		// GET: /admin/blog/hello-world/comments/5

		public ActionResult Show(int id = 0)
		{
			var comment = Comments.Find(id);

			if (comment == null)
				return HttpNotFound();

			return View(comment);
		}

		//
		// GET: /admin/blog/hello-world/comments/New

		public ActionResult New(string postId)
		{
			ViewBag.PostId = new SelectList(Posts.All(), "Id", "Title");
			return View();
		}

		//
		// POST: /admin/blog/hello-world/comments/

		[HttpPost]
		public ActionResult Create(string postId, Comment comment)
		{
			if (ModelState.IsValid)
			{
				var post = Posts.FindByUrl(postId);
				comment.Post = post;

				Comments.Add(comment);
				return RedirectToAction("Index");
			}

			ViewBag.PostId = new SelectList(Posts.All(), "Id", "Title", comment.PostId);
			return View("New", comment);
		}

		//
		// GET: /admin/blog/hello-world/comments/5/Edit

		public ActionResult Edit(string postId, int id = 0)
		{
			var comment = Comments.Find(id);

			if (comment == null)
				return HttpNotFound();

			ViewBag.PostId = new SelectList(Posts.All(), "Id", "Title", comment.PostId);
			return View(comment);
		}

		//
		// PUT: /admin/blog/hello-world/comments/5

		[HttpPut]
		public ActionResult Update(string postId, Comment comment)
		{
			if (ModelState.IsValid)
			{
				Comments.Update(comment);
				return RedirectToAction("Index");
			}

			ViewBag.PostId = new SelectList(Posts.All(), "Id", "Title", comment.PostId);
			return View("Edit", comment);
		}

		//
		// GET: /admin/blog/hello-world/comments/5/delete

		public ActionResult Delete(string postId, int id = 0)
		{
			var comment = Comments.Find(id);

			if (comment == null)
				return HttpNotFound();

			return View(comment);
		}

		//
		// DELETE: /admin/blog/hello-world/comments/5

		[HttpDelete]
		public ActionResult Destroy(string postId, int id)
		{
			var comment = Comments.Find(id);

			Comments.Delete(comment);

			return RedirectToAction("Index");
		}
	}
}