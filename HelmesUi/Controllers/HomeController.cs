using HelmesUi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HelmesUi.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      var vm = new CreateReactAppViewModel(HttpContext);

      return View(vm);
    }
  }
}
