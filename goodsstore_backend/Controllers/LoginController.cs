using Microsoft.AspNetCore.Mvc;

namespace goodsstore_backend.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Authorization(string login, string password)
        {
            if (login == "123" && password == "123")
            {

            }

            return $"{login}, {password}";
        }
    }
}
