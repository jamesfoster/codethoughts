namespace CodeThoughts.Controllers
{
	using System.Web.Mvc;
	using Data;
	using Model;

	public class CommentController : Controller
	{
		public ICommentRepository Comments { get; set; }
		public IPostRepository Posts { get; set; }

		public CommentController(ICommentRepository commentRepository, IPostRepository postRepository)
		{
			Comments = commentRepository;
			Posts = postRepository;
		}

		//
		// GET: /Comment/

		public ActionResult Index()
		{
			return View(Comments.All());
		}

		//
		// GET: /Comment/Details/5

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
		// GET: /Comment/Create

		public ActionResult Create()
		{
			ViewBag.PostId = new SelectList(Posts.All(), "Id", "Title");
			return View();
		}

		//
		// POST: /Comment/Create

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
	}
}