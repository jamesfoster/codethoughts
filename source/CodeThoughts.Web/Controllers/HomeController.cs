namespace CodeThoughts.Controllers
{
	using System.Linq;
	using System.Web.Mvc;
	using Data;

	public class HomeController : Controller
	{
		public IPostRepository Posts { get; set; }

		public HomeController(IPostRepository postRepository)
		{
			Posts = postRepository;
		}

		public ActionResult Index()
		{
			var posts = Posts
				.All()
				.OrderByDescending(p => p.DateCreated)
				.Take(10)
				.ToList();

			return View(posts);
		}

		ActionResult About()
		{
			ViewBag.Message = "Your app description page.";

			return View();
		}

		ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}