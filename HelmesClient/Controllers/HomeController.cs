using HelmesClient.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HelmesClient.Controllers
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
