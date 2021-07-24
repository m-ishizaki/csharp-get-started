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

        public IActionResult Index(string text, int answer)
        {
            if (!(answer > 0 && answer < 10))
            {
                answer = new System.Random().Next(1, 9);
            }
            else if (!int.TryParse(text, out var input))
            {
                ViewData["Message"] = $"{text} → 数字を入力してください";
            }
            else if (input < 1 || input > 9)
            {
                ViewData["Message"] = $"{input} → 1-9 の数字を 1 文字入力してください";
            }
            else if (input > answer)
            {
                ViewData["Message"] = $"{input} → 答えはもっと小さい値です";
            }
            else if (input < answer)
            {
                ViewData["Message"] = $"{input} → 答えはもっと大きい値です";
            }
            else
            {
                ViewData["Message"] = $"{input} → 正解！ ";
            }
            ViewData["Text"] = text;
            ViewData["Answer"] = answer;
            return View();
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
