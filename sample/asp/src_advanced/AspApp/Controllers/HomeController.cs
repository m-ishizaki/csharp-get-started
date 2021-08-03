using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspApp.Models;

namespace AspApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(IndexViewModel model)
        {
            if (!model.Game1.HasAnswer() || !model.Game2.HasAnswer())
            {
                model.Game1.Answer = new System.Random().Next(1, 9);
                model.Game2.Answer = new System.Random().Next(1, 9);
            }
            else if (!int.TryParse(model.Text, out var input))
            {
                model.Message = $"{model.Text} → 数字を入力してください";
            }
            else if (input < 1 || input > 9)
            {
                model.Message = $"{input} → 1-9 の数字を 1 文字入力してください";
            }
            else
            {
                model.Message = $"{model.Game1.Proceed(input)}\n{model.Game2.Proceed(input)}";
            }
            if (model.Game1.Cleared && model.Game2.Cleared)
            {
                model.Message += "\nゲームクリア！ ";
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
