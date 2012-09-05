namespace CodeThoughts.Areas.Admin.Controllers
{
	using System.Web.Mvc;
	using Models;

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
		// GET: /Admin/Comment/

		public ActionResult Index()
		{
			return View(Comments.All());
		}

		//
		// GET: /Admin/Comment/Details/5

		public ActionResult Details(int id = 0)
		{
			Comment comment = Comments.Find(id);
			if (comment == null)
			{
				return HttpNotFound();
			}
			return View(comment);
		}

		//
		// GET: /Admin/Comment/Create

		public ActionResult Create()
		{
			ViewBag.PostId = new SelectList(Posts.All(), "Id", "Title");
			return View();
		}

		//
		// POST: /Admin/Comment/Create

		[HttpPost]
		public ActionResult Create(Comment comment)
		{
			if (ModelState.IsValid)
			{
				Comments.Add(comment);
				return RedirectToAction("Index");
			}

			ViewBag.PostId = new SelectList(Posts.All(), "Id", "Title", comment.PostId);
			return View(comment);
		}

		//
		// GET: /Admin/Comment/Edit/5

		public ActionResult Edit(int id = 0)
		{
			Comment comment = Comments.Find(id);
			if (comment == null)
			{
				return HttpNotFound();
			}
			ViewBag.PostId = new SelectList(Posts.All(), "Id", "Title", comment.PostId);
			return View(comment);
		}

		//
		// POST: /Admin/Comment/Edit/5

		[HttpPost]
		public ActionResult Edit(Comment comment)
		{
			if (ModelState.IsValid)
			{
				Comments.Update(comment);
				return RedirectToAction("Index");
			}
			ViewBag.PostId = new SelectList(Posts.All(), "Id", "Title", comment.PostId);
			return View(comment);
		}

		//
		// GET: /Admin/Comment/Delete/5

		public ActionResult Delete(int id = 0)
		{
			Comment comment = Comments.Find(id);
			if (comment == null)
			{
				return HttpNotFound();
			}
			return View(comment);
		}

		//
		// POST: /Admin/Comment/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Comment comment = Comments.Find(id);
			Comments.Delete(comment);
			return RedirectToAction("Index");
		}
	}
}