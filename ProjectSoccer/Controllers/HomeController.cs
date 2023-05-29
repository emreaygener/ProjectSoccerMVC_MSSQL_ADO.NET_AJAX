using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectSoccer.DataAccessLayer.Repositories;
using ProjectSoccer.Models;
using System.Diagnostics;

namespace ProjectSoccer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClubRepository _clubRepository;
        private readonly PlayerRepository _playerRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ClubRepository clubRepository, PlayerRepository playerRepository, ILogger<HomeController> logger)
        {
            _clubRepository = clubRepository;
            _playerRepository = playerRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AboutMe()
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