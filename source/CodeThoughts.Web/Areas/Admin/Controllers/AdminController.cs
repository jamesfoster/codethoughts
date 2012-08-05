namespace CodeThoughts.Areas.Admin.Controllers
{
	using System.Web.Mvc;

	[Authorize(Roles = "admin")]
	public abstract class AdminController : Controller
	{
	}
}